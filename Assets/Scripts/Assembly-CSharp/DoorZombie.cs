using System;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x020000D4 RID: 212
public class DoorZombie : ArmorZombie
{
	// Token: 0x060003D9 RID: 985 RVA: 0x0001D5E4 File Offset: 0x0001B7E4
	protected override void SecondArmorBroken()
	{
		if (this.theSecondArmorHealth < this.theSecondArmorMaxHealth * 2 / 3 && this.theSecondArmorBroken < 1)
		{
			this.theSecondArmorBroken = 1;
			this.theSecondArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[18];
		}
		if (this.theSecondArmorHealth < this.theSecondArmorMaxHealth / 3 && this.theSecondArmorBroken < 2)
		{
			this.theSecondArmorBroken = 2;
			this.theSecondArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[19];
		}
	}

	// Token: 0x060003DA RID: 986 RVA: 0x0001D664 File Offset: 0x0001B864
	protected override void SecondArmorFall()
	{
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.name == "LoseDoor")
			{
				transform.gameObject.SetActive(true);
				transform.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
				transform.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += this.baseLayer + 29;
			}
		}
		this.anim.SetTrigger("loseDoor");
		this.anim.SetBool("isLoseDoor", true);
		this.loseDoor = true;
	}

	// Token: 0x060003DB RID: 987 RVA: 0x0001D744 File Offset: 0x0001B944
	protected override void BodyTakeDamage(int theDamage)
	{
		this.theHealth -= (float)theDamage;
		if (!this.isLoseHand && this.theHealth < (float)(this.theMaxHealth * 2 / 3) && this.loseDoor)
		{
			this.isLoseHand = true;
			GameAPP.PlaySound(7, 0.5f);
			for (int i = 0; i < base.transform.childCount; i++)
			{
				Transform child = base.transform.GetChild(i);
				if (child.CompareTag("ZombieHand"))
				{
					Object.Destroy(child.gameObject);
				}
				if (child.CompareTag("ZombieArmUpper"))
				{
					child.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[0];
				}
				if (child.name == "LoseArm")
				{
					child.gameObject.SetActive(true);
					child.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
					child.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += this.baseLayer + 29;
					child.gameObject.GetComponent<ParticleSystem>().collision.AddPlane(this.board.transform.GetChild(2 + this.theZombieRow));
					child.AddComponent<ZombieHead>();
				}
			}
		}
		if (this.theHealth < (float)(this.theMaxHealth / 3) && this.theStatus != 1)
		{
			this.theStatus = 1;
			GameAPP.PlaySound(7, 0.5f);
			for (int j = 0; j < base.transform.childCount; j++)
			{
				Transform child2 = base.transform.GetChild(j);
				if (child2.CompareTag("ZombieHead"))
				{
					Object.Destroy(child2.gameObject);
				}
				if (child2.name == "LoseHead")
				{
					child2.gameObject.SetActive(true);
					child2.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
					child2.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += this.baseLayer + 29;
					child2.gameObject.GetComponent<ParticleSystem>().collision.AddPlane(this.board.transform.GetChild(2 + this.theZombieRow));
					child2.AddComponent<ZombieHead>();
					Vector3 localScale = child2.transform.localScale;
					child2.transform.SetParent(this.board.transform);
					child2.transform.localScale = localScale;
				}
			}
			if (!this.loseDoor)
			{
				this.SecondArmorTakeDamage(this.theSecondArmorHealth);
			}
		}
	}

	// Token: 0x040001E0 RID: 480
	private bool loseDoor;
}

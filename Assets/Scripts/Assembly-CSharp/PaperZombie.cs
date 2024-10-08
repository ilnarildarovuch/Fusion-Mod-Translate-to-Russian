using System;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class PaperZombie : ArmorZombie
{
	// Token: 0x060003F4 RID: 1012 RVA: 0x0001E7FC File Offset: 0x0001C9FC
	protected override void Start()
	{
		base.Start();
		this.theStatus = 4;
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x0001E80C File Offset: 0x0001CA0C
	protected override void SecondArmorBroken()
	{
		if (this.theSecondArmorHealth < this.theSecondArmorMaxHealth * 2 / 3 && this.theSecondArmorBroken < 1)
		{
			this.theSecondArmorBroken = 1;
			this.theSecondArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[6];
		}
		if (this.theSecondArmorHealth < this.theSecondArmorMaxHealth / 3 && this.theSecondArmorBroken < 2)
		{
			this.theSecondArmorBroken = 2;
			this.theSecondArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[7];
		}
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x0001E88C File Offset: 0x0001CA8C
	protected override void SecondArmorFall()
	{
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.name == "LosePaper")
			{
				transform.gameObject.SetActive(true);
				transform.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
				transform.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += this.baseLayer + 29;
			}
		}
		GameAPP.PlaySound(44, 0.5f);
		this.anim.SetTrigger("losePaper");
		this.losePaper = true;
		this.theStatus = 5;
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x0001E96C File Offset: 0x0001CB6C
	protected override void BodyTakeDamage(int theDamage)
	{
		this.theHealth -= (float)theDamage;
		if (!this.isLoseHand && this.theHealth < (float)(this.theMaxHealth * 2 / 3) && this.losePaper)
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
					child.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[5];
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
					child2.GetChild(0).gameObject.GetComponent<ParticleSystem>().collision.AddPlane(this.board.transform.GetChild(2 + this.theZombieRow));
					child2.GetChild(0).gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
					child2.GetChild(0).gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += this.baseLayer + 29;
					child2.AddComponent<ZombieHead>();
					Vector3 localScale = child2.transform.localScale;
					child2.transform.SetParent(this.board.transform);
					child2.transform.localScale = localScale;
				}
			}
			if (!this.losePaper)
			{
				this.SecondArmorFall();
			}
		}
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x0001ECA7 File Offset: 0x0001CEA7
	public void AngrySound()
	{
		GameAPP.PlaySound(Random.Range(45, 47), 0.5f);
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x0001ECBC File Offset: 0x0001CEBC
	private void ChangeStatus()
	{
		if (this.theStatus != 1)
		{
			this.theStatus = 6;
		}
	}

	// Token: 0x040001E3 RID: 483
	protected bool losePaper;
}

using System;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x020000D6 RID: 214
public class ElitePaperZombie : PaperZombie
{
	// Token: 0x060003E9 RID: 1001 RVA: 0x0001E028 File Offset: 0x0001C228
	protected override void Start()
	{
		base.Start();
		this.theStatus = 4;
		this.theAttackDamage = 400;
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x0001E044 File Offset: 0x0001C244
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
					child.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[38];
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

	// Token: 0x060003EB RID: 1003 RVA: 0x0001E380 File Offset: 0x0001C580
	public override void TakeDamage(int theDamageType, int theDamage)
	{
		if (this.theStatus == 4)
		{
			if (GameAPP.difficulty > 4 && !this.isMindControlled && theDamage > 0)
			{
				theDamage /= 2;
			}
			if (GameAPP.difficulty == 1 && !this.isMindControlled)
			{
				theDamage += 10;
			}
			this.flashTime = 0.3f;
			if (this.theSecondArmor != null)
			{
				this.SecondArmorTakeDamage(theDamage);
				return;
			}
		}
		else
		{
			if (this.theStatus == 5)
			{
				base.TakeDamage(theDamageType, 0);
				return;
			}
			base.TakeDamage(theDamageType, theDamage);
		}
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x0001E401 File Offset: 0x0001C601
	public override void Charred()
	{
		if (this.theStatus != 6)
		{
			this.TakeDamage(10, 1800);
			return;
		}
		base.Charred();
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x0001E420 File Offset: 0x0001C620
	protected override void SecondArmorBroken()
	{
		if (this.theSecondArmorHealth < this.theSecondArmorMaxHealth * 2 / 3 && this.theSecondArmorBroken < 1)
		{
			this.theSecondArmorBroken = 1;
			this.theSecondArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[42];
		}
		if (this.theSecondArmorHealth < this.theSecondArmorMaxHealth / 3 && this.theSecondArmorBroken < 2)
		{
			this.theSecondArmorBroken = 2;
			this.theSecondArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[43];
		}
	}
}

using System;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x020000DE RID: 222
public class WallNutZ : ArmorZombie
{
	// Token: 0x0600040F RID: 1039 RVA: 0x0001F7F0 File Offset: 0x0001D9F0
	protected override void FirstArmorBroken()
	{
		if ((float)this.theFirstArmorHealth < (float)(this.theFirstArmorMaxHealth * 2) / 3f && this.theFirstArmorBroken < 1)
		{
			this.theFirstArmorBroken = 1;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[8];
		}
		if ((float)this.theFirstArmorHealth < (float)this.theFirstArmorMaxHealth / 3f && this.theFirstArmorBroken < 2)
		{
			this.theFirstArmorBroken = 2;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[9];
		}
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x0001F87C File Offset: 0x0001DA7C
	protected override void BodyTakeDamage(int theDamage)
	{
		this.theHealth -= (float)theDamage;
		if (!this.isLoseHand && this.theHealth < (float)(this.theMaxHealth * 2) / 3f)
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
					child.transform.localScale = new Vector3(4f, 4f, 4f);
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
		if (this.theHealth < (float)this.theMaxHealth / 3f && this.theStatus != 1)
		{
			this.theStatus = 1;
			for (int j = 0; j < base.transform.childCount; j++)
			{
				Transform child2 = base.transform.GetChild(j);
				if (child2.CompareTag("ZombieHead"))
				{
					Object.Destroy(child2.gameObject);
				}
			}
		}
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x0001FA50 File Offset: 0x0001DC50
	protected override int FirstArmorTakeDamage(int theDamage)
	{
		if (theDamage < this.theFirstArmorHealth)
		{
			this.theFirstArmorHealth -= theDamage;
			this.FirstArmorBroken();
			return 0;
		}
		int result = theDamage - this.theFirstArmorHealth;
		this.theFirstArmorHealth = 0;
		this.theFirstArmorType = 0;
		this.theFirstArmor = null;
		return result;
	}
}

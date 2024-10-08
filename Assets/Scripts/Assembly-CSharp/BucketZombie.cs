using System;
using UnityEngine;

// Token: 0x020000CE RID: 206
public class BucketZombie : ArmorZombie
{
	// Token: 0x060003C6 RID: 966 RVA: 0x0001CEE0 File Offset: 0x0001B0E0
	protected override void FirstArmorBroken()
	{
		if (this.theFirstArmorHealth < this.theFirstArmorMaxHealth * 2 / 3 && this.theFirstArmorBroken < 1)
		{
			this.theFirstArmorBroken = 1;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[3];
		}
		if (this.theFirstArmorHealth < this.theFirstArmorMaxHealth / 3 && this.theFirstArmorBroken < 2)
		{
			this.theFirstArmorBroken = 2;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[4];
		}
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x0001CF60 File Offset: 0x0001B160
	protected override void FirstArmorFall()
	{
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.name == "LoseBucket")
			{
				transform.gameObject.SetActive(true);
				transform.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
				transform.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += this.baseLayer + 29;
				transform.gameObject.GetComponent<ParticleSystem>().collision.AddPlane(this.board.transform.GetChild(2 + this.theZombieRow));
			}
		}
	}
}

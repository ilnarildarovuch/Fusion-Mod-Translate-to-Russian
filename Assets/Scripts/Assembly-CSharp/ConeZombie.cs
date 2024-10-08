using System;
using UnityEngine;

// Token: 0x020000D1 RID: 209
public class ConeZombie : ArmorZombie
{
	// Token: 0x060003CD RID: 973 RVA: 0x0001D134 File Offset: 0x0001B334
	protected override void FirstArmorBroken()
	{
		if (this.theFirstArmorHealth < this.theFirstArmorMaxHealth * 2 / 3 && this.theFirstArmorBroken < 1)
		{
			this.theFirstArmorBroken = 1;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[1];
		}
		if (this.theFirstArmorHealth < this.theFirstArmorMaxHealth / 3 && this.theFirstArmorBroken < 2)
		{
			this.theFirstArmorBroken = 2;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[2];
		}
	}

	// Token: 0x060003CE RID: 974 RVA: 0x0001D1B4 File Offset: 0x0001B3B4
	protected override void FirstArmorFall()
	{
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.name == "LoseCone")
			{
				transform.gameObject.SetActive(true);
				transform.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
				transform.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += this.baseLayer + 29;
				transform.gameObject.GetComponent<ParticleSystem>().collision.AddPlane(this.board.transform.GetChild(2 + this.theZombieRow));
			}
		}
	}
}

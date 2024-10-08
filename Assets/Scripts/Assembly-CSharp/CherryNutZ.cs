using System;
using UnityEngine;

// Token: 0x020000D0 RID: 208
public class CherryNutZ : WallNutZ
{
	// Token: 0x060003CB RID: 971 RVA: 0x0001D0A0 File Offset: 0x0001B2A0
	protected override void FirstArmorBroken()
	{
		if ((float)this.theFirstArmorHealth < (float)(this.theFirstArmorMaxHealth * 2) / 3f && this.theFirstArmorBroken < 1)
		{
			this.theFirstArmorBroken = 1;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[16];
		}
		if ((float)this.theFirstArmorHealth < (float)this.theFirstArmorMaxHealth / 3f && this.theFirstArmorBroken < 2)
		{
			this.theFirstArmorBroken = 2;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[17];
		}
	}
}

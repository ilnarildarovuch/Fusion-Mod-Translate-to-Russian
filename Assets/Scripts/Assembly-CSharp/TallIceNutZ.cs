using System;
using UnityEngine;

// Token: 0x020000DC RID: 220
public class TallIceNutZ : WallNutZ
{
	// Token: 0x06000408 RID: 1032 RVA: 0x0001F4D0 File Offset: 0x0001D6D0
	protected override void FirstArmorBroken()
	{
		if (this.theFirstArmorHealth < this.theFirstArmorMaxHealth * 2 / 3 && this.theFirstArmorBroken < 1)
		{
			this.theFirstArmorBroken = 1;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[27];
		}
		if (this.theFirstArmorHealth < this.theFirstArmorMaxHealth / 3 && this.theFirstArmorBroken < 2)
		{
			this.theFirstArmorBroken = 2;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[28];
		}
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x0001F54F File Offset: 0x0001D74F
	public override void SetFreeze(float time)
	{
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x0001F551 File Offset: 0x0001D751
	public override void SetCold(float time)
	{
	}
}

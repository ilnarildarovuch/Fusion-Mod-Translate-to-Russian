using System;
using UnityEngine;

// Token: 0x020000D9 RID: 217
public class RandomPlusZombie : RandomZombie
{
	// Token: 0x060003FB RID: 1019 RVA: 0x0001ECD8 File Offset: 0x0001CED8
	protected override void FirstArmorBroken()
	{
		if (this.theFirstArmorHealth < this.theFirstArmorMaxHealth * 2 / 3 && this.theFirstArmorBroken < 1)
		{
			this.theFirstArmorBroken = 1;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[24];
		}
		if (this.theFirstArmorHealth < this.theFirstArmorMaxHealth / 3 && this.theFirstArmorBroken < 2)
		{
			this.theFirstArmorBroken = 2;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[25];
		}
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x0001ED58 File Offset: 0x0001CF58
	protected override void RandomEvent(Zombie zombie)
	{
		float num = (float)5 / 5f;
		zombie.theHealth *= num;
		zombie.theMaxHealth = (int)((float)zombie.theMaxHealth * num);
		zombie.theFirstArmorHealth = (int)((float)zombie.theFirstArmorHealth * num);
		zombie.theFirstArmorMaxHealth = (int)((float)zombie.theFirstArmorMaxHealth * num);
		zombie.theSecondArmorHealth = (int)((float)zombie.theSecondArmorHealth * num);
		zombie.theSecondArmorMaxHealth = (int)((float)zombie.theSecondArmorMaxHealth * num);
	}
}

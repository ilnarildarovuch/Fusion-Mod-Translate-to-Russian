using System;
using UnityEngine;

// Token: 0x020000CC RID: 204
public class ArmorZombie : Zombie
{
	// Token: 0x060003BC RID: 956 RVA: 0x0001CB40 File Offset: 0x0001AD40
	protected virtual void FirstArmorBroken()
	{
	}

	// Token: 0x060003BD RID: 957 RVA: 0x0001CB42 File Offset: 0x0001AD42
	protected virtual void SecondArmorBroken()
	{
	}

	// Token: 0x060003BE RID: 958 RVA: 0x0001CB44 File Offset: 0x0001AD44
	protected virtual void FirstArmorFall()
	{
	}

	// Token: 0x060003BF RID: 959 RVA: 0x0001CB46 File Offset: 0x0001AD46
	protected virtual void SecondArmorFall()
	{
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x0001CB48 File Offset: 0x0001AD48
	protected override int FirstArmorTakeDamage(int theDamage)
	{
		if (theDamage < this.theFirstArmorHealth)
		{
			this.theFirstArmorHealth -= theDamage;
			this.FirstArmorBroken();
			return 0;
		}
		int result = theDamage - this.theFirstArmorHealth;
		Object.Destroy(this.theFirstArmor);
		this.FirstArmorFall();
		this.theFirstArmorHealth = 0;
		this.theFirstArmorType = 0;
		this.theFirstArmor = null;
		return result;
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x0001CBA8 File Offset: 0x0001ADA8
	protected override int SecondArmorTakeDamage(int theDamage)
	{
		if (theDamage < this.theSecondArmorHealth)
		{
			this.theSecondArmorHealth -= theDamage;
			this.SecondArmorBroken();
			return 0;
		}
		int result = theDamage - this.theSecondArmorHealth;
		Object.Destroy(this.theSecondArmor);
		this.SecondArmorFall();
		this.theSecondArmorHealth = 0;
		this.theSecondArmorType = 0;
		this.theSecondArmor = null;
		return result;
	}
}

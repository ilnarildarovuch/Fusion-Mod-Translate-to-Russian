using System;
using UnityEngine;

// Token: 0x02000046 RID: 70
public class SmallSun : Pea
{
	// Token: 0x06000150 RID: 336 RVA: 0x0000ADC4 File Offset: 0x00008FC4
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, this.theBulletDamage);
		this.PlaySound(component);
		base.GetComponent<Coin>().enabled = true;
		Object.Destroy(this);
	}
}

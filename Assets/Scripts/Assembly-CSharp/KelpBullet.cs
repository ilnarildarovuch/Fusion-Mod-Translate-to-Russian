using System;
using UnityEngine;

// Token: 0x0200003D RID: 61
public class KelpBullet : Bullet
{
	// Token: 0x06000136 RID: 310 RVA: 0x0000A548 File Offset: 0x00008748
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, this.theBulletDamage);
		if (component.inWater)
		{
			component.SetGrap(2f);
		}
		this.PlaySound(component);
		this.Die();
	}
}

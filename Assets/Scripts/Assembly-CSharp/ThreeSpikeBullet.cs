using System;
using UnityEngine;

// Token: 0x0200004B RID: 75
public class ThreeSpikeBullet : Bullet
{
	// Token: 0x06000160 RID: 352 RVA: 0x0000B3C0 File Offset: 0x000095C0
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(4, 5);
		DriverZombie driverZombie;
		if (component.gameObject.TryGetComponent<DriverZombie>(out driverZombie))
		{
			driverZombie.TakeDamage(4, (int)((float)driverZombie.theMaxHealth * 0.3f));
			this.Die();
		}
		else
		{
			this.hasHitTarget = false;
		}
		this.PlaySound(component);
	}
}

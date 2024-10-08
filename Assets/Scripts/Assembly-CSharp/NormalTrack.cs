using System;
using UnityEngine;

// Token: 0x0200003E RID: 62
public class NormalTrack : TrackBullet
{
	// Token: 0x06000138 RID: 312 RVA: 0x0000A594 File Offset: 0x00008794
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, this.theBulletDamage);
		this.PlaySound(component);
		this.Die();
	}
}

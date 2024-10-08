using System;
using UnityEngine;

// Token: 0x0200004C RID: 76
public class TrackBullet : Bullet
{
	// Token: 0x06000162 RID: 354 RVA: 0x0000B41E File Offset: 0x0000961E
	protected override void CheckZombie(GameObject zombie)
	{
		if (zombie == this.zombie)
		{
			this.hasHitTarget = true;
			this.HitZombie(zombie);
		}
	}

	// Token: 0x06000163 RID: 355 RVA: 0x0000B43C File Offset: 0x0000963C
	protected override void HitZombie(GameObject zombie)
	{
		zombie.GetComponent<Zombie>().TakeDamage(0, this.theBulletDamage);
		GameAPP.PlaySound(80, 0.5f);
		this.Die();
	}

	// Token: 0x06000164 RID: 356 RVA: 0x0000B462 File Offset: 0x00009662
	protected override void OnTriggerEnter2D(Collider2D collision)
	{
	}

	// Token: 0x06000165 RID: 357 RVA: 0x0000B464 File Offset: 0x00009664
	protected void OnTriggerStay2D(Collider2D collision)
	{
		if (this.hasHitTarget)
		{
			return;
		}
		if (collision.CompareTag("Zombie"))
		{
			this.CheckZombie(collision.gameObject);
		}
	}
}

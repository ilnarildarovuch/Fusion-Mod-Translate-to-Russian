using System;
using UnityEngine;

// Token: 0x02000038 RID: 56
public class IceDoomBullet : Bullet
{
	// Token: 0x06000126 RID: 294 RVA: 0x00009DA0 File Offset: 0x00007FA0
	protected override void HitZombie(GameObject zombie)
	{
		zombie.GetComponent<Zombie>().TakeDamage(3, this.theBulletDamage);
		PoolMgr.Instance.SpawnParticle(base.transform.position, 28);
		GameAPP.PlaySound(70, 0.5f);
		this.AttackZombie();
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00009DE0 File Offset: 0x00007FE0
	private void AttackZombie()
	{
		foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(base.transform.position, 1.5f, this.zombieLayer))
		{
			Zombie zombie;
			if (collider2D != null && collider2D.TryGetComponent<Zombie>(out zombie))
			{
				if (zombie.theStatus == 7)
				{
					return;
				}
				PolevaulterZombie polevaulterZombie;
				if (Mathf.Abs(zombie.theZombieRow - this.theBulletRow) <= 1 && !zombie.isMindControlled && (!zombie.gameObject.TryGetComponent<PolevaulterZombie>(out polevaulterZombie) || polevaulterZombie.polevaulterStatus != 1))
				{
					zombie.TakeDamage(1, 10);
				}
			}
		}
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00009E7C File Offset: 0x0000807C
	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (this.hitTimes < 3 && collision.CompareTag("Zombie"))
		{
			PolevaulterZombie polevaulterZombie;
			if (collision.TryGetComponent<PolevaulterZombie>(out polevaulterZombie) && polevaulterZombie.polevaulterStatus == 1)
			{
				return;
			}
			Zombie component = collision.GetComponent<Zombie>();
			if (component.theZombieRow != this.theBulletRow || component.isMindControlled)
			{
				return;
			}
			if (component.theStatus == 7)
			{
				return;
			}
			foreach (GameObject x in this.Z)
			{
				if (x != null && x == this.zombie)
				{
					return;
				}
			}
			this.hitTimes++;
			this.Z.Add(collision.gameObject);
			this.HitZombie(collision.gameObject);
			if (this.hitTimes == 3)
			{
				this.Die();
			}
		}
	}
}

using System;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class FireKelpBullet : Bullet
{
	// Token: 0x0600011D RID: 285 RVA: 0x000098CC File Offset: 0x00007ACC
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, this.theBulletDamage);
		if (component.inWater)
		{
			component.SetGrap(2f);
		}
		component.Warm(1);
		if (this.AllowSputter(component))
		{
			GameAPP.PlaySound(Random.Range(59, 61), 0.5f);
			PoolMgr.Instance.SpawnParticle(base.transform.position, 33).GetComponent<SpriteRenderer>().sortingLayerName = string.Format("particle{0}", this.theBulletRow);
			this.AttackOtherZombie(component);
		}
		else
		{
			this.PlaySound(component);
		}
		this.Die();
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00009970 File Offset: 0x00007B70
	private void AttackOtherZombie(Zombie zombie)
	{
		int theBulletDamage = this.theBulletDamage;
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 1f);
		for (int i = 0; i < array.Length; i++)
		{
			Zombie zombie2;
			if (array[i].TryGetComponent<Zombie>(out zombie2) && !(zombie2 == zombie) && zombie2.theZombieRow == this.theBulletRow && !zombie2.isMindControlled && this.AllowSputter(zombie2))
			{
				this.zombieToFired.Add(zombie2);
			}
		}
		int count = this.zombieToFired.Count;
		if (count == 0)
		{
			return;
		}
		int num = theBulletDamage / count;
		if (num == 0)
		{
			num = 1;
		}
		if ((float)num > 0.33333334f * (float)this.theBulletDamage)
		{
			num = (int)(0.33333334f * (float)this.theBulletDamage);
		}
		foreach (Zombie zombie3 in this.zombieToFired)
		{
			zombie3.TakeDamage(0, num);
			if (zombie3.inWater)
			{
				zombie3.SetGrap(2f);
			}
			zombie3.Warm(1);
		}
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00009A98 File Offset: 0x00007C98
	private bool AllowSputter(Zombie zombie)
	{
		return zombie.theSecondArmorType != 2 && zombie.theZombieType != 14;
	}
}

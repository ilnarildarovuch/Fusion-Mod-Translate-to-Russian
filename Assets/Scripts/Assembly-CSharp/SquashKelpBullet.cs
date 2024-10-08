using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
public class SquashKelpBullet : SquashBullet
{
	// Token: 0x0600015A RID: 346 RVA: 0x0000B104 File Offset: 0x00009304
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, this.theBulletDamage);
		if (component.inWater)
		{
			component.SetGrap(2f);
		}
		this.theMovingWay = -1;
		this.Vy *= -0.75f;
		base.GetComponent<BoxCollider2D>().enabled = false;
		this.originY = this.shadow.transform.position.y;
		this.PlaySound(component);
		if (Board.Instance.isEveStarted)
		{
			base.SetShadowPosition();
		}
		this.landY = this.shadow.transform.position.y + 0.3f;
	}

	// Token: 0x0600015B RID: 347 RVA: 0x0000B1B4 File Offset: 0x000093B4
	protected override void AttackZombie()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 0.5f);
		bool flag = false;
		Collider2D[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			Zombie zombie;
			if (array2[i].TryGetComponent<Zombie>(out zombie) && zombie.theZombieRow == this.theBulletRow && !zombie.isMindControlled)
			{
				zombie.TakeDamage(1, this.theBulletDamage);
				if (zombie.inWater)
				{
					zombie.SetGrap(2f);
				}
				flag = true;
			}
		}
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
		}
	}
}

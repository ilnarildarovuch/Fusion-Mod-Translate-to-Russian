using System;
using UnityEngine;

// Token: 0x02000085 RID: 133
public class FireSpike : Plant
{
	// Token: 0x060002BF RID: 703 RVA: 0x00016580 File Offset: 0x00014780
	protected override void Update()
	{
		base.Update();
		this.FireUpdate();
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x00016590 File Offset: 0x00014790
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Bullet bullet;
		if (collision.TryGetComponent<Bullet>(out bullet))
		{
			if (bullet.torchWood == base.gameObject || bullet.isZombieBullet)
			{
				return;
			}
			if (bullet.theMovingWay != 2 && bullet.theBulletRow != this.thePlantRow)
			{
				return;
			}
			if (bullet.theBulletType == 10)
			{
				Board.Instance.FirePuffPea(bullet, this);
			}
		}
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x000165F1 File Offset: 0x000147F1
	private void FireUpdate()
	{
		if (this.thePlantAttackCountDown > 0f)
		{
			this.thePlantAttackCountDown -= Time.deltaTime;
			if (this.thePlantAttackCountDown <= 0f)
			{
				this.SummonFire();
				this.thePlantAttackCountDown = this.thePlantAttackInterval;
			}
		}
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x00016634 File Offset: 0x00014834
	private void SummonFire()
	{
		Collider2D[] array = Physics2D.OverlapBoxAll(this.shadow.transform.position, new Vector2(1f, 1f), 0f);
		for (int i = 0; i < array.Length; i++)
		{
			Zombie zombie;
			if (array[i].TryGetComponent<Zombie>(out zombie) && zombie.theZombieRow == this.thePlantRow && !zombie.isMindControlled)
			{
				GameAPP.PlaySound(Random.Range(59, 61), 0.5f);
				int theZombieType = zombie.theZombieType;
				if (theZombieType == 16 || theZombieType == 18 || theZombieType == 201)
				{
					zombie.GetComponent<DriverZombie>().KillByCaltrop();
					this.thePlantHealth = 0;
				}
				else
				{
					zombie.TakeDamage(1, 20);
					zombie.Warm(0);
				}
			}
		}
	}
}

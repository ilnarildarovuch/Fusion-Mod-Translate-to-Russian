using System;
using UnityEngine;

// Token: 0x02000080 RID: 128
public class Caltrop : Plant
{
	// Token: 0x060002A4 RID: 676 RVA: 0x000159B4 File Offset: 0x00013BB4
	protected override void Update()
	{
		base.Update();
		if (this.thePlantAttackCountDown > 0f)
		{
			this.thePlantAttackCountDown -= Time.deltaTime;
			if (this.thePlantAttackCountDown <= 0f)
			{
				this.ReadyToAttack();
				this.thePlantAttackCountDown = this.thePlantAttackInterval + Random.Range(-0.1f, 0.1f);
			}
		}
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x00015A18 File Offset: 0x00013C18
	protected virtual void ReadyToAttack()
	{
		Collider2D[] array = Physics2D.OverlapBoxAll(this.shadow.transform.position, new Vector2(1f, 1f), 0f);
		for (int i = 0; i < array.Length; i++)
		{
			Zombie zombie;
			if (array[i].TryGetComponent<Zombie>(out zombie) && base.SearchUniqueZombie(zombie) && zombie.theZombieRow == this.thePlantRow)
			{
				this.anim.SetTrigger("attack");
			}
		}
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x00015A98 File Offset: 0x00013C98
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Zombie zombie;
		if (collision.TryGetComponent<Zombie>(out zombie) && zombie.theStatus != 1 && zombie.theZombieRow == this.thePlantRow && !zombie.isMindControlled)
		{
			int theZombieType = zombie.theZombieType;
			if (theZombieType == 16 || theZombieType == 18 || theZombieType == 201)
			{
				this.anim.SetTrigger("attack");
			}
		}
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x00015AF8 File Offset: 0x00013CF8
	protected virtual void KillCar()
	{
		Collider2D[] array = Physics2D.OverlapBoxAll(this.shadow.transform.position, new Vector2(1f, 1f), 0f);
		for (int i = 0; i < array.Length; i++)
		{
			DriverZombie driverZombie;
			if (array[i].TryGetComponent<DriverZombie>(out driverZombie) && driverZombie.theZombieRow == this.thePlantRow && !driverZombie.isMindControlled && driverZombie.theStatus != 1)
			{
				driverZombie.KillByCaltrop();
				GameAPP.PlaySound(77, 0.5f);
				this.Die();
			}
		}
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x00015B88 File Offset: 0x00013D88
	protected virtual void AnimAttack()
	{
		this.KillCar();
		Collider2D[] array = Physics2D.OverlapBoxAll(this.shadow.transform.position, new Vector2(1f, 1f), 0f);
		bool flag = false;
		Collider2D[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			Zombie zombie;
			if (array2[i].TryGetComponent<Zombie>(out zombie) && zombie.theZombieRow == this.thePlantRow && base.SearchUniqueZombie(zombie))
			{
				flag = true;
				zombie.TakeDamage(4, 20);
			}
		}
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
		}
	}
}

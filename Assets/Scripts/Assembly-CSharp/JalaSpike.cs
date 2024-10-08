using System;
using UnityEngine;

// Token: 0x0200008D RID: 141
public class JalaSpike : Caltrop
{
	// Token: 0x060002D6 RID: 726 RVA: 0x00016D58 File Offset: 0x00014F58
	protected override void KillCar()
	{
		Collider2D[] array = Physics2D.OverlapBoxAll(this.shadow.transform.position, new Vector2(1f, 1f), 0f);
		for (int i = 0; i < array.Length; i++)
		{
			DriverZombie driverZombie;
			if (array[i].TryGetComponent<DriverZombie>(out driverZombie) && driverZombie.theZombieRow == this.thePlantRow && !driverZombie.isMindControlled && driverZombie.theStatus != 1)
			{
				driverZombie.Die(2);
				GameAPP.PlaySound(77, 0.5f);
				this.Die();
			}
		}
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x00016DE8 File Offset: 0x00014FE8
	protected override void AnimAttack()
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
				zombie.SetJalaed();
			}
		}
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
		}
	}
}

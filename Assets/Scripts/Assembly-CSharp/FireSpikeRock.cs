using System;
using UnityEngine;

// Token: 0x02000086 RID: 134
public class FireSpikeRock : SpikeRock
{
	// Token: 0x060002C4 RID: 708 RVA: 0x000166F8 File Offset: 0x000148F8
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

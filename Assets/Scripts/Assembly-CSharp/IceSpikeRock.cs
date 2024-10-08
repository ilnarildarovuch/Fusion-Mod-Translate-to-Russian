using System;
using UnityEngine;

// Token: 0x0200008C RID: 140
public class IceSpikeRock : SpikeRock
{
	// Token: 0x060002D4 RID: 724 RVA: 0x00016CB4 File Offset: 0x00014EB4
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
				zombie.TakeDamage(5, 20);
				zombie.AddfreezeLevel(5);
			}
		}
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
		}
	}
}

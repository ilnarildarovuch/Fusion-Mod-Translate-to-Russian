using System;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class DoomPuff : Plant
{
	// Token: 0x0600020C RID: 524 RVA: 0x00010DA8 File Offset: 0x0000EFA8
	private void OnTriggerStay2D(Collider2D collision)
	{
		Zombie zombie;
		if (collision.TryGetComponent<Zombie>(out zombie) && zombie.theZombieRow == this.thePlantRow && zombie.theStatus != 1 && !zombie.isMindControlled)
		{
			GameAPP.PlaySound(41, 0.5f);
			ScreenShake.TriggerShake(0.1f);
			Object.Instantiate<GameObject>(GameAPP.particlePrefab[27], this.shadow.transform.position, Quaternion.identity, this.board.transform);
			this.AttackZombie();
			this.Die();
		}
	}

	// Token: 0x0600020D RID: 525 RVA: 0x00010E30 File Offset: 0x0000F030
	private void AttackZombie()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(this.shadow.transform.position, 1f);
		for (int i = 0; i < array.Length; i++)
		{
			Zombie zombie;
			if (array[i].gameObject.TryGetComponent<Zombie>(out zombie) && zombie.theZombieRow == this.thePlantRow && !zombie.isMindControlled)
			{
				if (zombie.theHealth > 1800f)
				{
					zombie.TakeDamage(10, 1800);
				}
				else
				{
					zombie.Charred();
				}
			}
		}
	}
}

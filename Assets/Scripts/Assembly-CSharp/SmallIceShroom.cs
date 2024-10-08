using System;
using UnityEngine;

// Token: 0x02000091 RID: 145
public class SmallIceShroom : Plant
{
	// Token: 0x060002ED RID: 749 RVA: 0x00017928 File Offset: 0x00015B28
	private void OnTriggerStay2D(Collider2D collision)
	{
		Zombie zombie;
		if (collision.TryGetComponent<Zombie>(out zombie) && zombie.theZombieRow == this.thePlantRow && zombie.theStatus != 1 && !zombie.isMindControlled && zombie.theFreezeCountDown == 0f)
		{
			PolevaulterZombie polevaulterZombie;
			if (zombie.TryGetComponent<PolevaulterZombie>(out polevaulterZombie) && polevaulterZombie.polevaulterStatus == 1)
			{
				return;
			}
			zombie.SetFreeze(4f);
			GameAPP.PlaySound(67, 0.5f);
			Object.Instantiate<GameObject>(GameAPP.particlePrefab[24], this.shadow.transform.position, Quaternion.identity, this.board.transform);
			this.Die();
		}
	}
}

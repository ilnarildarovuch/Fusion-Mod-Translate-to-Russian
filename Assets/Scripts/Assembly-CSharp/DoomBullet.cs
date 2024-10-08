using System;
using UnityEngine;

// Token: 0x02000034 RID: 52
public class DoomBullet : Bullet
{
	// Token: 0x0600011B RID: 283 RVA: 0x00009868 File Offset: 0x00007A68
	protected override void HitZombie(GameObject zombie)
	{
		zombie.GetComponent<Zombie>().TakeDamage(10, this.theBulletDamage);
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[27], base.transform.position, Quaternion.identity, GameAPP.board.transform);
		GameAPP.PlaySound(41, 0.5f);
		this.Die();
	}
}

using System;
using UnityEngine;

// Token: 0x02000039 RID: 57
public class IceSpark : IceDoomBullet
{
	// Token: 0x0600012A RID: 298 RVA: 0x00009F7C File Offset: 0x0000817C
	protected override void HitZombie(GameObject zombie)
	{
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[24], base.transform.position, Quaternion.identity, GameAPP.board.transform);
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(3, this.theBulletDamage);
		this.PlaySound(component);
	}
}

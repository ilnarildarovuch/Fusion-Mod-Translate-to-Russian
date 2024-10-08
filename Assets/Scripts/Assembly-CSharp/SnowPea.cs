using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
public class SnowPea : Pea
{
	// Token: 0x06000152 RID: 338 RVA: 0x0000AE08 File Offset: 0x00009008
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(2, this.theBulletDamage);
		GameObject gameObject = GameAPP.particlePrefab[24];
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, base.transform.position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		this.PlaySound(component);
		this.Die();
	}
}

using System;
using UnityEngine;

// Token: 0x02000042 RID: 66
public class Puff : Bullet
{
	// Token: 0x06000142 RID: 322 RVA: 0x0000A88C File Offset: 0x00008A8C
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, this.theBulletDamage);
		GameObject gameObject = GameAPP.particlePrefab[17];
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, base.transform.position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		this.PlaySound(component);
		this.Die();
	}
}

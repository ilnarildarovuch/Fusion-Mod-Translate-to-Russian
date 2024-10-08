using System;
using UnityEngine;

// Token: 0x02000043 RID: 67
public class PuffBlack : Bullet
{
	// Token: 0x06000144 RID: 324 RVA: 0x0000A904 File Offset: 0x00008B04
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, this.theBulletDamage);
		GameObject gameObject = GameAPP.particlePrefab[26];
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, base.transform.position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		this.PlaySound(component);
		this.Die();
	}
}

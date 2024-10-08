using System;
using UnityEngine;

// Token: 0x02000041 RID: 65
public class PotatoPea : Pea
{
	// Token: 0x06000140 RID: 320 RVA: 0x0000A804 File Offset: 0x00008A04
	protected override void HitZombie(GameObject zombie)
	{
		if (this.isHot)
		{
			base.FireZombie(zombie);
			return;
		}
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, this.theBulletDamage);
		GameObject gameObject = GameAPP.particlePrefab[15];
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, base.transform.position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		this.PlaySound(component);
		this.Die();
	}
}

using System;
using UnityEngine;

// Token: 0x02000032 RID: 50
public class CherryBullet : Bullet
{
	// Token: 0x06000116 RID: 278 RVA: 0x000096C8 File Offset: 0x000078C8
	protected override void HitZombie(GameObject zombie)
	{
		if (this.isHot)
		{
			base.FireZombie(zombie);
			return;
		}
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, this.theBulletDamage);
		GameObject gameObject = GameAPP.particlePrefab[10];
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, base.transform.position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		this.PlaySound(component);
		this.Die();
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00009748 File Offset: 0x00007948
	protected override void HitPlant(GameObject plant)
	{
		Plant component = plant.GetComponent<Plant>();
		component.thePlantHealth -= this.theBulletDamage;
		GameObject gameObject = GameAPP.particlePrefab[10];
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, base.transform.position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
		component.FlashOnce();
		this.Die();
	}
}

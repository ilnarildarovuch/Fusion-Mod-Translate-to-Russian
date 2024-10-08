using System;
using UnityEngine;

// Token: 0x02000040 RID: 64
public class Pea : Bullet
{
	// Token: 0x0600013D RID: 317 RVA: 0x0000A700 File Offset: 0x00008900
	protected override void HitZombie(GameObject zombie)
	{
		if (this.isHot)
		{
			base.FireZombie(zombie);
			return;
		}
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, this.theBulletDamage);
		GameObject gameObject = GameAPP.particlePrefab[0];
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, base.transform.position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		this.PlaySound(component);
		this.Die();
	}

	// Token: 0x0600013E RID: 318 RVA: 0x0000A77C File Offset: 0x0000897C
	protected override void HitPlant(GameObject plant)
	{
		Plant component = plant.GetComponent<Plant>();
		component.thePlantHealth -= this.theBulletDamage;
		GameObject gameObject = GameAPP.particlePrefab[0];
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, base.transform.position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
		component.FlashOnce();
		this.Die();
	}
}

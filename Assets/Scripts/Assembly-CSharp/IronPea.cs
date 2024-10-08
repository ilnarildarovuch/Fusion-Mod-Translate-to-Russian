using System;
using UnityEngine;

// Token: 0x0200003B RID: 59
public class IronPea : Bullet
{
	// Token: 0x06000130 RID: 304 RVA: 0x0000A2B8 File Offset: 0x000084B8
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		if (component.theSecondArmorHealth != 0)
		{
			component.TakeDamage(0, component.theSecondArmorHealth + this.theBulletDamage);
		}
		else
		{
			component.TakeDamage(0, this.theBulletDamage);
		}
		if (component.isMindControlled)
		{
			zombie.transform.position = new Vector3(zombie.transform.position.x - 0.2f, zombie.transform.position.y);
		}
		else
		{
			zombie.transform.position = new Vector3(zombie.transform.position.x + 0.2f, zombie.transform.position.y);
		}
		GameObject gameObject = GameAPP.particlePrefab[18];
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, base.transform.position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		this.PlaySound(component);
		this.Die();
	}

	// Token: 0x06000131 RID: 305 RVA: 0x0000A3BC File Offset: 0x000085BC
	protected override void HitPlant(GameObject plant)
	{
		Plant component = plant.GetComponent<Plant>();
		component.thePlantHealth -= this.theBulletDamage;
		GameObject gameObject = GameAPP.particlePrefab[18];
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, base.transform.position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
		component.FlashOnce();
		this.Die();
	}
}

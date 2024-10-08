using UnityEngine;

public class CherryBullet : Bullet
{
	protected override void HitZombie(GameObject zombie)
	{
		if (isHot)
		{
			FireZombie(zombie);
			return;
		}
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, theBulletDamage);
		GameObject gameObject = GameAPP.particlePrefab[10];
		GameObject obj = Object.Instantiate(gameObject, base.transform.position, Quaternion.identity);
		obj.transform.SetParent(GameAPP.board.transform);
		obj.name = gameObject.name;
		PlaySound(component);
		Die();
	}

	protected override void HitPlant(GameObject plant)
	{
		Plant component = plant.GetComponent<Plant>();
		component.thePlantHealth -= theBulletDamage;
		GameObject gameObject = GameAPP.particlePrefab[10];
		GameObject obj = Object.Instantiate(gameObject, base.transform.position, Quaternion.identity);
		obj.transform.SetParent(GameAPP.board.transform);
		obj.name = gameObject.name;
		GameAPP.PlaySound(Random.Range(0, 3));
		component.FlashOnce();
		Die();
	}
}

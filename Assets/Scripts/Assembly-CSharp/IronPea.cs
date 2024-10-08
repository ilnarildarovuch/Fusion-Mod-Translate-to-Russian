using UnityEngine;

public class IronPea : Bullet
{
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		if (component.theSecondArmorHealth != 0)
		{
			component.TakeDamage(0, component.theSecondArmorHealth + theBulletDamage);
		}
		else
		{
			component.TakeDamage(0, theBulletDamage);
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
		GameObject gameObject = GameAPP.particlePrefab[18];
		GameObject obj = Object.Instantiate(gameObject, base.transform.position, Quaternion.identity);
		obj.transform.SetParent(GameAPP.board.transform);
		obj.name = gameObject.name;
		GameAPP.PlaySound(Random.Range(0, 3));
		component.FlashOnce();
		Die();
	}
}

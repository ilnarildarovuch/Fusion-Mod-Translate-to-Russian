using UnityEngine;

public class PotatoPea : Pea
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
		GameObject gameObject = GameAPP.particlePrefab[15];
		GameObject obj = Object.Instantiate(gameObject, base.transform.position, Quaternion.identity);
		obj.transform.SetParent(GameAPP.board.transform);
		obj.name = gameObject.name;
		PlaySound(component);
		Die();
	}
}

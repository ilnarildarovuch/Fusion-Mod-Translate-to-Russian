using UnityEngine;

public class KelpBullet : Bullet
{
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, theBulletDamage);
		if (component.inWater)
		{
			component.SetGrap(2f);
		}
		PlaySound(component);
		Die();
	}
}

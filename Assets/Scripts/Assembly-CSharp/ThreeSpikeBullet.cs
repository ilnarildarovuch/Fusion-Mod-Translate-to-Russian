using UnityEngine;

public class ThreeSpikeBullet : Bullet
{
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(4, 5);
		if (component.gameObject.TryGetComponent<DriverZombie>(out var component2))
		{
			component2.TakeDamage(4, (int)((float)component2.theMaxHealth * 0.3f));
			Die();
		}
		else
		{
			hasHitTarget = false;
		}
		PlaySound(component);
	}
}

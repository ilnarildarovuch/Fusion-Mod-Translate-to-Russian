using UnityEngine;

public class NormalTrack : TrackBullet
{
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, theBulletDamage);
		PlaySound(component);
		Die();
	}
}

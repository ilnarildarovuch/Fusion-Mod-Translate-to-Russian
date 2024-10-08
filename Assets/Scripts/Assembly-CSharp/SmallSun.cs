using UnityEngine;

public class SmallSun : Pea
{
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, theBulletDamage);
		PlaySound(component);
		GetComponent<Coin>().enabled = true;
		Object.Destroy(this);
	}
}

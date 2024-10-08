using UnityEngine;

public class IceSpark : IceDoomBullet
{
	protected override void HitZombie(GameObject zombie)
	{
		Object.Instantiate(GameAPP.particlePrefab[24], base.transform.position, Quaternion.identity, GameAPP.board.transform);
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(3, theBulletDamage);
		PlaySound(component);
	}
}

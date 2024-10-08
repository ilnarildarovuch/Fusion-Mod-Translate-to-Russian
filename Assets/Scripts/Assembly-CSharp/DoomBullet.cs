using UnityEngine;

public class DoomBullet : Bullet
{
	protected override void HitZombie(GameObject zombie)
	{
		zombie.GetComponent<Zombie>().TakeDamage(10, theBulletDamage);
		Object.Instantiate(GameAPP.particlePrefab[27], base.transform.position, Quaternion.identity, GameAPP.board.transform);
		GameAPP.PlaySound(41);
		Die();
	}
}

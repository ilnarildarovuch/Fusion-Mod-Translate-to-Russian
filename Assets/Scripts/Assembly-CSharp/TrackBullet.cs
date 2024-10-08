using UnityEngine;

public class TrackBullet : Bullet
{
	protected override void CheckZombie(GameObject zombie)
	{
		if (zombie == base.zombie)
		{
			hasHitTarget = true;
			HitZombie(zombie);
		}
	}

	protected override void HitZombie(GameObject zombie)
	{
		zombie.GetComponent<Zombie>().TakeDamage(0, theBulletDamage);
		GameAPP.PlaySound(80);
		Die();
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
	}

	protected void OnTriggerStay2D(Collider2D collision)
	{
		if (!hasHitTarget && collision.CompareTag("Zombie"))
		{
			CheckZombie(collision.gameObject);
		}
	}
}

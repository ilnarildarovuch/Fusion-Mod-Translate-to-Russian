using UnityEngine;

public class IceDoomBullet : Bullet
{
	protected override void HitZombie(GameObject zombie)
	{
		zombie.GetComponent<Zombie>().TakeDamage(3, theBulletDamage);
		PoolMgr.Instance.SpawnParticle(base.transform.position, 28);
		GameAPP.PlaySound(70);
		AttackZombie();
	}

	private void AttackZombie()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 1.5f, zombieLayer);
		foreach (Collider2D collider2D in array)
		{
			if (collider2D != null && collider2D.TryGetComponent<Zombie>(out var component))
			{
				if (component.theStatus == 7)
				{
					break;
				}
				if (Mathf.Abs(component.theZombieRow - theBulletRow) <= 1 && !component.isMindControlled && (!component.gameObject.TryGetComponent<PolevaulterZombie>(out var component2) || component2.polevaulterStatus != 1))
				{
					component.TakeDamage(1, 10);
				}
			}
		}
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (hitTimes >= 3 || !collision.CompareTag("Zombie") || (collision.TryGetComponent<PolevaulterZombie>(out var component) && component.polevaulterStatus == 1))
		{
			return;
		}
		Zombie component2 = collision.GetComponent<Zombie>();
		if (component2.theZombieRow != theBulletRow || component2.isMindControlled || component2.theStatus == 7)
		{
			return;
		}
		foreach (GameObject item in Z)
		{
			if (item != null && item == zombie)
			{
				return;
			}
		}
		hitTimes++;
		Z.Add(collision.gameObject);
		HitZombie(collision.gameObject);
		if (hitTimes == 3)
		{
			Die();
		}
	}
}

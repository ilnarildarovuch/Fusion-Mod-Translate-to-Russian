using UnityEngine;

public class SquashKelpBullet : SquashBullet
{
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, theBulletDamage);
		if (component.inWater)
		{
			component.SetGrap(2f);
		}
		theMovingWay = -1;
		Vy *= -0.75f;
		GetComponent<BoxCollider2D>().enabled = false;
		originY = shadow.transform.position.y;
		PlaySound(component);
		if (Board.Instance.isEveStarted)
		{
			SetShadowPosition();
		}
		landY = shadow.transform.position.y + 0.3f;
	}

	protected override void AttackZombie()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 0.5f);
		bool flag = false;
		Collider2D[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			if (array2[i].TryGetComponent<Zombie>(out var component) && component.theZombieRow == theBulletRow && !component.isMindControlled)
			{
				component.TakeDamage(1, theBulletDamage);
				if (component.inWater)
				{
					component.SetGrap(2f);
				}
				flag = true;
			}
		}
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3));
		}
	}
}

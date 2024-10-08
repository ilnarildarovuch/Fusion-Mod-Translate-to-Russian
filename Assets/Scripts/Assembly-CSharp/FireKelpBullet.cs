using UnityEngine;

public class FireKelpBullet : Bullet
{
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, theBulletDamage);
		if (component.inWater)
		{
			component.SetGrap(2f);
		}
		component.Warm(1);
		if (AllowSputter(component))
		{
			GameAPP.PlaySound(Random.Range(59, 61));
			PoolMgr.Instance.SpawnParticle(base.transform.position, 33).GetComponent<SpriteRenderer>().sortingLayerName = $"particle{theBulletRow}";
			AttackOtherZombie(component);
		}
		else
		{
			PlaySound(component);
		}
		Die();
	}

	private void AttackOtherZombie(Zombie zombie)
	{
		int num = theBulletDamage;
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 1f);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].TryGetComponent<Zombie>(out var component) && !(component == zombie) && component.theZombieRow == theBulletRow && !component.isMindControlled && AllowSputter(component))
			{
				zombieToFired.Add(component);
			}
		}
		int count = zombieToFired.Count;
		if (count == 0)
		{
			return;
		}
		int num2 = num / count;
		if (num2 == 0)
		{
			num2 = 1;
		}
		if ((float)num2 > 1f / 3f * (float)theBulletDamage)
		{
			num2 = (int)(1f / 3f * (float)theBulletDamage);
		}
		foreach (Zombie item in zombieToFired)
		{
			item.TakeDamage(0, num2);
			if (item.inWater)
			{
				item.SetGrap(2f);
			}
			item.Warm(1);
		}
	}

	private bool AllowSputter(Zombie zombie)
	{
		if (zombie.theSecondArmorType == 2)
		{
			return false;
		}
		if (zombie.theZombieType == 14)
		{
			return false;
		}
		return true;
	}
}

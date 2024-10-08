using UnityEngine;

public class FireTrack : TrackBullet
{
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		int num = theBulletDamage;
		if (component.isJalaed)
		{
			num *= 2;
		}
		component.TakeDamage(4, num);
		component.Warm();
		PlaySound(component);
		Die();
	}

	protected override GameObject GetNearestZombie()
	{
		GameObject nearestJalaedZombie = GetNearestJalaedZombie();
		if (nearestJalaedZombie != null)
		{
			return nearestJalaedZombie;
		}
		float num = float.MaxValue;
		GameObject gameObject = null;
		foreach (GameObject item in Board.Instance.zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (!component.isMindControlled && component.theStatus != 1 && component.shadow.transform.position.x < 9.2f && component.TryGetComponent<Collider2D>(out var component2) && Vector2.Distance(component2.bounds.center, base.transform.position) < num)
				{
					gameObject = item;
					num = Vector2.Distance(component2.bounds.center, base.transform.position);
				}
			}
		}
		if (gameObject != null)
		{
			int theZombieRow = gameObject.GetComponent<Zombie>().theZombieRow;
			CreateBullet.Instance.SetLayer(theZombieRow, base.gameObject);
		}
		return gameObject;
	}

	private GameObject GetNearestJalaedZombie()
	{
		float num = float.MaxValue;
		GameObject gameObject = null;
		foreach (GameObject item in Board.Instance.zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (component.isJalaed && !component.isMindControlled && component.theStatus != 1 && component.shadow.transform.position.x < 9.2f && component.TryGetComponent<Collider2D>(out var component2) && Vector2.Distance(component2.bounds.center, base.transform.position) < num)
				{
					gameObject = item;
					num = Vector2.Distance(component2.bounds.center, base.transform.position);
				}
			}
		}
		if (gameObject != null)
		{
			int theZombieRow = gameObject.GetComponent<Zombie>().theZombieRow;
			CreateBullet.Instance.SetLayer(theZombieRow, base.gameObject);
		}
		return gameObject;
	}
}

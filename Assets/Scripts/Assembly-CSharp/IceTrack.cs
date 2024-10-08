using UnityEngine;

public class IceTrack : TrackBullet
{
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		int num = theBulletDamage;
		if (component.freezeSpeed == 0f)
		{
			num *= 4;
		}
		component.TakeDamage(5, num);
		component.AddfreezeLevel(5);
		PlaySound(component);
		Die();
	}

	protected override GameObject GetNearestZombie()
	{
		GameObject nearestFreezedZombie = GetNearestFreezedZombie();
		if (nearestFreezedZombie != null)
		{
			return nearestFreezedZombie;
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

	private GameObject GetNearestFreezedZombie()
	{
		float num = float.MaxValue;
		GameObject gameObject = null;
		foreach (GameObject item in Board.Instance.zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (component.freezeSpeed == 0f && !component.isMindControlled && component.theStatus != 1 && component.shadow.transform.position.x < 9.2f && component.TryGetComponent<Collider2D>(out var component2) && Vector2.Distance(component2.bounds.center, base.transform.position) < num)
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

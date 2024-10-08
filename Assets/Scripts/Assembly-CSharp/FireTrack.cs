using System;
using UnityEngine;

// Token: 0x02000037 RID: 55
public class FireTrack : TrackBullet
{
	// Token: 0x06000122 RID: 290 RVA: 0x00009AC4 File Offset: 0x00007CC4
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		int num = this.theBulletDamage;
		if (component.isJalaed)
		{
			num *= 2;
		}
		component.TakeDamage(4, num);
		component.Warm(0);
		this.PlaySound(component);
		this.Die();
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00009B08 File Offset: 0x00007D08
	protected override GameObject GetNearestZombie()
	{
		GameObject nearestJalaedZombie = this.GetNearestJalaedZombie();
		if (nearestJalaedZombie != null)
		{
			return nearestJalaedZombie;
		}
		float num = float.MaxValue;
		GameObject gameObject = null;
		foreach (GameObject gameObject2 in Board.Instance.zombieArray)
		{
			if (gameObject2 != null)
			{
				Zombie component = gameObject2.GetComponent<Zombie>();
				Collider2D collider2D;
				if (!component.isMindControlled && component.theStatus != 1 && component.shadow.transform.position.x < 9.2f && component.TryGetComponent<Collider2D>(out collider2D) && Vector2.Distance(collider2D.bounds.center, base.transform.position) < num)
				{
					gameObject = gameObject2;
					num = Vector2.Distance(collider2D.bounds.center, base.transform.position);
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

	// Token: 0x06000124 RID: 292 RVA: 0x00009C54 File Offset: 0x00007E54
	private GameObject GetNearestJalaedZombie()
	{
		float num = float.MaxValue;
		GameObject gameObject = null;
		foreach (GameObject gameObject2 in Board.Instance.zombieArray)
		{
			if (gameObject2 != null)
			{
				Zombie component = gameObject2.GetComponent<Zombie>();
				Collider2D collider2D;
				if (component.isJalaed && !component.isMindControlled && component.theStatus != 1 && component.shadow.transform.position.x < 9.2f && component.TryGetComponent<Collider2D>(out collider2D) && Vector2.Distance(collider2D.bounds.center, base.transform.position) < num)
				{
					gameObject = gameObject2;
					num = Vector2.Distance(collider2D.bounds.center, base.transform.position);
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

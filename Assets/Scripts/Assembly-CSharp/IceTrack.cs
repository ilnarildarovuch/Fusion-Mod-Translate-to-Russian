using System;
using UnityEngine;

// Token: 0x0200003A RID: 58
public class IceTrack : TrackBullet
{
	// Token: 0x0600012C RID: 300 RVA: 0x00009FD4 File Offset: 0x000081D4
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		int num = this.theBulletDamage;
		if (component.freezeSpeed == 0f)
		{
			num *= 4;
		}
		component.TakeDamage(5, num);
		component.AddfreezeLevel(5);
		this.PlaySound(component);
		this.Die();
	}

	// Token: 0x0600012D RID: 301 RVA: 0x0000A01C File Offset: 0x0000821C
	protected override GameObject GetNearestZombie()
	{
		GameObject nearestFreezedZombie = this.GetNearestFreezedZombie();
		if (nearestFreezedZombie != null)
		{
			return nearestFreezedZombie;
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

	// Token: 0x0600012E RID: 302 RVA: 0x0000A168 File Offset: 0x00008368
	private GameObject GetNearestFreezedZombie()
	{
		float num = float.MaxValue;
		GameObject gameObject = null;
		foreach (GameObject gameObject2 in Board.Instance.zombieArray)
		{
			if (gameObject2 != null)
			{
				Zombie component = gameObject2.GetComponent<Zombie>();
				Collider2D collider2D;
				if (component.freezeSpeed == 0f && !component.isMindControlled && component.theStatus != 1 && component.shadow.transform.position.x < 9.2f && component.TryGetComponent<Collider2D>(out collider2D) && Vector2.Distance(collider2D.bounds.center, base.transform.position) < num)
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

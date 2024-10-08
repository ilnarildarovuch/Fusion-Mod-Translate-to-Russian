using UnityEngine;

public class ScaredFume : Shooter
{
	private int shootType;

	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 9, 0);
		obj.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(57);
		return obj;
	}

	public void AttackZombie()
	{
		bool flag = false;
		foreach (GameObject item in board.zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (!(component.shadow.transform.position.x > shadow.transform.position.x + 7f) && !(component.shadow.transform.position.x < shadow.transform.position.x) && SearchUniqueZombie(component) && component.theZombieRow == thePlantRow)
				{
					zombieList.Add(component);
					flag = true;
				}
			}
		}
		for (int num = zombieList.Count - 1; num >= 0; num--)
		{
			if (zombieList[num] != null)
			{
				zombieList[num].TakeDamage(1, 20);
			}
		}
		zombieList.Clear();
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3));
		}
	}

	public void AnimShootFume()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		Object.Instantiate(GameAPP.particlePrefab[19], position, Quaternion.Euler(0f, 90f, 0f), board.transform).GetComponent<ParticleSystem>().GetComponent<Renderer>()
			.sortingLayerName = $"particle{thePlantRow}";
		GameAPP.PlaySound(58);
		AttackZombie();
	}

	protected override void PlantShootUpdate()
	{
		thePlantAttackCountDown -= Time.deltaTime;
		if (!(thePlantAttackCountDown < 0f))
		{
			return;
		}
		thePlantAttackCountDown = thePlantAttackInterval;
		if (SearchZombie() != null)
		{
			if (shootType == 0)
			{
				anim.Play("shoot", 1);
			}
			else
			{
				anim.SetTrigger("shoot1");
			}
		}
	}

	protected override GameObject SearchZombie()
	{
		Zombie zombie = null;
		float num = float.MaxValue;
		foreach (GameObject item in board.GetComponent<Board>().zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (component.theZombieRow == thePlantRow && component.shadow.transform.position.x < 9.2f && component.shadow.transform.position.x > shadow.transform.position.x && SearchUniqueZombie(component) && component.shadow.transform.position.x < num)
				{
					num = component.shadow.transform.position.x;
					zombie = component;
				}
			}
		}
		if (zombie == null)
		{
			return null;
		}
		if (zombie.shadow.transform.position.x > shadow.transform.position.x && zombie.shadow.transform.position.x < shadow.transform.position.x + 7f)
		{
			shootType = 1;
			return zombie.gameObject;
		}
		if (zombie.shadow.transform.position.x > shadow.transform.position.x + 7f)
		{
			shootType = 0;
			return zombie.gameObject;
		}
		return null;
	}
}

using UnityEngine;

public class FumeShroom : Shooter
{
	protected override GameObject SearchZombie()
	{
		foreach (GameObject item in board.GetComponent<Board>().zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (!component.isMindControlled && component.theZombieRow == thePlantRow && component.shadow.transform.position.x < 9.2f && component.shadow.transform.position.x > shadow.transform.position.x && component.shadow.transform.position.x < shadow.transform.position.x + 7f && SearchUniqueZombie(component))
				{
					return item;
				}
			}
		}
		return null;
	}

	protected virtual void AttackZombie()
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

	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		Object.Instantiate(GameAPP.particlePrefab[19], position, Quaternion.Euler(0f, 90f, 0f), board.transform).GetComponent<ParticleSystem>().GetComponent<Renderer>()
			.sortingLayerName = $"particle{thePlantRow}";
		GameAPP.PlaySound(58);
		AttackZombie();
		return null;
	}
}

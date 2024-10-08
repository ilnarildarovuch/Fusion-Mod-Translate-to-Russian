using UnityEngine;

public class HyponoFume : FumeShroom
{
	protected override void AttackZombie()
	{
		bool flag = false;
		foreach (GameObject item in board.zombieArray)
		{
			if (item != null && !(item.transform.position.x > base.transform.position.x + 7f) && !(item.transform.position.x < base.transform.position.x))
			{
				Zombie component = item.GetComponent<Zombie>();
				if (SearchUniqueZombie(component) && component.theZombieRow == thePlantRow)
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
				TrySetMindControl(zombieList[num]);
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
		Object.Instantiate(GameAPP.particlePrefab[21], position, Quaternion.Euler(0f, 90f, 0f), board.transform).GetComponent<ParticleSystem>().GetComponent<Renderer>()
			.sortingLayerName = $"particle{thePlantRow}";
		GameAPP.PlaySound(58);
		AttackZombie();
		return null;
	}

	private void TrySetMindControl(Zombie zombie)
	{
		float num = zombie.theFirstArmorMaxHealth + zombie.theMaxHealth;
		float num2 = ((float)zombie.theFirstArmorHealth + zombie.theHealth) / num;
		num2 = ((!((double)num2 > 0.5)) ? (num2 / 0.5f) : 1f);
		int num3 = 0;
		bool[] controlledLevel = zombie.controlledLevel;
		for (int i = 0; i < controlledLevel.Length; i++)
		{
			if (controlledLevel[i])
			{
				num3++;
			}
		}
		num2 -= (float)num3 * 0.05f;
		num2 = Mathf.Sqrt(num2);
		float num4 = 0.75f;
		num4 -= (float)num3 * 0.05f;
		if (num2 < num4)
		{
			num2 = num4;
		}
		if (Random.value >= num2)
		{
			zombie.SetMindControl();
		}
		else
		{
			zombie.TakeDamage(1, 20);
		}
	}
}

using UnityEngine;

public class RandomZombie : ConeZombie
{
	protected override void FirstArmorFall()
	{
		Vector3 position = shadow.transform.position;
		Zombie component = SetRandomZombie(position).GetComponent<Zombie>();
		if (isMindControlled)
		{
			component.SetMindControl(mustControl: true);
		}
		RandomEvent(component);
		base.FirstArmorFall();
		Object.Instantiate(GameAPP.particlePrefab[11], new Vector3(base.transform.position.x, position.y + 1f, 0f), Quaternion.identity).transform.SetParent(GameAPP.board.transform);
		Die(2);
	}

	protected virtual GameObject SetRandomZombie(Vector3 pos)
	{
		if (Random.Range(0, 34) < 24)
		{
			return board.GetComponent<CreateZombie>().SetZombie(0, theZombieRow, Random.Range(0, 24), pos.x);
		}
		if (board.isEveStarted)
		{
			int num;
			do
			{
				num = Random.Range(100, 112);
			}
			while (num == 102);
			return board.GetComponent<CreateZombie>().SetZombie(0, theZombieRow, num, pos.x);
		}
		return board.GetComponent<CreateZombie>().SetZombie(0, theZombieRow, Random.Range(100, 112), pos.x);
	}

	protected virtual void RandomEvent(Zombie zombie)
	{
		float num = (float)Random.Range(1, 6) / 5f;
		zombie.theHealth *= num;
		zombie.theMaxHealth = (int)((float)zombie.theMaxHealth * num);
		zombie.theFirstArmorHealth = (int)((float)zombie.theFirstArmorHealth * num);
		zombie.theFirstArmorMaxHealth = (int)((float)zombie.theFirstArmorMaxHealth * num);
		zombie.theSecondArmorHealth = (int)((float)zombie.theSecondArmorHealth * num);
		zombie.theSecondArmorMaxHealth = (int)((float)zombie.theSecondArmorMaxHealth * num);
	}

	protected override void FirstArmorBroken()
	{
		if (theFirstArmorHealth < theFirstArmorMaxHealth * 2 / 3 && theFirstArmorBroken < 1)
		{
			theFirstArmorBroken = 1;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[12];
		}
		if (theFirstArmorHealth < theFirstArmorMaxHealth / 3 && theFirstArmorBroken < 2)
		{
			theFirstArmorBroken = 2;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[13];
		}
	}
}

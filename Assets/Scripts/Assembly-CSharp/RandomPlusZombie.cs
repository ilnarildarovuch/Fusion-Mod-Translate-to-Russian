using UnityEngine;

public class RandomPlusZombie : RandomZombie
{
	protected override void FirstArmorBroken()
	{
		if (theFirstArmorHealth < theFirstArmorMaxHealth * 2 / 3 && theFirstArmorBroken < 1)
		{
			theFirstArmorBroken = 1;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[24];
		}
		if (theFirstArmorHealth < theFirstArmorMaxHealth / 3 && theFirstArmorBroken < 2)
		{
			theFirstArmorBroken = 2;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[25];
		}
	}

	protected override void RandomEvent(Zombie zombie)
	{
		float num = 5f / 5f;
		zombie.theHealth *= num;
		zombie.theMaxHealth = (int)((float)zombie.theMaxHealth * num);
		zombie.theFirstArmorHealth = (int)((float)zombie.theFirstArmorHealth * num);
		zombie.theFirstArmorMaxHealth = (int)((float)zombie.theFirstArmorMaxHealth * num);
		zombie.theSecondArmorHealth = (int)((float)zombie.theSecondArmorHealth * num);
		zombie.theSecondArmorMaxHealth = (int)((float)zombie.theSecondArmorMaxHealth * num);
	}
}

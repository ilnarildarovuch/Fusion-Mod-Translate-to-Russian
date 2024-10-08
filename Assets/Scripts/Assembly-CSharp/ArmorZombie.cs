using UnityEngine;

public class ArmorZombie : Zombie
{
	protected virtual void FirstArmorBroken()
	{
	}

	protected virtual void SecondArmorBroken()
	{
	}

	protected virtual void FirstArmorFall()
	{
	}

	protected virtual void SecondArmorFall()
	{
	}

	protected override int FirstArmorTakeDamage(int theDamage)
	{
		int num = theDamage;
		if (num < theFirstArmorHealth)
		{
			theFirstArmorHealth -= num;
			FirstArmorBroken();
			return 0;
		}
		num -= theFirstArmorHealth;
		Object.Destroy(theFirstArmor);
		FirstArmorFall();
		theFirstArmorHealth = 0;
		theFirstArmorType = 0;
		theFirstArmor = null;
		return num;
	}

	protected override int SecondArmorTakeDamage(int theDamage)
	{
		int num = theDamage;
		if (num < theSecondArmorHealth)
		{
			theSecondArmorHealth -= num;
			SecondArmorBroken();
			return 0;
		}
		num -= theSecondArmorHealth;
		Object.Destroy(theSecondArmor);
		SecondArmorFall();
		theSecondArmorHealth = 0;
		theSecondArmorType = 0;
		theSecondArmor = null;
		return num;
	}
}

using UnityEngine;

public class TallIceNutZ : WallNutZ
{
	protected override void FirstArmorBroken()
	{
		if (theFirstArmorHealth < theFirstArmorMaxHealth * 2 / 3 && theFirstArmorBroken < 1)
		{
			theFirstArmorBroken = 1;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[27];
		}
		if (theFirstArmorHealth < theFirstArmorMaxHealth / 3 && theFirstArmorBroken < 2)
		{
			theFirstArmorBroken = 2;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[28];
		}
	}

	public override void SetFreeze(float time)
	{
	}

	public override void SetCold(float time)
	{
	}
}

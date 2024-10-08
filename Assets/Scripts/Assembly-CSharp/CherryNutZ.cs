using UnityEngine;

public class CherryNutZ : WallNutZ
{
	protected override void FirstArmorBroken()
	{
		if ((float)theFirstArmorHealth < (float)(theFirstArmorMaxHealth * 2) / 3f && theFirstArmorBroken < 1)
		{
			theFirstArmorBroken = 1;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[16];
		}
		if ((float)theFirstArmorHealth < (float)theFirstArmorMaxHealth / 3f && theFirstArmorBroken < 2)
		{
			theFirstArmorBroken = 2;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[17];
		}
	}
}

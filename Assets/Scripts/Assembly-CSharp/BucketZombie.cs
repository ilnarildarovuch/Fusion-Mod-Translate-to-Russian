using UnityEngine;

public class BucketZombie : ArmorZombie
{
	protected override void FirstArmorBroken()
	{
		if (theFirstArmorHealth < theFirstArmorMaxHealth * 2 / 3 && theFirstArmorBroken < 1)
		{
			theFirstArmorBroken = 1;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[3];
		}
		if (theFirstArmorHealth < theFirstArmorMaxHealth / 3 && theFirstArmorBroken < 2)
		{
			theFirstArmorBroken = 2;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[4];
		}
	}

	protected override void FirstArmorFall()
	{
		foreach (Transform item in base.transform)
		{
			if (item.name == "LoseBucket")
			{
				item.gameObject.SetActive(value: true);
				item.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = $"zombie{theZombieRow}";
				item.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += baseLayer + 29;
				item.gameObject.GetComponent<ParticleSystem>().collision.AddPlane(board.transform.GetChild(2 + theZombieRow));
			}
		}
	}
}

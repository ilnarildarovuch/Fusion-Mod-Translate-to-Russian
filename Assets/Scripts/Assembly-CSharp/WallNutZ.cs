using Unity.VisualScripting;
using UnityEngine;

public class WallNutZ : ArmorZombie
{
	protected override void FirstArmorBroken()
	{
		if ((float)theFirstArmorHealth < (float)(theFirstArmorMaxHealth * 2) / 3f && theFirstArmorBroken < 1)
		{
			theFirstArmorBroken = 1;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[8];
		}
		if ((float)theFirstArmorHealth < (float)theFirstArmorMaxHealth / 3f && theFirstArmorBroken < 2)
		{
			theFirstArmorBroken = 2;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[9];
		}
	}

	protected override void BodyTakeDamage(int theDamage)
	{
		theHealth -= theDamage;
		if (!isLoseHand && theHealth < (float)(theMaxHealth * 2) / 3f)
		{
			isLoseHand = true;
			GameAPP.PlaySound(7);
			for (int i = 0; i < base.transform.childCount; i++)
			{
				Transform child = base.transform.GetChild(i);
				if (child.CompareTag("ZombieHand"))
				{
					Object.Destroy(child.gameObject);
				}
				if (child.CompareTag("ZombieArmUpper"))
				{
					child.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[0];
					child.transform.localScale = new Vector3(4f, 4f, 4f);
				}
				if (child.name == "LoseArm")
				{
					child.gameObject.SetActive(value: true);
					child.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = $"zombie{theZombieRow}";
					child.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += baseLayer + 29;
					child.gameObject.GetComponent<ParticleSystem>().collision.AddPlane(board.transform.GetChild(2 + theZombieRow));
					child.AddComponent<ZombieHead>();
				}
			}
		}
		if (!(theHealth < (float)theMaxHealth / 3f) || theStatus == 1)
		{
			return;
		}
		theStatus = 1;
		for (int j = 0; j < base.transform.childCount; j++)
		{
			Transform child2 = base.transform.GetChild(j);
			if (child2.CompareTag("ZombieHead"))
			{
				Object.Destroy(child2.gameObject);
			}
		}
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
		theFirstArmorHealth = 0;
		theFirstArmorType = 0;
		theFirstArmor = null;
		return num;
	}
}

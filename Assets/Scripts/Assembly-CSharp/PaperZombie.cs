using Unity.VisualScripting;
using UnityEngine;

public class PaperZombie : ArmorZombie
{
	protected bool losePaper;

	protected override void Start()
	{
		base.Start();
		theStatus = 4;
	}

	protected override void SecondArmorBroken()
	{
		if (theSecondArmorHealth < theSecondArmorMaxHealth * 2 / 3 && theSecondArmorBroken < 1)
		{
			theSecondArmorBroken = 1;
			theSecondArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[6];
		}
		if (theSecondArmorHealth < theSecondArmorMaxHealth / 3 && theSecondArmorBroken < 2)
		{
			theSecondArmorBroken = 2;
			theSecondArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[7];
		}
	}

	protected override void SecondArmorFall()
	{
		foreach (Transform item in base.transform)
		{
			if (item.name == "LosePaper")
			{
				item.gameObject.SetActive(value: true);
				item.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = $"zombie{theZombieRow}";
				item.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += baseLayer + 29;
			}
		}
		GameAPP.PlaySound(44);
		anim.SetTrigger("losePaper");
		losePaper = true;
		theStatus = 5;
	}

	protected override void BodyTakeDamage(int theDamage)
	{
		theHealth -= theDamage;
		if (!isLoseHand && theHealth < (float)(theMaxHealth * 2 / 3) && losePaper)
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
					child.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[5];
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
		if (!(theHealth < (float)(theMaxHealth / 3)) || theStatus == 1)
		{
			return;
		}
		theStatus = 1;
		GameAPP.PlaySound(7);
		for (int j = 0; j < base.transform.childCount; j++)
		{
			Transform child2 = base.transform.GetChild(j);
			if (child2.CompareTag("ZombieHead"))
			{
				Object.Destroy(child2.gameObject);
			}
			if (child2.name == "LoseHead")
			{
				child2.gameObject.SetActive(value: true);
				child2.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = $"zombie{theZombieRow}";
				child2.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += baseLayer + 29;
				child2.gameObject.GetComponent<ParticleSystem>().collision.AddPlane(board.transform.GetChild(2 + theZombieRow));
				child2.GetChild(0).gameObject.GetComponent<ParticleSystem>().collision.AddPlane(board.transform.GetChild(2 + theZombieRow));
				child2.GetChild(0).gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = $"zombie{theZombieRow}";
				child2.GetChild(0).gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += baseLayer + 29;
				child2.AddComponent<ZombieHead>();
				Vector3 localScale = child2.transform.localScale;
				child2.transform.SetParent(board.transform);
				child2.transform.localScale = localScale;
			}
		}
		if (!losePaper)
		{
			SecondArmorFall();
		}
	}

	public void AngrySound()
	{
		GameAPP.PlaySound(Random.Range(45, 47));
	}

	private void ChangeStatus()
	{
		if (theStatus != 1)
		{
			theStatus = 6;
		}
	}
}

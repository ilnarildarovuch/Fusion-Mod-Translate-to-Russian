using Unity.VisualScripting;
using UnityEngine;

public class PaperCherryZ : ArmorZombie
{
	private bool losePaper;

	public float theZombieAttackInterval = 0.75f;

	[SerializeField]
	private float theZombieAttackCountDown;

	[SerializeField]
	private bool isAngry;

	protected override void SecondArmorBroken()
	{
		if (theSecondArmorHealth < theSecondArmorMaxHealth * 2 / 3 && theSecondArmorBroken < 1)
		{
			theSecondArmorBroken = 1;
			theSecondArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[10];
		}
		if (theSecondArmorHealth < theSecondArmorMaxHealth / 3 && theSecondArmorBroken < 2)
		{
			theSecondArmorBroken = 2;
			theSecondArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[11];
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
		}
		if (!losePaper)
		{
			SecondArmorFall();
		}
	}

	public void AngrySound()
	{
		GameAPP.PlaySound(Random.Range(45, 47));
		isAngry = true;
	}

	private void Angry()
	{
		isAngry = true;
	}

	protected override void Update()
	{
		base.Update();
		if (isAngry)
		{
			ZombieShootUpdate();
		}
	}

	private void ZombieShootUpdate()
	{
		if (theStatus == 0 && GameAPP.theGameStatus == 0)
		{
			theZombieAttackCountDown -= Time.deltaTime;
			if (theZombieAttackCountDown < 0f)
			{
				theZombieAttackCountDown = theZombieAttackInterval;
				GetComponent<Animator>().Play("shoot", 1);
			}
		}
	}

	public virtual GameObject AnimShoot()
	{
		if (theStatus == 1)
		{
			return null;
		}
		Vector3 position = base.transform.Find("Zombie_head").GetChild(0).transform.position;
		float x = position.x;
		float theY = position.y - 0.1f;
		int theRow = theZombieRow;
		GameObject gameObject = board.GetComponent<CreateBullet>().SetBullet(x, theY, theRow, 3, 0);
		Vector3 position2 = gameObject.transform.GetChild(0).transform.position;
		gameObject.transform.GetChild(0).transform.position = new Vector3(position2.x, position2.y - 0.5f, position2.z);
		if (!isMindControlled)
		{
			gameObject.GetComponent<Bullet>().isZombieBullet = true;
		}
		gameObject.GetComponent<Bullet>().theBulletDamage = 0;
		GameAPP.PlaySound(Random.Range(3, 5));
		return gameObject;
	}
}

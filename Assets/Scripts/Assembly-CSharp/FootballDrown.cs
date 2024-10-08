using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FootballDrown : TallNutFootballZ
{
	protected float throwTime;

	private bool isThrow;

	protected override void Start()
	{
		base.Start();
		throwTime = Random.Range(3, 6);
	}

	protected override void Update()
	{
		base.Update();
		if (throwTime > 0f && !isThrow && !isMindControlled && theStatus != 1)
		{
			throwTime -= Time.deltaTime;
			if (throwTime <= 0f)
			{
				StartThrow();
			}
		}
	}

	private void StartThrow()
	{
		isThrow = true;
		anim.SetTrigger("throw");
	}

	private void AnimThrow()
	{
		GameAPP.PlaySound(Random.Range(3, 5));
		Vector2 vector = shadow.transform.position;
		DrownWeapon drownWeapon = Object.Instantiate(position: new Vector2(vector.x - 2f, vector.y + 3.1f), original: Resources.Load<GameObject>("Zombies/Zombie_drown/weapon"), rotation: Quaternion.Euler(0f, 0f, -17f), parent: board.transform).AddComponent<DrownWeapon>();
		drownWeapon.theRow = theZombieRow;
		List<Plant> list = new List<Plant>();
		GameObject[] plantArray = board.plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				if (component.thePlantRow == theZombieRow && component.shadow.transform.position.x + 6f < shadow.transform.position.x)
				{
					list.Add(component);
				}
			}
		}
		if (list.Count != 0)
		{
			list.Sort((Plant a, Plant b) => b.thePlantColumn.CompareTo(a.thePlantColumn));
			drownWeapon.target = list[0].gameObject;
		}
	}

	protected override void OnTriggerStay2D(Collider2D collision)
	{
		if (isThrow)
		{
			base.OnTriggerStay2D(collision);
		}
		else if (collision.gameObject.CompareTag("Plant") && !isMindControlled)
		{
			Plant component = collision.GetComponent<Plant>();
			if (component.thePlantRow == theZombieRow && !TypeMgr.IsCaltrop(component.thePlantType))
			{
				StartThrow();
			}
		}
	}

	protected override void FirstArmorBroken()
	{
		if (theFirstArmorHealth < theFirstArmorMaxHealth * 2 / 3 && theFirstArmorBroken < 1)
		{
			theFirstArmorBroken = 1;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[40];
		}
		if (theFirstArmorHealth < theFirstArmorMaxHealth / 3 && theFirstArmorBroken < 2)
		{
			theFirstArmorBroken = 2;
			theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[41];
		}
	}

	protected override void BodyTakeDamage(int theDamage)
	{
		theHealth -= theDamage;
		if (!isLoseHand && theHealth < (float)(theMaxHealth * 2 / 3))
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
		for (int j = 0; j < base.transform.childCount; j++)
		{
			Transform child2 = base.transform.GetChild(j);
			if (child2.CompareTag("ZombieHead"))
			{
				Object.Destroy(child2.gameObject);
			}
		}
	}
}

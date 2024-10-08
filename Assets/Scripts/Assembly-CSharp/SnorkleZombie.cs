using Unity.VisualScripting;
using UnityEngine;

public class SnorkleZombie : Zombie
{
	protected override void Awake()
	{
		base.Awake();
		theStatus = 7;
	}

	protected override void Start()
	{
		base.Start();
		if (GameAPP.theGameStatus == 0)
		{
			anim.Play("swim");
			inWater = true;
			SetMaskLayer();
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
				if (child.CompareTag("ZombieArmUpper"))
				{
					child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Zombies/Zombie_snorkle/Zombie_snorkle_outerarm_upper2");
				}
			}
		}
		if (!(theHealth < (float)(theMaxHealth / 3)))
		{
			return;
		}
		if (theStatus != 1)
		{
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
					child2.gameObject.GetComponent<ParticleSystem>().collision.AddPlane(board.transform.GetChild(2 + theZombieRow));
					child2.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = $"zombie{theZombieRow}";
					child2.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += baseLayer + 29;
					child2.AddComponent<ZombieHead>();
					Vector3 localScale = child2.transform.localScale;
					child2.transform.SetParent(board.transform);
					child2.transform.localScale = localScale;
				}
			}
		}
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("swim"))
		{
			Die(2);
		}
	}

	private void OutOfWater()
	{
		theStatus = 0;
	}

	private void GoInToWater()
	{
		theStatus = 7;
	}
}

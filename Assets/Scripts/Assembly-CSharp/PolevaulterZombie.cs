using Unity.VisualScripting;
using UnityEngine;

public class PolevaulterZombie : Zombie
{
	public enum PolStatus
	{
		run = 0,
		jump = 1,
		walk = 2
	}

	public int polevaulterStatus;

	private Vector2 jumpPos2;

	private Vector2 range = new Vector2(0.7f, 2f);

	private bool willJumpFail;

	private Vector3 failPos;

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (polevaulterStatus != 0 || theStatus != 0 || isMindControlled || theFreezeCountDown != 0f)
		{
			return;
		}
		jumpPos2 = new Vector2(shadow.transform.position.x - 0.7f, shadow.transform.position.y + 1f);
		Collider2D[] array = Physics2D.OverlapBoxAll(jumpPos2, range, 0f);
		bool flag = false;
		Collider2D[] array2 = array;
		foreach (Collider2D collider2D in array2)
		{
			if (!collider2D.CompareTag("Plant"))
			{
				continue;
			}
			Plant component = collider2D.GetComponent<Plant>();
			if (!TypeMgr.IsCaltrop(component.thePlantType) && component.thePlantRow == theZombieRow)
			{
				if (TypeMgr.IsTallNut(component.thePlantType))
				{
					willJumpFail = true;
					failPos = component.shadow.transform.position;
					failPos = new Vector3(failPos.x + 0.5f, shadow.transform.position.y, 1f);
				}
				flag = true;
				break;
			}
		}
		if (flag)
		{
			polevaulterStatus = 1;
			theStatus = 3;
			anim.SetTrigger("jump");
			shadow.SetActive(value: false);
		}
	}

	protected override void OnTriggerStay2D(Collider2D collision)
	{
		if (polevaulterStatus == 2)
		{
			base.OnTriggerStay2D(collision);
		}
	}

	public virtual void JumpOver()
	{
		if (shadow != null)
		{
			shadow.SetActive(value: true);
		}
		polevaulterStatus = 2;
		if (theStatus != 1)
		{
			theStatus = 0;
		}
	}

	public void PlayJumpSound1()
	{
		GameAPP.PlaySound(50);
	}

	public void PlayJumpSound2()
	{
		GameAPP.PlaySound(51);
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
					child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Zombies/Zombie_polevaulter/Zombie_polevaulter_outerarm_upper2");
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

	public override void Charred()
	{
		if (GameAPP.difficulty == 4 && theHealth + (float)theFirstArmorHealth > 1800f)
		{
			TakeDamage(10, 1800);
			return;
		}
		if (GameAPP.difficulty == 5 && theHealth + (float)theFirstArmorHealth > 900f)
		{
			TakeDamage(10, 1800);
			return;
		}
		if (polevaulterStatus != 1 && shadow != null && theStatus == 0)
		{
			GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Zombies/Charred/Zombie_Charred"), Vector2.zero, Quaternion.identity, board.transform);
			Vector3 position = gameObject.transform.Find("Shadow").gameObject.transform.position;
			Vector3 vector = shadow.transform.position - position;
			gameObject.transform.position += vector;
			SetLayer(theZombieRow, gameObject);
		}
		Die(2);
	}

	private void JumpFail()
	{
		if (!willJumpFail)
		{
			return;
		}
		GameAPP.PlaySound(64);
		if (theStatus != 1)
		{
			anim.Play("walk2");
			if (shadow != null)
			{
				shadow.SetActive(value: true);
				polevaulterStatus = 2;
				theStatus = 0;
				AdjustPosition(base.gameObject, failPos);
				Object.Instantiate(GameAPP.particlePrefab[23], new Vector3(shadow.transform.position.x, shadow.transform.position.y + 1.75f), Quaternion.identity, board.transform);
			}
		}
	}
}

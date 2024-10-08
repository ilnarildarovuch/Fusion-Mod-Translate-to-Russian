using Unity.VisualScripting;
using UnityEngine;

public class DolphinriderZ : Zombie
{
	private Vector2 jumpPos2;

	private Vector2 range = new Vector2(0.7f, 2f);

	private bool willJumpFail;

	private Vector3 failPos;

	private bool loseHead;

	protected override void Start()
	{
		base.Start();
		if (GameAPP.theGameStatus == 0)
		{
			GameAPP.PlaySound(78, 1f);
			theStatus = 8;
			anim.Play("ride");
			inWater = true;
			SetMaskLayer();
		}
	}

	public override void Die(int reason = 0)
	{
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("ride"))
		{
			reason = 2;
		}
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("dolphinjump"))
		{
			reason = 2;
		}
		base.Die(reason);
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (theStatus != 8 || isMindControlled || theFreezeCountDown != 0f)
		{
			return;
		}
		jumpPos2 = new Vector2(shadow.transform.position.x - 0.4f, shadow.transform.position.y + 1f);
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
			if (!TypeMgr.IsTangkelp(component.thePlantType) && component.thePlantRow == theZombieRow)
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
			theStatus = 9;
			anim.SetTrigger("jump");
			GameAPP.PlaySound(79);
		}
	}

	protected override void OnTriggerStay2D(Collider2D collision)
	{
		if (theStatus == 0)
		{
			base.OnTriggerStay2D(collision);
		}
	}

	public virtual void JumpOver()
	{
		if (theStatus != 1)
		{
			theStatus = 0;
		}
	}

	private void CreateWaterSplash()
	{
		Vector2 vector = shadow.transform.position;
		vector = new Vector2(vector.x, vector.y - 0.4f);
		GameObject obj = Object.Instantiate(Resources.Load<GameObject>("Particle/Anim/Water/WaterSplashPrefab"), vector, Quaternion.identity, GameAPP.board.transform);
		obj.transform.localScale = new Vector3(0.4f, 0.4f);
		foreach (Transform item in obj.transform)
		{
			item.GetComponent<SpriteRenderer>().sortingLayerName = $"particle{theZombieRow}";
		}
		Object.Instantiate(position: new Vector2(vector.x, vector.y + 0.4f), original: GameAPP.particlePrefab[32], rotation: Quaternion.identity, parent: GameAPP.board.transform);
		GameAPP.PlaySound(75);
	}

	protected override void BodyTakeDamage(int theDamage)
	{
		theHealth -= theDamage;
		if (!isLoseHand && theStatus == 0 && theHealth < (float)(theMaxHealth * 2 / 3))
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
					child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Zombies/Zombie_dolphinrider/Zombie_dolphinrider_outerarm_upper2");
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
		if (theStatus != 9 && shadow != null && theStatus == 0)
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
			anim.Play("swim");
			if (shadow != null)
			{
				theStatus = 0;
				AdjustPosition(base.gameObject, failPos);
				Object.Instantiate(GameAPP.particlePrefab[23], new Vector3(shadow.transform.position.x, shadow.transform.position.y + 1.75f), Quaternion.identity, board.transform);
			}
		}
	}
}

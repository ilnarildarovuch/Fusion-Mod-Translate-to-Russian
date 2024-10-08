using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie : MonoBehaviour
{
	public enum ZombieColor
	{
		Default = 0,
		Cold = 1,
		MindConrolled = 2,
		Jalaed = 3,
		Doom = 4
	}

	public Board board;

	public int theZombieType;

	public int theZombieRow;

	public int theStatus;

	public float theSpeed = 1f;

	public float theOriginSpeed = 1f;

	public float freezeSpeed = 1f;

	public float coldSpeed = 1f;

	public float grapSpeed = 1f;

	public int theAttackDamage = 50;

	public float theFreezeCountDown;

	public float theSlowCountDown;

	public float theButterCountDown;

	public bool isMoving = true;

	public bool isAttacking;

	public int baseLayer;

	public bool isMindControlled;

	public bool[] controlledLevel = new bool[7];

	public bool isJalaed;

	public int freezeLevel;

	private int freezeMaxLevel = 100;

	public float theHealth = 270f;

	public int theMaxHealth = 270;

	public GameObject theFirstArmor;

	public int theFirstArmorHealth;

	public int theFirstArmorMaxHealth;

	public int theFirstArmorType;

	public int theFirstArmorBroken;

	public GameObject theSecondArmor;

	public int theSecondArmorHealth;

	public int theSecondArmorMaxHealth;

	public int theSecondArmorType;

	public int theSecondArmorBroken;

	public GameObject theAttackTarget;

	public Animator anim;

	protected bool isLoseHand;

	public GameObject shadow;

	private GameObject iceTrap;

	private GameObject grap;

	protected float flashTime;

	public bool inWater;

	public bool isStopped;

	public bool isChangingRow;

	private Coroutine changeRow;

	private bool isDying;

	private int dieReason = -1;

	public bool isDoom;

	private int grapTimes;

	private bool droppedSun;

	protected virtual void Awake()
	{
		anim = GetComponent<Animator>();
		if (base.transform.Find("Shadow") != null)
		{
			shadow = base.transform.Find("Shadow").gameObject;
		}
	}

	protected virtual void Start()
	{
		if (GameAPP.difficulty == 5 && !board.isIZ)
		{
			theOriginSpeed += 0.1f;
		}
	}

	protected virtual void Update()
	{
		MoveUpdate();
		if (theHealth <= 0f)
		{
			Die();
		}
		if (theStatus == 1)
		{
			theHealth -= 0.3f * (float)theMaxHealth / 270f;
		}
		if (GameAPP.theGameStatus == 0 && ((isMindControlled && base.transform.position.x > 10f) || base.transform.position.x > 12f || base.transform.position.x < -10f))
		{
			Die(2);
		}
		if (Input.GetKeyDown(KeyCode.T))
		{
			ChangeRow(theZombieRow - 1);
		}
		if (Input.GetKeyDown(KeyCode.Y))
		{
			ChangeRow(theZombieRow + 1);
		}
	}

	protected virtual void FixedUpdate()
	{
		FlashUpdate();
	}

	public void ChangeRow(int theTargetRow)
	{
		if (theTargetRow != theZombieRow)
		{
			if (theTargetRow < 0)
			{
				theTargetRow = 0;
			}
			if (theTargetRow > board.roadNum - 1)
			{
				theTargetRow = board.roadNum - 1;
			}
			int theCurrentRow = theZombieRow;
			if (isChangingRow)
			{
				StopCoroutine(changeRow);
				isChangingRow = false;
			}
			changeRow = StartCoroutine(MoveRow(theTargetRow, theCurrentRow));
		}
	}

	private void SetRowLayer(GameObject obj, int theRow)
	{
		if (obj.name == "Shadow")
		{
			return;
		}
		if (obj.TryGetComponent<SpriteRenderer>(out var component))
		{
			component.sortingLayerName = $"zombie{theRow}";
		}
		if (obj.transform.childCount == 0)
		{
			return;
		}
		foreach (Transform item in obj.transform)
		{
			SetRowLayer(item.gameObject, theRow);
		}
	}

	private IEnumerator MoveRow(int theTargetRow, int theCurrentRow)
	{
		isChangingRow = true;
		int theNextRow = theTargetRow;
		if (Mathf.Abs(theTargetRow - theCurrentRow) > 1)
		{
			theNextRow = ((theTargetRow <= theCurrentRow) ? (theCurrentRow - 1) : (theCurrentRow + 1));
		}
		theZombieRow = theNextRow;
		SetRowLayer(base.gameObject, theNextRow);
		float startY = shadow.transform.position.y;
		float elapsedTime = 0f;
		float duringTime = 8f / theSpeed * (float)Mathf.Abs(theNextRow - theCurrentRow);
		float newY = Mouse.Instance.GetBoxYFromRow(theNextRow) + 0.1f;
		while (elapsedTime < duringTime)
		{
			float num = Mathf.Lerp(startY, newY, elapsedTime / duringTime) - shadow.transform.position.y;
			if (freezeSpeed != 0f && board.isTowerDefense && (!isAttacking || elapsedTime < 0.9f * duringTime))
			{
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + num, 1f);
				elapsedTime += Time.deltaTime;
			}
			yield return null;
		}
		theCurrentRow = theNextRow;
		isChangingRow = false;
		if (Mathf.Abs(theTargetRow - theCurrentRow) > 1)
		{
			changeRow = StartCoroutine(MoveRow(theTargetRow, theCurrentRow));
		}
		else if (Mathf.Abs(theTargetRow - theCurrentRow) == 1)
		{
			changeRow = StartCoroutine(MoveRow(theTargetRow, theCurrentRow));
		}
		float num2 = newY - shadow.transform.position.y;
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + num2);
	}

	public virtual void Die(int reason = 0)
	{
		if (isDoom)
		{
			reason = 2;
			Vector2 position = new Vector2(shadow.transform.position.x - 0.3f, shadow.transform.position.y + 0.2f);
			Vector2 vector = Camera.main.WorldToScreenPoint(shadow.transform.position);
			board.SetDoom(Mouse.Instance.GetColumnFromX(vector.x), theZombieRow, setPit: true, position);
		}
		theStatus = 1;
		if (iceTrap != null || theFreezeCountDown > 0f)
		{
			theFreezeCountDown = 0f;
			Unfrezzing();
		}
		if (grap != null)
		{
			Object.Destroy(grap);
		}
		if (isDying)
		{
			if (reason != dieReason)
			{
				Object.Destroy(base.gameObject);
			}
			return;
		}
		isDying = true;
		dieReason = reason;
		DieEvent();
		if (!isMindControlled)
		{
			board.theCurrentNumOfZombieUncontroled--;
		}
		if (!isMindControlled)
		{
			DropItem();
		}
		switch (reason)
		{
		case 0:
			Object.Destroy(GetComponent<Collider2D>());
			shadow.GetComponent<SpriteRenderer>().enabled = false;
			anim.SetTrigger("GoDie");
			break;
		case 1:
		{
			GameAPP.PlaySound(Random.Range(0, 3));
			Vector2 vector2 = base.transform.Find("Shadow").transform.position;
			Object.Instantiate(GameAPP.particlePrefab[11], new Vector3(vector2.x, vector2.y + 1f, 0f), Quaternion.identity).transform.SetParent(GameAPP.board.transform);
			Object.Destroy(base.gameObject);
			break;
		}
		case 2:
			Object.Destroy(base.gameObject);
			break;
		}
	}

	protected virtual void DieEvent()
	{
	}

	public virtual void TakeDamage(int theDamageType, int theDamage)
	{
		if (isJalaed)
		{
			theDamage = (int)(1.5f * (float)theDamage);
		}
		if (GameAPP.difficulty > 4 && !isMindControlled && theDamage > 0)
		{
			theDamage /= 2;
		}
		if (GameAPP.difficulty == 1 && !isMindControlled)
		{
			theDamage += 10;
		}
		flashTime = 0.3f;
		int num = theDamage;
		switch (theDamageType)
		{
		case 0:
			if (theSecondArmor != null)
			{
				num = SecondArmorTakeDamage(theDamage);
				if (num == 0)
				{
					break;
				}
			}
			if (theFirstArmor != null)
			{
				num = FirstArmorTakeDamage(num);
				if (num == 0)
				{
					break;
				}
			}
			BodyTakeDamage(num);
			break;
		case 1:
			if (theSecondArmor != null)
			{
				SecondArmorTakeDamage(num);
			}
			if (theFirstArmor != null)
			{
				num = FirstArmorTakeDamage(num);
				if (num == 0)
				{
					break;
				}
			}
			BodyTakeDamage(num);
			break;
		case 2:
			if (theSecondArmor != null)
			{
				num = SecondArmorTakeDamage(theDamage);
				if (num == 0)
				{
					break;
				}
			}
			SetCold(10f);
			if (theFirstArmor != null)
			{
				num = FirstArmorTakeDamage(num);
				if (num == 0)
				{
					break;
				}
			}
			BodyTakeDamage(num);
			break;
		case 3:
			if (theSecondArmor != null)
			{
				SecondArmorTakeDamage(num);
			}
			SetCold(10f);
			if (theFirstArmor != null)
			{
				num = FirstArmorTakeDamage(num);
				if (num == 0)
				{
					break;
				}
			}
			BodyTakeDamage(num);
			break;
		case 4:
			if (theFirstArmor != null)
			{
				num = FirstArmorTakeDamage(num);
				if (num == 0)
				{
					break;
				}
			}
			BodyTakeDamage(num);
			break;
		case 5:
			SetCold(10f);
			if (theFirstArmor != null)
			{
				num = FirstArmorTakeDamage(num);
				if (num == 0)
				{
					break;
				}
			}
			BodyTakeDamage(num);
			break;
		case 10:
			if (theSecondArmor != null)
			{
				SecondArmorTakeDamage(theDamage);
			}
			if (theFirstArmor != null)
			{
				num = FirstArmorTakeDamage(num);
				if (num == 0)
				{
					break;
				}
			}
			BodyTakeDamage(num);
			break;
		case 11:
			if (theSecondArmor != null)
			{
				SecondArmorTakeDamage(theDamage);
			}
			if (theFirstArmor != null)
			{
				num = FirstArmorTakeDamage(num);
				if (num == 0)
				{
					break;
				}
			}
			if (theHealth <= (float)num)
			{
				Die(2);
			}
			else
			{
				BodyTakeDamage(num);
			}
			break;
		case 6:
		case 7:
		case 8:
		case 9:
			break;
		}
	}

	protected virtual int FirstArmorTakeDamage(int theDamage)
	{
		return 0;
	}

	protected virtual int SecondArmorTakeDamage(int theDamage)
	{
		return 0;
	}

	protected virtual void BodyTakeDamage(int theDamage)
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

	private void FlashUpdate()
	{
		if (flashTime > 0f)
		{
			if (flashTime > 0.2f)
			{
				SetBrightness(-30f * flashTime + 10f, base.gameObject);
			}
			else if (flashTime > 0f)
			{
				SetBrightness(15f * flashTime + 1f, base.gameObject);
			}
			flashTime -= 0.02f;
			if (flashTime == 0f)
			{
				SetBrightness(1f, base.gameObject);
			}
		}
	}

	protected void SetBrightness(float b, GameObject obj)
	{
		if (!(obj != shadow))
		{
			return;
		}
		if (obj.TryGetComponent<SpriteRenderer>(out var component))
		{
			component.material.SetFloat("_Brightness", b);
		}
		if (obj.transform.childCount <= 0)
		{
			return;
		}
		foreach (Transform item in obj.transform)
		{
			SetBrightness(b, item.gameObject);
		}
	}

	protected virtual void OnTriggerStay2D(Collider2D collision)
	{
		if (theStatus != 1 && theAttackTarget == null)
		{
			if (!isMindControlled && collision.TryGetComponent<Plant>(out var component))
			{
				if (board.isTowerDefense && board.boxType[component.thePlantColumn, component.thePlantRow] != 2)
				{
					return;
				}
				if (component.thePlantRow == theZombieRow)
				{
					if (!TypeMgr.IsCaltrop(component.thePlantType))
					{
						theAttackTarget = collision.gameObject;
						anim.SetBool("isAttacking", value: true);
						isAttacking = true;
					}
					return;
				}
			}
			if (collision.TryGetComponent<Zombie>(out var component2) && component2.isMindControlled == !isMindControlled && component2.theZombieRow == theZombieRow)
			{
				theAttackTarget = collision.gameObject;
				anim.SetBool("isAttacking", value: true);
				isAttacking = true;
				return;
			}
			if (board.isIZ && collision.TryGetComponent<IZEBrains>(out var component3) && component3.theRow == theZombieRow && !isMindControlled)
			{
				theAttackTarget = component3.gameObject;
				anim.SetBool("isAttacking", value: true);
				isAttacking = true;
				return;
			}
		}
		if (theStatus != 1)
		{
			if (collision.gameObject == theAttackTarget)
			{
				Zombie component5;
				if (theAttackTarget.TryGetComponent<Plant>(out var component4) && component4.thePlantRow != theZombieRow)
				{
					theAttackTarget = null;
					isAttacking = false;
					anim.SetBool("isAttacking", value: false);
				}
				else if (theAttackTarget.TryGetComponent<Zombie>(out component5) && component5.theZombieRow != theZombieRow)
				{
					theAttackTarget = null;
					isAttacking = false;
					anim.SetBool("isAttacking", value: false);
				}
			}
		}
		else if (theStatus == 1)
		{
			theAttackTarget = null;
			isAttacking = false;
		}
	}

	protected virtual void OnTriggerExit2D(Collider2D collision)
	{
		if (theStatus != 1)
		{
			if (collision.gameObject == theAttackTarget)
			{
				theAttackTarget = null;
				isAttacking = false;
				anim.SetBool("isAttacking", value: false);
			}
		}
		else
		{
			theAttackTarget = null;
			isAttacking = false;
		}
	}

	public void PlayFallSound()
	{
		GameAPP.PlaySound(Random.Range(5, 7));
	}

	public void DestoryZombie()
	{
		DecreaseT(base.gameObject);
		Object.Destroy(base.gameObject, 0.1f);
	}

	private void DecreaseT(GameObject obj)
	{
		if (obj.TryGetComponent<SpriteRenderer>(out var component))
		{
			StartCoroutine(DecreaseTransparent(component.material));
		}
		if (obj.transform.childCount <= 0)
		{
			return;
		}
		foreach (Transform item in obj.transform)
		{
			DecreaseT(item.gameObject);
		}
	}

	private IEnumerator DecreaseTransparent(Material mt)
	{
		float i = 1f;
		while ((double)i > -0.2)
		{
			mt.SetFloat("_Transparent", i);
			yield return new WaitForFixedUpdate();
			i -= 0.2f;
		}
		mt.SetFloat("_Transparent", 0f);
	}

	public virtual void PlayEatSound()
	{
		if (theStatus == 1)
		{
			return;
		}
		GameObject gameObject = theAttackTarget;
		if (gameObject != null)
		{
			if (gameObject.TryGetComponent<Plant>(out var component))
			{
				if (isMindControlled)
				{
					isAttacking = false;
					anim.SetBool("isAttacking", value: false);
					return;
				}
				AttackPlant(component);
			}
			if (gameObject.TryGetComponent<Zombie>(out var component2))
			{
				if (component2.isMindControlled != isMindControlled)
				{
					AttackZombie(component2);
				}
				else
				{
					isAttacking = false;
					anim.SetBool("isAttacking", value: false);
				}
			}
			if (gameObject.TryGetComponent<IZEBrains>(out var component3))
			{
				if (!isMindControlled)
				{
					AttackBrain(component3);
					return;
				}
				isAttacking = false;
				anim.SetBool("isAttacking", value: false);
			}
		}
		else
		{
			isAttacking = false;
			anim.SetBool("isAttacking", value: false);
		}
	}

	private void AttackBrain(IZEBrains brain)
	{
		brain.theHealth -= theAttackDamage;
		GameAPP.PlaySound(Random.Range(8, 10), 0.3f);
		brain.FlashOnce();
	}

	private void AttackPlant(Plant plant)
	{
		switch (plant.thePlantType)
		{
		case 8:
		case 1023:
		case 1024:
		case 1026:
			SetMindControl(mustControl: true);
			plant.Die();
			return;
		case 900:
			SetMindControl(mustControl: true);
			plant.GetComponent<HyponoEmperor>().restHealth--;
			return;
		case 1041:
		{
			board.CreateFreeze(plant.shadow.transform.position);
			Vector2 vector2 = shadow.transform.position;
			CreateZombie.Instance.SetZombie(0, theZombieRow, 111, vector2.x).GetComponent<Zombie>().SetMindControl(mustControl: true);
			plant.Die();
			Die(2);
			return;
		}
		case 1045:
			isDoom = true;
			SetMindControl(mustControl: true);
			plant.Die();
			return;
		case 1022:
		{
			SetMindControl(mustControl: true);
			plant.Die();
			if (theSecondArmorMaxHealth > 0)
			{
				theSecondArmorMaxHealth /= 2;
				theSecondArmorHealth /= 2;
			}
			if (theFirstArmorMaxHealth > 0)
			{
				theFirstArmorMaxHealth /= 2;
				theFirstArmorHealth /= 2;
			}
			if (theHealth > 0f)
			{
				theMaxHealth /= 2;
				theHealth /= 2f;
			}
			Vector2 vector = shadow.transform.position;
			Vector3 localScale = base.transform.localScale;
			base.transform.localScale = new Vector3(0.5f * localScale.x, 0.5f * localScale.y, 0.5f * localScale.z);
			foreach (Transform item in base.transform)
			{
				if (item.TryGetComponent<ParticleSystem>(out var component))
				{
					Vector3 localScale2 = component.transform.localScale;
					component.transform.localScale = new Vector3(0.5f * localScale2.x, 0.5f * localScale2.y, 0.5f * localScale2.z);
				}
			}
			AdjustPosition(base.gameObject, vector);
			base.transform.Translate(0f, 0f, -1f);
			return;
		}
		case 1039:
			SetCold(10f);
			AddfreezeLevel(5);
			break;
		case 1073:
			TakeDamage(4, 20);
			SetJalaed();
			break;
		}
		if (plant.isNut)
		{
			GameAPP.PlaySound(10, 0.3f);
			GameObject gameObject = GameAPP.particlePrefab[5];
			if (plant.thePlantType == 1003)
			{
				gameObject = GameAPP.particlePrefab[6];
			}
			Transform transform = base.transform.Find("Zombie_jaw");
			if (transform != null)
			{
				GameObject obj = Object.Instantiate(gameObject, board.transform);
				obj.name = gameObject.name;
				obj.transform.position = transform.position;
			}
		}
		else
		{
			GameAPP.PlaySound(Random.Range(8, 10), 0.3f);
		}
		plant.FlashOnce();
		if (!plant.isAshy)
		{
			plant.thePlantHealth -= theAttackDamage;
			if (plant.thePlantHealth <= 0)
			{
				GameAPP.PlaySound(11);
			}
		}
	}

	private void AttackZombie(Zombie zombie)
	{
		zombie.TakeDamage(4, theAttackDamage);
		GameAPP.PlaySound(Random.Range(8, 10));
	}

	protected void DropItem()
	{
		if (board.isTowerDefense && !droppedSun)
		{
			switch (theZombieType)
			{
			case 109:
				CreateCoin.Instance.SetCoin(0, 0, 0, 0, shadow.transform.position + new Vector3(0f, 0.5f, 0f));
				CreateCoin.Instance.SetCoin(0, 0, 0, 0, shadow.transform.position + new Vector3(0f, 0.5f, 0f));
				break;
			case 18:
			case 106:
			case 111:
				CreateCoin.Instance.SetCoin(0, 0, 0, 0, shadow.transform.position + new Vector3(0f, 0.5f, 0f));
				CreateCoin.Instance.SetCoin(0, 0, 2, 0, shadow.transform.position + new Vector3(0f, 0.5f, 0f));
				break;
			case 4:
			case 5:
			case 9:
			case 15:
			case 16:
			case 107:
				CreateCoin.Instance.SetCoin(0, 0, 0, 0, shadow.transform.position + new Vector3(0f, 0.5f, 0f));
				break;
			default:
				CreateCoin.Instance.SetCoin(0, 0, 2, 0, shadow.transform.position + new Vector3(0f, 0.5f, 0f));
				break;
			}
			droppedSun = true;
		}
		if (board.isIZ || board.droppedAwardOrOver)
		{
			return;
		}
		if (GameAPP.theBoardType == 3)
		{
			if (board.theWave < board.theMaxWave || board.theCurrentNumOfZombieUncontroled > 0)
			{
				return;
			}
			if (board.theCurrentSurvivalRound >= board.theSurvivalMaxRound)
			{
				board.droppedAwardOrOver = true;
				GameObject gameObject = Resources.Load<GameObject>("Board/Award/TrophyPrefab");
				GameObject gameObject2 = Object.Instantiate(gameObject, board.gameObject.transform);
				gameObject2.name = gameObject.name;
				gameObject2.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 1f, 0f);
				Vector2 vector = Camera.main.WorldToViewportPoint(gameObject2.transform.position);
				if (vector.x < 0.2f)
				{
					vector.x = 0.2f;
				}
				else if (vector.x > 0.8f)
				{
					vector.x = 0.8f;
				}
				if (vector.y < 0.2f)
				{
					vector.y = 0.2f;
				}
				else if (vector.y > 0.8f)
				{
					vector.y = 0.8f;
				}
				gameObject2.transform.position = Camera.main.ViewportToWorldPoint(vector);
				gameObject2.transform.position = new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y, 0f);
			}
			else
			{
				board.droppedAwardOrOver = true;
				board.EnterNextRound();
			}
		}
		else if (board.theWave >= board.theMaxWave && board.theCurrentNumOfZombieUncontroled <= 0)
		{
			board.droppedAwardOrOver = true;
			GameObject gameObject3 = Resources.Load<GameObject>("Board/Award/TrophyPrefab");
			GameObject gameObject4 = Object.Instantiate(gameObject3, board.gameObject.transform);
			gameObject4.name = gameObject3.name;
			gameObject4.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 1f, 0f);
			Vector2 vector2 = Camera.main.WorldToViewportPoint(gameObject4.transform.position);
			if (vector2.x < 0.2f)
			{
				vector2.x = 0.2f;
			}
			else if (vector2.x > 0.8f)
			{
				vector2.x = 0.8f;
			}
			if (vector2.y < 0.2f)
			{
				vector2.y = 0.2f;
			}
			else if (vector2.y > 0.8f)
			{
				vector2.y = 0.8f;
			}
			gameObject4.transform.position = Camera.main.ViewportToWorldPoint(vector2);
			gameObject4.transform.position = new Vector3(gameObject4.transform.position.x, gameObject4.transform.position.y, 0f);
		}
	}

	public virtual void Charred()
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
		if (shadow != null && theStatus != 1 && !inWater && ExistAnim())
		{
			GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Zombies/Charred/Zombie_Charred"), Vector2.zero, Quaternion.identity, board.transform);
			Vector3 position = gameObject.transform.Find("Shadow").gameObject.transform.position;
			Vector3 vector = shadow.transform.position - position;
			gameObject.transform.position += vector;
			SetLayer(theZombieRow, gameObject);
		}
		Die(2);
	}

	private bool ExistAnim()
	{
		int num = theZombieType;
		if (num == 16 || num == 18 || num == 201)
		{
			return false;
		}
		return true;
	}

	protected void SetLayer(int theRow, GameObject charred)
	{
		foreach (Transform item in charred.transform)
		{
			Renderer component = item.GetComponent<Renderer>();
			item.GetComponent<Renderer>().sortingOrder += baseLayer;
			component.sortingLayerName = $"zombie{theRow}";
		}
	}

	public virtual void SetMindControl(bool mustControl = false)
	{
		if (!isMindControlled && (mustControl || GameAPP.difficulty != 5 || Random.Range(0, 2) != 1))
		{
			SetLayerMask();
			isJalaed = false;
			board.theCurrentNumOfZombieUncontroled--;
			DropItem();
			GameAPP.PlaySound(62);
			GameAPP.PlaySound(63);
			Vector2 vector = shadow.transform.position;
			Object.Instantiate(GameAPP.particlePrefab[20], new Vector3(vector.x, vector.y + 1.5f), Quaternion.identity, board.transform);
			isMindControlled = true;
			base.transform.Rotate(0f, 180f, 0f);
			AdjustPosition(base.gameObject, vector);
			base.transform.Translate(0f, 0f, -1f);
			if (isDoom)
			{
				SetColor(base.gameObject, 4);
			}
			else
			{
				SetColor(base.gameObject, 2);
			}
			theAttackTarget = null;
			isAttacking = false;
			if (anim.parameters.Any((AnimatorControllerParameter param) => param.name == "isAttacking"))
			{
				anim.SetBool("isAttacking", value: false);
			}
		}
	}

	public void SetMindControlWithOutEffect()
	{
		if (!isMindControlled)
		{
			SetLayerMask();
			isJalaed = false;
			board.theCurrentNumOfZombieUncontroled--;
			DropItem();
			Vector2 vector = shadow.transform.position;
			isMindControlled = true;
			base.transform.Rotate(0f, 180f, 0f);
			AdjustPosition(base.gameObject, vector);
			base.transform.Translate(0f, 0f, -1f);
			SetColor(base.gameObject, 2);
			theAttackTarget = null;
			isAttacking = false;
			if (anim.parameters.Any((AnimatorControllerParameter param) => param.name == "isAttacking"))
			{
				anim.SetBool("isAttacking", value: false);
			}
		}
	}

	private void SetLayerMask()
	{
		base.gameObject.layer = LayerMask.NameToLayer("MindControlledZombie");
		GetComponent<Collider2D>().excludeLayers = LayerMask.GetMask("MindControlledZombie");
	}

	public void AdjustPosition(GameObject zombie, Vector3 position)
	{
		if (shadow != null)
		{
			Vector3 position2 = shadow.transform.position;
			Vector3 vector = position - position2;
			zombie.transform.position += vector;
		}
	}

	public void SetColor(GameObject obj, int colorType)
	{
		Color color = default(Color);
		color.a = 1f;
		Color color2 = color;
		if (isJalaed)
		{
			colorType = 3;
		}
		switch (colorType)
		{
		case 1:
			color2.r = 0.3529412f;
			color2.g = 20f / 51f;
			color2.b = 1f;
			break;
		case 2:
			color2.r = 0.8235294f;
			color2.g = 0.47058824f;
			color2.b = 1f;
			break;
		case 3:
			color2.r = 1f;
			color2.g = 32f / 51f;
			color2.b = 32f / 51f;
			break;
		case 4:
			color2.r = 0.8235294f;
			color2.g = 4f / 51f;
			color2.b = 4f / 51f;
			break;
		default:
			color2 = Color.white;
			break;
		}
		if (obj.name == "Shadow")
		{
			return;
		}
		if (obj.TryGetComponent<SpriteRenderer>(out var component))
		{
			component.color = color2;
		}
		if (obj.transform.childCount == 0)
		{
			return;
		}
		foreach (Transform item in obj.transform)
		{
			SetColor(item.gameObject, colorType);
		}
	}

	public void AddfreezeLevel(int level)
	{
		freezeLevel += level;
		if (freezeLevel >= freezeMaxLevel && freezeSpeed != 0f)
		{
			freezeLevel = 0;
			if (freezeMaxLevel < 400)
			{
				freezeMaxLevel += 100;
			}
			SetFreeze(4f);
		}
	}

	public void SetJalaed()
	{
		SetColor(base.gameObject, 3);
		isJalaed = true;
		Warm();
	}

	public virtual void SetFreeze(float time)
	{
		if (isDying)
		{
			return;
		}
		isJalaed = false;
		int num = theStatus;
		if (num == 3 || num == 9)
		{
			SetCold(10f);
		}
		if (theFreezeCountDown > 0f && theFreezeCountDown < time)
		{
			theFreezeCountDown = time;
		}
		else if (theFreezeCountDown == 0f)
		{
			theFreezeCountDown = time;
			theSlowCountDown = 10f + time;
			coldSpeed = 0.5f;
			SetColor(base.gameObject, 1);
			freezeSpeed = 0f;
			if (iceTrap != null)
			{
				Object.Destroy(iceTrap);
			}
			if (!inWater)
			{
				GameObject original = Resources.Load<GameObject>("Image/ice/icetrap");
				iceTrap = Object.Instantiate(original, shadow.transform.position, Quaternion.identity);
				iceTrap.transform.SetParent(base.transform);
				iceTrap.GetComponent<SpriteRenderer>().sortingLayerName = $"particle{theZombieRow}";
			}
		}
	}

	private void Unfrezzing()
	{
		if (shadow != null)
		{
			Vector2 vector = shadow.transform.position;
			vector = new Vector2(vector.x, vector.y + 1f);
			GameObject obj = Resources.Load<GameObject>("Particle/Prefabs/IceTrap");
			Object.Instantiate(obj, vector, Quaternion.identity, board.transform);
			obj.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = $"particle{theZombieRow}";
		}
		freezeSpeed = 1f;
		freezeLevel = 0;
		Object.Destroy(iceTrap);
	}

	public virtual void SetCold(float time)
	{
		isJalaed = false;
		theSlowCountDown = time;
		coldSpeed = 0.5f;
		SetColor(base.gameObject, 1);
		if (theSlowCountDown == 0f)
		{
			GameAPP.PlaySound(67);
		}
	}

	public void SetGrap(float time)
	{
		if (grap == null)
		{
			GameObject original = Resources.Load<GameObject>("Bullet/Other/Grap");
			Vector2 vector = shadow.transform.position;
			vector = new Vector2(vector.x, vector.y + 0.25f);
			GameObject gameObject = Object.Instantiate(original, vector, Quaternion.identity);
			gameObject.transform.SetParent(base.transform);
			gameObject.GetComponent<SpriteRenderer>().sortingLayerName = $"zombie{theZombieRow}";
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = baseLayer + 29;
			grap = gameObject;
			GameAPP.PlaySound(71);
		}
		theSlowCountDown += time;
		int num = theMaxHealth + theFirstArmorMaxHealth;
		if (grapTimes > 5)
		{
			TakeDamage(1, (int)(0.01f * (float)num));
		}
		else if (grapTimes > 10)
		{
			TakeDamage(1, (int)(0.03f * (float)num));
		}
		else if (grapTimes > 15)
		{
			TakeDamage(1, (int)(0.05f * (float)num));
		}
		if (grapTimes >= 30)
		{
			TakeDamage(1, num);
		}
		grapTimes++;
		grapSpeed = 0.5f;
	}

	public void Warm(int warmType = 0)
	{
		freezeLevel = 0;
		if (theFreezeCountDown > 0f || iceTrap != null)
		{
			Unfrezzing();
		}
		if (warmType == 0 && grap == null)
		{
			theSlowCountDown = 0f;
			theFreezeCountDown = 0f;
			RestoreSpeed();
			return;
		}
		theFreezeCountDown = 0f;
		coldSpeed = 1f;
		if (isMindControlled)
		{
			SetColor(base.gameObject, 2);
		}
		else
		{
			SetColor(base.gameObject, 0);
		}
	}

	private void RestoreSpeed()
	{
		coldSpeed = 1f;
		grapSpeed = 1f;
		if (isMindControlled)
		{
			SetColor(base.gameObject, 2);
		}
		else
		{
			SetColor(base.gameObject, 0);
		}
		if (grap != null)
		{
			Object.Destroy(grap);
		}
	}

	protected void MoveUpdate()
	{
		theSpeed = theOriginSpeed * freezeSpeed * coldSpeed * grapSpeed;
		anim.SetFloat("Speed", theSpeed);
		if (theSlowCountDown > 0f)
		{
			theSlowCountDown -= Time.deltaTime;
			if (theSlowCountDown < 0f)
			{
				theSlowCountDown = 0f;
				theSpeed = theOriginSpeed;
				RestoreSpeed();
			}
		}
		if (theFreezeCountDown > 0f)
		{
			theFreezeCountDown -= Time.deltaTime;
			if (theFreezeCountDown < 0f)
			{
				theFreezeCountDown = 0f;
				Unfrezzing();
			}
		}
	}

	protected void SetMaskLayer()
	{
		GameObject obj = base.transform.GetChild(0).gameObject;
		obj.GetComponent<SpriteMask>().frontSortingOrder = baseLayer + 39;
		obj.GetComponent<SpriteMask>().frontSortingLayerID = SortingLayer.NameToID($"zombie{theZombieRow}");
		obj.GetComponent<SpriteMask>().backSortingOrder = baseLayer;
		obj.GetComponent<SpriteMask>().backSortingLayerID = SortingLayer.NameToID($"zombie{theZombieRow}");
	}
}

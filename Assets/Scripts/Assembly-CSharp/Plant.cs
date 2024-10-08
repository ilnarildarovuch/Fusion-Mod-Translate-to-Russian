using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
	public Board board;

	public int thePlantColumn;

	public int thePlantRow;

	public int thePlantType;

	public int thePlantMaxHealth = 300;

	public int thePlantHealth = 300;

	public float thePlantSpeed = 1f;

	protected float theConstSpeed;

	public float thePlantAttackInterval;

	public float thePlantAttackCountDown;

	public float thePlantProduceInterval;

	public float thePlantProduceCountDown;

	public float attributeCountdown;

	public Vector3 startPos;

	public bool isPot;

	public bool isLily;

	public bool isPumpkin;

	public bool isFly;

	public bool isAshy;

	public bool isNut;

	protected Animator anim;

	private bool alwaysLightUp;

	public GameObject shadow;

	public int baseLayer;

	private float brightness = 1f;

	private bool isFlashing;

	public int place;

	public bool isShort;

	public bool isFromWheat;

	private float wheatTime;

	public bool adjustPosByLily;

	private bool isCrashed;

	protected List<Zombie> zombieList = new List<Zombie>();

	public int zombieLayer;

	protected virtual void Awake()
	{
		if (base.transform.Find("Shadow") != null)
		{
			shadow = base.transform.Find("Shadow").gameObject;
		}
		else
		{
			Debug.LogError("Failed to find shadow." + base.gameObject);
		}
		anim = GetComponent<Animator>();
		theConstSpeed = Random.Range(0.9f, 1.1f);
		zombieLayer = LayerMask.GetMask("Zombie");
	}

	protected virtual void Start()
	{
	}

	protected virtual void Update()
	{
		PlantUpdate();
	}

	protected virtual void FixedUpdate()
	{
		MouseFixedUpdate();
	}

	private void MouseFixedUpdate()
	{
		if (!alwaysLightUp && !isFlashing && brightness != 1f)
		{
			SetBrightness(base.gameObject, 1f);
		}
		if (Mouse.Instance.theItemOnMouse == null)
		{
			alwaysLightUp = false;
			return;
		}
		RaycastHit2D[] array = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		for (int i = 0; i < array.Length; i++)
		{
			RaycastHit2D raycastHit2D = array[i];
			if (!raycastHit2D.collider.CompareTag("Plant") || !(raycastHit2D.collider.gameObject == base.gameObject))
			{
				continue;
			}
			if (Mouse.Instance.theItemOnMouse.name == "Shovel")
			{
				if (brightness != 2.5f)
				{
					SetBrightness(base.gameObject, 2.5f);
				}
				alwaysLightUp = true;
				return;
			}
			if (Mouse.Instance.theItemOnMouse.name == "Glove")
			{
				if (brightness != 2.5f)
				{
					SetBrightness(base.gameObject, 2.5f);
				}
				alwaysLightUp = true;
				return;
			}
		}
		if (Mouse.Instance.theItemOnMouse.CompareTag("Preview") && !board.isIZ && MixData.data[thePlantType, Mouse.Instance.thePlantTypeOnMouse] != 0)
		{
			if (brightness != 2.5f)
			{
				SetBrightness(base.gameObject, 2.5f);
			}
			alwaysLightUp = true;
		}
		else
		{
			alwaysLightUp = false;
		}
	}

	protected virtual void PlantUpdate()
	{
		if (thePlantHealth <= 0)
		{
			Die();
		}
		anim.SetFloat("Speed", thePlantSpeed);
		if (isFromWheat && board.theCurrentNumOfZombieUncontroled > 0)
		{
			WheatUpdate();
		}
	}

	private void WheatUpdate()
	{
		wheatTime += Time.deltaTime;
		if (!(wheatTime > 30f))
		{
			return;
		}
		Die();
		GameObject gameObject;
		while (true)
		{
			int theSeedType = Random.Range(1000, 1076);
			if (!board.createPlant.IsPuff(theSeedType))
			{
				gameObject = board.createPlant.SetPlant(thePlantColumn, thePlantRow, theSeedType);
				if (!(gameObject == null))
				{
					break;
				}
			}
		}
		gameObject.GetComponent<Plant>().isFromWheat = true;
		Vector2 vector = shadow.transform.position;
		Object.Instantiate(position: new Vector2(vector.x, vector.y + 0.5f), original: GameAPP.particlePrefab[11], rotation: Quaternion.identity, parent: board.transform);
	}

	public void FlashOnce()
	{
		if (!alwaysLightUp)
		{
			FlashPlant(base.gameObject);
		}
	}

	private void FlashPlant(GameObject obj)
	{
		if (obj.name == "Shadow")
		{
			return;
		}
		if (obj.TryGetComponent<SpriteRenderer>(out var component))
		{
			StartCoroutine(FlashObject(component.material));
		}
		if (obj.transform.childCount <= 0)
		{
			return;
		}
		foreach (Transform item in obj.transform)
		{
			FlashPlant(item.gameObject);
		}
	}

	private IEnumerator FlashObject(Material mt)
	{
		if (!alwaysLightUp)
		{
			for (float j = 1f; j < 4f; j += 1f)
			{
				mt.SetFloat("_Brightness", j);
				brightness = j;
				isFlashing = true;
				yield return new WaitForFixedUpdate();
			}
			for (float j = 4f; j > 0.75f; j -= 0.25f)
			{
				mt.SetFloat("_Brightness", j);
				brightness = j;
				isFlashing = true;
				yield return new WaitForFixedUpdate();
			}
			isFlashing = false;
		}
	}

	public virtual void Die()
	{
		if (Mouse.Instance.thePlantOnGlove == base.gameObject)
		{
			Object.Destroy(Mouse.Instance.theItemOnMouse);
			Mouse.Instance.theItemOnMouse = null;
			Mouse.Instance.thePlantTypeOnMouse = -1;
			Mouse.Instance.thePlantOnGlove = null;
		}
		if (board.isIZ && !board.isEveStarted)
		{
			GiveSunInIZ();
		}
		TryRemoveFromList();
		Object.Destroy(base.gameObject);
	}

	protected bool TryRemoveFromList()
	{
		for (int i = 0; i < board.plantArray.Length; i++)
		{
			if (board.plantArray[i] == base.gameObject)
			{
				board.plantArray[i] = null;
				return true;
			}
		}
		return false;
	}

	protected virtual GameObject SearchZombie()
	{
		foreach (GameObject item in GameAPP.board.GetComponent<Board>().zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (component.theZombieRow == thePlantRow && component.shadow.transform.position.x < 9.2f && component.shadow.transform.position.x > shadow.transform.position.x && SearchUniqueZombie(component))
				{
					return item;
				}
			}
		}
		return null;
	}

	protected virtual void PlantShootUpdate()
	{
		thePlantAttackCountDown -= Time.deltaTime;
		if (thePlantAttackCountDown < 0f)
		{
			thePlantAttackCountDown = thePlantAttackInterval;
			thePlantAttackCountDown += Random.Range(-0.1f, 0.1f);
			if (SearchZombie() != null)
			{
				anim.SetTrigger("shoot");
			}
			else if (board.isScaredyDream && thePlantType == 9)
			{
				anim.SetTrigger("shoot");
			}
		}
	}

	public virtual void ProducerUpdate()
	{
	}

	protected void SetBrightness(GameObject obj, float b)
	{
		brightness = b;
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
			SetBrightness(item.gameObject, b);
		}
	}

	public void Recover(int health)
	{
		thePlantHealth += health;
		if (thePlantHealth > thePlantMaxHealth)
		{
			thePlantHealth = thePlantMaxHealth;
		}
		Vector3 position = shadow.transform.position;
		position = new Vector3(position.x, position.y + 0.7f, 1f);
		Object.Instantiate(GameAPP.particlePrefab[16], board.transform).transform.position = position;
	}

	public void Crashed()
	{
		if (isCrashed)
		{
			return;
		}
		switch (thePlantType)
		{
		case 2:
			GetComponent<CherryBomb>().Bomb();
			break;
		case 10:
			board.CreateFreeze(shadow.transform.position);
			Die();
			break;
		case 11:
			GetComponent<DoomShroom>().AnimExplode();
			Die();
			break;
		case 1040:
			GetComponent<IceDoom>().AnimExplode();
			Die();
			break;
		case 16:
			GetComponent<Jalapeno>().AnimExplode();
			break;
		case 13:
		case 15:
		case 17:
		case 1049:
		case 1050:
		case 1051:
		case 1054:
		case 1057:
		case 1060:
		case 1066:
			return;
		case 1003:
		case 1010:
		case 1052:
		case 1053:
			Die();
			break;
		}
		isCrashed = true;
		if (board.boxType[thePlantColumn, thePlantRow] == 1)
		{
			Die();
			return;
		}
		if (board.isIZ && !board.isEveStarted)
		{
			GiveSunInIZ();
		}
		Object.Destroy(anim);
		Vector3 position = shadow.transform.position;
		base.transform.localScale = new Vector3(base.transform.localScale.x, 0.3f * base.transform.localScale.y);
		Vector3 position2 = shadow.transform.position;
		Vector3 vector = position - position2;
		base.transform.position += vector;
		shadow.SetActive(value: false);
		if (Mouse.Instance.thePlantOnGlove == base.gameObject)
		{
			Object.Destroy(Mouse.Instance.theItemOnMouse);
			Mouse.Instance.theItemOnMouse = null;
			Mouse.Instance.thePlantTypeOnMouse = -1;
			Mouse.Instance.thePlantOnGlove = null;
		}
		TryRemoveFromList();
		Collider2D[] components = GetComponents<Collider2D>();
		for (int i = 0; i < components.Length; i++)
		{
			Object.Destroy(components[i]);
		}
		Object.Destroy(base.gameObject, 3f);
		Object.Destroy(this);
	}

	private void GiveSunInIZ()
	{
		CreateCoin.Instance.SetCoin(thePlantColumn, thePlantRow, 0, 0);
		CreateCoin.Instance.SetCoin(thePlantColumn, thePlantRow, 0, 0);
		CreateCoin.Instance.SetCoin(thePlantColumn, thePlantRow, 0, 0);
		CreateCoin.Instance.SetCoin(thePlantColumn, thePlantRow, 0, 0);
		CreateCoin.Instance.SetCoin(thePlantColumn, thePlantRow, 0, 0);
	}

	protected bool SearchUniqueZombie(Zombie zombie)
	{
		if (zombie == null)
		{
			return false;
		}
		if (zombie.isMindControlled)
		{
			return false;
		}
		int theStatus = zombie.theStatus;
		if (theStatus == 1 || theStatus == 3 || theStatus == 9)
		{
			return false;
		}
		if (thePlantType == 1004)
		{
			return true;
		}
		if (zombie.theStatus == 7)
		{
			return false;
		}
		return true;
	}

	protected bool AttackUniqueZombie(Zombie zombie)
	{
		if (zombie == null)
		{
			return false;
		}
		if (zombie.isMindControlled)
		{
			return false;
		}
		int theStatus = zombie.theStatus;
		if (theStatus == 3 || theStatus == 9)
		{
			return false;
		}
		return true;
	}

	public void SetColor(GameObject obj, Color color)
	{
		if (obj.name == "Shadow")
		{
			return;
		}
		if (obj.TryGetComponent<SpriteRenderer>(out var component))
		{
			component.color = color;
		}
		if (obj.transform.childCount == 0)
		{
			return;
		}
		foreach (Transform item in obj.transform)
		{
			SetColor(item.gameObject, color);
		}
	}
}

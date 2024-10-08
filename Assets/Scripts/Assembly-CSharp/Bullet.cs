using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public int theBulletType;

	public int theMovingWay;

	public bool isZombieBullet;

	public int theBulletRow;

	public GameObject zombie;

	public GameObject torchWood;

	public float theExistTime;

	public int theBulletDamage = 20;

	public bool hasHitTarget;

	public bool isShort;

	public int zombieLayer;

	private Camera mainCamara;

	private Vector2 currentDireciton = new Vector2(1f, 0f);

	public bool isFromThreeTorch;

	public int hitTimes;

	public readonly List<GameObject> Z = new List<GameObject>();

	public readonly List<Zombie> zombieToFired = new List<Zombie>();

	public int fireLevel;

	public int zombieBlockType;

	public GameObject shadow;

	public GameObject fireParticle;

	public Sprite sprite;

	public int puffColor;

	public Vector2 startPosition;

	public bool isLand;

	public bool isHot;

	public float g = 2f;

	public GameObject parentPlant;

	public float Vx;

	public float Vy;

	public float Y;

	public float originY;

	public bool firstLand;

	protected virtual void Awake()
	{
		if (base.transform.Find("Shadow") != null)
		{
			shadow = base.transform.Find("Shadow").gameObject;
		}
		zombieLayer = LayerMask.GetMask("Zombie");
		startPosition = base.transform.position;
		mainCamara = Camera.main;
	}

	protected virtual void Start()
	{
		SetShadowPosition();
	}

	protected void SetShadowPosition()
	{
		if (!(shadow != null))
		{
			return;
		}
		if (isShort)
		{
			shadow.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - 0.3f);
			return;
		}
		int num = theMovingWay;
		if ((uint)num <= 1u || num == 3)
		{
			shadow.transform.position = new Vector3(shadow.transform.position.x, Mouse.Instance.GetBoxYFromRow(theBulletRow) + 0.2f);
		}
		else
		{
			shadow.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - 0.6f);
		}
	}

	public virtual void Die()
	{
		Object.Destroy(base.gameObject);
	}

	protected virtual void Update()
	{
		theExistTime += Time.deltaTime;
		if (theExistTime > 0.75f && theMovingWay == 3)
		{
			Die();
		}
		if (!isZombieBullet)
		{
			switch (theMovingWay)
			{
			case 0:
			case 2:
			case 3:
				base.transform.Translate(6f * Time.deltaTime * Vector3.right);
				break;
			case 1:
				RollingUpdate();
				break;
			case 4:
				if (theExistTime < 0.5f)
				{
					base.transform.Translate(5.5f * (1f - theExistTime) * (1f - theExistTime) * Time.deltaTime * Vector3.up);
				}
				base.transform.Translate(6f * Time.deltaTime * Vector3.right);
				break;
			case 5:
				if (theExistTime < 0.5f)
				{
					base.transform.Translate(5.5f * (1f - theExistTime) * (1f - theExistTime) * Time.deltaTime * Vector3.down);
				}
				base.transform.Translate(6f * Time.deltaTime * Vector3.right);
				break;
			case 6:
				TrackUpdate();
				break;
			case -1:
				break;
			}
		}
		else
		{
			base.transform.Translate(6f * Time.deltaTime * Vector3.left);
		}
	}

	private void TrackUpdate()
	{
		if (zombie == null)
		{
			zombie = GetNearestZombie();
			base.transform.position += 4f * Time.deltaTime * (Vector3)currentDireciton.normalized;
		}
		else if (zombie.GetComponent<Zombie>().theStatus != 1)
		{
			Vector2 vector = zombie.GetComponent<Collider2D>().bounds.center;
			base.transform.position = Vector2.MoveTowards(base.transform.position, vector, 4f * Time.deltaTime);
			Vector2 vector2 = vector - (Vector2)base.transform.position;
			if (vector2.magnitude > 0.1f)
			{
				currentDireciton = vector2;
			}
			float z = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
			base.transform.rotation = Quaternion.Euler(0f, 0f, z);
			shadow.transform.position = base.transform.GetChild(0).position - new Vector3(0f, 0.5f, 0f);
			shadow.transform.rotation = Quaternion.identity;
		}
		else
		{
			zombie = GetNearestZombie();
		}
	}

	protected virtual GameObject GetNearestZombie()
	{
		float num = float.MaxValue;
		GameObject gameObject = null;
		foreach (GameObject item in Board.Instance.zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (!component.isMindControlled && component.theStatus != 1 && component.shadow.transform.position.x < 9.2f && component.TryGetComponent<Collider2D>(out var component2) && Vector2.Distance(component2.bounds.center, base.transform.position) < num)
				{
					gameObject = item;
					num = Vector2.Distance(component2.bounds.center, base.transform.position);
				}
			}
		}
		if (gameObject != null)
		{
			int theZombieRow = gameObject.GetComponent<Zombie>().theZombieRow;
			CreateBullet.Instance.SetLayer(theZombieRow, base.gameObject);
		}
		return gameObject;
	}

	private void FixedUpdate()
	{
		Vector3 vector = mainCamara.WorldToScreenPoint(base.transform.position);
		if (vector.x < 0f || vector.x > (float)Screen.width || vector.y < 0f || vector.y > (float)Screen.height)
		{
			Die();
		}
	}

	private void RollingUpdate()
	{
		base.transform.Translate(2.5f * Time.deltaTime * Vector3.right);
		if (!isLand)
		{
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - g * Time.deltaTime);
			shadow.transform.position = new Vector3(shadow.transform.position.x, shadow.transform.position.y + g * Time.deltaTime);
			g += 10f * Time.deltaTime;
			if (base.transform.position.y < startPosition.y - 0.6f)
			{
				isLand = true;
			}
		}
	}

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (!hasHitTarget)
		{
			if (collision.CompareTag("Plant") && (isZombieBullet || Board.Instance.isScaredyDream))
			{
				CheckPlant(collision.gameObject);
			}
			else if (collision.CompareTag("Zombie"))
			{
				CheckZombie(collision.gameObject);
			}
		}
	}

	private void CheckPlant(GameObject plant)
	{
		Plant component = plant.GetComponent<Plant>();
		if (GameAPP.board.GetComponent<Board>().isScaredyDream && !isZombieBullet && (theBulletType == 0 || theBulletType == 9) && component.TryGetComponent<Shooter>(out var component2) && !component.isShort)
		{
			HitPlantInDream(component2);
		}
		else if (isZombieBullet && component.thePlantRow == theBulletRow && !component.isShort && component.thePlantType != 254)
		{
			hasHitTarget = true;
			HitPlant(plant);
		}
	}

	private void HitPlantInDream(Shooter shooter)
	{
		if (!(shooter.gameObject != parentPlant))
		{
			return;
		}
		if (shooter.dreamTime == 0f)
		{
			shooter.dreamTime = GetTime(shooter.thePlantType);
			GameObject gameObject = shooter.AnimShoot();
			if (gameObject == null)
			{
				Die();
				return;
			}
			Bullet component = gameObject.GetComponent<Bullet>();
			component.parentPlant = shooter.gameObject;
			component.transform.Translate(0.2f, 0f, 0f);
			if (component.theMovingWay != 1)
			{
				if (AllowUp(shooter.thePlantType))
				{
					GameObject gameObject2 = GameAPP.board.GetComponent<CreateBullet>().SetBullet(gameObject.transform.position.x, gameObject.transform.position.y, component.theBulletRow, component.theBulletType, 2);
					gameObject2.transform.Translate(-0.5f, 0f, 0f);
					Rotate(gameObject2, 90);
					gameObject2.GetComponent<Bullet>().parentPlant = shooter.gameObject;
				}
				if (AllowDown(shooter.thePlantType))
				{
					GameObject gameObject3 = GameAPP.board.GetComponent<CreateBullet>().SetBullet(gameObject.transform.position.x, gameObject.transform.position.y, component.theBulletRow, component.theBulletType, 2);
					gameObject3.transform.Translate(-0.5f, 0f, 0f);
					Rotate(gameObject3, -90);
					gameObject3.GetComponent<Bullet>().parentPlant = shooter.gameObject;
				}
			}
		}
		Die();
	}

	private bool AllowUp(int type)
	{
		if (type == 1017 || type == 1030 || type == 1032)
		{
			return true;
		}
		return false;
	}

	private bool AllowDown(int type)
	{
		if (type == 1017 || type == 1032)
		{
			return true;
		}
		return false;
	}

	private float GetTime(int type)
	{
		switch (type)
		{
		case 7:
		case 1000:
		case 1004:
		case 1005:
		case 1023:
		case 1025:
		case 1026:
		case 1034:
		case 1037:
		case 1043:
		case 1046:
			return 0.2f;
		default:
			return 0.05f;
		}
	}

	private void Rotate(GameObject obj, int angle)
	{
		Vector2 vector = Vector2.zero;
		foreach (Transform item in obj.transform)
		{
			if (item.name == "Shadow")
			{
				vector = item.transform.position;
			}
		}
		obj.transform.Rotate(0f, 0f, angle);
		foreach (Transform item2 in obj.transform)
		{
			if (item2.name == "Shadow")
			{
				item2.Rotate(0f, 0f, -angle);
				item2.transform.position = vector;
			}
		}
	}

	protected virtual void CheckZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		if (isZombieBullet)
		{
			if (component.isMindControlled && component.theZombieRow == theBulletRow)
			{
				HitZombie(zombie);
			}
		}
		else
		{
			if (component.isMindControlled)
			{
				return;
			}
			int theStatus = component.theStatus;
			if (theStatus == 3 || theStatus == 9)
			{
				return;
			}
			switch (theMovingWay)
			{
			case 0:
			case 2:
			case 3:
			case 5:
				if (component.theStatus == 7)
				{
					return;
				}
				break;
			}
			if (component.theZombieRow == theBulletRow || theMovingWay == 2)
			{
				hasHitTarget = true;
				HitZombie(zombie);
			}
		}
	}

	protected virtual void HitPlant(GameObject plant)
	{
		GameAPP.PlaySound(Random.Range(0, 3));
		plant.GetComponent<Plant>().thePlantHealth -= theBulletDamage;
		Die();
	}

	protected virtual void HitZombie(GameObject zombie)
	{
	}

	protected virtual void PlaySound(Zombie zombie)
	{
		if (zombie.theSecondArmorType != 0)
		{
			if (zombie.theSecondArmorType == 1)
			{
				GameAPP.PlaySound(Random.Range(0, 3));
				return;
			}
			if (zombie.theSecondArmorType == 2)
			{
				GameAPP.PlaySound(Random.Range(14, 16));
				return;
			}
		}
		if (zombie.theFirstArmorType != 0)
		{
			if (zombie.theFirstArmorType == 1)
			{
				GameAPP.PlaySound(Random.Range(0, 3));
				GameAPP.PlaySound(Random.Range(12, 14));
				return;
			}
			if (zombie.theFirstArmorType == 2)
			{
				GameAPP.PlaySound(Random.Range(14, 16));
				return;
			}
		}
		switch (zombie.theZombieType)
		{
		case 14:
		case 16:
		case 18:
		case 200:
		case 201:
			GameAPP.PlaySound(Random.Range(14, 16));
			break;
		default:
			GameAPP.PlaySound(Random.Range(0, 3));
			break;
		}
	}

	public void FireZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, theBulletDamage);
		component.Warm();
		if (AllowSputter(component))
		{
			GameAPP.PlaySound(Random.Range(59, 61));
			PoolMgr.Instance.SpawnParticle(base.transform.position, 33).GetComponent<SpriteRenderer>().sortingLayerName = $"particle{theBulletRow}";
			AttackOtherZombie(component);
		}
		else
		{
			PlaySound(component);
		}
		Die();
	}

	private void AttackOtherZombie(Zombie zombie)
	{
		int num = theBulletDamage;
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 1f, zombieLayer);
		for (int i = 0; i < array.Length; i++)
		{
			Zombie component = array[i].GetComponent<Zombie>();
			if (!(component == zombie) && component.theZombieRow == theBulletRow && !component.isMindControlled && AllowSputter(component))
			{
				zombieToFired.Add(component);
			}
		}
		int count = zombieToFired.Count;
		if (count == 0)
		{
			return;
		}
		int num2 = num / count;
		if (num2 == 0)
		{
			num2 = 1;
		}
		if ((float)num2 > 1f / 3f * (float)theBulletDamage)
		{
			num2 = (int)(1f / 3f * (float)theBulletDamage);
		}
		foreach (Zombie item in zombieToFired)
		{
			item.TakeDamage(0, num2);
			item.Warm();
		}
	}

	private bool AllowSputter(Zombie zombie)
	{
		if (zombie.theSecondArmorType == 2)
		{
			return false;
		}
		switch (zombie.theZombieType)
		{
		case 14:
		case 16:
		case 18:
		case 200:
		case 201:
			return false;
		default:
			return true;
		}
	}
}

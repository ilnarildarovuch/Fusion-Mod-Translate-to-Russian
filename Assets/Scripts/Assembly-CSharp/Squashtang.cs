using System.Collections;
using UnityEngine;

public class Squashtang : Plant
{
	protected Zombie TargetZombie;

	private Collider2D[] colliders;

	private Vector2 range = new Vector2(2f, 2f);

	private float existTime;

	private readonly float floatStrength = 0.05f;

	private readonly float frequency = 1.2f;

	protected GameObject grab;

	private int grabTimes;

	private void CreateParticle()
	{
		GameAPP.PlaySound(75);
		Vector2 vector = shadow.transform.position;
		vector = new Vector2(vector.x, vector.y - 0.7f);
		SetWaterSplat(vector, new Vector2(0.7f, 0.7f));
	}

	protected override void Awake()
	{
		base.Awake();
		grab = base.transform.Find("Grab").gameObject;
	}

	protected override void Update()
	{
		base.Update();
		PostionUpdate();
	}

	private void PostionUpdate()
	{
		existTime += Time.deltaTime;
		float num = Mathf.Sin(existTime * frequency) * floatStrength;
		base.transform.position = startPos + Vector3.up * num;
	}

	public override void Die()
	{
		if (TargetZombie != null)
		{
			Vector2 vector = TargetZombie.shadow.transform.position;
			vector = new Vector2(vector.x, vector.y - 0.3f);
			int theZombieType = TargetZombie.theZombieType;
			if (theZombieType == 14 || theZombieType == 200)
			{
				SetWaterSplat(new Vector2(vector.x, vector.y - 0.2f), new Vector2(1f, 1f));
			}
			else
			{
				SetWaterSplat(new Vector2(vector.x, vector.y + 0.2f), new Vector2(0.27f, 0.27f));
			}
			TargetZombie.Die(2);
		}
		base.Die();
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		colliders = Physics2D.OverlapBoxAll(startPos, range, 0f);
		Collider2D[] array = colliders;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].TryGetComponent<Zombie>(out var component) && AbleToAttack(component) && TargetZombie == null)
			{
				TargetZombie = component;
				anim.SetTrigger("searchzombie");
			}
		}
	}

	private void StartGrab()
	{
		if (TargetZombie != null && AbleToAttack(TargetZombie))
		{
			TargetZombie.theOriginSpeed = 0f;
			TargetZombie.GetComponent<Collider2D>().enabled = false;
			anim.SetTrigger("grab");
			SetLayer();
			Vector2 vector = TargetZombie.shadow.transform.position;
			grab.transform.position = new Vector3(vector.x - 0.75f, vector.y + 0.25f);
			GameAPP.PlaySound(62);
			Vector2 position = new Vector2(vector.x, vector.y - 0.2f);
			int theZombieType = TargetZombie.theZombieType;
			if (theZombieType != 14 && theZombieType != 200)
			{
				SetWaterSplat(position, new Vector2(0.27f, 0.27f));
			}
			GameAPP.PlaySound(71);
		}
	}

	private void Grab()
	{
		Vector2 vector = TargetZombie.shadow.transform.position;
		GameAPP.PlaySound(71);
		if (TargetZombie.theZombieType == 14)
		{
			vector = new Vector2(vector.x, vector.y - 0.3f);
			SetWaterSplat(vector, new Vector2(1f, 1f));
		}
		else
		{
			vector = new Vector2(vector.x, vector.y - 0.1f);
			SetWaterSplat(vector, new Vector2(0.27f, 0.27f));
		}
		grab.transform.GetChild(0).localPosition = new Vector3(0f, 0f, 0f);
		StartCoroutine(MoveObject(grab.transform.GetChild(0).gameObject, isZombie: false));
		StartCoroutine(MoveObject(TargetZombie.gameObject, isZombie: true));
	}

	private void CrashZombie()
	{
		Collider2D[] array = Physics2D.OverlapBoxAll(shadow.transform.position, new Vector2(1.5f, 3f), 0f);
		foreach (Collider2D collider2D in array)
		{
			if (!(collider2D.gameObject == TargetZombie) && collider2D.TryGetComponent<Zombie>(out var component) && !component.isMindControlled && component.theZombieRow == thePlantRow)
			{
				component.TakeDamage(11, 600);
			}
		}
	}

	private void SetLayer()
	{
		foreach (Transform item in grab.transform.GetChild(0).transform)
		{
			item.GetComponent<SpriteRenderer>().sortingLayerName = $"zombie{thePlantRow}";
			item.GetComponent<SpriteRenderer>().sortingOrder = TargetZombie.baseLayer + 29;
		}
		SpriteMask component = grab.transform.GetChild(1).gameObject.GetComponent<SpriteMask>();
		component.frontSortingOrder = TargetZombie.baseLayer + 40;
		component.frontSortingLayerID = SortingLayer.NameToID($"zombie{thePlantRow}");
		component.backSortingOrder = TargetZombie.baseLayer;
		component.backSortingLayerID = SortingLayer.NameToID($"zombie{thePlantRow}");
	}

	private IEnumerator MoveObject(GameObject obj, bool isZombie)
	{
		float time = 0f;
		while (time < 0.5f)
		{
			if (obj != null)
			{
				Vector2 vector = new Vector2(((Vector2)obj.transform.position).x, obj.transform.position.y - Time.deltaTime);
				obj.transform.position = vector;
			}
			time += Time.deltaTime;
			yield return null;
		}
		if (isZombie)
		{
			TargetZombie.Die(2);
		}
		else if (++grabTimes == 3)
		{
			Die();
		}
	}

	private GameObject SetWaterSplat(Vector2 position, Vector2 scale)
	{
		GameObject gameObject = Object.Instantiate(Resources.Load<GameObject>("Particle/Anim/Water/WaterSplashPrefab"), position, Quaternion.identity, GameAPP.board.transform);
		SetParticleLayer(gameObject);
		gameObject.transform.localScale = scale;
		Object.Instantiate(GameAPP.particlePrefab[32], position, Quaternion.identity, GameAPP.board.transform);
		return gameObject;
	}

	private void SetParticleLayer(GameObject particle)
	{
		foreach (Transform item in particle.transform)
		{
			item.GetComponent<SpriteRenderer>().sortingLayerName = $"particle{thePlantRow}";
		}
	}

	private bool AbleToAttack(Zombie zombie)
	{
		if (zombie.theStatus != 1 && !zombie.isMindControlled && zombie.theZombieRow == thePlantRow)
		{
			return true;
		}
		return false;
	}
}

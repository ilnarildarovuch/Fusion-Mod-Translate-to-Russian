using System.Collections;
using UnityEngine;

public class Tanglekelp : Plant
{
	protected Zombie TargetZombie;

	private Collider2D[] colliders;

	private Vector2 range = new Vector2(2f, 2f);

	private float existTime;

	private readonly float floatStrength = 0.05f;

	private readonly float frequency = 1.2f;

	protected GameObject grab;

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

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		colliders = Physics2D.OverlapBoxAll(shadow.transform.position, range, 0f);
		Collider2D[] array = colliders;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].TryGetComponent<Zombie>(out var component) && component.theStatus != 1 && !component.isMindControlled && component.theZombieRow == thePlantRow && TargetZombie == null)
			{
				TargetZombie = component;
				component.theOriginSpeed = 0f;
				component.GetComponent<Collider2D>().enabled = false;
				anim.SetTrigger("grab");
				SetLayer();
				Vector2 vector = TargetZombie.shadow.transform.position;
				grab.transform.position = new Vector3(vector.x - 0.75f, vector.y + 0.25f);
				GameAPP.PlaySound(62);
				Vector2 position = new Vector2(vector.x, vector.y - 0.2f);
				if (TargetZombie.theZombieType != 14)
				{
					SetWaterSplat(position, new Vector2(0.27f, 0.27f));
				}
				GameAPP.PlaySound(71);
			}
		}
	}

	protected virtual void Grab()
	{
		Vector2 vector = TargetZombie.shadow.transform.position;
		GameAPP.PlaySound(71);
		int theZombieType = TargetZombie.theZombieType;
		if (theZombieType == 14 || theZombieType == 200)
		{
			vector = new Vector2(vector.x, vector.y - 0.3f);
			SetWaterSplat(vector, new Vector2(1f, 1f));
		}
		else
		{
			vector = new Vector2(vector.x, vector.y - 0.1f);
			SetWaterSplat(vector, new Vector2(0.27f, 0.27f));
		}
		StartCoroutine(MoveObject(grab.transform.GetChild(0).gameObject, isZombie: false));
		StartCoroutine(MoveObject(TargetZombie.gameObject, isZombie: true));
	}

	private void SetLayer()
	{
		foreach (Transform item in grab.transform.GetChild(0).transform)
		{
			item.GetComponent<SpriteRenderer>().sortingLayerName = $"zombie{thePlantRow}";
			item.GetComponent<SpriteRenderer>().sortingOrder += TargetZombie.baseLayer + 29 - baseLayer;
		}
		SpriteMask component = grab.transform.GetChild(1).gameObject.GetComponent<SpriteMask>();
		component.frontSortingOrder = TargetZombie.baseLayer + 40;
		component.frontSortingLayerID = SortingLayer.NameToID($"zombie{thePlantRow}");
		component.backSortingOrder = TargetZombie.baseLayer;
		component.backSortingLayerID = SortingLayer.NameToID($"zombie{thePlantRow}");
	}

	public override void Die()
	{
		if (TargetZombie != null)
		{
			Vector2 vector = TargetZombie.shadow.transform.position;
			vector = new Vector2(vector.x, vector.y);
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

	protected virtual IEnumerator MoveObject(GameObject obj, bool isZombie)
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
			yield break;
		}
		GameAPP.PlaySound(71);
		Vector2 vector2 = shadow.transform.position;
		vector2 = new Vector2(vector2.x, vector2.y - 0.2f);
		SetWaterSplat(vector2, new Vector2(0.4f, 0.4f));
		Die();
	}

	protected GameObject SetWaterSplat(Vector2 position, Vector2 scale)
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

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(shadow.transform.position, range);
		colliders = Physics2D.OverlapBoxAll(startPos, range, 0f);
		Collider2D[] array = colliders;
		foreach (Collider2D obj in array)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(obj.bounds.center, 0.1f);
		}
	}
}

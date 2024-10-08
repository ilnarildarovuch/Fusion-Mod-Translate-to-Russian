using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squash : Plant
{
	private Collider2D[] cols;

	private Vector2 pos;

	private GameObject targetZombie;

	private bool isJump;

	private BoxCollider2D[] boxCols;

	private bool willJumpInWater;

	private readonly List<GameObject> squashZombieList = new List<GameObject>();

	protected Vector2 range = new Vector2(3f, 3f);

	private float startTime;

	private float endTime;

	private Vector2 startJumpPos;

	private Vector2 endPos;

	protected override void Start()
	{
		base.Start();
		boxCols = GetComponents<BoxCollider2D>();
	}

	protected override void Update()
	{
		base.Update();
		pos = shadow.transform.position;
		if (board.boxType[thePlantColumn, thePlantRow] == 1)
		{
			willJumpInWater = true;
		}
		else
		{
			willJumpInWater = false;
		}
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (!isJump)
		{
			SquashUpdate();
		}
	}

	protected virtual void SquashUpdate()
	{
		Vector2 vector = shadow.transform.position;
		vector = new Vector2(vector.x + 0.5f, vector.y);
		cols = Physics2D.OverlapBoxAll(vector, range, 0f);
		Collider2D[] array = cols;
		foreach (Collider2D collider2D in array)
		{
			if (SearchZombie(collider2D.gameObject))
			{
				squashZombieList.Add(collider2D.gameObject);
			}
		}
		targetZombie = GetNearestZombie();
		if (targetZombie != null)
		{
			isJump = true;
			Zombie component = targetZombie.GetComponent<Zombie>();
			if (component.shadow.transform.position.x > shadow.transform.position.x)
			{
				anim.SetTrigger("lookright");
			}
			else
			{
				anim.SetTrigger("lookleft");
			}
			base.transform.SetParent(board.transform);
			startTime = Time.time;
			startJumpPos = component.shadow.transform.position;
			GameAPP.PlaySound(Random.Range(72, 74));
			isAshy = true;
			base.transform.SetParent(board.transform);
			BoxCollider2D[] array2 = boxCols;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].enabled = false;
			}
		}
	}

	private GameObject GetNearestZombie()
	{
		float num = float.MaxValue;
		GameObject result = null;
		foreach (GameObject squashZombie in squashZombieList)
		{
			Zombie component = squashZombie.GetComponent<Zombie>();
			if (Mathf.Abs(component.shadow.transform.position.x - shadow.transform.position.x) < num)
			{
				num = Mathf.Abs(component.shadow.transform.position.x - shadow.transform.position.x);
				result = squashZombie;
			}
		}
		return result;
	}

	private bool SearchZombie(GameObject obj)
	{
		if (obj.TryGetComponent<Zombie>(out var component) && component.theStatus != 1 && component.theZombieRow == thePlantRow && !component.isMindControlled)
		{
			return true;
		}
		return false;
	}

	protected virtual void AttackZombie()
	{
		Collider2D[] array = Physics2D.OverlapBoxAll(shadow.transform.position, new Vector2(1f, 3f), 0f);
		foreach (Collider2D collider2D in array)
		{
			if (SearchZombie(collider2D.gameObject))
			{
				collider2D.GetComponent<Zombie>().TakeDamage(11, 1800);
			}
		}
		if (willJumpInWater)
		{
			GameObject original = Resources.Load<GameObject>("Particle/Anim/Water/WaterSplashPrefab");
			Vector2 vector = shadow.transform.position;
			vector = new Vector2(vector.x, vector.y - 1.75f);
			Object.Instantiate(original, vector, Quaternion.identity, GameAPP.board.transform);
			GameAPP.PlaySound(75);
			Die();
		}
		else
		{
			GameAPP.PlaySound(74);
			ScreenShake.TriggerShake(0.05f);
		}
	}

	private IEnumerator MoveToZombie(Vector3 endPos, float speed)
	{
		Vector3 startPos = shadow.transform.position;
		float num = Vector2.Distance(startPos, endPos);
		if (num > 2f)
		{
			speed = num / 2f * speed;
		}
		float moveTime = Vector3.Distance(startPos, endPos) / speed;
		float elapsedTime = 0f;
		while (elapsedTime < moveTime)
		{
			Vector3 position = Vector3.Lerp(startPos, endPos, EaseInOut(elapsedTime / moveTime));
			SetTransform(base.gameObject, position);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		SetTransform(base.gameObject, endPos);
	}

	private void SetTransform(GameObject plant, Vector3 position)
	{
		Vector3 position2 = shadow.transform.position;
		Vector3 vector = position - position2;
		plant.transform.position += vector;
	}

	private float EaseInOut(float t)
	{
		if (!(t < 0.5f))
		{
			return 1f - 2f * (1f - t) * (1f - t);
		}
		return 2f * t * t;
	}

	private void AnimMove()
	{
		Object.Destroy(shadow.GetComponent<SpriteRenderer>());
		TryRemoveFromList();
		AdjustLayer(base.gameObject);
		Vector2 vector3;
		if (targetZombie != null)
		{
			Zombie component = targetZombie.GetComponent<Zombie>();
			thePlantRow = component.theZombieRow;
			endTime = Time.time;
			float num = startTime - endTime;
			endPos = component.shadow.transform.position;
			Vector2 vector = new Vector2(startJumpPos.x - endPos.x, startJumpPos.y - endPos.y);
			Vector2 vector2 = new Vector2(vector.x / num, vector.y / num);
			vector3 = component.shadow.transform.position;
			vector3 = new Vector2(vector3.x + 0.5f * vector2.x, vector3.y + 1.75f);
		}
		else
		{
			Vector2 vector4 = shadow.transform.position;
			vector3 = new Vector2(startJumpPos.x, vector4.y + 1.75f);
		}
		StartCoroutine(MoveToZombie(vector3, 8f));
	}

	private void AdjustLayer(GameObject obj)
	{
		if (!(obj != null))
		{
			return;
		}
		if (obj.TryGetComponent<SpriteRenderer>(out var component))
		{
			component.sortingLayerName = $"bullet{thePlantRow}";
		}
		if (obj.transform.childCount == 0)
		{
			return;
		}
		foreach (Transform item in obj.transform)
		{
			AdjustLayer(item.gameObject);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawCube(new Vector3(shadow.transform.position.x, shadow.transform.position.y), new Vector3(3f, 3f));
		Collider2D[] array = Physics2D.OverlapBoxAll(pos, new Vector2(4f, 4f), 0f);
		foreach (Collider2D obj in array)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(obj.bounds.center, 0.1f);
		}
	}
}

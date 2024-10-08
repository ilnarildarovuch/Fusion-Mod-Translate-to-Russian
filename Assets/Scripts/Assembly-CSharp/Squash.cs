using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000093 RID: 147
public class Squash : Plant
{
	// Token: 0x060002F3 RID: 755 RVA: 0x00017B74 File Offset: 0x00015D74
	protected override void Start()
	{
		base.Start();
		this.boxCols = base.GetComponents<BoxCollider2D>();
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x00017B88 File Offset: 0x00015D88
	protected override void Update()
	{
		base.Update();
		this.pos = this.shadow.transform.position;
		if (this.board.boxType[this.thePlantColumn, this.thePlantRow] == 1)
		{
			this.willJumpInWater = true;
			return;
		}
		this.willJumpInWater = false;
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x00017BE4 File Offset: 0x00015DE4
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (!this.isJump)
		{
			this.SquashUpdate();
		}
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x00017BFC File Offset: 0x00015DFC
	protected virtual void SquashUpdate()
	{
		Vector2 vector = this.shadow.transform.position;
		vector = new Vector2(vector.x + 0.5f, vector.y);
		this.cols = Physics2D.OverlapBoxAll(vector, this.range, 0f);
		foreach (Collider2D collider2D in this.cols)
		{
			if (this.SearchZombie(collider2D.gameObject))
			{
				this.squashZombieList.Add(collider2D.gameObject);
			}
		}
		this.targetZombie = this.GetNearestZombie();
		if (this.targetZombie != null)
		{
			this.isJump = true;
			Zombie component = this.targetZombie.GetComponent<Zombie>();
			if (component.shadow.transform.position.x > this.shadow.transform.position.x)
			{
				this.anim.SetTrigger("lookright");
			}
			else
			{
				this.anim.SetTrigger("lookleft");
			}
			base.transform.SetParent(this.board.transform);
			this.startTime = Time.time;
			this.startJumpPos = component.shadow.transform.position;
			GameAPP.PlaySound(Random.Range(72, 74), 0.5f);
			this.isAshy = true;
			base.transform.SetParent(this.board.transform);
			BoxCollider2D[] array2 = this.boxCols;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].enabled = false;
			}
		}
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x00017D98 File Offset: 0x00015F98
	private GameObject GetNearestZombie()
	{
		float num = float.MaxValue;
		GameObject result = null;
		foreach (GameObject gameObject in this.squashZombieList)
		{
			Zombie component = gameObject.GetComponent<Zombie>();
			if (Mathf.Abs(component.shadow.transform.position.x - this.shadow.transform.position.x) < num)
			{
				num = Mathf.Abs(component.shadow.transform.position.x - this.shadow.transform.position.x);
				result = gameObject;
			}
		}
		return result;
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x00017E60 File Offset: 0x00016060
	private bool SearchZombie(GameObject obj)
	{
		Zombie zombie;
		return obj.TryGetComponent<Zombie>(out zombie) && zombie.theStatus != 1 && zombie.theZombieRow == this.thePlantRow && !zombie.isMindControlled;
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x00017E9C File Offset: 0x0001609C
	protected virtual void AttackZombie()
	{
		foreach (Collider2D collider2D in Physics2D.OverlapBoxAll(this.shadow.transform.position, new Vector2(1f, 3f), 0f))
		{
			if (this.SearchZombie(collider2D.gameObject))
			{
				collider2D.GetComponent<Zombie>().TakeDamage(11, 1800);
			}
		}
		if (this.willJumpInWater)
		{
			GameObject original = Resources.Load<GameObject>("Particle/Anim/Water/WaterSplashPrefab");
			Vector2 vector = this.shadow.transform.position;
			vector = new Vector2(vector.x, vector.y - 1.75f);
			Object.Instantiate<GameObject>(original, vector, Quaternion.identity, GameAPP.board.transform);
			GameAPP.PlaySound(75, 0.5f);
			this.Die();
			return;
		}
		GameAPP.PlaySound(74, 0.5f);
		ScreenShake.TriggerShake(0.05f);
	}

	// Token: 0x060002FA RID: 762 RVA: 0x00017F8F File Offset: 0x0001618F
	private IEnumerator MoveToZombie(Vector3 endPos, float speed)
	{
		Vector3 startPos = this.shadow.transform.position;
		float num = Vector2.Distance(startPos, endPos);
		if (num > 2f)
		{
			speed = num / 2f * speed;
		}
		float moveTime = Vector3.Distance(startPos, endPos) / speed;
		float elapsedTime = 0f;
		while (elapsedTime < moveTime)
		{
			Vector3 position = Vector3.Lerp(startPos, endPos, this.EaseInOut(elapsedTime / moveTime));
			this.SetTransform(base.gameObject, position);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		this.SetTransform(base.gameObject, endPos);
		yield break;
	}

	// Token: 0x060002FB RID: 763 RVA: 0x00017FAC File Offset: 0x000161AC
	private void SetTransform(GameObject plant, Vector3 position)
	{
		Vector3 position2 = this.shadow.transform.position;
		Vector3 b = position - position2;
		plant.transform.position += b;
	}

	// Token: 0x060002FC RID: 764 RVA: 0x00017FE9 File Offset: 0x000161E9
	private float EaseInOut(float t)
	{
		if (t >= 0.5f)
		{
			return 1f - 2f * (1f - t) * (1f - t);
		}
		return 2f * t * t;
	}

	// Token: 0x060002FD RID: 765 RVA: 0x00018018 File Offset: 0x00016218
	private void AnimMove()
	{
		Object.Destroy(this.shadow.GetComponent<SpriteRenderer>());
		base.TryRemoveFromList();
		this.AdjustLayer(base.gameObject);
		Vector2 vector3;
		if (this.targetZombie != null)
		{
			Zombie component = this.targetZombie.GetComponent<Zombie>();
			this.thePlantRow = component.theZombieRow;
			this.endTime = Time.time;
			float num = this.startTime - this.endTime;
			this.endPos = component.shadow.transform.position;
			Vector2 vector = new Vector2(this.startJumpPos.x - this.endPos.x, this.startJumpPos.y - this.endPos.y);
			Vector2 vector2 = new Vector2(vector.x / num, vector.y / num);
			vector3 = component.shadow.transform.position;
			vector3 = new Vector2(vector3.x + 0.5f * vector2.x, vector3.y + 1.75f);
		}
		else
		{
			Vector2 vector4 = this.shadow.transform.position;
			vector3 = new Vector2(this.startJumpPos.x, vector4.y + 1.75f);
		}
		base.StartCoroutine(this.MoveToZombie(vector3, 8f));
	}

	// Token: 0x060002FE RID: 766 RVA: 0x00018180 File Offset: 0x00016380
	private void AdjustLayer(GameObject obj)
	{
		if (obj != null)
		{
			SpriteRenderer spriteRenderer;
			if (obj.TryGetComponent<SpriteRenderer>(out spriteRenderer))
			{
				spriteRenderer.sortingLayerName = string.Format("bullet{0}", this.thePlantRow);
			}
			if (obj.transform.childCount != 0)
			{
				foreach (object obj2 in obj.transform)
				{
					Transform transform = (Transform)obj2;
					this.AdjustLayer(transform.gameObject);
				}
			}
		}
	}

	// Token: 0x060002FF RID: 767 RVA: 0x0001821C File Offset: 0x0001641C
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawCube(new Vector3(this.shadow.transform.position.x, this.shadow.transform.position.y), new Vector3(3f, 3f));
		foreach (Collider2D collider2D in Physics2D.OverlapBoxAll(this.pos, new Vector2(4f, 4f), 0f))
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(collider2D.bounds.center, 0.1f);
		}
	}

	// Token: 0x040001AD RID: 429
	private Collider2D[] cols;

	// Token: 0x040001AE RID: 430
	private Vector2 pos;

	// Token: 0x040001AF RID: 431
	private GameObject targetZombie;

	// Token: 0x040001B0 RID: 432
	private bool isJump;

	// Token: 0x040001B1 RID: 433
	private BoxCollider2D[] boxCols;

	// Token: 0x040001B2 RID: 434
	private bool willJumpInWater;

	// Token: 0x040001B3 RID: 435
	private readonly List<GameObject> squashZombieList = new List<GameObject>();

	// Token: 0x040001B4 RID: 436
	protected Vector2 range = new Vector2(3f, 3f);

	// Token: 0x040001B5 RID: 437
	private float startTime;

	// Token: 0x040001B6 RID: 438
	private float endTime;

	// Token: 0x040001B7 RID: 439
	private Vector2 startJumpPos;

	// Token: 0x040001B8 RID: 440
	private Vector2 endPos;
}

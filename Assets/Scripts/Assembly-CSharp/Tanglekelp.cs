using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class Tanglekelp : Plant
{
	// Token: 0x06000239 RID: 569 RVA: 0x00011E47 File Offset: 0x00010047
	protected override void Awake()
	{
		base.Awake();
		this.grab = base.transform.Find("Grab").gameObject;
	}

	// Token: 0x0600023A RID: 570 RVA: 0x00011E6A File Offset: 0x0001006A
	protected override void Update()
	{
		base.Update();
		this.PostionUpdate();
	}

	// Token: 0x0600023B RID: 571 RVA: 0x00011E78 File Offset: 0x00010078
	private void PostionUpdate()
	{
		this.existTime += Time.deltaTime;
		float d = Mathf.Sin(this.existTime * this.frequency) * this.floatStrength;
		base.transform.position = this.startPos + Vector3.up * d;
	}

	// Token: 0x0600023C RID: 572 RVA: 0x00011ED4 File Offset: 0x000100D4
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		this.colliders = Physics2D.OverlapBoxAll(this.shadow.transform.position, this.range, 0f);
		Collider2D[] array = this.colliders;
		for (int i = 0; i < array.Length; i++)
		{
			Zombie zombie;
			if (array[i].TryGetComponent<Zombie>(out zombie) && zombie.theStatus != 1 && !zombie.isMindControlled && zombie.theZombieRow == this.thePlantRow && this.TargetZombie == null)
			{
				this.TargetZombie = zombie;
				zombie.theOriginSpeed = 0f;
				zombie.GetComponent<Collider2D>().enabled = false;
				this.anim.SetTrigger("grab");
				this.SetLayer();
				Vector2 vector = this.TargetZombie.shadow.transform.position;
				this.grab.transform.position = new Vector3(vector.x - 0.75f, vector.y + 0.25f);
				GameAPP.PlaySound(62, 0.5f);
				Vector2 position = new Vector2(vector.x, vector.y - 0.2f);
				if (this.TargetZombie.theZombieType != 14)
				{
					this.SetWaterSplat(position, new Vector2(0.27f, 0.27f));
				}
				GameAPP.PlaySound(71, 0.5f);
			}
		}
	}

	// Token: 0x0600023D RID: 573 RVA: 0x0001204C File Offset: 0x0001024C
	protected virtual void Grab()
	{
		Vector2 vector = this.TargetZombie.shadow.transform.position;
		GameAPP.PlaySound(71, 0.5f);
		int theZombieType = this.TargetZombie.theZombieType;
		if (theZombieType == 14 || theZombieType == 200)
		{
			vector = new Vector2(vector.x, vector.y - 0.3f);
			this.SetWaterSplat(vector, new Vector2(1f, 1f));
		}
		else
		{
			vector = new Vector2(vector.x, vector.y - 0.1f);
			this.SetWaterSplat(vector, new Vector2(0.27f, 0.27f));
		}
		base.StartCoroutine(this.MoveObject(this.grab.transform.GetChild(0).gameObject, false));
		base.StartCoroutine(this.MoveObject(this.TargetZombie.gameObject, true));
	}

	// Token: 0x0600023E RID: 574 RVA: 0x00012138 File Offset: 0x00010338
	private void SetLayer()
	{
		foreach (object obj in this.grab.transform.GetChild(0).transform)
		{
			Transform transform = (Transform)obj;
			transform.GetComponent<SpriteRenderer>().sortingLayerName = string.Format("zombie{0}", this.thePlantRow);
			transform.GetComponent<SpriteRenderer>().sortingOrder += this.TargetZombie.baseLayer + 29 - this.baseLayer;
		}
		SpriteMask component = this.grab.transform.GetChild(1).gameObject.GetComponent<SpriteMask>();
		component.frontSortingOrder = this.TargetZombie.baseLayer + 40;
		component.frontSortingLayerID = SortingLayer.NameToID(string.Format("zombie{0}", this.thePlantRow));
		component.backSortingOrder = this.TargetZombie.baseLayer;
		component.backSortingLayerID = SortingLayer.NameToID(string.Format("zombie{0}", this.thePlantRow));
	}

	// Token: 0x0600023F RID: 575 RVA: 0x00012260 File Offset: 0x00010460
	public override void Die()
	{
		if (this.TargetZombie != null)
		{
			Vector2 vector = this.TargetZombie.shadow.transform.position;
			vector = new Vector2(vector.x, vector.y);
			int theZombieType = this.TargetZombie.theZombieType;
			if (theZombieType == 14 || theZombieType == 200)
			{
				this.SetWaterSplat(new Vector2(vector.x, vector.y - 0.2f), new Vector2(1f, 1f));
			}
			else
			{
				this.SetWaterSplat(new Vector2(vector.x, vector.y + 0.2f), new Vector2(0.27f, 0.27f));
			}
			this.TargetZombie.Die(2);
		}
		base.Die();
	}

	// Token: 0x06000240 RID: 576 RVA: 0x00012333 File Offset: 0x00010533
	protected virtual IEnumerator MoveObject(GameObject obj, bool isZombie)
	{
		float time = 0f;
		while (time < 0.5f)
		{
			if (obj != null)
			{
				Vector2 vector = obj.transform.position;
				vector = new Vector2(vector.x, obj.transform.position.y - Time.deltaTime);
				obj.transform.position = vector;
			}
			time += Time.deltaTime;
			yield return null;
		}
		if (isZombie)
		{
			this.TargetZombie.Die(2);
		}
		else
		{
			GameAPP.PlaySound(71, 0.5f);
			Vector2 vector2 = this.shadow.transform.position;
			vector2 = new Vector2(vector2.x, vector2.y - 0.2f);
			this.SetWaterSplat(vector2, new Vector2(0.4f, 0.4f));
			this.Die();
		}
		yield break;
	}

	// Token: 0x06000241 RID: 577 RVA: 0x00012350 File Offset: 0x00010550
	protected GameObject SetWaterSplat(Vector2 position, Vector2 scale)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>("Particle/Anim/Water/WaterSplashPrefab"), position, Quaternion.identity, GameAPP.board.transform);
		this.SetParticleLayer(gameObject);
		gameObject.transform.localScale = scale;
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[32], position, Quaternion.identity, GameAPP.board.transform);
		return gameObject;
	}

	// Token: 0x06000242 RID: 578 RVA: 0x000123C0 File Offset: 0x000105C0
	private void SetParticleLayer(GameObject particle)
	{
		foreach (object obj in particle.transform)
		{
			((Transform)obj).GetComponent<SpriteRenderer>().sortingLayerName = string.Format("particle{0}", this.thePlantRow);
		}
	}

	// Token: 0x06000243 RID: 579 RVA: 0x00012430 File Offset: 0x00010630
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(this.shadow.transform.position, this.range);
		this.colliders = Physics2D.OverlapBoxAll(this.startPos, this.range, 0f);
		foreach (Collider2D collider2D in this.colliders)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(collider2D.bounds.center, 0.1f);
		}
	}

	// Token: 0x04000179 RID: 377
	protected Zombie TargetZombie;

	// Token: 0x0400017A RID: 378
	private Collider2D[] colliders;

	// Token: 0x0400017B RID: 379
	private Vector2 range = new Vector2(2f, 2f);

	// Token: 0x0400017C RID: 380
	private float existTime;

	// Token: 0x0400017D RID: 381
	private readonly float floatStrength = 0.05f;

	// Token: 0x0400017E RID: 382
	private readonly float frequency = 1.2f;

	// Token: 0x0400017F RID: 383
	protected GameObject grab;
}

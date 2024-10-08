using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200006E RID: 110
public class Squashtang : Plant
{
	// Token: 0x0600022A RID: 554 RVA: 0x000116E0 File Offset: 0x0000F8E0
	private void CreateParticle()
	{
		GameAPP.PlaySound(75, 0.5f);
		Vector2 vector = this.shadow.transform.position;
		vector = new Vector2(vector.x, vector.y - 0.7f);
		this.SetWaterSplat(vector, new Vector2(0.7f, 0.7f));
	}

	// Token: 0x0600022B RID: 555 RVA: 0x0001173F File Offset: 0x0000F93F
	protected override void Awake()
	{
		base.Awake();
		this.grab = base.transform.Find("Grab").gameObject;
	}

	// Token: 0x0600022C RID: 556 RVA: 0x00011762 File Offset: 0x0000F962
	protected override void Update()
	{
		base.Update();
		this.PostionUpdate();
	}

	// Token: 0x0600022D RID: 557 RVA: 0x00011770 File Offset: 0x0000F970
	private void PostionUpdate()
	{
		this.existTime += Time.deltaTime;
		float d = Mathf.Sin(this.existTime * this.frequency) * this.floatStrength;
		base.transform.position = this.startPos + Vector3.up * d;
	}

	// Token: 0x0600022E RID: 558 RVA: 0x000117CC File Offset: 0x0000F9CC
	public override void Die()
	{
		if (this.TargetZombie != null)
		{
			Vector2 vector = this.TargetZombie.shadow.transform.position;
			vector = new Vector2(vector.x, vector.y - 0.3f);
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

	// Token: 0x0600022F RID: 559 RVA: 0x000118A8 File Offset: 0x0000FAA8
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		this.colliders = Physics2D.OverlapBoxAll(this.startPos, this.range, 0f);
		Collider2D[] array = this.colliders;
		for (int i = 0; i < array.Length; i++)
		{
			Zombie zombie;
			if (array[i].TryGetComponent<Zombie>(out zombie) && this.AbleToAttack(zombie) && this.TargetZombie == null)
			{
				this.TargetZombie = zombie;
				this.anim.SetTrigger("searchzombie");
			}
		}
	}

	// Token: 0x06000230 RID: 560 RVA: 0x0001192C File Offset: 0x0000FB2C
	private void StartGrab()
	{
		if (this.TargetZombie != null && this.AbleToAttack(this.TargetZombie))
		{
			this.TargetZombie.theOriginSpeed = 0f;
			this.TargetZombie.GetComponent<Collider2D>().enabled = false;
			this.anim.SetTrigger("grab");
			this.SetLayer();
			Vector2 vector = this.TargetZombie.shadow.transform.position;
			this.grab.transform.position = new Vector3(vector.x - 0.75f, vector.y + 0.25f);
			GameAPP.PlaySound(62, 0.5f);
			Vector2 position = new Vector2(vector.x, vector.y - 0.2f);
			int theZombieType = this.TargetZombie.theZombieType;
			if (theZombieType != 14 && theZombieType != 200)
			{
				this.SetWaterSplat(position, new Vector2(0.27f, 0.27f));
			}
			GameAPP.PlaySound(71, 0.5f);
		}
	}

	// Token: 0x06000231 RID: 561 RVA: 0x00011A3C File Offset: 0x0000FC3C
	private void Grab()
	{
		Vector2 vector = this.TargetZombie.shadow.transform.position;
		GameAPP.PlaySound(71, 0.5f);
		if (this.TargetZombie.theZombieType == 14)
		{
			vector = new Vector2(vector.x, vector.y - 0.3f);
			this.SetWaterSplat(vector, new Vector2(1f, 1f));
		}
		else
		{
			vector = new Vector2(vector.x, vector.y - 0.1f);
			this.SetWaterSplat(vector, new Vector2(0.27f, 0.27f));
		}
		this.grab.transform.GetChild(0).localPosition = new Vector3(0f, 0f, 0f);
		base.StartCoroutine(this.MoveObject(this.grab.transform.GetChild(0).gameObject, false));
		base.StartCoroutine(this.MoveObject(this.TargetZombie.gameObject, true));
	}

	// Token: 0x06000232 RID: 562 RVA: 0x00011B48 File Offset: 0x0000FD48
	private void CrashZombie()
	{
		foreach (Collider2D collider2D in Physics2D.OverlapBoxAll(this.shadow.transform.position, new Vector2(1.5f, 3f), 0f))
		{
			Zombie zombie;
			if (!(collider2D.gameObject == this.TargetZombie) && collider2D.TryGetComponent<Zombie>(out zombie) && !zombie.isMindControlled && zombie.theZombieRow == this.thePlantRow)
			{
				zombie.TakeDamage(11, 600);
			}
		}
	}

	// Token: 0x06000233 RID: 563 RVA: 0x00011BD8 File Offset: 0x0000FDD8
	private void SetLayer()
	{
		foreach (object obj in this.grab.transform.GetChild(0).transform)
		{
			Transform transform = (Transform)obj;
			transform.GetComponent<SpriteRenderer>().sortingLayerName = string.Format("zombie{0}", this.thePlantRow);
			transform.GetComponent<SpriteRenderer>().sortingOrder = this.TargetZombie.baseLayer + 29;
		}
		SpriteMask component = this.grab.transform.GetChild(1).gameObject.GetComponent<SpriteMask>();
		component.frontSortingOrder = this.TargetZombie.baseLayer + 40;
		component.frontSortingLayerID = SortingLayer.NameToID(string.Format("zombie{0}", this.thePlantRow));
		component.backSortingOrder = this.TargetZombie.baseLayer;
		component.backSortingLayerID = SortingLayer.NameToID(string.Format("zombie{0}", this.thePlantRow));
	}

	// Token: 0x06000234 RID: 564 RVA: 0x00011CF0 File Offset: 0x0000FEF0
	private IEnumerator MoveObject(GameObject obj, bool isZombie)
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
			int num = this.grabTimes + 1;
			this.grabTimes = num;
			if (num == 3)
			{
				this.Die();
			}
		}
		yield break;
	}

	// Token: 0x06000235 RID: 565 RVA: 0x00011D10 File Offset: 0x0000FF10
	private GameObject SetWaterSplat(Vector2 position, Vector2 scale)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>("Particle/Anim/Water/WaterSplashPrefab"), position, Quaternion.identity, GameAPP.board.transform);
		this.SetParticleLayer(gameObject);
		gameObject.transform.localScale = scale;
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[32], position, Quaternion.identity, GameAPP.board.transform);
		return gameObject;
	}

	// Token: 0x06000236 RID: 566 RVA: 0x00011D80 File Offset: 0x0000FF80
	private void SetParticleLayer(GameObject particle)
	{
		foreach (object obj in particle.transform)
		{
			((Transform)obj).GetComponent<SpriteRenderer>().sortingLayerName = string.Format("particle{0}", this.thePlantRow);
		}
	}

	// Token: 0x06000237 RID: 567 RVA: 0x00011DF0 File Offset: 0x0000FFF0
	private bool AbleToAttack(Zombie zombie)
	{
		return zombie.theStatus != 1 && !zombie.isMindControlled && zombie.theZombieRow == this.thePlantRow;
	}

	// Token: 0x04000171 RID: 369
	protected Zombie TargetZombie;

	// Token: 0x04000172 RID: 370
	private Collider2D[] colliders;

	// Token: 0x04000173 RID: 371
	private Vector2 range = new Vector2(2f, 2f);

	// Token: 0x04000174 RID: 372
	private float existTime;

	// Token: 0x04000175 RID: 373
	private readonly float floatStrength = 0.05f;

	// Token: 0x04000176 RID: 374
	private readonly float frequency = 1.2f;

	// Token: 0x04000177 RID: 375
	protected GameObject grab;

	// Token: 0x04000178 RID: 376
	private int grabTimes;
}

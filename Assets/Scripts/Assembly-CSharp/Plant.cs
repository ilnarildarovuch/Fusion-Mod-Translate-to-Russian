using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class Plant : MonoBehaviour
{
	// Token: 0x060001ED RID: 493 RVA: 0x0000FF1C File Offset: 0x0000E11C
	protected virtual void Awake()
	{
		if (base.transform.Find("Shadow") != null)
		{
			this.shadow = base.transform.Find("Shadow").gameObject;
		}
		else
		{
			string str = "Failed to find shadow.";
			GameObject gameObject = base.gameObject;
			Debug.LogError(str + ((gameObject != null) ? gameObject.ToString() : null));
		}
		this.anim = base.GetComponent<Animator>();
		this.theConstSpeed = Random.Range(0.9f, 1.1f);
		this.zombieLayer = LayerMask.GetMask(new string[]
		{
			"Zombie"
		});
	}

	// Token: 0x060001EE RID: 494 RVA: 0x0000FFB9 File Offset: 0x0000E1B9
	protected virtual void Start()
	{
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0000FFBB File Offset: 0x0000E1BB
	protected virtual void Update()
	{
		this.PlantUpdate();
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000FFC3 File Offset: 0x0000E1C3
	protected virtual void FixedUpdate()
	{
		this.MouseFixedUpdate();
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x0000FFCC File Offset: 0x0000E1CC
	private void MouseFixedUpdate()
	{
		if (!this.alwaysLightUp && !this.isFlashing && this.brightness != 1f)
		{
			this.SetBrightness(base.gameObject, 1f);
		}
		if (Mouse.Instance.theItemOnMouse == null)
		{
			this.alwaysLightUp = false;
			return;
		}
		foreach (RaycastHit2D raycastHit2D in Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero))
		{
			if (raycastHit2D.collider.CompareTag("Plant") && raycastHit2D.collider.gameObject == base.gameObject)
			{
				if (Mouse.Instance.theItemOnMouse.name == "Shovel")
				{
					if (this.brightness != 2.5f)
					{
						this.SetBrightness(base.gameObject, 2.5f);
					}
					this.alwaysLightUp = true;
					return;
				}
				if (Mouse.Instance.theItemOnMouse.name == "Glove")
				{
					if (this.brightness != 2.5f)
					{
						this.SetBrightness(base.gameObject, 2.5f);
					}
					this.alwaysLightUp = true;
					return;
				}
			}
		}
		if (Mouse.Instance.theItemOnMouse.CompareTag("Preview") && !this.board.isIZ && MixData.data[this.thePlantType, Mouse.Instance.thePlantTypeOnMouse] != 0)
		{
			if (this.brightness != 2.5f)
			{
				this.SetBrightness(base.gameObject, 2.5f);
			}
			this.alwaysLightUp = true;
			return;
		}
		this.alwaysLightUp = false;
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x0001017C File Offset: 0x0000E37C
	protected virtual void PlantUpdate()
	{
		if (this.thePlantHealth <= 0)
		{
			this.Die();
		}
		this.anim.SetFloat("Speed", this.thePlantSpeed);
		if (this.isFromWheat && this.board.theCurrentNumOfZombieUncontroled > 0)
		{
			this.WheatUpdate();
		}
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x000101CC File Offset: 0x0000E3CC
	private void WheatUpdate()
	{
		this.wheatTime += Time.deltaTime;
		if (this.wheatTime > 30f)
		{
			this.Die();
			GameObject gameObject;
			for (;;)
			{
				int theSeedType = Random.Range(1000, 1076);
				if (!this.board.createPlant.IsPuff(theSeedType))
				{
					gameObject = this.board.createPlant.SetPlant(this.thePlantColumn, this.thePlantRow, theSeedType, null, default(Vector2), false, 0f);
					if (!(gameObject == null))
					{
						break;
					}
				}
			}
			gameObject.GetComponent<Plant>().isFromWheat = true;
			Vector2 vector = this.shadow.transform.position;
			vector = new Vector2(vector.x, vector.y + 0.5f);
			Object.Instantiate<GameObject>(GameAPP.particlePrefab[11], vector, Quaternion.identity, this.board.transform);
		}
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x000102BA File Offset: 0x0000E4BA
	public void FlashOnce()
	{
		if (this.alwaysLightUp)
		{
			return;
		}
		this.FlashPlant(base.gameObject);
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x000102D4 File Offset: 0x0000E4D4
	private void FlashPlant(GameObject obj)
	{
		if (obj.name == "Shadow")
		{
			return;
		}
		SpriteRenderer spriteRenderer;
		if (obj.TryGetComponent<SpriteRenderer>(out spriteRenderer))
		{
			base.StartCoroutine(this.FlashObject(spriteRenderer.material));
		}
		if (obj.transform.childCount > 0)
		{
			foreach (object obj2 in obj.transform)
			{
				Transform transform = (Transform)obj2;
				this.FlashPlant(transform.gameObject);
			}
		}
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x00010370 File Offset: 0x0000E570
	private IEnumerator FlashObject(Material mt)
	{
		if (this.alwaysLightUp)
		{
			yield break;
		}
		for (float i = 1f; i < 4f; i += 1f)
		{
			mt.SetFloat("_Brightness", i);
			this.brightness = i;
			this.isFlashing = true;
			yield return new WaitForFixedUpdate();
		}
		for (float i = 4f; i > 0.75f; i -= 0.25f)
		{
			mt.SetFloat("_Brightness", i);
			this.brightness = i;
			this.isFlashing = true;
			yield return new WaitForFixedUpdate();
		}
		this.isFlashing = false;
		yield break;
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x00010388 File Offset: 0x0000E588
	public virtual void Die()
	{
		if (Mouse.Instance.thePlantOnGlove == base.gameObject)
		{
			Object.Destroy(Mouse.Instance.theItemOnMouse);
			Mouse.Instance.theItemOnMouse = null;
			Mouse.Instance.thePlantTypeOnMouse = -1;
			Mouse.Instance.thePlantOnGlove = null;
		}
		if (this.board.isIZ && !this.board.isEveStarted)
		{
			this.GiveSunInIZ();
		}
		this.TryRemoveFromList();
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x00010410 File Offset: 0x0000E610
	protected bool TryRemoveFromList()
	{
		for (int i = 0; i < this.board.plantArray.Length; i++)
		{
			if (this.board.plantArray[i] == base.gameObject)
			{
				this.board.plantArray[i] = null;
				return true;
			}
		}
		return false;
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x00010460 File Offset: 0x0000E660
	protected virtual GameObject SearchZombie()
	{
		foreach (GameObject gameObject in GameAPP.board.GetComponent<Board>().zombieArray)
		{
			if (gameObject != null)
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (component.theZombieRow == this.thePlantRow && component.shadow.transform.position.x < 9.2f && component.shadow.transform.position.x > this.shadow.transform.position.x && this.SearchUniqueZombie(component))
				{
					return gameObject;
				}
			}
		}
		return null;
	}

	// Token: 0x060001FA RID: 506 RVA: 0x00010530 File Offset: 0x0000E730
	protected virtual void PlantShootUpdate()
	{
		this.thePlantAttackCountDown -= Time.deltaTime;
		if (this.thePlantAttackCountDown < 0f)
		{
			this.thePlantAttackCountDown = this.thePlantAttackInterval;
			this.thePlantAttackCountDown += Random.Range(-0.1f, 0.1f);
			if (this.SearchZombie() != null)
			{
				this.anim.SetTrigger("shoot");
				return;
			}
			if (this.board.isScaredyDream && this.thePlantType == 9)
			{
				this.anim.SetTrigger("shoot");
			}
		}
	}

	// Token: 0x060001FB RID: 507 RVA: 0x000105CA File Offset: 0x0000E7CA
	public virtual void ProducerUpdate()
	{
	}

	// Token: 0x060001FC RID: 508 RVA: 0x000105CC File Offset: 0x0000E7CC
	protected void SetBrightness(GameObject obj, float b)
	{
		this.brightness = b;
		SpriteRenderer spriteRenderer;
		if (obj.TryGetComponent<SpriteRenderer>(out spriteRenderer))
		{
			spriteRenderer.material.SetFloat("_Brightness", b);
		}
		if (obj.transform.childCount > 0)
		{
			foreach (object obj2 in obj.transform)
			{
				Transform transform = (Transform)obj2;
				this.SetBrightness(transform.gameObject, b);
			}
		}
	}

	// Token: 0x060001FD RID: 509 RVA: 0x0001065C File Offset: 0x0000E85C
	public void Recover(int health)
	{
		this.thePlantHealth += health;
		if (this.thePlantHealth > this.thePlantMaxHealth)
		{
			this.thePlantHealth = this.thePlantMaxHealth;
		}
		Vector3 position = this.shadow.transform.position;
		position = new Vector3(position.x, position.y + 0.7f, 1f);
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[16], this.board.transform).transform.position = position;
	}

	// Token: 0x060001FE RID: 510 RVA: 0x000106E4 File Offset: 0x0000E8E4
	public void Crashed()
	{
		if (this.isCrashed)
		{
			return;
		}
		int i = this.thePlantType;
		if (i <= 1003)
		{
			if (i == 2)
			{
				base.GetComponent<CherryBomb>().Bomb();
				goto IL_130;
			}
			switch (i)
			{
			case 10:
				this.board.CreateFreeze(this.shadow.transform.position);
				this.Die();
				goto IL_130;
			case 11:
				base.GetComponent<DoomShroom>().AnimExplode();
				this.Die();
				goto IL_130;
			case 12:
			case 14:
				goto IL_130;
			case 13:
			case 15:
			case 17:
				break;
			case 16:
				base.GetComponent<Jalapeno>().AnimExplode();
				goto IL_130;
			default:
				if (i != 1003)
				{
					goto IL_130;
				}
				goto IL_12A;
			}
		}
		else if (i <= 1040)
		{
			if (i == 1010)
			{
				goto IL_12A;
			}
			if (i != 1040)
			{
				goto IL_130;
			}
			base.GetComponent<IceDoom>().AnimExplode();
			this.Die();
			goto IL_130;
		}
		else
		{
			switch (i)
			{
			case 1049:
			case 1050:
			case 1051:
			case 1054:
			case 1057:
			case 1060:
				break;
			case 1052:
			case 1053:
				goto IL_12A;
			case 1055:
			case 1056:
			case 1058:
			case 1059:
				goto IL_130;
			default:
				if (i != 1066)
				{
					goto IL_130;
				}
				break;
			}
		}
		return;
		IL_12A:
		this.Die();
		IL_130:
		this.isCrashed = true;
		if (this.board.boxType[this.thePlantColumn, this.thePlantRow] == 1)
		{
			this.Die();
			return;
		}
		if (this.board.isIZ && !this.board.isEveStarted)
		{
			this.GiveSunInIZ();
		}
		Object.Destroy(this.anim);
		Vector3 position = this.shadow.transform.position;
		base.transform.localScale = new Vector3(base.transform.localScale.x, 0.3f * base.transform.localScale.y);
		Vector3 position2 = this.shadow.transform.position;
		Vector3 b = position - position2;
		base.transform.position += b;
		this.shadow.SetActive(false);
		if (Mouse.Instance.thePlantOnGlove == base.gameObject)
		{
			Object.Destroy(Mouse.Instance.theItemOnMouse);
			Mouse.Instance.theItemOnMouse = null;
			Mouse.Instance.thePlantTypeOnMouse = -1;
			Mouse.Instance.thePlantOnGlove = null;
		}
		this.TryRemoveFromList();
		Collider2D[] components = base.GetComponents<Collider2D>();
		for (i = 0; i < components.Length; i++)
		{
			Object.Destroy(components[i]);
		}
		Object.Destroy(base.gameObject, 3f);
		Object.Destroy(this);
	}

	// Token: 0x060001FF RID: 511 RVA: 0x0001097C File Offset: 0x0000EB7C
	private void GiveSunInIZ()
	{
		CreateCoin.Instance.SetCoin(this.thePlantColumn, this.thePlantRow, 0, 0, default(Vector3));
		CreateCoin.Instance.SetCoin(this.thePlantColumn, this.thePlantRow, 0, 0, default(Vector3));
		CreateCoin.Instance.SetCoin(this.thePlantColumn, this.thePlantRow, 0, 0, default(Vector3));
		CreateCoin.Instance.SetCoin(this.thePlantColumn, this.thePlantRow, 0, 0, default(Vector3));
		CreateCoin.Instance.SetCoin(this.thePlantColumn, this.thePlantRow, 0, 0, default(Vector3));
	}

	// Token: 0x06000200 RID: 512 RVA: 0x00010A34 File Offset: 0x0000EC34
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
		return theStatus != 1 && theStatus != 3 && theStatus != 9 && (this.thePlantType == 1004 || zombie.theStatus != 7);
	}

	// Token: 0x06000201 RID: 513 RVA: 0x00010A88 File Offset: 0x0000EC88
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
		return theStatus != 3 && theStatus != 9;
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00010AC0 File Offset: 0x0000ECC0
	public void SetColor(GameObject obj, Color color)
	{
		if (obj.name == "Shadow")
		{
			return;
		}
		SpriteRenderer spriteRenderer;
		if (obj.TryGetComponent<SpriteRenderer>(out spriteRenderer))
		{
			spriteRenderer.color = color;
		}
		if (obj.transform.childCount != 0)
		{
			foreach (object obj2 in obj.transform)
			{
				Transform transform = (Transform)obj2;
				this.SetColor(transform.gameObject, color);
			}
		}
	}

	// Token: 0x04000146 RID: 326
	public Board board;

	// Token: 0x04000147 RID: 327
	public int thePlantColumn;

	// Token: 0x04000148 RID: 328
	public int thePlantRow;

	// Token: 0x04000149 RID: 329
	public int thePlantType;

	// Token: 0x0400014A RID: 330
	public int thePlantMaxHealth = 300;

	// Token: 0x0400014B RID: 331
	public int thePlantHealth = 300;

	// Token: 0x0400014C RID: 332
	public float thePlantSpeed = 1f;

	// Token: 0x0400014D RID: 333
	protected float theConstSpeed;

	// Token: 0x0400014E RID: 334
	public float thePlantAttackInterval;

	// Token: 0x0400014F RID: 335
	public float thePlantAttackCountDown;

	// Token: 0x04000150 RID: 336
	public float thePlantProduceInterval;

	// Token: 0x04000151 RID: 337
	public float thePlantProduceCountDown;

	// Token: 0x04000152 RID: 338
	public float attributeCountdown;

	// Token: 0x04000153 RID: 339
	public Vector3 startPos;

	// Token: 0x04000154 RID: 340
	public bool isPot;

	// Token: 0x04000155 RID: 341
	public bool isLily;

	// Token: 0x04000156 RID: 342
	public bool isPumpkin;

	// Token: 0x04000157 RID: 343
	public bool isFly;

	// Token: 0x04000158 RID: 344
	public bool isAshy;

	// Token: 0x04000159 RID: 345
	public bool isNut;

	// Token: 0x0400015A RID: 346
	protected Animator anim;

	// Token: 0x0400015B RID: 347
	private bool alwaysLightUp;

	// Token: 0x0400015C RID: 348
	public GameObject shadow;

	// Token: 0x0400015D RID: 349
	public int baseLayer;

	// Token: 0x0400015E RID: 350
	private float brightness = 1f;

	// Token: 0x0400015F RID: 351
	private bool isFlashing;

	// Token: 0x04000160 RID: 352
	public int place;

	// Token: 0x04000161 RID: 353
	public bool isShort;

	// Token: 0x04000162 RID: 354
	public bool isFromWheat;

	// Token: 0x04000163 RID: 355
	private float wheatTime;

	// Token: 0x04000164 RID: 356
	public bool adjustPosByLily;

	// Token: 0x04000165 RID: 357
	private bool isCrashed;

	// Token: 0x04000166 RID: 358
	protected List<Zombie> zombieList = new List<Zombie>();

	// Token: 0x04000167 RID: 359
	public int zombieLayer;
}

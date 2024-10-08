using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000059 RID: 89
public class Bucket : MonoBehaviour
{
	// Token: 0x060001B2 RID: 434 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
	protected virtual void Start()
	{
		if (GameAPP.board.GetComponent<Board>().isIZ)
		{
			Object.Destroy(base.gameObject);
		}
		base.transform.Translate(0f, 0f, -1f);
		this.startPosition = base.transform.position;
		this.velocity = new Vector2(Random.Range(-1.5f, 1.5f), this.verticalSpeed);
		this.m = GameAPP.board.GetComponent<Mouse>();
		GameAPP.board.GetComponent<Board>().theTotalNumOfCoin++;
		int num = GameAPP.board.GetComponent<Board>().theTotalNumOfCoin;
		if (num > 1000)
		{
			num %= 1000;
		}
		int num2 = 5 * num;
		SpriteRenderer spriteRenderer;
		if (base.TryGetComponent<SpriteRenderer>(out spriteRenderer))
		{
			spriteRenderer.sortingOrder += num2;
		}
		if (base.transform.childCount != 0)
		{
			using (IEnumerator enumerator = base.transform.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SpriteRenderer spriteRenderer2;
					if (((Transform)enumerator.Current).TryGetComponent<SpriteRenderer>(out spriteRenderer2))
					{
						spriteRenderer2.sortingOrder += num2;
					}
				}
			}
		}
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x0000E918 File Offset: 0x0000CB18
	protected virtual void Update()
	{
		this.PositionUpdate();
		this.existTime += Time.deltaTime;
		if (this.existTime > 4f * GameAPP.gameSpeed && !this.isFlash)
		{
			this.isFlash = true;
			base.StartCoroutine(this.Flash());
		}
		if (this.existTime > 8f * GameAPP.gameSpeed && this.m.theItemOnMouse != base.gameObject)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x0000E9A2 File Offset: 0x0000CBA2
	private IEnumerator Flash()
	{
		Color color = Color.white;
		bool decrease = true;
		for (;;)
		{
			SpriteRenderer spriteRenderer;
			if (this.m.theItemOnMouse != base.gameObject && base.TryGetComponent<SpriteRenderer>(out spriteRenderer))
			{
				if (spriteRenderer.color.r > 0.5f && decrease)
				{
					color.r -= this.existTime * 2f / 255f;
					color.g -= this.existTime * 2f / 255f;
					color.b -= this.existTime * 2f / 255f;
					spriteRenderer.color = color;
					if (spriteRenderer.color.r < 0.5f)
					{
						decrease = false;
					}
				}
				else
				{
					color.r += this.existTime * 2f / 255f;
					color.g += this.existTime * 2f / 255f;
					color.b += this.existTime * 2f / 255f;
					spriteRenderer.color = color;
					if (spriteRenderer.color.r > 0.9f)
					{
						decrease = true;
					}
				}
			}
			yield return new WaitForFixedUpdate();
		}
		yield break;
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
	protected void PositionUpdate()
	{
		if (!this.isLand)
		{
			this.velocity.y = this.velocity.y - this.gravity * Time.deltaTime;
			base.transform.Translate(this.velocity * Time.deltaTime);
			if (base.transform.position.y < this.startPosition.y - 1f)
			{
				this.isLand = true;
			}
		}
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x0000EA2E File Offset: 0x0000CC2E
	public virtual void Pick()
	{
		this.isLand = true;
		this.m.theItemOnMouse = base.gameObject;
		this.m.thePlantOnGlove = null;
		this.m.thePlantTypeOnMouse = -1;
		base.GetComponent<Collider2D>().enabled = false;
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x0000EA6C File Offset: 0x0000CC6C
	public virtual void Use()
	{
		Vector2 vector = new Vector2((float)this.m.theMouseColumn, (float)this.m.theMouseRow);
		foreach (GameObject gameObject in GameAPP.board.GetComponent<Board>().plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				if ((float)component.thePlantColumn == vector.x && (float)component.thePlantRow == vector.y)
				{
					int thePlantType = component.thePlantType;
					if (thePlantType <= 3)
					{
						if (thePlantType != 0)
						{
							if (thePlantType == 3)
							{
								component.Die();
								GameAPP.board.GetComponent<CreatePlant>().SetPlant(component.thePlantColumn, component.thePlantRow, 1029, null, default(Vector2), false, 0f);
								Object.Destroy(base.gameObject);
							}
						}
						else
						{
							component.Die();
							GameAPP.board.GetComponent<CreatePlant>().SetPlant(component.thePlantColumn, component.thePlantRow, 1020, null, default(Vector2), false, 0f);
							Object.Destroy(base.gameObject);
						}
					}
					else if (thePlantType == 1020 || thePlantType - 1028 <= 1)
					{
						component.Recover(1000);
						Object.Destroy(base.gameObject);
					}
				}
			}
		}
		base.GetComponent<Collider2D>().enabled = true;
	}

	// Token: 0x04000122 RID: 290
	private readonly float verticalSpeed = 4f;

	// Token: 0x04000123 RID: 291
	protected float gravity = 9.8f;

	// Token: 0x04000124 RID: 292
	private Vector2 velocity;

	// Token: 0x04000125 RID: 293
	private Vector2 startPosition;

	// Token: 0x04000126 RID: 294
	private bool isLand;

	// Token: 0x04000127 RID: 295
	protected Mouse m;

	// Token: 0x04000128 RID: 296
	protected float existTime;

	// Token: 0x04000129 RID: 297
	private bool isFlash;
}

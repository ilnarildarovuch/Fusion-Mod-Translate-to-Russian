using System;
using UnityEngine;

// Token: 0x0200005D RID: 93
public class GridItem : MonoBehaviour
{
	// Token: 0x060001BF RID: 447 RVA: 0x0000EE2E File Offset: 0x0000D02E
	private void Start()
	{
		this.r = base.GetComponent<SpriteRenderer>();
		this.crater = this.r.sprite;
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x0000EE4D File Offset: 0x0000D04D
	private void Update()
	{
		this.existTime += Time.deltaTime;
		if (this.itemType == 0)
		{
			this.CraterUpdate();
		}
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x0000EE70 File Offset: 0x0000D070
	private void CraterUpdate()
	{
		if (this.board.isIZ)
		{
			this.Die();
		}
		if (this.existTime > 90f)
		{
			this.r.sprite = this.crater_fading;
			if (this.existTime > 180f)
			{
				this.Die();
				return;
			}
		}
		else
		{
			this.r.sprite = this.crater;
		}
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x0000EED4 File Offset: 0x0000D0D4
	private void Die()
	{
		for (int i = 0; i < this.board.griditemArray.Length; i++)
		{
			if (this.board.griditemArray[i] == base.gameObject)
			{
				this.board.griditemArray[i] = null;
			}
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0000EF2C File Offset: 0x0000D12C
	public static void CreateGridItem(int theColumn, int theRow, int theType)
	{
		Board component = GameAPP.board.GetComponent<Board>();
		float boxXFromColumn = GameAPP.board.GetComponent<Mouse>().GetBoxXFromColumn(theColumn);
		Vector2 v;
		if (component.roadNum == 5)
		{
			v = new Vector2(boxXFromColumn, 2.5f - 1.65f * (float)theRow);
		}
		else
		{
			v = new Vector2(boxXFromColumn, 2.5f - 1.4f * (float)theRow);
		}
		GameObject gameObject = Object.Instantiate<GameObject>(GameAPP.gridItemPrefab[theType], v, Quaternion.identity, component.transform);
		GridItem component2 = gameObject.GetComponent<GridItem>();
		component2.board = component;
		component2.theItemColumn = theColumn;
		component2.theItemRow = theRow;
		for (int i = 0; i < component.griditemArray.Length; i++)
		{
			if (component.griditemArray[i] == null)
			{
				component.griditemArray[i] = gameObject;
				return;
			}
		}
	}

	// Token: 0x0400012A RID: 298
	private float existTime;

	// Token: 0x0400012B RID: 299
	public int itemType;

	// Token: 0x0400012C RID: 300
	public int theItemRow;

	// Token: 0x0400012D RID: 301
	public int theItemColumn;

	// Token: 0x0400012E RID: 302
	private Sprite crater;

	// Token: 0x0400012F RID: 303
	public Sprite crater_fading;

	// Token: 0x04000130 RID: 304
	private SpriteRenderer r;

	// Token: 0x04000131 RID: 305
	public Board board;
}

using System;
using UnityEngine;

// Token: 0x02000029 RID: 41
public class BackToAlmanac : MonoBehaviour
{
	// Token: 0x060000B6 RID: 182 RVA: 0x00005B21 File Offset: 0x00003D21
	private void Start()
	{
		this.originPosition = base.transform.position;
		this.r = base.GetComponent<SpriteRenderer>();
		this.originSprite = this.r.sprite;
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00005B51 File Offset: 0x00003D51
	private void OnMouseEnter()
	{
		this.r.sprite = this.highLightSprite;
		CursorChange.SetClickCursor();
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00005B69 File Offset: 0x00003D69
	private void OnMouseExit()
	{
		base.transform.position = this.originPosition;
		this.r.sprite = this.originSprite;
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00005B92 File Offset: 0x00003D92
	private void OnMouseDown()
	{
		GameAPP.PlaySound(29, 0.5f);
		base.transform.position = new Vector3(this.originPosition.x + 0.02f, this.originPosition.y - 0.02f);
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00005BD2 File Offset: 0x00003DD2
	private void OnMouseUp()
	{
		CursorChange.SetDefaultCursor();
		base.transform.position = this.originPosition;
		Object.Destroy(base.transform.parent.gameObject);
		UIMgr.EnterAlmanac(false);
	}

	// Token: 0x0400009B RID: 155
	public Sprite highLightSprite;

	// Token: 0x0400009C RID: 156
	private Sprite originSprite;

	// Token: 0x0400009D RID: 157
	private Vector3 originPosition;

	// Token: 0x0400009E RID: 158
	private SpriteRenderer r;
}

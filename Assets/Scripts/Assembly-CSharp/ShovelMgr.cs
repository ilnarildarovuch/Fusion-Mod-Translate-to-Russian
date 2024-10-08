using System;
using UnityEngine;

// Token: 0x02000019 RID: 25
public class ShovelMgr : MonoBehaviour
{
	// Token: 0x0600006A RID: 106 RVA: 0x00004668 File Offset: 0x00002868
	private void Start()
	{
		this.m = GameAPP.board.GetComponent<Mouse>();
	}

	// Token: 0x0600006B RID: 107 RVA: 0x0000467A File Offset: 0x0000287A
	public void PickUp()
	{
		this.isPickUp = true;
		base.GetComponent<BoxCollider2D>().enabled = false;
		base.transform.SetParent(GameAPP.canvasUp.transform);
	}

	// Token: 0x0600006C RID: 108 RVA: 0x000046A4 File Offset: 0x000028A4
	public void PutDown()
	{
		this.isPickUp = false;
		base.GetComponent<BoxCollider2D>().enabled = true;
		base.transform.SetParent(this.defaultParent.transform);
		base.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
	}

	// Token: 0x0600006D RID: 109 RVA: 0x000046F4 File Offset: 0x000028F4
	private void OnMouseEnter()
	{
		if (this.m.theItemOnMouse == null)
		{
			CursorChange.SetClickCursor();
		}
	}

	// Token: 0x0600006E RID: 110 RVA: 0x0000470E File Offset: 0x0000290E
	private void OnMouseExit()
	{
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00004718 File Offset: 0x00002918
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1) && GameAPP.theGameStatus == 0 && !this.isPickUp && this.m.theItemOnMouse == null)
		{
			this.m.theItemOnMouse = base.gameObject;
			GameAPP.PlaySound(21, 0.5f);
			this.PickUp();
		}
	}

	// Token: 0x0400005F RID: 95
	public bool isPickUp;

	// Token: 0x04000060 RID: 96
	public GameObject defaultParent;

	// Token: 0x04000061 RID: 97
	protected Mouse m;
}

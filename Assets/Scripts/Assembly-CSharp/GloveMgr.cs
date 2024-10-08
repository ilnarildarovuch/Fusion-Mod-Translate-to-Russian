using System;
using UnityEngine;

// Token: 0x02000010 RID: 16
public class GloveMgr : MonoBehaviour
{
	// Token: 0x0600002D RID: 45 RVA: 0x00003074 File Offset: 0x00001274
	private void Start()
	{
		this.r = base.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
		this.m = GameAPP.board.GetComponent<Mouse>();
		if (GameAPP.theBoardType == 1)
		{
			int theBoardLevel = GameAPP.theBoardLevel;
			if (theBoardLevel == 3 || theBoardLevel == 15 || theBoardLevel == 17)
			{
				this.fullCD = 3f;
			}
		}
	}

	// Token: 0x0600002E RID: 46 RVA: 0x000030D4 File Offset: 0x000012D4
	public void PickUp()
	{
		this.isPickUp = true;
		base.GetComponent<BoxCollider2D>().enabled = false;
		base.transform.SetParent(GameAPP.canvasUp.transform);
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00003100 File Offset: 0x00001300
	public void PutDown()
	{
		this.isPickUp = false;
		base.GetComponent<BoxCollider2D>().enabled = true;
		base.transform.SetParent(this.defaultParent.transform);
		base.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00003150 File Offset: 0x00001350
	private void OnMouseEnter()
	{
		if (this.m.theItemOnMouse == null)
		{
			CursorChange.SetClickCursor();
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x0000316A File Offset: 0x0000136A
	private void OnMouseExit()
	{
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00003174 File Offset: 0x00001374
	private void Update()
	{
		this.CDUpdate();
		if (Input.GetKeyDown(KeyCode.Alpha2) && GameAPP.theGameStatus == 0 && !this.isPickUp && this.avaliable && this.m.theItemOnMouse == null)
		{
			this.m.theItemOnMouse = base.gameObject;
			GameAPP.PlaySound(19, 0.5f);
			this.PickUp();
		}
	}

	// Token: 0x06000033 RID: 51 RVA: 0x000031E0 File Offset: 0x000013E0
	private void CDUpdate()
	{
		this.r.anchoredPosition = new Vector2(0f, this.CD * (10f / this.fullCD) * 7.5f);
		if (GameAPP.developerMode)
		{
			this.CD = this.fullCD;
		}
		if (this.CD < this.fullCD)
		{
			this.CD += Time.deltaTime;
			this.avaliable = false;
			if (this.CD > this.fullCD)
			{
				this.avaliable = true;
				this.CD = this.fullCD;
			}
		}
		if (this.CD >= this.fullCD)
		{
			this.avaliable = true;
		}
	}

	// Token: 0x04000032 RID: 50
	public bool isPickUp;

	// Token: 0x04000033 RID: 51
	public GameObject defaultParent;

	// Token: 0x04000034 RID: 52
	private float fullCD = 10f;

	// Token: 0x04000035 RID: 53
	public float CD;

	// Token: 0x04000036 RID: 54
	public bool avaliable = true;

	// Token: 0x04000037 RID: 55
	private RectTransform r;

	// Token: 0x04000038 RID: 56
	protected Mouse m;
}

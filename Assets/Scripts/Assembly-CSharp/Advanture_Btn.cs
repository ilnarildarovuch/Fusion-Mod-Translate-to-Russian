using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200000A RID: 10
public class Advanture_Btn : MonoBehaviour
{
	// Token: 0x06000013 RID: 19 RVA: 0x00002220 File Offset: 0x00000420
	private void Start()
	{
		if (base.name == "Window")
		{
			this.rectTransform = base.transform.parent.gameObject.GetComponent<RectTransform>();
		}
		else
		{
			this.rectTransform = base.GetComponent<RectTransform>();
		}
		this.originPosition = this.rectTransform.anchoredPosition;
		this.image = base.GetComponent<Image>();
		this.originSprite = this.image.sprite;
		if (this.levelType == 0 && this.buttonNumber > 0 && GameAPP.advLevelCompleted[this.buttonNumber])
		{
			base.transform.GetChild(1).gameObject.SetActive(true);
		}
		if (this.levelType == 1 && this.buttonNumber > 0 && GameAPP.clgLevelCompleted[this.buttonNumber])
		{
			base.transform.GetChild(1).gameObject.SetActive(true);
		}
		if (this.levelType == 2 && this.buttonNumber > 0 && GameAPP.gameLevelCompleted[this.buttonNumber])
		{
			base.transform.GetChild(1).gameObject.SetActive(true);
		}
		if (this.levelType == 3 && this.buttonNumber > 0 && GameAPP.survivalLevelCompleted[this.buttonNumber])
		{
			base.transform.GetChild(1).gameObject.SetActive(true);
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002376 File Offset: 0x00000576
	private void OnMouseEnter()
	{
		this.image.sprite = this.highLightSprite;
		CursorChange.SetClickCursor();
	}

	// Token: 0x06000015 RID: 21 RVA: 0x0000238E File Offset: 0x0000058E
	private void OnMouseExit()
	{
		this.image.sprite = this.originSprite;
		this.rectTransform.anchoredPosition = this.originPosition;
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000023BC File Offset: 0x000005BC
	private void OnMouseDown()
	{
		GameAPP.PlaySound(29, 0.5f);
		this.rectTransform.anchoredPosition = new Vector2(this.originPosition.x + 1f, this.originPosition.y - 1f);
	}

	// Token: 0x06000017 RID: 23 RVA: 0x000023FC File Offset: 0x000005FC
	private void OnMouseUp()
	{
		this.rectTransform.anchoredPosition = this.originPosition;
		switch (this.buttonNumber)
		{
		case -3:
			if (this.levelCtrl.currentPage > 0)
			{
				this.levelCtrl.ChangePage(this.levelCtrl.currentPage - 1);
				return;
			}
			break;
		case -2:
			if (this.levelCtrl.currentPage < 1)
			{
				this.levelCtrl.ChangePage(this.levelCtrl.currentPage + 1);
				return;
			}
			break;
		case -1:
			CursorChange.SetDefaultCursor();
			UIMgr.EnterMainMenu();
			return;
		default:
			CursorChange.SetDefaultCursor();
			UIMgr.EnterGame(this.levelType, this.buttonNumber, null);
			break;
		}
	}

	// Token: 0x0400000A RID: 10
	public Sprite highLightSprite;

	// Token: 0x0400000B RID: 11
	public int levelType;

	// Token: 0x0400000C RID: 12
	public int buttonNumber;

	// Token: 0x0400000D RID: 13
	public GameObject thisMenu;

	// Token: 0x0400000E RID: 14
	public ClgLevelMgr levelCtrl;

	// Token: 0x0400000F RID: 15
	private Sprite originSprite;

	// Token: 0x04000010 RID: 16
	private Image image;

	// Token: 0x04000011 RID: 17
	private Vector3 originPosition;

	// Token: 0x04000012 RID: 18
	private RectTransform rectTransform;
}

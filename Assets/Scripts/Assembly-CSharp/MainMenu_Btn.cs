using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200001E RID: 30
public class MainMenu_Btn : MonoBehaviour
{
	// Token: 0x06000087 RID: 135 RVA: 0x00004BC0 File Offset: 0x00002DC0
	private void Start()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.originPosition = this.rectTransform.anchoredPosition;
		this.image = base.GetComponent<Image>();
		this.originSprite = this.image.sprite;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00004C0C File Offset: 0x00002E0C
	private void OnMouseEnter()
	{
		if (GameAPP.theGameStatus == -1)
		{
			this.image.sprite = this.highLightSprite;
			GameAPP.PlaySound(27, 0.5f);
			CursorChange.SetClickCursor();
		}
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00004C38 File Offset: 0x00002E38
	private void OnMouseExit()
	{
		this.image.sprite = this.originSprite;
		this.rectTransform.anchoredPosition = this.originPosition;
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00004C68 File Offset: 0x00002E68
	private void OnMouseDown()
	{
		if (GameAPP.theGameStatus != -1)
		{
			return;
		}
		int num = this.buttonNumber;
		if (num <= 3)
		{
			GameAPP.PlaySound(28, 0.5f);
		}
		else
		{
			GameAPP.PlaySound(19, 0.5f);
		}
		this.rectTransform.anchoredPosition = new Vector2(this.originPosition.x + 1f, this.originPosition.y - 1f);
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00004CD8 File Offset: 0x00002ED8
	private void OnMouseUp()
	{
		CursorChange.SetDefaultCursor();
		this.image.sprite = this.originSprite;
		this.rectTransform.anchoredPosition = this.originPosition;
		if (GameAPP.theGameStatus != -1)
		{
			return;
		}
		this.rectTransform.anchoredPosition = this.originPosition;
		switch (this.buttonNumber)
		{
		case 0:
			UIMgr.EnterAdvantureMenu();
			Object.Destroy(this.thisMenu);
			return;
		case 1:
			UIMgr.EnterChallengeMenu();
			Object.Destroy(this.thisMenu);
			return;
		case 2:
			UIMgr.EnterIZEMenu();
			Object.Destroy(this.thisMenu);
			return;
		case 3:
			UIMgr.EnterSurvivalEMenu();
			Object.Destroy(this.thisMenu);
			return;
		case 4:
			GameAPP.theGameStatus = -2;
			UIMgr.EnterOtherMenu();
			return;
		case 5:
			UIMgr.EnterAlmanac(true);
			Object.Destroy(this.thisMenu);
			return;
		case 6:
			GameAPP.theGameStatus = -2;
			UIMgr.EnterOtherMenu();
			return;
		case 7:
			GameAPP.theGameStatus = -2;
			UIMgr.EnterOtherMenu();
			return;
		case 8:
			GameAPP.theGameStatus = -2;
			UIMgr.EnterPauseMenu(1);
			return;
		case 9:
			GameAPP.theGameStatus = -2;
			UIMgr.EnterHelpMenu();
			return;
		case 10:
			Application.Quit();
			return;
		default:
			return;
		}
	}

	// Token: 0x04000070 RID: 112
	public Sprite highLightSprite;

	// Token: 0x04000071 RID: 113
	public int buttonNumber;

	// Token: 0x04000072 RID: 114
	public GameObject thisMenu;

	// Token: 0x04000073 RID: 115
	private Sprite originSprite;

	// Token: 0x04000074 RID: 116
	private Image image;

	// Token: 0x04000075 RID: 117
	private Vector3 originPosition;

	// Token: 0x04000076 RID: 118
	private RectTransform rectTransform;
}

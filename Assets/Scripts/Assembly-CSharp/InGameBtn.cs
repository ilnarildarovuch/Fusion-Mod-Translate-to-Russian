using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000012 RID: 18
public class InGameBtn : MonoBehaviour
{
	// Token: 0x06000036 RID: 54 RVA: 0x000032B0 File Offset: 0x000014B0
	private void Start()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.originPosition = this.rectTransform.anchoredPosition;
		this.image = base.GetComponent<Image>();
		this.originSprite = this.image.sprite;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x000032FC File Offset: 0x000014FC
	private void OnMouseEnter()
	{
		this.image.sprite = this.highLightSprite;
		CursorChange.SetClickCursor();
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00003314 File Offset: 0x00001514
	private void OnMouseExit()
	{
		this.image.sprite = this.originSprite;
		if (this.buttonNumber != 3)
		{
			this.rectTransform.anchoredPosition = this.originPosition;
		}
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x06000039 RID: 57 RVA: 0x0000334C File Offset: 0x0000154C
	private void OnMouseDown()
	{
		if (this.buttonNumber == 0 || this.buttonNumber == 3)
		{
			GameAPP.PlaySound(28, 0.5f);
		}
		else
		{
			GameAPP.PlaySound(19, 0.5f);
		}
		if (this.buttonNumber != 3)
		{
			this.rectTransform.anchoredPosition = new Vector2(this.originPosition.x + 1f, this.originPosition.y - 1f);
		}
	}

	// Token: 0x0600003A RID: 58 RVA: 0x000033C0 File Offset: 0x000015C0
	private void OnMouseUp()
	{
		CursorChange.SetDefaultCursor();
		if (this.buttonNumber != 3)
		{
			this.rectTransform.anchoredPosition = this.originPosition;
		}
		int num = this.buttonNumber;
		if (num != 0)
		{
			if (num != 3)
			{
				return;
			}
			if (GameAPP.theGameStatus == 0)
			{
				this.SpeedTrigger();
			}
		}
		else
		{
			if (GameAPP.theGameStatus == 0 && GameAPP.board.GetComponent<Board>().isIZ)
			{
				base.transform.parent.GetComponent<IZEMgr>().PauseGame();
				return;
			}
			if (!GameAPP.board.GetComponent<Board>().isIZ)
			{
				Object.Destroy(GameAPP.board);
				UIMgr.EnterMainMenu();
				return;
			}
		}
	}

	// Token: 0x0600003B RID: 59 RVA: 0x0000345C File Offset: 0x0000165C
	private void Update()
	{
		if (GameAPP.theGameStatus == 0 && this.buttonNumber == 3 && Input.GetKeyDown(KeyCode.Alpha3))
		{
			this.SpeedTrigger();
		}
	}

	// Token: 0x0600003C RID: 60 RVA: 0x0000347D File Offset: 0x0000167D
	private void SpeedTrigger()
	{
		this.speedTrigger = !this.speedTrigger;
		if (this.speedTrigger)
		{
			Time.timeScale = 0.2f;
			return;
		}
		Time.timeScale = GameAPP.gameSpeed;
	}

	// Token: 0x04000039 RID: 57
	public Sprite highLightSprite;

	// Token: 0x0400003A RID: 58
	public int buttonNumber;

	// Token: 0x0400003B RID: 59
	public GameObject thisMenu;

	// Token: 0x0400003C RID: 60
	private Sprite originSprite;

	// Token: 0x0400003D RID: 61
	private Image image;

	// Token: 0x0400003E RID: 62
	private Vector3 originPosition;

	// Token: 0x0400003F RID: 63
	private RectTransform rectTransform;

	// Token: 0x04000040 RID: 64
	private bool speedTrigger;
}

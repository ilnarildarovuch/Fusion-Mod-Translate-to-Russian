using System;
using UnityEngine;

// Token: 0x02000022 RID: 34
public class PauseMenu_Btn : MonoBehaviour
{
	// Token: 0x06000094 RID: 148 RVA: 0x00004E58 File Offset: 0x00003058
	private void Start()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.originPosition = this.rectTransform.anchoredPosition;
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00004E7C File Offset: 0x0000307C
	private void OnMouseEnter()
	{
		CursorChange.SetClickCursor();
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00004E83 File Offset: 0x00003083
	private void OnMouseExit()
	{
		this.rectTransform.anchoredPosition = this.originPosition;
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00004EA0 File Offset: 0x000030A0
	private void OnMouseDown()
	{
		GameAPP.PlaySoundNotPause(28, 0.5f);
		this.rectTransform.anchoredPosition = new Vector2(this.originPosition.x + 1f, this.originPosition.y - 1f);
	}

	// Token: 0x06000098 RID: 152 RVA: 0x00004EE0 File Offset: 0x000030E0
	private void OnMouseUp()
	{
		CursorChange.SetDefaultCursor();
		this.rectTransform.anchoredPosition = this.originPosition;
		switch (this.buttonNumber)
		{
		case 1:
			PauseMenuMgr.Instance.checkRestart.SetActive(true);
			PauseMenuMgr.Instance.btnQuit.GetComponent<Collider2D>().enabled = false;
			PauseMenuMgr.Instance.btnRestart.GetComponent<Collider2D>().enabled = false;
			return;
		case 2:
			PauseMenuMgr.Instance.checkQuit.SetActive(true);
			PauseMenuMgr.Instance.btnQuit.GetComponent<Collider2D>().enabled = false;
			PauseMenuMgr.Instance.btnRestart.GetComponent<Collider2D>().enabled = false;
			return;
		case 3:
		case 7:
		case 8:
		case 9:
			break;
		case 4:
			PauseMenuMgr.Instance.checkQuit.SetActive(false);
			PauseMenuMgr.Instance.checkRestart.SetActive(false);
			PauseMenuMgr.Instance.btnQuit.GetComponent<Collider2D>().enabled = true;
			PauseMenuMgr.Instance.btnRestart.GetComponent<Collider2D>().enabled = true;
			return;
		case 5:
			if (Board.Instance.isEndless)
			{
				Board.Instance.ClearTheBoard();
			}
			this.Restart();
			return;
		case 6:
			Object.Destroy(GameAPP.board);
			GameAPP.board = null;
			UIMgr.EnterMainMenu();
			return;
		case 10:
			UIMgr.BackToGame(this.thisMenu);
			break;
		default:
			return;
		}
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00005040 File Offset: 0x00003240
	private void Restart()
	{
		foreach (object obj in GameAPP.canvasUp.transform)
		{
			Transform transform = (Transform)obj;
			if (transform != null)
			{
				Object.Destroy(transform.gameObject);
			}
		}
		foreach (object obj2 in GameAPP.canvas.transform)
		{
			Transform transform2 = (Transform)obj2;
			if (transform2 != null)
			{
				Object.Destroy(transform2.gameObject);
			}
		}
		Board.Instance.theCurrentSurvivalRound = 1;
		Object.Destroy(GameAPP.board);
		GameAPP.board = null;
		UIMgr.EnterGame(GameAPP.theBoardType, GameAPP.theBoardLevel, null);
	}

	// Token: 0x0400007E RID: 126
	public int buttonNumber;

	// Token: 0x0400007F RID: 127
	public GameObject thisMenu;

	// Token: 0x04000080 RID: 128
	private Vector3 originPosition;

	// Token: 0x04000081 RID: 129
	private RectTransform rectTransform;
}

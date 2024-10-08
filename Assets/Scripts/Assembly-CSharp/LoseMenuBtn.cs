using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class LoseMenuBtn : MonoBehaviour
{
	// Token: 0x06000061 RID: 97 RVA: 0x000044EE File Offset: 0x000026EE
	private void Start()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.originPosition = this.rectTransform.anchoredPosition;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00004512 File Offset: 0x00002712
	private void OnMouseEnter()
	{
		CursorChange.SetClickCursor();
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00004519 File Offset: 0x00002719
	private void OnMouseExit()
	{
		this.rectTransform.anchoredPosition = this.originPosition;
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00004536 File Offset: 0x00002736
	private void OnMouseDown()
	{
		GameAPP.PlaySoundNotPause(28, 0.5f);
		this.rectTransform.anchoredPosition = new Vector2(this.originPosition.x + 1f, this.originPosition.y - 1f);
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00004578 File Offset: 0x00002778
	private void OnMouseUp()
	{
		CursorChange.SetDefaultCursor();
		if (Board.Instance.isEndless)
		{
			Board.Instance.ClearTheBoard();
		}
		if (this.tryAgain)
		{
			Board.Instance.theCurrentSurvivalRound = 1;
			Object.Destroy(GameAPP.board);
			GameAPP.board = null;
			UIMgr.EnterGame(GameAPP.theBoardType, GameAPP.theBoardLevel, null);
			return;
		}
		Object.Destroy(GameAPP.board);
		GameAPP.board = null;
		UIMgr.EnterMainMenu();
	}

	// Token: 0x0400005A RID: 90
	public bool tryAgain;

	// Token: 0x0400005B RID: 91
	private Vector3 originPosition;

	// Token: 0x0400005C RID: 92
	private RectTransform rectTransform;
}

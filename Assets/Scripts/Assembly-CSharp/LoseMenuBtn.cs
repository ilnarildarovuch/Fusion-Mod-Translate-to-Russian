using UnityEngine;

public class LoseMenuBtn : MonoBehaviour
{
	public bool tryAgain;

	private Vector3 originPosition;

	private RectTransform rectTransform;

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		originPosition = rectTransform.anchoredPosition;
	}

	private void OnMouseEnter()
	{
		CursorChange.SetClickCursor();
	}

	private void OnMouseExit()
	{
		rectTransform.anchoredPosition = originPosition;
		CursorChange.SetDefaultCursor();
	}

	private void OnMouseDown()
	{
		GameAPP.PlaySoundNotPause(28);
		rectTransform.anchoredPosition = new Vector2(originPosition.x + 1f, originPosition.y - 1f);
	}

	private void OnMouseUp()
	{
		CursorChange.SetDefaultCursor();
		if (Board.Instance.isEndless)
		{
			Board.Instance.ClearTheBoard();
		}
		if (tryAgain)
		{
			Board.Instance.theCurrentSurvivalRound = 1;
			Object.Destroy(GameAPP.board);
			GameAPP.board = null;
			UIMgr.EnterGame(GameAPP.theBoardType, GameAPP.theBoardLevel);
		}
		else
		{
			Object.Destroy(GameAPP.board);
			GameAPP.board = null;
			UIMgr.EnterMainMenu();
		}
	}
}

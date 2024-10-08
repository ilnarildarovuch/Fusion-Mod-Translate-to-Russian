using UnityEngine;

public class PauseMenu_Btn : MonoBehaviour
{
	public int buttonNumber;

	public GameObject thisMenu;

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
		rectTransform.anchoredPosition = originPosition;
		switch (buttonNumber)
		{
		case 1:
			PauseMenuMgr.Instance.checkRestart.SetActive(value: true);
			PauseMenuMgr.Instance.btnQuit.GetComponent<Collider2D>().enabled = false;
			PauseMenuMgr.Instance.btnRestart.GetComponent<Collider2D>().enabled = false;
			break;
		case 2:
			PauseMenuMgr.Instance.checkQuit.SetActive(value: true);
			PauseMenuMgr.Instance.btnQuit.GetComponent<Collider2D>().enabled = false;
			PauseMenuMgr.Instance.btnRestart.GetComponent<Collider2D>().enabled = false;
			break;
		case 4:
			PauseMenuMgr.Instance.checkQuit.SetActive(value: false);
			PauseMenuMgr.Instance.checkRestart.SetActive(value: false);
			PauseMenuMgr.Instance.btnQuit.GetComponent<Collider2D>().enabled = true;
			PauseMenuMgr.Instance.btnRestart.GetComponent<Collider2D>().enabled = true;
			break;
		case 5:
			if (Board.Instance.isEndless)
			{
				Board.Instance.ClearTheBoard();
			}
			Restart();
			break;
		case 6:
			Object.Destroy(GameAPP.board);
			GameAPP.board = null;
			UIMgr.EnterMainMenu();
			break;
		case 10:
			UIMgr.BackToGame(thisMenu);
			break;
		case 3:
		case 7:
		case 8:
		case 9:
			break;
		}
	}

	private void Restart()
	{
		foreach (Transform item in GameAPP.canvasUp.transform)
		{
			if (item != null)
			{
				Object.Destroy(item.gameObject);
			}
		}
		foreach (Transform item2 in GameAPP.canvas.transform)
		{
			if (item2 != null)
			{
				Object.Destroy(item2.gameObject);
			}
		}
		Board.Instance.theCurrentSurvivalRound = 1;
		Object.Destroy(GameAPP.board);
		GameAPP.board = null;
		UIMgr.EnterGame(GameAPP.theBoardType, GameAPP.theBoardLevel);
	}
}

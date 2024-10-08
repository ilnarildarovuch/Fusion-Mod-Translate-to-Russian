using UnityEngine;
using UnityEngine.UI;

public class InGameBtn : MonoBehaviour
{
	public Sprite highLightSprite;

	public int buttonNumber;

	public GameObject thisMenu;

	private Sprite originSprite;

	private Image image;

	private Vector3 originPosition;

	private RectTransform rectTransform;

	private bool speedTrigger;

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		originPosition = rectTransform.anchoredPosition;
		image = GetComponent<Image>();
		originSprite = image.sprite;
	}

	private void OnMouseEnter()
	{
		image.sprite = highLightSprite;
		CursorChange.SetClickCursor();
	}

	private void OnMouseExit()
	{
		image.sprite = originSprite;
		if (buttonNumber != 3)
		{
			rectTransform.anchoredPosition = originPosition;
		}
		CursorChange.SetDefaultCursor();
	}

	private void OnMouseDown()
	{
		if (buttonNumber == 0 || buttonNumber == 3)
		{
			GameAPP.PlaySound(28);
		}
		else
		{
			GameAPP.PlaySound(19);
		}
		if (buttonNumber != 3)
		{
			rectTransform.anchoredPosition = new Vector2(originPosition.x + 1f, originPosition.y - 1f);
		}
	}

	private void OnMouseUp()
	{
		CursorChange.SetDefaultCursor();
		if (buttonNumber != 3)
		{
			rectTransform.anchoredPosition = originPosition;
		}
		switch (buttonNumber)
		{
		case 0:
			if (GameAPP.theGameStatus == 0 && GameAPP.board.GetComponent<Board>().isIZ)
			{
				base.transform.parent.GetComponent<IZEMgr>().PauseGame();
			}
			else if (!GameAPP.board.GetComponent<Board>().isIZ)
			{
				Object.Destroy(GameAPP.board);
				UIMgr.EnterMainMenu();
			}
			break;
		case 3:
			if (GameAPP.theGameStatus == 0)
			{
				SpeedTrigger();
			}
			break;
		}
	}

	private void Update()
	{
		if (GameAPP.theGameStatus == 0 && buttonNumber == 3 && Input.GetKeyDown(KeyCode.Alpha3))
		{
			SpeedTrigger();
		}
	}

	private void SpeedTrigger()
	{
		speedTrigger = !speedTrigger;
		if (speedTrigger)
		{
			Time.timeScale = 0.2f;
		}
		else
		{
			Time.timeScale = GameAPP.gameSpeed;
		}
	}
}

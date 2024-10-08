using UnityEngine;
using UnityEngine.UI;

public class Advanture_Btn : MonoBehaviour
{
	public Sprite highLightSprite;

	public int levelType;

	public int buttonNumber;

	public GameObject thisMenu;

	public ClgLevelMgr levelCtrl;

	private Sprite originSprite;

	private Image image;

	private Vector3 originPosition;

	private RectTransform rectTransform;

	private void Start()
	{
		if (base.name == "Window")
		{
			rectTransform = base.transform.parent.gameObject.GetComponent<RectTransform>();
		}
		else
		{
			rectTransform = GetComponent<RectTransform>();
		}
		originPosition = rectTransform.anchoredPosition;
		image = GetComponent<Image>();
		originSprite = image.sprite;
		if (levelType == 0 && buttonNumber > 0 && GameAPP.advLevelCompleted[buttonNumber])
		{
			base.transform.GetChild(1).gameObject.SetActive(value: true);
		}
		if (levelType == 1 && buttonNumber > 0 && GameAPP.clgLevelCompleted[buttonNumber])
		{
			base.transform.GetChild(1).gameObject.SetActive(value: true);
		}
		if (levelType == 2 && buttonNumber > 0 && GameAPP.gameLevelCompleted[buttonNumber])
		{
			base.transform.GetChild(1).gameObject.SetActive(value: true);
		}
		if (levelType == 3 && buttonNumber > 0 && GameAPP.survivalLevelCompleted[buttonNumber])
		{
			base.transform.GetChild(1).gameObject.SetActive(value: true);
		}
	}

	private void OnMouseEnter()
	{
		image.sprite = highLightSprite;
		CursorChange.SetClickCursor();
	}

	private void OnMouseExit()
	{
		image.sprite = originSprite;
		rectTransform.anchoredPosition = originPosition;
		CursorChange.SetDefaultCursor();
	}

	private void OnMouseDown()
	{
		GameAPP.PlaySound(29);
		rectTransform.anchoredPosition = new Vector2(originPosition.x + 1f, originPosition.y - 1f);
	}

	private void OnMouseUp()
	{
		rectTransform.anchoredPosition = originPosition;
		switch (buttonNumber)
		{
		case -1:
			CursorChange.SetDefaultCursor();
			UIMgr.EnterMainMenu();
			break;
		case -3:
			if (levelCtrl.currentPage > 0)
			{
				levelCtrl.ChangePage(levelCtrl.currentPage - 1);
			}
			break;
		case -2:
			if (levelCtrl.currentPage < 1)
			{
				levelCtrl.ChangePage(levelCtrl.currentPage + 1);
			}
			break;
		default:
			CursorChange.SetDefaultCursor();
			UIMgr.EnterGame(levelType, buttonNumber);
			break;
		}
	}
}

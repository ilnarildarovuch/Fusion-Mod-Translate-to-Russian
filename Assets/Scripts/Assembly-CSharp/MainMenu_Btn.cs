using UnityEngine;
using UnityEngine.UI;

public class MainMenu_Btn : MonoBehaviour
{
	public Sprite highLightSprite;

	public int buttonNumber;

	public GameObject thisMenu;

	private Sprite originSprite;

	private Image image;

	private Vector3 originPosition;

	private RectTransform rectTransform;

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		originPosition = rectTransform.anchoredPosition;
		image = GetComponent<Image>();
		originSprite = image.sprite;
	}

	private void OnMouseEnter()
	{
		if (GameAPP.theGameStatus == -1)
		{
			image.sprite = highLightSprite;
			GameAPP.PlaySound(27);
			CursorChange.SetClickCursor();
		}
	}

	private void OnMouseExit()
	{
		image.sprite = originSprite;
		rectTransform.anchoredPosition = originPosition;
		CursorChange.SetDefaultCursor();
	}

	private void OnMouseDown()
	{
		if (GameAPP.theGameStatus == -1)
		{
			int num = buttonNumber;
			if ((uint)num <= 3u)
			{
				GameAPP.PlaySound(28);
			}
			else
			{
				GameAPP.PlaySound(19);
			}
			rectTransform.anchoredPosition = new Vector2(originPosition.x + 1f, originPosition.y - 1f);
		}
	}

	private void OnMouseUp()
	{
		CursorChange.SetDefaultCursor();
		image.sprite = originSprite;
		rectTransform.anchoredPosition = originPosition;
		if (GameAPP.theGameStatus == -1)
		{
			rectTransform.anchoredPosition = originPosition;
			switch (buttonNumber)
			{
			case 0:
				UIMgr.EnterAdvantureMenu();
				Object.Destroy(thisMenu);
				break;
			case 1:
				UIMgr.EnterChallengeMenu();
				Object.Destroy(thisMenu);
				break;
			case 2:
				UIMgr.EnterIZEMenu();
				Object.Destroy(thisMenu);
				break;
			case 3:
				UIMgr.EnterSurvivalEMenu();
				Object.Destroy(thisMenu);
				break;
			case 4:
				GameAPP.theGameStatus = -2;
				UIMgr.EnterOtherMenu();
				break;
			case 5:
				UIMgr.EnterAlmanac(changeMusic: true);
				Object.Destroy(thisMenu);
				break;
			case 6:
				GameAPP.theGameStatus = -2;
				UIMgr.EnterOtherMenu();
				break;
			case 7:
				GameAPP.theGameStatus = -2;
				UIMgr.EnterOtherMenu();
				break;
			case 8:
				GameAPP.theGameStatus = -2;
				UIMgr.EnterPauseMenu(1);
				break;
			case 9:
				GameAPP.theGameStatus = -2;
				UIMgr.EnterHelpMenu();
				break;
			case 10:
				Application.Quit();
				break;
			}
		}
	}
}

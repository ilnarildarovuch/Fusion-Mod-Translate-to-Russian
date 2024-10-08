using UnityEngine;
using UnityEngine.UI;

public class StartGameBtn : MonoBehaviour
{
	public Sprite highLightSprite;

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
		base.transform.GetChild(2).gameObject.SetActive(value: true);
		CursorChange.SetClickCursor();
	}

	private void OnMouseExit()
	{
		rectTransform.anchoredPosition = originPosition;
		base.transform.GetChild(2).gameObject.SetActive(value: false);
		CursorChange.SetDefaultCursor();
	}

	private void OnMouseDown()
	{
		GameAPP.PlaySound(19);
		rectTransform.anchoredPosition = new Vector2(originPosition.x + 1f, originPosition.y - 1f);
	}

	private void OnMouseUp()
	{
		CursorChange.SetDefaultCursor();
		rectTransform.anchoredPosition = originPosition;
		GameAPP.board.GetComponent<InitBoard>().RemoveUI();
	}
}

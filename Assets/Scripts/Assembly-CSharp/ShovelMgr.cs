using UnityEngine;

public class ShovelMgr : MonoBehaviour
{
	public bool isPickUp;

	public GameObject defaultParent;

	protected Mouse m;

	private void Start()
	{
		m = GameAPP.board.GetComponent<Mouse>();
	}

	public void PickUp()
	{
		isPickUp = true;
		GetComponent<BoxCollider2D>().enabled = false;
		base.transform.SetParent(GameAPP.canvasUp.transform);
	}

	public void PutDown()
	{
		isPickUp = false;
		GetComponent<BoxCollider2D>().enabled = true;
		base.transform.SetParent(defaultParent.transform);
		GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
	}

	private void OnMouseEnter()
	{
		if (m.theItemOnMouse == null)
		{
			CursorChange.SetClickCursor();
		}
	}

	private void OnMouseExit()
	{
		CursorChange.SetDefaultCursor();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1) && GameAPP.theGameStatus == 0 && !isPickUp && m.theItemOnMouse == null)
		{
			m.theItemOnMouse = base.gameObject;
			GameAPP.PlaySound(21);
			PickUp();
		}
	}
}

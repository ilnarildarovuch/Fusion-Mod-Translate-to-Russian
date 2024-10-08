using UnityEngine;

public class GloveMgr : MonoBehaviour
{
	public bool isPickUp;

	public GameObject defaultParent;

	private float fullCD = 10f;

	public float CD;

	public bool avaliable = true;

	private RectTransform r;

	protected Mouse m;

	private void Start()
	{
		r = base.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
		m = GameAPP.board.GetComponent<Mouse>();
		if (GameAPP.theBoardType == 1)
		{
			int theBoardLevel = GameAPP.theBoardLevel;
			if (theBoardLevel == 3 || theBoardLevel == 15 || theBoardLevel == 17)
			{
				fullCD = 3f;
			}
		}
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
		CDUpdate();
		if (Input.GetKeyDown(KeyCode.Alpha2) && GameAPP.theGameStatus == 0 && !isPickUp && avaliable && m.theItemOnMouse == null)
		{
			m.theItemOnMouse = base.gameObject;
			GameAPP.PlaySound(19);
			PickUp();
		}
	}

	private void CDUpdate()
	{
		r.anchoredPosition = new Vector2(0f, CD * (10f / fullCD) * 7.5f);
		if (GameAPP.developerMode)
		{
			CD = fullCD;
		}
		if (CD < fullCD)
		{
			CD += Time.deltaTime;
			avaliable = false;
			if (CD > fullCD)
			{
				avaliable = true;
				CD = fullCD;
			}
		}
		if (CD >= fullCD)
		{
			avaliable = true;
		}
	}
}

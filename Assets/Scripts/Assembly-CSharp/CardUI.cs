using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
	public int theSeedType;

	public int theSeedCost = 100;

	public bool isSelected;

	public int theNumberInCardSort;

	public InGameUIMgr thisUI;

	public GameObject parent;

	public bool isAvailable = true;

	public float CD;

	public float fullCD = 7.5f;

	private Slider slider;

	public bool isPickUp;

	public bool isExtra;

	private void Start()
	{
		slider = base.transform.GetChild(2).gameObject.GetComponent<Slider>();
		if (GameAPP.theBoardType == 1)
		{
			switch (GameAPP.theBoardLevel)
			{
			case 15:
			case 17:
				if (theSeedType != 256)
				{
					Object.Destroy(base.gameObject);
				}
				else
				{
					theSeedCost = 75;
				}
				break;
			case 25:
			case 26:
				if (theSeedType == 9)
				{
					Object.Destroy(base.gameObject);
				}
				break;
			case 35:
				if (theSeedType == 1)
				{
					Object.Destroy(base.gameObject);
				}
				else if (theSeedType == 256)
				{
					Object.Destroy(base.gameObject);
				}
				break;
			}
		}
		if (!GameAPP.board.GetComponent<Board>().isNight)
		{
			int num = theSeedType;
			if ((uint)(num - 6) <= 5u)
			{
				theSeedCost += 75;
			}
		}
	}

	private void OnMouseEnter()
	{
		if (GameAPP.board.GetComponent<Mouse>().theItemOnMouse == null)
		{
			CursorChange.SetClickCursor();
		}
	}

	private void OnMouseExit()
	{
		CursorChange.SetDefaultCursor();
	}

	private void OnMouseDown()
	{
		CursorChange.SetDefaultCursor();
		if (GameAPP.theGameStatus == 3)
		{
			GameAPP.PlaySound(19);
			if (!isSelected)
			{
				isSelected = true;
				thisUI.AddCardToBank(base.gameObject);
			}
			else
			{
				isSelected = false;
				thisUI.RemoveCardFromBank(base.gameObject);
			}
		}
	}

	public void PickUp()
	{
		base.transform.GetChild(3).gameObject.SetActive(value: true);
		isPickUp = true;
	}

	public void PutDown()
	{
		base.transform.GetChild(3).gameObject.SetActive(value: false);
		isPickUp = false;
	}

	private void Update()
	{
		if (GameAPP.theGameStatus == 0)
		{
			if (CD < fullCD)
			{
				CD += Time.deltaTime;
				isAvailable = false;
			}
			else
			{
				CD = fullCD;
			}
			if (CD == fullCD && Board.Instance.theSun >= theSeedCost && !isPickUp)
			{
				isAvailable = true;
				base.transform.GetChild(3).gameObject.SetActive(value: false);
			}
			else
			{
				isAvailable = false;
				base.transform.GetChild(3).gameObject.SetActive(value: true);
			}
			CDUpdate();
		}
		if (Board.Instance.freeCD)
		{
			CD = fullCD;
			isAvailable = true;
		}
		base.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = theSeedCost.ToString();
	}

	private void CDUpdate()
	{
		slider.value = 1f - CD / fullCD;
	}
}

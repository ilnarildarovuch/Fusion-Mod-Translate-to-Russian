using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IZECard : MonoBehaviour
{
	public int theZombieType;

	public int theZombieCost = 100;

	private Slider slider;

	public bool isPickUp;

	private void Start()
	{
		if (theZombieType <= 20 && theZombieType >= 14 && GameAPP.theBoardLevel <= 12)
		{
			Object.Destroy(base.transform.parent.gameObject);
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

	public void PickUp()
	{
		base.transform.GetChild(2).gameObject.SetActive(value: true);
		isPickUp = true;
	}

	public void PutDown()
	{
		base.transform.GetChild(2).gameObject.SetActive(value: false);
		isPickUp = false;
	}

	private void Update()
	{
		if (GameAPP.board != null && GameAPP.board.GetComponent<Board>().theSun >= theZombieCost && !isPickUp)
		{
			base.transform.GetChild(2).gameObject.SetActive(value: false);
		}
		else
		{
			base.transform.GetChild(2).gameObject.SetActive(value: true);
		}
		base.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = theZombieCost.ToString();
	}
}

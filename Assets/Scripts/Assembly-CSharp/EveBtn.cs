using UnityEngine;

public class EveBtn : MonoBehaviour
{
	public int buttonNumber;

	private Board board;

	private Vector3 originPosition;

	private RectTransform rectTransform;

	public GameObject present;

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		originPosition = rectTransform.anchoredPosition;
		board = GameAPP.board.GetComponent<Board>();
		if (buttonNumber == 0 && (GameAPP.theBoardType != 2 || GameAPP.theBoardLevel != 1))
		{
			base.gameObject.SetActive(value: false);
		}
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
		case 0:
			OpenEveGame();
			break;
		case 1:
			StartEveGame();
			break;
		case 2:
			SaveThePlant();
			break;
		case 3:
			LoadPlants();
			break;
		case 4:
			SetPlant();
			break;
		case 5:
			AutoGame();
			break;
		}
	}

	public static void AutoGame()
	{
		GameAPP.board.GetComponent<Board>().isEveStart = true;
		GameAPP.board.GetComponent<Board>().isAutoEve = true;
		SaveThePlant();
	}

	public void OpenEveGame()
	{
		GameAPP.board.GetComponent<Board>().isEveStarted = true;
		base.transform.parent.GetChild(1).gameObject.SetActive(value: true);
		base.transform.parent.GetChild(2).gameObject.SetActive(value: true);
		base.transform.parent.GetChild(3).gameObject.SetActive(value: true);
		base.transform.parent.GetChild(4).gameObject.SetActive(value: true);
		base.transform.parent.GetChild(5).gameObject.SetActive(value: true);
		present.SetActive(value: true);
		GameObject.Find("InGameUIIZE").transform.GetChild(3).gameObject.SetActive(value: true);
	}

	private void StartEveGame()
	{
		board.isEveStart = !board.isEveStart;
	}

	public static void SaveThePlant()
	{
		GameAPP.plantEVE.Clear();
		GameObject[] plantArray = GameAPP.board.GetComponent<Board>().plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				GameAPP.EVEPlant eVEPlant = default(GameAPP.EVEPlant);
				eVEPlant.type = component.thePlantType;
				eVEPlant.row = component.thePlantRow;
				eVEPlant.column = component.thePlantColumn;
				GameAPP.EVEPlant item = eVEPlant;
				GameAPP.plantEVE.Add(item);
			}
		}
	}

	public static void LoadPlants()
	{
		GameObject[] plantArray = GameAPP.board.GetComponent<Board>().plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (gameObject != null)
			{
				gameObject.GetComponent<Plant>().Die();
			}
		}
		foreach (GameAPP.EVEPlant item in GameAPP.plantEVE)
		{
			GameObject gameObject2 = GameAPP.board.GetComponent<CreatePlant>().SetPlant(item.column, item.row, item.type);
			if (gameObject2 != null && gameObject2.TryGetComponent<PotatoMine>(out var component))
			{
				component.attributeCountdown = 0f;
			}
		}
	}

	public static void SetPlant()
	{
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				if (GameAPP.board.GetComponent<CreatePlant>().CheckBox(i, j, 0))
				{
					GameAPP.board.GetComponent<Board>().SetEvePlants(i, j);
				}
			}
		}
	}
}

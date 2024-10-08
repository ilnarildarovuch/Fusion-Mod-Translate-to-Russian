using UnityEngine;

public class Mouse : MonoBehaviour
{
	public static Mouse Instance;

	public Renderer r;

	public float theMouseX;

	public float theMouseY;

	public int theMouseRow;

	public int theMouseColumn;

	public float theBoxXofMouse;

	public float theBoxYofMouse;

	public int thePlantTypeOnMouse = -1;

	public int theZombieTypeOnMouse = -1;

	public GameObject plantShadow;

	public GameObject zombieShadow;

	public GameObject theItemOnMouse;

	public CardUI theCardOnMouse;

	public IZECard theIZECardOnMouse;

	public GameObject thePlantOnGlove;

	public float modifyX = 1f;

	public float modifyY = 1f;

	private bool existShadow;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		DrawItemOnMouse();
		PlantPreviewUpdate();
		if (GameAPP.theGameStatus == 0)
		{
			MouseClick();
		}
		modifyX = (float)Screen.width / 1920f;
		modifyY = (float)Screen.height / 600f;
		theMouseX = Input.mousePosition.x;
		theMouseY = (float)Screen.height - Input.mousePosition.y;
		theMouseColumn = GetColumnFromX(theMouseX);
		theMouseRow = GetRowFromY(theMouseY);
		theBoxXofMouse = GetBoxXFromColumn(theMouseColumn);
		theBoxYofMouse = GetBoxYFromRow(theMouseRow);
	}

	public int GetColumnFromX(float X)
	{
		X -= modifyX * 350f;
		if ((float)(int)X / (modifyX * 148f) < 0f)
		{
			return 0;
		}
		if ((float)(int)X / (modifyX * 148f) > 9f)
		{
			return 9;
		}
		return (int)(X / (modifyX * 148f));
	}

	public int GetRowFromY(float Y)
	{
		if (Board.Instance.roadNum == 5)
		{
			if (Y <= modifyY * 70f)
			{
				return 0;
			}
			if (Y >= modifyY * 500f)
			{
				return 4;
			}
			Y -= modifyY * 70f;
			return (int)((float)(int)Y / (modifyY * 100f));
		}
		if (Y <= modifyY * 70f)
		{
			return 0;
		}
		if (Y >= modifyY * 500f)
		{
			return 5;
		}
		Y -= modifyY * 70f;
		return (int)((float)(int)Y / (modifyY * 90f));
	}

	public float GetBoxXFromColumn(int theColumn)
	{
		return -4.8f + 1.35f * (float)theColumn;
	}

	public float GetBoxYFromRow(int theRow)
	{
		if (Board.Instance.roadNum == 5)
		{
			return 2.3f - 1.67f * (float)theRow;
		}
		return 2.3f - 1.45f * (float)theRow;
	}

	private void CreatePlantOnMouse(int theSeedType)
	{
		GameObject gameObject = GameAPP.prePlantPrefab[theSeedType];
		GameObject gameObject2 = Object.Instantiate(gameObject);
		gameObject2.name = gameObject.name;
		gameObject2.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.GetComponent<SpriteRenderer>().sortingLayerName = "up";
		gameObject2.GetComponent<SpriteRenderer>().sortingOrder = 30000;
		theItemOnMouse = gameObject2;
	}

	private void CreateZombieOnMouse(int theZombieType)
	{
		GameObject gameObject = ((theZombieType != -5) ? GameAPP.preZombiePrefab[theZombieType] : GameAPP.prePlantPrefab[256]);
		GameObject gameObject2 = Object.Instantiate(gameObject);
		gameObject2.name = gameObject.name;
		gameObject2.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.GetComponent<SpriteRenderer>().sortingLayerName = "up";
		gameObject2.GetComponent<SpriteRenderer>().sortingOrder = 30000;
		theItemOnMouse = gameObject2;
	}

	private void DrawItemOnMouse()
	{
		if (theItemOnMouse != null)
		{
			if (theItemOnMouse.name == "Shovel")
			{
				theItemOnMouse.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				theItemOnMouse.transform.position = new Vector3(theItemOnMouse.transform.position.x + 0.4f, theItemOnMouse.transform.position.y + 0.4f, 0f);
			}
			else
			{
				theItemOnMouse.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				theItemOnMouse.transform.position = new Vector3(theItemOnMouse.transform.position.x, theItemOnMouse.transform.position.y, -3f);
			}
		}
	}

	private void PlantPreviewUpdate()
	{
		if (theItemOnMouse != null)
		{
			if (theItemOnMouse.CompareTag("Preview"))
			{
				if (!existShadow)
				{
					existShadow = true;
					GameObject gameObject = (plantShadow = Object.Instantiate(theItemOnMouse));
					gameObject.transform.SetParent(GameAPP.board.transform);
					SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
					Color color = component.color;
					color.a = 0.5f;
					component.color = color;
					component.sortingLayerName = "Default";
				}
			}
			else if (plantShadow != null)
			{
				Object.Destroy(plantShadow);
				plantShadow = null;
				existShadow = false;
			}
		}
		else if (plantShadow != null)
		{
			Object.Destroy(plantShadow);
			plantShadow = null;
			existShadow = false;
		}
		if (!(plantShadow != null))
		{
			return;
		}
		if (GetComponent<Board>().isIZ && theZombieTypeOnMouse != -5)
		{
			if (theMouseY > 75f * modifyY && theMouseColumn > 4)
			{
				plantShadow.transform.position = new Vector3(theBoxXofMouse, theBoxYofMouse + 1f, 0f);
			}
			else if (theMouseY > 75f * modifyY && theMouseColumn <= 4)
			{
				plantShadow.transform.position = new Vector3(GetBoxXFromColumn(5), theBoxYofMouse + 1f, 0f);
			}
			else
			{
				plantShadow.transform.position = new Vector3(100f, 100f, 100f);
			}
		}
		else if (theMouseY > 75f * modifyY && GetComponent<CreatePlant>().CheckBox(theMouseColumn, theMouseRow, thePlantTypeOnMouse))
		{
			plantShadow.transform.position = new Vector3(theBoxXofMouse, theBoxYofMouse + 0.7f, 0f);
		}
		else
		{
			plantShadow.transform.position = new Vector3(100f, 100f, 100f);
		}
	}

	private void TryToSetPlantByCard()
	{
		if (theMouseY > 75f * modifyY)
		{
			int theColumn = GetComponent<Mouse>().theMouseColumn;
			int theRow = GetComponent<Mouse>().theMouseRow;
			int theSeedType = thePlantTypeOnMouse;
			if (GetComponent<CreatePlant>().SetPlant(theColumn, theRow, theSeedType) != null)
			{
				GameAPP.board.GetComponent<Board>().theSun -= theCardOnMouse.theSeedCost;
				theCardOnMouse.CD = 0f;
				theCardOnMouse.PutDown();
				Object.Destroy(theItemOnMouse);
				ClearItemOnMouse();
			}
		}
		else
		{
			PutDownItem();
		}
	}

	private void TryToSetZombieByCard()
	{
		if (theMouseY > 75f * modifyY)
		{
			int num = theMouseColumn;
			if (num < 5 && theZombieTypeOnMouse != -5)
			{
				num = 5;
			}
			float boxXFromColumn = GetBoxXFromColumn(num);
			int theRow = theMouseRow;
			int num2 = theZombieTypeOnMouse;
			GameObject gameObject = ((num2 != -5) ? CreateZombie.Instance.SetZombie(0, theRow, num2, boxXFromColumn) : CreatePlant.Instance.SetPlant(num, theRow, 256));
			if (gameObject != null)
			{
				if (Board.Instance.roadType[theMouseRow] == 1)
				{
					GameAPP.PlaySound(75);
				}
				else
				{
					GameAPP.PlaySound(Random.Range(22, 24));
				}
				GameAPP.board.GetComponent<Board>().theSun -= theIZECardOnMouse.theZombieCost;
				theIZECardOnMouse.PutDown();
				Object.Destroy(theItemOnMouse);
				ClearItemOnMouse();
			}
		}
		else
		{
			PutDownItem();
		}
	}

	private void TryToSetPlantByGlove()
	{
		if (theMouseY > 75f * modifyY)
		{
			int theColumn = GetComponent<Mouse>().theMouseColumn;
			int theRow = GetComponent<Mouse>().theMouseRow;
			int theSeedType = thePlantTypeOnMouse;
			if (GetComponent<CreatePlant>().SetPlant(theColumn, theRow, theSeedType, thePlantOnGlove) != null)
			{
				GameObject.Find("Glove").GetComponent<GloveMgr>().CD = 0f;
				Object.Destroy(theItemOnMouse);
				ClearItemOnMouse();
			}
		}
		else
		{
			PutDownItem();
		}
	}

	private void PutDownItem()
	{
		if (!(theItemOnMouse != null))
		{
			return;
		}
		ShovelMgr component;
		GloveMgr component2;
		if (theItemOnMouse.CompareTag("Preview"))
		{
			if (theCardOnMouse != null)
			{
				theCardOnMouse.PutDown();
			}
			if (theIZECardOnMouse != null)
			{
				theIZECardOnMouse.PutDown();
			}
			Object.Destroy(theItemOnMouse);
			ClearItemOnMouse();
		}
		else if (theItemOnMouse.TryGetComponent<ShovelMgr>(out component))
		{
			component.PutDown();
			ClearItemOnMouse();
		}
		else if (theItemOnMouse.TryGetComponent<GloveMgr>(out component2))
		{
			component2.PutDown();
			ClearItemOnMouse();
		}
		else
		{
			Object.Destroy(theItemOnMouse);
			ClearItemOnMouse();
		}
		GameAPP.PlaySound(19);
	}

	private void MouseClick()
	{
		if (Input.GetMouseButtonDown(0))
		{
			LeftEvent();
		}
		if (Input.GetMouseButtonDown(1))
		{
			PutDownItem();
		}
	}

	private void LeftEvent()
	{
		if (theItemOnMouse == null)
		{
			LeftClickWithNothing();
		}
		else
		{
			LeftClickWithSomeThing();
		}
	}

	private void LeftClickWithNothing()
	{
		RaycastHit2D raycastHit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (!(raycastHit2D.collider != null))
		{
			return;
		}
		GameObject gameObject = raycastHit2D.collider.gameObject;
		ShovelMgr component2;
		GloveMgr component3;
		CardUI component4;
		IZECard component5;
		DoomFume component6;
		if (gameObject.TryGetComponent<Bucket>(out var component))
		{
			component.Pick();
		}
		else if (gameObject.TryGetComponent<ShovelMgr>(out component2))
		{
			if (!component2.isPickUp)
			{
				component2.PickUp();
				theItemOnMouse = component2.gameObject;
				GameAPP.PlaySound(21);
			}
		}
		else if (gameObject.TryGetComponent<GloveMgr>(out component3))
		{
			if (!component3.isPickUp && component3.avaliable)
			{
				component3.PickUp();
				theItemOnMouse = component3.gameObject;
				GameAPP.PlaySound(19);
			}
		}
		else if (gameObject.TryGetComponent<CardUI>(out component4))
		{
			ClickOnCard(component4);
		}
		else if (gameObject.TryGetComponent<IZECard>(out component5))
		{
			ClickOnIZECard(component5);
		}
		else if (gameObject.TryGetComponent<DoomFume>(out component6))
		{
			component6.Shoot();
		}
	}

	private void LeftClickWithSomeThing()
	{
		Bucket component;
		if (theItemOnMouse.CompareTag("Preview"))
		{
			if (thePlantOnGlove == null)
			{
				if (!GetComponent<Board>().isIZ)
				{
					TryToSetPlantByCard();
				}
				else
				{
					TryToSetZombieByCard();
				}
			}
			else
			{
				TryToSetPlantByGlove();
			}
		}
		else if (theItemOnMouse.name == "Shovel")
		{
			TryToRemovePlant();
		}
		else if (theItemOnMouse.name == "Glove")
		{
			TryToPickPlant();
		}
		else if (theItemOnMouse.TryGetComponent<Bucket>(out component))
		{
			component.Use();
			ClearItemOnMouse();
		}
	}

	private void TryToRemovePlant()
	{
		GameObject obj = theItemOnMouse;
		ClearItemOnMouse();
		RaycastHit2D raycastHit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (raycastHit2D.collider != null && raycastHit2D.collider.TryGetComponent<Plant>(out var component))
		{
			component.Die();
			GameAPP.PlaySound(23);
		}
		obj.GetComponent<ShovelMgr>().PutDown();
	}

	private void TryToPickPlant()
	{
		GameObject obj = theItemOnMouse;
		ClearItemOnMouse();
		RaycastHit2D raycastHit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (raycastHit2D.collider != null && raycastHit2D.collider.TryGetComponent<Plant>(out var component) && component.thePlantType != 255)
		{
			thePlantOnGlove = component.gameObject;
			thePlantTypeOnMouse = component.thePlantType;
			CreatePlantOnMouse(thePlantTypeOnMouse);
			GameAPP.PlaySound(25);
		}
		obj.GetComponent<GloveMgr>().PutDown();
	}

	public void ClickOnCard(CardUI card)
	{
		if (GameAPP.board.GetComponent<Board>().theSun >= card.theSeedCost)
		{
			if (card.isAvailable)
			{
				card.PickUp();
				theCardOnMouse = card;
				thePlantTypeOnMouse = card.theSeedType;
				CreatePlantOnMouse(thePlantTypeOnMouse);
				GameAPP.PlaySound(25);
			}
			else
			{
				GameAPP.PlaySound(26);
			}
		}
		else
		{
			GameAPP.PlaySound(26);
		}
	}

	public void ClickOnIZECard(IZECard card)
	{
		if (GameAPP.board.GetComponent<Board>().theSun >= card.theZombieCost)
		{
			card.PickUp();
			theIZECardOnMouse = card;
			theZombieTypeOnMouse = card.theZombieType;
			CreateZombieOnMouse(theZombieTypeOnMouse);
			GameAPP.PlaySound(25);
		}
		else
		{
			GameAPP.PlaySound(26);
		}
	}

	public void ClearItemOnMouse(bool clearItem = false)
	{
		if (clearItem)
		{
			Object.Destroy(theItemOnMouse);
		}
		thePlantTypeOnMouse = -1;
		theZombieTypeOnMouse = -1;
		theIZECardOnMouse = null;
		theItemOnMouse = null;
		theCardOnMouse = null;
		thePlantOnGlove = null;
	}
}

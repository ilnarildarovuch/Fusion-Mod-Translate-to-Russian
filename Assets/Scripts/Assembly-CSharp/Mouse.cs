using System;
using UnityEngine;

// Token: 0x020000FC RID: 252
public class Mouse : MonoBehaviour
{
	// Token: 0x060004E1 RID: 1249 RVA: 0x0002A319 File Offset: 0x00028519
	private void Awake()
	{
		Mouse.Instance = this;
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x0002A324 File Offset: 0x00028524
	private void Update()
	{
		this.DrawItemOnMouse();
		this.PlantPreviewUpdate();
		if (GameAPP.theGameStatus == 0)
		{
			this.MouseClick();
		}
		this.modifyX = (float)Screen.width / 1920f;
		this.modifyY = (float)Screen.height / 600f;
		this.theMouseX = Input.mousePosition.x;
		this.theMouseY = (float)Screen.height - Input.mousePosition.y;
		this.theMouseColumn = this.GetColumnFromX(this.theMouseX);
		this.theMouseRow = this.GetRowFromY(this.theMouseY);
		this.theBoxXofMouse = this.GetBoxXFromColumn(this.theMouseColumn);
		this.theBoxYofMouse = this.GetBoxYFromRow(this.theMouseRow);
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x0002A3E0 File Offset: 0x000285E0
	public int GetColumnFromX(float X)
	{
		X -= this.modifyX * 350f;
		if ((float)((int)X) / (this.modifyX * 148f) < 0f)
		{
			return 0;
		}
		if ((float)((int)X) / (this.modifyX * 148f) > 9f)
		{
			return 9;
		}
		return (int)(X / (this.modifyX * 148f));
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x0002A440 File Offset: 0x00028640
	public int GetRowFromY(float Y)
	{
		if (Board.Instance.roadNum == 5)
		{
			if (Y <= this.modifyY * 70f)
			{
				return 0;
			}
			if (Y >= this.modifyY * 500f)
			{
				return 4;
			}
			Y -= this.modifyY * 70f;
			return (int)((float)((int)Y) / (this.modifyY * 100f));
		}
		else
		{
			if (Y <= this.modifyY * 70f)
			{
				return 0;
			}
			if (Y >= this.modifyY * 500f)
			{
				return 5;
			}
			Y -= this.modifyY * 70f;
			return (int)((float)((int)Y) / (this.modifyY * 90f));
		}
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x0002A4E1 File Offset: 0x000286E1
	public float GetBoxXFromColumn(int theColumn)
	{
		return -4.8f + 1.35f * (float)theColumn;
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x0002A4F4 File Offset: 0x000286F4
	public float GetBoxYFromRow(int theRow)
	{
		float result;
		if (Board.Instance.roadNum == 5)
		{
			result = 2.3f - 1.67f * (float)theRow;
		}
		else
		{
			result = 2.3f - 1.45f * (float)theRow;
		}
		return result;
	}

	// Token: 0x060004E7 RID: 1255 RVA: 0x0002A530 File Offset: 0x00028730
	private void CreatePlantOnMouse(int theSeedType)
	{
		GameObject gameObject = GameAPP.prePlantPrefab[theSeedType];
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject);
		gameObject2.name = gameObject.name;
		gameObject2.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.GetComponent<SpriteRenderer>().sortingLayerName = "up";
		gameObject2.GetComponent<SpriteRenderer>().sortingOrder = 30000;
		this.theItemOnMouse = gameObject2;
	}

	// Token: 0x060004E8 RID: 1256 RVA: 0x0002A5B0 File Offset: 0x000287B0
	private void CreateZombieOnMouse(int theZombieType)
	{
		GameObject gameObject;
		if (theZombieType == -5)
		{
			gameObject = GameAPP.prePlantPrefab[256];
		}
		else
		{
			gameObject = GameAPP.preZombiePrefab[theZombieType];
		}
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject);
		gameObject2.name = gameObject.name;
		gameObject2.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.GetComponent<SpriteRenderer>().sortingLayerName = "up";
		gameObject2.GetComponent<SpriteRenderer>().sortingOrder = 30000;
		this.theItemOnMouse = gameObject2;
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x0002A644 File Offset: 0x00028844
	private void DrawItemOnMouse()
	{
		if (this.theItemOnMouse != null)
		{
			if (this.theItemOnMouse.name == "Shovel")
			{
				this.theItemOnMouse.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				this.theItemOnMouse.transform.position = new Vector3(this.theItemOnMouse.transform.position.x + 0.4f, this.theItemOnMouse.transform.position.y + 0.4f, 0f);
				return;
			}
			this.theItemOnMouse.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			this.theItemOnMouse.transform.position = new Vector3(this.theItemOnMouse.transform.position.x, this.theItemOnMouse.transform.position.y, -3f);
		}
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x0002A74C File Offset: 0x0002894C
	private void PlantPreviewUpdate()
	{
		if (this.theItemOnMouse != null)
		{
			if (this.theItemOnMouse.CompareTag("Preview"))
			{
				if (!this.existShadow)
				{
					this.existShadow = true;
					GameObject gameObject = Object.Instantiate<GameObject>(this.theItemOnMouse);
					this.plantShadow = gameObject;
					gameObject.transform.SetParent(GameAPP.board.transform);
					SpriteRenderer component = gameObject.GetComponent<SpriteRenderer>();
					Color color = component.color;
					color.a = 0.5f;
					component.color = color;
					component.sortingLayerName = "Default";
				}
			}
			else if (this.plantShadow != null)
			{
				Object.Destroy(this.plantShadow);
				this.plantShadow = null;
				this.existShadow = false;
			}
		}
		else if (this.plantShadow != null)
		{
			Object.Destroy(this.plantShadow);
			this.plantShadow = null;
			this.existShadow = false;
		}
		if (this.plantShadow != null)
		{
			if (base.GetComponent<Board>().isIZ && this.theZombieTypeOnMouse != -5)
			{
				if (this.theMouseY > 75f * this.modifyY && this.theMouseColumn > 4)
				{
					this.plantShadow.transform.position = new Vector3(this.theBoxXofMouse, this.theBoxYofMouse + 1f, 0f);
					return;
				}
				if (this.theMouseY > 75f * this.modifyY && this.theMouseColumn <= 4)
				{
					this.plantShadow.transform.position = new Vector3(this.GetBoxXFromColumn(5), this.theBoxYofMouse + 1f, 0f);
					return;
				}
				this.plantShadow.transform.position = new Vector3(100f, 100f, 100f);
				return;
			}
			else
			{
				if (this.theMouseY > 75f * this.modifyY && base.GetComponent<CreatePlant>().CheckBox(this.theMouseColumn, this.theMouseRow, this.thePlantTypeOnMouse))
				{
					this.plantShadow.transform.position = new Vector3(this.theBoxXofMouse, this.theBoxYofMouse + 0.7f, 0f);
					return;
				}
				this.plantShadow.transform.position = new Vector3(100f, 100f, 100f);
			}
		}
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x0002A9A0 File Offset: 0x00028BA0
	private void TryToSetPlantByCard()
	{
		if (this.theMouseY > 75f * this.modifyY)
		{
			int theColumn = base.GetComponent<Mouse>().theMouseColumn;
			int theRow = base.GetComponent<Mouse>().theMouseRow;
			int theSeedType = this.thePlantTypeOnMouse;
			if (base.GetComponent<CreatePlant>().SetPlant(theColumn, theRow, theSeedType, null, default(Vector2), false, 0f) != null)
			{
				GameAPP.board.GetComponent<Board>().theSun -= this.theCardOnMouse.theSeedCost;
				this.theCardOnMouse.CD = 0f;
				this.theCardOnMouse.PutDown();
				Object.Destroy(this.theItemOnMouse);
				this.ClearItemOnMouse(false);
				return;
			}
		}
		else
		{
			this.PutDownItem();
		}
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x0002AA60 File Offset: 0x00028C60
	private void TryToSetZombieByCard()
	{
		if (this.theMouseY > 75f * this.modifyY)
		{
			int num = this.theMouseColumn;
			if (num < 5 && this.theZombieTypeOnMouse != -5)
			{
				num = 5;
			}
			float boxXFromColumn = this.GetBoxXFromColumn(num);
			int theRow = this.theMouseRow;
			int num2 = this.theZombieTypeOnMouse;
			GameObject x;
			if (num2 == -5)
			{
				x = CreatePlant.Instance.SetPlant(num, theRow, 256, null, default(Vector2), false, 0f);
			}
			else
			{
				x = CreateZombie.Instance.SetZombie(0, theRow, num2, boxXFromColumn, false);
			}
			if (x != null)
			{
				if (Board.Instance.roadType[this.theMouseRow] == 1)
				{
					GameAPP.PlaySound(75, 0.5f);
				}
				else
				{
					GameAPP.PlaySound(Random.Range(22, 24), 0.5f);
				}
				GameAPP.board.GetComponent<Board>().theSun -= this.theIZECardOnMouse.theZombieCost;
				this.theIZECardOnMouse.PutDown();
				Object.Destroy(this.theItemOnMouse);
				this.ClearItemOnMouse(false);
				return;
			}
		}
		else
		{
			this.PutDownItem();
		}
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x0002AB74 File Offset: 0x00028D74
	private void TryToSetPlantByGlove()
	{
		if (this.theMouseY > 75f * this.modifyY)
		{
			int theColumn = base.GetComponent<Mouse>().theMouseColumn;
			int theRow = base.GetComponent<Mouse>().theMouseRow;
			int theSeedType = this.thePlantTypeOnMouse;
			if (base.GetComponent<CreatePlant>().SetPlant(theColumn, theRow, theSeedType, this.thePlantOnGlove, default(Vector2), false, 0f) != null)
			{
				GameObject.Find("Glove").GetComponent<GloveMgr>().CD = 0f;
				Object.Destroy(this.theItemOnMouse);
				this.ClearItemOnMouse(false);
				return;
			}
		}
		else
		{
			this.PutDownItem();
		}
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x0002AC14 File Offset: 0x00028E14
	private void PutDownItem()
	{
		if (this.theItemOnMouse != null)
		{
			ShovelMgr shovelMgr;
			GloveMgr gloveMgr;
			if (this.theItemOnMouse.CompareTag("Preview"))
			{
				if (this.theCardOnMouse != null)
				{
					this.theCardOnMouse.PutDown();
				}
				if (this.theIZECardOnMouse != null)
				{
					this.theIZECardOnMouse.PutDown();
				}
				Object.Destroy(this.theItemOnMouse);
				this.ClearItemOnMouse(false);
			}
			else if (this.theItemOnMouse.TryGetComponent<ShovelMgr>(out shovelMgr))
			{
				shovelMgr.PutDown();
				this.ClearItemOnMouse(false);
			}
			else if (this.theItemOnMouse.TryGetComponent<GloveMgr>(out gloveMgr))
			{
				gloveMgr.PutDown();
				this.ClearItemOnMouse(false);
			}
			else
			{
				Object.Destroy(this.theItemOnMouse);
				this.ClearItemOnMouse(false);
			}
			GameAPP.PlaySound(19, 0.5f);
		}
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x0002ACE4 File Offset: 0x00028EE4
	private void MouseClick()
	{
		if (Input.GetMouseButtonDown(0))
		{
			this.LeftEvent();
		}
		if (Input.GetMouseButtonDown(1))
		{
			this.PutDownItem();
		}
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x0002AD02 File Offset: 0x00028F02
	private void LeftEvent()
	{
		if (this.theItemOnMouse == null)
		{
			this.LeftClickWithNothing();
			return;
		}
		this.LeftClickWithSomeThing();
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x0002AD20 File Offset: 0x00028F20
	private void LeftClickWithNothing()
	{
		RaycastHit2D raycastHit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (raycastHit2D.collider != null)
		{
			GameObject gameObject = raycastHit2D.collider.gameObject;
			Bucket bucket;
			if (gameObject.TryGetComponent<Bucket>(out bucket))
			{
				bucket.Pick();
				return;
			}
			ShovelMgr shovelMgr;
			if (gameObject.TryGetComponent<ShovelMgr>(out shovelMgr))
			{
				if (!shovelMgr.isPickUp)
				{
					shovelMgr.PickUp();
					this.theItemOnMouse = shovelMgr.gameObject;
					GameAPP.PlaySound(21, 0.5f);
				}
				return;
			}
			GloveMgr gloveMgr;
			if (gameObject.TryGetComponent<GloveMgr>(out gloveMgr))
			{
				if (!gloveMgr.isPickUp && gloveMgr.avaliable)
				{
					gloveMgr.PickUp();
					this.theItemOnMouse = gloveMgr.gameObject;
					GameAPP.PlaySound(19, 0.5f);
				}
				return;
			}
			CardUI card;
			if (gameObject.TryGetComponent<CardUI>(out card))
			{
				this.ClickOnCard(card);
				return;
			}
			IZECard card2;
			if (gameObject.TryGetComponent<IZECard>(out card2))
			{
				this.ClickOnIZECard(card2);
				return;
			}
			DoomFume doomFume;
			if (gameObject.TryGetComponent<DoomFume>(out doomFume))
			{
				doomFume.Shoot();
				return;
			}
		}
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x0002AE24 File Offset: 0x00029024
	private void LeftClickWithSomeThing()
	{
		if (this.theItemOnMouse.CompareTag("Preview"))
		{
			if (!(this.thePlantOnGlove == null))
			{
				this.TryToSetPlantByGlove();
				return;
			}
			if (!base.GetComponent<Board>().isIZ)
			{
				this.TryToSetPlantByCard();
				return;
			}
			this.TryToSetZombieByCard();
			return;
		}
		else
		{
			if (this.theItemOnMouse.name == "Shovel")
			{
				this.TryToRemovePlant();
				return;
			}
			if (this.theItemOnMouse.name == "Glove")
			{
				this.TryToPickPlant();
				return;
			}
			Bucket bucket;
			if (this.theItemOnMouse.TryGetComponent<Bucket>(out bucket))
			{
				bucket.Use();
				this.ClearItemOnMouse(false);
				return;
			}
			return;
		}
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x0002AECC File Offset: 0x000290CC
	private void TryToRemovePlant()
	{
		GameObject gameObject = this.theItemOnMouse;
		this.ClearItemOnMouse(false);
		RaycastHit2D raycastHit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		Plant plant;
		if (raycastHit2D.collider != null && raycastHit2D.collider.TryGetComponent<Plant>(out plant))
		{
			plant.Die();
			GameAPP.PlaySound(23, 0.5f);
		}
		gameObject.GetComponent<ShovelMgr>().PutDown();
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x0002AF40 File Offset: 0x00029140
	private void TryToPickPlant()
	{
		GameObject gameObject = this.theItemOnMouse;
		this.ClearItemOnMouse(false);
		RaycastHit2D raycastHit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		Plant plant;
		if (raycastHit2D.collider != null && raycastHit2D.collider.TryGetComponent<Plant>(out plant) && plant.thePlantType != 255)
		{
			this.thePlantOnGlove = plant.gameObject;
			this.thePlantTypeOnMouse = plant.thePlantType;
			this.CreatePlantOnMouse(this.thePlantTypeOnMouse);
			GameAPP.PlaySound(25, 0.5f);
		}
		gameObject.GetComponent<GloveMgr>().PutDown();
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x0002AFE0 File Offset: 0x000291E0
	public void ClickOnCard(CardUI card)
	{
		if (GameAPP.board.GetComponent<Board>().theSun < card.theSeedCost)
		{
			GameAPP.PlaySound(26, 0.5f);
			return;
		}
		if (card.isAvailable)
		{
			card.PickUp();
			this.theCardOnMouse = card;
			this.thePlantTypeOnMouse = card.theSeedType;
			this.CreatePlantOnMouse(this.thePlantTypeOnMouse);
			GameAPP.PlaySound(25, 0.5f);
			return;
		}
		GameAPP.PlaySound(26, 0.5f);
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x0002B058 File Offset: 0x00029258
	public void ClickOnIZECard(IZECard card)
	{
		if (GameAPP.board.GetComponent<Board>().theSun >= card.theZombieCost)
		{
			card.PickUp();
			this.theIZECardOnMouse = card;
			this.theZombieTypeOnMouse = card.theZombieType;
			this.CreateZombieOnMouse(this.theZombieTypeOnMouse);
			GameAPP.PlaySound(25, 0.5f);
			return;
		}
		GameAPP.PlaySound(26, 0.5f);
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x0002B0BA File Offset: 0x000292BA
	public void ClearItemOnMouse(bool clearItem = false)
	{
		if (clearItem)
		{
			Object.Destroy(this.theItemOnMouse);
		}
		this.thePlantTypeOnMouse = -1;
		this.theZombieTypeOnMouse = -1;
		this.theIZECardOnMouse = null;
		this.theItemOnMouse = null;
		this.theCardOnMouse = null;
		this.thePlantOnGlove = null;
	}

	// Token: 0x04000265 RID: 613
	public static Mouse Instance;

	// Token: 0x04000266 RID: 614
	public Renderer r;

	// Token: 0x04000267 RID: 615
	public float theMouseX;

	// Token: 0x04000268 RID: 616
	public float theMouseY;

	// Token: 0x04000269 RID: 617
	public int theMouseRow;

	// Token: 0x0400026A RID: 618
	public int theMouseColumn;

	// Token: 0x0400026B RID: 619
	public float theBoxXofMouse;

	// Token: 0x0400026C RID: 620
	public float theBoxYofMouse;

	// Token: 0x0400026D RID: 621
	public int thePlantTypeOnMouse = -1;

	// Token: 0x0400026E RID: 622
	public int theZombieTypeOnMouse = -1;

	// Token: 0x0400026F RID: 623
	public GameObject plantShadow;

	// Token: 0x04000270 RID: 624
	public GameObject zombieShadow;

	// Token: 0x04000271 RID: 625
	public GameObject theItemOnMouse;

	// Token: 0x04000272 RID: 626
	public CardUI theCardOnMouse;

	// Token: 0x04000273 RID: 627
	public IZECard theIZECardOnMouse;

	// Token: 0x04000274 RID: 628
	public GameObject thePlantOnGlove;

	// Token: 0x04000275 RID: 629
	public float modifyX = 1f;

	// Token: 0x04000276 RID: 630
	public float modifyY = 1f;

	// Token: 0x04000277 RID: 631
	private bool existShadow;
}

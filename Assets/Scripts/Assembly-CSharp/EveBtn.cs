using System;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class EveBtn : MonoBehaviour
{
	// Token: 0x06000048 RID: 72 RVA: 0x00003CFC File Offset: 0x00001EFC
	private void Start()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.originPosition = this.rectTransform.anchoredPosition;
		this.board = GameAPP.board.GetComponent<Board>();
		if (this.buttonNumber == 0 && (GameAPP.theBoardType != 2 || GameAPP.theBoardLevel != 1))
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00003D5F File Offset: 0x00001F5F
	private void OnMouseEnter()
	{
		CursorChange.SetClickCursor();
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00003D66 File Offset: 0x00001F66
	private void OnMouseExit()
	{
		this.rectTransform.anchoredPosition = this.originPosition;
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00003D83 File Offset: 0x00001F83
	private void OnMouseDown()
	{
		GameAPP.PlaySoundNotPause(28, 0.5f);
		this.rectTransform.anchoredPosition = new Vector2(this.originPosition.x + 1f, this.originPosition.y - 1f);
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00003DC4 File Offset: 0x00001FC4
	private void OnMouseUp()
	{
		CursorChange.SetDefaultCursor();
		this.rectTransform.anchoredPosition = this.originPosition;
		switch (this.buttonNumber)
		{
		case 0:
			this.OpenEveGame();
			return;
		case 1:
			this.StartEveGame();
			return;
		case 2:
			EveBtn.SaveThePlant();
			return;
		case 3:
			EveBtn.LoadPlants();
			return;
		case 4:
			EveBtn.SetPlant();
			return;
		case 5:
			EveBtn.AutoGame();
			return;
		default:
			return;
		}
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00003E37 File Offset: 0x00002037
	public static void AutoGame()
	{
		GameAPP.board.GetComponent<Board>().isEveStart = true;
		GameAPP.board.GetComponent<Board>().isAutoEve = true;
		EveBtn.SaveThePlant();
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00003E60 File Offset: 0x00002060
	public void OpenEveGame()
	{
		GameAPP.board.GetComponent<Board>().isEveStarted = true;
		base.transform.parent.GetChild(1).gameObject.SetActive(true);
		base.transform.parent.GetChild(2).gameObject.SetActive(true);
		base.transform.parent.GetChild(3).gameObject.SetActive(true);
		base.transform.parent.GetChild(4).gameObject.SetActive(true);
		base.transform.parent.GetChild(5).gameObject.SetActive(true);
		this.present.SetActive(true);
		GameObject.Find("InGameUIIZE").transform.GetChild(3).gameObject.SetActive(true);
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00003F35 File Offset: 0x00002135
	private void StartEveGame()
	{
		this.board.isEveStart = !this.board.isEveStart;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00003F50 File Offset: 0x00002150
	public static void SaveThePlant()
	{
		GameAPP.plantEVE.Clear();
		foreach (GameObject gameObject in GameAPP.board.GetComponent<Board>().plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				GameAPP.EVEPlant item = new GameAPP.EVEPlant
				{
					type = component.thePlantType,
					row = component.thePlantRow,
					column = component.thePlantColumn
				};
				GameAPP.plantEVE.Add(item);
			}
		}
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00003FD8 File Offset: 0x000021D8
	public static void LoadPlants()
	{
		foreach (GameObject gameObject in GameAPP.board.GetComponent<Board>().plantArray)
		{
			if (gameObject != null)
			{
				gameObject.GetComponent<Plant>().Die();
			}
		}
		foreach (GameAPP.EVEPlant eveplant in GameAPP.plantEVE)
		{
			GameObject gameObject2 = GameAPP.board.GetComponent<CreatePlant>().SetPlant(eveplant.column, eveplant.row, eveplant.type, null, default(Vector2), false, 0f);
			PotatoMine potatoMine;
			if (gameObject2 != null && gameObject2.TryGetComponent<PotatoMine>(out potatoMine))
			{
				potatoMine.attributeCountdown = 0f;
			}
		}
	}

	// Token: 0x06000052 RID: 82 RVA: 0x000040B4 File Offset: 0x000022B4
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

	// Token: 0x04000050 RID: 80
	public int buttonNumber;

	// Token: 0x04000051 RID: 81
	private Board board;

	// Token: 0x04000052 RID: 82
	private Vector3 originPosition;

	// Token: 0x04000053 RID: 83
	private RectTransform rectTransform;

	// Token: 0x04000054 RID: 84
	public GameObject present;
}

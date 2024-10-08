using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

// Token: 0x02000060 RID: 96
public class InitBoard : MonoBehaviour
{
	// Token: 0x060001CE RID: 462 RVA: 0x0000F188 File Offset: 0x0000D388
	private void Awake()
	{
		InitBoard.Instance = this;
		this.board = Board.Instance;
		this.InitSelectUI();
		this.board.theMaxWave = InitZombieList.theMaxWave;
		this.UniqueBoardSettings(this.board);
		this.StartInit();
	}

	// Token: 0x060001CF RID: 463 RVA: 0x0000F1C3 File Offset: 0x0000D3C3
	public void StartInit()
	{
		this.InitZombieFromList();
		Camera.main.transform.position = new Vector3(-1f, 0f, -200f);
		base.Invoke("StartMoveRight", 1f);
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x0000F200 File Offset: 0x0000D400
	private void StartMoveRight()
	{
		InGameUIMgr.Instance.Bottom.gameObject.SetActive(true);
		Vector3 endPos = new Vector3(5f, Camera.main.transform.position.y, Camera.main.transform.position.z);
		float speed = 5f;
		base.StartCoroutine(this.MoveObject(endPos, speed, "right", Camera.main.gameObject));
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0000F27C File Offset: 0x0000D47C
	private void StartMoveLeft()
	{
		InGameUIMgr.Instance.Bottom.gameObject.SetActive(false);
		Vector3 endPos = new Vector3(0f, Camera.main.transform.position.y, Camera.main.transform.position.z);
		float speed = 5f;
		base.StartCoroutine(this.MoveObject(endPos, speed, "left", Camera.main.gameObject));
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0000F2F6 File Offset: 0x0000D4F6
	private IEnumerator MoveObject(Vector3 endPos, float speed, string direction, GameObject obj)
	{
		Vector3 startPos = obj.transform.position;
		float moveTime = Vector3.Distance(startPos, endPos) / speed;
		float elapsedTime = 0f;
		GameObject levelText = InGameUIMgr.Instance.LevelName1;
		Color col = Color.black;
		Color col2 = Color.white;
		while (elapsedTime < moveTime)
		{
			obj.transform.position = Vector3.Lerp(startPos, endPos, this.EaseInOut(elapsedTime / moveTime));
			if (direction == "right")
			{
				if (col.a > 0f)
				{
					col.a -= Time.deltaTime;
					col2.a -= Time.deltaTime;
				}
				else
				{
					col.a = 0f;
					col2.a = 0f;
				}
				levelText.GetComponent<TextMeshProUGUI>().color = col;
				levelText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = col2;
			}
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		Camera.main.transform.position = endPos;
		this.MoveOverEvent(direction);
		yield break;
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x0000F324 File Offset: 0x0000D524
	private void MoveOverEvent(string direction)
	{
		if (!(direction == "right"))
		{
			if (direction == "left")
			{
				base.StartCoroutine(this.DecreaseVolume());
				this.theInGameUI.transform.SetParent(GameAPP.canvas.transform);
				if (!this.board.isEndless && !this.board.isTowerDefense && this.board.theCurrentSurvivalRound <= 1)
				{
					base.GetComponent<CreateMower>().SetMower(GameAPP.board.GetComponent<Board>().roadType);
					for (int i = 0; i < GameAPP.board.GetComponent<Board>().mowerArray.Length; i++)
					{
						if (GameAPP.board.GetComponent<Board>().mowerArray[i] != null)
						{
							base.StartCoroutine(this.MoveMowers(GameAPP.board.GetComponent<Board>().mowerArray[i]));
						}
					}
				}
				base.Invoke("ReadySetPlant", 0.5f);
			}
			return;
		}
		if (this.CheckIfOptionalCard())
		{
			GameAPP.theGameStatus = 3;
			this.ShowUI();
			return;
		}
		base.Invoke("StartMoveLeft", 1f);
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x0000F443 File Offset: 0x0000D643
	private IEnumerator DecreaseVolume()
	{
		while (GameAPP.gameAPP.GetComponent<AudioSource>().volume > 0f)
		{
			GameAPP.gameAPP.GetComponent<AudioSource>().volume -= Time.deltaTime;
			yield return null;
		}
		GameAPP.gameAPP.GetComponent<AudioSource>().volume -= 0f;
		yield break;
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x0000F44B File Offset: 0x0000D64B
	private void ReadySetPlant()
	{
		GameAPP.PlaySound(31, 0.5f);
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("Board/RSP/StartPlantPrefab")).transform.SetParent(base.transform);
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x0000F478 File Offset: 0x0000D678
	private IEnumerator MoveMowers(GameObject mower)
	{
		while (mower.transform.localPosition.x < 4f)
		{
			Vector3 b = new Vector3(Time.deltaTime * 3f, 0f, 0f);
			mower.transform.localPosition += b;
			yield return null;
		}
		mower.transform.localPosition = new Vector3(4f, mower.transform.localPosition.y);
		yield break;
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x0000F487 File Offset: 0x0000D687
	private float EaseInOut(float t)
	{
		if (t >= 0.5f)
		{
			return 1f - 2f * (1f - t) * (1f - t);
		}
		return 2f * t * t;
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x0000F4B6 File Offset: 0x0000D6B6
	private bool CheckIfOptionalCard()
	{
		return true;
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x0000F4BC File Offset: 0x0000D6BC
	public void InitSelectUI()
	{
		GameObject gameObject = Resources.Load<GameObject>("UI/InGameMenu/InGameUIFHD");
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, GameAPP.canvasUp.transform);
		gameObject2.name = gameObject.name;
		this.theInGameUI = gameObject2;
		this.board.theInGameUI = gameObject2;
	}

	// Token: 0x060001DA RID: 474 RVA: 0x0000F504 File Offset: 0x0000D704
	private IEnumerator MoveDirection(GameObject obj, float distance, int direction)
	{
		float duration = 0.2f;
		Vector3 endPosition = new Vector3(0f, 0f, 0f);
		Vector3 startPosition = obj.GetComponent<RectTransform>().anchoredPosition;
		if (direction == 0)
		{
			endPosition = obj.GetComponent<RectTransform>().anchoredPosition - Vector2.up * distance;
		}
		else if (direction == 1)
		{
			endPosition = obj.GetComponent<RectTransform>().anchoredPosition + Vector2.up * distance;
		}
		float elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			obj.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(startPosition, endPosition, elapsedTime / duration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		obj.GetComponent<RectTransform>().anchoredPosition = endPosition;
		yield break;
	}

	// Token: 0x060001DB RID: 475 RVA: 0x0000F524 File Offset: 0x0000D724
	private void ShowUI()
	{
		InGameUIMgr.Instance.LevelName1.SetActive(false);
		InGameUIMgr.Instance.BackToMenu.SetActive(true);
		base.StartCoroutine(this.MoveDirection(InGameUIMgr.Instance.SeedBank, 79f, 0));
		base.StartCoroutine(this.MoveDirection(InGameUIMgr.Instance.Bottom, 525f, 1));
	}

	// Token: 0x060001DC RID: 476 RVA: 0x0000F58C File Offset: 0x0000D78C
	public void RemoveUI()
	{
		GameAPP.theGameStatus = 2;
		base.StartCoroutine(this.MoveDirection(InGameUIMgr.Instance.Bottom, 525f, 0));
		base.Invoke("StartMoveLeft", 0.5f);
		for (int i = 0; i < this.theInGameUI.GetComponent<InGameUIMgr>().seed.Length; i++)
		{
			if (this.theInGameUI.GetComponent<InGameUIMgr>().seed[i] != null && this.theInGameUI.GetComponent<InGameUIMgr>().seed[i].transform.childCount != 0)
			{
				this.theInGameUI.GetComponent<InGameUIMgr>().seed[i].transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
			}
		}
		this.theInGameUI.transform.GetChild(8).gameObject.SetActive(false);
	}

	// Token: 0x060001DD RID: 477 RVA: 0x0000F674 File Offset: 0x0000D874
	private void InitZombieFromList()
	{
		int num = 0;
		for (int i = 0; i < InitZombieList.zombieTypeList.Length; i++)
		{
			if (InitZombieList.zombieTypeList[i] != -1)
			{
				num++;
			}
		}
		Vector2[] array = RandomVectorGenerator.GenerateRandomVectors(num, 9.5f, 12.5f, -5f, 1f, 1.2f);
		Queue<GameObject> queue = new Queue<GameObject>();
		for (int j = 0; j < InitZombieList.zombieTypeList.Length; j++)
		{
			if (InitZombieList.zombieTypeList[j] != -1)
			{
				GameObject gameObject = CreateZombie.Instance.SetZombie(0, 0, InitZombieList.zombieTypeList[j], 0f, true);
				gameObject.GetComponent<Collider2D>().enabled = false;
				if (InitZombieList.zombieTypeList[j] == 14)
				{
					gameObject.transform.position = new Vector3(10f, -1.5f);
				}
				else
				{
					gameObject.transform.position = array[j];
				}
				queue.Enqueue(gameObject);
			}
		}
		queue = new Queue<GameObject>(from z in queue
		orderby z.transform.Find("Shadow").position.y descending
		select z);
		int num2 = 0;
		while (queue.Count > 0)
		{
			num2 += 40;
			GameObject obj = queue.Dequeue();
			this.ResetLayer(obj, num2);
		}
	}

	// Token: 0x060001DE RID: 478 RVA: 0x0000F7B8 File Offset: 0x0000D9B8
	private void ResetLayer(GameObject obj, int baseLayer)
	{
		if (obj.name == "Shadow")
		{
			return;
		}
		SpriteRenderer spriteRenderer;
		if (obj.TryGetComponent<SpriteRenderer>(out spriteRenderer))
		{
			spriteRenderer.sortingOrder += baseLayer;
		}
		if (obj.transform.childCount != 0)
		{
			foreach (object obj2 in obj.transform)
			{
				Transform transform = (Transform)obj2;
				this.ResetLayer(transform.gameObject, baseLayer);
			}
		}
	}

	// Token: 0x060001DF RID: 479 RVA: 0x0000F850 File Offset: 0x0000DA50
	private void UniqueBoardSettings(Board board)
	{
		if (GameAPP.theBoardType == 1)
		{
			int theBoardLevel = GameAPP.theBoardLevel;
			if (theBoardLevel == 3 || theBoardLevel == 15 || theBoardLevel == 17)
			{
				board.theSun = 1000;
			}
		}
	}

	// Token: 0x04000137 RID: 311
	public GameObject theInGameUI;

	// Token: 0x04000138 RID: 312
	private Board board;

	// Token: 0x04000139 RID: 313
	public static InitBoard Instance;
}

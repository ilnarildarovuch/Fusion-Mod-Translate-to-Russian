using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InitBoard : MonoBehaviour
{
	public GameObject theInGameUI;

	private Board board;

	public static InitBoard Instance;

	private void Awake()
	{
		Instance = this;
		board = Board.Instance;
		InitSelectUI();
		board.theMaxWave = InitZombieList.theMaxWave;
		UniqueBoardSettings(board);
		StartInit();
	}

	public void StartInit()
	{
		InitZombieFromList();
		Camera.main.transform.position = new Vector3(-1f, 0f, -200f);
		Invoke("StartMoveRight", 1f);
	}

	private void StartMoveRight()
	{
		InGameUIMgr.Instance.Bottom.gameObject.SetActive(value: true);
		Vector3 endPos = new Vector3(5f, Camera.main.transform.position.y, Camera.main.transform.position.z);
		float speed = 5f;
		StartCoroutine(MoveObject(endPos, speed, "right", Camera.main.gameObject));
	}

	private void StartMoveLeft()
	{
		InGameUIMgr.Instance.Bottom.gameObject.SetActive(value: false);
		Vector3 endPos = new Vector3(0f, Camera.main.transform.position.y, Camera.main.transform.position.z);
		float speed = 5f;
		StartCoroutine(MoveObject(endPos, speed, "left", Camera.main.gameObject));
	}

	private IEnumerator MoveObject(Vector3 endPos, float speed, string direction, GameObject obj)
	{
		Vector3 startPos = obj.transform.position;
		float moveTime = Vector3.Distance(startPos, endPos) / speed;
		float elapsedTime = 0f;
		GameObject levelText = InGameUIMgr.Instance.LevelName1;
		Color col1 = Color.black;
		Color col2 = Color.white;
		while (elapsedTime < moveTime)
		{
			obj.transform.position = Vector3.Lerp(startPos, endPos, EaseInOut(elapsedTime / moveTime));
			if (direction == "right")
			{
				if (col1.a > 0f)
				{
					col1.a -= Time.deltaTime;
					col2.a -= Time.deltaTime;
				}
				else
				{
					col1.a = 0f;
					col2.a = 0f;
				}
				levelText.GetComponent<TextMeshProUGUI>().color = col1;
				levelText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = col2;
			}
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		Camera.main.transform.position = endPos;
		MoveOverEvent(direction);
	}

	private void MoveOverEvent(string direction)
	{
		if (direction == "right")
		{
			if (CheckIfOptionalCard())
			{
				GameAPP.theGameStatus = 3;
				ShowUI();
			}
			else
			{
				Invoke("StartMoveLeft", 1f);
			}
		}
		else
		{
			if (!(direction == "left"))
			{
				return;
			}
			StartCoroutine(DecreaseVolume());
			theInGameUI.transform.SetParent(GameAPP.canvas.transform);
			if (!board.isEndless && !board.isTowerDefense && board.theCurrentSurvivalRound <= 1)
			{
				GetComponent<CreateMower>().SetMower(GameAPP.board.GetComponent<Board>().roadType);
				for (int i = 0; i < GameAPP.board.GetComponent<Board>().mowerArray.Length; i++)
				{
					if (GameAPP.board.GetComponent<Board>().mowerArray[i] != null)
					{
						StartCoroutine(MoveMowers(GameAPP.board.GetComponent<Board>().mowerArray[i]));
					}
				}
			}
			Invoke("ReadySetPlant", 0.5f);
		}
	}

	private IEnumerator DecreaseVolume()
	{
		while (GameAPP.gameAPP.GetComponent<AudioSource>().volume > 0f)
		{
			GameAPP.gameAPP.GetComponent<AudioSource>().volume -= Time.deltaTime;
			yield return null;
		}
		GameAPP.gameAPP.GetComponent<AudioSource>().volume -= 0f;
	}

	private void ReadySetPlant()
	{
		GameAPP.PlaySound(31);
		Object.Instantiate(Resources.Load<GameObject>("Board/RSP/StartPlantPrefab")).transform.SetParent(base.transform);
	}

	private IEnumerator MoveMowers(GameObject mower)
	{
		while (mower.transform.localPosition.x < 4f)
		{
			Vector3 vector = new Vector3(Time.deltaTime * 3f, 0f, 0f);
			mower.transform.localPosition += vector;
			yield return null;
		}
		mower.transform.localPosition = new Vector3(4f, mower.transform.localPosition.y);
	}

	private float EaseInOut(float t)
	{
		if (!(t < 0.5f))
		{
			return 1f - 2f * (1f - t) * (1f - t);
		}
		return 2f * t * t;
	}

	private bool CheckIfOptionalCard()
	{
		return true;
	}

	public void InitSelectUI()
	{
		GameObject gameObject = Resources.Load<GameObject>("UI/InGameMenu/InGameUIFHD");
		GameObject gameObject2 = Object.Instantiate(gameObject, GameAPP.canvasUp.transform);
		gameObject2.name = gameObject.name;
		theInGameUI = gameObject2;
		board.theInGameUI = gameObject2;
	}

	private IEnumerator MoveDirection(GameObject obj, float distance, int direction)
	{
		float duration = 0.2f;
		Vector3 endPosition = new Vector3(0f, 0f, 0f);
		Vector3 startPosition = obj.GetComponent<RectTransform>().anchoredPosition;
		switch (direction)
		{
		case 0:
			endPosition = obj.GetComponent<RectTransform>().anchoredPosition - Vector2.up * distance;
			break;
		case 1:
			endPosition = obj.GetComponent<RectTransform>().anchoredPosition + Vector2.up * distance;
			break;
		}
		float elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			obj.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(startPosition, endPosition, elapsedTime / duration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		obj.GetComponent<RectTransform>().anchoredPosition = endPosition;
	}

	private void ShowUI()
	{
		InGameUIMgr.Instance.LevelName1.SetActive(value: false);
		InGameUIMgr.Instance.BackToMenu.SetActive(value: true);
		StartCoroutine(MoveDirection(InGameUIMgr.Instance.SeedBank, 79f, 0));
		StartCoroutine(MoveDirection(InGameUIMgr.Instance.Bottom, 525f, 1));
	}

	public void RemoveUI()
	{
		GameAPP.theGameStatus = 2;
		StartCoroutine(MoveDirection(InGameUIMgr.Instance.Bottom, 525f, 0));
		Invoke("StartMoveLeft", 0.5f);
		for (int i = 0; i < theInGameUI.GetComponent<InGameUIMgr>().seed.Length; i++)
		{
			if (theInGameUI.GetComponent<InGameUIMgr>().seed[i] != null && theInGameUI.GetComponent<InGameUIMgr>().seed[i].transform.childCount != 0)
			{
				theInGameUI.GetComponent<InGameUIMgr>().seed[i].transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(value: true);
			}
		}
		theInGameUI.transform.GetChild(8).gameObject.SetActive(value: false);
	}

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
		Vector2[] array = RandomVectorGenerator.GenerateRandomVectors(num, 9.5f, 12.5f, -5f, 1f);
		Queue<GameObject> queue = new Queue<GameObject>();
		for (int j = 0; j < InitZombieList.zombieTypeList.Length; j++)
		{
			if (InitZombieList.zombieTypeList[j] != -1)
			{
				GameObject gameObject = CreateZombie.Instance.SetZombie(0, 0, InitZombieList.zombieTypeList[j], 0f, isIdle: true);
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
		queue = new Queue<GameObject>(queue.OrderByDescending((GameObject z) => z.transform.Find("Shadow").position.y));
		int num2 = 0;
		while (queue.Count > 0)
		{
			num2 += 40;
			GameObject obj = queue.Dequeue();
			ResetLayer(obj, num2);
		}
	}

	private void ResetLayer(GameObject obj, int baseLayer)
	{
		if (obj.name == "Shadow")
		{
			return;
		}
		if (obj.TryGetComponent<SpriteRenderer>(out var component))
		{
			component.sortingOrder += baseLayer;
		}
		if (obj.transform.childCount == 0)
		{
			return;
		}
		foreach (Transform item in obj.transform)
		{
			ResetLayer(item.gameObject, baseLayer);
		}
	}

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
}

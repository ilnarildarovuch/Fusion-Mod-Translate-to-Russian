using System.Collections;
using TMPro;
using UnityEngine;

public class InGameUIMgr : MonoBehaviour
{
	public static InGameUIMgr Instance;

	public GameObject[] cardOnBank = new GameObject[16];

	public GameObject[] seed = new GameObject[16];

	public TextMeshProUGUI sun;

	public GameObject ShovelBank;

	public GameObject Bottom;

	public GameObject SeedBank;

	public GameObject LevProgress;

	public GameObject LevelName1;

	public GameObject LevelName2;

	public GameObject LevelName3;

	public GameObject GloveBank;

	public GameObject BackToMenu;

	public GameObject SlowTrigger;

	public GameObject Difficulty;

	private void Awake()
	{
		Instance = this;
		ShovelBank = base.transform.GetChild(0).gameObject;
		Bottom = base.transform.GetChild(1).gameObject;
		SeedBank = base.transform.GetChild(2).gameObject;
		LevProgress = base.transform.GetChild(3).gameObject;
		LevelName1 = base.transform.GetChild(4).gameObject;
		LevelName2 = base.transform.GetChild(5).gameObject;
		LevelName3 = base.transform.GetChild(6).gameObject;
		GloveBank = base.transform.GetChild(7).gameObject;
		BackToMenu = base.transform.GetChild(8).gameObject;
		SlowTrigger = base.transform.GetChild(9).gameObject;
		Difficulty = base.transform.GetChild(10).gameObject;
	}

	private void Start()
	{
		string text = $"冒险模式：第{GameAPP.theBoardLevel}关";
		TextMeshProUGUI[] array = new TextMeshProUGUI[6]
		{
			base.transform.GetChild(4).GetComponent<TextMeshProUGUI>(),
			base.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>(),
			base.transform.GetChild(5).GetComponent<TextMeshProUGUI>(),
			base.transform.GetChild(5).GetChild(0).GetComponent<TextMeshProUGUI>(),
			base.transform.GetChild(6).GetComponent<TextMeshProUGUI>(),
			base.transform.GetChild(6).GetChild(0).GetComponent<TextMeshProUGUI>()
		};
		TextMeshProUGUI[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].text = text;
		}
		SetUniqueText(array);
	}

	public void AddCardToBank(GameObject card)
	{
		for (int i = 0; i < cardOnBank.Length; i++)
		{
			if (cardOnBank[i] == null)
			{
				cardOnBank[i] = card;
				card.GetComponent<CardUI>().theNumberInCardSort = i;
				card.transform.SetParent(seed[i].transform);
				StartCoroutine(MoveCard(card));
				break;
			}
			if (i == 13)
			{
				break;
			}
		}
	}

	public void RemoveCardFromBank(GameObject card)
	{
		for (int i = 0; i < cardOnBank.Length; i++)
		{
			if (!(cardOnBank[i] == card))
			{
				continue;
			}
			CardUI component = card.GetComponent<CardUI>();
			card.transform.SetParent(component.parent.transform);
			if (component.isExtra && component.parent.transform.childCount == 3)
			{
				component.parent.transform.GetChild(1).transform.SetSiblingIndex(2);
				card.transform.SetSiblingIndex(1);
			}
			StartCoroutine(MoveCard(card));
			for (int j = i; j < cardOnBank.Length - 1; j++)
			{
				cardOnBank[j] = cardOnBank[j + 1];
				if (cardOnBank[j] != null)
				{
					int num = --cardOnBank[j].GetComponent<CardUI>().theNumberInCardSort;
					cardOnBank[j].transform.SetParent(seed[num].transform);
					StartCoroutine(MoveCard(cardOnBank[j]));
				}
			}
		}
	}

	private IEnumerator MoveCard(GameObject card)
	{
		Vector3 startPosition = card.GetComponent<RectTransform>().anchoredPosition;
		Vector3 target = new Vector3(0f, 0f, 0f);
		float elapsedTime = 0f;
		float duration = 0.1f;
		while (elapsedTime < duration)
		{
			card.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPosition, target, elapsedTime / duration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		card.GetComponent<RectTransform>().anchoredPosition = target;
	}

	private void Update()
	{
		int theSun = GameAPP.board.GetComponent<Board>().theSun;
		sun.text = theSun.ToString();
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
		{
			if (GameAPP.theGameStatus == 0)
			{
				PauseGame();
			}
			else if (GameAPP.theGameStatus == 1)
			{
				UIMgr.BackToGame(GameObject.Find("PauseMenuFHD"));
			}
		}
	}

	private void PauseGame()
	{
		UIMgr.EnterPauseMenu(0);
		GameAPP.gameAPP.GetComponent<AudioSource>().Pause();
		Camera.main.GetComponent<AudioSource>().Pause();
		GameAPP.canvas.GetComponent<Canvas>().sortingLayerName = "UI";
	}

	private void SetUniqueText(TextMeshProUGUI[] T)
	{
		if (GameAPP.theBoardType == 0)
		{
			return;
		}
		if (GameAPP.theBoardType == 1)
		{
			switch (GameAPP.theBoardLevel)
			{
			case 1:
				ChangeString(T, "旅行体验：第1关");
				break;
			case 2:
				ChangeString(T, "旅行体验：第2关");
				break;
			case 3:
				ChangeString(T, "旅行体验：第3关");
				break;
			case 4:
				ChangeString(T, "旅行体验：第4关");
				break;
			case 5:
				ChangeString(T, "旅行体验：第5关");
				break;
			case 6:
				ChangeString(T, "旅行体验：第6关");
				break;
			case 7:
				ChangeString(T, "超级樱桃射手：挑战1");
				break;
			case 8:
				ChangeString(T, "超级樱桃射手：挑战2");
				break;
			case 9:
				ChangeString(T, "超级樱桃射手：挑战3");
				break;
			case 10:
				ChangeString(T, "超级大嘴花：挑战1");
				break;
			case 11:
				ChangeString(T, "超级大嘴花：挑战2");
				break;
			case 12:
				ChangeString(T, "超级大嘴花：挑战3");
				break;
			case 13:
				ChangeString(T, "十旗挑战：白天");
				break;
			case 14:
				ChangeString(T, "十旗挑战：植物僵尸");
				break;
			case 15:
				ChangeString(T, "十旗挑战：随机植物");
				break;
			case 16:
				ChangeString(T, "十旗挑战：随机僵尸");
				break;
			case 17:
				ChangeString(T, "十旗挑战：随机植物VS随机僵尸");
				break;
			case 18:
				ChangeString(T, "十旗挑战：黑夜");
				break;
			case 19:
				ChangeString(T, "超级魅惑菇：挑战1");
				break;
			case 20:
				ChangeString(T, "超级魅惑菇：挑战2");
				break;
			case 21:
				ChangeString(T, "超级魅惑菇：挑战3");
				break;
			case 22:
				ChangeString(T, "超级大喷菇：挑战1");
				break;
			case 23:
				ChangeString(T, "超级大喷菇：挑战2");
				break;
			case 24:
				ChangeString(T, "超级大喷菇：挑战3");
				break;
			case 25:
				ChangeString(T, "胆小菇之梦");
				break;
			case 26:
				ChangeString(T, "十旗挑战：胆小菇之梦");
				break;
			case 27:
				ChangeString(T, "十旗挑战：黑夜舞会");
				break;
			case 28:
				ChangeString(T, "撑杆舞会");
				break;
			case 29:
				ChangeString(T, "合理密植");
				break;
			case 30:
				ChangeString(T, "二爷战争");
				break;
			case 31:
				ChangeString(T, "超级火炬挑战");
				break;
			case 32:
				ChangeString(T, "超级窝草挑战");
				break;
			case 33:
				ChangeString(T, "十旗挑战：泳池");
				break;
			case 34:
				ChangeString(T, "泳池激斗");
				break;
			case 35:
				ChangeString(T, "经典塔防");
				break;
			default:
				ChangeString(T, "挑战模式");
				break;
			}
		}
		if (GameAPP.theBoardType == 3)
		{
			string text = "生存模式：白天";
			switch (GameAPP.theBoardLevel)
			{
			case 1:
				text = "生存模式：白天";
				break;
			case 2:
				text = "生存模式：黑夜";
				break;
			case 3:
				text = "生存模式：泳池";
				break;
			case 4:
				text = "生存模式：白天（困难）";
				break;
			case 5:
				text = "生存模式：黑夜（困难）";
				break;
			case 6:
				text = "生存模式：泳池（困难）";
				break;
			case 7:
				text = "生存模式：泳池（无尽）";
				break;
			case 8:
				text = "生存模式：旅行";
				break;
			}
			if (Board.Instance.theCurrentSurvivalRound > 1)
			{
				text += $" {Board.Instance.theCurrentSurvivalRound - 1}轮已经完成";
			}
			ChangeString(T, text);
		}
	}

	private void ChangeString(TextMeshProUGUI[] T, string name)
	{
		for (int i = 0; i < T.Length; i++)
		{
			T[i].text = name;
		}
	}
}

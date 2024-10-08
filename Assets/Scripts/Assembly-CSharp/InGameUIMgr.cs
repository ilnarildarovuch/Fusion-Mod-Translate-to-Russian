using System;
using System.Collections;
using TMPro;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class InGameUIMgr : MonoBehaviour
{
	// Token: 0x0600003E RID: 62 RVA: 0x000034B4 File Offset: 0x000016B4
	private void Awake()
	{
		InGameUIMgr.Instance = this;
		this.ShovelBank = base.transform.GetChild(0).gameObject;
		this.Bottom = base.transform.GetChild(1).gameObject;
		this.SeedBank = base.transform.GetChild(2).gameObject;
		this.LevProgress = base.transform.GetChild(3).gameObject;
		this.LevelName1 = base.transform.GetChild(4).gameObject;
		this.LevelName2 = base.transform.GetChild(5).gameObject;
		this.LevelName3 = base.transform.GetChild(6).gameObject;
		this.GloveBank = base.transform.GetChild(7).gameObject;
		this.BackToMenu = base.transform.GetChild(8).gameObject;
		this.SlowTrigger = base.transform.GetChild(9).gameObject;
		this.Difficulty = base.transform.GetChild(10).gameObject;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x000035C8 File Offset: 0x000017C8
	private void Start()
	{
		string text = string.Format("冒险模式：第{0}关", GameAPP.theBoardLevel);
		TextMeshProUGUI[] array = new TextMeshProUGUI[]
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
		this.SetUniqueText(array);
	}

	// Token: 0x06000040 RID: 64 RVA: 0x0000369C File Offset: 0x0000189C
	public void AddCardToBank(GameObject card)
	{
		for (int i = 0; i < this.cardOnBank.Length; i++)
		{
			if (this.cardOnBank[i] == null)
			{
				this.cardOnBank[i] = card;
				card.GetComponent<CardUI>().theNumberInCardSort = i;
				card.transform.SetParent(this.seed[i].transform);
				base.StartCoroutine(this.MoveCard(card));
				return;
			}
			if (i == 13)
			{
				return;
			}
		}
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00003710 File Offset: 0x00001910
	public void RemoveCardFromBank(GameObject card)
	{
		for (int i = 0; i < this.cardOnBank.Length; i++)
		{
			if (this.cardOnBank[i] == card)
			{
				CardUI component = card.GetComponent<CardUI>();
				card.transform.SetParent(component.parent.transform);
				if (component.isExtra && component.parent.transform.childCount == 3)
				{
					component.parent.transform.GetChild(1).transform.SetSiblingIndex(2);
					card.transform.SetSiblingIndex(1);
				}
				base.StartCoroutine(this.MoveCard(card));
				for (int j = i; j < this.cardOnBank.Length - 1; j++)
				{
					this.cardOnBank[j] = this.cardOnBank[j + 1];
					if (this.cardOnBank[j] != null)
					{
						CardUI component2 = this.cardOnBank[j].GetComponent<CardUI>();
						int num = component2.theNumberInCardSort - 1;
						component2.theNumberInCardSort = num;
						int num2 = num;
						this.cardOnBank[j].transform.SetParent(this.seed[num2].transform);
						base.StartCoroutine(this.MoveCard(this.cardOnBank[j]));
					}
				}
			}
		}
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00003846 File Offset: 0x00001A46
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
		yield break;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00003858 File Offset: 0x00001A58
	private void Update()
	{
		int theSun = GameAPP.board.GetComponent<Board>().theSun;
		this.sun.text = theSun.ToString();
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
		{
			if (GameAPP.theGameStatus == 0)
			{
				this.PauseGame();
				return;
			}
			if (GameAPP.theGameStatus == 1)
			{
				UIMgr.BackToGame(GameObject.Find("PauseMenuFHD"));
			}
		}
	}

	// Token: 0x06000044 RID: 68 RVA: 0x000038BE File Offset: 0x00001ABE
	private void PauseGame()
	{
		UIMgr.EnterPauseMenu(0);
		GameAPP.gameAPP.GetComponent<AudioSource>().Pause();
		Camera.main.GetComponent<AudioSource>().Pause();
		GameAPP.canvas.GetComponent<Canvas>().sortingLayerName = "UI";
	}

	// Token: 0x06000045 RID: 69 RVA: 0x000038F8 File Offset: 0x00001AF8
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
				this.ChangeString(T, "旅行体验：第1关");
				break;
			case 2:
				this.ChangeString(T, "旅行体验：第2关");
				break;
			case 3:
				this.ChangeString(T, "旅行体验：第3关");
				break;
			case 4:
				this.ChangeString(T, "旅行体验：第4关");
				break;
			case 5:
				this.ChangeString(T, "旅行体验：第5关");
				break;
			case 6:
				this.ChangeString(T, "旅行体验：第6关");
				break;
			case 7:
				this.ChangeString(T, "超级樱桃射手：挑战1");
				break;
			case 8:
				this.ChangeString(T, "超级樱桃射手：挑战2");
				break;
			case 9:
				this.ChangeString(T, "超级樱桃射手：挑战3");
				break;
			case 10:
				this.ChangeString(T, "超级大嘴花：挑战1");
				break;
			case 11:
				this.ChangeString(T, "超级大嘴花：挑战2");
				break;
			case 12:
				this.ChangeString(T, "超级大嘴花：挑战3");
				break;
			case 13:
				this.ChangeString(T, "十旗挑战：白天");
				break;
			case 14:
				this.ChangeString(T, "十旗挑战：植物僵尸");
				break;
			case 15:
				this.ChangeString(T, "十旗挑战：随机植物");
				break;
			case 16:
				this.ChangeString(T, "十旗挑战：随机僵尸");
				break;
			case 17:
				this.ChangeString(T, "十旗挑战：随机植物VS随机僵尸");
				break;
			case 18:
				this.ChangeString(T, "十旗挑战：黑夜");
				break;
			case 19:
				this.ChangeString(T, "超级魅惑菇：挑战1");
				break;
			case 20:
				this.ChangeString(T, "超级魅惑菇：挑战2");
				break;
			case 21:
				this.ChangeString(T, "超级魅惑菇：挑战3");
				break;
			case 22:
				this.ChangeString(T, "超级大喷菇：挑战1");
				break;
			case 23:
				this.ChangeString(T, "超级大喷菇：挑战2");
				break;
			case 24:
				this.ChangeString(T, "超级大喷菇：挑战3");
				break;
			case 25:
				this.ChangeString(T, "胆小菇之梦");
				break;
			case 26:
				this.ChangeString(T, "十旗挑战：胆小菇之梦");
				break;
			case 27:
				this.ChangeString(T, "十旗挑战：黑夜舞会");
				break;
			case 28:
				this.ChangeString(T, "撑杆舞会");
				break;
			case 29:
				this.ChangeString(T, "合理密植");
				break;
			case 30:
				this.ChangeString(T, "二爷战争");
				break;
			case 31:
				this.ChangeString(T, "超级火炬挑战");
				break;
			case 32:
				this.ChangeString(T, "超级窝草挑战");
				break;
			case 33:
				this.ChangeString(T, "十旗挑战：泳池");
				break;
			case 34:
				this.ChangeString(T, "泳池激斗");
				break;
			case 35:
				this.ChangeString(T, "经典塔防");
				break;
			default:
				this.ChangeString(T, "挑战模式");
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
				text += string.Format(" {0}轮已经完成", Board.Instance.theCurrentSurvivalRound - 1);
			}
			this.ChangeString(T, text);
			return;
		}
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00003CB4 File Offset: 0x00001EB4
	private void ChangeString(TextMeshProUGUI[] T, string name)
	{
		for (int i = 0; i < T.Length; i++)
		{
			T[i].text = name;
		}
	}

	// Token: 0x04000041 RID: 65
	public static InGameUIMgr Instance;

	// Token: 0x04000042 RID: 66
	public GameObject[] cardOnBank = new GameObject[16];

	// Token: 0x04000043 RID: 67
	public GameObject[] seed = new GameObject[16];

	// Token: 0x04000044 RID: 68
	public TextMeshProUGUI sun;

	// Token: 0x04000045 RID: 69
	public GameObject ShovelBank;

	// Token: 0x04000046 RID: 70
	public GameObject Bottom;

	// Token: 0x04000047 RID: 71
	public GameObject SeedBank;

	// Token: 0x04000048 RID: 72
	public GameObject LevProgress;

	// Token: 0x04000049 RID: 73
	public GameObject LevelName1;

	// Token: 0x0400004A RID: 74
	public GameObject LevelName2;

	// Token: 0x0400004B RID: 75
	public GameObject LevelName3;

	// Token: 0x0400004C RID: 76
	public GameObject GloveBank;

	// Token: 0x0400004D RID: 77
	public GameObject BackToMenu;

	// Token: 0x0400004E RID: 78
	public GameObject SlowTrigger;

	// Token: 0x0400004F RID: 79
	public GameObject Difficulty;
}

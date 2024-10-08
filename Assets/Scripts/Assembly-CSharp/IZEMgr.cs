using TMPro;
using UnityEngine;

public class IZEMgr : MonoBehaviour
{
	public TextMeshProUGUI sun;

	private void Start()
	{
		string text = "我是僵尸";
		TextMeshProUGUI[] array = new TextMeshProUGUI[2]
		{
			base.transform.GetChild(0).GetComponent<TextMeshProUGUI>(),
			base.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>()
		};
		TextMeshProUGUI[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].text = text;
		}
		SetUniqueText(array);
	}

	private void Update()
	{
		if (!(GameAPP.board != null))
		{
			return;
		}
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

	public void PauseGame()
	{
		UIMgr.EnterPauseMenu(0);
		GameAPP.gameAPP.GetComponent<AudioSource>().Pause();
		Camera.main.GetComponent<AudioSource>().Pause();
		GameAPP.canvas.GetComponent<Canvas>().sortingLayerName = "UI";
	}

	private void SetUniqueText(TextMeshProUGUI[] T)
	{
		if (GameAPP.theBoardType == 2)
		{
			switch (GameAPP.theBoardLevel)
			{
			case 1:
				ChangeString(T, "我是僵尸！");
				break;
			case 2:
				ChangeString(T, "我也是僵尸！");
				break;
			case 3:
				ChangeString(T, "你能吃了它吗！");
				break;
			case 4:
				ChangeString(T, "雷区！");
				break;
			case 5:
				ChangeString(T, "完全傻了！");
				break;
			case 6:
				ChangeString(T, "决战白天！");
				break;
			case 7:
				ChangeString(T, "卑鄙的低矮植物！");
				break;
			case 8:
				ChangeString(T, "QQ弹弹！");
				break;
			case 9:
				ChangeString(T, "当代女大学生！");
				break;
			case 10:
				ChangeString(T, "胆小菇前传！");
				break;
			case 11:
				ChangeString(T, "冰冻关卡！");
				break;
			case 12:
				ChangeString(T, "决战黑夜！");
				break;
			case 13:
				ChangeString(T, "初见泳池！");
				break;
			case 14:
				ChangeString(T, "三三得九！");
				break;
			case 15:
				ChangeString(T, "嗯？");
				break;
			case 16:
				ChangeString(T, "尸愁之路！");
				break;
			case 17:
				ChangeString(T, "严肃火炬！");
				break;
			case 18:
				ChangeString(T, "决战泳池！");
				break;
			default:
				ChangeString(T, "挑战模式");
				break;
			}
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

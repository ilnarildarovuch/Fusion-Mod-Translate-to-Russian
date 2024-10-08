using System;
using TMPro;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class IZEMgr : MonoBehaviour
{
	// Token: 0x0600005B RID: 91 RVA: 0x0000423C File Offset: 0x0000243C
	private void Start()
	{
		string text = "我是僵尸";
		TextMeshProUGUI[] array = new TextMeshProUGUI[]
		{
			base.transform.GetChild(0).GetComponent<TextMeshProUGUI>(),
			base.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>()
		};
		TextMeshProUGUI[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].text = text;
		}
		this.SetUniqueText(array);
	}

	// Token: 0x0600005C RID: 92 RVA: 0x000042A4 File Offset: 0x000024A4
	private void Update()
	{
		if (GameAPP.board != null)
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
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00004317 File Offset: 0x00002517
	public void PauseGame()
	{
		UIMgr.EnterPauseMenu(0);
		GameAPP.gameAPP.GetComponent<AudioSource>().Pause();
		Camera.main.GetComponent<AudioSource>().Pause();
		GameAPP.canvas.GetComponent<Canvas>().sortingLayerName = "UI";
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00004354 File Offset: 0x00002554
	private void SetUniqueText(TextMeshProUGUI[] T)
	{
		if (GameAPP.theBoardType == 2)
		{
			switch (GameAPP.theBoardLevel)
			{
			case 1:
				this.ChangeString(T, "我是僵尸！");
				return;
			case 2:
				this.ChangeString(T, "我也是僵尸！");
				return;
			case 3:
				this.ChangeString(T, "你能吃了它吗！");
				return;
			case 4:
				this.ChangeString(T, "雷区！");
				return;
			case 5:
				this.ChangeString(T, "完全傻了！");
				return;
			case 6:
				this.ChangeString(T, "决战白天！");
				return;
			case 7:
				this.ChangeString(T, "卑鄙的低矮植物！");
				return;
			case 8:
				this.ChangeString(T, "QQ弹弹！");
				return;
			case 9:
				this.ChangeString(T, "当代女大学生！");
				return;
			case 10:
				this.ChangeString(T, "胆小菇前传！");
				return;
			case 11:
				this.ChangeString(T, "冰冻关卡！");
				return;
			case 12:
				this.ChangeString(T, "决战黑夜！");
				return;
			case 13:
				this.ChangeString(T, "初见泳池！");
				return;
			case 14:
				this.ChangeString(T, "三三得九！");
				return;
			case 15:
				this.ChangeString(T, "嗯？");
				return;
			case 16:
				this.ChangeString(T, "尸愁之路！");
				return;
			case 17:
				this.ChangeString(T, "严肃火炬！");
				return;
			case 18:
				this.ChangeString(T, "决战泳池！");
				return;
			default:
				this.ChangeString(T, "挑战模式");
				break;
			}
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x000044C0 File Offset: 0x000026C0
	private void ChangeString(TextMeshProUGUI[] T, string name)
	{
		for (int i = 0; i < T.Length; i++)
		{
			T[i].text = name;
		}
	}

	// Token: 0x04000059 RID: 89
	public TextMeshProUGUI sun;
}

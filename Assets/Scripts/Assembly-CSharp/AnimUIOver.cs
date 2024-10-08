using System;
using UnityEngine;

// Token: 0x0200002E RID: 46
public class AnimUIOver : MonoBehaviour
{
	// Token: 0x060000D0 RID: 208 RVA: 0x00005EC3 File Offset: 0x000040C3
	private void Start()
	{
		this.board = GameAPP.board.GetComponent<Board>();
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00005ED8 File Offset: 0x000040D8
	public void Die()
	{
		GameAPP.theGameStatus = 0;
		this.board.droppedAwardOrOver = false;
		foreach (object obj in this.board.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.CompareTag("Zombie"))
			{
				Object.Destroy(transform.gameObject);
			}
		}
		switch (UIMgr.GetSceneType(GameAPP.theBoardType, GameAPP.theBoardLevel, -1))
		{
		case 0:
			GameAPP.ChangeMusic(2);
			break;
		case 1:
			GameAPP.ChangeMusic(4);
			break;
		case 2:
			GameAPP.ChangeMusic(6);
			break;
		}
		InGameUIMgr.Instance.ShovelBank.SetActive(true);
		InGameUIMgr.Instance.SlowTrigger.SetActive(true);
		InGameUIMgr.Instance.LevelName2.SetActive(true);
		if (GameAPP.developerMode || GameAPP.advLevelCompleted[3])
		{
			InGameUIMgr.Instance.GloveBank.SetActive(true);
		}
		else if (GameAPP.theBoardType == 1)
		{
			int theBoardLevel = GameAPP.theBoardLevel;
			if (theBoardLevel - 1 <= 2)
			{
				InGameUIMgr.Instance.GloveBank.SetActive(true);
			}
		}
		string text = null;
		if (GameAPP.theBoardType == 0)
		{
			switch (GameAPP.theBoardLevel)
			{
			case 1:
				text = "试试把豌豆种到向日葵上面";
				break;
			case 2:
				text = "手里拿着植物时可以融合的植物会发光";
				break;
			case 3:
				text = "坚果融合后可以回满血哦";
				break;
			case 4:
				text = "手套可以自由移动或融合植物";
				break;
			case 7:
				text = "礼盒可开出基础植物，亦可作为任意基础植物进行融合";
				break;
			case 8:
				text = "僵尸掉落的铁桶可以放到豌豆射手和坚果墙上";
				break;
			case 9:
				text = "高坚果作为紫卡只能放在坚果墙上";
				break;
			case 10:
				text = "一个格子里可以放3个小喷菇";
				break;
			case 11:
				text = "小秘密：小喷菇+向日葵=阳光菇";
				break;
			case 13:
				text = "肥料可用于给植物回血，更多用法自行探索";
				break;
			case 16:
				text = "毁灭大喷菇要手动点击哦";
				break;
			case 17:
				text = "橄榄帽可以放到高坚果上";
				break;
			case 18:
				text = "忧郁菇作为紫卡只能放在大喷菇上";
				break;
			case 19:
				text = "听说了吗？有的睡莲头上会长植物";
				break;
			case 24:
				text = "传说有些僵尸被刺红温了更容易受伤";
				break;
			case 26:
				text = "地刺王作为紫卡只能放在地刺上";
				break;
			case 27:
				text = "猫尾草作为紫卡只能放在睡莲上";
				break;
			}
		}
		if (GameAPP.theBoardType == 1)
		{
			int theBoardLevel = GameAPP.theBoardLevel;
			if (theBoardLevel <= 19)
			{
				switch (theBoardLevel)
				{
				case 1:
					text = "超级樱桃射手+樱桃机枪射手";
					break;
				case 2:
					text = "火爆窝瓜+窝炬";
					break;
				case 3:
					text = "魅惑菇+魅惑菇";
					break;
				case 4:
					text = "超级大喷菇+超级魅惑菇";
					break;
				case 5:
					text = "超级大嘴花+樱桃大嘴花";
					break;
				case 6:
					text = "小心你没见过的僵尸！";
					break;
				case 7:
					text = "豌豆+樱桃+樱桃";
					break;
				case 8:
				case 9:
					break;
				case 10:
					text = "豌豆+坚果+大嘴花";
					break;
				default:
					if (theBoardLevel == 19)
					{
						text = "大喷菇+胆小菇+魅惑菇";
					}
					break;
				}
			}
			else if (theBoardLevel != 22)
			{
				switch (theBoardLevel)
				{
				case 29:
					text = "这一关只能种植或融合小喷菇！";
					break;
				case 31:
					text = "火炬+辣椒+辣椒";
					break;
				case 32:
					text = "水草+窝瓜+三线";
					break;
				case 35:
					text = "击败僵尸以获取阳光";
					break;
				}
			}
			else
			{
				text = "大喷菇+寒冰菇+毁灭菇";
			}
		}
		if (text != null)
		{
			this.board.inGameText.EnableText(text, 7f);
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00006238 File Offset: 0x00004438
	public void Die1()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x040000A8 RID: 168
	private Board board;
}

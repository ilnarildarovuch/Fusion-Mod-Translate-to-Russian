using System;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class TravelMenuBtn : MonoBehaviour
{
	// Token: 0x06000077 RID: 119 RVA: 0x0000488E File Offset: 0x00002A8E
	private void Start()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.originPosition = this.rectTransform.anchoredPosition;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x000048B2 File Offset: 0x00002AB2
	private void OnMouseEnter()
	{
		CursorChange.SetClickCursor();
	}

	// Token: 0x06000079 RID: 121 RVA: 0x000048B9 File Offset: 0x00002AB9
	private void OnMouseExit()
	{
		this.rectTransform.anchoredPosition = this.originPosition;
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x0600007A RID: 122 RVA: 0x000048D6 File Offset: 0x00002AD6
	private void OnMouseDown()
	{
		GameAPP.PlaySoundNotPause(28, 0.5f);
		this.rectTransform.anchoredPosition = new Vector2(this.originPosition.x + 1f, this.originPosition.y - 1f);
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00004918 File Offset: 0x00002B18
	private void OnMouseUp()
	{
		CursorChange.SetDefaultCursor();
		this.rectTransform.anchoredPosition = this.originPosition;
		switch (this.choiceNumber)
		{
		case -1:
			this.QuitQuickly();
			return;
		case 0:
			this.UnlockPlant();
			LevelData.plantInTravel.RemoveAll((LevelData.PlantInTravel plant) => plant.thePlantColumn > 2);
			this.QuitSlow();
			return;
		case 1:
			this.UnlockPlant();
			GameAPP.hardZombie = true;
			this.QuitSlow();
			return;
		default:
			return;
		}
	}

	// Token: 0x0600007C RID: 124 RVA: 0x000049AC File Offset: 0x00002BAC
	private void ShowText(int num)
	{
		switch (num)
		{
		case 0:
			InGameText.INSTANCE.EnableText("已解超级樱桃机枪射手，樱桃机枪+超级樱桃射手", 5f);
			return;
		case 1:
			InGameText.INSTANCE.EnableText("已解锁火爆窝炬，火爆窝瓜+窝炬", 5f);
			return;
		case 2:
			InGameText.INSTANCE.EnableText("已解锁樱桃战神，樱桃大嘴花+超级大嘴花", 5f);
			return;
		case 3:
			InGameText.INSTANCE.EnableText("已解锁究极大喷菇，超级大喷菇+超级魅惑菇", 5f);
			return;
		default:
			return;
		}
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00004A23 File Offset: 0x00002C23
	private void QuitQuickly()
	{
		Object.Destroy(this.thisMenu);
		Time.timeScale = GameAPP.gameSpeed;
		Board.Instance.DarkQuit();
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00004A44 File Offset: 0x00002C44
	private void QuitSlow()
	{
		Object.Destroy(this.thisMenu);
		Time.timeScale = GameAPP.gameSpeed;
		Board.Instance.ChoiceOver();
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00004A68 File Offset: 0x00002C68
	private void UnlockPlant()
	{
		bool flag = true;
		int num;
		for (;;)
		{
			num = Random.Range(0, 4);
			bool[] unlocked = GameAPP.unlocked;
			for (int i = 0; i < unlocked.Length; i++)
			{
				if (!unlocked[i])
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				break;
			}
			if (!GameAPP.unlocked[num])
			{
				goto Block_4;
			}
		}
		InGameText.INSTANCE.EnableText("已解锁全部植物", 5f);
		LevelData.plantInTravel.RemoveAll((LevelData.PlantInTravel plant) => plant.thePlantColumn > 2);
		Object.Destroy(this.thisMenu);
		Time.timeScale = GameAPP.gameSpeed;
		GameAPP.theGameStatus = 0;
		Board.Instance.ChoiceOver();
		return;
		Block_4:
		GameAPP.unlocked[num] = true;
		this.ShowText(num);
	}

	// Token: 0x04000067 RID: 103
	public int choiceNumber;

	// Token: 0x04000068 RID: 104
	public GameObject thisMenu;

	// Token: 0x04000069 RID: 105
	private Vector3 originPosition;

	// Token: 0x0400006A RID: 106
	private RectTransform rectTransform;
}

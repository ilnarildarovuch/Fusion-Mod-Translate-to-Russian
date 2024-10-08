using UnityEngine;

public class TravelMenuBtn : MonoBehaviour
{
	public int choiceNumber;

	public GameObject thisMenu;

	private Vector3 originPosition;

	private RectTransform rectTransform;

	private void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		originPosition = rectTransform.anchoredPosition;
	}

	private void OnMouseEnter()
	{
		CursorChange.SetClickCursor();
	}

	private void OnMouseExit()
	{
		rectTransform.anchoredPosition = originPosition;
		CursorChange.SetDefaultCursor();
	}

	private void OnMouseDown()
	{
		GameAPP.PlaySoundNotPause(28);
		rectTransform.anchoredPosition = new Vector2(originPosition.x + 1f, originPosition.y - 1f);
	}

	private void OnMouseUp()
	{
		CursorChange.SetDefaultCursor();
		rectTransform.anchoredPosition = originPosition;
		switch (choiceNumber)
		{
		case -1:
			QuitQuickly();
			break;
		case 0:
			UnlockPlant();
			LevelData.plantInTravel.RemoveAll((LevelData.PlantInTravel plant) => plant.thePlantColumn > 2);
			QuitSlow();
			break;
		case 1:
			UnlockPlant();
			GameAPP.hardZombie = true;
			QuitSlow();
			break;
		}
	}

	private void ShowText(int num)
	{
		switch (num)
		{
		case 0:
			InGameText.INSTANCE.EnableText("已解超级樱桃机枪射手，樱桃机枪+超级樱桃射手", 5f);
			break;
		case 1:
			InGameText.INSTANCE.EnableText("已解锁火爆窝炬，火爆窝瓜+窝炬", 5f);
			break;
		case 2:
			InGameText.INSTANCE.EnableText("已解锁樱桃战神，樱桃大嘴花+超级大嘴花", 5f);
			break;
		case 3:
			InGameText.INSTANCE.EnableText("已解锁究极大喷菇，超级大喷菇+超级魅惑菇", 5f);
			break;
		}
	}

	private void QuitQuickly()
	{
		Object.Destroy(thisMenu);
		Time.timeScale = GameAPP.gameSpeed;
		Board.Instance.DarkQuit();
	}

	private void QuitSlow()
	{
		Object.Destroy(thisMenu);
		Time.timeScale = GameAPP.gameSpeed;
		Board.Instance.ChoiceOver();
	}

	private void UnlockPlant()
	{
		bool flag = true;
		int num;
		do
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
				InGameText.INSTANCE.EnableText("已解锁全部植物", 5f);
				LevelData.plantInTravel.RemoveAll((LevelData.PlantInTravel plant) => plant.thePlantColumn > 2);
				Object.Destroy(thisMenu);
				Time.timeScale = GameAPP.gameSpeed;
				GameAPP.theGameStatus = 0;
				Board.Instance.ChoiceOver();
				return;
			}
		}
		while (GameAPP.unlocked[num]);
		GameAPP.unlocked[num] = true;
		ShowText(num);
	}
}

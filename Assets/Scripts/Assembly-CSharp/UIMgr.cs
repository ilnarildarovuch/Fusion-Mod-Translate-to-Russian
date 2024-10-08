using System;
using TMPro;
using UnityEngine;

// Token: 0x02000109 RID: 265
public class UIMgr : MonoBehaviour
{
	// Token: 0x06000544 RID: 1348 RVA: 0x0002D6CC File Offset: 0x0002B8CC
	public static void EnterMainMenu()
	{
		UIMgr.ClearCanvas();
		Camera.main.transform.position = new Vector3(0f, 0f, -200f);
		Object.Destroy(GameObject.Find("Tutor"));
		GameAPP.theGameStatus = -1;
		Time.timeScale = GameAPP.gameSpeed;
		GameAPP.theBoardLevel = -1;
		GameAPP.theBoardType = -1;
		GameAPP.theGameStatus = -1;
		GameAPP.ChangeMusic(0);
		GameAPP.PlaySoundNotPause(38, 0.5f);
		GameObject gameObject = Resources.Load<GameObject>("UI/MainMenu/MainMenuFHD");
		Object.Instantiate<GameObject>(gameObject, GameAPP.canvas.transform).name = gameObject.name;
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x0002D76C File Offset: 0x0002B96C
	public static void EnterAdvantureMenu()
	{
		if (GameAPP.theBoardType == 1)
		{
			UIMgr.EnterChallengeMenu();
			return;
		}
		if (GameAPP.theBoardType == 2)
		{
			UIMgr.EnterIZEMenu();
			return;
		}
		if (GameAPP.theBoardType == 3)
		{
			UIMgr.EnterSurvivalEMenu();
			return;
		}
		UIMgr.MenuNormalSettings();
		GameObject gameObject = Resources.Load<GameObject>("UI/AdvantureMenu/AdvantureMenuFHD");
		Object.Instantiate<GameObject>(gameObject, GameAPP.canvas.transform).name = gameObject.name;
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x0002D7D0 File Offset: 0x0002B9D0
	public static void EnterChallengeMenu()
	{
		UIMgr.MenuNormalSettings();
		GameObject gameObject = Resources.Load<GameObject>("UI/AdvantureMenu/ChallengeMenuFHD");
		Object.Instantiate<GameObject>(gameObject, GameAPP.canvas.transform).name = gameObject.name;
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x0002D808 File Offset: 0x0002BA08
	public static void EnterIZEMenu()
	{
		UIMgr.MenuNormalSettings();
		GameObject gameObject = Resources.Load<GameObject>("UI/AdvantureMenu/IZEMenuFHD");
		Object.Instantiate<GameObject>(gameObject, GameAPP.canvas.transform).name = gameObject.name;
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x0002D840 File Offset: 0x0002BA40
	public static void EnterSurvivalEMenu()
	{
		UIMgr.MenuNormalSettings();
		GameObject gameObject = Resources.Load<GameObject>("UI/AdvantureMenu/SurvivalMenuFHD");
		Object.Instantiate<GameObject>(gameObject, GameAPP.canvas.transform).name = gameObject.name;
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x0002D878 File Offset: 0x0002BA78
	private static void MenuNormalSettings()
	{
		Time.timeScale = GameAPP.gameSpeed;
		GameAPP.theBoardLevel = -1;
		GameAPP.theBoardType = -1;
		GameAPP.theGameStatus = -1;
		GameAPP.ChangeMusic(1);
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x0002D89C File Offset: 0x0002BA9C
	public static void EnterPauseMenu(int place)
	{
		if (place == 0)
		{
			GameAPP.PlaySoundNotPause(30, 0.5f);
			GameAPP.theGameStatus = 1;
			Time.timeScale = 0f;
		}
		GameObject gameObject = Resources.Load<GameObject>("UI/PauseMenu/PauseMenuFHD");
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, GameAPP.canvas.transform);
		gameObject2.name = gameObject.name;
		if (place == 1)
		{
			gameObject2.transform.GetChild(2).gameObject.SetActive(false);
			gameObject2.transform.GetChild(3).gameObject.SetActive(false);
			gameObject2.transform.GetChild(4).gameObject.SetActive(false);
			gameObject2.transform.GetChild(5).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "确定";
			gameObject2.transform.GetChild(6).gameObject.SetActive(true);
		}
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x0002D97C File Offset: 0x0002BB7C
	public static void EnterGame(int levelType, int levelNumber, GameObject menu = null)
	{
		UIMgr.ClearCanvas();
		Time.timeScale = GameAPP.gameSpeed;
		for (int i = 0; i < GameAPP.unlocked.Length; i++)
		{
			GameAPP.unlocked[i] = false;
		}
		GameAPP.hardZombie = false;
		if (levelType == 2)
		{
			UIMgr.EnterIZGame(levelNumber);
			return;
		}
		GameAPP.ChangeMusic(1);
		GameAPP.theBoardType = levelType;
		GameAPP.theBoardLevel = levelNumber;
		GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>(UIMgr.GetMapName(levelType, levelNumber)));
		gameObject.name = "Background";
		GameAPP.board = gameObject;
		GameAPP.board.AddComponent<Board>();
		if (levelType == 3)
		{
			if (levelNumber != 7)
			{
				if (levelNumber != 8)
				{
					Board.Instance.theCurrentSurvivalRound = 1;
				}
				else
				{
					Board.Instance.isTravel = true;
					Board.Instance.theCurrentSurvivalRound = 1;
				}
			}
			else
			{
				Board.Instance.EnterEndlessGame();
			}
			InitZombieList.InitZombie(levelType, levelNumber, Board.Instance.theCurrentSurvivalRound);
		}
		else
		{
			InitZombieList.InitZombie(levelType, levelNumber, 0);
		}
		GameAPP.theGameStatus = 2;
		GameAPP.canvas.GetComponent<Canvas>().sortingLayerName = "Default";
		gameObject.AddComponent<InitBoard>();
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x0002DA7C File Offset: 0x0002BC7C
	public static void EnterTravelGame(int levelType, int levelNumber, int theRound)
	{
		Time.timeScale = GameAPP.gameSpeed;
		GameAPP.ChangeMusic(1);
		GameAPP.theBoardType = levelType;
		GameAPP.theBoardLevel = levelNumber;
		string path = "Background/background1";
		if (theRound == 4)
		{
			path = "Background/background2";
		}
		else if (theRound == 7)
		{
			path = "Background/background3";
		}
		else
		{
			Debug.LogError("旅行模式关卡错误");
		}
		GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>(path));
		gameObject.name = "Background";
		GameAPP.board = gameObject;
		Board board = GameAPP.board.AddComponent<Board>();
		board.theSun = LevelData.level.theSun;
		board.isTravel = true;
		if (theRound == 4)
		{
			board.isNight = true;
		}
		else if (theRound == 7)
		{
			board.roadNum = 6;
			board.roadType[2] = 1;
			board.roadType[3] = 1;
			for (int i = 0; i < board.boxType.GetLength(0); i++)
			{
				board.boxType[i, 2] = 1;
				board.boxType[i, 3] = 1;
			}
		}
		Board.Instance.theCurrentSurvivalRound = LevelData.level.theCurrentRound;
		InitZombieList.InitZombie(levelType, levelNumber, Board.Instance.theCurrentSurvivalRound);
		LevelData.LoadPlant();
		GameAPP.theGameStatus = 2;
		GameAPP.canvas.GetComponent<Canvas>().sortingLayerName = "Default";
		gameObject.AddComponent<InitBoard>();
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x0002DBB4 File Offset: 0x0002BDB4
	private static void EnterIZGame(int levelNumber)
	{
		GameAPP.ChangeMusic(15);
		GameAPP.theBoardType = 2;
		GameAPP.theBoardLevel = levelNumber;
		if (levelNumber > 12)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>("Background/backgroundIZPool"));
			gameObject.transform.position = new Vector3(-10f, 5f, 0f);
			gameObject.name = "Background";
			GameAPP.board = gameObject;
			gameObject.AddComponent<Board>().isIZ = true;
			Board.Instance.roadNum = 6;
			Board.Instance.roadType[2] = 1;
			Board.Instance.roadType[3] = 1;
			for (int i = 0; i < Board.Instance.boxType.GetLength(0); i++)
			{
				Board.Instance.boxType[i, 2] = 1;
				Board.Instance.boxType[i, 3] = 1;
			}
			GameObject gameObject2 = Resources.Load<GameObject>("UI/InGameMenu/IZE/InGameUIIZE");
			GameObject gameObject3 = Object.Instantiate<GameObject>(gameObject2, GameAPP.canvas.transform);
			gameObject3.transform.position = new Vector3(0f, 0f, -1f);
			gameObject3.name = gameObject2.name;
		}
		else
		{
			GameObject gameObject4 = Object.Instantiate<GameObject>(Resources.Load<GameObject>("Background/backgroundIZE"));
			gameObject4.transform.position = new Vector3(-10f, 5f, 0f);
			gameObject4.name = "Background";
			GameAPP.board = gameObject4;
			gameObject4.AddComponent<Board>().isIZ = true;
			GameObject gameObject5 = Resources.Load<GameObject>("UI/InGameMenu/IZE/InGameUIIZE");
			GameObject gameObject6 = Object.Instantiate<GameObject>(gameObject5, GameAPP.canvas.transform);
			gameObject6.transform.position = new Vector3(0f, 0f, -1f);
			gameObject6.name = gameObject5.name;
		}
		GameAPP.canvas.GetComponent<Canvas>().sortingLayerName = "Default";
		GameAPP.theGameStatus = 0;
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x0002DD80 File Offset: 0x0002BF80
	public static void EVEAuto(int road)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>("Background/backgroundIZE"));
		gameObject.transform.position = new Vector3(-10f, 5f, 0f);
		gameObject.name = "Background";
		GameAPP.board = gameObject;
		GameAPP.canvas.GetComponent<Canvas>().sortingLayerName = "Default";
		gameObject.AddComponent<Board>().isIZ = true;
		gameObject.GetComponent<Board>().isAutoEve = true;
		gameObject.GetComponent<Board>().isEveStarted = true;
		gameObject.GetComponent<Board>().isEveStart = true;
		GameAPP.theGameStatus = 0;
		GameObject gameObject2 = Resources.Load<GameObject>("UI/InGameMenu/IZE/InGameUIIZE");
		GameObject gameObject3 = Object.Instantiate<GameObject>(gameObject2, GameAPP.canvas.transform);
		gameObject3.name = gameObject2.name;
		gameObject3.transform.GetChild(4).GetChild(0).GetComponent<EveBtn>().OpenEveGame();
		EveBtn.LoadPlants();
		foreach (GameObject gameObject4 in gameObject.GetComponent<Board>().plantArray)
		{
			if (gameObject4 != null)
			{
				Plant component = gameObject4.GetComponent<Plant>();
				if (component.thePlantRow != road)
				{
					component.Die();
				}
			}
		}
		EveBtn.SetPlant();
		EveBtn.AutoGame();
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x0002DEA8 File Offset: 0x0002C0A8
	public static void BackToGame(GameObject menu)
	{
		if (GameAPP.theGameStatus == -2)
		{
			GameAPP.theGameStatus = -1;
			Time.timeScale = GameAPP.gameSpeed;
			Object.Destroy(menu);
			return;
		}
		GameAPP.theGameStatus = 0;
		Time.timeScale = GameAPP.gameSpeed;
		GameAPP.gameAPP.GetComponent<AudioSource>().UnPause();
		Camera.main.GetComponent<AudioSource>().UnPause();
		GameAPP.canvas.GetComponent<Canvas>().sortingLayerName = "Default";
		Object.Destroy(menu);
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x0002DF20 File Offset: 0x0002C120
	public static void EnterLoseMenu()
	{
		GameAPP.theGameStatus = 1;
		Time.timeScale = 0f;
		GameAPP.gameAPP.GetComponent<AudioSource>().Pause();
		Camera.main.GetComponent<AudioSource>().Pause();
		GameAPP.canvas.GetComponent<Canvas>().sortingLayerName = "up";
		Object.Destroy(GameAPP.board.GetComponent<InitBoard>().theInGameUI);
		GameObject gameObject = Resources.Load<GameObject>("UI/InGameMenu/Lose/LoseMenu");
		Object.Instantiate<GameObject>(gameObject, GameAPP.canvas.transform).name = gameObject.name;
		GameAPP.PlaySoundNotPause(52, 0.5f);
		Object.Destroy(GameAPP.board.GetComponent<Mouse>().theItemOnMouse);
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x0002DFCC File Offset: 0x0002C1CC
	public static void EnterHelpMenu()
	{
		GameObject gameObject = Resources.Load<GameObject>("UI/MainMenu/HelpMenu");
		Object.Instantiate<GameObject>(gameObject, GameAPP.canvas.transform).name = gameObject.name;
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x0002E000 File Offset: 0x0002C200
	public static void EnterOtherMenu()
	{
		GameObject gameObject = Resources.Load<GameObject>("UI/MainMenu/OtherMenu");
		Object.Instantiate<GameObject>(gameObject, GameAPP.canvas.transform).name = gameObject.name;
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x0002E034 File Offset: 0x0002C234
	public static void EnterAlmanac(bool changeMusic = false)
	{
		if (changeMusic)
		{
			GameAPP.ChangeMusic(1);
		}
		GameObject gameObject = Resources.Load<GameObject>("UI/Almanac/Almanac");
		Object.Instantiate<GameObject>(gameObject).name = gameObject.name;
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x0002E068 File Offset: 0x0002C268
	public static void LookPlant()
	{
		GameObject gameObject = Resources.Load<GameObject>("UI/Almanac/AlmanacPlant");
		Object.Instantiate<GameObject>(gameObject).name = gameObject.name;
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x0002E094 File Offset: 0x0002C294
	private static void ClearCanvas()
	{
		foreach (object obj in GameAPP.canvasUp.transform)
		{
			Transform transform = (Transform)obj;
			if (transform != null)
			{
				Object.Destroy(transform.gameObject);
			}
		}
		foreach (object obj2 in GameAPP.canvas.transform)
		{
			Transform transform2 = (Transform)obj2;
			if (transform2 != null)
			{
				Object.Destroy(transform2.gameObject);
			}
		}
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x0002E158 File Offset: 0x0002C358
	private static string GetMapName(int theLevelType, int theLevelNumber)
	{
		string result;
		switch (UIMgr.GetSceneType(theLevelType, theLevelNumber, -1))
		{
		case 0:
			if (theLevelNumber == 35)
			{
				result = "Background/background4";
			}
			else
			{
				result = "Background/background1";
			}
			break;
		case 1:
			result = "Background/background2";
			break;
		case 2:
			result = "Background/background3";
			break;
		default:
			result = "Background/background1";
			break;
		}
		return result;
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x0002E1B0 File Offset: 0x0002C3B0
	public static int GetSceneType(int theLevelType, int theLevelNumber, int sceneType = -1)
	{
		if (sceneType != -1 && Board.Instance.isTravel)
		{
			return sceneType;
		}
		switch (theLevelType)
		{
		case 0:
			if (theLevelNumber - 10 <= 8)
			{
				return 1;
			}
			if (theLevelNumber - 19 > 8)
			{
				return 0;
			}
			return 2;
		case 1:
			switch (theLevelNumber)
			{
			case 3:
			case 4:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 23:
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 29:
				return 1;
			case 5:
			case 6:
			case 32:
			case 33:
			case 34:
				return 2;
			}
			return 0;
		case 3:
			switch (theLevelNumber)
			{
			case 2:
			case 5:
				return 1;
			case 3:
			case 6:
			case 7:
				return 2;
			case 8:
				if (!(Board.Instance != null))
				{
					return 0;
				}
				if (Board.Instance.theCurrentSurvivalRound <= 6 && Board.Instance.theCurrentSurvivalRound > 3)
				{
					return 1;
				}
				if (Board.Instance.theCurrentSurvivalRound > 6)
				{
					return 2;
				}
				return 0;
			}
			return 0;
		}
		return 0;
	}

	// Token: 0x02000155 RID: 341
	public enum SceneType
	{
		// Token: 0x04000509 RID: 1289
		Day,
		// Token: 0x0400050A RID: 1290
		Night,
		// Token: 0x0400050B RID: 1291
		Pool,
		// Token: 0x0400050C RID: 1292
		NightPool,
		// Token: 0x0400050D RID: 1293
		Roof,
		// Token: 0x0400050E RID: 1294
		NightRoof
	}
}

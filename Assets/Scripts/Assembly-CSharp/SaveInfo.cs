using System.IO;
using UnityEngine;

public class SaveInfo : MonoBehaviour
{
	private string filePath;

	private string savePath;

	public static SaveInfo Instance;

	private void Awake()
	{
		Instance = this;
		filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
		savePath = Path.Combine(Application.persistentDataPath, "poolEndless.json");
		LoadPlayerData();
		LoadEndLessData();
		Application.quitting += SavePlayerData;
		Application.quitting += SaveEndlessData;
	}

	private void LoadPlayerData()
	{
		if (File.Exists(filePath))
		{
			LevelCompleted levelCompleted = JsonUtility.FromJson<LevelCompleted>(File.ReadAllText(filePath));
			if (levelCompleted.advLevelCompleted != null)
			{
				GameAPP.advLevelCompleted = levelCompleted.advLevelCompleted;
			}
			if (levelCompleted.clgLevelCompleted != null)
			{
				GameAPP.clgLevelCompleted = levelCompleted.clgLevelCompleted;
			}
			if (levelCompleted.unlockMixPlant != null)
			{
				GameAPP.unlockMixPlant = levelCompleted.unlockMixPlant;
			}
			if (levelCompleted.gameLevelCompleted != null)
			{
				GameAPP.gameLevelCompleted = levelCompleted.gameLevelCompleted;
			}
			if (levelCompleted.survivalLevelCompleted != null)
			{
				GameAPP.survivalLevelCompleted = levelCompleted.survivalLevelCompleted;
			}
			GameAPP.difficulty = levelCompleted.difficulty;
			GameAPP.gameSpeed = levelCompleted.gameSpeed;
			GameAPP.gameMusicVolume = levelCompleted.gameMusicVolume;
			GameAPP.gameSoundVolume = levelCompleted.gameSoundVolume;
		}
		else
		{
			Directory.CreateDirectory(Path.GetDirectoryName(filePath));
		}
	}

	private void LoadEndLessData()
	{
		if (File.Exists(savePath))
		{
			PoolEndless poolEndless = JsonUtility.FromJson<PoolEndless>(File.ReadAllText(savePath));
			if (poolEndless.plant != null)
			{
				PlantsInLevel.plant = poolEndless.plant;
			}
			if (poolEndless.boardData != null)
			{
				PlantsInLevel.boardData = poolEndless.boardData;
			}
			PlantsInLevel.maxRound = poolEndless.maxRound;
		}
		else
		{
			Directory.CreateDirectory(Path.GetDirectoryName(savePath));
		}
	}

	public void SavePlayerData()
	{
		string contents = JsonUtility.ToJson(new LevelCompleted
		{
			isSaved = true,
			advLevelCompleted = GameAPP.advLevelCompleted,
			clgLevelCompleted = GameAPP.clgLevelCompleted,
			gameLevelCompleted = GameAPP.gameLevelCompleted,
			difficulty = GameAPP.difficulty,
			gameMusicVolume = GameAPP.gameMusicVolume,
			gameSoundVolume = GameAPP.gameSoundVolume,
			gameSpeed = GameAPP.gameSpeed,
			unlockMixPlant = GameAPP.unlockMixPlant,
			survivalLevelCompleted = GameAPP.survivalLevelCompleted
		});
		Directory.CreateDirectory(Path.GetDirectoryName(filePath));
		File.WriteAllText(filePath, contents);
		Debug.Log("Player data saved.");
	}

	public void SaveEndlessData()
	{
		string contents = JsonUtility.ToJson(new PoolEndless
		{
			plant = PlantsInLevel.plant,
			boardData = PlantsInLevel.boardData,
			maxRound = PlantsInLevel.maxRound
		});
		Directory.CreateDirectory(Path.GetDirectoryName(savePath));
		File.WriteAllText(savePath, contents);
	}
}

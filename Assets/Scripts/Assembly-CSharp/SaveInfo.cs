using System;
using System.IO;
using UnityEngine;

// Token: 0x02000101 RID: 257
public class SaveInfo : MonoBehaviour
{
	// Token: 0x06000511 RID: 1297 RVA: 0x0002B724 File Offset: 0x00029924
	private void Awake()
	{
		SaveInfo.Instance = this;
		this.filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
		this.savePath = Path.Combine(Application.persistentDataPath, "poolEndless.json");
		this.LoadPlayerData();
		this.LoadEndLessData();
		Application.quitting += this.SavePlayerData;
		Application.quitting += this.SaveEndlessData;
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x0002B790 File Offset: 0x00029990
	private void LoadPlayerData()
	{
		if (File.Exists(this.filePath))
		{
			LevelCompleted levelCompleted = JsonUtility.FromJson<LevelCompleted>(File.ReadAllText(this.filePath));
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
			return;
		}
		Directory.CreateDirectory(Path.GetDirectoryName(this.filePath));
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x0002B85C File Offset: 0x00029A5C
	private void LoadEndLessData()
	{
		if (File.Exists(this.savePath))
		{
			PoolEndless poolEndless = JsonUtility.FromJson<PoolEndless>(File.ReadAllText(this.savePath));
			if (poolEndless.plant != null)
			{
				PlantsInLevel.plant = poolEndless.plant;
			}
			if (poolEndless.boardData != null)
			{
				PlantsInLevel.boardData = poolEndless.boardData;
			}
			PlantsInLevel.maxRound = poolEndless.maxRound;
			return;
		}
		Directory.CreateDirectory(Path.GetDirectoryName(this.savePath));
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x0002B8CC File Offset: 0x00029ACC
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
		Directory.CreateDirectory(Path.GetDirectoryName(this.filePath));
		File.WriteAllText(this.filePath, contents);
		Debug.Log("Player data saved.");
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x0002B978 File Offset: 0x00029B78
	public void SaveEndlessData()
	{
		string contents = JsonUtility.ToJson(new PoolEndless
		{
			plant = PlantsInLevel.plant,
			boardData = PlantsInLevel.boardData,
			maxRound = PlantsInLevel.maxRound
		});
		Directory.CreateDirectory(Path.GetDirectoryName(this.savePath));
		File.WriteAllText(this.savePath, contents);
	}

	// Token: 0x04000283 RID: 643
	private string filePath;

	// Token: 0x04000284 RID: 644
	private string savePath;

	// Token: 0x04000285 RID: 645
	public static SaveInfo Instance;
}

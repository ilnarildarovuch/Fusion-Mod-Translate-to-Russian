using System;

[Serializable]
public class LevelCompleted
{
	public bool isSaved;

	public bool[] advLevelCompleted;

	public bool[] clgLevelCompleted;

	public bool[] gameLevelCompleted;

	public int difficulty = 2;

	public float gameMusicVolume = 1f;

	public float gameSoundVolume = 1f;

	public float gameSpeed = 1f;

	public bool[] unlockMixPlant;

	public bool[] survivalLevelCompleted;
}

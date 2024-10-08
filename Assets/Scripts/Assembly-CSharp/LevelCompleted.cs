using System;

// Token: 0x02000102 RID: 258
[Serializable]
public class LevelCompleted
{
	// Token: 0x04000286 RID: 646
	public bool isSaved;

	// Token: 0x04000287 RID: 647
	public bool[] advLevelCompleted;

	// Token: 0x04000288 RID: 648
	public bool[] clgLevelCompleted;

	// Token: 0x04000289 RID: 649
	public bool[] gameLevelCompleted;

	// Token: 0x0400028A RID: 650
	public int difficulty = 2;

	// Token: 0x0400028B RID: 651
	public float gameMusicVolume = 1f;

	// Token: 0x0400028C RID: 652
	public float gameSoundVolume = 1f;

	// Token: 0x0400028D RID: 653
	public float gameSpeed = 1f;

	// Token: 0x0400028E RID: 654
	public bool[] unlockMixPlant;

	// Token: 0x0400028F RID: 655
	public bool[] survivalLevelCompleted;
}

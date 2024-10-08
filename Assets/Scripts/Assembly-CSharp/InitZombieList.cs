using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000106 RID: 262
public class InitZombieList : MonoBehaviour
{
	// Token: 0x0600051F RID: 1311 RVA: 0x0002BAE0 File Offset: 0x00029CE0
	private static List<int> GetRandomZombiesFromLandNormal()
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>(InitZombieList.zombieInLandNormal);
		int num = 0;
		while (num < 5 && list2.Count > 0)
		{
			int index = Random.Range(0, list2.Count);
			list.Add(list2[index]);
			list2.RemoveAt(index);
			num++;
		}
		return list;
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x0002BB38 File Offset: 0x00029D38
	private static List<int> GetRandomZombiesFromLandHard()
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>(InitZombieList.zombieInLandHard);
		int num = Random.Range(7, 11);
		int num2 = 0;
		while (num2 < num && list2.Count > 0)
		{
			int index = Random.Range(0, list2.Count);
			list.Add(list2[index]);
			list2.RemoveAt(index);
			num2++;
		}
		return list;
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x0002BB9C File Offset: 0x00029D9C
	private static List<int> GetRandomZombiesFromPoolNormal()
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>(InitZombieList.zombieInPoolNormal);
		int num = 0;
		while (num < 5 && list2.Count > 0)
		{
			int index = Random.Range(0, list2.Count);
			list.Add(list2[index]);
			list2.RemoveAt(index);
			num++;
		}
		return list;
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x0002BBF4 File Offset: 0x00029DF4
	private static List<int> GetRandomZombiesFromPoolHard()
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>(InitZombieList.zombieInPoolHard);
		int num = Random.Range(7, 11);
		int num2 = 0;
		while (num2 < num && list2.Count > 0)
		{
			int index = Random.Range(0, list2.Count);
			list.Add(list2[index]);
			list2.RemoveAt(index);
			num2++;
		}
		return list;
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x0002BC58 File Offset: 0x00029E58
	private static List<int> GetRandomZombiesFromTravel()
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>(InitZombieList.zombieInTravel);
		int num = 0;
		while (num < 2 && list2.Count > 0)
		{
			int index = Random.Range(0, list2.Count);
			list.Add(list2[index]);
			list2.RemoveAt(index);
			num++;
		}
		return list;
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x0002BCB0 File Offset: 0x00029EB0
	public static void InitZombie(int theLevelType, int theLevelNumber, int theSurvivalRound = 0)
	{
		InitZombieList.InitList();
		if (GameAPP.difficulty == 5)
		{
			if (theLevelType == 0 && (theLevelNumber == 0 || theLevelNumber == 1))
			{
				InitZombieList.multiplier = 3;
			}
			else
			{
				InitZombieList.multiplier = 4;
			}
		}
		else
		{
			InitZombieList.multiplier = GameAPP.difficulty;
		}
		if (theSurvivalRound == 0)
		{
			InitZombieList.SetAllowZombieTypeSpawn(theLevelType, theLevelNumber);
		}
		else
		{
			InitZombieList.SurvivalZombieTypeSpawn(theLevelNumber, theSurvivalRound);
		}
		InitZombieList.theMaxWave = 10;
		InitZombieList.zombiePoint = 0;
		switch (theLevelType)
		{
		case 0:
			InitZombieList.InitAdvWave(theLevelNumber);
			break;
		case 1:
			InitZombieList.InitChallengeWave(theLevelNumber);
			break;
		case 3:
			InitZombieList.InitSurvivalWave(theLevelNumber);
			break;
		}
		for (int i = 1; i <= InitZombieList.theMaxWave; i++)
		{
			if (theLevelType == 3)
			{
				InitZombieList.zombiePoint = (i + (theSurvivalRound - 1) * 10) * InitZombieList.multiplier;
			}
			else
			{
				InitZombieList.zombiePoint = i * InitZombieList.multiplier;
			}
			while (InitZombieList.zombiePoint > 0)
			{
				bool flag = false;
				int num;
				do
				{
					num = InitZombieList.PickZombie();
					if (i < 10)
					{
						if (GameAPP.difficulty == 5)
						{
							break;
						}
						if (num <= 10)
						{
							if (num != 6 && num != 10)
							{
								goto IL_FE;
							}
						}
						else if (num != 15)
						{
							switch (num)
							{
							case 104:
							case 106:
							case 108:
							case 109:
								break;
							case 105:
							case 107:
								goto IL_FE;
							default:
								goto IL_FE;
							}
						}
						flag = true;
						goto IL_100;
						IL_FE:
						flag = false;
					}
					IL_100:;
				}
				while (flag);
				int num2 = InitZombieList.AddZombieToList(num, i);
				InitZombieList.zombiePoint -= num2;
			}
			if (i == InitZombieList.theMaxWave)
			{
				for (int j = 0; j < InitZombieList.allowZombieTypeSpawn.Length; j++)
				{
					if (InitZombieList.allowZombieTypeSpawn[j])
					{
						InitZombieList.zombiePoint = 20;
						InitZombieList.AddZombieToList(j, i);
					}
				}
				InitZombieList.zombiePoint = -1;
			}
		}
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x0002BE2C File Offset: 0x0002A02C
	private static void InitList()
	{
		for (int i = 0; i < InitZombieList.zombieList.GetLength(0); i++)
		{
			for (int j = 0; j < InitZombieList.zombieList.GetLength(1); j++)
			{
				InitZombieList.zombieList[i, j] = -1;
			}
		}
		for (int k = 0; k < InitZombieList.zombieTypeList.Length; k++)
		{
			InitZombieList.zombieTypeList[k] = -1;
		}
		for (int l = 0; l < InitZombieList.allowZombieTypeSpawn.Length; l++)
		{
			InitZombieList.allowZombieTypeSpawn[l] = false;
		}
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x0002BEA8 File Offset: 0x0002A0A8
	private static int PickZombie()
	{
		int num = 0;
		foreach (KeyValuePair<int, int> keyValuePair in InitZombieList.zombieWeights)
		{
			if (InitZombieList.allowZombieTypeSpawn[keyValuePair.Key])
			{
				num += keyValuePair.Value;
			}
		}
		int num2 = Random.Range(1, num + 1);
		int num3 = 0;
		foreach (KeyValuePair<int, int> keyValuePair2 in InitZombieList.zombieWeights)
		{
			if (InitZombieList.allowZombieTypeSpawn[keyValuePair2.Key])
			{
				num3 += keyValuePair2.Value;
				if (num2 <= num3)
				{
					return keyValuePair2.Key;
				}
			}
		}
		return 0;
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x0002BF84 File Offset: 0x0002A184
	private static int AddZombieToList(int zombieType, int wave)
	{
		int num;
		switch (zombieType)
		{
		case 0:
			num = 1;
			goto IL_C7;
		case 1:
		case 7:
		case 11:
		case 12:
		case 13:
			goto IL_C5;
		case 2:
			break;
		case 3:
		case 8:
		case 17:
			goto IL_AD;
		case 4:
		case 5:
		case 9:
		case 19:
			goto IL_B1;
		case 6:
		case 14:
		case 15:
		case 18:
		case 20:
		case 21:
			num = 6;
			goto IL_C7;
		case 10:
			goto IL_BD;
		case 16:
			goto IL_B5;
		default:
			switch (zombieType)
			{
			case 100:
				break;
			case 101:
				goto IL_AD;
			case 102:
				goto IL_C5;
			case 103:
			case 105:
				goto IL_B1;
			case 104:
			case 109:
				goto IL_BD;
			case 106:
			case 107:
			case 108:
			case 110:
			case 111:
				goto IL_B5;
			default:
				if (zombieType - 200 > 3)
				{
					goto IL_C5;
				}
				num = 5;
				goto IL_C7;
			}
			break;
		}
		num = 2;
		goto IL_C7;
		IL_AD:
		num = 3;
		goto IL_C7;
		IL_B1:
		num = 4;
		goto IL_C7;
		IL_B5:
		num = 5;
		goto IL_C7;
		IL_BD:
		num = 7;
		goto IL_C7;
		IL_C5:
		num = 1;
		IL_C7:
		if (num > InitZombieList.zombiePoint)
		{
			zombieType = 0;
			if (GameAPP.theBoardType == 1)
			{
				int theBoardLevel = GameAPP.theBoardLevel;
				if (theBoardLevel != 8)
				{
					switch (theBoardLevel)
					{
					case 11:
					case 14:
						break;
					case 12:
					case 13:
					case 15:
						goto IL_115;
					case 16:
					case 17:
						zombieType = 105;
						goto IL_115;
					default:
						goto IL_115;
					}
				}
				zombieType = 100;
			}
		}
		IL_115:
		if (InitZombieList.ContainsType(zombieType, InitZombieList.zombieTypeList))
		{
			for (int i = 0; i < InitZombieList.zombieTypeList.Length; i++)
			{
				if (InitZombieList.zombieTypeList[i] == -1)
				{
					InitZombieList.zombieTypeList[i] = zombieType;
					break;
				}
			}
		}
		for (int j = 0; j < InitZombieList.zombieList.GetLength(0); j++)
		{
			if (InitZombieList.zombieList[j, wave] == -1)
			{
				InitZombieList.zombieList[j, wave] = zombieType;
				break;
			}
		}
		return num;
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x0002C110 File Offset: 0x0002A310
	private static bool ContainsType(int type, int[] list)
	{
		for (int i = 0; i < list.Length; i++)
		{
			if (list[i] == type)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x0002C138 File Offset: 0x0002A338
	private static void InitAdvWave(int theLevelNumber)
	{
		switch (theLevelNumber)
		{
		case 1:
		case 2:
		case 3:
		case 10:
		case 11:
		case 12:
		case 19:
		case 20:
			InitZombieList.theMaxWave = 10;
			return;
		case 4:
		case 5:
		case 6:
		case 13:
		case 14:
		case 15:
		case 21:
		case 22:
			InitZombieList.theMaxWave = 20;
			return;
		case 7:
		case 8:
		case 9:
		case 16:
		case 17:
		case 18:
		case 23:
		case 24:
			InitZombieList.theMaxWave = 30;
			return;
		case 25:
		case 26:
		case 27:
			InitZombieList.theMaxWave = 40;
			return;
		default:
			return;
		}
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x0002C1DC File Offset: 0x0002A3DC
	private static void InitChallengeWave(int theLevelNumber)
	{
		switch (theLevelNumber)
		{
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
			InitZombieList.theMaxWave = 40;
			return;
		case 7:
		case 8:
		case 10:
		case 11:
		case 19:
		case 20:
		case 22:
		case 23:
			InitZombieList.theMaxWave = 30;
			return;
		case 9:
		case 12:
		case 21:
		case 24:
		case 25:
		case 28:
		case 31:
		case 32:
		case 34:
		case 35:
			InitZombieList.theMaxWave = 40;
			return;
		case 13:
		case 14:
		case 15:
		case 16:
		case 17:
		case 18:
		case 26:
		case 27:
		case 33:
			InitZombieList.theMaxWave = 100;
			return;
		}
		InitZombieList.theMaxWave = 20;
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x0002C2A8 File Offset: 0x0002A4A8
	private static void InitSurvivalWave(int theLevelNumber)
	{
		switch (theLevelNumber)
		{
		case 1:
		case 2:
		case 3:
			InitZombieList.theMaxWave = 10;
			Board.Instance.theSurvivalMaxRound = 5;
			return;
		case 4:
		case 5:
		case 6:
		case 7:
			InitZombieList.theMaxWave = 20;
			Board.Instance.theSurvivalMaxRound = int.MaxValue;
			return;
		case 8:
			InitZombieList.theMaxWave = 20;
			Board.Instance.theSurvivalMaxRound = 9;
			return;
		default:
			InitZombieList.theMaxWave = 10;
			Board.Instance.theSurvivalMaxRound = 5;
			return;
		}
	}

	// Token: 0x0600052C RID: 1324 RVA: 0x0002C330 File Offset: 0x0002A530
	private static void SurvivalZombieTypeSpawn(int theLevelNumber, int theRound)
	{
		if (Board.Instance.isTravel && Board.Instance.theCurrentSurvivalRound > 6)
		{
			InitZombieList.PoolHard();
			if (GameAPP.hardZombie)
			{
				InitZombieList.Travel();
			}
			return;
		}
		if (theRound == 1)
		{
			InitZombieList.allowZombieTypeSpawn[0] = true;
			InitZombieList.allowZombieTypeSpawn[2] = true;
			InitZombieList.allowZombieTypeSpawn[4] = true;
			return;
		}
		InitZombieList.allowZombieTypeSpawn[4] = true;
		switch (theLevelNumber)
		{
		case 1:
		case 2:
			InitZombieList.LandNormal();
			return;
		case 3:
			InitZombieList.PoolNormal();
			return;
		case 4:
		case 5:
			InitZombieList.LandHard();
			return;
		case 6:
		case 7:
			InitZombieList.PoolHard();
			return;
		case 8:
			InitZombieList.LandHard();
			if (GameAPP.hardZombie)
			{
				InitZombieList.Travel();
				InitZombieList.allowZombieTypeSpawn[200] = false;
				return;
			}
			break;
		default:
			InitZombieList.LandHard();
			break;
		}
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x0002C3F4 File Offset: 0x0002A5F4
	private static void LandNormal()
	{
		foreach (int num in InitZombieList.GetRandomZombiesFromLandNormal())
		{
			InitZombieList.allowZombieTypeSpawn[num] = true;
		}
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x0002C448 File Offset: 0x0002A648
	private static void LandHard()
	{
		foreach (int num in InitZombieList.GetRandomZombiesFromLandHard())
		{
			InitZombieList.allowZombieTypeSpawn[num] = true;
		}
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x0002C49C File Offset: 0x0002A69C
	private static void PoolNormal()
	{
		foreach (int num in InitZombieList.GetRandomZombiesFromPoolNormal())
		{
			InitZombieList.allowZombieTypeSpawn[num] = true;
		}
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x0002C4F0 File Offset: 0x0002A6F0
	private static void PoolHard()
	{
		foreach (int num in InitZombieList.GetRandomZombiesFromPoolHard())
		{
			InitZombieList.allowZombieTypeSpawn[num] = true;
		}
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x0002C544 File Offset: 0x0002A744
	private static void Travel()
	{
		foreach (int num in InitZombieList.GetRandomZombiesFromTravel())
		{
			InitZombieList.allowZombieTypeSpawn[num] = true;
		}
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x0002C598 File Offset: 0x0002A798
	private static void SetAllowZombieTypeSpawn(int theLevelType, int theLevelNumber)
	{
		if (theLevelType == 0)
		{
			switch (theLevelNumber)
			{
			case 1:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				break;
			case 2:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				break;
			case 3:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				break;
			case 4:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[3] = true;
				break;
			case 5:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				InitZombieList.allowZombieTypeSpawn[100] = true;
				break;
			case 6:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				InitZombieList.allowZombieTypeSpawn[21] = true;
				break;
			case 7:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				InitZombieList.allowZombieTypeSpawn[21] = true;
				InitZombieList.allowZombieTypeSpawn[5] = true;
				break;
			case 8:
				InitZombieList.allowZombieTypeSpawn[106] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				InitZombieList.allowZombieTypeSpawn[108] = true;
				InitZombieList.allowZombieTypeSpawn[8] = true;
				break;
			case 9:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[3] = true;
				InitZombieList.allowZombieTypeSpawn[106] = true;
				InitZombieList.allowZombieTypeSpawn[21] = true;
				InitZombieList.allowZombieTypeSpawn[104] = true;
				InitZombieList.allowZombieTypeSpawn[107] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				break;
			case 10:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				break;
			case 11:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[8] = true;
				break;
			case 12:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[5] = true;
				InitZombieList.allowZombieTypeSpawn[9] = true;
				break;
			case 13:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[5] = true;
				InitZombieList.allowZombieTypeSpawn[9] = true;
				InitZombieList.allowZombieTypeSpawn[3] = true;
				break;
			case 14:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				InitZombieList.allowZombieTypeSpawn[5] = true;
				InitZombieList.allowZombieTypeSpawn[9] = true;
				InitZombieList.allowZombieTypeSpawn[3] = true;
				break;
			case 15:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				InitZombieList.allowZombieTypeSpawn[5] = true;
				InitZombieList.allowZombieTypeSpawn[9] = true;
				InitZombieList.allowZombieTypeSpawn[6] = true;
				break;
			case 16:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				InitZombieList.allowZombieTypeSpawn[5] = true;
				InitZombieList.allowZombieTypeSpawn[9] = true;
				InitZombieList.allowZombieTypeSpawn[6] = true;
				InitZombieList.allowZombieTypeSpawn[10] = true;
				break;
			case 17:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				InitZombieList.allowZombieTypeSpawn[5] = true;
				InitZombieList.allowZombieTypeSpawn[109] = true;
				InitZombieList.allowZombieTypeSpawn[6] = true;
				InitZombieList.allowZombieTypeSpawn[10] = true;
				break;
			case 18:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				InitZombieList.allowZombieTypeSpawn[5] = true;
				InitZombieList.allowZombieTypeSpawn[109] = true;
				InitZombieList.allowZombieTypeSpawn[21] = true;
				InitZombieList.allowZombieTypeSpawn[111] = true;
				InitZombieList.allowZombieTypeSpawn[8] = true;
				InitZombieList.allowZombieTypeSpawn[9] = true;
				break;
			case 19:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				break;
			case 20:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[9] = true;
				break;
			case 21:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				InitZombieList.allowZombieTypeSpawn[17] = true;
				break;
			case 22:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[3] = true;
				InitZombieList.allowZombieTypeSpawn[5] = true;
				InitZombieList.allowZombieTypeSpawn[14] = true;
				break;
			case 23:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				InitZombieList.allowZombieTypeSpawn[5] = true;
				InitZombieList.allowZombieTypeSpawn[14] = true;
				InitZombieList.allowZombieTypeSpawn[16] = true;
				break;
			case 24:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				InitZombieList.allowZombieTypeSpawn[8] = true;
				InitZombieList.allowZombieTypeSpawn[14] = true;
				InitZombieList.allowZombieTypeSpawn[18] = true;
				break;
			case 25:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				InitZombieList.allowZombieTypeSpawn[8] = true;
				InitZombieList.allowZombieTypeSpawn[14] = true;
				InitZombieList.allowZombieTypeSpawn[18] = true;
				InitZombieList.allowZombieTypeSpawn[20] = true;
				break;
			case 26:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				InitZombieList.allowZombieTypeSpawn[16] = true;
				InitZombieList.allowZombieTypeSpawn[14] = true;
				InitZombieList.allowZombieTypeSpawn[18] = true;
				InitZombieList.allowZombieTypeSpawn[20] = true;
				break;
			case 27:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[19] = true;
				InitZombieList.allowZombieTypeSpawn[16] = true;
				InitZombieList.allowZombieTypeSpawn[14] = true;
				InitZombieList.allowZombieTypeSpawn[15] = true;
				InitZombieList.allowZombieTypeSpawn[20] = true;
				InitZombieList.allowZombieTypeSpawn[18] = true;
				InitZombieList.allowZombieTypeSpawn[17] = true;
				break;
			default:
				InitZombieList.AllowAll();
				break;
			}
		}
		if (theLevelType == 1)
		{
			switch (theLevelNumber)
			{
			case 1:
			case 2:
			case 3:
			case 4:
				InitZombieList.LandHard();
				return;
			case 5:
				InitZombieList.PoolHard();
				return;
			case 6:
				InitZombieList.PoolHard();
				InitZombieList.allowZombieTypeSpawn[203] = true;
				InitZombieList.allowZombieTypeSpawn[200] = true;
				InitZombieList.allowZombieTypeSpawn[202] = true;
				InitZombieList.allowZombieTypeSpawn[201] = true;
				return;
			case 7:
			case 10:
				InitZombieList.AllowDayNormal();
				return;
			case 8:
			case 11:
			case 14:
				InitZombieList.AllowPlantZombie();
				return;
			case 9:
			case 12:
			case 13:
				InitZombieList.AllowDay();
				return;
			case 16:
			case 17:
				InitZombieList.allowZombieTypeSpawn[105] = true;
				InitZombieList.allowZombieTypeSpawn[110] = true;
				return;
			case 18:
			case 21:
			case 24:
				InitZombieList.AllowNight();
				return;
			case 19:
			case 22:
			case 29:
				InitZombieList.AllowNightNormal();
				return;
			case 20:
			case 23:
			case 27:
				InitZombieList.AllowEliteNight();
				return;
			case 28:
				InitZombieList.allowZombieTypeSpawn[3] = true;
				InitZombieList.allowZombieTypeSpawn[6] = true;
				InitZombieList.allowZombieTypeSpawn[10] = true;
				return;
			case 30:
				InitZombieList.allowZombieTypeSpawn[5] = true;
				InitZombieList.allowZombieTypeSpawn[104] = true;
				InitZombieList.allowZombieTypeSpawn[15] = true;
				return;
			case 31:
				InitZombieList.allowZombieTypeSpawn[4] = true;
				InitZombieList.allowZombieTypeSpawn[106] = true;
				InitZombieList.allowZombieTypeSpawn[9] = true;
				InitZombieList.allowZombieTypeSpawn[111] = true;
				InitZombieList.allowZombieTypeSpawn[109] = true;
				InitZombieList.allowZombieTypeSpawn[15] = true;
				return;
			case 32:
				InitZombieList.allowZombieTypeSpawn[14] = true;
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				InitZombieList.allowZombieTypeSpawn[17] = true;
				InitZombieList.allowZombieTypeSpawn[19] = true;
				return;
			case 33:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				InitZombieList.allowZombieTypeSpawn[17] = true;
				InitZombieList.allowZombieTypeSpawn[9] = true;
				InitZombieList.allowZombieTypeSpawn[14] = true;
				InitZombieList.allowZombieTypeSpawn[15] = true;
				InitZombieList.allowZombieTypeSpawn[16] = true;
				InitZombieList.allowZombieTypeSpawn[18] = true;
				InitZombieList.allowZombieTypeSpawn[20] = true;
				InitZombieList.allowZombieTypeSpawn[19] = true;
				return;
			case 34:
				InitZombieList.allowZombieTypeSpawn[16] = true;
				InitZombieList.allowZombieTypeSpawn[18] = true;
				InitZombieList.allowZombieTypeSpawn[14] = true;
				return;
			case 35:
				InitZombieList.allowZombieTypeSpawn[0] = true;
				InitZombieList.allowZombieTypeSpawn[2] = true;
				InitZombieList.allowZombieTypeSpawn[4] = true;
				InitZombieList.allowZombieTypeSpawn[8] = true;
				InitZombieList.allowZombieTypeSpawn[5] = true;
				InitZombieList.allowZombieTypeSpawn[15] = true;
				InitZombieList.allowZombieTypeSpawn[9] = true;
				InitZombieList.allowZombieTypeSpawn[109] = true;
				InitZombieList.allowZombieTypeSpawn[20] = true;
				InitZombieList.allowZombieTypeSpawn[16] = true;
				InitZombieList.allowZombieTypeSpawn[18] = true;
				InitZombieList.allowZombieTypeSpawn[106] = true;
				InitZombieList.allowZombieTypeSpawn[111] = true;
				InitZombieList.allowZombieTypeSpawn[107] = true;
				return;
			}
			InitZombieList.AllowAll();
		}
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x0002CDA8 File Offset: 0x0002AFA8
	private static void AllowAll()
	{
		InitZombieList.allowZombieTypeSpawn[0] = true;
		InitZombieList.allowZombieTypeSpawn[2] = true;
		InitZombieList.allowZombieTypeSpawn[3] = true;
		InitZombieList.allowZombieTypeSpawn[4] = true;
		InitZombieList.allowZombieTypeSpawn[5] = true;
		InitZombieList.allowZombieTypeSpawn[6] = true;
		InitZombieList.allowZombieTypeSpawn[8] = true;
		InitZombieList.allowZombieTypeSpawn[9] = true;
		InitZombieList.allowZombieTypeSpawn[10] = true;
		InitZombieList.allowZombieTypeSpawn[11] = true;
		InitZombieList.allowZombieTypeSpawn[12] = true;
		InitZombieList.allowZombieTypeSpawn[13] = true;
		InitZombieList.allowZombieTypeSpawn[15] = true;
		InitZombieList.allowZombieTypeSpawn[100] = true;
		InitZombieList.allowZombieTypeSpawn[101] = true;
		InitZombieList.allowZombieTypeSpawn[103] = true;
		InitZombieList.allowZombieTypeSpawn[104] = true;
		InitZombieList.allowZombieTypeSpawn[105] = true;
		InitZombieList.allowZombieTypeSpawn[106] = true;
		InitZombieList.allowZombieTypeSpawn[107] = true;
		InitZombieList.allowZombieTypeSpawn[108] = true;
		InitZombieList.allowZombieTypeSpawn[109] = true;
		InitZombieList.allowZombieTypeSpawn[110] = true;
		InitZombieList.allowZombieTypeSpawn[111] = true;
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x0002CE88 File Offset: 0x0002B088
	private static void AllowDay()
	{
		InitZombieList.allowZombieTypeSpawn[0] = true;
		InitZombieList.allowZombieTypeSpawn[2] = true;
		InitZombieList.allowZombieTypeSpawn[3] = true;
		InitZombieList.allowZombieTypeSpawn[5] = true;
		InitZombieList.allowZombieTypeSpawn[4] = true;
		InitZombieList.allowZombieTypeSpawn[104] = true;
		InitZombieList.allowZombieTypeSpawn[106] = true;
		InitZombieList.allowZombieTypeSpawn[108] = true;
		InitZombieList.allowZombieTypeSpawn[100] = true;
		InitZombieList.allowZombieTypeSpawn[101] = true;
		InitZombieList.allowZombieTypeSpawn[107] = true;
		InitZombieList.allowZombieTypeSpawn[103] = true;
	}

	// Token: 0x06000535 RID: 1333 RVA: 0x0002CEFC File Offset: 0x0002B0FC
	private static void AllowDayNormal()
	{
		InitZombieList.allowZombieTypeSpawn[0] = true;
		InitZombieList.allowZombieTypeSpawn[2] = true;
		InitZombieList.allowZombieTypeSpawn[3] = true;
		InitZombieList.allowZombieTypeSpawn[4] = true;
		InitZombieList.allowZombieTypeSpawn[5] = true;
		InitZombieList.allowZombieTypeSpawn[100] = true;
		InitZombieList.allowZombieTypeSpawn[101] = true;
		InitZombieList.allowZombieTypeSpawn[103] = true;
	}

	// Token: 0x06000536 RID: 1334 RVA: 0x0002CF4C File Offset: 0x0002B14C
	private static void AllowNightNormal()
	{
		InitZombieList.allowZombieTypeSpawn[0] = true;
		InitZombieList.allowZombieTypeSpawn[2] = true;
		InitZombieList.allowZombieTypeSpawn[3] = true;
		InitZombieList.allowZombieTypeSpawn[4] = true;
		InitZombieList.allowZombieTypeSpawn[8] = true;
		InitZombieList.allowZombieTypeSpawn[9] = true;
		InitZombieList.allowZombieTypeSpawn[111] = true;
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x0002CF88 File Offset: 0x0002B188
	private static void AllowNight()
	{
		InitZombieList.allowZombieTypeSpawn[0] = true;
		InitZombieList.allowZombieTypeSpawn[2] = true;
		InitZombieList.allowZombieTypeSpawn[3] = true;
		InitZombieList.allowZombieTypeSpawn[100] = true;
		InitZombieList.allowZombieTypeSpawn[103] = true;
		InitZombieList.allowZombieTypeSpawn[4] = true;
		InitZombieList.allowZombieTypeSpawn[9] = true;
		InitZombieList.allowZombieTypeSpawn[8] = true;
		InitZombieList.allowZombieTypeSpawn[6] = true;
		InitZombieList.allowZombieTypeSpawn[10] = true;
		InitZombieList.allowZombieTypeSpawn[104] = true;
		InitZombieList.allowZombieTypeSpawn[111] = true;
		InitZombieList.allowZombieTypeSpawn[109] = true;
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x0002D004 File Offset: 0x0002B204
	private static void AllowEliteNight()
	{
		InitZombieList.allowZombieTypeSpawn[104] = true;
		InitZombieList.allowZombieTypeSpawn[108] = true;
		InitZombieList.allowZombieTypeSpawn[106] = true;
		InitZombieList.allowZombieTypeSpawn[109] = true;
		InitZombieList.allowZombieTypeSpawn[111] = true;
		InitZombieList.allowZombieTypeSpawn[6] = true;
		InitZombieList.allowZombieTypeSpawn[10] = true;
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x0002D044 File Offset: 0x0002B244
	private static void AllowPlantZombie()
	{
		InitZombieList.allowZombieTypeSpawn[100] = true;
		InitZombieList.allowZombieTypeSpawn[101] = true;
		InitZombieList.allowZombieTypeSpawn[103] = true;
		InitZombieList.allowZombieTypeSpawn[104] = true;
		InitZombieList.allowZombieTypeSpawn[106] = true;
		InitZombieList.allowZombieTypeSpawn[107] = true;
		InitZombieList.allowZombieTypeSpawn[108] = true;
		InitZombieList.allowZombieTypeSpawn[109] = true;
		InitZombieList.allowZombieTypeSpawn[111] = true;
	}

	// Token: 0x04000295 RID: 661
	private static int multiplier = 1;

	// Token: 0x04000296 RID: 662
	public static int theMaxWave;

	// Token: 0x04000297 RID: 663
	public static int[,] zombieList = new int[50, 101];

	// Token: 0x04000298 RID: 664
	public static int[] zombieTypeList = new int[64];

	// Token: 0x04000299 RID: 665
	private static bool[] allowZombieTypeSpawn = new bool[256];

	// Token: 0x0400029A RID: 666
	private static int zombiePoint;

	// Token: 0x0400029B RID: 667
	private static readonly Dictionary<int, int> zombieWeights = new Dictionary<int, int>
	{
		{
			0,
			4000
		},
		{
			2,
			3000
		},
		{
			4,
			2000
		},
		{
			8,
			2000
		},
		{
			5,
			2000
		},
		{
			3,
			2000
		},
		{
			17,
			1500
		},
		{
			100,
			1500
		},
		{
			19,
			1000
		},
		{
			9,
			1000
		},
		{
			101,
			1000
		},
		{
			103,
			1000
		},
		{
			107,
			750
		},
		{
			20,
			750
		},
		{
			6,
			750
		},
		{
			16,
			750
		},
		{
			105,
			750
		},
		{
			14,
			500
		},
		{
			18,
			500
		},
		{
			111,
			500
		},
		{
			108,
			500
		},
		{
			106,
			500
		},
		{
			104,
			300
		},
		{
			110,
			300
		},
		{
			15,
			300
		},
		{
			109,
			300
		},
		{
			10,
			300
		},
		{
			21,
			300
		},
		{
			200,
			1000
		},
		{
			201,
			1000
		},
		{
			202,
			1000
		},
		{
			203,
			1000
		}
	};

	// Token: 0x0400029C RID: 668
	private static readonly List<int> zombieInLandNormal = new List<int>
	{
		2,
		4,
		8,
		5,
		3,
		100,
		9,
		101,
		103,
		107,
		16,
		111,
		108,
		106
	};

	// Token: 0x0400029D RID: 669
	private static readonly List<int> zombieInLandHard = new List<int>
	{
		4,
		9,
		101,
		103,
		107,
		20,
		6,
		16,
		18,
		111,
		108,
		106,
		104,
		15,
		109,
		10,
		21
	};

	// Token: 0x0400029E RID: 670
	private static readonly List<int> zombieInPoolNormal = new List<int>
	{
		2,
		4,
		8,
		5,
		3,
		100,
		9,
		101,
		103,
		107,
		16,
		111,
		108,
		106,
		17,
		19
	};

	// Token: 0x0400029F RID: 671
	private static readonly List<int> zombieInPoolHard = new List<int>
	{
		4,
		9,
		17,
		19,
		14,
		101,
		103,
		107,
		20,
		6,
		16,
		18,
		111,
		108,
		106,
		104,
		15,
		109,
		10,
		21
	};

	// Token: 0x040002A0 RID: 672
	private static readonly List<int> zombieInTravel = new List<int>
	{
		200,
		201,
		202,
		203
	};

	// Token: 0x02000152 RID: 338
	public enum ZombietType
	{
		// Token: 0x040004B2 RID: 1202
		NormalZombie,
		// Token: 0x040004B3 RID: 1203
		ConeZombie = 2,
		// Token: 0x040004B4 RID: 1204
		PolevaulterZombie,
		// Token: 0x040004B5 RID: 1205
		BucketZombie,
		// Token: 0x040004B6 RID: 1206
		PaperZombie,
		// Token: 0x040004B7 RID: 1207
		DancePolZombie,
		// Token: 0x040004B8 RID: 1208
		DancePolZombie2,
		// Token: 0x040004B9 RID: 1209
		DoorZombie,
		// Token: 0x040004BA RID: 1210
		FootballZombie,
		// Token: 0x040004BB RID: 1211
		JacksonZombie,
		// Token: 0x040004BC RID: 1212
		ZombieDuck,
		// Token: 0x040004BD RID: 1213
		ConeZombieDuck,
		// Token: 0x040004BE RID: 1214
		BucketZombieDuck,
		// Token: 0x040004BF RID: 1215
		SubmarineZombie,
		// Token: 0x040004C0 RID: 1216
		ElitePaperZombie,
		// Token: 0x040004C1 RID: 1217
		DriverZombie,
		// Token: 0x040004C2 RID: 1218
		SnorkleZombie,
		// Token: 0x040004C3 RID: 1219
		SuperDriver,
		// Token: 0x040004C4 RID: 1220
		Dolphinrider,
		// Token: 0x040004C5 RID: 1221
		DrownZombie,
		// Token: 0x040004C6 RID: 1222
		DollDiamond,
		// Token: 0x040004C7 RID: 1223
		DollGold,
		// Token: 0x040004C8 RID: 1224
		DollSilver,
		// Token: 0x040004C9 RID: 1225
		PeaShooterZombie = 100,
		// Token: 0x040004CA RID: 1226
		CherryShooterZombie,
		// Token: 0x040004CB RID: 1227
		SuperCherryShooterZombie,
		// Token: 0x040004CC RID: 1228
		WallNutZombie,
		// Token: 0x040004CD RID: 1229
		CherryPaperZombie,
		// Token: 0x040004CE RID: 1230
		RandomZombie,
		// Token: 0x040004CF RID: 1231
		BucketNutZombie,
		// Token: 0x040004D0 RID: 1232
		CherryNutZombie,
		// Token: 0x040004D1 RID: 1233
		IronPeaZzombie,
		// Token: 0x040004D2 RID: 1234
		TallNutFootballZombie,
		// Token: 0x040004D3 RID: 1235
		RandomPlusZombie,
		// Token: 0x040004D4 RID: 1236
		TallIceNutZombie,
		// Token: 0x040004D5 RID: 1237
		SuperSubmarine = 200,
		// Token: 0x040004D6 RID: 1238
		JacksonDriver,
		// Token: 0x040004D7 RID: 1239
		FootballDrown,
		// Token: 0x040004D8 RID: 1240
		CherryPaperZ95
	}

	// Token: 0x02000153 RID: 339
	public enum ChallengeLevelName
	{
		// Token: 0x040004DA RID: 1242
		Travel1 = 1,
		// Token: 0x040004DB RID: 1243
		Travel2,
		// Token: 0x040004DC RID: 1244
		Travel3,
		// Token: 0x040004DD RID: 1245
		Travel4,
		// Token: 0x040004DE RID: 1246
		Travel5,
		// Token: 0x040004DF RID: 1247
		Travel6,
		// Token: 0x040004E0 RID: 1248
		SuperCherryShooter1,
		// Token: 0x040004E1 RID: 1249
		SuperCherryShooter2,
		// Token: 0x040004E2 RID: 1250
		SuperCherryShooter3,
		// Token: 0x040004E3 RID: 1251
		SuperChomper1,
		// Token: 0x040004E4 RID: 1252
		SuperChomper2,
		// Token: 0x040004E5 RID: 1253
		SuperChomper3,
		// Token: 0x040004E6 RID: 1254
		FlagDay,
		// Token: 0x040004E7 RID: 1255
		FlagPlantZombie,
		// Token: 0x040004E8 RID: 1256
		FlagRandomPlant,
		// Token: 0x040004E9 RID: 1257
		FlagRandomZombie,
		// Token: 0x040004EA RID: 1258
		FlagRandomAll,
		// Token: 0x040004EB RID: 1259
		FlagNight,
		// Token: 0x040004EC RID: 1260
		SuperHypno1,
		// Token: 0x040004ED RID: 1261
		SuperHypno2,
		// Token: 0x040004EE RID: 1262
		SuperHypno3,
		// Token: 0x040004EF RID: 1263
		SuperFume1,
		// Token: 0x040004F0 RID: 1264
		SuperFume2,
		// Token: 0x040004F1 RID: 1265
		SuperFume3,
		// Token: 0x040004F2 RID: 1266
		ScaredDream,
		// Token: 0x040004F3 RID: 1267
		FlagDream,
		// Token: 0x040004F4 RID: 1268
		FlagElite,
		// Token: 0x040004F5 RID: 1269
		PolDance,
		// Token: 0x040004F6 RID: 1270
		PuffTime,
		// Token: 0x040004F7 RID: 1271
		PaperBattle,
		// Token: 0x040004F8 RID: 1272
		SuperTorch,
		// Token: 0x040004F9 RID: 1273
		SuperKelp,
		// Token: 0x040004FA RID: 1274
		FlagPool,
		// Token: 0x040004FB RID: 1275
		DriverBattle,
		// Token: 0x040004FC RID: 1276
		TowerDefense
	}

	// Token: 0x02000154 RID: 340
	public enum ZombieStatus
	{
		// Token: 0x040004FE RID: 1278
		Default,
		// Token: 0x040004FF RID: 1279
		Dying,
		// Token: 0x04000500 RID: 1280
		Pol_run,
		// Token: 0x04000501 RID: 1281
		Pol_jump,
		// Token: 0x04000502 RID: 1282
		Paper_lookPaper,
		// Token: 0x04000503 RID: 1283
		Paper_losePaper,
		// Token: 0x04000504 RID: 1284
		Paper_angry,
		// Token: 0x04000505 RID: 1285
		Snokle_inWater,
		// Token: 0x04000506 RID: 1286
		Dolphinrider_fast,
		// Token: 0x04000507 RID: 1287
		Dolphinrider_jump
	}
}

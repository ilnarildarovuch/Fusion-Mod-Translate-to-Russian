using System.Collections.Generic;
using UnityEngine;

public class InitZombieList : MonoBehaviour
{
	public enum ZombietType
	{
		NormalZombie = 0,
		ConeZombie = 2,
		PolevaulterZombie = 3,
		BucketZombie = 4,
		PaperZombie = 5,
		DancePolZombie = 6,
		DancePolZombie2 = 7,
		DoorZombie = 8,
		FootballZombie = 9,
		JacksonZombie = 10,
		ZombieDuck = 11,
		ConeZombieDuck = 12,
		BucketZombieDuck = 13,
		SubmarineZombie = 14,
		ElitePaperZombie = 15,
		DriverZombie = 16,
		SnorkleZombie = 17,
		SuperDriver = 18,
		Dolphinrider = 19,
		DrownZombie = 20,
		DollDiamond = 21,
		DollGold = 22,
		DollSilver = 23,
		PeaShooterZombie = 100,
		CherryShooterZombie = 101,
		SuperCherryShooterZombie = 102,
		WallNutZombie = 103,
		CherryPaperZombie = 104,
		RandomZombie = 105,
		BucketNutZombie = 106,
		CherryNutZombie = 107,
		IronPeaZzombie = 108,
		TallNutFootballZombie = 109,
		RandomPlusZombie = 110,
		TallIceNutZombie = 111,
		SuperSubmarine = 200,
		JacksonDriver = 201,
		FootballDrown = 202,
		CherryPaperZ95 = 203
	}

	public enum ChallengeLevelName
	{
		Travel1 = 1,
		Travel2 = 2,
		Travel3 = 3,
		Travel4 = 4,
		Travel5 = 5,
		Travel6 = 6,
		SuperCherryShooter1 = 7,
		SuperCherryShooter2 = 8,
		SuperCherryShooter3 = 9,
		SuperChomper1 = 10,
		SuperChomper2 = 11,
		SuperChomper3 = 12,
		FlagDay = 13,
		FlagPlantZombie = 14,
		FlagRandomPlant = 15,
		FlagRandomZombie = 16,
		FlagRandomAll = 17,
		FlagNight = 18,
		SuperHypno1 = 19,
		SuperHypno2 = 20,
		SuperHypno3 = 21,
		SuperFume1 = 22,
		SuperFume2 = 23,
		SuperFume3 = 24,
		ScaredDream = 25,
		FlagDream = 26,
		FlagElite = 27,
		PolDance = 28,
		PuffTime = 29,
		PaperBattle = 30,
		SuperTorch = 31,
		SuperKelp = 32,
		FlagPool = 33,
		DriverBattle = 34,
		TowerDefense = 35
	}

	public enum ZombieStatus
	{
		Default = 0,
		Dying = 1,
		Pol_run = 2,
		Pol_jump = 3,
		Paper_lookPaper = 4,
		Paper_losePaper = 5,
		Paper_angry = 6,
		Snokle_inWater = 7,
		Dolphinrider_fast = 8,
		Dolphinrider_jump = 9
	}

	private static int multiplier = 1;

	public static int theMaxWave;

	public static int[,] zombieList = new int[50, 101];

	public static int[] zombieTypeList = new int[64];

	private static bool[] allowZombieTypeSpawn = new bool[256];

	private static int zombiePoint;

	private static readonly Dictionary<int, int> zombieWeights = new Dictionary<int, int>
	{
		{ 0, 4000 },
		{ 2, 3000 },
		{ 4, 2000 },
		{ 8, 2000 },
		{ 5, 2000 },
		{ 3, 2000 },
		{ 17, 1500 },
		{ 100, 1500 },
		{ 19, 1000 },
		{ 9, 1000 },
		{ 101, 1000 },
		{ 103, 1000 },
		{ 107, 750 },
		{ 20, 750 },
		{ 6, 750 },
		{ 16, 750 },
		{ 105, 750 },
		{ 14, 500 },
		{ 18, 500 },
		{ 111, 500 },
		{ 108, 500 },
		{ 106, 500 },
		{ 104, 300 },
		{ 110, 300 },
		{ 15, 300 },
		{ 109, 300 },
		{ 10, 300 },
		{ 21, 300 },
		{ 200, 1000 },
		{ 201, 1000 },
		{ 202, 1000 },
		{ 203, 1000 }
	};

	private static readonly List<int> zombieInLandNormal = new List<int>
	{
		2, 4, 8, 5, 3, 100, 9, 101, 103, 107,
		16, 111, 108, 106
	};

	private static readonly List<int> zombieInLandHard = new List<int>
	{
		4, 9, 101, 103, 107, 20, 6, 16, 18, 111,
		108, 106, 104, 15, 109, 10, 21
	};

	private static readonly List<int> zombieInPoolNormal = new List<int>
	{
		2, 4, 8, 5, 3, 100, 9, 101, 103, 107,
		16, 111, 108, 106, 17, 19
	};

	private static readonly List<int> zombieInPoolHard = new List<int>
	{
		4, 9, 17, 19, 14, 101, 103, 107, 20, 6,
		16, 18, 111, 108, 106, 104, 15, 109, 10, 21
	};

	private static readonly List<int> zombieInTravel = new List<int> { 200, 201, 202, 203 };

	private static List<int> GetRandomZombiesFromLandNormal()
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>(zombieInLandNormal);
		for (int i = 0; i < 5; i++)
		{
			if (list2.Count <= 0)
			{
				break;
			}
			int index = Random.Range(0, list2.Count);
			list.Add(list2[index]);
			list2.RemoveAt(index);
		}
		return list;
	}

	private static List<int> GetRandomZombiesFromLandHard()
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>(zombieInLandHard);
		int num = Random.Range(7, 11);
		for (int i = 0; i < num; i++)
		{
			if (list2.Count <= 0)
			{
				break;
			}
			int index = Random.Range(0, list2.Count);
			list.Add(list2[index]);
			list2.RemoveAt(index);
		}
		return list;
	}

	private static List<int> GetRandomZombiesFromPoolNormal()
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>(zombieInPoolNormal);
		for (int i = 0; i < 5; i++)
		{
			if (list2.Count <= 0)
			{
				break;
			}
			int index = Random.Range(0, list2.Count);
			list.Add(list2[index]);
			list2.RemoveAt(index);
		}
		return list;
	}

	private static List<int> GetRandomZombiesFromPoolHard()
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>(zombieInPoolHard);
		int num = Random.Range(7, 11);
		for (int i = 0; i < num; i++)
		{
			if (list2.Count <= 0)
			{
				break;
			}
			int index = Random.Range(0, list2.Count);
			list.Add(list2[index]);
			list2.RemoveAt(index);
		}
		return list;
	}

	private static List<int> GetRandomZombiesFromTravel()
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>(zombieInTravel);
		for (int i = 0; i < 2; i++)
		{
			if (list2.Count <= 0)
			{
				break;
			}
			int index = Random.Range(0, list2.Count);
			list.Add(list2[index]);
			list2.RemoveAt(index);
		}
		return list;
	}

	public static void InitZombie(int theLevelType, int theLevelNumber, int theSurvivalRound = 0)
	{
		InitList();
		if (GameAPP.difficulty == 5)
		{
			if (theLevelType == 0 && (theLevelNumber == 0 || theLevelNumber == 1))
			{
				multiplier = 3;
			}
			else
			{
				multiplier = 4;
			}
		}
		else
		{
			multiplier = GameAPP.difficulty;
		}
		if (theSurvivalRound == 0)
		{
			SetAllowZombieTypeSpawn(theLevelType, theLevelNumber);
		}
		else
		{
			SurvivalZombieTypeSpawn(theLevelNumber, theSurvivalRound);
		}
		theMaxWave = 10;
		zombiePoint = 0;
		switch (theLevelType)
		{
		case 0:
			InitAdvWave(theLevelNumber);
			break;
		case 1:
			InitChallengeWave(theLevelNumber);
			break;
		case 3:
			InitSurvivalWave(theLevelNumber);
			break;
		}
		for (int i = 1; i <= theMaxWave; i++)
		{
			if (theLevelType == 3)
			{
				zombiePoint = (i + (theSurvivalRound - 1) * 10) * multiplier;
			}
			else
			{
				zombiePoint = i * multiplier;
			}
			while (zombiePoint > 0)
			{
				bool flag = false;
				int num;
				do
				{
					num = PickZombie();
					if (i < 10)
					{
						if (GameAPP.difficulty == 5)
						{
							break;
						}
						switch (num)
						{
						case 6:
						case 10:
						case 15:
						case 104:
						case 106:
						case 108:
						case 109:
							flag = true;
							break;
						default:
							flag = false;
							break;
						}
					}
				}
				while (flag);
				int num2 = AddZombieToList(num, i);
				zombiePoint -= num2;
			}
			if (i != theMaxWave)
			{
				continue;
			}
			for (int j = 0; j < allowZombieTypeSpawn.Length; j++)
			{
				if (allowZombieTypeSpawn[j])
				{
					zombiePoint = 20;
					AddZombieToList(j, i);
				}
			}
			zombiePoint = -1;
		}
	}

	private static void InitList()
	{
		for (int i = 0; i < zombieList.GetLength(0); i++)
		{
			for (int j = 0; j < zombieList.GetLength(1); j++)
			{
				zombieList[i, j] = -1;
			}
		}
		for (int k = 0; k < zombieTypeList.Length; k++)
		{
			zombieTypeList[k] = -1;
		}
		for (int l = 0; l < allowZombieTypeSpawn.Length; l++)
		{
			allowZombieTypeSpawn[l] = false;
		}
	}

	private static int PickZombie()
	{
		int num = 0;
		foreach (KeyValuePair<int, int> zombieWeight in zombieWeights)
		{
			if (allowZombieTypeSpawn[zombieWeight.Key])
			{
				num += zombieWeight.Value;
			}
		}
		int num2 = Random.Range(1, num + 1);
		int num3 = 0;
		foreach (KeyValuePair<int, int> zombieWeight2 in zombieWeights)
		{
			if (allowZombieTypeSpawn[zombieWeight2.Key])
			{
				num3 += zombieWeight2.Value;
				if (num2 <= num3)
				{
					return zombieWeight2.Key;
				}
			}
		}
		return 0;
	}

	private static int AddZombieToList(int zombieType, int wave)
	{
		int num = 1;
		switch (zombieType)
		{
		case 0:
			num = 1;
			break;
		case 2:
		case 100:
			num = 2;
			break;
		case 3:
		case 8:
		case 17:
		case 101:
			num = 3;
			break;
		case 4:
		case 5:
		case 9:
		case 19:
		case 103:
		case 105:
			num = 4;
			break;
		case 16:
		case 106:
		case 107:
		case 108:
		case 110:
		case 111:
			num = 5;
			break;
		case 6:
		case 14:
		case 15:
		case 18:
		case 20:
		case 21:
			num = 6;
			break;
		case 10:
		case 104:
		case 109:
			num = 7;
			break;
		case 200:
		case 201:
		case 202:
		case 203:
			num = 5;
			break;
		default:
			num = 1;
			break;
		}
		if (num > zombiePoint)
		{
			zombieType = 0;
			if (GameAPP.theBoardType == 1)
			{
				switch (GameAPP.theBoardLevel)
				{
				case 16:
				case 17:
					zombieType = 105;
					break;
				case 8:
				case 11:
				case 14:
					zombieType = 100;
					break;
				}
			}
		}
		if (ContainsType(zombieType, zombieTypeList))
		{
			for (int i = 0; i < zombieTypeList.Length; i++)
			{
				if (zombieTypeList[i] == -1)
				{
					zombieTypeList[i] = zombieType;
					break;
				}
			}
		}
		for (int j = 0; j < zombieList.GetLength(0); j++)
		{
			if (zombieList[j, wave] == -1)
			{
				zombieList[j, wave] = zombieType;
				break;
			}
		}
		return num;
	}

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
			theMaxWave = 10;
			break;
		case 4:
		case 5:
		case 6:
		case 13:
		case 14:
		case 15:
		case 21:
		case 22:
			theMaxWave = 20;
			break;
		case 7:
		case 8:
		case 9:
		case 16:
		case 17:
		case 18:
		case 23:
		case 24:
			theMaxWave = 30;
			break;
		case 25:
		case 26:
		case 27:
			theMaxWave = 40;
			break;
		}
	}

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
			theMaxWave = 40;
			break;
		case 7:
		case 8:
		case 10:
		case 11:
		case 19:
		case 20:
		case 22:
		case 23:
			theMaxWave = 30;
			break;
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
			theMaxWave = 40;
			break;
		case 13:
		case 14:
		case 15:
		case 16:
		case 17:
		case 18:
		case 26:
		case 27:
		case 33:
			theMaxWave = 100;
			break;
		default:
			theMaxWave = 20;
			break;
		}
	}

	private static void InitSurvivalWave(int theLevelNumber)
	{
		switch (theLevelNumber)
		{
		case 1:
		case 2:
		case 3:
			theMaxWave = 10;
			Board.Instance.theSurvivalMaxRound = 5;
			break;
		case 4:
		case 5:
		case 6:
		case 7:
			theMaxWave = 20;
			Board.Instance.theSurvivalMaxRound = int.MaxValue;
			break;
		case 8:
			theMaxWave = 20;
			Board.Instance.theSurvivalMaxRound = 9;
			break;
		default:
			theMaxWave = 10;
			Board.Instance.theSurvivalMaxRound = 5;
			break;
		}
	}

	private static void SurvivalZombieTypeSpawn(int theLevelNumber, int theRound)
	{
		if (Board.Instance.isTravel && Board.Instance.theCurrentSurvivalRound > 6)
		{
			PoolHard();
			if (GameAPP.hardZombie)
			{
				Travel();
			}
			return;
		}
		if (theRound == 1)
		{
			allowZombieTypeSpawn[0] = true;
			allowZombieTypeSpawn[2] = true;
			allowZombieTypeSpawn[4] = true;
			return;
		}
		allowZombieTypeSpawn[4] = true;
		switch (theLevelNumber)
		{
		case 1:
		case 2:
			LandNormal();
			break;
		case 3:
			PoolNormal();
			break;
		case 4:
		case 5:
			LandHard();
			break;
		case 6:
		case 7:
			PoolHard();
			break;
		case 8:
			LandHard();
			if (GameAPP.hardZombie)
			{
				Travel();
				allowZombieTypeSpawn[200] = false;
			}
			break;
		default:
			LandHard();
			break;
		}
	}

	private static void LandNormal()
	{
		foreach (int item in GetRandomZombiesFromLandNormal())
		{
			allowZombieTypeSpawn[item] = true;
		}
	}

	private static void LandHard()
	{
		foreach (int item in GetRandomZombiesFromLandHard())
		{
			allowZombieTypeSpawn[item] = true;
		}
	}

	private static void PoolNormal()
	{
		foreach (int item in GetRandomZombiesFromPoolNormal())
		{
			allowZombieTypeSpawn[item] = true;
		}
	}

	private static void PoolHard()
	{
		foreach (int item in GetRandomZombiesFromPoolHard())
		{
			allowZombieTypeSpawn[item] = true;
		}
	}

	private static void Travel()
	{
		foreach (int item in GetRandomZombiesFromTravel())
		{
			allowZombieTypeSpawn[item] = true;
		}
	}

	private static void SetAllowZombieTypeSpawn(int theLevelType, int theLevelNumber)
	{
		if (theLevelType == 0)
		{
			switch (theLevelNumber)
			{
			case 1:
				allowZombieTypeSpawn[0] = true;
				break;
			case 2:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				break;
			case 3:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[4] = true;
				break;
			case 4:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[3] = true;
				break;
			case 5:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[4] = true;
				allowZombieTypeSpawn[100] = true;
				break;
			case 6:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[4] = true;
				allowZombieTypeSpawn[21] = true;
				break;
			case 7:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[4] = true;
				allowZombieTypeSpawn[21] = true;
				allowZombieTypeSpawn[5] = true;
				break;
			case 8:
				allowZombieTypeSpawn[106] = true;
				allowZombieTypeSpawn[4] = true;
				allowZombieTypeSpawn[108] = true;
				allowZombieTypeSpawn[8] = true;
				break;
			case 9:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[3] = true;
				allowZombieTypeSpawn[106] = true;
				allowZombieTypeSpawn[21] = true;
				allowZombieTypeSpawn[104] = true;
				allowZombieTypeSpawn[107] = true;
				allowZombieTypeSpawn[4] = true;
				break;
			case 10:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				break;
			case 11:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[8] = true;
				break;
			case 12:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[5] = true;
				allowZombieTypeSpawn[9] = true;
				break;
			case 13:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[5] = true;
				allowZombieTypeSpawn[9] = true;
				allowZombieTypeSpawn[3] = true;
				break;
			case 14:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[4] = true;
				allowZombieTypeSpawn[5] = true;
				allowZombieTypeSpawn[9] = true;
				allowZombieTypeSpawn[3] = true;
				break;
			case 15:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[4] = true;
				allowZombieTypeSpawn[5] = true;
				allowZombieTypeSpawn[9] = true;
				allowZombieTypeSpawn[6] = true;
				break;
			case 16:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[4] = true;
				allowZombieTypeSpawn[5] = true;
				allowZombieTypeSpawn[9] = true;
				allowZombieTypeSpawn[6] = true;
				allowZombieTypeSpawn[10] = true;
				break;
			case 17:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[4] = true;
				allowZombieTypeSpawn[5] = true;
				allowZombieTypeSpawn[109] = true;
				allowZombieTypeSpawn[6] = true;
				allowZombieTypeSpawn[10] = true;
				break;
			case 18:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[4] = true;
				allowZombieTypeSpawn[5] = true;
				allowZombieTypeSpawn[109] = true;
				allowZombieTypeSpawn[21] = true;
				allowZombieTypeSpawn[111] = true;
				allowZombieTypeSpawn[8] = true;
				allowZombieTypeSpawn[9] = true;
				break;
			case 19:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				break;
			case 20:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[9] = true;
				break;
			case 21:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[4] = true;
				allowZombieTypeSpawn[17] = true;
				break;
			case 22:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[3] = true;
				allowZombieTypeSpawn[5] = true;
				allowZombieTypeSpawn[14] = true;
				break;
			case 23:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[4] = true;
				allowZombieTypeSpawn[5] = true;
				allowZombieTypeSpawn[14] = true;
				allowZombieTypeSpawn[16] = true;
				break;
			case 24:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[4] = true;
				allowZombieTypeSpawn[8] = true;
				allowZombieTypeSpawn[14] = true;
				allowZombieTypeSpawn[18] = true;
				break;
			case 25:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[4] = true;
				allowZombieTypeSpawn[8] = true;
				allowZombieTypeSpawn[14] = true;
				allowZombieTypeSpawn[18] = true;
				allowZombieTypeSpawn[20] = true;
				break;
			case 26:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[4] = true;
				allowZombieTypeSpawn[16] = true;
				allowZombieTypeSpawn[14] = true;
				allowZombieTypeSpawn[18] = true;
				allowZombieTypeSpawn[20] = true;
				break;
			case 27:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[19] = true;
				allowZombieTypeSpawn[16] = true;
				allowZombieTypeSpawn[14] = true;
				allowZombieTypeSpawn[15] = true;
				allowZombieTypeSpawn[20] = true;
				allowZombieTypeSpawn[18] = true;
				allowZombieTypeSpawn[17] = true;
				break;
			default:
				AllowAll();
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
				LandHard();
				break;
			case 5:
				PoolHard();
				break;
			case 6:
				PoolHard();
				allowZombieTypeSpawn[203] = true;
				allowZombieTypeSpawn[200] = true;
				allowZombieTypeSpawn[202] = true;
				allowZombieTypeSpawn[201] = true;
				break;
			case 16:
			case 17:
				allowZombieTypeSpawn[105] = true;
				allowZombieTypeSpawn[110] = true;
				break;
			case 7:
			case 10:
				AllowDayNormal();
				break;
			case 8:
			case 11:
			case 14:
				AllowPlantZombie();
				break;
			case 9:
			case 12:
			case 13:
				AllowDay();
				break;
			case 19:
			case 22:
			case 29:
				AllowNightNormal();
				break;
			case 20:
			case 23:
			case 27:
				AllowEliteNight();
				break;
			case 18:
			case 21:
			case 24:
				AllowNight();
				break;
			case 28:
				allowZombieTypeSpawn[3] = true;
				allowZombieTypeSpawn[6] = true;
				allowZombieTypeSpawn[10] = true;
				break;
			case 30:
				allowZombieTypeSpawn[5] = true;
				allowZombieTypeSpawn[104] = true;
				allowZombieTypeSpawn[15] = true;
				break;
			case 31:
				allowZombieTypeSpawn[4] = true;
				allowZombieTypeSpawn[106] = true;
				allowZombieTypeSpawn[9] = true;
				allowZombieTypeSpawn[111] = true;
				allowZombieTypeSpawn[109] = true;
				allowZombieTypeSpawn[15] = true;
				break;
			case 32:
				allowZombieTypeSpawn[14] = true;
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[4] = true;
				allowZombieTypeSpawn[17] = true;
				allowZombieTypeSpawn[19] = true;
				break;
			case 33:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[4] = true;
				allowZombieTypeSpawn[17] = true;
				allowZombieTypeSpawn[9] = true;
				allowZombieTypeSpawn[14] = true;
				allowZombieTypeSpawn[15] = true;
				allowZombieTypeSpawn[16] = true;
				allowZombieTypeSpawn[18] = true;
				allowZombieTypeSpawn[20] = true;
				allowZombieTypeSpawn[19] = true;
				break;
			case 34:
				allowZombieTypeSpawn[16] = true;
				allowZombieTypeSpawn[18] = true;
				allowZombieTypeSpawn[14] = true;
				break;
			case 35:
				allowZombieTypeSpawn[0] = true;
				allowZombieTypeSpawn[2] = true;
				allowZombieTypeSpawn[4] = true;
				allowZombieTypeSpawn[8] = true;
				allowZombieTypeSpawn[5] = true;
				allowZombieTypeSpawn[15] = true;
				allowZombieTypeSpawn[9] = true;
				allowZombieTypeSpawn[109] = true;
				allowZombieTypeSpawn[20] = true;
				allowZombieTypeSpawn[16] = true;
				allowZombieTypeSpawn[18] = true;
				allowZombieTypeSpawn[106] = true;
				allowZombieTypeSpawn[111] = true;
				allowZombieTypeSpawn[107] = true;
				break;
			default:
				AllowAll();
				break;
			}
		}
	}

	private static void AllowAll()
	{
		allowZombieTypeSpawn[0] = true;
		allowZombieTypeSpawn[2] = true;
		allowZombieTypeSpawn[3] = true;
		allowZombieTypeSpawn[4] = true;
		allowZombieTypeSpawn[5] = true;
		allowZombieTypeSpawn[6] = true;
		allowZombieTypeSpawn[8] = true;
		allowZombieTypeSpawn[9] = true;
		allowZombieTypeSpawn[10] = true;
		allowZombieTypeSpawn[11] = true;
		allowZombieTypeSpawn[12] = true;
		allowZombieTypeSpawn[13] = true;
		allowZombieTypeSpawn[15] = true;
		allowZombieTypeSpawn[100] = true;
		allowZombieTypeSpawn[101] = true;
		allowZombieTypeSpawn[103] = true;
		allowZombieTypeSpawn[104] = true;
		allowZombieTypeSpawn[105] = true;
		allowZombieTypeSpawn[106] = true;
		allowZombieTypeSpawn[107] = true;
		allowZombieTypeSpawn[108] = true;
		allowZombieTypeSpawn[109] = true;
		allowZombieTypeSpawn[110] = true;
		allowZombieTypeSpawn[111] = true;
	}

	private static void AllowDay()
	{
		allowZombieTypeSpawn[0] = true;
		allowZombieTypeSpawn[2] = true;
		allowZombieTypeSpawn[3] = true;
		allowZombieTypeSpawn[5] = true;
		allowZombieTypeSpawn[4] = true;
		allowZombieTypeSpawn[104] = true;
		allowZombieTypeSpawn[106] = true;
		allowZombieTypeSpawn[108] = true;
		allowZombieTypeSpawn[100] = true;
		allowZombieTypeSpawn[101] = true;
		allowZombieTypeSpawn[107] = true;
		allowZombieTypeSpawn[103] = true;
	}

	private static void AllowDayNormal()
	{
		allowZombieTypeSpawn[0] = true;
		allowZombieTypeSpawn[2] = true;
		allowZombieTypeSpawn[3] = true;
		allowZombieTypeSpawn[4] = true;
		allowZombieTypeSpawn[5] = true;
		allowZombieTypeSpawn[100] = true;
		allowZombieTypeSpawn[101] = true;
		allowZombieTypeSpawn[103] = true;
	}

	private static void AllowNightNormal()
	{
		allowZombieTypeSpawn[0] = true;
		allowZombieTypeSpawn[2] = true;
		allowZombieTypeSpawn[3] = true;
		allowZombieTypeSpawn[4] = true;
		allowZombieTypeSpawn[8] = true;
		allowZombieTypeSpawn[9] = true;
		allowZombieTypeSpawn[111] = true;
	}

	private static void AllowNight()
	{
		allowZombieTypeSpawn[0] = true;
		allowZombieTypeSpawn[2] = true;
		allowZombieTypeSpawn[3] = true;
		allowZombieTypeSpawn[100] = true;
		allowZombieTypeSpawn[103] = true;
		allowZombieTypeSpawn[4] = true;
		allowZombieTypeSpawn[9] = true;
		allowZombieTypeSpawn[8] = true;
		allowZombieTypeSpawn[6] = true;
		allowZombieTypeSpawn[10] = true;
		allowZombieTypeSpawn[104] = true;
		allowZombieTypeSpawn[111] = true;
		allowZombieTypeSpawn[109] = true;
	}

	private static void AllowEliteNight()
	{
		allowZombieTypeSpawn[104] = true;
		allowZombieTypeSpawn[108] = true;
		allowZombieTypeSpawn[106] = true;
		allowZombieTypeSpawn[109] = true;
		allowZombieTypeSpawn[111] = true;
		allowZombieTypeSpawn[6] = true;
		allowZombieTypeSpawn[10] = true;
	}

	private static void AllowPlantZombie()
	{
		allowZombieTypeSpawn[100] = true;
		allowZombieTypeSpawn[101] = true;
		allowZombieTypeSpawn[103] = true;
		allowZombieTypeSpawn[104] = true;
		allowZombieTypeSpawn[106] = true;
		allowZombieTypeSpawn[107] = true;
		allowZombieTypeSpawn[108] = true;
		allowZombieTypeSpawn[109] = true;
		allowZombieTypeSpawn[111] = true;
	}
}

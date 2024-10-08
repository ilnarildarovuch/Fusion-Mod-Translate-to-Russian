using System;
using UnityEngine;

public class MixData
{
	public enum PlantType
	{
		Peashooter = 0,
		SunFlower = 1,
		CherryBomb = 2,
		WallNut = 3,
		PotatoMine = 4,
		Chomper = 5,
		SmallPuff = 6,
		FumeShroom = 7,
		HypnoShroom = 8,
		ScaredyShroom = 9,
		IceShroom = 10,
		DoomShroom = 11,
		LilyPad = 12,
		Squash = 13,
		ThreePeater = 14,
		Tanglekelp = 15,
		Jalapeno = 16,
		Caltrop = 17,
		TorchWood = 18,
		CattailGirl = 252,
		Wheat = 253,
		EndoFlame = 254,
		BigWallNut = 255,
		Present = 256,
		HyponoEmperor = 900,
		SuperCherryGatling = 901,
		JalaSquashTorch = 902,
		SuperCherryChomper = 903,
		FinalFume = 904,
		PeaSunFlower = 1000,
		Cherryshooter = 1001,
		SunBomb = 1002,
		CherryNut = 1003,
		PeaNut = 1004,
		SuperCherryShooter = 1005,
		SunNut = 1006,
		PeaMine = 1007,
		DoubleCherry = 1008,
		SunMine = 1009,
		PotatoNut = 1010,
		PeaChomper = 1011,
		NutChomper = 1012,
		SuperChomper = 1013,
		SunChomper = 1014,
		PotatoChomper = 1015,
		CherryChomper = 1016,
		CherryGatling = 1017,
		PeaSmallPuff = 1018,
		DoublePuff = 1019,
		IronPea = 1020,
		PuffNut = 1021,
		HypnoPuff = 1022,
		HypnoFume = 1023,
		ScaredyHypno = 1024,
		ScaredFume = 1025,
		SuperHypno = 1026,
		TallNut = 1027,
		TallNutFootball = 1028,
		IronNut = 1029,
		DoubleShooer = 1030,
		SunShroom = 1031,
		GatlingPea = 1032,
		TwinFlower = 1033,
		SnowPeaShooter = 1034,
		IcePuff = 1035,
		SmallIceShroom = 1036,
		IceFumeShroom = 1037,
		IceScaredyShroom = 1038,
		TallIceNut = 1039,
		IceDoom = 1040,
		IceHypno = 1041,
		ScaredyDoom = 1042,
		DoomFume = 1043,
		PuffDoom = 1044,
		HypnoDoom = 1045,
		IceDoomFume = 1046,
		ThreeSquash = 1047,
		EliteTorchWood = 1048,
		Jalakelp = 1049,
		Squashkelp = 1050,
		Threekelp = 1051,
		SuperTorch = 1052,
		JalaTorch = 1053,
		JalaSquash = 1054,
		ThreeTorch = 1055,
		KelpTorch = 1056,
		FireSquash = 1057,
		DarkThreePeater = 1058,
		SquashTorch = 1059,
		SpikeRock = 1060,
		FireSpike = 1061,
		JalaSpike = 1062,
		SquashSpike = 1063,
		ThreeSpike = 1064,
		GatlingPuff = 1065,
		SuperKelp = 1066,
		CattailPlant = 1067,
		IceCattail = 1068,
		FireCattail = 1069,
		GloomShroom = 1070,
		FireGloom = 1071,
		IceGloom = 1072,
		TallFireNut = 1073,
		IceSpikeRock = 1074,
		FireSpikeRock = 1075
	}

	public static int[,] data = new int[2048, 2048];

	public static void InitMixData()
	{
		InitTravel();
		SpecialPlant();
		data[0, 0] = 1030;
		data[3, 3] = 255;
		data[1030, 1030] = 1032;
		data[1, 1] = 1033;
		data[2, 4] = 4;
		data[4, 2] = 4;
		data[0, 1] = 1000;
		data[1, 0] = 1000;
		data[0, 2] = 1001;
		data[2, 0] = 1001;
		data[1, 2] = 1002;
		data[2, 1] = 1002;
		data[2, 3] = 1003;
		data[3, 2] = 1003;
		data[0, 3] = 1004;
		data[3, 0] = 1004;
		data[1001, 2] = 1005;
		data[2, 1001] = 1005;
		data[1, 3] = 1006;
		data[3, 1] = 1006;
		data[0, 4] = 1007;
		data[4, 0] = 1007;
		data[1030, 2] = 1008;
		data[2, 1030] = 1008;
		data[1001, 0] = 1008;
		data[0, 1001] = 1008;
		data[1, 4] = 1009;
		data[4, 1] = 1009;
		data[3, 4] = 1010;
		data[4, 3] = 1010;
		data[0, 5] = 1011;
		data[5, 0] = 1011;
		data[3, 5] = 1012;
		data[5, 3] = 1012;
		data[1012, 0] = 1013;
		data[0, 1012] = 1013;
		data[1011, 3] = 1013;
		data[3, 1011] = 1013;
		data[1004, 5] = 1013;
		data[5, 1004] = 1013;
		data[1, 5] = 1014;
		data[5, 1] = 1014;
		data[5, 4] = 1015;
		data[4, 5] = 1015;
		data[5, 2] = 1016;
		data[2, 5] = 1016;
		data[1032, 2] = 1017;
		data[2, 1032] = 1017;
		data[1008, 1030] = 1017;
		data[1030, 1008] = 1017;
		data[6, 1] = 1031;
		data[6, 0] = 1018;
		data[6, 1030] = 1019;
		data[6, 3] = 1021;
		data[6, 7] = 6;
		data[6, 9] = 6;
		data[6, 8] = 1022;
		data[6, 10] = 1036;
		data[6, 11] = 1044;
		data[6, 1032] = 1065;
		data[7, 8] = 1023;
		data[8, 7] = 1023;
		data[8, 9] = 1024;
		data[9, 8] = 1024;
		data[7, 9] = 1025;
		data[9, 7] = 1025;
		data[1025, 8] = 1026;
		data[8, 1025] = 1026;
		data[1024, 7] = 1026;
		data[7, 1024] = 1026;
		data[1023, 9] = 1026;
		data[9, 1023] = 1026;
		data[10, 0] = 1034;
		data[0, 10] = 1034;
		data[1034, 6] = 1035;
		data[6, 1034] = 1035;
		data[10, 7] = 1037;
		data[7, 10] = 1037;
		data[10, 9] = 1038;
		data[9, 10] = 1038;
		data[10, 1027] = 1039;
		data[1027, 10] = 1039;
		data[10, 8] = 1041;
		data[8, 10] = 1041;
		data[10, 11] = 1040;
		data[11, 10] = 1040;
		data[7, 11] = 1043;
		data[11, 7] = 1043;
		data[9, 11] = 1042;
		data[11, 9] = 1042;
		data[8, 11] = 1045;
		data[11, 8] = 1045;
		data[1040, 7] = 1046;
		data[7, 1040] = 1046;
		data[1037, 11] = 1046;
		data[11, 1037] = 1046;
		data[1043, 10] = 1046;
		data[10, 1043] = 1046;
		data[1070, 16] = 1071;
		data[1070, 10] = 1072;
		data[1037, 1070] = 1072;
		data[1070, 1037] = 1072;
		data[14, 13] = 1047;
		data[13, 14] = 1047;
		data[14, 18] = 1055;
		data[18, 14] = 1055;
		data[13, 16] = 1054;
		data[16, 13] = 1054;
		data[14, 16] = 1058;
		data[16, 14] = 1058;
		data[13, 18] = 1059;
		data[18, 13] = 1059;
		data[18, 16] = 1053;
		data[16, 18] = 1053;
		data[16, 1053] = 1052;
		data[1053, 16] = 1052;
		data[15, 16] = 1049;
		data[15, 13] = 1050;
		data[15, 14] = 1051;
		data[15, 18] = 1056;
		data[17, 18] = 1061;
		data[18, 17] = 1061;
		data[17, 16] = 1062;
		data[16, 17] = 1062;
		data[17, 13] = 1063;
		data[13, 17] = 1063;
		data[17, 14] = 1064;
		data[14, 17] = 1064;
		data[1050, 14] = 1066;
		data[1051, 13] = 1066;
		data[15, 1047] = 1066;
		data[1067, 10] = 1068;
		data[10, 1067] = 1068;
		data[1067, 16] = 1069;
		data[16, 1067] = 1069;
		data[1027, 16] = 1073;
		data[16, 1027] = 1073;
		data[1060, 16] = 1075;
		data[16, 1060] = 1075;
		data[1062, 1060] = 1075;
		data[1060, 10] = 1074;
		data[10, 1060] = 1074;
	}

	private static void InitTravel()
	{
		data[8, 8] = 900;
		data[1017, 1005] = 901;
		data[1005, 1017] = 901;
		data[1059, 1054] = 902;
		data[1054, 1059] = 902;
		data[1013, 1016] = 903;
		data[1016, 1013] = 903;
		data[1046, 1026] = 904;
		data[1026, 1046] = 904;
	}

	private static void SpecialPlant()
	{
		data[3, 1027] = 1027;
		data[17, 1060] = 1060;
		data[7, 1070] = 1070;
		data[12, 1067] = 1067;
	}

	public static void SetPlants(int level)
	{
		switch (level)
		{
		case 2:
			SetPlantsInLv2();
			break;
		case 3:
			SetPlantsInLv3();
			break;
		case 4:
			SetPlantsInLv4();
			break;
		case 5:
			SetPlantsInLv5();
			break;
		case 6:
			SetPlantsInLv6();
			break;
		case 7:
			SetPlantsInLv7();
			break;
		case 8:
			SetPlantsInLv8();
			break;
		case 9:
			SetPlantsInLv9();
			break;
		case 10:
			SetPlantsInLv10();
			break;
		case 11:
			SetPlantsInLv11();
			break;
		case 12:
			SetPlantsInLv12();
			break;
		case 13:
			SetPlantsInLv13();
			break;
		case 14:
			SetPlantsInLv14();
			break;
		case 15:
			SetPlantsInLv15();
			break;
		case 16:
			SetPlantsInLv16();
			break;
		case 17:
			SetPlantsInLv17();
			break;
		case 18:
			SetPlantsInLv18();
			break;
		}
	}

	private static int GetRandomNumberInNumbers(int[] numbers)
	{
		int num = new System.Random().Next(numbers.Length);
		return numbers[num];
	}

	private static void SetPlantsInLv2()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[6] { 1030, 1032, 0, 1034, 1030, 1020 };
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				component.SetPlant(i, j, GetRandomNumberInNumbers(numbers));
			}
		}
	}

	private static void SetPlantsInLv3()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[6] { 1016, 1012, 1011, 1015, 1034, 1030 };
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				if (i == 0)
				{
					component.SetPlant(i, j, 1005);
				}
				else
				{
					component.SetPlant(i, j, GetRandomNumberInNumbers(numbers));
				}
			}
		}
	}

	private static void SetPlantsInLv4()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[6] { 1015, 4, 1010, 1007, 5, 1016 };
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				PotatoMine component2;
				if (i == 0)
				{
					component.SetPlant(i, j, 1013);
				}
				else if (component.SetPlant(i, j, GetRandomNumberInNumbers(numbers)).TryGetComponent<PotatoMine>(out component2))
				{
					component2.attributeCountdown = 0f;
				}
			}
		}
	}

	private static void SetPlantsInLv5()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[6] { 1012, 1003, 1029, 1004, 3, 1034 };
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				component.SetPlant(i, j, GetRandomNumberInNumbers(numbers));
			}
		}
	}

	private static void SetPlantsInLv6()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[10] { 1001, 1008, 1017, 1003, 1010, 1013, 1034, 1029, 1020, 1016 };
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				component.SetPlant(i, j, GetRandomNumberInNumbers(numbers));
			}
		}
	}

	private static void SetPlantsInLv7()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[6] { 1021, 1022, 1019, 1018, 6, 1036 };
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				int randomNumberInNumbers = GetRandomNumberInNumbers(numbers);
				component.SetPlant(i, j, randomNumberInNumbers);
				if (GameAPP.board.GetComponent<CreatePlant>().IsPuff(randomNumberInNumbers))
				{
					component.SetPlant(i, j, randomNumberInNumbers);
					component.SetPlant(i, j, randomNumberInNumbers);
				}
			}
		}
	}

	private static void SetPlantsInLv8()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[6] { 7, 1023, 1037, 1025, 6, 1018 };
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				int randomNumberInNumbers = GetRandomNumberInNumbers(numbers);
				component.SetPlant(i, j, randomNumberInNumbers);
				if (GameAPP.board.GetComponent<CreatePlant>().IsPuff(randomNumberInNumbers))
				{
					component.SetPlant(i, j, randomNumberInNumbers);
					component.SetPlant(i, j, randomNumberInNumbers);
				}
			}
		}
	}

	private static void SetPlantsInLv9()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[7] { 1022, 8, 1041, 1024, 7, 1021, 1018 };
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				int randomNumberInNumbers = GetRandomNumberInNumbers(numbers);
				component.SetPlant(i, j, randomNumberInNumbers);
				if (GameAPP.board.GetComponent<CreatePlant>().IsPuff(randomNumberInNumbers))
				{
					component.SetPlant(i, j, randomNumberInNumbers);
					component.SetPlant(i, j, randomNumberInNumbers);
				}
			}
		}
	}

	private static void SetPlantsInLv10()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[7] { 1025, 1024, 9, 1026, 1038, 7, 1018 };
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				int randomNumberInNumbers = GetRandomNumberInNumbers(numbers);
				component.SetPlant(i, j, randomNumberInNumbers);
				if (GameAPP.board.GetComponent<CreatePlant>().IsPuff(randomNumberInNumbers))
				{
					component.SetPlant(i, j, randomNumberInNumbers);
					component.SetPlant(i, j, randomNumberInNumbers);
				}
			}
		}
	}

	private static void SetPlantsInLv11()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[6] { 1035, 1041, 1037, 1036, 1039, 1038 };
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				int randomNumberInNumbers = GetRandomNumberInNumbers(numbers);
				component.SetPlant(i, j, randomNumberInNumbers);
				if (GameAPP.board.GetComponent<CreatePlant>().IsPuff(randomNumberInNumbers))
				{
					component.SetPlant(i, j, randomNumberInNumbers);
					component.SetPlant(i, j, randomNumberInNumbers);
				}
			}
		}
	}

	private static void SetPlantsInLv12()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[14]
		{
			1037, 1038, 1046, 1026, 1023, 1024, 1044, 1042, 1021, 1026,
			1035, 1019, 1025, 1028
		};
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				int randomNumberInNumbers = GetRandomNumberInNumbers(numbers);
				component.SetPlant(i, j, randomNumberInNumbers);
				if (GameAPP.board.GetComponent<CreatePlant>().IsPuff(randomNumberInNumbers))
				{
					component.SetPlant(i, j, randomNumberInNumbers);
					component.SetPlant(i, j, randomNumberInNumbers);
				}
			}
		}
	}

	private static void SetPlantsInLv13()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[6] { 1005, 1032, 1034, 17, 1032, 1047 };
		int[] numbers2 = new int[3] { 12, 15, 1051 };
		int[] numbers3 = new int[4] { 1005, 1032, 1034, 1032 };
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				if (j == 2 || j == 3)
				{
					int randomNumberInNumbers = GetRandomNumberInNumbers(numbers2);
					component.SetPlant(i, j, randomNumberInNumbers);
					if (randomNumberInNumbers == 12)
					{
						int randomNumberInNumbers2 = GetRandomNumberInNumbers(numbers3);
						component.SetPlant(i, j, randomNumberInNumbers2);
					}
				}
				else
				{
					int randomNumberInNumbers3 = GetRandomNumberInNumbers(numbers);
					component.SetPlant(i, j, randomNumberInNumbers3);
				}
			}
		}
	}

	private static void SetPlantsInLv14()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[4] { 14, 1055, 1064, 1047 };
		int[] numbers2 = new int[2] { 12, 1051 };
		int[] numbers3 = new int[3] { 14, 1055, 1047 };
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				if (j == 2 || j == 3)
				{
					int randomNumberInNumbers = GetRandomNumberInNumbers(numbers2);
					component.SetPlant(i, j, randomNumberInNumbers);
					if (randomNumberInNumbers == 12)
					{
						int randomNumberInNumbers2 = GetRandomNumberInNumbers(numbers3);
						component.SetPlant(i, j, randomNumberInNumbers2);
					}
				}
				else
				{
					int randomNumberInNumbers3 = GetRandomNumberInNumbers(numbers);
					component.SetPlant(i, j, randomNumberInNumbers3);
				}
			}
		}
	}

	private static void SetPlantsInLv15()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[6] { 13, 1054, 1063, 5, 1016, 3 };
		int[] numbers2 = new int[1] { 12 };
		int[] numbers3 = new int[4] { 13, 1054, 5, 3 };
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				if (j == 2 || j == 3)
				{
					int randomNumberInNumbers = GetRandomNumberInNumbers(numbers2);
					component.SetPlant(i, j, randomNumberInNumbers);
					if (randomNumberInNumbers == 12)
					{
						int randomNumberInNumbers2 = GetRandomNumberInNumbers(numbers3);
						component.SetPlant(i, j, randomNumberInNumbers2);
					}
				}
				else
				{
					int randomNumberInNumbers3 = GetRandomNumberInNumbers(numbers);
					component.SetPlant(i, j, randomNumberInNumbers3);
				}
			}
		}
	}

	private static void SetPlantsInLv16()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[6] { 1061, 1062, 1063, 1064, 1060, 3 };
		int[] numbers2 = new int[3] { 12, 1049, 15 };
		int[] numbers3 = new int[2] { 1070, 3 };
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				if (j == 2 || j == 3)
				{
					int randomNumberInNumbers = GetRandomNumberInNumbers(numbers2);
					component.SetPlant(i, j, randomNumberInNumbers);
					if (randomNumberInNumbers == 12)
					{
						int randomNumberInNumbers2 = GetRandomNumberInNumbers(numbers3);
						component.SetPlant(i, j, randomNumberInNumbers2, null, default(Vector2), isFreeSet: true);
					}
				}
				else
				{
					int randomNumberInNumbers3 = GetRandomNumberInNumbers(numbers);
					component.SetPlant(i, j, randomNumberInNumbers3, null, default(Vector2), isFreeSet: true);
				}
			}
		}
	}

	private static void SetPlantsInLv17()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[7] { 1032, 1017, 14, 1052, 1055, 1053, 1061 };
		int[] numbers2 = new int[2] { 12, 1050 };
		int[] numbers3 = new int[5] { 1032, 1017, 1052, 1055, 1053 };
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				if (j == 2 || j == 3)
				{
					int randomNumberInNumbers = GetRandomNumberInNumbers(numbers2);
					component.SetPlant(i, j, randomNumberInNumbers);
					if (randomNumberInNumbers == 12)
					{
						int randomNumberInNumbers2 = GetRandomNumberInNumbers(numbers3);
						component.SetPlant(i, j, randomNumberInNumbers2, null, default(Vector2), isFreeSet: true);
					}
				}
				else
				{
					int randomNumberInNumbers3 = GetRandomNumberInNumbers(numbers);
					component.SetPlant(i, j, randomNumberInNumbers3, null, default(Vector2), isFreeSet: true);
				}
			}
		}
	}

	private static void SetPlantsInLv18()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[11]
		{
			1047, 1073, 1052, 1058, 1060, 1063, 14, 1062, 1016, 1020,
			1055
		};
		int[] numbers2 = new int[5] { 12, 1067, 15, 1051, 1056 };
		int[] numbers3 = new int[8] { 1047, 1052, 1058, 14, 1073, 1016, 1020, 1055 };
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				if (j == 2 || j == 3)
				{
					int randomNumberInNumbers = GetRandomNumberInNumbers(numbers2);
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), isFreeSet: true);
					if (randomNumberInNumbers == 12)
					{
						int randomNumberInNumbers2 = GetRandomNumberInNumbers(numbers3);
						component.SetPlant(i, j, randomNumberInNumbers2, null, default(Vector2), isFreeSet: true);
					}
				}
				else
				{
					int randomNumberInNumbers3 = GetRandomNumberInNumbers(numbers);
					component.SetPlant(i, j, randomNumberInNumbers3, null, default(Vector2), isFreeSet: true);
				}
			}
		}
	}
}

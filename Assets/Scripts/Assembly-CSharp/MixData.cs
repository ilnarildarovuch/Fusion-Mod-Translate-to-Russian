using System;
using UnityEngine;

// Token: 0x02000070 RID: 112
public class MixData
{
	// Token: 0x06000245 RID: 581 RVA: 0x000124F4 File Offset: 0x000106F4
	public static void InitMixData()
	{
		MixData.InitTravel();
		MixData.SpecialPlant();
		MixData.data[0, 0] = 1030;
		MixData.data[3, 3] = 255;
		MixData.data[1030, 1030] = 1032;
		MixData.data[1, 1] = 1033;
		MixData.data[2, 4] = 4;
		MixData.data[4, 2] = 4;
		MixData.data[0, 1] = 1000;
		MixData.data[1, 0] = 1000;
		MixData.data[0, 2] = 1001;
		MixData.data[2, 0] = 1001;
		MixData.data[1, 2] = 1002;
		MixData.data[2, 1] = 1002;
		MixData.data[2, 3] = 1003;
		MixData.data[3, 2] = 1003;
		MixData.data[0, 3] = 1004;
		MixData.data[3, 0] = 1004;
		MixData.data[1001, 2] = 1005;
		MixData.data[2, 1001] = 1005;
		MixData.data[1, 3] = 1006;
		MixData.data[3, 1] = 1006;
		MixData.data[0, 4] = 1007;
		MixData.data[4, 0] = 1007;
		MixData.data[1030, 2] = 1008;
		MixData.data[2, 1030] = 1008;
		MixData.data[1001, 0] = 1008;
		MixData.data[0, 1001] = 1008;
		MixData.data[1, 4] = 1009;
		MixData.data[4, 1] = 1009;
		MixData.data[3, 4] = 1010;
		MixData.data[4, 3] = 1010;
		MixData.data[0, 5] = 1011;
		MixData.data[5, 0] = 1011;
		MixData.data[3, 5] = 1012;
		MixData.data[5, 3] = 1012;
		MixData.data[1012, 0] = 1013;
		MixData.data[0, 1012] = 1013;
		MixData.data[1011, 3] = 1013;
		MixData.data[3, 1011] = 1013;
		MixData.data[1004, 5] = 1013;
		MixData.data[5, 1004] = 1013;
		MixData.data[1, 5] = 1014;
		MixData.data[5, 1] = 1014;
		MixData.data[5, 4] = 1015;
		MixData.data[4, 5] = 1015;
		MixData.data[5, 2] = 1016;
		MixData.data[2, 5] = 1016;
		MixData.data[1032, 2] = 1017;
		MixData.data[2, 1032] = 1017;
		MixData.data[1008, 1030] = 1017;
		MixData.data[1030, 1008] = 1017;
		MixData.data[6, 1] = 1031;
		MixData.data[6, 0] = 1018;
		MixData.data[6, 1030] = 1019;
		MixData.data[6, 3] = 1021;
		MixData.data[6, 7] = 6;
		MixData.data[6, 9] = 6;
		MixData.data[6, 8] = 1022;
		MixData.data[6, 10] = 1036;
		MixData.data[6, 11] = 1044;
		MixData.data[6, 1032] = 1065;
		MixData.data[7, 8] = 1023;
		MixData.data[8, 7] = 1023;
		MixData.data[8, 9] = 1024;
		MixData.data[9, 8] = 1024;
		MixData.data[7, 9] = 1025;
		MixData.data[9, 7] = 1025;
		MixData.data[1025, 8] = 1026;
		MixData.data[8, 1025] = 1026;
		MixData.data[1024, 7] = 1026;
		MixData.data[7, 1024] = 1026;
		MixData.data[1023, 9] = 1026;
		MixData.data[9, 1023] = 1026;
		MixData.data[10, 0] = 1034;
		MixData.data[0, 10] = 1034;
		MixData.data[1034, 6] = 1035;
		MixData.data[6, 1034] = 1035;
		MixData.data[10, 7] = 1037;
		MixData.data[7, 10] = 1037;
		MixData.data[10, 9] = 1038;
		MixData.data[9, 10] = 1038;
		MixData.data[10, 1027] = 1039;
		MixData.data[1027, 10] = 1039;
		MixData.data[10, 8] = 1041;
		MixData.data[8, 10] = 1041;
		MixData.data[10, 11] = 1040;
		MixData.data[11, 10] = 1040;
		MixData.data[7, 11] = 1043;
		MixData.data[11, 7] = 1043;
		MixData.data[9, 11] = 1042;
		MixData.data[11, 9] = 1042;
		MixData.data[8, 11] = 1045;
		MixData.data[11, 8] = 1045;
		MixData.data[1040, 7] = 1046;
		MixData.data[7, 1040] = 1046;
		MixData.data[1037, 11] = 1046;
		MixData.data[11, 1037] = 1046;
		MixData.data[1043, 10] = 1046;
		MixData.data[10, 1043] = 1046;
		MixData.data[1070, 16] = 1071;
		MixData.data[1070, 10] = 1072;
		MixData.data[1037, 1070] = 1072;
		MixData.data[1070, 1037] = 1072;
		MixData.data[14, 13] = 1047;
		MixData.data[13, 14] = 1047;
		MixData.data[14, 18] = 1055;
		MixData.data[18, 14] = 1055;
		MixData.data[13, 16] = 1054;
		MixData.data[16, 13] = 1054;
		MixData.data[14, 16] = 1058;
		MixData.data[16, 14] = 1058;
		MixData.data[13, 18] = 1059;
		MixData.data[18, 13] = 1059;
		MixData.data[18, 16] = 1053;
		MixData.data[16, 18] = 1053;
		MixData.data[16, 1053] = 1052;
		MixData.data[1053, 16] = 1052;
		MixData.data[15, 16] = 1049;
		MixData.data[15, 13] = 1050;
		MixData.data[15, 14] = 1051;
		MixData.data[15, 18] = 1056;
		MixData.data[17, 18] = 1061;
		MixData.data[18, 17] = 1061;
		MixData.data[17, 16] = 1062;
		MixData.data[16, 17] = 1062;
		MixData.data[17, 13] = 1063;
		MixData.data[13, 17] = 1063;
		MixData.data[17, 14] = 1064;
		MixData.data[14, 17] = 1064;
		MixData.data[1050, 14] = 1066;
		MixData.data[1051, 13] = 1066;
		MixData.data[15, 1047] = 1066;
		MixData.data[1067, 10] = 1068;
		MixData.data[10, 1067] = 1068;
		MixData.data[1067, 16] = 1069;
		MixData.data[16, 1067] = 1069;
		MixData.data[1027, 16] = 1073;
		MixData.data[16, 1027] = 1073;
		MixData.data[1060, 16] = 1075;
		MixData.data[16, 1060] = 1075;
		MixData.data[1062, 1060] = 1075;
		MixData.data[1060, 10] = 1074;
		MixData.data[10, 1060] = 1074;
	}

	// Token: 0x06000246 RID: 582 RVA: 0x00012FC4 File Offset: 0x000111C4
	private static void InitTravel()
	{
		MixData.data[8, 8] = 900;
		MixData.data[1017, 1005] = 901;
		MixData.data[1005, 1017] = 901;
		MixData.data[1059, 1054] = 902;
		MixData.data[1054, 1059] = 902;
		MixData.data[1013, 1016] = 903;
		MixData.data[1016, 1013] = 903;
		MixData.data[1046, 1026] = 904;
		MixData.data[1026, 1046] = 904;
	}

	// Token: 0x06000247 RID: 583 RVA: 0x000130AC File Offset: 0x000112AC
	private static void SpecialPlant()
	{
		MixData.data[3, 1027] = 1027;
		MixData.data[17, 1060] = 1060;
		MixData.data[7, 1070] = 1070;
		MixData.data[12, 1067] = 1067;
	}

	// Token: 0x06000248 RID: 584 RVA: 0x00013110 File Offset: 0x00011310
	public static void SetPlants(int level)
	{
		switch (level)
		{
		case 2:
			MixData.SetPlantsInLv2();
			return;
		case 3:
			MixData.SetPlantsInLv3();
			return;
		case 4:
			MixData.SetPlantsInLv4();
			return;
		case 5:
			MixData.SetPlantsInLv5();
			return;
		case 6:
			MixData.SetPlantsInLv6();
			return;
		case 7:
			MixData.SetPlantsInLv7();
			return;
		case 8:
			MixData.SetPlantsInLv8();
			return;
		case 9:
			MixData.SetPlantsInLv9();
			return;
		case 10:
			MixData.SetPlantsInLv10();
			return;
		case 11:
			MixData.SetPlantsInLv11();
			return;
		case 12:
			MixData.SetPlantsInLv12();
			return;
		case 13:
			MixData.SetPlantsInLv13();
			return;
		case 14:
			MixData.SetPlantsInLv14();
			return;
		case 15:
			MixData.SetPlantsInLv15();
			return;
		case 16:
			MixData.SetPlantsInLv16();
			return;
		case 17:
			MixData.SetPlantsInLv17();
			return;
		case 18:
			MixData.SetPlantsInLv18();
			return;
		default:
			return;
		}
	}

	// Token: 0x06000249 RID: 585 RVA: 0x000131D0 File Offset: 0x000113D0
	private static int GetRandomNumberInNumbers(int[] numbers)
	{
		int num = new Random().Next(numbers.Length);
		return numbers[num];
	}

	// Token: 0x0600024A RID: 586 RVA: 0x000131F0 File Offset: 0x000113F0
	private static void SetPlantsInLv2()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[]
		{
			1030,
			1032,
			0,
			1034,
			1030,
			1020
		};
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				component.SetPlant(i, j, MixData.GetRandomNumberInNumbers(numbers), null, default(Vector2), false, 0f);
			}
		}
	}

	// Token: 0x0600024B RID: 587 RVA: 0x00013254 File Offset: 0x00011454
	private static void SetPlantsInLv3()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[]
		{
			1016,
			1012,
			1011,
			1015,
			1034,
			1030
		};
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				if (i == 0)
				{
					component.SetPlant(i, j, 1005, null, default(Vector2), false, 0f);
				}
				else
				{
					component.SetPlant(i, j, MixData.GetRandomNumberInNumbers(numbers), null, default(Vector2), false, 0f);
				}
			}
		}
	}

	// Token: 0x0600024C RID: 588 RVA: 0x000132DC File Offset: 0x000114DC
	private static void SetPlantsInLv4()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[]
		{
			1015,
			4,
			1010,
			1007,
			5,
			1016
		};
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				PotatoMine potatoMine;
				if (i == 0)
				{
					component.SetPlant(i, j, 1013, null, default(Vector2), false, 0f);
				}
				else if (component.SetPlant(i, j, MixData.GetRandomNumberInNumbers(numbers), null, default(Vector2), false, 0f).TryGetComponent<PotatoMine>(out potatoMine))
				{
					potatoMine.attributeCountdown = 0f;
				}
			}
		}
	}

	// Token: 0x0600024D RID: 589 RVA: 0x00013378 File Offset: 0x00011578
	private static void SetPlantsInLv5()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[]
		{
			1012,
			1003,
			1029,
			1004,
			3,
			1034
		};
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				component.SetPlant(i, j, MixData.GetRandomNumberInNumbers(numbers), null, default(Vector2), false, 0f);
			}
		}
	}

	// Token: 0x0600024E RID: 590 RVA: 0x000133DC File Offset: 0x000115DC
	private static void SetPlantsInLv6()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[]
		{
			1001,
			1008,
			1017,
			1003,
			1010,
			1013,
			1034,
			1029,
			1020,
			1016
		};
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				component.SetPlant(i, j, MixData.GetRandomNumberInNumbers(numbers), null, default(Vector2), false, 0f);
			}
		}
	}

	// Token: 0x0600024F RID: 591 RVA: 0x00013440 File Offset: 0x00011640
	private static void SetPlantsInLv7()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[]
		{
			1021,
			1022,
			1019,
			1018,
			6,
			1036
		};
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				int randomNumberInNumbers = MixData.GetRandomNumberInNumbers(numbers);
				component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
				if (GameAPP.board.GetComponent<CreatePlant>().IsPuff(randomNumberInNumbers))
				{
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
				}
			}
		}
	}

	// Token: 0x06000250 RID: 592 RVA: 0x000134F4 File Offset: 0x000116F4
	private static void SetPlantsInLv8()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[]
		{
			7,
			1023,
			1037,
			1025,
			6,
			1018
		};
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				int randomNumberInNumbers = MixData.GetRandomNumberInNumbers(numbers);
				component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
				if (GameAPP.board.GetComponent<CreatePlant>().IsPuff(randomNumberInNumbers))
				{
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
				}
			}
		}
	}

	// Token: 0x06000251 RID: 593 RVA: 0x000135A8 File Offset: 0x000117A8
	private static void SetPlantsInLv9()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[]
		{
			1022,
			8,
			1041,
			1024,
			7,
			1021,
			1018
		};
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				int randomNumberInNumbers = MixData.GetRandomNumberInNumbers(numbers);
				component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
				if (GameAPP.board.GetComponent<CreatePlant>().IsPuff(randomNumberInNumbers))
				{
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
				}
			}
		}
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0001365C File Offset: 0x0001185C
	private static void SetPlantsInLv10()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[]
		{
			1025,
			1024,
			9,
			1026,
			1038,
			7,
			1018
		};
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				int randomNumberInNumbers = MixData.GetRandomNumberInNumbers(numbers);
				component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
				if (GameAPP.board.GetComponent<CreatePlant>().IsPuff(randomNumberInNumbers))
				{
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
				}
			}
		}
	}

	// Token: 0x06000253 RID: 595 RVA: 0x00013710 File Offset: 0x00011910
	private static void SetPlantsInLv11()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[]
		{
			1035,
			1041,
			1037,
			1036,
			1039,
			1038
		};
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				int randomNumberInNumbers = MixData.GetRandomNumberInNumbers(numbers);
				component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
				if (GameAPP.board.GetComponent<CreatePlant>().IsPuff(randomNumberInNumbers))
				{
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
				}
			}
		}
	}

	// Token: 0x06000254 RID: 596 RVA: 0x000137C4 File Offset: 0x000119C4
	private static void SetPlantsInLv12()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[]
		{
			1037,
			1038,
			1046,
			1026,
			1023,
			1024,
			1044,
			1042,
			1021,
			1026,
			1035,
			1019,
			1025,
			1028
		};
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				int randomNumberInNumbers = MixData.GetRandomNumberInNumbers(numbers);
				component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
				if (GameAPP.board.GetComponent<CreatePlant>().IsPuff(randomNumberInNumbers))
				{
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
				}
			}
		}
	}

	// Token: 0x06000255 RID: 597 RVA: 0x0001387C File Offset: 0x00011A7C
	private static void SetPlantsInLv13()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[]
		{
			1005,
			1032,
			1034,
			17,
			1032,
			1047
		};
		int[] numbers2 = new int[]
		{
			12,
			15,
			1051
		};
		int[] numbers3 = new int[]
		{
			1005,
			1032,
			1034,
			1032
		};
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				if (j == 2 || j == 3)
				{
					int randomNumberInNumbers = MixData.GetRandomNumberInNumbers(numbers2);
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
					if (randomNumberInNumbers == 12)
					{
						int randomNumberInNumbers2 = MixData.GetRandomNumberInNumbers(numbers3);
						component.SetPlant(i, j, randomNumberInNumbers2, null, default(Vector2), false, 0f);
					}
				}
				else
				{
					int randomNumberInNumbers3 = MixData.GetRandomNumberInNumbers(numbers);
					component.SetPlant(i, j, randomNumberInNumbers3, null, default(Vector2), false, 0f);
				}
			}
		}
	}

	// Token: 0x06000256 RID: 598 RVA: 0x0001397C File Offset: 0x00011B7C
	private static void SetPlantsInLv14()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[]
		{
			14,
			1055,
			1064,
			1047
		};
		int[] numbers2 = new int[]
		{
			12,
			1051
		};
		int[] numbers3 = new int[]
		{
			14,
			1055,
			1047
		};
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				if (j == 2 || j == 3)
				{
					int randomNumberInNumbers = MixData.GetRandomNumberInNumbers(numbers2);
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
					if (randomNumberInNumbers == 12)
					{
						int randomNumberInNumbers2 = MixData.GetRandomNumberInNumbers(numbers3);
						component.SetPlant(i, j, randomNumberInNumbers2, null, default(Vector2), false, 0f);
					}
				}
				else
				{
					int randomNumberInNumbers3 = MixData.GetRandomNumberInNumbers(numbers);
					component.SetPlant(i, j, randomNumberInNumbers3, null, default(Vector2), false, 0f);
				}
			}
		}
	}

	// Token: 0x06000257 RID: 599 RVA: 0x00013A7C File Offset: 0x00011C7C
	private static void SetPlantsInLv15()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[]
		{
			13,
			1054,
			1063,
			5,
			1016,
			3
		};
		int[] numbers2 = new int[]
		{
			12
		};
		int[] numbers3 = new int[]
		{
			13,
			1054,
			5,
			3
		};
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				if (j == 2 || j == 3)
				{
					int randomNumberInNumbers = MixData.GetRandomNumberInNumbers(numbers2);
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
					if (randomNumberInNumbers == 12)
					{
						int randomNumberInNumbers2 = MixData.GetRandomNumberInNumbers(numbers3);
						component.SetPlant(i, j, randomNumberInNumbers2, null, default(Vector2), false, 0f);
					}
				}
				else
				{
					int randomNumberInNumbers3 = MixData.GetRandomNumberInNumbers(numbers);
					component.SetPlant(i, j, randomNumberInNumbers3, null, default(Vector2), false, 0f);
				}
			}
		}
	}

	// Token: 0x06000258 RID: 600 RVA: 0x00013B74 File Offset: 0x00011D74
	private static void SetPlantsInLv16()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[]
		{
			1061,
			1062,
			1063,
			1064,
			1060,
			3
		};
		int[] numbers2 = new int[]
		{
			12,
			1049,
			15
		};
		int[] numbers3 = new int[]
		{
			1070,
			3
		};
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				if (j == 2 || j == 3)
				{
					int randomNumberInNumbers = MixData.GetRandomNumberInNumbers(numbers2);
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
					if (randomNumberInNumbers == 12)
					{
						int randomNumberInNumbers2 = MixData.GetRandomNumberInNumbers(numbers3);
						component.SetPlant(i, j, randomNumberInNumbers2, null, default(Vector2), true, 0f);
					}
				}
				else
				{
					int randomNumberInNumbers3 = MixData.GetRandomNumberInNumbers(numbers);
					component.SetPlant(i, j, randomNumberInNumbers3, null, default(Vector2), true, 0f);
				}
			}
		}
	}

	// Token: 0x06000259 RID: 601 RVA: 0x00013C74 File Offset: 0x00011E74
	private static void SetPlantsInLv17()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[]
		{
			1032,
			1017,
			14,
			1052,
			1055,
			1053,
			1061
		};
		int[] numbers2 = new int[]
		{
			12,
			1050
		};
		int[] numbers3 = new int[]
		{
			1032,
			1017,
			1052,
			1055,
			1053
		};
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				if (j == 2 || j == 3)
				{
					int randomNumberInNumbers = MixData.GetRandomNumberInNumbers(numbers2);
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), false, 0f);
					if (randomNumberInNumbers == 12)
					{
						int randomNumberInNumbers2 = MixData.GetRandomNumberInNumbers(numbers3);
						component.SetPlant(i, j, randomNumberInNumbers2, null, default(Vector2), true, 0f);
					}
				}
				else
				{
					int randomNumberInNumbers3 = MixData.GetRandomNumberInNumbers(numbers);
					component.SetPlant(i, j, randomNumberInNumbers3, null, default(Vector2), true, 0f);
				}
			}
		}
	}

	// Token: 0x0600025A RID: 602 RVA: 0x00013D74 File Offset: 0x00011F74
	private static void SetPlantsInLv18()
	{
		CreatePlant component = GameAPP.board.GetComponent<CreatePlant>();
		int[] numbers = new int[]
		{
			1047,
			1073,
			1052,
			1058,
			1060,
			1063,
			14,
			1062,
			1016,
			1020,
			1055
		};
		int[] numbers2 = new int[]
		{
			12,
			1067,
			15,
			1051,
			1056
		};
		int[] numbers3 = new int[]
		{
			1047,
			1052,
			1058,
			14,
			1073,
			1016,
			1020,
			1055
		};
		for (int i = 0; i < 5; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				if (j == 2 || j == 3)
				{
					int randomNumberInNumbers = MixData.GetRandomNumberInNumbers(numbers2);
					component.SetPlant(i, j, randomNumberInNumbers, null, default(Vector2), true, 0f);
					if (randomNumberInNumbers == 12)
					{
						int randomNumberInNumbers2 = MixData.GetRandomNumberInNumbers(numbers3);
						component.SetPlant(i, j, randomNumberInNumbers2, null, default(Vector2), true, 0f);
					}
				}
				else
				{
					int randomNumberInNumbers3 = MixData.GetRandomNumberInNumbers(numbers);
					component.SetPlant(i, j, randomNumberInNumbers3, null, default(Vector2), true, 0f);
				}
			}
		}
	}

	// Token: 0x04000180 RID: 384
	public static int[,] data = new int[2048, 2048];

	// Token: 0x02000138 RID: 312
	public enum PlantType
	{
		// Token: 0x040003C5 RID: 965
		Peashooter,
		// Token: 0x040003C6 RID: 966
		SunFlower,
		// Token: 0x040003C7 RID: 967
		CherryBomb,
		// Token: 0x040003C8 RID: 968
		WallNut,
		// Token: 0x040003C9 RID: 969
		PotatoMine,
		// Token: 0x040003CA RID: 970
		Chomper,
		// Token: 0x040003CB RID: 971
		SmallPuff,
		// Token: 0x040003CC RID: 972
		FumeShroom,
		// Token: 0x040003CD RID: 973
		HypnoShroom,
		// Token: 0x040003CE RID: 974
		ScaredyShroom,
		// Token: 0x040003CF RID: 975
		IceShroom,
		// Token: 0x040003D0 RID: 976
		DoomShroom,
		// Token: 0x040003D1 RID: 977
		LilyPad,
		// Token: 0x040003D2 RID: 978
		Squash,
		// Token: 0x040003D3 RID: 979
		ThreePeater,
		// Token: 0x040003D4 RID: 980
		Tanglekelp,
		// Token: 0x040003D5 RID: 981
		Jalapeno,
		// Token: 0x040003D6 RID: 982
		Caltrop,
		// Token: 0x040003D7 RID: 983
		TorchWood,
		// Token: 0x040003D8 RID: 984
		CattailGirl = 252,
		// Token: 0x040003D9 RID: 985
		Wheat,
		// Token: 0x040003DA RID: 986
		EndoFlame,
		// Token: 0x040003DB RID: 987
		BigWallNut,
		// Token: 0x040003DC RID: 988
		Present,
		// Token: 0x040003DD RID: 989
		HyponoEmperor = 900,
		// Token: 0x040003DE RID: 990
		SuperCherryGatling,
		// Token: 0x040003DF RID: 991
		JalaSquashTorch,
		// Token: 0x040003E0 RID: 992
		SuperCherryChomper,
		// Token: 0x040003E1 RID: 993
		FinalFume,
		// Token: 0x040003E2 RID: 994
		PeaSunFlower = 1000,
		// Token: 0x040003E3 RID: 995
		Cherryshooter,
		// Token: 0x040003E4 RID: 996
		SunBomb,
		// Token: 0x040003E5 RID: 997
		CherryNut,
		// Token: 0x040003E6 RID: 998
		PeaNut,
		// Token: 0x040003E7 RID: 999
		SuperCherryShooter,
		// Token: 0x040003E8 RID: 1000
		SunNut,
		// Token: 0x040003E9 RID: 1001
		PeaMine,
		// Token: 0x040003EA RID: 1002
		DoubleCherry,
		// Token: 0x040003EB RID: 1003
		SunMine,
		// Token: 0x040003EC RID: 1004
		PotatoNut,
		// Token: 0x040003ED RID: 1005
		PeaChomper,
		// Token: 0x040003EE RID: 1006
		NutChomper,
		// Token: 0x040003EF RID: 1007
		SuperChomper,
		// Token: 0x040003F0 RID: 1008
		SunChomper,
		// Token: 0x040003F1 RID: 1009
		PotatoChomper,
		// Token: 0x040003F2 RID: 1010
		CherryChomper,
		// Token: 0x040003F3 RID: 1011
		CherryGatling,
		// Token: 0x040003F4 RID: 1012
		PeaSmallPuff,
		// Token: 0x040003F5 RID: 1013
		DoublePuff,
		// Token: 0x040003F6 RID: 1014
		IronPea,
		// Token: 0x040003F7 RID: 1015
		PuffNut,
		// Token: 0x040003F8 RID: 1016
		HypnoPuff,
		// Token: 0x040003F9 RID: 1017
		HypnoFume,
		// Token: 0x040003FA RID: 1018
		ScaredyHypno,
		// Token: 0x040003FB RID: 1019
		ScaredFume,
		// Token: 0x040003FC RID: 1020
		SuperHypno,
		// Token: 0x040003FD RID: 1021
		TallNut,
		// Token: 0x040003FE RID: 1022
		TallNutFootball,
		// Token: 0x040003FF RID: 1023
		IronNut,
		// Token: 0x04000400 RID: 1024
		DoubleShooer,
		// Token: 0x04000401 RID: 1025
		SunShroom,
		// Token: 0x04000402 RID: 1026
		GatlingPea,
		// Token: 0x04000403 RID: 1027
		TwinFlower,
		// Token: 0x04000404 RID: 1028
		SnowPeaShooter,
		// Token: 0x04000405 RID: 1029
		IcePuff,
		// Token: 0x04000406 RID: 1030
		SmallIceShroom,
		// Token: 0x04000407 RID: 1031
		IceFumeShroom,
		// Token: 0x04000408 RID: 1032
		IceScaredyShroom,
		// Token: 0x04000409 RID: 1033
		TallIceNut,
		// Token: 0x0400040A RID: 1034
		IceDoom,
		// Token: 0x0400040B RID: 1035
		IceHypno,
		// Token: 0x0400040C RID: 1036
		ScaredyDoom,
		// Token: 0x0400040D RID: 1037
		DoomFume,
		// Token: 0x0400040E RID: 1038
		PuffDoom,
		// Token: 0x0400040F RID: 1039
		HypnoDoom,
		// Token: 0x04000410 RID: 1040
		IceDoomFume,
		// Token: 0x04000411 RID: 1041
		ThreeSquash,
		// Token: 0x04000412 RID: 1042
		EliteTorchWood,
		// Token: 0x04000413 RID: 1043
		Jalakelp,
		// Token: 0x04000414 RID: 1044
		Squashkelp,
		// Token: 0x04000415 RID: 1045
		Threekelp,
		// Token: 0x04000416 RID: 1046
		SuperTorch,
		// Token: 0x04000417 RID: 1047
		JalaTorch,
		// Token: 0x04000418 RID: 1048
		JalaSquash,
		// Token: 0x04000419 RID: 1049
		ThreeTorch,
		// Token: 0x0400041A RID: 1050
		KelpTorch,
		// Token: 0x0400041B RID: 1051
		FireSquash,
		// Token: 0x0400041C RID: 1052
		DarkThreePeater,
		// Token: 0x0400041D RID: 1053
		SquashTorch,
		// Token: 0x0400041E RID: 1054
		SpikeRock,
		// Token: 0x0400041F RID: 1055
		FireSpike,
		// Token: 0x04000420 RID: 1056
		JalaSpike,
		// Token: 0x04000421 RID: 1057
		SquashSpike,
		// Token: 0x04000422 RID: 1058
		ThreeSpike,
		// Token: 0x04000423 RID: 1059
		GatlingPuff,
		// Token: 0x04000424 RID: 1060
		SuperKelp,
		// Token: 0x04000425 RID: 1061
		CattailPlant,
		// Token: 0x04000426 RID: 1062
		IceCattail,
		// Token: 0x04000427 RID: 1063
		FireCattail,
		// Token: 0x04000428 RID: 1064
		GloomShroom,
		// Token: 0x04000429 RID: 1065
		FireGloom,
		// Token: 0x0400042A RID: 1066
		IceGloom,
		// Token: 0x0400042B RID: 1067
		TallFireNut,
		// Token: 0x0400042C RID: 1068
		IceSpikeRock,
		// Token: 0x0400042D RID: 1069
		FireSpikeRock
	}
}

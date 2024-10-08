using System;
using UnityEngine;

// Token: 0x02000108 RID: 264
public class TypeMgr : MonoBehaviour
{
	// Token: 0x0600053E RID: 1342 RVA: 0x0002D596 File Offset: 0x0002B796
	public static bool IsPotatoMine(int theSeedType)
	{
		if (theSeedType <= 1007)
		{
			if (theSeedType != 4 && theSeedType != 1007)
			{
				return false;
			}
		}
		else if (theSeedType - 1009 > 1 && theSeedType != 1015)
		{
			return false;
		}
		return true;
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x0002D5C4 File Offset: 0x0002B7C4
	public static bool IsNut(int theSeedType)
	{
		if (TypeMgr.IsTallNut(theSeedType))
		{
			return true;
		}
		if (theSeedType <= 903)
		{
			if (theSeedType != 3 && theSeedType != 255 && theSeedType != 903)
			{
				return false;
			}
		}
		else
		{
			switch (theSeedType)
			{
			case 1003:
			case 1004:
			case 1006:
			case 1010:
			case 1012:
			case 1013:
				break;
			case 1005:
			case 1007:
			case 1008:
			case 1009:
			case 1011:
				return false;
			default:
				if (theSeedType != 1021 && theSeedType != 1029)
				{
					return false;
				}
				break;
			}
		}
		return true;
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x0002D644 File Offset: 0x0002B844
	public static bool IsCaltrop(int theSeedType)
	{
		return theSeedType == 17 || theSeedType - 1060 <= 4 || theSeedType - 1074 <= 1;
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x0002D662 File Offset: 0x0002B862
	public static bool IsTallNut(int theSeedType)
	{
		if (theSeedType <= 1028)
		{
			if (theSeedType != 903 && theSeedType - 1027 > 1)
			{
				return false;
			}
		}
		else if (theSeedType != 1039 && theSeedType != 1073)
		{
			return false;
		}
		return true;
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x0002D693 File Offset: 0x0002B893
	public static bool IsTangkelp(int theSeedType)
	{
		if (theSeedType <= 1051)
		{
			if (theSeedType != 15 && theSeedType - 1049 > 2)
			{
				return false;
			}
		}
		else if (theSeedType != 1056 && theSeedType != 1066)
		{
			return false;
		}
		return true;
	}
}

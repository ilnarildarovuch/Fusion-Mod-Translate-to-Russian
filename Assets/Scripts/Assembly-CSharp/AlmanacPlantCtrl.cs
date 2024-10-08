using System;
using UnityEngine;

// Token: 0x02000028 RID: 40
public class AlmanacPlantCtrl : MonoBehaviour
{
	// Token: 0x060000B1 RID: 177 RVA: 0x0000581E File Offset: 0x00003A1E
	private void Start()
	{
		this.cardGroupPath = "UI/Almanac/MixGroups/Plant";
		this.basicCard = base.transform.GetChild(0).gameObject;
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00005844 File Offset: 0x00003A44
	public void GetSeedType(int theSeedType, bool isBasicCard)
	{
		if (theSeedType == 100 || theSeedType == 101)
		{
			this.basicCard.SetActive(false);
			GameObject gameObject = Resources.Load<GameObject>(this.cardGroupPath + theSeedType.ToString());
			if (gameObject != null)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, base.transform);
				this.localMixGroup = gameObject2;
			}
			return;
		}
		if (theSeedType != this.plantSelected)
		{
			this.plantSelected = theSeedType;
			Object.Destroy(this.localShowPlant);
			Object.Destroy(this.localCardBank);
			GameObject gameObject3 = Object.Instantiate<GameObject>(Resources.Load<GameObject>(this.GetPath(theSeedType)), base.transform);
			gameObject3.name = "CardBank";
			gameObject3.GetComponent<AlmanacMgr>().theSeedType = theSeedType;
			GameObject gameObject4 = CreatePlant.SetPlantInAlmamac(this.v, theSeedType);
			gameObject4.transform.SetParent(base.transform);
			this.localShowPlant = gameObject4;
			this.localCardBank = gameObject3;
			if (isBasicCard)
			{
				this.basicCard.SetActive(false);
				GameObject gameObject5 = Resources.Load<GameObject>(this.cardGroupPath + theSeedType.ToString());
				if (gameObject5 != null)
				{
					GameObject gameObject6 = Object.Instantiate<GameObject>(gameObject5, base.transform);
					this.localMixGroup = gameObject6;
				}
			}
		}
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x0000596A File Offset: 0x00003B6A
	public void ShowBasicCard()
	{
		this.basicCard.SetActive(true);
		Object.Destroy(this.localMixGroup);
		this.plantSelected = -1;
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x0000598C File Offset: 0x00003B8C
	private string GetPath(int theSeedType)
	{
		if (theSeedType <= 252)
		{
			switch (theSeedType)
			{
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
			case 11:
				break;
			case 12:
			case 15:
				goto IL_150;
			case 13:
			case 14:
				goto IL_158;
			default:
				if (theSeedType != 252)
				{
					goto IL_158;
				}
				goto IL_150;
			}
		}
		else if (theSeedType != 900 && theSeedType != 904)
		{
			switch (theSeedType)
			{
			case 1018:
			case 1019:
			case 1021:
			case 1022:
			case 1023:
			case 1024:
			case 1025:
			case 1026:
			case 1031:
			case 1035:
			case 1036:
			case 1037:
			case 1038:
			case 1040:
			case 1041:
			case 1042:
			case 1043:
			case 1044:
			case 1045:
			case 1046:
			case 1065:
			case 1070:
			case 1071:
			case 1072:
				break;
			case 1020:
			case 1027:
			case 1028:
			case 1029:
			case 1030:
			case 1032:
			case 1033:
			case 1034:
			case 1039:
			case 1047:
			case 1048:
			case 1052:
			case 1053:
			case 1054:
			case 1055:
			case 1057:
			case 1058:
			case 1059:
			case 1060:
			case 1061:
			case 1062:
			case 1063:
			case 1064:
				goto IL_158;
			case 1049:
			case 1050:
			case 1051:
			case 1056:
			case 1066:
			case 1067:
			case 1068:
			case 1069:
				goto IL_150;
			default:
				goto IL_158;
			}
		}
		return "UI/Almanac/PlantPrefabs/AlmanacNight";
		IL_150:
		return "UI/Almanac/PlantPrefabs/AlmanacPool";
		IL_158:
		return "UI/Almanac/PlantPrefabs/AlmanacDay";
	}

	// Token: 0x04000094 RID: 148
	public int plantSelected = -1;

	// Token: 0x04000095 RID: 149
	public string cardGroupPath;

	// Token: 0x04000096 RID: 150
	private GameObject localShowPlant;

	// Token: 0x04000097 RID: 151
	private GameObject localCardBank;

	// Token: 0x04000098 RID: 152
	private GameObject localMixGroup;

	// Token: 0x04000099 RID: 153
	private GameObject basicCard;

	// Token: 0x0400009A RID: 154
	private Vector3 v = new Vector3(5.45f, 1.4f, 0f);
}

using UnityEngine;

public class AlmanacPlantCtrl : MonoBehaviour
{
	public int plantSelected = -1;

	public string cardGroupPath;

	private GameObject localShowPlant;

	private GameObject localCardBank;

	private GameObject localMixGroup;

	private GameObject basicCard;

	private Vector3 v = new Vector3(5.45f, 1.4f, 0f);

	private void Start()
	{
		cardGroupPath = "UI/Almanac/MixGroups/Plant";
		basicCard = base.transform.GetChild(0).gameObject;
	}

	public void GetSeedType(int theSeedType, bool isBasicCard)
	{
		if (theSeedType == 100 || theSeedType == 101)
		{
			basicCard.SetActive(value: false);
			GameObject gameObject = Resources.Load<GameObject>(cardGroupPath + theSeedType);
			if (gameObject != null)
			{
				GameObject gameObject2 = Object.Instantiate(gameObject, base.transform);
				localMixGroup = gameObject2;
			}
		}
		else
		{
			if (theSeedType == plantSelected)
			{
				return;
			}
			plantSelected = theSeedType;
			Object.Destroy(localShowPlant);
			Object.Destroy(localCardBank);
			GameObject gameObject3 = Object.Instantiate(Resources.Load<GameObject>(GetPath(theSeedType)), base.transform);
			gameObject3.name = "CardBank";
			gameObject3.GetComponent<AlmanacMgr>().theSeedType = theSeedType;
			GameObject gameObject4 = CreatePlant.SetPlantInAlmamac(v, theSeedType);
			gameObject4.transform.SetParent(base.transform);
			localShowPlant = gameObject4;
			localCardBank = gameObject3;
			if (isBasicCard)
			{
				basicCard.SetActive(value: false);
				GameObject gameObject5 = Resources.Load<GameObject>(cardGroupPath + theSeedType);
				if (gameObject5 != null)
				{
					GameObject gameObject6 = Object.Instantiate(gameObject5, base.transform);
					localMixGroup = gameObject6;
				}
			}
		}
	}

	public void ShowBasicCard()
	{
		basicCard.SetActive(value: true);
		Object.Destroy(localMixGroup);
		plantSelected = -1;
	}

	private string GetPath(int theSeedType)
	{
		switch (theSeedType)
		{
		case 6:
		case 7:
		case 8:
		case 9:
		case 10:
		case 11:
		case 900:
		case 904:
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
			return "UI/Almanac/PlantPrefabs/AlmanacNight";
		case 12:
		case 15:
		case 252:
		case 1049:
		case 1050:
		case 1051:
		case 1056:
		case 1066:
		case 1067:
		case 1068:
		case 1069:
			return "UI/Almanac/PlantPrefabs/AlmanacPool";
		default:
			return "UI/Almanac/PlantPrefabs/AlmanacDay";
		}
	}
}

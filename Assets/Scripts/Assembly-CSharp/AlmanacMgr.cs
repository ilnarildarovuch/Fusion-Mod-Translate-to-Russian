using System;
using System.IO;
using TMPro;
using UnityEngine;

public class AlmanacMgr : MonoBehaviour
{
	[Serializable]
	public class PlantInfo
	{
		public string name;

		public string introduce;

		public string info;

		public string cost;

		public int seedType;
	}

	[Serializable]
	public class PlantData
	{
		public PlantInfo[] plants = new PlantInfo[2048];
	}

	public int theSeedType;

	private GameObject plantName;

	private GameObject info;

	private GameObject cost;

	private void Awake()
	{
		plantName = base.transform.Find("Name").gameObject;
		info = base.transform.Find("Info").gameObject;
		cost = base.transform.Find("Cost").gameObject;
	}

	private void Start()
	{
		InitNameAndInfoFromJson();
	}

	private void InitNameAndInfoFromJson()
	{
		TextMeshPro component = info.GetComponent<TextMeshPro>();
		TextMeshPro component2 = plantName.GetComponent<TextMeshPro>();
		TextMeshPro component3 = plantName.transform.GetChild(0).GetComponent<TextMeshPro>();
		TextMeshPro component4 = cost.GetComponent<TextMeshPro>();
		PlantInfo[] plants = JsonUtility.FromJson<PlantData>(File.ReadAllText(Application.dataPath + "/LawnStrings.json")).plants;
		foreach (PlantInfo plantInfo in plants)
		{
			if (plantInfo.seedType == theSeedType)
			{
				component.text = plantInfo.info + "\n\n" + plantInfo.introduce;
				component2.text = plantInfo.name;
				component3.text = plantInfo.name;
				component4.text = plantInfo.cost;
				break;
			}
		}
	}
}

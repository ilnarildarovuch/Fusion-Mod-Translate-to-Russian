using System;
using System.IO;
using TMPro;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class AlmanacMgr : MonoBehaviour
{
	// Token: 0x06000204 RID: 516 RVA: 0x00010B90 File Offset: 0x0000ED90
	private void Awake()
	{
		this.plantName = base.transform.Find("Name").gameObject;
		this.info = base.transform.Find("Info").gameObject;
		this.cost = base.transform.Find("Cost").gameObject;
	}

	// Token: 0x06000205 RID: 517 RVA: 0x00010BEE File Offset: 0x0000EDEE
	private void Start()
	{
		this.InitNameAndInfoFromJson();
	}

	// Token: 0x06000206 RID: 518 RVA: 0x00010BF8 File Offset: 0x0000EDF8
	private void InitNameAndInfoFromJson()
	{
		TextMeshPro component = this.info.GetComponent<TextMeshPro>();
		TextMeshPro component2 = this.plantName.GetComponent<TextMeshPro>();
		TextMeshPro component3 = this.plantName.transform.GetChild(0).GetComponent<TextMeshPro>();
		TextMeshPro component4 = this.cost.GetComponent<TextMeshPro>();
		foreach (AlmanacMgr.PlantInfo plantInfo in JsonUtility.FromJson<AlmanacMgr.PlantData>(File.ReadAllText(Application.dataPath + "/LawnStrings.json")).plants)
		{
			if (plantInfo.seedType == this.theSeedType)
			{
				component.text = plantInfo.info + "\n\n" + plantInfo.introduce;
				component2.text = plantInfo.name;
				component3.text = plantInfo.name;
				component4.text = plantInfo.cost;
				return;
			}
		}
	}

	// Token: 0x04000168 RID: 360
	public int theSeedType;

	// Token: 0x04000169 RID: 361
	private GameObject plantName;

	// Token: 0x0400016A RID: 362
	private GameObject info;

	// Token: 0x0400016B RID: 363
	private GameObject cost;

	// Token: 0x02000134 RID: 308
	[Serializable]
	public class PlantInfo
	{
		// Token: 0x040003B2 RID: 946
		public string name;

		// Token: 0x040003B3 RID: 947
		public string introduce;

		// Token: 0x040003B4 RID: 948
		public string info;

		// Token: 0x040003B5 RID: 949
		public string cost;

		// Token: 0x040003B6 RID: 950
		public int seedType;
	}

	// Token: 0x02000135 RID: 309
	[Serializable]
	public class PlantData
	{
		// Token: 0x040003B7 RID: 951
		public AlmanacMgr.PlantInfo[] plants = new AlmanacMgr.PlantInfo[2048];
	}
}

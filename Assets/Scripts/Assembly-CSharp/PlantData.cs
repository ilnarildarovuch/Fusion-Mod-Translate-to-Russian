using System;
using UnityEngine;

// Token: 0x020000F5 RID: 245
public class PlantData : MonoBehaviour
{
	// Token: 0x060004B4 RID: 1204 RVA: 0x00026BBC File Offset: 0x00024DBC
	public static void InitPlantData()
	{
		PlantData.plantData[0] = new PlantData.PlantData_
		{
			attackInterval = 1.5f,
			produceInterval = 0f,
			attackDamage = 20f,
			plantHealth = 300f,
			coolDownTime = 7.5f,
			cardPrice = 100f
		};
		PlantData.plantData[1] = new PlantData.PlantData_
		{
			attackInterval = 0f,
			produceInterval = 25f,
			attackDamage = 0f,
			plantHealth = 300f,
			coolDownTime = 7.5f,
			cardPrice = 50f
		};
		PlantData.plantData[2] = new PlantData.PlantData_
		{
			attackInterval = 0f,
			produceInterval = 0f,
			attackDamage = 0f,
			plantHealth = 300f,
			coolDownTime = 50f,
			cardPrice = 150f
		};
		PlantData.plantData[3] = new PlantData.PlantData_
		{
			attackInterval = 0f,
			produceInterval = 0f,
			attackDamage = 0f,
			plantHealth = 4000f,
			coolDownTime = 30f,
			cardPrice = 50f
		};
		PlantData.plantData[4] = new PlantData.PlantData_
		{
			attackInterval = 0f,
			produceInterval = 0f,
			attackDamage = 0f,
			plantHealth = 300f,
			coolDownTime = 30f,
			cardPrice = 25f
		};
	}

	// Token: 0x04000233 RID: 563
	public static PlantData.PlantData_[] plantData = new PlantData.PlantData_[2048];

	// Token: 0x0200014C RID: 332
	public struct PlantData_
	{
		// Token: 0x0400048C RID: 1164
		public float attackInterval;

		// Token: 0x0400048D RID: 1165
		public float produceInterval;

		// Token: 0x0400048E RID: 1166
		public float attackDamage;

		// Token: 0x0400048F RID: 1167
		public float plantHealth;

		// Token: 0x04000490 RID: 1168
		public float coolDownTime;

		// Token: 0x04000491 RID: 1169
		public float cardPrice;
	}
}

using UnityEngine;

public class PlantData : MonoBehaviour
{
	public struct PlantData_
	{
		public float attackInterval;

		public float produceInterval;

		public float attackDamage;

		public float plantHealth;

		public float coolDownTime;

		public float cardPrice;
	}

	public static PlantData_[] plantData = new PlantData_[2048];

	public static void InitPlantData()
	{
		plantData[0] = new PlantData_
		{
			attackInterval = 1.5f,
			produceInterval = 0f,
			attackDamage = 20f,
			plantHealth = 300f,
			coolDownTime = 7.5f,
			cardPrice = 100f
		};
		plantData[1] = new PlantData_
		{
			attackInterval = 0f,
			produceInterval = 25f,
			attackDamage = 0f,
			plantHealth = 300f,
			coolDownTime = 7.5f,
			cardPrice = 50f
		};
		plantData[2] = new PlantData_
		{
			attackInterval = 0f,
			produceInterval = 0f,
			attackDamage = 0f,
			plantHealth = 300f,
			coolDownTime = 50f,
			cardPrice = 150f
		};
		plantData[3] = new PlantData_
		{
			attackInterval = 0f,
			produceInterval = 0f,
			attackDamage = 0f,
			plantHealth = 4000f,
			coolDownTime = 30f,
			cardPrice = 50f
		};
		plantData[4] = new PlantData_
		{
			attackInterval = 0f,
			produceInterval = 0f,
			attackDamage = 0f,
			plantHealth = 300f,
			coolDownTime = 30f,
			cardPrice = 25f
		};
	}
}

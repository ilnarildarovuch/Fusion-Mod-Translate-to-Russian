using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
	public struct PlantInTravel
	{
		public int thePlantType;

		public int thePlantRow;

		public int thePlantColumn;

		public int thePlantHealth;
	}

	public struct LevelInfo
	{
		public int theSun;

		public int theCurrentRound;

		public int theMaxRound;
	}

	public static List<PlantInTravel> plantInTravel = new List<PlantInTravel>();

	public static LevelInfo level = default(LevelInfo);

	public static void AddPlant(Plant plant)
	{
		PlantInTravel plantInTravel = default(PlantInTravel);
		plantInTravel.thePlantType = plant.thePlantType;
		plantInTravel.thePlantRow = plant.thePlantRow;
		plantInTravel.thePlantColumn = plant.thePlantColumn;
		plantInTravel.thePlantHealth = plant.thePlantHealth;
		PlantInTravel item = plantInTravel;
		LevelData.plantInTravel.Add(item);
	}

	public static void ClearPlant()
	{
		plantInTravel.Clear();
	}

	public static void LoadPlant()
	{
		foreach (PlantInTravel item in plantInTravel)
		{
			GameObject gameObject;
			if (Board.Instance.roadNum == 6)
			{
				if (item.thePlantRow != 2)
				{
					gameObject = ((item.thePlantRow == 3) ? CreatePlant.Instance.SetPlant(item.thePlantColumn, 4, item.thePlantType) : ((item.thePlantRow != 4) ? CreatePlant.Instance.SetPlant(item.thePlantColumn, item.thePlantRow, item.thePlantType) : CreatePlant.Instance.SetPlant(item.thePlantColumn, 5, item.thePlantType)));
				}
				else
				{
					int theRow = Random.Range(2, 4);
					CreatePlant.Instance.SetPlant(item.thePlantColumn, theRow, 12);
					gameObject = CreatePlant.Instance.SetPlant(item.thePlantColumn, theRow, item.thePlantType);
				}
			}
			else
			{
				gameObject = CreatePlant.Instance.SetPlant(item.thePlantColumn, item.thePlantRow, item.thePlantType);
			}
			if (gameObject != null)
			{
				gameObject.GetComponent<Plant>().thePlantHealth = item.thePlantHealth;
			}
		}
	}

	public static void SaveLevel(Board board)
	{
		level.theSun = board.theSun;
		level.theCurrentRound = board.theCurrentSurvivalRound;
		level.theMaxRound = board.theSurvivalMaxRound;
	}
}

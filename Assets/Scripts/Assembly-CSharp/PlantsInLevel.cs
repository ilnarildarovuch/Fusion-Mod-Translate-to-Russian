using UnityEngine;

public class PlantsInLevel : MonoBehaviour
{
	public static int[] plant = new int[1024];

	public static int[] boardData = new int[3];

	public static int maxRound = 0;

	public static void ClearBoard()
	{
		for (int i = 0; i < boardData.Length; i++)
		{
			boardData[i] = 0;
		}
		ClearPlant();
	}

	public static void SaveBoard()
	{
		boardData[0] = 1;
		boardData[1] = Board.Instance.theSun;
		boardData[2] = Board.Instance.theCurrentSurvivalRound;
		if (maxRound < boardData[2] - 1)
		{
			maxRound = boardData[2] - 1;
		}
		SavePlants();
	}

	public static bool LoadBoard()
	{
		if (boardData[0] == 0)
		{
			return false;
		}
		if (boardData[0] == 1)
		{
			LoadPlant();
			Board.Instance.theSun = boardData[1];
			Board.Instance.theCurrentSurvivalRound = boardData[2];
			return true;
		}
		Debug.LogError("boardData error");
		return false;
	}

	public static void SavePlants()
	{
		ClearPlant();
		GameObject[] plantArray = Board.Instance.plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				AddPlant(component.thePlantColumn, component.thePlantRow, component.thePlantType);
			}
		}
	}

	public static void AddPlant(int theColumn, int theRow, int thePlantType)
	{
		for (int i = 0; i < plant.Length; i += 4)
		{
			if (plant[i] == 0)
			{
				plant[i] = thePlantType + 1;
				plant[i + 1] = theColumn;
				plant[i + 2] = theRow;
				if (thePlantType == 12)
				{
					plant[i + 3] = 1;
				}
				else
				{
					plant[i + 3] = 0;
				}
				break;
			}
		}
	}

	public static void LoadPlant()
	{
		for (int i = 0; i < plant.Length; i += 4)
		{
			if (plant[i] != 0 && plant[i + 3] == 1)
			{
				CreatePlant.Instance.SetPlant(plant[i + 1], plant[i + 2], plant[i] - 1);
			}
		}
		for (int j = 0; j < plant.Length; j += 4)
		{
			if (plant[j] != 0)
			{
				CreatePlant.Instance.SetPlant(plant[j + 1], plant[j + 2], plant[j] - 1);
			}
		}
	}

	public static void ClearPlant()
	{
		for (int i = 0; i < plant.Length; i++)
		{
			plant[i] = 0;
		}
	}
}

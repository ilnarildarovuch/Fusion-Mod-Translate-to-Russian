using System;
using UnityEngine;

// Token: 0x02000100 RID: 256
public class PlantsInLevel : MonoBehaviour
{
	// Token: 0x06000508 RID: 1288 RVA: 0x0002B478 File Offset: 0x00029678
	public static void ClearBoard()
	{
		for (int i = 0; i < PlantsInLevel.boardData.Length; i++)
		{
			PlantsInLevel.boardData[i] = 0;
		}
		PlantsInLevel.ClearPlant();
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x0002B4A4 File Offset: 0x000296A4
	public static void SaveBoard()
	{
		PlantsInLevel.boardData[0] = 1;
		PlantsInLevel.boardData[1] = Board.Instance.theSun;
		PlantsInLevel.boardData[2] = Board.Instance.theCurrentSurvivalRound;
		if (PlantsInLevel.maxRound < PlantsInLevel.boardData[2] - 1)
		{
			PlantsInLevel.maxRound = PlantsInLevel.boardData[2] - 1;
		}
		PlantsInLevel.SavePlants();
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x0002B500 File Offset: 0x00029700
	public static bool LoadBoard()
	{
		if (PlantsInLevel.boardData[0] == 0)
		{
			return false;
		}
		if (PlantsInLevel.boardData[0] == 1)
		{
			PlantsInLevel.LoadPlant();
			Board.Instance.theSun = PlantsInLevel.boardData[1];
			Board.Instance.theCurrentSurvivalRound = PlantsInLevel.boardData[2];
			return true;
		}
		Debug.LogError("boardData error");
		return false;
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x0002B558 File Offset: 0x00029758
	public static void SavePlants()
	{
		PlantsInLevel.ClearPlant();
		foreach (GameObject gameObject in Board.Instance.plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				PlantsInLevel.AddPlant(component.thePlantColumn, component.thePlantRow, component.thePlantType);
			}
		}
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x0002B5B0 File Offset: 0x000297B0
	public static void AddPlant(int theColumn, int theRow, int thePlantType)
	{
		int i = 0;
		while (i < PlantsInLevel.plant.Length)
		{
			if (PlantsInLevel.plant[i] == 0)
			{
				PlantsInLevel.plant[i] = thePlantType + 1;
				PlantsInLevel.plant[i + 1] = theColumn;
				PlantsInLevel.plant[i + 2] = theRow;
				if (thePlantType == 12)
				{
					PlantsInLevel.plant[i + 3] = 1;
					return;
				}
				PlantsInLevel.plant[i + 3] = 0;
				return;
			}
			else
			{
				i += 4;
			}
		}
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x0002B614 File Offset: 0x00029814
	public static void LoadPlant()
	{
		for (int i = 0; i < PlantsInLevel.plant.Length; i += 4)
		{
			if (PlantsInLevel.plant[i] != 0 && PlantsInLevel.plant[i + 3] == 1)
			{
				CreatePlant.Instance.SetPlant(PlantsInLevel.plant[i + 1], PlantsInLevel.plant[i + 2], PlantsInLevel.plant[i] - 1, null, default(Vector2), false, 0f);
			}
		}
		for (int j = 0; j < PlantsInLevel.plant.Length; j += 4)
		{
			if (PlantsInLevel.plant[j] != 0)
			{
				CreatePlant.Instance.SetPlant(PlantsInLevel.plant[j + 1], PlantsInLevel.plant[j + 2], PlantsInLevel.plant[j] - 1, null, default(Vector2), false, 0f);
			}
		}
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x0002B6D0 File Offset: 0x000298D0
	public static void ClearPlant()
	{
		for (int i = 0; i < PlantsInLevel.plant.Length; i++)
		{
			PlantsInLevel.plant[i] = 0;
		}
	}

	// Token: 0x04000280 RID: 640
	public static int[] plant = new int[1024];

	// Token: 0x04000281 RID: 641
	public static int[] boardData = new int[3];

	// Token: 0x04000282 RID: 642
	public static int maxRound = 0;
}

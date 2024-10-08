using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F4 RID: 244
public class LevelData : MonoBehaviour
{
	// Token: 0x060004AE RID: 1198 RVA: 0x00026950 File Offset: 0x00024B50
	public static void AddPlant(Plant plant)
	{
		LevelData.PlantInTravel item = new LevelData.PlantInTravel
		{
			thePlantType = plant.thePlantType,
			thePlantRow = plant.thePlantRow,
			thePlantColumn = plant.thePlantColumn,
			thePlantHealth = plant.thePlantHealth
		};
		LevelData.plantInTravel.Add(item);
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x000269A6 File Offset: 0x00024BA6
	public static void ClearPlant()
	{
		LevelData.plantInTravel.Clear();
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x000269B4 File Offset: 0x00024BB4
	public static void LoadPlant()
	{
		foreach (LevelData.PlantInTravel plantInTravel in LevelData.plantInTravel)
		{
			GameObject gameObject;
			if (Board.Instance.roadNum == 6)
			{
				if (plantInTravel.thePlantRow == 2)
				{
					int theRow = Random.Range(2, 4);
					CreatePlant.Instance.SetPlant(plantInTravel.thePlantColumn, theRow, 12, null, default(Vector2), false, 0f);
					gameObject = CreatePlant.Instance.SetPlant(plantInTravel.thePlantColumn, theRow, plantInTravel.thePlantType, null, default(Vector2), false, 0f);
				}
				else if (plantInTravel.thePlantRow == 3)
				{
					gameObject = CreatePlant.Instance.SetPlant(plantInTravel.thePlantColumn, 4, plantInTravel.thePlantType, null, default(Vector2), false, 0f);
				}
				else if (plantInTravel.thePlantRow == 4)
				{
					gameObject = CreatePlant.Instance.SetPlant(plantInTravel.thePlantColumn, 5, plantInTravel.thePlantType, null, default(Vector2), false, 0f);
				}
				else
				{
					gameObject = CreatePlant.Instance.SetPlant(plantInTravel.thePlantColumn, plantInTravel.thePlantRow, plantInTravel.thePlantType, null, default(Vector2), false, 0f);
				}
			}
			else
			{
				gameObject = CreatePlant.Instance.SetPlant(plantInTravel.thePlantColumn, plantInTravel.thePlantRow, plantInTravel.thePlantType, null, default(Vector2), false, 0f);
			}
			if (gameObject != null)
			{
				gameObject.GetComponent<Plant>().thePlantHealth = plantInTravel.thePlantHealth;
			}
		}
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x00026B68 File Offset: 0x00024D68
	public static void SaveLevel(Board board)
	{
		LevelData.level.theSun = board.theSun;
		LevelData.level.theCurrentRound = board.theCurrentSurvivalRound;
		LevelData.level.theMaxRound = board.theSurvivalMaxRound;
	}

	// Token: 0x04000231 RID: 561
	public static List<LevelData.PlantInTravel> plantInTravel = new List<LevelData.PlantInTravel>();

	// Token: 0x04000232 RID: 562
	public static LevelData.LevelInfo level = default(LevelData.LevelInfo);

	// Token: 0x0200014A RID: 330
	public struct PlantInTravel
	{
		// Token: 0x04000485 RID: 1157
		public int thePlantType;

		// Token: 0x04000486 RID: 1158
		public int thePlantRow;

		// Token: 0x04000487 RID: 1159
		public int thePlantColumn;

		// Token: 0x04000488 RID: 1160
		public int thePlantHealth;
	}

	// Token: 0x0200014B RID: 331
	public struct LevelInfo
	{
		// Token: 0x04000489 RID: 1161
		public int theSun;

		// Token: 0x0400048A RID: 1162
		public int theCurrentRound;

		// Token: 0x0400048B RID: 1163
		public int theMaxRound;
	}
}

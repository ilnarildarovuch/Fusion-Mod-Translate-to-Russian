using System.Collections.Generic;
using UnityEngine;

public class CreatePlant : MonoBehaviour
{
	private Board board;

	public static CreatePlant Instance;

	private void Awake()
	{
		board = GetComponent<Board>();
		Instance = this;
	}

	public static GameObject SetPlantInAlmamac(Vector3 position, int theSeedType)
	{
		if (GameAPP.plantPrefab[theSeedType] == null)
		{
			return null;
		}
		GameObject gameObject = Object.Instantiate(GameAPP.plantPrefab[theSeedType], new Vector3(-10f, 0f, 0f), Quaternion.identity);
		Plant component = gameObject.GetComponent<Plant>();
		Vector3 position2 = component.shadow.transform.position;
		Vector3 vector = position - position2;
		gameObject.transform.position += vector;
		gameObject.GetComponent<Plant>().startPos = gameObject.transform.position;
		Object.Destroy(component);
		return gameObject;
	}

	public GameObject SetPlant(int theColumn, int theRow, int theSeedType, GameObject glovePlant = null, Vector2 puffV = default(Vector2), bool isFreeSet = false, float customX = 0f)
	{
		if (theSeedType == -1)
		{
			return null;
		}
		if (theColumn < 0 || theColumn > 9 || theRow < 0 || theRow > board.roadNum - 1)
		{
			return null;
		}
		float x = Mouse.Instance.GetBoxXFromColumn(theColumn);
		if (customX != 0f)
		{
			x = customX;
		}
		float num = Mouse.Instance.GetBoxYFromRow(theRow);
		if (GetPot(theColumn, theRow))
		{
			num += 0.25f;
		}
		Vector2 vector = new Vector2(x, num);
		if (puffV != default(Vector2))
		{
			vector = puffV;
			vector = new Vector2(vector.x, vector.y - 0.3f);
		}
		if (CheckBox(theColumn, theRow, theSeedType) || isFreeSet)
		{
			if (!board.isIZ)
			{
				if (theSeedType == 252)
				{
					GameAPP.PlaySound(Random.Range(81, 83));
				}
				else if (Board.Instance.boxType[theColumn, theRow] == 1)
				{
					GameAPP.PlaySound(71);
				}
				else
				{
					GameAPP.PlaySound(Random.Range(22, 24));
				}
			}
			GameObject gameObject;
			if (glovePlant == null)
			{
				gameObject = Object.Instantiate(GameAPP.plantPrefab[theSeedType], new Vector3(-10f, 0f, 0f), Quaternion.identity);
				gameObject.name = GameAPP.plantPrefab[theSeedType].name;
			}
			else
			{
				gameObject = glovePlant;
			}
			Plant component = gameObject.GetComponent<Plant>();
			component.thePlantType = theSeedType;
			component.thePlantColumn = theColumn;
			component.thePlantRow = theRow;
			component.board = board;
			component.adjustPosByLily = false;
			vector = new Vector2(vector.x, vector.y + 0.3f);
			if (theSeedType != 12)
			{
				SetLayer(theColumn, theRow, gameObject);
			}
			SetTransform(gameObject, vector);
			SetPlantAttributes(component);
			if (theSeedType != 255 && glovePlant == null)
			{
				for (int i = 0; i < board.plantArray.Length; i++)
				{
					if (board.plantArray[i] == null)
					{
						board.plantArray[i] = gameObject;
						break;
					}
				}
			}
			if (glovePlant == null && !board.isIZ)
			{
				UniqueEvent(theSeedType, gameObject, theRow);
			}
			if (!board.isIZ)
			{
				GameObject gameObject2 = ((board.boxType[theColumn, theRow] != 1) ? GameAPP.particlePrefab[1] : GameAPP.particlePrefab[32]);
				GameObject obj = Object.Instantiate(gameObject2, gameObject.transform.Find("Shadow").position, Quaternion.identity);
				obj.name = gameObject2.name;
				obj.transform.SetParent(base.gameObject.transform);
			}
			return gameObject;
		}
		GameObject gameObject3 = CheckMix(theColumn, theRow, theSeedType, glovePlant);
		if (gameObject3 != null)
		{
			if (glovePlant != null)
			{
				GetComponent<Mouse>().thePlantOnGlove = null;
				glovePlant.GetComponent<Plant>().Die();
			}
			return gameObject3;
		}
		return null;
	}

	public bool CheckBox(int theBoxColumn, int theBoxRow, int theSeedType)
	{
		if (Mouse.Instance.GetBoxXFromColumn(theBoxColumn) > Board.Instance.iceRoadX[theBoxRow])
		{
			return false;
		}
		if (GameAPP.theBoardType == 1 && GameAPP.theBoardLevel == 29 && !IsPuff(theSeedType))
		{
			return false;
		}
		GameObject[] griditemArray = board.griditemArray;
		foreach (GameObject gameObject in griditemArray)
		{
			if (gameObject != null)
			{
				GridItem component = gameObject.GetComponent<GridItem>();
				if (theBoxColumn == component.theItemColumn && theBoxRow == component.theItemRow)
				{
					return false;
				}
			}
		}
		if (IsPuff(theSeedType))
		{
			return CheckPuff(theBoxColumn, theBoxRow);
		}
		if (SpecialPlant(theSeedType) && Mouse.Instance.thePlantOnGlove == null)
		{
			return false;
		}
		if (board.boxType[theBoxColumn, theBoxRow] == 1)
		{
			if (OnHardLand(theSeedType))
			{
				return false;
			}
			if (!IsWaterPlant(theSeedType))
			{
				bool flag = false;
				griditemArray = board.plantArray;
				foreach (GameObject gameObject2 in griditemArray)
				{
					if (!(gameObject2 != null))
					{
						continue;
					}
					Plant component2 = gameObject2.GetComponent<Plant>();
					if (component2.thePlantRow != theBoxRow || component2.thePlantColumn != theBoxColumn)
					{
						continue;
					}
					if (component2.isLily)
					{
						flag = true;
						continue;
					}
					if (PresentCheck(theSeedType, component2))
					{
						return true;
					}
					return false;
				}
				if (flag)
				{
					return true;
				}
				return false;
			}
			griditemArray = board.plantArray;
			foreach (GameObject gameObject3 in griditemArray)
			{
				if (gameObject3 != null)
				{
					Plant component3 = gameObject3.GetComponent<Plant>();
					if (component3.thePlantRow == theBoxRow && component3.thePlantColumn == theBoxColumn)
					{
						return false;
					}
				}
			}
		}
		if (board.boxType[theBoxColumn, theBoxRow] == 0 && IsWaterPlant(theSeedType))
		{
			return false;
		}
		if (board.boxType[theBoxColumn, theBoxRow] == 2 && !TypeMgr.IsCaltrop(theSeedType) && !TypeMgr.IsNut(theSeedType) && !TypeMgr.IsPotatoMine(theSeedType))
		{
			return false;
		}
		griditemArray = board.plantArray;
		foreach (GameObject gameObject4 in griditemArray)
		{
			if (!(gameObject4 != null))
			{
				continue;
			}
			Plant component4 = gameObject4.GetComponent<Plant>();
			int thePlantColumn = component4.thePlantColumn;
			int thePlantRow = component4.thePlantRow;
			if (thePlantColumn == theBoxColumn && thePlantRow == theBoxRow)
			{
				if (PresentCheck(theSeedType, component4))
				{
					return true;
				}
				return false;
			}
		}
		return true;
	}

	public bool IsWaterPlant(int theSeedType)
	{
		switch (theSeedType)
		{
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
			return true;
		default:
			return false;
		}
	}

	private bool OnHardLand(int theSeedType)
	{
		switch (theSeedType)
		{
		case 4:
		case 17:
		case 1007:
		case 1009:
		case 1010:
		case 1015:
		case 1060:
		case 1061:
		case 1062:
		case 1063:
		case 1064:
		case 1074:
		case 1075:
			return true;
		default:
			return false;
		}
	}

	private bool SpecialPlant(int theSeedType)
	{
		switch (theSeedType)
		{
		case 1027:
		case 1060:
		case 1067:
		case 1070:
			return true;
		default:
			return false;
		}
	}

	private bool CheckPuff(int theColumn, int theRow)
	{
		int num = 0;
		GameObject[] plantArray = board.plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (!(gameObject != null))
			{
				continue;
			}
			Plant component = gameObject.GetComponent<Plant>();
			if (component.thePlantColumn == theColumn && component.thePlantRow == theRow)
			{
				if (IsPuff(component.thePlantType))
				{
					num++;
				}
				else if (!component.isLily)
				{
					return false;
				}
			}
		}
		if (num < 3)
		{
			return true;
		}
		return false;
	}

	private GameObject CheckMix(int theBoxColumn, int theBoxRow, int theSeedType, GameObject glovePlant)
	{
		List<Plant> list = new List<Plant>();
		GameObject[] plantArray = board.plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (!(gameObject != null))
			{
				continue;
			}
			Plant component = gameObject.GetComponent<Plant>();
			if ((!(glovePlant != null) || !(gameObject == glovePlant)) && component.thePlantColumn == theBoxColumn && component.thePlantRow == theBoxRow)
			{
				if (component.thePlantType == 256)
				{
					return null;
				}
				list.Add(component);
			}
		}
		foreach (Plant item in list)
		{
			int thePlantType = item.thePlantType;
			if (Lim(MixData.data[thePlantType, theSeedType]))
			{
				InGameText.INSTANCE.EnableText("通关挑战模式解锁配方", 7f);
				return null;
			}
			if (LimTravel(MixData.data[thePlantType, theSeedType]))
			{
				return null;
			}
			if (MixData.data[thePlantType, theSeedType] == 0)
			{
				continue;
			}
			if (GameAPP.theBoardType == 1 && GameAPP.theBoardLevel == 29 && !IsPuff(MixData.data[thePlantType, theSeedType]))
			{
				InGameText.INSTANCE.EnableText("只能融合小喷菇！", 7f);
				return null;
			}
			float attributeCountdown = 15f;
			if (thePlantType == 4)
			{
				attributeCountdown = item.gameObject.GetComponent<PotatoMine>().attributeCountdown;
			}
			if (thePlantType == 6)
			{
				return MixPuffEvent(theBoxColumn, theBoxRow, theSeedType);
			}
			item.Die();
			if (MixData.data[thePlantType, theSeedType] == 4)
			{
				BombPotato(item.gameObject, theBoxColumn, theBoxRow);
			}
			GameObject gameObject2 = SetPlant(theBoxColumn, theBoxRow, MixData.data[thePlantType, theSeedType], null, default(Vector2), isFreeSet: true);
			if (gameObject2 != null)
			{
				MixEvent(theSeedType, gameObject2, theBoxRow);
				if (gameObject2.TryGetComponent<PotatoMine>(out var component2))
				{
					component2.attributeCountdown = attributeCountdown;
				}
				return gameObject2;
			}
		}
		return null;
	}

	private void BombPotato(GameObject plant, int theBoxColumn, int theBoxRow)
	{
		SetPlant(theBoxColumn + 1, theBoxRow - 1, 4);
		SetPlant(theBoxColumn + 1, theBoxRow, 4);
		SetPlant(theBoxColumn + 1, theBoxRow + 1, 4);
		SetPlant(theBoxColumn - 1, theBoxRow - 1, 4);
		SetPlant(theBoxColumn - 1, theBoxRow, 4);
		SetPlant(theBoxColumn - 1, theBoxRow + 1, 4);
		SetPlant(theBoxColumn, theBoxRow + 1, 4);
		SetPlant(theBoxColumn, theBoxRow - 1, 4);
		GameObject gameObject = GameAPP.particlePrefab[2];
		Vector3 position = new Vector3(plant.transform.position.x, plant.transform.position.y + 0.5f, 0f);
		GameObject obj = Object.Instantiate(gameObject, position, Quaternion.identity, base.transform);
		obj.name = gameObject.name;
		obj.GetComponent<BombCherry>().bombRow = theBoxRow;
		ScreenShake.TriggerShake();
		GameAPP.PlaySound(40);
	}

	private GameObject MixPuffEvent(int column, int row, int theSeedType)
	{
		int num = 0;
		List<Plant> list = new List<Plant>();
		GameObject gameObject = null;
		bool flag = false;
		GameObject[] plantArray = board.plantArray;
		foreach (GameObject gameObject2 in plantArray)
		{
			if (!(gameObject2 != null))
			{
				continue;
			}
			Plant component = gameObject2.GetComponent<Plant>();
			if (component.thePlantRow != row || component.thePlantColumn != column)
			{
				continue;
			}
			if (component.thePlantType == 6)
			{
				num++;
				list.Add(component);
				continue;
			}
			flag = true;
			if (theSeedType == 7 || theSeedType == 9)
			{
				return null;
			}
		}
		foreach (Plant item in list)
		{
			if (item != null)
			{
				item.Die();
			}
		}
		if (theSeedType == 7 && !flag)
		{
			return PuffToFume(column, row, num);
		}
		if (theSeedType == 9 && !flag)
		{
			return PuffToScaredy(column, row, num);
		}
		for (int j = 0; j < num; j++)
		{
			GameObject gameObject3 = SetPlant(column, row, MixData.data[6, theSeedType]);
			if (gameObject3 != null)
			{
				gameObject = gameObject3;
			}
		}
		if (gameObject != null)
		{
			MixEvent(theSeedType, gameObject, row);
		}
		return gameObject;
	}

	private GameObject PuffToScaredy(int column, int row, int puffNum)
	{
		GameObject result = SetPlant(column, row, 9, null, default(Vector2), isFreeSet: true);
		switch (puffNum)
		{
		case 1:
			SetPlant(column + 1, row, 6);
			break;
		case 2:
			SetPlant(column + 1, row, 6);
			SetPlant(column, row + 1, 6);
			SetPlant(column, row - 1, 6);
			break;
		case 3:
			SetPlant(column + 1, row, 6);
			SetPlant(column + 1, row, 6);
			SetPlant(column, row + 1, 6);
			SetPlant(column, row - 1, 6);
			SetPlant(column, row + 1, 6);
			SetPlant(column, row - 1, 6);
			break;
		}
		return result;
	}

	private GameObject PuffToFume(int column, int row, int puffNum)
	{
		GameObject result = SetPlant(column, row, 7, null, default(Vector2), isFreeSet: true);
		switch (puffNum)
		{
		case 1:
			SetPlant(column + 1, row, 6);
			break;
		case 2:
			SetPlant(column + 1, row, 6);
			SetPlant(column + 1, row, 6);
			SetPlant(column + 2, row, 6);
			break;
		case 3:
			SetPlant(column + 1, row, 6);
			SetPlant(column + 1, row, 6);
			SetPlant(column + 1, row, 6);
			SetPlant(column + 2, row, 6);
			SetPlant(column + 2, row, 6);
			SetPlant(column + 3, row, 6);
			break;
		}
		return result;
	}

	private bool PresentCheck(int theSeedType, Plant p)
	{
		if (board.isIZ)
		{
			return false;
		}
		if (theSeedType == 256 && p.thePlantType == 256)
		{
			return false;
		}
		if (theSeedType == 256)
		{
			if (p.thePlantType >= 100)
			{
				switch (p.thePlantType)
				{
				case 1001:
				case 1004:
				case 1011:
				case 1012:
				case 1023:
				case 1024:
				case 1025:
				case 1030:
				case 1032:
					return true;
				case 1037:
				case 1040:
				case 1043:
					return true;
				case 1027:
					return true;
				case 1053:
					return true;
				case 1060:
					return true;
				case 1070:
					return true;
				default:
					return false;
				}
			}
			return true;
		}
		return false;
	}

	public bool GetPot(int thePotColumn, int thePotRow)
	{
		GameObject[] plantArray = board.plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (gameObject != null)
			{
				int thePlantColumn = gameObject.GetComponent<Plant>().thePlantColumn;
				int thePlantRow = gameObject.GetComponent<Plant>().thePlantRow;
				bool isPot = gameObject.GetComponent<Plant>().isPot;
				if (thePlantColumn == thePotColumn && thePlantRow == thePotRow && isPot)
				{
					return true;
				}
			}
		}
		return false;
	}

	private void SetLayer(int theColumn, int theRow, GameObject thePlant)
	{
		Plant component = thePlant.GetComponent<Plant>();
		int baseLayer = component.baseLayer;
		int baseLayer2 = (9 - theColumn) * 3000;
		int num = int.MinValue;
		bool flag = false;
		GameObject[] plantArray = board.plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (!(gameObject == thePlant) && !(gameObject == null))
			{
				Plant component2 = gameObject.GetComponent<Plant>();
				if (component2.thePlantRow == theRow && component2.thePlantColumn == theColumn && component2.baseLayer > num)
				{
					num = component2.baseLayer;
					flag = true;
				}
			}
		}
		if (flag)
		{
			baseLayer2 = num + 30;
		}
		component.baseLayer = baseLayer2;
		StartSetLayer(thePlant, baseLayer2, baseLayer, theRow);
	}

	private void StartSetLayer(GameObject obj, int baseLayer, int plantBaseLayer, int theRow)
	{
		if (!(obj.name == "Shadow"))
		{
			if (obj.TryGetComponent<SpriteRenderer>(out var component))
			{
				component.sortingOrder += baseLayer;
				component.sortingOrder -= plantBaseLayer;
				component.sortingLayerName = $"plant{theRow}";
			}
			if (obj.TryGetComponent<ParticleSystem>(out var component2))
			{
				Renderer component3 = component2.GetComponent<Renderer>();
				component3.sortingOrder += baseLayer;
				component3.sortingOrder -= plantBaseLayer;
				component3.sortingLayerName = $"plant{theRow}";
			}
			for (int i = 0; i < obj.transform.childCount; i++)
			{
				StartSetLayer(obj.transform.GetChild(i).gameObject, baseLayer, plantBaseLayer, theRow);
			}
		}
	}

	private void SetPlantAttributes(Plant plant)
	{
		plant.thePlantSpeed = Random.Range(0.9f, 1.1f);
		if (plant.thePlantAttackInterval != 0f)
		{
			plant.thePlantAttackCountDown = Random.Range(0.5f, 1.5f);
		}
		if (plant.thePlantProduceInterval != 0f)
		{
			plant.thePlantProduceCountDown = Random.Range(4f, 7f);
		}
	}

	private void SetTransform(GameObject plant, Vector3 position)
	{
		foreach (Transform item in plant.transform)
		{
			if (item.name == "Shadow")
			{
				Vector3 position2 = item.position;
				Vector3 vector = position - position2;
				plant.transform.position += vector;
				plant.GetComponent<Plant>().startPos = plant.transform.position;
			}
		}
		SetPuffTransform(plant);
		plant.transform.SetParent(GameAPP.board.transform);
	}

	public void SetPuffTransform(GameObject plant)
	{
		Plant component = plant.GetComponent<Plant>();
		if (!IsPuff(component.thePlantType))
		{
			return;
		}
		bool[] array = new bool[3];
		Vector3 position = plant.transform.position;
		GameObject[] plantArray = board.plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (gameObject != null)
			{
				Plant component2 = gameObject.GetComponent<Plant>();
				if (IsPuff(component2.thePlantType) && InTheSameBox(component, component2))
				{
					array[component2.place] = true;
				}
			}
		}
		for (int j = 0; j < array.Length; j++)
		{
			if (!array[j])
			{
				component.place = j;
				break;
			}
		}
		switch (component.place)
		{
		case 0:
			component.transform.position = new Vector3(position.x, position.y + 0.4f);
			SetPuffLayer(component.gameObject, isLower: true, component.thePlantRow);
			break;
		case 1:
			component.transform.position = new Vector3(position.x + 0.3f, position.y - 0.1f);
			SetPuffLayer(component.gameObject, isLower: false, component.thePlantRow);
			break;
		case 2:
			component.transform.position = new Vector3(position.x - 0.3f, position.y - 0.1f);
			SetPuffLayer(component.gameObject, isLower: false, component.thePlantRow);
			break;
		}
	}

	public void SetPuffLayer(GameObject plant, bool isLower, int theRow)
	{
		if (plant.name == "Shadow")
		{
			if (isLower)
			{
				plant.GetComponent<SpriteRenderer>().sortingLayerName = $"plantlow{theRow}";
			}
			else
			{
				plant.GetComponent<SpriteRenderer>().sortingLayerName = $"plantlow{theRow}";
			}
			return;
		}
		if (plant.TryGetComponent<SpriteRenderer>(out var component))
		{
			if (isLower)
			{
				component.sortingLayerName = $"plantlow{theRow}";
			}
			else
			{
				component.sortingLayerName = $"plant{theRow}";
			}
		}
		if (plant.transform.childCount == 0)
		{
			return;
		}
		foreach (Transform item in plant.transform)
		{
			if (item != null)
			{
				SetPuffLayer(item.gameObject, isLower, theRow);
			}
		}
	}

	private bool Lim(int theSeedType)
	{
		switch (theSeedType)
		{
		case 1005:
			if (GameAPP.theBoardType == 1)
			{
				int theBoardLevel = GameAPP.theBoardLevel;
				if ((uint)(theBoardLevel - 7) <= 2u)
				{
					return false;
				}
			}
			if (!GameAPP.clgLevelCompleted[7] || !GameAPP.clgLevelCompleted[8] || !GameAPP.clgLevelCompleted[9])
			{
				return true;
			}
			break;
		case 1013:
			if (GameAPP.theBoardType == 1)
			{
				int theBoardLevel = GameAPP.theBoardLevel;
				if ((uint)(theBoardLevel - 10) <= 2u)
				{
					return false;
				}
			}
			if (!GameAPP.clgLevelCompleted[10] || !GameAPP.clgLevelCompleted[11] || !GameAPP.clgLevelCompleted[12])
			{
				return true;
			}
			break;
		case 1026:
			if (GameAPP.theBoardType == 1)
			{
				int theBoardLevel = GameAPP.theBoardLevel;
				if ((uint)(theBoardLevel - 19) <= 2u)
				{
					return false;
				}
			}
			if (!GameAPP.clgLevelCompleted[19] || !GameAPP.clgLevelCompleted[20] || !GameAPP.clgLevelCompleted[21])
			{
				return true;
			}
			break;
		case 1046:
			if (GameAPP.theBoardType == 1)
			{
				int theBoardLevel = GameAPP.theBoardLevel;
				if ((uint)(theBoardLevel - 22) <= 2u)
				{
					return false;
				}
			}
			if (!GameAPP.clgLevelCompleted[22] || !GameAPP.clgLevelCompleted[23] || !GameAPP.clgLevelCompleted[24])
			{
				return true;
			}
			break;
		case 1052:
			if (GameAPP.theBoardType == 1 && GameAPP.theBoardLevel == 31)
			{
				return false;
			}
			if (!GameAPP.clgLevelCompleted[31])
			{
				return true;
			}
			break;
		case 1066:
			if (GameAPP.theBoardType == 1 && GameAPP.theBoardLevel == 32)
			{
				return false;
			}
			if (!GameAPP.clgLevelCompleted[32])
			{
				return true;
			}
			break;
		}
		return false;
	}

	private bool LimTravel(int theSeedType)
	{
		if (GameAPP.developerMode)
		{
			return false;
		}
		if (GameAPP.theBoardType == 1 && 0 < GameAPP.theBoardLevel && GameAPP.theBoardLevel < 7)
		{
			return false;
		}
		if (board.isTravel)
		{
			switch (theSeedType)
			{
			case 901:
				if (GameAPP.unlocked[0])
				{
					return false;
				}
				InGameText.INSTANCE.EnableText("尚未解锁配方", 7f);
				return true;
			case 902:
				if (GameAPP.unlocked[1])
				{
					return false;
				}
				InGameText.INSTANCE.EnableText("尚未解锁配方", 7f);
				return true;
			case 903:
				if (GameAPP.unlocked[2])
				{
					return false;
				}
				InGameText.INSTANCE.EnableText("尚未解锁配方", 7f);
				return true;
			case 904:
				if (GameAPP.unlocked[3])
				{
					return false;
				}
				InGameText.INSTANCE.EnableText("尚未解锁配方", 7f);
				return true;
			default:
				return false;
			}
		}
		if ((uint)(theSeedType - 900) <= 4u)
		{
			InGameText.INSTANCE.EnableText("该配方仅旅行模式可用", 7f);
			return true;
		}
		return false;
	}

	private void MixEvent(int theSeedType, GameObject plant, int theRow)
	{
		Plant component = plant.GetComponent<Plant>();
		Vector3 position = component.shadow.transform.position;
		position = new Vector3(position.x, position.y + 0.5f);
		switch (theSeedType)
		{
		case 2:
			Board.Instance.CreateExplode(position, theRow);
			break;
		case 10:
			Board.Instance.CreateFreeze(position);
			break;
		case 11:
		{
			Vector2 position2 = new Vector2(component.shadow.transform.position.x - 0.3f, component.shadow.transform.position.y + 0.3f);
			Board.Instance.SetDoom(component.thePlantColumn, component.thePlantRow, setPit: false, position2);
			break;
		}
		case 16:
			Board.Instance.CreateFireLine(theRow);
			break;
		}
	}

	private void UniqueEvent(int theSeedType, GameObject plant, int theRow)
	{
		Vector3 position = plant.GetComponent<Plant>().shadow.transform.position;
		position = new Vector3(position.x, position.y + 0.5f);
		switch (theSeedType)
		{
		case 1007:
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, position);
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, position);
			break;
		case 1009:
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, position);
			break;
		case 1015:
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, position);
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, position);
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, position);
			break;
		case 1058:
			if (theRow != 0)
			{
				Board.Instance.CreateFireLine(theRow - 1);
			}
			if (theRow != board.roadNum - 1)
			{
				Board.Instance.CreateFireLine(theRow + 1);
			}
			Board.Instance.CreateFireLine(theRow);
			break;
		}
	}

	public bool IsPuff(int theSeedType)
	{
		switch (theSeedType)
		{
		case 6:
		case 1018:
		case 1019:
		case 1021:
		case 1022:
		case 1031:
		case 1035:
		case 1036:
		case 1044:
		case 1065:
			return true;
		default:
			return false;
		}
	}

	public bool InTheSameBox(Plant p1, Plant p2)
	{
		if (p1.thePlantRow == p2.thePlantRow && p1.thePlantColumn == p2.thePlantColumn)
		{
			return true;
		}
		return false;
	}
}

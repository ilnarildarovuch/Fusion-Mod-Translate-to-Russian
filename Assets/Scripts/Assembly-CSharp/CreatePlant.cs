using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000054 RID: 84
public class CreatePlant : MonoBehaviour
{
	// Token: 0x06000182 RID: 386 RVA: 0x0000C19D File Offset: 0x0000A39D
	private void Awake()
	{
		this.board = base.GetComponent<Board>();
		CreatePlant.Instance = this;
	}

	// Token: 0x06000183 RID: 387 RVA: 0x0000C1B4 File Offset: 0x0000A3B4
	public static GameObject SetPlantInAlmamac(Vector3 position, int theSeedType)
	{
		if (GameAPP.plantPrefab[theSeedType] == null)
		{
			return null;
		}
		GameObject gameObject = Object.Instantiate<GameObject>(GameAPP.plantPrefab[theSeedType], new Vector3(-10f, 0f, 0f), Quaternion.identity);
		Plant component = gameObject.GetComponent<Plant>();
		Vector3 position2 = component.shadow.transform.position;
		Vector3 b = position - position2;
		gameObject.transform.position += b;
		gameObject.GetComponent<Plant>().startPos = gameObject.transform.position;
		Object.Destroy(component);
		return gameObject;
	}

	// Token: 0x06000184 RID: 388 RVA: 0x0000C24C File Offset: 0x0000A44C
	public GameObject SetPlant(int theColumn, int theRow, int theSeedType, GameObject glovePlant = null, Vector2 puffV = default(Vector2), bool isFreeSet = false, float customX = 0f)
	{
		if (theSeedType == -1)
		{
			return null;
		}
		if (theColumn < 0 || theColumn > 9 || theRow < 0 || theRow > this.board.roadNum - 1)
		{
			return null;
		}
		float x = Mouse.Instance.GetBoxXFromColumn(theColumn);
		if (customX != 0f)
		{
			x = customX;
		}
		float num = Mouse.Instance.GetBoxYFromRow(theRow);
		if (this.GetPot(theColumn, theRow))
		{
			num += 0.25f;
		}
		Vector2 vector = new Vector2(x, num);
		if (puffV != default(Vector2))
		{
			vector = puffV;
			vector = new Vector2(vector.x, vector.y - 0.3f);
		}
		if (this.CheckBox(theColumn, theRow, theSeedType) || isFreeSet)
		{
			if (!this.board.isIZ)
			{
				if (theSeedType == 252)
				{
					GameAPP.PlaySound(Random.Range(81, 83), 0.5f);
				}
				else if (Board.Instance.boxType[theColumn, theRow] == 1)
				{
					GameAPP.PlaySound(71, 0.5f);
				}
				else
				{
					GameAPP.PlaySound(Random.Range(22, 24), 0.5f);
				}
			}
			GameObject gameObject;
			if (glovePlant == null)
			{
				gameObject = Object.Instantiate<GameObject>(GameAPP.plantPrefab[theSeedType], new Vector3(-10f, 0f, 0f), Quaternion.identity);
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
			component.board = this.board;
			component.adjustPosByLily = false;
			vector = new Vector2(vector.x, vector.y + 0.3f);
			if (theSeedType != 12)
			{
				this.SetLayer(theColumn, theRow, gameObject);
			}
			this.SetTransform(gameObject, vector);
			this.SetPlantAttributes(component);
			if (theSeedType != 255 && glovePlant == null)
			{
				for (int i = 0; i < this.board.plantArray.Length; i++)
				{
					if (this.board.plantArray[i] == null)
					{
						this.board.plantArray[i] = gameObject;
						break;
					}
				}
			}
			if (glovePlant == null && !this.board.isIZ)
			{
				this.UniqueEvent(theSeedType, gameObject, theRow);
			}
			if (!this.board.isIZ)
			{
				GameObject gameObject2;
				if (this.board.boxType[theColumn, theRow] == 1)
				{
					gameObject2 = GameAPP.particlePrefab[32];
				}
				else
				{
					gameObject2 = GameAPP.particlePrefab[1];
				}
				GameObject gameObject3 = Object.Instantiate<GameObject>(gameObject2, gameObject.transform.Find("Shadow").position, Quaternion.identity);
				gameObject3.name = gameObject2.name;
				gameObject3.transform.SetParent(base.gameObject.transform);
			}
			return gameObject;
		}
		GameObject gameObject4 = this.CheckMix(theColumn, theRow, theSeedType, glovePlant);
		if (gameObject4 != null)
		{
			if (glovePlant != null)
			{
				base.GetComponent<Mouse>().thePlantOnGlove = null;
				glovePlant.GetComponent<Plant>().Die();
			}
			return gameObject4;
		}
		return null;
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0000C54C File Offset: 0x0000A74C
	public bool CheckBox(int theBoxColumn, int theBoxRow, int theSeedType)
	{
		if (Mouse.Instance.GetBoxXFromColumn(theBoxColumn) > Board.Instance.iceRoadX[theBoxRow])
		{
			return false;
		}
		if (GameAPP.theBoardType == 1 && GameAPP.theBoardLevel == 29 && !this.IsPuff(theSeedType))
		{
			return false;
		}
		foreach (GameObject gameObject in this.board.griditemArray)
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
		if (this.IsPuff(theSeedType))
		{
			return this.CheckPuff(theBoxColumn, theBoxRow);
		}
		if (this.SpecialPlant(theSeedType) && Mouse.Instance.thePlantOnGlove == null)
		{
			return false;
		}
		if (this.board.boxType[theBoxColumn, theBoxRow] == 1)
		{
			if (this.OnHardLand(theSeedType))
			{
				return false;
			}
			if (!this.IsWaterPlant(theSeedType))
			{
				bool result = false;
				foreach (GameObject gameObject2 in this.board.plantArray)
				{
					if (gameObject2 != null)
					{
						Plant component2 = gameObject2.GetComponent<Plant>();
						if (component2.thePlantRow == theBoxRow && component2.thePlantColumn == theBoxColumn)
						{
							if (!component2.isLily)
							{
								return this.PresentCheck(theSeedType, component2);
							}
							result = true;
						}
					}
				}
				return result;
			}
			foreach (GameObject gameObject3 in this.board.plantArray)
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
		if (this.board.boxType[theBoxColumn, theBoxRow] == 0 && this.IsWaterPlant(theSeedType))
		{
			return false;
		}
		if (this.board.boxType[theBoxColumn, theBoxRow] == 2 && !TypeMgr.IsCaltrop(theSeedType) && !TypeMgr.IsNut(theSeedType) && !TypeMgr.IsPotatoMine(theSeedType))
		{
			return false;
		}
		foreach (GameObject gameObject4 in this.board.plantArray)
		{
			if (gameObject4 != null)
			{
				Plant component4 = gameObject4.GetComponent<Plant>();
				int thePlantColumn = component4.thePlantColumn;
				int thePlantRow = component4.thePlantRow;
				if (thePlantColumn == theBoxColumn && thePlantRow == theBoxRow)
				{
					return this.PresentCheck(theSeedType, component4);
				}
			}
		}
		return true;
	}

	// Token: 0x06000186 RID: 390 RVA: 0x0000C78D File Offset: 0x0000A98D
	public bool IsWaterPlant(int theSeedType)
	{
		if (theSeedType <= 252)
		{
			if (theSeedType != 12 && theSeedType != 15 && theSeedType != 252)
			{
				return false;
			}
		}
		else if (theSeedType - 1049 > 2 && theSeedType != 1056 && theSeedType - 1066 > 3)
		{
			return false;
		}
		return true;
	}

	// Token: 0x06000187 RID: 391 RVA: 0x0000C7CC File Offset: 0x0000A9CC
	private bool OnHardLand(int theSeedType)
	{
		if (theSeedType <= 1007)
		{
			if (theSeedType != 4 && theSeedType != 17 && theSeedType != 1007)
			{
				return false;
			}
		}
		else if (theSeedType <= 1015)
		{
			if (theSeedType - 1009 > 1 && theSeedType != 1015)
			{
				return false;
			}
		}
		else if (theSeedType - 1060 > 4 && theSeedType - 1074 > 1)
		{
			return false;
		}
		return true;
	}

	// Token: 0x06000188 RID: 392 RVA: 0x0000C827 File Offset: 0x0000AA27
	private bool SpecialPlant(int theSeedType)
	{
		if (theSeedType <= 1060)
		{
			if (theSeedType != 1027 && theSeedType != 1060)
			{
				return false;
			}
		}
		else if (theSeedType != 1067 && theSeedType != 1070)
		{
			return false;
		}
		return true;
	}

	// Token: 0x06000189 RID: 393 RVA: 0x0000C858 File Offset: 0x0000AA58
	private bool CheckPuff(int theColumn, int theRow)
	{
		int num = 0;
		foreach (GameObject gameObject in this.board.plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				if (component.thePlantColumn == theColumn && component.thePlantRow == theRow)
				{
					if (this.IsPuff(component.thePlantType))
					{
						num++;
					}
					else if (!component.isLily)
					{
						return false;
					}
				}
			}
		}
		return num < 3;
	}

	// Token: 0x0600018A RID: 394 RVA: 0x0000C8D4 File Offset: 0x0000AAD4
	private GameObject CheckMix(int theBoxColumn, int theBoxRow, int theSeedType, GameObject glovePlant)
	{
		List<Plant> list = new List<Plant>();
		foreach (GameObject gameObject in this.board.plantArray)
		{
			if (gameObject != null)
			{
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
		}
		foreach (Plant plant in list)
		{
			int thePlantType = plant.thePlantType;
			if (this.Lim(MixData.data[thePlantType, theSeedType]))
			{
				InGameText.INSTANCE.EnableText("通关挑战模式解锁配方", 7f);
				return null;
			}
			if (this.LimTravel(MixData.data[thePlantType, theSeedType]))
			{
				return null;
			}
			if (MixData.data[thePlantType, theSeedType] != 0)
			{
				if (GameAPP.theBoardType == 1 && GameAPP.theBoardLevel == 29 && !this.IsPuff(MixData.data[thePlantType, theSeedType]))
				{
					InGameText.INSTANCE.EnableText("只能融合小喷菇！", 7f);
					return null;
				}
				float attributeCountdown = 15f;
				if (thePlantType == 4)
				{
					attributeCountdown = plant.gameObject.GetComponent<PotatoMine>().attributeCountdown;
				}
				if (thePlantType == 6)
				{
					return this.MixPuffEvent(theBoxColumn, theBoxRow, theSeedType);
				}
				plant.Die();
				if (MixData.data[thePlantType, theSeedType] == 4)
				{
					this.BombPotato(plant.gameObject, theBoxColumn, theBoxRow);
				}
				GameObject gameObject2 = this.SetPlant(theBoxColumn, theBoxRow, MixData.data[thePlantType, theSeedType], null, default(Vector2), true, 0f);
				if (gameObject2 != null)
				{
					this.MixEvent(theSeedType, gameObject2, theBoxRow);
					PotatoMine potatoMine;
					if (gameObject2.TryGetComponent<PotatoMine>(out potatoMine))
					{
						potatoMine.attributeCountdown = attributeCountdown;
					}
					return gameObject2;
				}
			}
		}
		return null;
	}

	// Token: 0x0600018B RID: 395 RVA: 0x0000CB04 File Offset: 0x0000AD04
	private void BombPotato(GameObject plant, int theBoxColumn, int theBoxRow)
	{
		this.SetPlant(theBoxColumn + 1, theBoxRow - 1, 4, null, default(Vector2), false, 0f);
		this.SetPlant(theBoxColumn + 1, theBoxRow, 4, null, default(Vector2), false, 0f);
		this.SetPlant(theBoxColumn + 1, theBoxRow + 1, 4, null, default(Vector2), false, 0f);
		this.SetPlant(theBoxColumn - 1, theBoxRow - 1, 4, null, default(Vector2), false, 0f);
		this.SetPlant(theBoxColumn - 1, theBoxRow, 4, null, default(Vector2), false, 0f);
		this.SetPlant(theBoxColumn - 1, theBoxRow + 1, 4, null, default(Vector2), false, 0f);
		this.SetPlant(theBoxColumn, theBoxRow + 1, 4, null, default(Vector2), false, 0f);
		this.SetPlant(theBoxColumn, theBoxRow - 1, 4, null, default(Vector2), false, 0f);
		GameObject gameObject = GameAPP.particlePrefab[2];
		Vector3 position = new Vector3(plant.transform.position.x, plant.transform.position.y + 0.5f, 0f);
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, position, Quaternion.identity, base.transform);
		gameObject2.name = gameObject.name;
		gameObject2.GetComponent<BombCherry>().bombRow = theBoxRow;
		ScreenShake.TriggerShake(0.15f);
		GameAPP.PlaySound(40, 0.5f);
	}

	// Token: 0x0600018C RID: 396 RVA: 0x0000CC74 File Offset: 0x0000AE74
	private GameObject MixPuffEvent(int column, int row, int theSeedType)
	{
		int num = 0;
		List<Plant> list = new List<Plant>();
		GameObject gameObject = null;
		bool flag = false;
		foreach (GameObject gameObject2 in this.board.plantArray)
		{
			if (gameObject2 != null)
			{
				Plant component = gameObject2.GetComponent<Plant>();
				if (component.thePlantRow == row && component.thePlantColumn == column)
				{
					if (component.thePlantType == 6)
					{
						num++;
						list.Add(component);
					}
					else
					{
						flag = true;
						if (theSeedType == 7 || theSeedType == 9)
						{
							return null;
						}
					}
				}
			}
		}
		foreach (Plant plant in list)
		{
			if (plant != null)
			{
				plant.Die();
			}
		}
		if (theSeedType == 7 && !flag)
		{
			return this.PuffToFume(column, row, num);
		}
		if (theSeedType == 9 && !flag)
		{
			return this.PuffToScaredy(column, row, num);
		}
		for (int j = 0; j < num; j++)
		{
			GameObject gameObject3 = this.SetPlant(column, row, MixData.data[6, theSeedType], null, default(Vector2), false, 0f);
			if (gameObject3 != null)
			{
				gameObject = gameObject3;
			}
		}
		if (gameObject != null)
		{
			this.MixEvent(theSeedType, gameObject, row);
		}
		return gameObject;
	}

	// Token: 0x0600018D RID: 397 RVA: 0x0000CDC8 File Offset: 0x0000AFC8
	private GameObject PuffToScaredy(int column, int row, int puffNum)
	{
		GameObject result = this.SetPlant(column, row, 9, null, default(Vector2), true, 0f);
		switch (puffNum)
		{
		case 1:
			this.SetPlant(column + 1, row, 6, null, default(Vector2), false, 0f);
			break;
		case 2:
			this.SetPlant(column + 1, row, 6, null, default(Vector2), false, 0f);
			this.SetPlant(column, row + 1, 6, null, default(Vector2), false, 0f);
			this.SetPlant(column, row - 1, 6, null, default(Vector2), false, 0f);
			break;
		case 3:
			this.SetPlant(column + 1, row, 6, null, default(Vector2), false, 0f);
			this.SetPlant(column + 1, row, 6, null, default(Vector2), false, 0f);
			this.SetPlant(column, row + 1, 6, null, default(Vector2), false, 0f);
			this.SetPlant(column, row - 1, 6, null, default(Vector2), false, 0f);
			this.SetPlant(column, row + 1, 6, null, default(Vector2), false, 0f);
			this.SetPlant(column, row - 1, 6, null, default(Vector2), false, 0f);
			break;
		}
		return result;
	}

	// Token: 0x0600018E RID: 398 RVA: 0x0000CF2C File Offset: 0x0000B12C
	private GameObject PuffToFume(int column, int row, int puffNum)
	{
		GameObject result = this.SetPlant(column, row, 7, null, default(Vector2), true, 0f);
		switch (puffNum)
		{
		case 1:
			this.SetPlant(column + 1, row, 6, null, default(Vector2), false, 0f);
			break;
		case 2:
			this.SetPlant(column + 1, row, 6, null, default(Vector2), false, 0f);
			this.SetPlant(column + 1, row, 6, null, default(Vector2), false, 0f);
			this.SetPlant(column + 2, row, 6, null, default(Vector2), false, 0f);
			break;
		case 3:
			this.SetPlant(column + 1, row, 6, null, default(Vector2), false, 0f);
			this.SetPlant(column + 1, row, 6, null, default(Vector2), false, 0f);
			this.SetPlant(column + 1, row, 6, null, default(Vector2), false, 0f);
			this.SetPlant(column + 2, row, 6, null, default(Vector2), false, 0f);
			this.SetPlant(column + 2, row, 6, null, default(Vector2), false, 0f);
			this.SetPlant(column + 3, row, 6, null, default(Vector2), false, 0f);
			break;
		}
		return result;
	}

	// Token: 0x0600018F RID: 399 RVA: 0x0000D090 File Offset: 0x0000B290
	private bool PresentCheck(int theSeedType, Plant p)
	{
		if (this.board.isIZ)
		{
			return false;
		}
		if (theSeedType == 256 && p.thePlantType == 256)
		{
			return false;
		}
		if (theSeedType != 256)
		{
			return false;
		}
		if (p.thePlantType >= 100)
		{
			int thePlantType = p.thePlantType;
			if (thePlantType <= 1037)
			{
				if (thePlantType <= 1004)
				{
					if (thePlantType != 1001 && thePlantType != 1004)
					{
						return false;
					}
				}
				else if (thePlantType - 1011 > 1)
				{
					switch (thePlantType)
					{
					case 1023:
					case 1024:
					case 1025:
					case 1030:
					case 1032:
						break;
					case 1026:
					case 1028:
					case 1029:
					case 1031:
						return false;
					case 1027:
						return true;
					default:
						if (thePlantType != 1037)
						{
							return false;
						}
						return true;
					}
				}
				return true;
			}
			if (thePlantType <= 1043)
			{
				if (thePlantType != 1040 && thePlantType != 1043)
				{
					return false;
				}
			}
			else
			{
				if (thePlantType == 1053)
				{
					return true;
				}
				if (thePlantType == 1060)
				{
					return true;
				}
				if (thePlantType != 1070)
				{
					return false;
				}
				return true;
			}
			return true;
		}
		return true;
	}

	// Token: 0x06000190 RID: 400 RVA: 0x0000D19C File Offset: 0x0000B39C
	public bool GetPot(int thePotColumn, int thePotRow)
	{
		foreach (GameObject gameObject in this.board.plantArray)
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

	// Token: 0x06000191 RID: 401 RVA: 0x0000D20C File Offset: 0x0000B40C
	private void SetLayer(int theColumn, int theRow, GameObject thePlant)
	{
		Plant component = thePlant.GetComponent<Plant>();
		int baseLayer = component.baseLayer;
		int baseLayer2 = (9 - theColumn) * 3000;
		int num = int.MinValue;
		bool flag = false;
		foreach (GameObject gameObject in this.board.plantArray)
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
		this.StartSetLayer(thePlant, baseLayer2, baseLayer, theRow);
	}

	// Token: 0x06000192 RID: 402 RVA: 0x0000D2C4 File Offset: 0x0000B4C4
	private void StartSetLayer(GameObject obj, int baseLayer, int plantBaseLayer, int theRow)
	{
		if (obj.name == "Shadow")
		{
			return;
		}
		SpriteRenderer spriteRenderer;
		if (obj.TryGetComponent<SpriteRenderer>(out spriteRenderer))
		{
			spriteRenderer.sortingOrder += baseLayer;
			spriteRenderer.sortingOrder -= plantBaseLayer;
			spriteRenderer.sortingLayerName = string.Format("plant{0}", theRow);
		}
		ParticleSystem particleSystem;
		if (obj.TryGetComponent<ParticleSystem>(out particleSystem))
		{
			Renderer component = particleSystem.GetComponent<Renderer>();
			component.sortingOrder += baseLayer;
			component.sortingOrder -= plantBaseLayer;
			component.sortingLayerName = string.Format("plant{0}", theRow);
		}
		for (int i = 0; i < obj.transform.childCount; i++)
		{
			this.StartSetLayer(obj.transform.GetChild(i).gameObject, baseLayer, plantBaseLayer, theRow);
		}
	}

	// Token: 0x06000193 RID: 403 RVA: 0x0000D394 File Offset: 0x0000B594
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

	// Token: 0x06000194 RID: 404 RVA: 0x0000D3FC File Offset: 0x0000B5FC
	private void SetTransform(GameObject plant, Vector3 position)
	{
		foreach (object obj in plant.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.name == "Shadow")
			{
				Vector3 position2 = transform.position;
				Vector3 b = position - position2;
				plant.transform.position += b;
				plant.GetComponent<Plant>().startPos = plant.transform.position;
			}
		}
		this.SetPuffTransform(plant);
		plant.transform.SetParent(GameAPP.board.transform);
	}

	// Token: 0x06000195 RID: 405 RVA: 0x0000D4BC File Offset: 0x0000B6BC
	public void SetPuffTransform(GameObject plant)
	{
		Plant component = plant.GetComponent<Plant>();
		if (this.IsPuff(component.thePlantType))
		{
			bool[] array = new bool[3];
			Vector3 position = plant.transform.position;
			foreach (GameObject gameObject in this.board.plantArray)
			{
				if (gameObject != null)
				{
					Plant component2 = gameObject.GetComponent<Plant>();
					if (this.IsPuff(component2.thePlantType) && this.InTheSameBox(component, component2))
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
				this.SetPuffLayer(component.gameObject, true, component.thePlantRow);
				return;
			case 1:
				component.transform.position = new Vector3(position.x + 0.3f, position.y - 0.1f);
				this.SetPuffLayer(component.gameObject, false, component.thePlantRow);
				return;
			case 2:
				component.transform.position = new Vector3(position.x - 0.3f, position.y - 0.1f);
				this.SetPuffLayer(component.gameObject, false, component.thePlantRow);
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x06000196 RID: 406 RVA: 0x0000D63C File Offset: 0x0000B83C
	public void SetPuffLayer(GameObject plant, bool isLower, int theRow)
	{
		if (!(plant.name == "Shadow"))
		{
			SpriteRenderer spriteRenderer;
			if (plant.TryGetComponent<SpriteRenderer>(out spriteRenderer))
			{
				if (isLower)
				{
					spriteRenderer.sortingLayerName = string.Format("plantlow{0}", theRow);
				}
				else
				{
					spriteRenderer.sortingLayerName = string.Format("plant{0}", theRow);
				}
			}
			if (plant.transform.childCount != 0)
			{
				foreach (object obj in plant.transform)
				{
					Transform transform = (Transform)obj;
					if (transform != null)
					{
						this.SetPuffLayer(transform.gameObject, isLower, theRow);
					}
				}
			}
			return;
		}
		if (isLower)
		{
			plant.GetComponent<SpriteRenderer>().sortingLayerName = string.Format("plantlow{0}", theRow);
			return;
		}
		plant.GetComponent<SpriteRenderer>().sortingLayerName = string.Format("plantlow{0}", theRow);
	}

	// Token: 0x06000197 RID: 407 RVA: 0x0000D73C File Offset: 0x0000B93C
	private bool Lim(int theSeedType)
	{
		if (theSeedType <= 1026)
		{
			if (theSeedType != 1005)
			{
				if (theSeedType != 1013)
				{
					if (theSeedType == 1026)
					{
						if (GameAPP.theBoardType == 1)
						{
							int theBoardLevel = GameAPP.theBoardLevel;
							if (theBoardLevel - 19 <= 2)
							{
								return false;
							}
						}
						if (!GameAPP.clgLevelCompleted[19] || !GameAPP.clgLevelCompleted[20] || !GameAPP.clgLevelCompleted[21])
						{
							return true;
						}
					}
				}
				else
				{
					if (GameAPP.theBoardType == 1)
					{
						int theBoardLevel = GameAPP.theBoardLevel;
						if (theBoardLevel - 10 <= 2)
						{
							return false;
						}
					}
					if (!GameAPP.clgLevelCompleted[10] || !GameAPP.clgLevelCompleted[11] || !GameAPP.clgLevelCompleted[12])
					{
						return true;
					}
				}
			}
			else
			{
				if (GameAPP.theBoardType == 1)
				{
					int theBoardLevel = GameAPP.theBoardLevel;
					if (theBoardLevel - 7 <= 2)
					{
						return false;
					}
				}
				if (!GameAPP.clgLevelCompleted[7] || !GameAPP.clgLevelCompleted[8] || !GameAPP.clgLevelCompleted[9])
				{
					return true;
				}
			}
		}
		else if (theSeedType != 1046)
		{
			if (theSeedType != 1052)
			{
				if (theSeedType == 1066)
				{
					if (GameAPP.theBoardType == 1 && GameAPP.theBoardLevel == 32)
					{
						return false;
					}
					if (!GameAPP.clgLevelCompleted[32])
					{
						return true;
					}
				}
			}
			else
			{
				if (GameAPP.theBoardType == 1 && GameAPP.theBoardLevel == 31)
				{
					return false;
				}
				if (!GameAPP.clgLevelCompleted[31])
				{
					return true;
				}
			}
		}
		else
		{
			if (GameAPP.theBoardType == 1)
			{
				int theBoardLevel = GameAPP.theBoardLevel;
				if (theBoardLevel - 22 <= 2)
				{
					return false;
				}
			}
			if (!GameAPP.clgLevelCompleted[22] || !GameAPP.clgLevelCompleted[23] || !GameAPP.clgLevelCompleted[24])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000198 RID: 408 RVA: 0x0000D8B8 File Offset: 0x0000BAB8
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
		if (this.board.isTravel)
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
		else
		{
			if (theSeedType - 900 <= 4)
			{
				InGameText.INSTANCE.EnableText("该配方仅旅行模式可用", 7f);
				return true;
			}
			return false;
		}
	}

	// Token: 0x06000199 RID: 409 RVA: 0x0000D9C0 File Offset: 0x0000BBC0
	private void MixEvent(int theSeedType, GameObject plant, int theRow)
	{
		Plant component = plant.GetComponent<Plant>();
		Vector3 position = component.shadow.transform.position;
		position = new Vector3(position.x, position.y + 0.5f);
		if (theSeedType <= 10)
		{
			if (theSeedType == 2)
			{
				Board.Instance.CreateExplode(position, theRow);
				return;
			}
			if (theSeedType != 10)
			{
				return;
			}
			Board.Instance.CreateFreeze(position);
			return;
		}
		else
		{
			if (theSeedType == 11)
			{
				Vector2 position2 = new Vector2(component.shadow.transform.position.x - 0.3f, component.shadow.transform.position.y + 0.3f);
				Board.Instance.SetDoom(component.thePlantColumn, component.thePlantRow, false, position2);
				return;
			}
			if (theSeedType != 16)
			{
				return;
			}
			Board.Instance.CreateFireLine(theRow);
			return;
		}
	}

	// Token: 0x0600019A RID: 410 RVA: 0x0000DA9C File Offset: 0x0000BC9C
	private void UniqueEvent(int theSeedType, GameObject plant, int theRow)
	{
		Vector3 position = plant.GetComponent<Plant>().shadow.transform.position;
		position = new Vector3(position.x, position.y + 0.5f);
		if (theSeedType <= 1009)
		{
			if (theSeedType == 1007)
			{
				CreateCoin.Instance.SetCoin(0, 0, 0, 0, position);
				CreateCoin.Instance.SetCoin(0, 0, 0, 0, position);
				return;
			}
			if (theSeedType != 1009)
			{
				return;
			}
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, position);
			return;
		}
		else
		{
			if (theSeedType == 1015)
			{
				CreateCoin.Instance.SetCoin(0, 0, 0, 0, position);
				CreateCoin.Instance.SetCoin(0, 0, 0, 0, position);
				CreateCoin.Instance.SetCoin(0, 0, 0, 0, position);
				return;
			}
			if (theSeedType != 1058)
			{
				return;
			}
			if (theRow != 0)
			{
				Board.Instance.CreateFireLine(theRow - 1);
			}
			if (theRow != this.board.roadNum - 1)
			{
				Board.Instance.CreateFireLine(theRow + 1);
			}
			Board.Instance.CreateFireLine(theRow);
			return;
		}
	}

	// Token: 0x0600019B RID: 411 RVA: 0x0000DBA0 File Offset: 0x0000BDA0
	public bool IsPuff(int theSeedType)
	{
		if (theSeedType <= 1022)
		{
			if (theSeedType != 6 && theSeedType - 1018 > 1 && theSeedType - 1021 > 1)
			{
				return false;
			}
		}
		else if (theSeedType <= 1036)
		{
			if (theSeedType != 1031 && theSeedType - 1035 > 1)
			{
				return false;
			}
		}
		else if (theSeedType != 1044 && theSeedType != 1065)
		{
			return false;
		}
		return true;
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0000DBFE File Offset: 0x0000BDFE
	public bool InTheSameBox(Plant p1, Plant p2)
	{
		return p1.thePlantRow == p2.thePlantRow && p1.thePlantColumn == p2.thePlantColumn;
	}

	// Token: 0x04000114 RID: 276
	private Board board;

	// Token: 0x04000115 RID: 277
	public static CreatePlant Instance;
}

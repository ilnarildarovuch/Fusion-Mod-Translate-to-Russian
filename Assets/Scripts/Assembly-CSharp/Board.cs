using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
	public static Board Instance;

	public GameObject[] plantArray = new GameObject[256];

	public GameObject[] bulletArray = new GameObject[1024];

	public List<GameObject> zombieArray = new List<GameObject>();

	public GameObject[] coinArray = new GameObject[1024];

	public GameObject[] griditemArray = new GameObject[256];

	public GameObject[] mowerArray = new GameObject[6];

	public GameObject[] fireLineArray = new GameObject[6];

	public float[] iceRoadX = new float[6] { 15f, 15f, 15f, 15f, 15f, 15f };

	public float[] iceRoadFadeTime = new float[6];

	public int[] roadType = new int[6];

	public int[,] boxType = new int[10, 6];

	public int theSun = 500;

	public int theCurrentNumOfZombieUncontroled;

	public int theTotalNumOfZombie;

	public int theTotalNumOfCoin;

	public float time;

	public float theFallingSunCountDown = 7.5f;

	public float newZombieWaveCountDown = 15f;

	public float nextZombieWaveCountDown = 30f;

	private float hugeWaveCountDown;

	private bool isHugeWave;

	public int theWave;

	public int theMaxWave;

	public int theSurvivalMaxRound;

	public int theCurrentSurvivalRound;

	public bool isEndless;

	public bool isTravel;

	public float zombieTotalHealth;

	public float zombieCurrentHealth;

	public float zombieHealthUpdater;

	public int musicType;

	public float holdOnTime;

	public float iceDoomFreezeTime;

	public bool isIZ;

	public bool isNight;

	public int roadNum = 5;

	public GameObject theInGameUI;

	public CreateZombie createZombie;

	public CreatePlant createPlant;

	public bool droppedAwardOrOver;

	public InGameText inGameText;

	public bool freeCD;

	public bool isEveStart;

	public bool isEveStarted;

	private float eveCountDown;

	private float eveCurrentTime;

	public bool[] disAllowSetZombie = new bool[5];

	public bool isAutoEve;

	public bool isScaredyDream;

	public bool isTowerDefense;

	private void Awake()
	{
		Instance = this;
		if (GameAPP.developerMode)
		{
			freeCD = true;
		}
		createZombie = this.AddComponent<CreateZombie>();
		createPlant = this.AddComponent<CreatePlant>();
		this.AddComponent<CreateCoin>();
		this.AddComponent<CreateBullet>();
		this.AddComponent<Mouse>();
		this.AddComponent<CreateMower>();
		this.AddComponent<PoolMgr>();
		inGameText = InGameText.Instance().GetComponent<InGameText>();
		switch (UIMgr.GetSceneType(GameAPP.theBoardType, GameAPP.theBoardLevel))
		{
		case 1:
			theSun = 150;
			isNight = true;
			break;
		case 2:
		{
			theSun = 250;
			roadNum = 6;
			roadType[2] = 1;
			roadType[3] = 1;
			for (int i = 0; i < boxType.GetLength(0); i++)
			{
				boxType[i, 2] = 1;
				boxType[i, 3] = 1;
			}
			break;
		}
		}
		if (GameAPP.theBoardType == 1)
		{
			switch (GameAPP.theBoardLevel)
			{
			case 25:
			case 26:
				isScaredyDream = true;
				break;
			case 35:
				BoxMgr.SetBox();
				isTowerDefense = true;
				break;
			}
		}
		if (GameAPP.theBoardType == 2)
		{
			isNight = true;
		}
	}

	private void Start()
	{
		if (isScaredyDream)
		{
			createPlant.SetPlant(0, 3, 9);
		}
		if (isIZ)
		{
			if (GameAPP.theBoardLevel == 1)
			{
				if (!isAutoEve)
				{
					for (int i = 0; i < 5; i++)
					{
						for (int j = 0; j < 5; j++)
						{
							SetEvePlants(i, j);
						}
					}
				}
			}
			else
			{
				MixData.SetPlants(GameAPP.theBoardLevel);
			}
		}
		SetPrePlants();
	}

	private void Update()
	{
		time += Time.deltaTime;
		if (GameAPP.theBoardLevel != -1 && GameAPP.theGameStatus == 0)
		{
			IceRoadUpdate();
			if (isIZ && isEveStart)
			{
				EveUpdate();
			}
			else
			{
				LevelUpdate();
			}
		}
		if (GameAPP.developerMode)
		{
			theSun = 10000;
		}
	}

	private void EveUpdate()
	{
		eveCountDown -= Time.deltaTime;
		eveCurrentTime += Time.deltaTime;
		if (eveCountDown < 0f)
		{
			SetRandomZombies();
			if (eveCurrentTime < 120f)
			{
				eveCountDown = 8f;
			}
			else if (eveCurrentTime < 240f)
			{
				eveCountDown = 7f;
			}
			else if (eveCurrentTime < 360f)
			{
				eveCountDown = 6f;
			}
			else if (eveCurrentTime < 480f)
			{
				eveCountDown = 5f;
			}
			else if (eveCurrentTime < 600f)
			{
				eveCountDown = 4f;
			}
			else if (eveCurrentTime < 720f)
			{
				eveCountDown = 3f;
			}
			else
			{
				eveCountDown = 2f;
			}
		}
	}

	private void SetRandomZombies()
	{
		for (int i = 0; i < 5; i++)
		{
			if (!disAllowSetZombie[i])
			{
				createZombie.SetZombie(0, i, 105);
			}
		}
	}

	public void SetEvePlants(int i, int j)
	{
		while (true)
		{
			int num = Random.Range(998, 1076);
			switch (num)
			{
			case 1:
			case 1002:
			case 1009:
			case 1031:
			case 1033:
			case 1036:
			case 1040:
			case 1041:
			case 1043:
			case 1045:
			case 1048:
			case 1054:
			case 1057:
			case 1058:
			case 1059:
				continue;
			}
			if (i == 0)
			{
				while (true)
				{
					num = Random.Range(998, 1076);
					switch (num)
					{
					case 1:
					case 18:
					case 1002:
					case 1003:
					case 1006:
					case 1009:
					case 1010:
					case 1011:
					case 1012:
					case 1013:
					case 1014:
					case 1015:
					case 1016:
					case 1021:
					case 1027:
					case 1028:
					case 1029:
					case 1031:
					case 1033:
					case 1036:
					case 1039:
					case 1040:
					case 1041:
					case 1043:
					case 1045:
					case 1048:
					case 1052:
					case 1053:
					case 1054:
					case 1055:
					case 1057:
					case 1058:
					case 1059:
					case 1060:
						continue;
					}
					break;
				}
			}
			if (num == 999)
			{
				num = 253;
			}
			if (num == 998)
			{
				num = 900;
			}
			if (!CreatePlant.Instance.IsWaterPlant(num))
			{
				if (createPlant.IsPuff(num))
				{
					createPlant.SetPlant(i, j, num);
					createPlant.SetPlant(i, j, num);
				}
				GameObject gameObject = createPlant.SetPlant(i, j, num, null, default(Vector2), isFreeSet: true);
				if (gameObject != null && gameObject.TryGetComponent<PotatoMine>(out var component))
				{
					component.attributeCountdown = 0f;
				}
				return;
			}
		}
	}

	private void LevelUpdate()
	{
		zombieHealthUpdater += Time.deltaTime;
		if (zombieHealthUpdater > 1f)
		{
			zombieHealthUpdater = 0f;
			UpdateZombieHealth();
		}
		if (!isNight && !isTowerDefense)
		{
			SunUpdate();
		}
		if (iceDoomFreezeTime == 0f)
		{
			NewZombieUpdate();
			return;
		}
		iceDoomFreezeTime -= Time.deltaTime;
		if (iceDoomFreezeTime <= 0f)
		{
			iceDoomFreezeTime = 0f;
		}
	}

	private void IceRoadUpdate()
	{
		for (int i = 0; i < iceRoadFadeTime.Length; i++)
		{
			if (iceRoadFadeTime[i] > 0f)
			{
				iceRoadFadeTime[i] -= Time.deltaTime;
				continue;
			}
			iceRoadFadeTime[i] = 0f;
			iceRoadX[i] = 15f;
		}
	}

	private void UpdateZombieHealth()
	{
		int num = 0;
		foreach (GameObject item in zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (!component.isMindControlled)
				{
					num += (int)component.theHealth + component.theFirstArmorHealth + component.theSecondArmorHealth;
				}
			}
		}
		zombieCurrentHealth = num;
	}

	private void SunUpdate()
	{
		theFallingSunCountDown -= Time.deltaTime;
		if (!(theFallingSunCountDown < 0f))
		{
			return;
		}
		int theColumn = Random.Range(3, 9);
		if (GameAPP.theBoardType == 1)
		{
			int theBoardLevel = GameAPP.theBoardLevel;
			if (theBoardLevel == 3 || theBoardLevel == 15 || theBoardLevel == 17)
			{
				CreateCoin.Instance.SetCoin(theColumn, -1, 0, 1);
				CreateCoin.Instance.SetCoin(theColumn, -1, 0, 1);
				CreateCoin.Instance.SetCoin(theColumn, -1, 0, 1);
			}
		}
		CreateCoin.Instance.SetCoin(theColumn, -1, 0, 1);
		theFallingSunCountDown = 7.5f;
	}

	private void NewZombieUpdate()
	{
		if (theWave >= theMaxWave)
		{
			return;
		}
		newZombieWaveCountDown -= Time.deltaTime;
		holdOnTime += Time.deltaTime;
		if (zombieCurrentHealth < zombieTotalHealth / Random.Range(3.5f, 4f) && theWave > 0 && holdOnTime > 6f)
		{
			newZombieWaveCountDown = -1f;
			holdOnTime = 0f;
		}
		if (!(newZombieWaveCountDown < 0f))
		{
			return;
		}
		if (theWave == 0)
		{
			InGameUIMgr.Instance.LevProgress.SetActive(value: true);
			InGameUIMgr.Instance.LevelName2.SetActive(value: false);
			InGameUIMgr.Instance.LevelName3.SetActive(value: true);
		}
		if ((theWave + 1) % 10 == 0)
		{
			if (!isHugeWave)
			{
				isHugeWave = true;
				GameAPP.PlaySound(32);
				GameObject gameObject = Resources.Load<GameObject>("Board/RSP/HugeWavePrefab");
				if (gameObject == null)
				{
					Debug.LogError("hugeWavePrefab预制体加载错误");
				}
				GameObject obj = Object.Instantiate(gameObject);
				obj.name = gameObject.name;
				obj.transform.SetParent(base.transform);
			}
			hugeWaveCountDown += Time.deltaTime;
			if (hugeWaveCountDown > 5f)
			{
				hugeWaveCountDown = 0f;
			}
		}
		if (hugeWaveCountDown != 0f)
		{
			return;
		}
		theWave++;
		isHugeWave = false;
		if (theWave == 1)
		{
			GameAPP.PlaySound(34);
		}
		if (theWave == theMaxWave)
		{
			GameAPP.PlaySound(33);
			GameObject gameObject2 = Resources.Load<GameObject>("Board/RSP/FinalWavePrefab");
			if (gameObject2 == null)
			{
				Debug.LogError("finalWavePrefab预制体加载错误");
			}
			GameObject obj2 = Object.Instantiate(gameObject2);
			obj2.name = gameObject2.name;
			obj2.transform.SetParent(base.transform);
		}
		if (theWave % 10 == 0)
		{
			GameAPP.PlaySound(35);
		}
		int num = 0;
		for (int i = 0; i < InitZombieList.zombieList.GetLength(0); i++)
		{
			if (InitZombieList.zombieList[i, theWave] != -1)
			{
				num++;
			}
		}
		int[] array = ((roadNum != 5) ? PickUniqueRandomNumbers(0, roadNum - 1, num) : PickUniqueRandomNumbers(0, roadNum - 1, num));
		zombieTotalHealth = 0f;
		for (int j = 0; j < InitZombieList.zombieList.GetLength(0); j++)
		{
			if (InitZombieList.zombieList[j, theWave] == -1)
			{
				continue;
			}
			int num2 = InitZombieList.zombieList[j, theWave];
			if (roadType[array[j]] == 1)
			{
				switch (num2)
				{
				default:
					array[j] = GetRandomLandRow();
					break;
				case 0:
				case 2:
				case 4:
				case 14:
				case 17:
				case 19:
				case 200:
					break;
				}
			}
			else
			{
				switch (num2)
				{
				case 14:
				case 17:
				case 19:
				case 200:
					array[j] = Random.Range(2, 4);
					break;
				}
			}
			if (num2 == 18 && iceRoadX[array[j]] == 15f)
			{
				num2 = 16;
			}
			if (isTowerDefense)
			{
				if (theWave < 20)
				{
					array[j] = 4;
				}
				else if (Random.Range(0, 2) == 0)
				{
					array[j] = 1;
				}
				else
				{
					array[j] = 4;
				}
			}
			Zombie component = createZombie.SetZombie(0, array[j], num2).GetComponent<Zombie>();
			if (isTowerDefense)
			{
				float num3 = (float)theWave / 10f;
				component.theHealth *= num3 + 1f;
				component.theFirstArmorHealth = (int)((float)component.theFirstArmorHealth * (num3 + 1f));
				component.theSecondArmorHealth = (int)((float)component.theSecondArmorHealth * (num3 + 1f));
				component.theMaxHealth = (int)component.theHealth;
				component.theFirstArmorMaxHealth = component.theFirstArmorHealth;
				component.theSecondArmorHealth = component.theSecondArmorMaxHealth;
			}
			zombieTotalHealth += (int)component.theHealth + component.theFirstArmorHealth + component.theSecondArmorHealth;
		}
		zombieCurrentHealth += zombieTotalHealth;
		newZombieWaveCountDown = nextZombieWaveCountDown;
	}

	private int GetRandomLandRow()
	{
		return Random.Range(0, 4) switch
		{
			0 => 0, 
			1 => 1, 
			2 => 4, 
			3 => 5, 
			_ => -1, 
		};
	}

	private int[] PickUniqueRandomNumbers(int min, int max, int count)
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		for (int i = 0; i < count; i++)
		{
			if (i % (max + 1) == 0 && i != 0)
			{
				list2.Clear();
			}
			int item;
			do
			{
				item = Random.Range(min, max + 1);
			}
			while (list2.Contains(item));
			list2.Add(item);
			list.Add(item);
		}
		return list.ToArray();
	}

	public void SetDoom(int theColumn, int theRow, bool setPit, Vector2 position)
	{
		Object.Instantiate(GameAPP.particlePrefab[25], position, Quaternion.identity, base.transform);
		ScreenShake.TriggerShake();
		GameAPP.PlaySound(41);
		if (!setPit)
		{
			return;
		}
		if (boxType[theColumn, theRow] != 1)
		{
			if (isNight)
			{
				GridItem.CreateGridItem(theColumn, theRow, 1);
			}
			else
			{
				GridItem.CreateGridItem(theColumn, theRow, 0);
			}
		}
		GameObject[] array = plantArray;
		foreach (GameObject gameObject in array)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				if (component.thePlantRow == theRow && component.thePlantColumn == theColumn)
				{
					component.Die();
				}
			}
		}
	}

	public void CreateExplode(Vector2 v, int theRow)
	{
		GameObject gameObject = GameAPP.particlePrefab[2];
		GameObject obj = Object.Instantiate(gameObject, v, Quaternion.identity, base.transform);
		obj.name = gameObject.name;
		obj.GetComponent<BombCherry>().bombRow = theRow;
		ScreenShake.TriggerShake();
		GameAPP.PlaySound(40);
	}

	public void CreateFreeze(Vector2 pos)
	{
		List<Zombie> list = new List<Zombie>();
		foreach (GameObject item in zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (component.theStatus != 1 && !component.isMindControlled)
				{
					list.Add(component);
				}
			}
		}
		foreach (Zombie item2 in list)
		{
			item2.SetFreeze(4f);
			item2.TakeDamage(1, 20);
		}
		GameAPP.PlaySound(67);
		Object.Instantiate(Resources.Load<GameObject>("Particle/Prefabs/IceShroomExplode"), pos, Quaternion.identity, base.transform);
	}

	public void CreateFireLine(int row)
	{
		GameAPP.PlaySound(42);
		ScreenShake.TriggerShake();
		if (fireLineArray[row] == null)
		{
			GameObject original = Resources.Load<GameObject>("Particle/Anim/FineLine/FireLine");
			float num = base.transform.Find("floor" + row).transform.position.y + 0.3f;
			num = ((roadNum != 5) ? (2.2f - 1.45f * (float)row) : (1.9f - 1.7f * (float)row));
			Vector2 vector = new Vector2(-6f, num);
			GameObject obj = Object.Instantiate(original, vector, Quaternion.identity, base.transform);
			obj.GetComponent<FireLineMgr>().theFireRow = row;
			foreach (Transform item in obj.transform)
			{
				item.GetComponent<SpriteRenderer>().sortingLayerName = $"particle{row}";
				item.GetComponent<SpriteRenderer>().sortingOrder += 1000;
			}
		}
		else
		{
			Object.Destroy(fireLineArray[row]);
			CreateFireLine(row);
		}
		List<Zombie> list = new List<Zombie>();
		foreach (GameObject item2 in zombieArray)
		{
			if (item2 != null)
			{
				Zombie component = item2.GetComponent<Zombie>();
				if (component.theZombieRow == row && !component.isMindControlled)
				{
					list.Add(component);
				}
			}
		}
		foreach (Zombie item3 in list)
		{
			int theZombieType = item3.theZombieType;
			if (theZombieType == 16 || theZombieType == 18 || theZombieType == 201)
			{
				item3.Die(2);
				continue;
			}
			item3.Warm();
			if (item3.theHealth > 1800f)
			{
				item3.TakeDamage(10, 1800);
			}
			else
			{
				item3.Charred();
			}
		}
		GameObject[] array = plantArray;
		foreach (GameObject gameObject in array)
		{
			if (gameObject != null)
			{
				Plant component2 = gameObject.GetComponent<Plant>();
				if (component2.thePlantRow == row && component2.thePlantType == 1073)
				{
					component2.Recover(1000);
				}
			}
		}
		Instance.iceRoadX[row] = 15f;
	}

	public bool YellowFirePea(Bullet bullet, Plant torch, bool fromThreeTorch = false)
	{
		if (bullet.fireLevel < 1)
		{
			GameAPP.PlaySound(61);
			bullet.fireLevel = 1;
			bullet.isHot = true;
			if (fromThreeTorch)
			{
				bullet.theBulletDamage = 30;
				bullet.isFromThreeTorch = true;
			}
			else
			{
				bullet.theBulletDamage = 40;
			}
			bullet.torchWood = torch.gameObject;
			KillSprite(bullet.gameObject);
			if (bullet.fireParticle != null)
			{
				Object.Destroy(bullet.fireParticle);
			}
			bullet.fireParticle = Object.Instantiate(GameAPP.bulletPrefab[25], bullet.transform.position, Quaternion.identity);
			bullet.fireParticle.transform.SetParent(bullet.transform);
			if (fromThreeTorch)
			{
				Vector2 vector = bullet.fireParticle.transform.localScale;
				bullet.fireParticle.transform.localScale = new Vector3(vector.x * 0.75f, vector.y * 0.75f);
			}
			CreateBullet.Instance.SetLayer(bullet.theBulletRow, bullet.fireParticle);
			return true;
		}
		return false;
	}

	public void OrangeFirePea(Bullet bullet, Plant torch)
	{
		if (bullet.fireLevel < 2)
		{
			GameAPP.PlaySound(61);
			bullet.fireLevel = 2;
			bullet.isHot = true;
			if (bullet.isFromThreeTorch)
			{
				bullet.theBulletDamage = 45;
			}
			else
			{
				bullet.theBulletDamage = 60;
			}
			bullet.torchWood = torch.gameObject;
			KillSprite(bullet.gameObject);
			if (bullet.fireParticle != null)
			{
				Object.Destroy(bullet.fireParticle);
			}
			bullet.fireParticle = Object.Instantiate(GameAPP.bulletPrefab[26], bullet.transform.position, Quaternion.identity);
			bullet.fireParticle.transform.SetParent(bullet.transform);
			if (bullet.isFromThreeTorch)
			{
				Vector2 vector = bullet.fireParticle.transform.localScale;
				bullet.fireParticle.transform.localScale = new Vector3(vector.x * 0.75f, vector.y * 0.75f);
			}
			CreateBullet.Instance.SetLayer(bullet.theBulletRow, bullet.fireParticle);
		}
	}

	public bool RedFirePea(Bullet bullet, Plant torch)
	{
		if (bullet.fireLevel < 3)
		{
			GameAPP.PlaySound(61);
			bullet.fireLevel = 3;
			bullet.isHot = true;
			if (bullet.isFromThreeTorch)
			{
				bullet.theBulletDamage = 60;
			}
			else
			{
				bullet.theBulletDamage = 80;
			}
			bullet.torchWood = torch.gameObject;
			KillSprite(bullet.gameObject);
			if (bullet.fireParticle != null)
			{
				Object.Destroy(bullet.fireParticle);
			}
			bullet.fireParticle = Object.Instantiate(GameAPP.bulletPrefab[27], bullet.transform.position, Quaternion.identity);
			bullet.fireParticle.transform.SetParent(bullet.transform);
			if (bullet.isFromThreeTorch)
			{
				Vector2 vector = bullet.fireParticle.transform.localScale;
				bullet.fireParticle.transform.localScale = new Vector3(vector.x * 0.75f, vector.y * 0.75f);
			}
			CreateBullet.Instance.SetLayer(bullet.theBulletRow, bullet.fireParticle);
			return true;
		}
		return false;
	}

	public void FirePuffPea(Bullet bullet, Plant torch)
	{
		if (bullet.fireLevel < 1)
		{
			GameAPP.PlaySound(61);
			bullet.fireLevel = 1;
			bullet.isHot = true;
			bullet.theBulletDamage = 40;
			bullet.torchWood = torch.gameObject;
			KillSprite(bullet.gameObject);
			if (bullet.fireParticle != null)
			{
				Object.Destroy(bullet.fireParticle);
			}
			bullet.fireParticle = Object.Instantiate(GameAPP.bulletPrefab[19], bullet.transform.position, Quaternion.identity);
			bullet.fireParticle.transform.SetParent(bullet.transform);
			CreateBullet.Instance.SetLayer(bullet.theBulletRow, bullet.fireParticle);
		}
	}

	public bool FireCherry(Bullet bullet, Plant torch)
	{
		if (bullet.fireLevel < 1)
		{
			GameAPP.PlaySound(61);
			bullet.fireLevel = 1;
			bullet.isHot = true;
			bullet.theBulletDamage = 120;
			bullet.torchWood = torch.gameObject;
			KillSprite(bullet.gameObject);
			if (bullet.fireParticle != null)
			{
				Object.Destroy(bullet.fireParticle);
			}
			bullet.fireParticle = Object.Instantiate(GameAPP.bulletPrefab[31], bullet.transform.position, Quaternion.identity);
			bullet.fireParticle.transform.SetParent(bullet.transform);
			CreateBullet.Instance.SetLayer(bullet.theBulletRow, bullet.fireParticle);
			return true;
		}
		return false;
	}

	private void KillSprite(GameObject obj)
	{
		if (obj.name == "Shadow")
		{
			return;
		}
		if (obj.TryGetComponent<SpriteRenderer>(out var component))
		{
			component.enabled = false;
		}
		if (obj.transform.childCount <= 0)
		{
			return;
		}
		foreach (Transform item in obj.transform)
		{
			KillSprite(item.gameObject);
		}
	}

	public void EnterNextRound()
	{
		GameAPP.PlaySound(32);
		GameAPP.music.Stop();
		GameAPP.musicDrum.Stop();
		InGameText.INSTANCE.EnableText("更多的僵尸要来了！", 3f);
		foreach (GameObject item in zombieArray)
		{
			if (item != null)
			{
				item.GetComponent<Zombie>().Die(2);
			}
		}
		if (!isTravel)
		{
			Invoke("StartNextRound", 3f);
		}
		else
		{
			Invoke("Travel", 3f);
		}
	}

	private void Travel()
	{
		if (theCurrentSurvivalRound == 3 || theCurrentSurvivalRound == 6)
		{
			theCurrentSurvivalRound++;
			LevelData.SaveLevel(this);
			LevelData.ClearPlant();
			GameObject[] array = plantArray;
			foreach (GameObject gameObject in array)
			{
				if (gameObject != null)
				{
					LevelData.AddPlant(gameObject.GetComponent<Plant>());
				}
			}
			ShowChoice(theCurrentSurvivalRound);
		}
		else
		{
			StartNextRound();
		}
	}

	private void ShowChoice(int round)
	{
		Object.Instantiate(Resources.Load<GameObject>("UI/InGameMenu/Travel/TravelMenu"), GameAPP.canvasUp.transform);
		if (round == 7)
		{
			TravelMenuMgr.Instance.ChangeText(1);
		}
		Time.timeScale = 0f;
		GameAPP.theGameStatus = 2;
	}

	public void ChoiceOver()
	{
		Invoke("DarkQuit", 4f);
	}

	public void DarkQuit()
	{
		Object.Instantiate(Resources.Load<GameObject>("UI/DarkQuit"));
		Invoke("DelayNextRound", 1f);
	}

	public void DelayNextRound()
	{
		Object.Instantiate(Resources.Load<GameObject>("UI/DarkEnter"));
		GameAPP.ClearItemInCanvas();
		UIMgr.EnterTravelGame(GameAPP.theBoardType, GameAPP.theBoardLevel, theCurrentSurvivalRound);
		Object.Destroy(base.gameObject);
	}

	private void StartNextRound()
	{
		GameAPP.theGameStatus = 2;
		theWave = 0;
		newZombieWaveCountDown = 6f;
		theCurrentSurvivalRound++;
		GameAPP.ChangeMusic(1);
		InitZombieList.InitZombie(GameAPP.theBoardType, GameAPP.theBoardLevel, theCurrentSurvivalRound);
		foreach (Transform item in GameAPP.canvasUp.transform)
		{
			if (item.name != "Tutor")
			{
				Object.Destroy(item.gameObject);
			}
		}
		foreach (Transform item2 in GameAPP.canvas.transform)
		{
			Object.Destroy(item2.gameObject);
		}
		foreach (GameObject item3 in zombieArray)
		{
			if (item3 != null)
			{
				Zombie component = item3.GetComponent<Zombie>();
				if (component.isMindControlled)
				{
					component.Die(2);
				}
			}
		}
		Time.timeScale = GameAPP.gameSpeed;
		Mouse.Instance.ClearItemOnMouse(clearItem: true);
		Object.Destroy(Mouse.Instance.plantShadow);
		InitBoard.Instance.InitSelectUI();
		theMaxWave = InitZombieList.theMaxWave;
		InitBoard.Instance.StartInit();
		InGameUIMgr.Instance.LevelName1.GetComponent<TextMeshProUGUI>().text = $"第{theCurrentSurvivalRound - 1}已完成";
		InGameUIMgr.Instance.LevelName1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"第{theCurrentSurvivalRound - 1}已完成";
		if (isEndless)
		{
			SaveTheBoard();
		}
	}

	public void SaveTheBoard()
	{
		PlantsInLevel.SaveBoard();
		SaveInfo.Instance.SaveEndlessData();
	}

	public void ClearTheBoard()
	{
		PlantsInLevel.ClearBoard();
		SaveInfo.Instance.SaveEndlessData();
	}

	public void EnterEndlessGame()
	{
		isEndless = true;
		if (PlantsInLevel.LoadBoard())
		{
			InitZombieList.InitZombie(GameAPP.theBoardType, GameAPP.theBoardLevel, theCurrentSurvivalRound);
			return;
		}
		Instance.theCurrentSurvivalRound = 1;
		InitZombieList.InitZombie(GameAPP.theBoardType, GameAPP.theBoardLevel, 1);
	}

	private void SetPrePlants()
	{
		if (GameAPP.theBoardType == 1)
		{
			switch (GameAPP.theBoardLevel)
			{
			case 1:
				CreatePlant.Instance.SetPlant(0, 1, 901);
				CreatePlant.Instance.SetPlant(0, 3, 901);
				break;
			case 2:
				CreatePlant.Instance.SetPlant(0, 1, 901);
				CreatePlant.Instance.SetPlant(0, 3, 901);
				CreatePlant.Instance.SetPlant(1, 1, 902);
				CreatePlant.Instance.SetPlant(1, 3, 902);
				break;
			case 3:
				CreatePlant.Instance.SetPlant(0, 1, 900);
				CreatePlant.Instance.SetPlant(0, 3, 900);
				break;
			case 4:
				CreatePlant.Instance.SetPlant(0, 1, 904);
				CreatePlant.Instance.SetPlant(0, 3, 904);
				break;
			case 5:
				CreatePlant.Instance.SetPlant(3, 1, 903);
				CreatePlant.Instance.SetPlant(3, 4, 903);
				break;
			case 6:
				CreatePlant.Instance.SetPlant(3, 2, 12);
				CreatePlant.Instance.SetPlant(3, 3, 12);
				CreatePlant.Instance.SetPlant(3, 2, 903);
				CreatePlant.Instance.SetPlant(3, 3, 903);
				CreatePlant.Instance.SetPlant(0, 1, 904);
				CreatePlant.Instance.SetPlant(0, 4, 904);
				CreatePlant.Instance.SetPlant(1, 1, 900);
				CreatePlant.Instance.SetPlant(1, 4, 900);
				CreatePlant.Instance.SetPlant(0, 0, 901);
				CreatePlant.Instance.SetPlant(0, 5, 901);
				CreatePlant.Instance.SetPlant(1, 0, 902);
				CreatePlant.Instance.SetPlant(1, 5, 902);
				break;
			case 31:
				SetSuperTorch();
				break;
			case 32:
				SetSuperKelp();
				break;
			}
		}
	}

	private void SetSuperTorch()
	{
		CreatePlant.Instance.SetPlant(4, 0, 0);
		CreatePlant.Instance.SetPlant(5, 0, 18);
		CreatePlant.Instance.SetPlant(4, 1, 1000);
		CreatePlant.Instance.SetPlant(5, 1, 1053);
		CreatePlant.Instance.SetPlant(4, 2, 1001);
		CreatePlant.Instance.SetPlant(5, 2, 1052);
		CreatePlant.Instance.SetPlant(4, 3, 1000);
		CreatePlant.Instance.SetPlant(5, 3, 1053);
		CreatePlant.Instance.SetPlant(4, 4, 0);
		CreatePlant.Instance.SetPlant(5, 4, 18);
	}

	private void SetSuperKelp()
	{
		CreatePlant.Instance.SetPlant(0, 2, 1066);
		CreatePlant.Instance.SetPlant(0, 3, 1066);
		CreatePlant.Instance.SetPlant(1, 2, 1051);
		CreatePlant.Instance.SetPlant(1, 3, 1051);
		CreatePlant.Instance.SetPlant(2, 2, 1050);
		CreatePlant.Instance.SetPlant(2, 3, 1050);
	}
}

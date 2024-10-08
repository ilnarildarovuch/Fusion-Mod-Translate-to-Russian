using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x0200002F RID: 47
public class Board : MonoBehaviour
{
	// Token: 0x060000D4 RID: 212 RVA: 0x00006250 File Offset: 0x00004450
	private void Awake()
	{
		Board.Instance = this;
		if (GameAPP.developerMode)
		{
			this.freeCD = true;
		}
		this.createZombie = this.AddComponent<CreateZombie>();
		this.createPlant = this.AddComponent<CreatePlant>();
		this.AddComponent<CreateCoin>();
		this.AddComponent<CreateBullet>();
		this.AddComponent<Mouse>();
		this.AddComponent<CreateMower>();
		this.AddComponent<PoolMgr>();
		this.inGameText = InGameText.Instance().GetComponent<InGameText>();
		switch (UIMgr.GetSceneType(GameAPP.theBoardType, GameAPP.theBoardLevel, -1))
		{
		case 1:
			this.theSun = 150;
			this.isNight = true;
			break;
		case 2:
			this.theSun = 250;
			this.roadNum = 6;
			this.roadType[2] = 1;
			this.roadType[3] = 1;
			for (int i = 0; i < this.boxType.GetLength(0); i++)
			{
				this.boxType[i, 2] = 1;
				this.boxType[i, 3] = 1;
			}
			break;
		}
		if (GameAPP.theBoardType == 1)
		{
			int theBoardLevel = GameAPP.theBoardLevel;
			if (theBoardLevel - 25 > 1)
			{
				if (theBoardLevel == 35)
				{
					BoxMgr.SetBox();
					this.isTowerDefense = true;
				}
			}
			else
			{
				this.isScaredyDream = true;
			}
		}
		if (GameAPP.theBoardType == 2)
		{
			this.isNight = true;
		}
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x0000638C File Offset: 0x0000458C
	private void Start()
	{
		if (this.isScaredyDream)
		{
			this.createPlant.SetPlant(0, 3, 9, null, default(Vector2), false, 0f);
		}
		if (this.isIZ)
		{
			if (GameAPP.theBoardLevel == 1)
			{
				if (!this.isAutoEve)
				{
					for (int i = 0; i < 5; i++)
					{
						for (int j = 0; j < 5; j++)
						{
							this.SetEvePlants(i, j);
						}
					}
				}
			}
			else
			{
				MixData.SetPlants(GameAPP.theBoardLevel);
			}
		}
		this.SetPrePlants();
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x0000640C File Offset: 0x0000460C
	private void Update()
	{
		this.time += Time.deltaTime;
		if (GameAPP.theBoardLevel != -1 && GameAPP.theGameStatus == 0)
		{
			this.IceRoadUpdate();
			if (this.isIZ && this.isEveStart)
			{
				this.EveUpdate();
			}
			else
			{
				this.LevelUpdate();
			}
		}
		if (GameAPP.developerMode)
		{
			this.theSun = 10000;
		}
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00006470 File Offset: 0x00004670
	private void EveUpdate()
	{
		this.eveCountDown -= Time.deltaTime;
		this.eveCurrentTime += Time.deltaTime;
		if (this.eveCountDown < 0f)
		{
			this.SetRandomZombies();
			if (this.eveCurrentTime < 120f)
			{
				this.eveCountDown = 8f;
				return;
			}
			if (this.eveCurrentTime < 240f)
			{
				this.eveCountDown = 7f;
				return;
			}
			if (this.eveCurrentTime < 360f)
			{
				this.eveCountDown = 6f;
				return;
			}
			if (this.eveCurrentTime < 480f)
			{
				this.eveCountDown = 5f;
				return;
			}
			if (this.eveCurrentTime < 600f)
			{
				this.eveCountDown = 4f;
				return;
			}
			if (this.eveCurrentTime < 720f)
			{
				this.eveCountDown = 3f;
				return;
			}
			this.eveCountDown = 2f;
		}
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x00006558 File Offset: 0x00004758
	private void SetRandomZombies()
	{
		for (int i = 0; i < 5; i++)
		{
			if (!this.disAllowSetZombie[i])
			{
				this.createZombie.SetZombie(0, i, 105, 0f, false);
			}
		}
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x00006594 File Offset: 0x00004794
	public void SetEvePlants(int i, int j)
	{
		int num;
		do
		{
			num = Random.Range(998, 1076);
			if (num <= 1031)
			{
				if (num <= 1002)
				{
					if (num == 1 || num == 1002)
					{
						continue;
					}
				}
				else if (num == 1009 || num == 1031)
				{
					continue;
				}
			}
			else if (num <= 1036)
			{
				if (num == 1033 || num == 1036)
				{
					continue;
				}
			}
			else
			{
				switch (num)
				{
				case 1040:
				case 1041:
				case 1043:
				case 1045:
				case 1048:
					continue;
				case 1042:
				case 1044:
				case 1046:
				case 1047:
					break;
				default:
					if (num == 1054 || num - 1057 <= 2)
					{
						continue;
					}
					break;
				}
			}
			if (i == 0)
			{
				for (;;)
				{
					num = Random.Range(998, 1076);
					if (num != 1 && num != 18)
					{
						switch (num)
						{
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
							break;
						default:
							goto IL_1B9;
						}
					}
				}
			}
			IL_1B9:
			if (num == 999)
			{
				num = 253;
			}
			if (num == 998)
			{
				num = 900;
			}
		}
		while (CreatePlant.Instance.IsWaterPlant(num));
		if (this.createPlant.IsPuff(num))
		{
			this.createPlant.SetPlant(i, j, num, null, default(Vector2), false, 0f);
			this.createPlant.SetPlant(i, j, num, null, default(Vector2), false, 0f);
		}
		GameObject gameObject = this.createPlant.SetPlant(i, j, num, null, default(Vector2), true, 0f);
		PotatoMine potatoMine;
		if (gameObject != null && gameObject.TryGetComponent<PotatoMine>(out potatoMine))
		{
			potatoMine.attributeCountdown = 0f;
		}
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00006810 File Offset: 0x00004A10
	private void LevelUpdate()
	{
		this.zombieHealthUpdater += Time.deltaTime;
		if (this.zombieHealthUpdater > 1f)
		{
			this.zombieHealthUpdater = 0f;
			this.UpdateZombieHealth();
		}
		if (!this.isNight && !this.isTowerDefense)
		{
			this.SunUpdate();
		}
		if (this.iceDoomFreezeTime == 0f)
		{
			this.NewZombieUpdate();
			return;
		}
		this.iceDoomFreezeTime -= Time.deltaTime;
		if (this.iceDoomFreezeTime <= 0f)
		{
			this.iceDoomFreezeTime = 0f;
		}
	}

	// Token: 0x060000DB RID: 219 RVA: 0x000068A4 File Offset: 0x00004AA4
	private void IceRoadUpdate()
	{
		for (int i = 0; i < this.iceRoadFadeTime.Length; i++)
		{
			if (this.iceRoadFadeTime[i] > 0f)
			{
				this.iceRoadFadeTime[i] -= Time.deltaTime;
			}
			else
			{
				this.iceRoadFadeTime[i] = 0f;
				this.iceRoadX[i] = 15f;
			}
		}
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00006904 File Offset: 0x00004B04
	private void UpdateZombieHealth()
	{
		int num = 0;
		foreach (GameObject gameObject in this.zombieArray)
		{
			if (gameObject != null)
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (!component.isMindControlled)
				{
					num += (int)component.theHealth + component.theFirstArmorHealth + component.theSecondArmorHealth;
				}
			}
		}
		this.zombieCurrentHealth = (float)num;
	}

	// Token: 0x060000DD RID: 221 RVA: 0x0000698C File Offset: 0x00004B8C
	private void SunUpdate()
	{
		this.theFallingSunCountDown -= Time.deltaTime;
		if (this.theFallingSunCountDown < 0f)
		{
			int theColumn = Random.Range(3, 9);
			if (GameAPP.theBoardType == 1)
			{
				int theBoardLevel = GameAPP.theBoardLevel;
				if (theBoardLevel == 3 || theBoardLevel == 15 || theBoardLevel == 17)
				{
					CreateCoin.Instance.SetCoin(theColumn, -1, 0, 1, default(Vector3));
					CreateCoin.Instance.SetCoin(theColumn, -1, 0, 1, default(Vector3));
					CreateCoin.Instance.SetCoin(theColumn, -1, 0, 1, default(Vector3));
				}
			}
			CreateCoin.Instance.SetCoin(theColumn, -1, 0, 1, default(Vector3));
			this.theFallingSunCountDown = 7.5f;
		}
	}

	// Token: 0x060000DE RID: 222 RVA: 0x00006A4C File Offset: 0x00004C4C
	private void NewZombieUpdate()
	{
		if (this.theWave >= this.theMaxWave)
		{
			return;
		}
		this.newZombieWaveCountDown -= Time.deltaTime;
		this.holdOnTime += Time.deltaTime;
		if (this.zombieCurrentHealth < this.zombieTotalHealth / Random.Range(3.5f, 4f) && this.theWave > 0 && this.holdOnTime > 6f)
		{
			this.newZombieWaveCountDown = -1f;
			this.holdOnTime = 0f;
		}
		if (this.newZombieWaveCountDown < 0f)
		{
			if (this.theWave == 0)
			{
				InGameUIMgr.Instance.LevProgress.SetActive(true);
				InGameUIMgr.Instance.LevelName2.SetActive(false);
				InGameUIMgr.Instance.LevelName3.SetActive(true);
			}
			if ((this.theWave + 1) % 10 == 0)
			{
				if (!this.isHugeWave)
				{
					this.isHugeWave = true;
					GameAPP.PlaySound(32, 0.5f);
					GameObject gameObject = Resources.Load<GameObject>("Board/RSP/HugeWavePrefab");
					if (gameObject == null)
					{
						Debug.LogError("hugeWavePrefab预制体加载错误");
					}
					GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject);
					gameObject2.name = gameObject.name;
					gameObject2.transform.SetParent(base.transform);
				}
				this.hugeWaveCountDown += Time.deltaTime;
				if (this.hugeWaveCountDown > 5f)
				{
					this.hugeWaveCountDown = 0f;
				}
			}
			if (this.hugeWaveCountDown == 0f)
			{
				this.theWave++;
				this.isHugeWave = false;
				if (this.theWave == 1)
				{
					GameAPP.PlaySound(34, 0.5f);
				}
				if (this.theWave == this.theMaxWave)
				{
					GameAPP.PlaySound(33, 0.5f);
					GameObject gameObject3 = Resources.Load<GameObject>("Board/RSP/FinalWavePrefab");
					if (gameObject3 == null)
					{
						Debug.LogError("finalWavePrefab预制体加载错误");
					}
					GameObject gameObject4 = Object.Instantiate<GameObject>(gameObject3);
					gameObject4.name = gameObject3.name;
					gameObject4.transform.SetParent(base.transform);
				}
				if (this.theWave % 10 == 0)
				{
					GameAPP.PlaySound(35, 0.5f);
				}
				int num = 0;
				for (int i = 0; i < InitZombieList.zombieList.GetLength(0); i++)
				{
					if (InitZombieList.zombieList[i, this.theWave] != -1)
					{
						num++;
					}
				}
				int[] array;
				if (this.roadNum == 5)
				{
					array = this.PickUniqueRandomNumbers(0, this.roadNum - 1, num);
				}
				else
				{
					array = this.PickUniqueRandomNumbers(0, this.roadNum - 1, num);
				}
				this.zombieTotalHealth = 0f;
				for (int j = 0; j < InitZombieList.zombieList.GetLength(0); j++)
				{
					if (InitZombieList.zombieList[j, this.theWave] != -1)
					{
						int num2 = InitZombieList.zombieList[j, this.theWave];
						if (this.roadType[array[j]] == 1)
						{
							if (num2 <= 14)
							{
								switch (num2)
								{
								case 0:
								case 2:
								case 4:
									goto IL_32C;
								case 1:
								case 3:
									break;
								default:
									if (num2 == 14)
									{
										goto IL_32C;
									}
									break;
								}
							}
							else if (num2 == 17 || num2 == 19 || num2 == 200)
							{
								goto IL_32C;
							}
							array[j] = this.GetRandomLandRow();
						}
						else
						{
							if (num2 <= 17)
							{
								if (num2 != 14 && num2 != 17)
								{
									goto IL_32C;
								}
							}
							else if (num2 != 19 && num2 != 200)
							{
								goto IL_32C;
							}
							array[j] = Random.Range(2, 4);
						}
						IL_32C:
						if (num2 == 18 && this.iceRoadX[array[j]] == 15f)
						{
							num2 = 16;
						}
						if (this.isTowerDefense)
						{
							if (this.theWave < 20)
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
						Zombie component = this.createZombie.SetZombie(0, array[j], num2, 0f, false).GetComponent<Zombie>();
						if (this.isTowerDefense)
						{
							float num3 = (float)this.theWave / 10f;
							component.theHealth *= num3 + 1f;
							component.theFirstArmorHealth = (int)((float)component.theFirstArmorHealth * (num3 + 1f));
							component.theSecondArmorHealth = (int)((float)component.theSecondArmorHealth * (num3 + 1f));
							component.theMaxHealth = (int)component.theHealth;
							component.theFirstArmorMaxHealth = component.theFirstArmorHealth;
							component.theSecondArmorHealth = component.theSecondArmorMaxHealth;
						}
						this.zombieTotalHealth += (float)((int)component.theHealth + component.theFirstArmorHealth + component.theSecondArmorHealth);
					}
				}
				this.zombieCurrentHealth += this.zombieTotalHealth;
				this.newZombieWaveCountDown = this.nextZombieWaveCountDown;
			}
		}
	}

	// Token: 0x060000DF RID: 223 RVA: 0x00006EDC File Offset: 0x000050DC
	private int GetRandomLandRow()
	{
		switch (Random.Range(0, 4))
		{
		case 0:
			return 0;
		case 1:
			return 1;
		case 2:
			return 4;
		case 3:
			return 5;
		default:
			return -1;
		}
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x00006F14 File Offset: 0x00005114
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

	// Token: 0x060000E1 RID: 225 RVA: 0x00006F70 File Offset: 0x00005170
	public void SetDoom(int theColumn, int theRow, bool setPit, Vector2 position)
	{
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[25], position, Quaternion.identity, base.transform);
		ScreenShake.TriggerShake(0.15f);
		GameAPP.PlaySound(41, 0.5f);
		if (setPit)
		{
			if (this.boxType[theColumn, theRow] != 1)
			{
				if (this.isNight)
				{
					GridItem.CreateGridItem(theColumn, theRow, 1);
				}
				else
				{
					GridItem.CreateGridItem(theColumn, theRow, 0);
				}
			}
			foreach (GameObject gameObject in this.plantArray)
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
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x00007024 File Offset: 0x00005224
	public void CreateExplode(Vector2 v, int theRow)
	{
		GameObject gameObject = GameAPP.particlePrefab[2];
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, v, Quaternion.identity, base.transform);
		gameObject2.name = gameObject.name;
		gameObject2.GetComponent<BombCherry>().bombRow = theRow;
		ScreenShake.TriggerShake(0.15f);
		GameAPP.PlaySound(40, 0.5f);
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x00007080 File Offset: 0x00005280
	public void CreateFreeze(Vector2 pos)
	{
		List<Zombie> list = new List<Zombie>();
		foreach (GameObject gameObject in this.zombieArray)
		{
			if (gameObject != null)
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (component.theStatus != 1 && !component.isMindControlled)
				{
					list.Add(component);
				}
			}
		}
		foreach (Zombie zombie in list)
		{
			zombie.SetFreeze(4f);
			zombie.TakeDamage(1, 20);
		}
		GameAPP.PlaySound(67, 0.5f);
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("Particle/Prefabs/IceShroomExplode"), pos, Quaternion.identity, base.transform);
	}

	// Token: 0x060000E4 RID: 228 RVA: 0x00007170 File Offset: 0x00005370
	public void CreateFireLine(int row)
	{
		GameAPP.PlaySound(42, 0.5f);
		ScreenShake.TriggerShake(0.15f);
		if (this.fireLineArray[row] == null)
		{
			GameObject original = Resources.Load<GameObject>("Particle/Anim/FineLine/FireLine");
			float y = base.transform.Find("floor" + row.ToString()).transform.position.y + 0.3f;
			if (this.roadNum == 5)
			{
				y = 1.9f - 1.7f * (float)row;
			}
			else
			{
				y = 2.2f - 1.45f * (float)row;
			}
			Vector2 v = new Vector2(-6f, y);
			GameObject gameObject = Object.Instantiate<GameObject>(original, v, Quaternion.identity, base.transform);
			gameObject.GetComponent<FireLineMgr>().theFireRow = row;
			using (IEnumerator enumerator = gameObject.transform.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					transform.GetComponent<SpriteRenderer>().sortingLayerName = string.Format("particle{0}", row);
					transform.GetComponent<SpriteRenderer>().sortingOrder += 1000;
				}
				goto IL_138;
			}
		}
		Object.Destroy(this.fireLineArray[row]);
		this.CreateFireLine(row);
		IL_138:
		List<Zombie> list = new List<Zombie>();
		foreach (GameObject gameObject2 in this.zombieArray)
		{
			if (gameObject2 != null)
			{
				Zombie component = gameObject2.GetComponent<Zombie>();
				if (component.theZombieRow == row && !component.isMindControlled)
				{
					list.Add(component);
				}
			}
		}
		foreach (Zombie zombie in list)
		{
			int i = zombie.theZombieType;
			if (i == 16 || i == 18 || i == 201)
			{
				zombie.Die(2);
			}
			else
			{
				zombie.Warm(0);
				if (zombie.theHealth > 1800f)
				{
					zombie.TakeDamage(10, 1800);
				}
				else
				{
					zombie.Charred();
				}
			}
		}
		foreach (GameObject gameObject3 in this.plantArray)
		{
			if (gameObject3 != null)
			{
				Plant component2 = gameObject3.GetComponent<Plant>();
				if (component2.thePlantRow == row && component2.thePlantType == 1073)
				{
					component2.Recover(1000);
				}
			}
		}
		Board.Instance.iceRoadX[row] = 15f;
	}

	// Token: 0x060000E5 RID: 229 RVA: 0x00007430 File Offset: 0x00005630
	public bool YellowFirePea(Bullet bullet, Plant torch, bool fromThreeTorch = false)
	{
		if (bullet.fireLevel < 1)
		{
			GameAPP.PlaySound(61, 0.5f);
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
			this.KillSprite(bullet.gameObject);
			if (bullet.fireParticle != null)
			{
				Object.Destroy(bullet.fireParticle);
			}
			bullet.fireParticle = Object.Instantiate<GameObject>(GameAPP.bulletPrefab[25], bullet.transform.position, Quaternion.identity);
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

	// Token: 0x060000E6 RID: 230 RVA: 0x00007548 File Offset: 0x00005748
	public void OrangeFirePea(Bullet bullet, Plant torch)
	{
		if (bullet.fireLevel < 2)
		{
			GameAPP.PlaySound(61, 0.5f);
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
			this.KillSprite(bullet.gameObject);
			if (bullet.fireParticle != null)
			{
				Object.Destroy(bullet.fireParticle);
			}
			bullet.fireParticle = Object.Instantiate<GameObject>(GameAPP.bulletPrefab[26], bullet.transform.position, Quaternion.identity);
			bullet.fireParticle.transform.SetParent(bullet.transform);
			if (bullet.isFromThreeTorch)
			{
				Vector2 vector = bullet.fireParticle.transform.localScale;
				bullet.fireParticle.transform.localScale = new Vector3(vector.x * 0.75f, vector.y * 0.75f);
			}
			CreateBullet.Instance.SetLayer(bullet.theBulletRow, bullet.fireParticle);
		}
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00007660 File Offset: 0x00005860
	public bool RedFirePea(Bullet bullet, Plant torch)
	{
		if (bullet.fireLevel < 3)
		{
			GameAPP.PlaySound(61, 0.5f);
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
			this.KillSprite(bullet.gameObject);
			if (bullet.fireParticle != null)
			{
				Object.Destroy(bullet.fireParticle);
			}
			bullet.fireParticle = Object.Instantiate<GameObject>(GameAPP.bulletPrefab[27], bullet.transform.position, Quaternion.identity);
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

	// Token: 0x060000E8 RID: 232 RVA: 0x0000777C File Offset: 0x0000597C
	public void FirePuffPea(Bullet bullet, Plant torch)
	{
		if (bullet.fireLevel < 1)
		{
			GameAPP.PlaySound(61, 0.5f);
			bullet.fireLevel = 1;
			bullet.isHot = true;
			bullet.theBulletDamage = 40;
			bullet.torchWood = torch.gameObject;
			this.KillSprite(bullet.gameObject);
			if (bullet.fireParticle != null)
			{
				Object.Destroy(bullet.fireParticle);
			}
			bullet.fireParticle = Object.Instantiate<GameObject>(GameAPP.bulletPrefab[19], bullet.transform.position, Quaternion.identity);
			bullet.fireParticle.transform.SetParent(bullet.transform);
			CreateBullet.Instance.SetLayer(bullet.theBulletRow, bullet.fireParticle);
		}
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x00007838 File Offset: 0x00005A38
	public bool FireCherry(Bullet bullet, Plant torch)
	{
		if (bullet.fireLevel < 1)
		{
			GameAPP.PlaySound(61, 0.5f);
			bullet.fireLevel = 1;
			bullet.isHot = true;
			bullet.theBulletDamage = 120;
			bullet.torchWood = torch.gameObject;
			this.KillSprite(bullet.gameObject);
			if (bullet.fireParticle != null)
			{
				Object.Destroy(bullet.fireParticle);
			}
			bullet.fireParticle = Object.Instantiate<GameObject>(GameAPP.bulletPrefab[31], bullet.transform.position, Quaternion.identity);
			bullet.fireParticle.transform.SetParent(bullet.transform);
			CreateBullet.Instance.SetLayer(bullet.theBulletRow, bullet.fireParticle);
			return true;
		}
		return false;
	}

	// Token: 0x060000EA RID: 234 RVA: 0x000078F8 File Offset: 0x00005AF8
	private void KillSprite(GameObject obj)
	{
		if (obj.name == "Shadow")
		{
			return;
		}
		SpriteRenderer spriteRenderer;
		if (obj.TryGetComponent<SpriteRenderer>(out spriteRenderer))
		{
			spriteRenderer.enabled = false;
		}
		if (obj.transform.childCount > 0)
		{
			foreach (object obj2 in obj.transform)
			{
				Transform transform = (Transform)obj2;
				this.KillSprite(transform.gameObject);
			}
		}
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00007988 File Offset: 0x00005B88
	public void EnterNextRound()
	{
		GameAPP.PlaySound(32, 0.5f);
		GameAPP.music.Stop();
		GameAPP.musicDrum.Stop();
		InGameText.INSTANCE.EnableText("更多的僵尸要来了！", 3f);
		foreach (GameObject gameObject in this.zombieArray)
		{
			if (gameObject != null)
			{
				gameObject.GetComponent<Zombie>().Die(2);
			}
		}
		if (!this.isTravel)
		{
			base.Invoke("StartNextRound", 3f);
			return;
		}
		base.Invoke("Travel", 3f);
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00007A48 File Offset: 0x00005C48
	private void Travel()
	{
		if (this.theCurrentSurvivalRound == 3 || this.theCurrentSurvivalRound == 6)
		{
			this.theCurrentSurvivalRound++;
			LevelData.SaveLevel(this);
			LevelData.ClearPlant();
			foreach (GameObject gameObject in this.plantArray)
			{
				if (gameObject != null)
				{
					LevelData.AddPlant(gameObject.GetComponent<Plant>());
				}
			}
			this.ShowChoice(this.theCurrentSurvivalRound);
			return;
		}
		this.StartNextRound();
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00007AC0 File Offset: 0x00005CC0
	private void ShowChoice(int round)
	{
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("UI/InGameMenu/Travel/TravelMenu"), GameAPP.canvasUp.transform);
		if (round == 7)
		{
			TravelMenuMgr.Instance.ChangeText(1);
		}
		Time.timeScale = 0f;
		GameAPP.theGameStatus = 2;
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00007AFB File Offset: 0x00005CFB
	public void ChoiceOver()
	{
		base.Invoke("DarkQuit", 4f);
	}

	// Token: 0x060000EF RID: 239 RVA: 0x00007B0D File Offset: 0x00005D0D
	public void DarkQuit()
	{
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("UI/DarkQuit"));
		base.Invoke("DelayNextRound", 1f);
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x00007B2F File Offset: 0x00005D2F
	public void DelayNextRound()
	{
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("UI/DarkEnter"));
		GameAPP.ClearItemInCanvas();
		UIMgr.EnterTravelGame(GameAPP.theBoardType, GameAPP.theBoardLevel, this.theCurrentSurvivalRound);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00007B68 File Offset: 0x00005D68
	private void StartNextRound()
	{
		GameAPP.theGameStatus = 2;
		this.theWave = 0;
		this.newZombieWaveCountDown = 6f;
		this.theCurrentSurvivalRound++;
		GameAPP.ChangeMusic(1);
		InitZombieList.InitZombie(GameAPP.theBoardType, GameAPP.theBoardLevel, this.theCurrentSurvivalRound);
		foreach (object obj in GameAPP.canvasUp.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.name != "Tutor")
			{
				Object.Destroy(transform.gameObject);
			}
		}
		foreach (object obj2 in GameAPP.canvas.transform)
		{
			Object.Destroy(((Transform)obj2).gameObject);
		}
		foreach (GameObject gameObject in this.zombieArray)
		{
			if (gameObject != null)
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (component.isMindControlled)
				{
					component.Die(2);
				}
			}
		}
		Time.timeScale = GameAPP.gameSpeed;
		Mouse.Instance.ClearItemOnMouse(true);
		Object.Destroy(Mouse.Instance.plantShadow);
		InitBoard.Instance.InitSelectUI();
		this.theMaxWave = InitZombieList.theMaxWave;
		InitBoard.Instance.StartInit();
		InGameUIMgr.Instance.LevelName1.GetComponent<TextMeshProUGUI>().text = string.Format("第{0}已完成", this.theCurrentSurvivalRound - 1);
		InGameUIMgr.Instance.LevelName1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = string.Format("第{0}已完成", this.theCurrentSurvivalRound - 1);
		if (this.isEndless)
		{
			this.SaveTheBoard();
		}
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00007D7C File Offset: 0x00005F7C
	public void SaveTheBoard()
	{
		PlantsInLevel.SaveBoard();
		SaveInfo.Instance.SaveEndlessData();
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00007D8D File Offset: 0x00005F8D
	public void ClearTheBoard()
	{
		PlantsInLevel.ClearBoard();
		SaveInfo.Instance.SaveEndlessData();
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x00007DA0 File Offset: 0x00005FA0
	public void EnterEndlessGame()
	{
		this.isEndless = true;
		if (PlantsInLevel.LoadBoard())
		{
			InitZombieList.InitZombie(GameAPP.theBoardType, GameAPP.theBoardLevel, this.theCurrentSurvivalRound);
			return;
		}
		Board.Instance.theCurrentSurvivalRound = 1;
		InitZombieList.InitZombie(GameAPP.theBoardType, GameAPP.theBoardLevel, 1);
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x00007DEC File Offset: 0x00005FEC
	private void SetPrePlants()
	{
		if (GameAPP.theBoardType == 1)
		{
			int theBoardLevel = GameAPP.theBoardLevel;
			switch (theBoardLevel)
			{
			case 1:
				CreatePlant.Instance.SetPlant(0, 1, 901, null, default(Vector2), false, 0f);
				CreatePlant.Instance.SetPlant(0, 3, 901, null, default(Vector2), false, 0f);
				return;
			case 2:
				CreatePlant.Instance.SetPlant(0, 1, 901, null, default(Vector2), false, 0f);
				CreatePlant.Instance.SetPlant(0, 3, 901, null, default(Vector2), false, 0f);
				CreatePlant.Instance.SetPlant(1, 1, 902, null, default(Vector2), false, 0f);
				CreatePlant.Instance.SetPlant(1, 3, 902, null, default(Vector2), false, 0f);
				return;
			case 3:
				CreatePlant.Instance.SetPlant(0, 1, 900, null, default(Vector2), false, 0f);
				CreatePlant.Instance.SetPlant(0, 3, 900, null, default(Vector2), false, 0f);
				return;
			case 4:
				CreatePlant.Instance.SetPlant(0, 1, 904, null, default(Vector2), false, 0f);
				CreatePlant.Instance.SetPlant(0, 3, 904, null, default(Vector2), false, 0f);
				return;
			case 5:
				CreatePlant.Instance.SetPlant(3, 1, 903, null, default(Vector2), false, 0f);
				CreatePlant.Instance.SetPlant(3, 4, 903, null, default(Vector2), false, 0f);
				return;
			case 6:
				CreatePlant.Instance.SetPlant(3, 2, 12, null, default(Vector2), false, 0f);
				CreatePlant.Instance.SetPlant(3, 3, 12, null, default(Vector2), false, 0f);
				CreatePlant.Instance.SetPlant(3, 2, 903, null, default(Vector2), false, 0f);
				CreatePlant.Instance.SetPlant(3, 3, 903, null, default(Vector2), false, 0f);
				CreatePlant.Instance.SetPlant(0, 1, 904, null, default(Vector2), false, 0f);
				CreatePlant.Instance.SetPlant(0, 4, 904, null, default(Vector2), false, 0f);
				CreatePlant.Instance.SetPlant(1, 1, 900, null, default(Vector2), false, 0f);
				CreatePlant.Instance.SetPlant(1, 4, 900, null, default(Vector2), false, 0f);
				CreatePlant.Instance.SetPlant(0, 0, 901, null, default(Vector2), false, 0f);
				CreatePlant.Instance.SetPlant(0, 5, 901, null, default(Vector2), false, 0f);
				CreatePlant.Instance.SetPlant(1, 0, 902, null, default(Vector2), false, 0f);
				CreatePlant.Instance.SetPlant(1, 5, 902, null, default(Vector2), false, 0f);
				return;
			default:
				if (theBoardLevel == 31)
				{
					this.SetSuperTorch();
					return;
				}
				if (theBoardLevel != 32)
				{
					return;
				}
				this.SetSuperKelp();
				break;
			}
		}
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00008178 File Offset: 0x00006378
	private void SetSuperTorch()
	{
		CreatePlant.Instance.SetPlant(4, 0, 0, null, default(Vector2), false, 0f);
		CreatePlant.Instance.SetPlant(5, 0, 18, null, default(Vector2), false, 0f);
		CreatePlant.Instance.SetPlant(4, 1, 1000, null, default(Vector2), false, 0f);
		CreatePlant.Instance.SetPlant(5, 1, 1053, null, default(Vector2), false, 0f);
		CreatePlant.Instance.SetPlant(4, 2, 1001, null, default(Vector2), false, 0f);
		CreatePlant.Instance.SetPlant(5, 2, 1052, null, default(Vector2), false, 0f);
		CreatePlant.Instance.SetPlant(4, 3, 1000, null, default(Vector2), false, 0f);
		CreatePlant.Instance.SetPlant(5, 3, 1053, null, default(Vector2), false, 0f);
		CreatePlant.Instance.SetPlant(4, 4, 0, null, default(Vector2), false, 0f);
		CreatePlant.Instance.SetPlant(5, 4, 18, null, default(Vector2), false, 0f);
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x000082CC File Offset: 0x000064CC
	private void SetSuperKelp()
	{
		CreatePlant.Instance.SetPlant(0, 2, 1066, null, default(Vector2), false, 0f);
		CreatePlant.Instance.SetPlant(0, 3, 1066, null, default(Vector2), false, 0f);
		CreatePlant.Instance.SetPlant(1, 2, 1051, null, default(Vector2), false, 0f);
		CreatePlant.Instance.SetPlant(1, 3, 1051, null, default(Vector2), false, 0f);
		CreatePlant.Instance.SetPlant(2, 2, 1050, null, default(Vector2), false, 0f);
		CreatePlant.Instance.SetPlant(2, 3, 1050, null, default(Vector2), false, 0f);
	}

	// Token: 0x040000A9 RID: 169
	public static Board Instance;

	// Token: 0x040000AA RID: 170
	public GameObject[] plantArray = new GameObject[256];

	// Token: 0x040000AB RID: 171
	public GameObject[] bulletArray = new GameObject[1024];

	// Token: 0x040000AC RID: 172
	public List<GameObject> zombieArray = new List<GameObject>();

	// Token: 0x040000AD RID: 173
	public GameObject[] coinArray = new GameObject[1024];

	// Token: 0x040000AE RID: 174
	public GameObject[] griditemArray = new GameObject[256];

	// Token: 0x040000AF RID: 175
	public GameObject[] mowerArray = new GameObject[6];

	// Token: 0x040000B0 RID: 176
	public GameObject[] fireLineArray = new GameObject[6];

	// Token: 0x040000B1 RID: 177
	public float[] iceRoadX = new float[]
	{
		15f,
		15f,
		15f,
		15f,
		15f,
		15f
	};

	// Token: 0x040000B2 RID: 178
	public float[] iceRoadFadeTime = new float[6];

	// Token: 0x040000B3 RID: 179
	public int[] roadType = new int[6];

	// Token: 0x040000B4 RID: 180
	public int[,] boxType = new int[10, 6];

	// Token: 0x040000B5 RID: 181
	public int theSun = 500;

	// Token: 0x040000B6 RID: 182
	public int theCurrentNumOfZombieUncontroled;

	// Token: 0x040000B7 RID: 183
	public int theTotalNumOfZombie;

	// Token: 0x040000B8 RID: 184
	public int theTotalNumOfCoin;

	// Token: 0x040000B9 RID: 185
	public float time;

	// Token: 0x040000BA RID: 186
	public float theFallingSunCountDown = 7.5f;

	// Token: 0x040000BB RID: 187
	public float newZombieWaveCountDown = 15f;

	// Token: 0x040000BC RID: 188
	public float nextZombieWaveCountDown = 30f;

	// Token: 0x040000BD RID: 189
	private float hugeWaveCountDown;

	// Token: 0x040000BE RID: 190
	private bool isHugeWave;

	// Token: 0x040000BF RID: 191
	public int theWave;

	// Token: 0x040000C0 RID: 192
	public int theMaxWave;

	// Token: 0x040000C1 RID: 193
	public int theSurvivalMaxRound;

	// Token: 0x040000C2 RID: 194
	public int theCurrentSurvivalRound;

	// Token: 0x040000C3 RID: 195
	public bool isEndless;

	// Token: 0x040000C4 RID: 196
	public bool isTravel;

	// Token: 0x040000C5 RID: 197
	public float zombieTotalHealth;

	// Token: 0x040000C6 RID: 198
	public float zombieCurrentHealth;

	// Token: 0x040000C7 RID: 199
	public float zombieHealthUpdater;

	// Token: 0x040000C8 RID: 200
	public int musicType;

	// Token: 0x040000C9 RID: 201
	public float holdOnTime;

	// Token: 0x040000CA RID: 202
	public float iceDoomFreezeTime;

	// Token: 0x040000CB RID: 203
	public bool isIZ;

	// Token: 0x040000CC RID: 204
	public bool isNight;

	// Token: 0x040000CD RID: 205
	public int roadNum = 5;

	// Token: 0x040000CE RID: 206
	public GameObject theInGameUI;

	// Token: 0x040000CF RID: 207
	public CreateZombie createZombie;

	// Token: 0x040000D0 RID: 208
	public CreatePlant createPlant;

	// Token: 0x040000D1 RID: 209
	public bool droppedAwardOrOver;

	// Token: 0x040000D2 RID: 210
	public InGameText inGameText;

	// Token: 0x040000D3 RID: 211
	public bool freeCD;

	// Token: 0x040000D4 RID: 212
	public bool isEveStart;

	// Token: 0x040000D5 RID: 213
	public bool isEveStarted;

	// Token: 0x040000D6 RID: 214
	private float eveCountDown;

	// Token: 0x040000D7 RID: 215
	private float eveCurrentTime;

	// Token: 0x040000D8 RID: 216
	public bool[] disAllowSetZombie = new bool[5];

	// Token: 0x040000D9 RID: 217
	public bool isAutoEve;

	// Token: 0x040000DA RID: 218
	public bool isScaredyDream;

	// Token: 0x040000DB RID: 219
	public bool isTowerDefense;
}

using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x020000F7 RID: 247
public class GameAPP : MonoBehaviour
{
	// Token: 0x060004BA RID: 1210 RVA: 0x00026E9C File Offset: 0x0002509C
	private void Awake()
	{
		this.AddComponent<SaveInfo>();
		GameAPP.music = base.GetComponent<AudioSource>();
		GameAPP.musicDrum = Camera.main.GetComponent<AudioSource>();
		GameAPP.LoadResources();
		GameAPP.gameAPP = base.gameObject;
		GameAPP.canvas = GameObject.Find("Canvas");
		GameAPP.canvasUp = GameObject.Find("CanvasUp");
		Application.targetFrameRate = 200;
		MixData.InitMixData();
		Time.timeScale = GameAPP.gameSpeed;
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x00026F16 File Offset: 0x00025116
	private void Start()
	{
		UIMgr.EnterMainMenu();
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x00026F20 File Offset: 0x00025120
	public static void PlaySoundNotPause(int theSoundID, float theVolume = 0.5f)
	{
		if (GameAPP.audioPrefab[theSoundID] == null)
		{
			GameAPP.LoadSound();
		}
		AudioClip clip = GameAPP.audioPrefab[theSoundID];
		AudioManager.Instance.PlaySound(clip, theVolume, 1f);
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x00026F5C File Offset: 0x0002515C
	public static void PlaySound(int theSoundID, float theVolume = 0.5f)
	{
		int num = 0;
		float num2 = float.MaxValue;
		foreach (SoundCtrl soundCtrl in GameAPP.sound)
		{
			if (soundCtrl.theSoundID == theSoundID)
			{
				num++;
				if (soundCtrl.existTime < num2)
				{
					num2 = soundCtrl.existTime;
				}
			}
		}
		if (num2 < 0.1f)
		{
			return;
		}
		if (num > 4)
		{
			return;
		}
		if (theSoundID != 49)
		{
			if (theSoundID == 69 && num > 1)
			{
				return;
			}
		}
		else
		{
			theVolume = 0.4f;
		}
		AudioClip clip = GameAPP.audioPrefab[theSoundID];
		GameObject gameObject = new GameObject();
		gameObject.name = "SoundPlayer";
		AudioSource audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = clip;
		SoundCtrl soundCtrl2 = gameObject.AddComponent<SoundCtrl>();
		soundCtrl2.theSoundID = theSoundID;
		GameAPP.sound.Add(soundCtrl2);
		audioSource.volume = theVolume * GameAPP.gameSoundVolume;
		switch (theSoundID)
		{
		case 0:
		case 1:
		case 2:
			audioSource.time = 0.05f;
			audioSource.pitch = Random.Range(1f, 1.7f);
			goto IL_189;
		case 3:
		case 4:
		case 16:
		case 17:
			audioSource.pitch = Random.Range(1f, 1.8f);
			goto IL_189;
		case 5:
		case 6:
		case 8:
		case 9:
		case 11:
			goto IL_189;
		case 7:
		case 10:
		case 13:
		case 14:
		case 15:
			audioSource.pitch = Random.Range(1f, 1.5f);
			goto IL_189;
		case 12:
			break;
		default:
			if (theSoundID != 57 && theSoundID != 80)
			{
				goto IL_189;
			}
			break;
		}
		audioSource.pitch = Random.Range(1f, 1.4f);
		IL_189:
		audioSource.Play();
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x00027108 File Offset: 0x00025308
	public static void ChangeMusic(int id)
	{
		GameAPP.music.Stop();
		GameAPP.musicDrum.Stop();
		AudioClip clip = GameAPP.musicPrefab[id];
		GameAPP.music.clip = clip;
		switch (id)
		{
		case 0:
			GameAPP.music.time = 1f;
			GameAPP.music.Play();
			return;
		case 2:
		{
			AudioClip clip2 = GameAPP.musicPrefab[3];
			GameAPP.musicDrum.clip = clip2;
			GameAPP.musicDrum.volume = 0f;
			GameAPP.musicDrum.time = 0f;
			GameAPP.musicDrum.Play();
			Board.Instance.musicType = 1;
			break;
		}
		case 4:
			Board.Instance.musicType = 2;
			break;
		case 6:
		{
			AudioClip clip3 = GameAPP.musicPrefab[7];
			GameAPP.musicDrum.clip = clip3;
			GameAPP.musicDrum.volume = 0f;
			GameAPP.musicDrum.time = 0f;
			GameAPP.musicDrum.Play();
			Board.Instance.musicType = 1;
			break;
		}
		}
		GameAPP.music.volume = GameAPP.gameMusicVolume;
		GameAPP.music.time = 0f;
		GameAPP.music.Play();
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x00027240 File Offset: 0x00025440
	private static void LoadResources()
	{
		GameAPP.LoadSound();
		GameAPP.LoadPlant();
		GameAPP.LoadPrePlant();
		GameAPP.LoadZombie();
		GameAPP.LoadPreZombie();
		GameAPP.LoadBullet();
		GameAPP.LoadParticle();
		GameAPP.LoadCoin();
		GameAPP.LoadSprite();
		GameAPP.LoadMusic();
		GameAPP.LoadGridItem();
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x0002727C File Offset: 0x0002547C
	private static void LoadSound()
	{
		GameAPP.audioPrefab[0] = Resources.Load<AudioClip>("Sound/BulletHit/splat");
		GameAPP.audioPrefab[1] = Resources.Load<AudioClip>("Sound/BulletHit/splat2");
		GameAPP.audioPrefab[2] = Resources.Load<AudioClip>("Sound/BulletHit/splat3");
		GameAPP.audioPrefab[3] = Resources.Load<AudioClip>("Sound/PlantShoot/throw");
		GameAPP.audioPrefab[4] = Resources.Load<AudioClip>("Sound/PlantShoot/throw2");
		GameAPP.audioPrefab[5] = Resources.Load<AudioClip>("Sound/ZombieDie/zombie_falling_1");
		GameAPP.audioPrefab[6] = Resources.Load<AudioClip>("Sound/ZombieDie/zombie_falling_2");
		GameAPP.audioPrefab[7] = Resources.Load<AudioClip>("Sound/ZombieDie/limbs_pop");
		GameAPP.audioPrefab[8] = Resources.Load<AudioClip>("Sound/ZombieEat/chomp");
		GameAPP.audioPrefab[9] = Resources.Load<AudioClip>("Sound/ZombieEat/chomp2");
		GameAPP.audioPrefab[10] = Resources.Load<AudioClip>("Sound/ZombieEat/chompsoft");
		GameAPP.audioPrefab[11] = Resources.Load<AudioClip>("Sound/ZombieEat/gulp");
		GameAPP.audioPrefab[12] = Resources.Load<AudioClip>("Sound/BulletHit/plastichit");
		GameAPP.audioPrefab[13] = Resources.Load<AudioClip>("Sound/BulletHit/plastichit2");
		GameAPP.audioPrefab[14] = Resources.Load<AudioClip>("Sound/BulletHit/shieldhit");
		GameAPP.audioPrefab[15] = Resources.Load<AudioClip>("Sound/BulletHit/shieldhit2");
		GameAPP.audioPrefab[16] = Resources.Load<AudioClip>("Sound/CollectItem/points");
		GameAPP.audioPrefab[17] = Resources.Load<AudioClip>("Sound/CollectItem/coin");
		GameAPP.audioPrefab[18] = Resources.Load<AudioClip>("Sound/CollectItem/coin");
		GameAPP.audioPrefab[19] = Resources.Load<AudioClip>("Sound/BottonAndPutDown/tap");
		GameAPP.audioPrefab[20] = Resources.Load<AudioClip>("Sound/BottonAndPutDown/tap2");
		GameAPP.audioPrefab[21] = Resources.Load<AudioClip>("Sound/BottonAndPutDown/shovel");
		GameAPP.audioPrefab[22] = Resources.Load<AudioClip>("Sound/PlacePlant/plant");
		GameAPP.audioPrefab[23] = Resources.Load<AudioClip>("Sound/PlacePlant/plant2");
		GameAPP.audioPrefab[24] = Resources.Load<AudioClip>("Sound/PlacePlant/plant_water");
		GameAPP.audioPrefab[25] = Resources.Load<AudioClip>("Sound/PlacePlant/seedlift");
		GameAPP.audioPrefab[26] = Resources.Load<AudioClip>("Sound/ClickFail/buzzer");
		GameAPP.audioPrefab[27] = Resources.Load<AudioClip>("Sound/BottonAndPutDown/bleep");
		GameAPP.audioPrefab[28] = Resources.Load<AudioClip>("Sound/BottonAndPutDown/gravebutton");
		GameAPP.audioPrefab[29] = Resources.Load<AudioClip>("Sound/BottonAndPutDown/buttonclick");
		GameAPP.audioPrefab[30] = Resources.Load<AudioClip>("Sound/BottonAndPutDown/pause");
		GameAPP.audioPrefab[31] = Resources.Load<AudioClip>("Sound/TextSound/readysetplant");
		GameAPP.audioPrefab[32] = Resources.Load<AudioClip>("Sound/TextSound/hugewave");
		GameAPP.audioPrefab[33] = Resources.Load<AudioClip>("Sound/TextSound/finalwave");
		GameAPP.audioPrefab[34] = Resources.Load<AudioClip>("Sound/TextSound/awooga");
		GameAPP.audioPrefab[35] = Resources.Load<AudioClip>("Sound/TextSound/siren");
		GameAPP.audioPrefab[37] = Resources.Load<AudioClip>("Sound/Award/lightfill");
		GameAPP.audioPrefab[38] = Resources.Load<AudioClip>("Sound/BottonAndPutDown/roll_in");
		GameAPP.audioPrefab[39] = Resources.Load<AudioClip>("Sound/Bomb/reverse_explosion");
		GameAPP.audioPrefab[40] = Resources.Load<AudioClip>("Sound/Bomb/cherrybomb");
		GameAPP.audioPrefab[41] = Resources.Load<AudioClip>("Sound/Bomb/doomshroom");
		GameAPP.audioPrefab[42] = Resources.Load<AudioClip>("Sound/Bomb/jalapeno");
		GameAPP.audioPrefab[43] = Resources.Load<AudioClip>("Sound/Bomb/explosion");
		GameAPP.audioPrefab[44] = Resources.Load<AudioClip>("Sound/Zombie/newspaper_rip");
		GameAPP.audioPrefab[45] = Resources.Load<AudioClip>("Sound/Zombie/newspaper_rarrgh");
		GameAPP.audioPrefab[46] = Resources.Load<AudioClip>("Sound/Zombie/newspaper_rarrgh2");
		GameAPP.audioPrefab[47] = Resources.Load<AudioClip>("Sound/Bomb/potato_mine");
		GameAPP.audioPrefab[48] = Resources.Load<AudioClip>("Sound/plant/dirt_rise");
		GameAPP.audioPrefab[49] = Resources.Load<AudioClip>("Sound/plant/bigchomp");
		GameAPP.audioPrefab[50] = Resources.Load<AudioClip>("Sound/Zombie/grassstep");
		GameAPP.audioPrefab[51] = Resources.Load<AudioClip>("Sound/Zombie/polevault");
		GameAPP.audioPrefab[52] = Resources.Load<AudioClip>("Sound/lose/losemusic");
		GameAPP.audioPrefab[53] = Resources.Load<AudioClip>("Sound/plant/bowling");
		GameAPP.audioPrefab[54] = Resources.Load<AudioClip>("Sound/plant/bowlingimpact");
		GameAPP.audioPrefab[55] = Resources.Load<AudioClip>("Sound/plant/bowlingimpact2");
		GameAPP.audioPrefab[56] = Resources.Load<AudioClip>("Sound/plant/plantgrow");
		GameAPP.audioPrefab[57] = Resources.Load<AudioClip>("Sound/plant/puff");
		GameAPP.audioPrefab[58] = Resources.Load<AudioClip>("Sound/plant/fume");
		GameAPP.audioPrefab[59] = Resources.Load<AudioClip>("Sound/fire/ignite");
		GameAPP.audioPrefab[60] = Resources.Load<AudioClip>("Sound/fire/ignite2");
		GameAPP.audioPrefab[61] = Resources.Load<AudioClip>("Sound/fire/firepea");
		GameAPP.audioPrefab[62] = Resources.Load<AudioClip>("Sound/plant/floop");
		GameAPP.audioPrefab[63] = Resources.Load<AudioClip>("Sound/plant/mindcontrolled");
		GameAPP.audioPrefab[64] = Resources.Load<AudioClip>("Sound/Zombie/bonk");
		GameAPP.audioPrefab[65] = Resources.Load<AudioClip>("Sound/Item/fertilizer");
		GameAPP.audioPrefab[66] = Resources.Load<AudioClip>("Sound/Item/prize");
		GameAPP.audioPrefab[67] = Resources.Load<AudioClip>("Sound/Zombie/frozen");
		GameAPP.audioPrefab[68] = Resources.Load<AudioClip>("Sound/plant/snow_pea_sparkles");
		GameAPP.audioPrefab[69] = Resources.Load<AudioClip>("Sound/Zombie/dancer");
		GameAPP.audioPrefab[70] = Resources.Load<AudioClip>("Sound/Bomb/SmallDoom");
		GameAPP.audioPrefab[71] = Resources.Load<AudioClip>("Sound/PlacePlant/plant_water");
		GameAPP.audioPrefab[72] = Resources.Load<AudioClip>("Sound/plant/squash_hmm");
		GameAPP.audioPrefab[73] = Resources.Load<AudioClip>("Sound/plant/squash_hmm2");
		GameAPP.audioPrefab[74] = Resources.Load<AudioClip>("Sound/plant/gargantuar_thump");
		GameAPP.audioPrefab[75] = Resources.Load<AudioClip>("Sound/Zombie/zombiesplash");
		GameAPP.audioPrefab[76] = Resources.Load<AudioClip>("Sound/Zombie/zamboni");
		GameAPP.audioPrefab[77] = Resources.Load<AudioClip>("Sound/Zombie/balloon_pop");
		GameAPP.audioPrefab[78] = Resources.Load<AudioClip>("Sound/Zombie/dolphin_appears");
		GameAPP.audioPrefab[79] = Resources.Load<AudioClip>("Sound/Zombie/dolphin_before_jumping");
		GameAPP.audioPrefab[80] = Resources.Load<AudioClip>("Sound/Girl/Cattail_hit");
		GameAPP.audioPrefab[81] = Resources.Load<AudioClip>("Sound/Girl/Cattail_Plant1");
		GameAPP.audioPrefab[82] = Resources.Load<AudioClip>("Sound/Girl/Cattail_Plant2");
		GameAPP.audioPrefab[83] = Resources.Load<AudioClip>("Sound/plant/magnetshroom");
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x00027858 File Offset: 0x00025A58
	private static void LoadPlant()
	{
		GameAPP.plantPrefab[0] = Resources.Load<GameObject>("Plants/PeaShooter/PeashooterPrefab");
		GameAPP.plantPrefab[1] = Resources.Load<GameObject>("Plants/SunFlower/SunflowerPrefab");
		GameAPP.plantPrefab[2] = Resources.Load<GameObject>("Plants/CherryBomb/CherryBombPrefab");
		GameAPP.plantPrefab[3] = Resources.Load<GameObject>("Plants/WallNut/WallNutPrefab");
		GameAPP.plantPrefab[4] = Resources.Load<GameObject>("Plants/PotatoMine/PotatoMinePrefab");
		GameAPP.plantPrefab[5] = Resources.Load<GameObject>("Plants/Chomper/ChomperPrefab");
		GameAPP.plantPrefab[6] = Resources.Load<GameObject>("Plants/SmallPuff/SmallPuffPrefab");
		GameAPP.plantPrefab[7] = Resources.Load<GameObject>("Plants/FumeShroom/FumeShroomPrefab");
		GameAPP.plantPrefab[8] = Resources.Load<GameObject>("Plants/HypnoShroom/HypnoShroomPrefab");
		GameAPP.plantPrefab[9] = Resources.Load<GameObject>("Plants/ScaredyShroom/ScaredyShroomPrefab");
		GameAPP.plantPrefab[10] = Resources.Load<GameObject>("Plants/IceShroom/IceShroomPrefab");
		GameAPP.plantPrefab[11] = Resources.Load<GameObject>("Plants/DoomShroom/DoomShroomPrefab");
		GameAPP.plantPrefab[12] = Resources.Load<GameObject>("Plants/LilyPad/LilyPadPrefab");
		GameAPP.plantPrefab[13] = Resources.Load<GameObject>("Plants/Squash/SquashPrefab");
		GameAPP.plantPrefab[14] = Resources.Load<GameObject>("Plants/ThreePeater/ThreePeaterPrefab");
		GameAPP.plantPrefab[15] = Resources.Load<GameObject>("Plants/Tanglekelp/TanglekelpPrefab");
		GameAPP.plantPrefab[16] = Resources.Load<GameObject>("Plants/Jalapeno/JalapenoPrefab");
		GameAPP.plantPrefab[17] = Resources.Load<GameObject>("Plants/Caltrop/CaltropPrefab");
		GameAPP.plantPrefab[18] = Resources.Load<GameObject>("Plants/TorchWood/TorchWoodPrefab");
		GameAPP.plantPrefab[252] = Resources.Load<GameObject>("Plants/UniquePlants/PlantGirls/Cattail/CattailPrefab");
		GameAPP.plantPrefab[253] = Resources.Load<GameObject>("Plants/UniquePlants/Wheat/WheatPrefab");
		GameAPP.plantPrefab[254] = Resources.Load<GameObject>("Plants/UniquePlants/Endoflame/EndoFlamePrefab");
		GameAPP.plantPrefab[255] = Resources.Load<GameObject>("Plants/WallNut/BigWallNutPrefab");
		GameAPP.plantPrefab[256] = Resources.Load<GameObject>("Plants/Present/PresentPrefab");
		GameAPP.plantPrefab[900] = Resources.Load<GameObject>("Plants/Travel/HyponoT/HypnoEmperorPrefab");
		GameAPP.plantPrefab[901] = Resources.Load<GameObject>("Plants/_Mixer/SuperCherryShooter/SuperCherryGatlingPrefab");
		GameAPP.plantPrefab[902] = Resources.Load<GameObject>("Plants/TorchWood/FireSquashTorchPrefab");
		GameAPP.plantPrefab[903] = Resources.Load<GameObject>("Plants/_Mixer/SuperChomper/SuperCherryChomperPrefab");
		GameAPP.plantPrefab[904] = Resources.Load<GameObject>("Plants/_Mixer/IceDoomFume/FinalFumePrefab");
		GameAPP.plantPrefab[1000] = Resources.Load<GameObject>("Plants/_Mixer/PeaSunFlower/PeaSunFlowerPrefab");
		GameAPP.plantPrefab[1001] = Resources.Load<GameObject>("Plants/_Mixer/CherryShooter/CherryshooterPrefab");
		GameAPP.plantPrefab[1002] = Resources.Load<GameObject>("Plants/_Mixer/SunBomb/SunBombPrefab");
		GameAPP.plantPrefab[1003] = Resources.Load<GameObject>("Plants/_Mixer/CherryNut/CherryNutPrefab");
		GameAPP.plantPrefab[1004] = Resources.Load<GameObject>("Plants/_Mixer/NutShooter/NutShooterPrefab");
		GameAPP.plantPrefab[1005] = Resources.Load<GameObject>("Plants/_Mixer/SuperCherryShooter/SuperCherryShooterPrefab");
		GameAPP.plantPrefab[1006] = Resources.Load<GameObject>("Plants/_Mixer/SunNut/SunNutPrefab");
		GameAPP.plantPrefab[1007] = Resources.Load<GameObject>("Plants/_Mixer/PeaMine/PeaMinePrefab");
		GameAPP.plantPrefab[1008] = Resources.Load<GameObject>("Plants/_Mixer/DoubleCherry/DoubleCherryPrefab");
		GameAPP.plantPrefab[1009] = Resources.Load<GameObject>("Plants/_Mixer/SunMine/SunMinePrefab");
		GameAPP.plantPrefab[1010] = Resources.Load<GameObject>("Plants/_Mixer/PotatoNut/PotatoNutPrefab");
		GameAPP.plantPrefab[1011] = Resources.Load<GameObject>("Plants/_Mixer/PeaChomper/PeaChomperPrefab");
		GameAPP.plantPrefab[1012] = Resources.Load<GameObject>("Plants/_Mixer/NutChomper/NutChomperPrefab");
		GameAPP.plantPrefab[1013] = Resources.Load<GameObject>("Plants/_Mixer/SuperChomper/SuperChomperPrefab");
		GameAPP.plantPrefab[1014] = Resources.Load<GameObject>("Plants/_Mixer/SunChomper/SunChomperPrefab");
		GameAPP.plantPrefab[1015] = Resources.Load<GameObject>("Plants/_Mixer/PotatoChomper/PotatoChomperPrefab");
		GameAPP.plantPrefab[1016] = Resources.Load<GameObject>("Plants/_Mixer/CherryChomper/CherryChomperPrefab");
		GameAPP.plantPrefab[1017] = Resources.Load<GameObject>("Plants/_Mixer/CherryGatlingPea/CherryGatlingPrefab");
		GameAPP.plantPrefab[1018] = Resources.Load<GameObject>("Plants/_Mixer/PeaSmallPuff/PeaSmallPuffPrefab");
		GameAPP.plantPrefab[1019] = Resources.Load<GameObject>("Plants/_Mixer/DoublePuff/DoublePuffPrefab");
		GameAPP.plantPrefab[1020] = Resources.Load<GameObject>("Plants/_Mixer/IronPea/IronPeaPrefab");
		GameAPP.plantPrefab[1021] = Resources.Load<GameObject>("Plants/_Mixer/PuffNut/PuffNutPrefab");
		GameAPP.plantPrefab[1022] = Resources.Load<GameObject>("Plants/_Mixer/HypnoPuff/HypnoPuffPrefab");
		GameAPP.plantPrefab[1023] = Resources.Load<GameObject>("Plants/_Mixer/HypnoFume/HypnoFumePrefab");
		GameAPP.plantPrefab[1024] = Resources.Load<GameObject>("Plants/_Mixer/ScaredyHypno/ScaredyHypnoPrefab");
		GameAPP.plantPrefab[1025] = Resources.Load<GameObject>("Plants/_Mixer/ScaredFume/ScaredFumePrefab");
		GameAPP.plantPrefab[1026] = Resources.Load<GameObject>("Plants/_Mixer/SuperHypno/SuperHypnoPrefab");
		GameAPP.plantPrefab[1027] = Resources.Load<GameObject>("Plants/TallNut/TallNutPrefab");
		GameAPP.plantPrefab[1028] = Resources.Load<GameObject>("Plants/TallNut/TallNutFootballPrefab");
		GameAPP.plantPrefab[1029] = Resources.Load<GameObject>("Plants/WallNut/IronNutPrefab");
		GameAPP.plantPrefab[1030] = Resources.Load<GameObject>("Plants/DoublePea/DoubleShooterPrefab");
		GameAPP.plantPrefab[1031] = Resources.Load<GameObject>("Plants/SunShroom/SunShroomPrefab");
		GameAPP.plantPrefab[1032] = Resources.Load<GameObject>("Plants/GatlingPea/GatlingPeaPrefab");
		GameAPP.plantPrefab[1033] = Resources.Load<GameObject>("Plants/TwinFlower/TwinFlowerPrefab");
		GameAPP.plantPrefab[1034] = Resources.Load<GameObject>("Plants/ShowPeaShooter/SnowPeaShooterPrefab");
		GameAPP.plantPrefab[1035] = Resources.Load<GameObject>("Plants/_Mixer/IcePuff/IcePuffPrefab");
		GameAPP.plantPrefab[1036] = Resources.Load<GameObject>("Plants/_Mixer/SmallIceShroom/SmallIceShroomPrefab");
		GameAPP.plantPrefab[1037] = Resources.Load<GameObject>("Plants/_Mixer/IceFumeShroom/IceFumeShroomPrefab");
		GameAPP.plantPrefab[1038] = Resources.Load<GameObject>("Plants/_Mixer/IceScaredyShroom/IceScaredyShroomPrefab");
		GameAPP.plantPrefab[1039] = Resources.Load<GameObject>("Plants/TallNut/TallIceNutPrefab");
		GameAPP.plantPrefab[1040] = Resources.Load<GameObject>("Plants/_Mixer/IceDoom/IceDoomPrefab");
		GameAPP.plantPrefab[1041] = Resources.Load<GameObject>("Plants/_Mixer/IceHypno/IceHypnoPrefab");
		GameAPP.plantPrefab[1042] = Resources.Load<GameObject>("Plants/_Mixer/ScaredyDoom/ScaredyDoomPrefab");
		GameAPP.plantPrefab[1043] = Resources.Load<GameObject>("Plants/_Mixer/DoomFume/DoomFumePrefab");
		GameAPP.plantPrefab[1044] = Resources.Load<GameObject>("Plants/_Mixer/DoomPuff/PuffDoomPrefab");
		GameAPP.plantPrefab[1045] = Resources.Load<GameObject>("Plants/_Mixer/HypnoDoom/HypnoDoomPrefab");
		GameAPP.plantPrefab[1046] = Resources.Load<GameObject>("Plants/_Mixer/IceDoomFume/IceDoomFumePrefab");
		GameAPP.plantPrefab[1047] = Resources.Load<GameObject>("Plants/_Mixer/ThreeSquash/ThreeSquashPrefab");
		GameAPP.plantPrefab[1048] = Resources.Load<GameObject>("Plants/TorchWood/EliteTorchWoodPrefab");
		GameAPP.plantPrefab[1049] = Resources.Load<GameObject>("Plants/_Mixer/Jalatang/JalatangPrefab");
		GameAPP.plantPrefab[1050] = Resources.Load<GameObject>("Plants/_Mixer/Squashtang/SquashtangPrefab");
		GameAPP.plantPrefab[1051] = Resources.Load<GameObject>("Plants/_Mixer/Threetang/ThreetangPrefab");
		GameAPP.plantPrefab[1052] = Resources.Load<GameObject>("Plants/TorchWood/EpicTorchWoodPrefab");
		GameAPP.plantPrefab[1053] = Resources.Load<GameObject>("Plants/TorchWood/AdvancedTorchWoodPrefab");
		GameAPP.plantPrefab[1054] = Resources.Load<GameObject>("Plants/Squash/JalaSquashPrefab");
		GameAPP.plantPrefab[1055] = Resources.Load<GameObject>("Plants/_Mixer/ThreeTorch/ThreeTorchPrefab");
		GameAPP.plantPrefab[1056] = Resources.Load<GameObject>("Plants/_Mixer/KelpTorch/KelpTorchPrefab");
		GameAPP.plantPrefab[1057] = Resources.Load<GameObject>("Plants/Squash/FireSquashPrefab");
		GameAPP.plantPrefab[1058] = Resources.Load<GameObject>("Plants/ThreePeater/DarkThreePeaterPrefab");
		GameAPP.plantPrefab[1059] = Resources.Load<GameObject>("Plants/TorchWood/SquashTorchPrefab");
		GameAPP.plantPrefab[1060] = Resources.Load<GameObject>("Plants/Caltrop/SpikeRock/SpikerockPrefab");
		GameAPP.plantPrefab[1061] = Resources.Load<GameObject>("Plants/Caltrop/FireSpike/FireSpikePrefab");
		GameAPP.plantPrefab[1062] = Resources.Load<GameObject>("Plants/Caltrop/JalaSpike/JalaSpikePrefab");
		GameAPP.plantPrefab[1063] = Resources.Load<GameObject>("Plants/Caltrop/SquashSpike/SquashSpikePrefab");
		GameAPP.plantPrefab[1064] = Resources.Load<GameObject>("Plants/Caltrop/ThreeSpike/ThreeSpikePrefab");
		GameAPP.plantPrefab[1065] = Resources.Load<GameObject>("Plants/_Mixer/DoublePuff/GatlingPuffPrefab");
		GameAPP.plantPrefab[1066] = Resources.Load<GameObject>("Plants/_Mixer/SuperKelp/SuperKelpPrefab");
		GameAPP.plantPrefab[1067] = Resources.Load<GameObject>("Plants/CattailPlant/CattailPlantPrefab");
		GameAPP.plantPrefab[1068] = Resources.Load<GameObject>("Plants/CattailPlant/IceCattailPrefab");
		GameAPP.plantPrefab[1069] = Resources.Load<GameObject>("Plants/CattailPlant/FireCattailPrefab");
		GameAPP.plantPrefab[1070] = Resources.Load<GameObject>("Plants/GloomShroom/GloomShroomPrefab");
		GameAPP.plantPrefab[1071] = Resources.Load<GameObject>("Plants/GloomShroom/FireGloomPrefab");
		GameAPP.plantPrefab[1072] = Resources.Load<GameObject>("Plants/GloomShroom/IceGloomPrefab");
		GameAPP.plantPrefab[1073] = Resources.Load<GameObject>("Plants/TallNut/TallFireNutPrefab");
		GameAPP.plantPrefab[1074] = Resources.Load<GameObject>("Plants/Caltrop/SpikeRock/IceSpikerockPrefab");
		GameAPP.plantPrefab[1075] = Resources.Load<GameObject>("Plants/Caltrop/SpikeRock/FireSpikerockPrefab");
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x000280C0 File Offset: 0x000262C0
	private static void LoadPrePlant()
	{
		GameAPP.prePlantPrefab[0] = Resources.Load<GameObject>("Plants/PeaShooter/PeaShooterPreview");
		GameAPP.prePlantPrefab[1] = Resources.Load<GameObject>("Plants/SunFlower/SunflowerPreview");
		GameAPP.prePlantPrefab[2] = Resources.Load<GameObject>("Plants/CherryBomb/CherryBombPreview");
		GameAPP.prePlantPrefab[3] = Resources.Load<GameObject>("Plants/WallNut/WallNutPreview");
		GameAPP.prePlantPrefab[4] = Resources.Load<GameObject>("Plants/PotatoMine/PotatoMinePreview");
		GameAPP.prePlantPrefab[5] = Resources.Load<GameObject>("Plants/Chomper/ChomperPreview");
		GameAPP.prePlantPrefab[6] = Resources.Load<GameObject>("Plants/SmallPuff/SmallPuffPreview");
		GameAPP.prePlantPrefab[7] = Resources.Load<GameObject>("Plants/FumeShroom/FumeShroomPreview");
		GameAPP.prePlantPrefab[8] = Resources.Load<GameObject>("Plants/HypnoShroom/HypnoShroomPreview");
		GameAPP.prePlantPrefab[9] = Resources.Load<GameObject>("Plants/ScaredyShroom/ScaredyShroomPreview");
		GameAPP.prePlantPrefab[10] = Resources.Load<GameObject>("Plants/IceShroom/IceShroomPreview");
		GameAPP.prePlantPrefab[11] = Resources.Load<GameObject>("Plants/DoomShroom/DoomShroomPreview");
		GameAPP.prePlantPrefab[12] = Resources.Load<GameObject>("Plants/LilyPad/LilyPadPreview");
		GameAPP.prePlantPrefab[13] = Resources.Load<GameObject>("Plants/Squash/SquashPreview");
		GameAPP.prePlantPrefab[14] = Resources.Load<GameObject>("Plants/ThreePeater/ThreePeaterPreview");
		GameAPP.prePlantPrefab[15] = Resources.Load<GameObject>("Plants/Tanglekelp/TanglekelpPreview");
		GameAPP.prePlantPrefab[16] = Resources.Load<GameObject>("Plants/Jalapeno/JalapenoPreview");
		GameAPP.prePlantPrefab[17] = Resources.Load<GameObject>("Plants/Caltrop/CaltropPreview");
		GameAPP.prePlantPrefab[18] = Resources.Load<GameObject>("Plants/TorchWood/TorchWoodPreview");
		GameAPP.prePlantPrefab[252] = Resources.Load<GameObject>("Plants/UniquePlants/PlantGirls/Cattail/CattailPreview");
		GameAPP.prePlantPrefab[253] = Resources.Load<GameObject>("Plants/UniquePlants/Wheat/WheatPreview");
		GameAPP.prePlantPrefab[254] = Resources.Load<GameObject>("Plants/UniquePlants/Endoflame/EndoFlamePreview");
		GameAPP.prePlantPrefab[256] = Resources.Load<GameObject>("Plants/Present/PresentPreview");
		GameAPP.prePlantPrefab[900] = Resources.Load<GameObject>("Plants/Travel/HyponoT/HypnoEmperorPreview");
		GameAPP.prePlantPrefab[901] = Resources.Load<GameObject>("Plants/_Mixer/SuperCherryShooter/SuperCherryGatlingPreview");
		GameAPP.prePlantPrefab[902] = Resources.Load<GameObject>("Plants/TorchWood/FireSquashTorchPreview");
		GameAPP.prePlantPrefab[903] = Resources.Load<GameObject>("Plants/_Mixer/SuperChomper/SuperCherryChomperPreview");
		GameAPP.prePlantPrefab[904] = Resources.Load<GameObject>("Plants/_Mixer/IceDoomFume/FinalFumePreview");
		GameAPP.prePlantPrefab[1000] = Resources.Load<GameObject>("Plants/_Mixer/PeaSunFlower/PeaSunFlowerPreview");
		GameAPP.prePlantPrefab[1001] = Resources.Load<GameObject>("Plants/_Mixer/CherryShooter/CherryshooterPreview");
		GameAPP.prePlantPrefab[1002] = Resources.Load<GameObject>("Plants/_Mixer/SunBomb/SunBombPreview");
		GameAPP.prePlantPrefab[1003] = Resources.Load<GameObject>("Plants/_Mixer/CherryNut/CherryNutPreview");
		GameAPP.prePlantPrefab[1004] = Resources.Load<GameObject>("Plants/_Mixer/NutShooter/NutShooterPreview");
		GameAPP.prePlantPrefab[1005] = Resources.Load<GameObject>("Plants/_Mixer/SuperCherryShooter/SuperCherryShooterPreview");
		GameAPP.prePlantPrefab[1006] = Resources.Load<GameObject>("Plants/_Mixer/SunNut/SunNutPreview");
		GameAPP.prePlantPrefab[1007] = Resources.Load<GameObject>("Plants/_Mixer/PeaMine/PeaMinePreview");
		GameAPP.prePlantPrefab[1008] = Resources.Load<GameObject>("Plants/_Mixer/DoubleCherry/DoubleCherryPreview");
		GameAPP.prePlantPrefab[1009] = Resources.Load<GameObject>("Plants/_Mixer/SunMine/SunMinePreview");
		GameAPP.prePlantPrefab[1010] = Resources.Load<GameObject>("Plants/_Mixer/PotatoNut/PotatoNutPreview");
		GameAPP.prePlantPrefab[1011] = Resources.Load<GameObject>("Plants/_Mixer/PeaChomper/PeaChomperPreview");
		GameAPP.prePlantPrefab[1012] = Resources.Load<GameObject>("Plants/_Mixer/NutChomper/NutChomperPreview");
		GameAPP.prePlantPrefab[1013] = Resources.Load<GameObject>("Plants/_Mixer/SuperChomper/SuperChomperPreview");
		GameAPP.prePlantPrefab[1014] = Resources.Load<GameObject>("Plants/_Mixer/SunChomper/SunChomperPreview");
		GameAPP.prePlantPrefab[1015] = Resources.Load<GameObject>("Plants/_Mixer/PotatoChomper/PotatoChomperPreview");
		GameAPP.prePlantPrefab[1016] = Resources.Load<GameObject>("Plants/_Mixer/CherryChomper/CherryChomperPreview");
		GameAPP.prePlantPrefab[1017] = Resources.Load<GameObject>("Plants/_Mixer/CherryGatlingPea/CherryGatlingPreview");
		GameAPP.prePlantPrefab[1018] = Resources.Load<GameObject>("Plants/_Mixer/PeaSmallPuff/PeaSmallPuffPreview");
		GameAPP.prePlantPrefab[1019] = Resources.Load<GameObject>("Plants/_Mixer/DoublePuff/DoublePuffPreview");
		GameAPP.prePlantPrefab[1020] = Resources.Load<GameObject>("Plants/_Mixer/IronPea/IronPeaPreview");
		GameAPP.prePlantPrefab[1021] = Resources.Load<GameObject>("Plants/_Mixer/PuffNut/PuffNutPreview");
		GameAPP.prePlantPrefab[1022] = Resources.Load<GameObject>("Plants/_Mixer/HypnoPuff/HypnoPuffPreview");
		GameAPP.prePlantPrefab[1023] = Resources.Load<GameObject>("Plants/_Mixer/HypnoFume/HypnoFumePreview");
		GameAPP.prePlantPrefab[1024] = Resources.Load<GameObject>("Plants/_Mixer/ScaredyHypno/ScaredyHypnoPreview");
		GameAPP.prePlantPrefab[1025] = Resources.Load<GameObject>("Plants/_Mixer/ScaredFume/ScaredFumePreview");
		GameAPP.prePlantPrefab[1026] = Resources.Load<GameObject>("Plants/_Mixer/SuperHypno/SuperHypnoPreview");
		GameAPP.prePlantPrefab[1027] = Resources.Load<GameObject>("Plants/TallNut/TallNutPreview");
		GameAPP.prePlantPrefab[1028] = Resources.Load<GameObject>("Plants/TallNut/TallNutFootballPreview");
		GameAPP.prePlantPrefab[1029] = Resources.Load<GameObject>("Plants/WallNut/IronNutPreview");
		GameAPP.prePlantPrefab[1030] = Resources.Load<GameObject>("Plants/DoublePea/DoubleShooterPreview");
		GameAPP.prePlantPrefab[1031] = Resources.Load<GameObject>("Plants/SunShroom/SunShroomPreview");
		GameAPP.prePlantPrefab[1032] = Resources.Load<GameObject>("Plants/GatlingPea/GatlingPeaPreview");
		GameAPP.prePlantPrefab[1033] = Resources.Load<GameObject>("Plants/TwinFlower/TwinFlowerPreview");
		GameAPP.prePlantPrefab[1034] = Resources.Load<GameObject>("Plants/ShowPeaShooter/SnowPeaShooterPreview");
		GameAPP.prePlantPrefab[1035] = Resources.Load<GameObject>("Plants/_Mixer/IcePuff/IcePuffPreview");
		GameAPP.prePlantPrefab[1036] = Resources.Load<GameObject>("Plants/_Mixer/SmallIceShroom/SmallIceShroomPreview");
		GameAPP.prePlantPrefab[1037] = Resources.Load<GameObject>("Plants/_Mixer/IceFumeShroom/IceFumeShroomPreview");
		GameAPP.prePlantPrefab[1038] = Resources.Load<GameObject>("Plants/_Mixer/IceScaredyShroom/IceScaredyShroomPreview");
		GameAPP.prePlantPrefab[1039] = Resources.Load<GameObject>("Plants/TallNut/TallIceNutPreview");
		GameAPP.prePlantPrefab[1040] = Resources.Load<GameObject>("Plants/_Mixer/IceDoom/IceDoomPreview");
		GameAPP.prePlantPrefab[1041] = Resources.Load<GameObject>("Plants/_Mixer/IceHypno/IceHypnoPreview");
		GameAPP.prePlantPrefab[1042] = Resources.Load<GameObject>("Plants/_Mixer/ScaredyDoom/ScaredyDoomPreview");
		GameAPP.prePlantPrefab[1043] = Resources.Load<GameObject>("Plants/_Mixer/DoomFume/DoomFumePreview");
		GameAPP.prePlantPrefab[1044] = Resources.Load<GameObject>("Plants/_Mixer/DoomPuff/PuffDoomPreview");
		GameAPP.prePlantPrefab[1045] = Resources.Load<GameObject>("Plants/_Mixer/HypnoDoom/HypnoDoomPreview");
		GameAPP.prePlantPrefab[1046] = Resources.Load<GameObject>("Plants/_Mixer/IceDoomFume/IceDoomFumePreview");
		GameAPP.prePlantPrefab[1047] = Resources.Load<GameObject>("Plants/_Mixer/ThreeSquash/ThreeSquashPreview");
		GameAPP.prePlantPrefab[1048] = Resources.Load<GameObject>("Plants/TorchWood/TorchWoodPreview");
		GameAPP.prePlantPrefab[1049] = Resources.Load<GameObject>("Plants/_Mixer/Jalatang/JalatangPreview");
		GameAPP.prePlantPrefab[1050] = Resources.Load<GameObject>("Plants/_Mixer/Squashtang/SquashtangPreview");
		GameAPP.prePlantPrefab[1051] = Resources.Load<GameObject>("Plants/_Mixer/Threetang/ThreetangPreview");
		GameAPP.prePlantPrefab[1052] = Resources.Load<GameObject>("Plants/TorchWood/EpicTorchWoodPreview");
		GameAPP.prePlantPrefab[1053] = Resources.Load<GameObject>("Plants/TorchWood/AdvancedTorchWoodPreview");
		GameAPP.prePlantPrefab[1054] = Resources.Load<GameObject>("Plants/Squash/JalaSquashPreview");
		GameAPP.prePlantPrefab[1055] = Resources.Load<GameObject>("Plants/_Mixer/ThreeTorch/ThreeTorchPreview");
		GameAPP.prePlantPrefab[1056] = Resources.Load<GameObject>("Plants/_Mixer/KelpTorch/KelpTorchPreview");
		GameAPP.prePlantPrefab[1057] = Resources.Load<GameObject>("Plants/Squash/FireSquashPreview");
		GameAPP.prePlantPrefab[1058] = Resources.Load<GameObject>("Plants/ThreePeater/DarkThreePeaterPreview");
		GameAPP.prePlantPrefab[1059] = Resources.Load<GameObject>("Plants/TorchWood/SquashTorchPreview");
		GameAPP.prePlantPrefab[1060] = Resources.Load<GameObject>("Plants/Caltrop/SpikeRock/SpikerockPreview");
		GameAPP.prePlantPrefab[1061] = Resources.Load<GameObject>("Plants/Caltrop/FireSpike/FireSpikePreview");
		GameAPP.prePlantPrefab[1062] = Resources.Load<GameObject>("Plants/Caltrop/JalaSpike/JalaSpikePreview");
		GameAPP.prePlantPrefab[1063] = Resources.Load<GameObject>("Plants/Caltrop/SquashSpike/SquashSpikePreview");
		GameAPP.prePlantPrefab[1064] = Resources.Load<GameObject>("Plants/Caltrop/ThreeSpike/ThreeSpikePreview");
		GameAPP.prePlantPrefab[1065] = Resources.Load<GameObject>("Plants/_Mixer/DoublePuff/GatlingPuffPreview");
		GameAPP.prePlantPrefab[1066] = Resources.Load<GameObject>("Plants/_Mixer/SuperKelp/SuperKelpPreview");
		GameAPP.prePlantPrefab[1067] = Resources.Load<GameObject>("Plants/CattailPlant/CattailPlantPreview");
		GameAPP.prePlantPrefab[1068] = Resources.Load<GameObject>("Plants/CattailPlant/IceCattailPreview");
		GameAPP.prePlantPrefab[1069] = Resources.Load<GameObject>("Plants/CattailPlant/FireCattailPreview");
		GameAPP.prePlantPrefab[1070] = Resources.Load<GameObject>("Plants/GloomShroom/GloomShroomPreview");
		GameAPP.prePlantPrefab[1071] = Resources.Load<GameObject>("Plants/GloomShroom/FireGloomPreview");
		GameAPP.prePlantPrefab[1072] = Resources.Load<GameObject>("Plants/GloomShroom/IceGloomPreview");
		GameAPP.prePlantPrefab[1073] = Resources.Load<GameObject>("Plants/TallNut/TallFireNutPreview");
		GameAPP.prePlantPrefab[1074] = Resources.Load<GameObject>("Plants/Caltrop/SpikeRock/IceSpikerockPreview");
		GameAPP.prePlantPrefab[1075] = Resources.Load<GameObject>("Plants/Caltrop/SpikeRock/FireSpikerockPreview");
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x00028914 File Offset: 0x00026B14
	private static void LoadZombie()
	{
		GameAPP.zombiePrefab[0] = Resources.Load<GameObject>("Zombies/Zombie/Zombie");
		GameAPP.zombiePrefab[1] = Resources.Load<GameObject>("Zombies/Zombie/Zombie");
		GameAPP.zombiePrefab[2] = Resources.Load<GameObject>("Zombies/Zombie/ConeZombie");
		GameAPP.zombiePrefab[3] = Resources.Load<GameObject>("Zombies/Zombie_polevaulter/PolevaulterZombie");
		GameAPP.zombiePrefab[4] = Resources.Load<GameObject>("Zombies/Zombie/BucketZombie");
		GameAPP.zombiePrefab[5] = Resources.Load<GameObject>("Zombies/PaperZombie/PaperZombie");
		GameAPP.zombiePrefab[6] = Resources.Load<GameObject>("Zombies/Zombie_polevaulter/Dance/DancePolevaulterPrefab");
		GameAPP.zombiePrefab[7] = Resources.Load<GameObject>("Zombies/Zombie_polevaulter/Dance/DancePolevaulter2Prefab");
		GameAPP.zombiePrefab[8] = Resources.Load<GameObject>("Zombies/Door/ZombieDoor");
		GameAPP.zombiePrefab[9] = Resources.Load<GameObject>("Zombies/Zombie_football/Zombie_footballPrefab");
		GameAPP.zombiePrefab[10] = Resources.Load<GameObject>("Zombies/Zombie_Jackson/Zombie_JacksonPrefab");
		GameAPP.zombiePrefab[11] = Resources.Load<GameObject>("Zombies/Zombie/InWater/ZombieDuck");
		GameAPP.zombiePrefab[12] = Resources.Load<GameObject>("Zombies/Zombie/InWater/ZombieDuckCone");
		GameAPP.zombiePrefab[13] = Resources.Load<GameObject>("Zombies/Zombie/InWater/ZombieDuckBucket");
		GameAPP.zombiePrefab[14] = Resources.Load<GameObject>("Zombies/SubmarineZombie/SubmarineZombiePrefab");
		GameAPP.zombiePrefab[15] = Resources.Load<GameObject>("Zombies/PaperZombie/PaperZombie95");
		GameAPP.zombiePrefab[16] = Resources.Load<GameObject>("Zombies/Zombie_Driver/ZombieDriverPrefab");
		GameAPP.zombiePrefab[17] = Resources.Load<GameObject>("Zombies/Zombie_snorkle/Zombie_snorklePrefab");
		GameAPP.zombiePrefab[18] = Resources.Load<GameObject>("Zombies/Zombie_Driver/SuperDriver/SuperDriverPrefab");
		GameAPP.zombiePrefab[19] = Resources.Load<GameObject>("Zombies/Zombie_dolphinrider/DolphinriderPrefab");
		GameAPP.zombiePrefab[20] = Resources.Load<GameObject>("Zombies/Zombie_drown/DrownZombie");
		GameAPP.zombiePrefab[21] = Resources.Load<GameObject>("Zombies/Zombie/DollDiamondZombie");
		GameAPP.zombiePrefab[22] = Resources.Load<GameObject>("Zombies/Zombie/DollGoldZombie");
		GameAPP.zombiePrefab[23] = Resources.Load<GameObject>("Zombies/Zombie/DollSilverZombie");
		GameAPP.zombiePrefab[100] = Resources.Load<GameObject>("Zombies/PlantZombie/PeaShooterZ");
		GameAPP.zombiePrefab[101] = Resources.Load<GameObject>("Zombies/PlantZombie/CherryShooterZ");
		GameAPP.zombiePrefab[102] = Resources.Load<GameObject>("Zombies/PlantZombie/SuperCherryZ");
		GameAPP.zombiePrefab[103] = Resources.Load<GameObject>("Zombies/PlantZombie/WallNutZ");
		GameAPP.zombiePrefab[104] = Resources.Load<GameObject>("Zombies/PlantZombie/Paper/CherryPaper");
		GameAPP.zombiePrefab[105] = Resources.Load<GameObject>("Zombies/Zombie/RandomZombie");
		GameAPP.zombiePrefab[106] = Resources.Load<GameObject>("Zombies/PlantZombie/BucketNutZ");
		GameAPP.zombiePrefab[107] = Resources.Load<GameObject>("Zombies/PlantZombie/CherryNutZ/CherryNutZ");
		GameAPP.zombiePrefab[108] = Resources.Load<GameObject>("Zombies/PlantZombie/IronPeaZ/IronPeaZPrefab");
		GameAPP.zombiePrefab[109] = Resources.Load<GameObject>("Zombies/Zombie_football/TallNutFootballZ/TallNutFootballZPrefab");
		GameAPP.zombiePrefab[110] = Resources.Load<GameObject>("Zombies/Zombie/RandomPlusZombie");
		GameAPP.zombiePrefab[111] = Resources.Load<GameObject>("Zombies/PlantZombie/TallIceNutZ/TallIceNutZ");
		GameAPP.zombiePrefab[200] = Resources.Load<GameObject>("Zombies/InTravel/SuperSubmarine/SuperSubmarinePrefab");
		GameAPP.zombiePrefab[201] = Resources.Load<GameObject>("Zombies/InTravel/JacksonDriver/JacksonDriverPrefab");
		GameAPP.zombiePrefab[202] = Resources.Load<GameObject>("Zombies/InTravel/FootballDrown/FootballDrownPrefab");
		GameAPP.zombiePrefab[203] = Resources.Load<GameObject>("Zombies/InTravel/CherryPaper95/CherryPaper95");
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x00028BF4 File Offset: 0x00026DF4
	private static void LoadPreZombie()
	{
		GameAPP.preZombiePrefab[0] = Resources.Load<GameObject>("Zombies/Zombie/ZombiePreview");
		GameAPP.preZombiePrefab[1] = Resources.Load<GameObject>("Zombies/Zombie/ZombiePreview");
		GameAPP.preZombiePrefab[2] = Resources.Load<GameObject>("Zombies/Zombie/ConeZombiePreview");
		GameAPP.preZombiePrefab[3] = Resources.Load<GameObject>("Zombies/Zombie_polevaulter/PolevaulterZombiePreview");
		GameAPP.preZombiePrefab[4] = Resources.Load<GameObject>("Zombies/Zombie/BucketZombiePreview");
		GameAPP.preZombiePrefab[5] = Resources.Load<GameObject>("Zombies/PaperZombie/PaperZombiePreview");
		GameAPP.preZombiePrefab[8] = Resources.Load<GameObject>("Zombies/Door/ZombieDoorPreview");
		GameAPP.preZombiePrefab[9] = Resources.Load<GameObject>("Zombies/Zombie_football/Zombie_footballPreview");
		GameAPP.preZombiePrefab[10] = Resources.Load<GameObject>("Zombies/Zombie_Jackson/Zombie_JacksonPreview");
		GameAPP.preZombiePrefab[14] = Resources.Load<GameObject>("Zombies/SubmarineZombie/SubmarineZombiePreview");
		GameAPP.preZombiePrefab[16] = Resources.Load<GameObject>("Zombies/Zombie_Driver/ZombieDriverPreview");
		GameAPP.preZombiePrefab[17] = Resources.Load<GameObject>("Zombies/Zombie_snorkle/Zombie_snorklePreview");
		GameAPP.preZombiePrefab[19] = Resources.Load<GameObject>("Zombies/Zombie_dolphinrider/DolphinriderPreview");
		GameAPP.preZombiePrefab[104] = Resources.Load<GameObject>("Zombies/PlantZombie/Paper/CherryPaperPreview");
		GameAPP.preZombiePrefab[105] = Resources.Load<GameObject>("Zombies/Zombie/RandomZombiePreview");
		GameAPP.preZombiePrefab[109] = Resources.Load<GameObject>("Zombies/Zombie_football/TallNutFootballZ/TallNutFootballZPreview");
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x00028D1C File Offset: 0x00026F1C
	private static void LoadBullet()
	{
		GameAPP.bulletPrefab[0] = Resources.Load<GameObject>("Bullet/Prefabs/ProjectilePea");
		GameAPP.bulletPrefab[1] = Resources.Load<GameObject>("Bullet/Prefabs/CherryBullet");
		GameAPP.bulletPrefab[2] = Resources.Load<GameObject>("Bullet/Prefabs/NutBullet");
		GameAPP.bulletPrefab[3] = Resources.Load<GameObject>("Bullet/Prefabs/SuperCherryBullet");
		GameAPP.bulletPrefab[4] = Resources.Load<GameObject>("Bullet/Prefabs/zombieblock1");
		GameAPP.bulletPrefab[5] = Resources.Load<GameObject>("Bullet/Prefabs/zombieblock2");
		GameAPP.bulletPrefab[6] = Resources.Load<GameObject>("Bullet/Prefabs/zombieblock3");
		GameAPP.bulletPrefab[7] = Resources.Load<GameObject>("Bullet/Prefabs/ProjectilePotato");
		GameAPP.bulletPrefab[8] = Resources.Load<GameObject>("Bullet/Prefabs/BulletSun");
		GameAPP.bulletPrefab[9] = Resources.Load<GameObject>("Bullet/Prefabs/Puff");
		GameAPP.bulletPrefab[10] = Resources.Load<GameObject>("Bullet/Prefabs/PuffPea");
		GameAPP.bulletPrefab[11] = Resources.Load<GameObject>("Bullet/Prefabs/ProjectileIronPea");
		GameAPP.bulletPrefab[12] = Resources.Load<GameObject>("Bullet/ThreeSpikeBullet/ThreeSpikeBullet");
		GameAPP.bulletPrefab[13] = Resources.Load<GameObject>("Bullet/Prefabs/PuffRandomColor");
		GameAPP.bulletPrefab[14] = Resources.Load<GameObject>("Bullet/Prefabs/PuffLove");
		GameAPP.bulletPrefab[15] = Resources.Load<GameObject>("Bullet/Prefabs/ProjectileSnowPea");
		GameAPP.bulletPrefab[16] = Resources.Load<GameObject>("Bullet/Prefabs/PuffSnowPea");
		GameAPP.bulletPrefab[17] = Resources.Load<GameObject>("Bullet/Prefabs/ProjectileIcicle");
		GameAPP.bulletPrefab[18] = Resources.Load<GameObject>("Bullet/Prefabs/ProjectileIcicleSmall");
		GameAPP.bulletPrefab[19] = Resources.Load<GameObject>("Bullet/Prefabs/FirePuffPea");
		GameAPP.bulletPrefab[20] = Resources.Load<GameObject>("Bullet/Prefabs/TrackBullet");
		GameAPP.bulletPrefab[21] = Resources.Load<GameObject>("Bullet/Prefabs/PuffSnow");
		GameAPP.bulletPrefab[22] = Resources.Load<GameObject>("Bullet/Prefabs/PuffBlack");
		GameAPP.bulletPrefab[23] = Resources.Load<GameObject>("Bullet/Prefabs/DoomBullet");
		GameAPP.bulletPrefab[24] = Resources.Load<GameObject>("Bullet/Prefabs/IceDoomBullet");
		GameAPP.bulletPrefab[25] = Resources.Load<GameObject>("Bullet/Prefabs/FirePea1");
		GameAPP.bulletPrefab[26] = Resources.Load<GameObject>("Bullet/Prefabs/FirePea2");
		GameAPP.bulletPrefab[27] = Resources.Load<GameObject>("Bullet/Prefabs/FirePea3");
		GameAPP.bulletPrefab[28] = Resources.Load<GameObject>("Bullet/Prefabs/SquashBullet");
		GameAPP.bulletPrefab[29] = Resources.Load<GameObject>("Bullet/Prefabs/TangkelpBullet");
		GameAPP.bulletPrefab[30] = Resources.Load<GameObject>("Bullet/Prefabs/FirekelpBullet");
		GameAPP.bulletPrefab[31] = Resources.Load<GameObject>("Bullet/FireCherryAn/FireCherry");
		GameAPP.bulletPrefab[32] = Resources.Load<GameObject>("Bullet/Prefabs/SquashKelpBullet");
		GameAPP.bulletPrefab[33] = Resources.Load<GameObject>("Bullet/Prefabs/ProjectileCactus");
		GameAPP.bulletPrefab[34] = Resources.Load<GameObject>("Bullet/Prefabs/ProjectileIceCactus");
		GameAPP.bulletPrefab[35] = Resources.Load<GameObject>("Bullet/Prefabs/ProjectileFireCactus");
		GameAPP.bulletPrefab[36] = Resources.Load<GameObject>("Bullet/Prefabs/CherrySquashBullet");
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x00028FBC File Offset: 0x000271BC
	private static void LoadParticle()
	{
		GameAPP.particlePrefab[0] = Resources.Load<GameObject>("Particle/Prefabs/PeaSplat");
		GameAPP.particlePrefab[1] = Resources.Load<GameObject>("Particle/Prefabs/Dirt");
		GameAPP.particlePrefab[2] = Resources.Load<GameObject>("Particle/Prefabs/BombCloud");
		GameAPP.particlePrefab[3] = Resources.Load<GameObject>("Particle/Prefabs/SunBombCloud");
		GameAPP.particlePrefab[4] = Resources.Load<GameObject>("Particle/Prefabs/CherrySplat");
		GameAPP.particlePrefab[5] = Resources.Load<GameObject>("Particle/Prefabs/NutPartical");
		GameAPP.particlePrefab[6] = Resources.Load<GameObject>("Particle/Prefabs/CherryNutPartical");
		GameAPP.particlePrefab[7] = Resources.Load<GameObject>("Particle/Prefabs/NutSplat");
		GameAPP.particlePrefab[8] = Resources.Load<GameObject>("Particle/Prefabs/PotaoParticle");
		GameAPP.particlePrefab[9] = Resources.Load<GameObject>("Particle/Prefabs/PotatoRise");
		GameAPP.particlePrefab[10] = Resources.Load<GameObject>("Particle/Prefabs/GreenCherrySplat");
		GameAPP.particlePrefab[11] = Resources.Load<GameObject>("Particle/Prefabs/RandomCloud");
		GameAPP.particlePrefab[12] = Resources.Load<GameObject>("Particle/Prefabs/ZombieBlockSplat");
		GameAPP.particlePrefab[13] = Resources.Load<GameObject>("Particle/Prefabs/PurpleNutDust");
		GameAPP.particlePrefab[14] = Resources.Load<GameObject>("Particle/Prefabs/BombCloudSmall");
		GameAPP.particlePrefab[15] = Resources.Load<GameObject>("Particle/Prefabs/PotatoSplat");
		GameAPP.particlePrefab[16] = Resources.Load<GameObject>("Particle/Prefabs/Health");
		GameAPP.particlePrefab[17] = Resources.Load<GameObject>("Particle/Prefabs/PuffSplat");
		GameAPP.particlePrefab[18] = Resources.Load<GameObject>("Particle/Prefabs/IronPeaSplat");
		GameAPP.particlePrefab[19] = Resources.Load<GameObject>("Particle/Prefabs/Fume");
		GameAPP.particlePrefab[20] = Resources.Load<GameObject>("Particle/Prefabs/MindControl");
		GameAPP.particlePrefab[21] = Resources.Load<GameObject>("Particle/Prefabs/FumeColorful");
		GameAPP.particlePrefab[22] = Resources.Load<GameObject>("Particle/Prefabs/FumeColorful2");
		GameAPP.particlePrefab[23] = Resources.Load<GameObject>("Particle/Prefabs/Star");
		GameAPP.particlePrefab[24] = Resources.Load<GameObject>("Particle/Prefabs/SnowPeaSplat");
		GameAPP.particlePrefab[25] = Resources.Load<GameObject>("Particle/Prefabs/Doom");
		GameAPP.particlePrefab[26] = Resources.Load<GameObject>("Particle/Prefabs/PuffBlackSplat");
		GameAPP.particlePrefab[27] = Resources.Load<GameObject>("Particle/Prefabs/DoomSplat");
		GameAPP.particlePrefab[28] = Resources.Load<GameObject>("Particle/Prefabs/IceDoomSplat");
		GameAPP.particlePrefab[29] = Resources.Load<GameObject>("Particle/Prefabs/IceDoom");
		GameAPP.particlePrefab[30] = Resources.Load<GameObject>("Particle/Prefabs/FumeIce");
		GameAPP.particlePrefab[31] = Resources.Load<GameObject>("Particle/Prefabs/FumeDoom");
		GameAPP.particlePrefab[32] = Resources.Load<GameObject>("Particle/Prefabs/WaterSplats");
		GameAPP.particlePrefab[33] = Resources.Load<GameObject>("Particle/Prefabs/Fire");
		GameAPP.particlePrefab[34] = Resources.Load<GameObject>("Particle/Prefabs/MachineExplode");
		GameAPP.particlePrefab[35] = Resources.Load<GameObject>("Particle/Prefabs/FireFree");
		GameAPP.particlePrefab[36] = Resources.Load<GameObject>("Particle/Prefabs/MachineExplodeRed");
		GameAPP.particlePrefab[37] = Resources.Load<GameObject>("Particle/Prefabs/Gloom");
		GameAPP.particlePrefab[38] = Resources.Load<GameObject>("Particle/Prefabs/GloomFire");
		GameAPP.particlePrefab[39] = Resources.Load<GameObject>("Particle/Prefabs/GloomIce");
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x00029290 File Offset: 0x00027490
	private static void LoadSprite()
	{
		GameAPP.spritePrefab[0] = Resources.Load<Sprite>("Zombies/Image/reanim/Zombie_outerarm_upper2");
		GameAPP.spritePrefab[1] = Resources.Load<Sprite>("Zombies/Image/reanim/Zombie_cone2");
		GameAPP.spritePrefab[2] = Resources.Load<Sprite>("Zombies/Image/reanim/Zombie_cone3");
		GameAPP.spritePrefab[3] = Resources.Load<Sprite>("Zombies/Image/reanim/Zombie_bucket2");
		GameAPP.spritePrefab[4] = Resources.Load<Sprite>("Zombies/Image/reanim/Zombie_bucket3");
		GameAPP.spritePrefab[5] = Resources.Load<Sprite>("Zombies/PaperZombie/paper_bone");
		GameAPP.spritePrefab[6] = Resources.Load<Sprite>("Zombies/PaperZombie/paper2");
		GameAPP.spritePrefab[7] = Resources.Load<Sprite>("Zombies/PaperZombie/paper3");
		GameAPP.spritePrefab[8] = Resources.Load<Sprite>("Plants/WallNut/crackedA");
		GameAPP.spritePrefab[9] = Resources.Load<Sprite>("Plants/WallNut/crackedB");
		GameAPP.spritePrefab[10] = Resources.Load<Sprite>("Zombies/PlantZombie/Paper/CherryPaper1");
		GameAPP.spritePrefab[11] = Resources.Load<Sprite>("Zombies/PlantZombie/Paper/CherryPaper2");
		GameAPP.spritePrefab[12] = Resources.Load<Sprite>("Zombies/Zombie/Zombie_cone2");
		GameAPP.spritePrefab[13] = Resources.Load<Sprite>("Zombies/Zombie/Zombie_cone3");
		GameAPP.spritePrefab[14] = Resources.Load<Sprite>("Zombies/PlantZombie/ironNut2");
		GameAPP.spritePrefab[15] = Resources.Load<Sprite>("Zombies/PlantZombie/ironNut3");
		GameAPP.spritePrefab[16] = Resources.Load<Sprite>("Plants/_Mixer/CherryNut/crack1");
		GameAPP.spritePrefab[17] = Resources.Load<Sprite>("Plants/_Mixer/CherryNut/crack2");
		GameAPP.spritePrefab[18] = Resources.Load<Sprite>("Zombies/Door/Zombie_screendoor2");
		GameAPP.spritePrefab[19] = Resources.Load<Sprite>("Zombies/Door/Zombie_screendoor3");
		GameAPP.spritePrefab[20] = Resources.Load<Sprite>("Zombies/Zombie_football/Zombie_football_helmet2");
		GameAPP.spritePrefab[21] = Resources.Load<Sprite>("Zombies/Zombie_football/Zombie_football_helmet3");
		GameAPP.spritePrefab[22] = Resources.Load<Sprite>("Zombies/Zombie_football/TallNutFootballZ/tnf2");
		GameAPP.spritePrefab[23] = Resources.Load<Sprite>("Zombies/Zombie_football/TallNutFootballZ/tnf3");
		GameAPP.spritePrefab[24] = Resources.Load<Sprite>("Zombies/Zombie/Gold2");
		GameAPP.spritePrefab[25] = Resources.Load<Sprite>("Zombies/Zombie/Gold3");
		GameAPP.spritePrefab[26] = Resources.Load<Sprite>("Zombies/Zombie_Jackson/Zombie_Jackson_outerarm_upper2");
		GameAPP.spritePrefab[27] = Resources.Load<Sprite>("Zombies/PlantZombie/TallIceNutZ/TallIceCracked1");
		GameAPP.spritePrefab[28] = Resources.Load<Sprite>("Zombies/PlantZombie/TallIceNutZ/TallIceCracked2");
		GameAPP.spritePrefab[29] = Resources.Load<Sprite>("Zombies/Zombie_Driver/Zombie_zamboni_1_damage1");
		GameAPP.spritePrefab[30] = Resources.Load<Sprite>("Zombies/Zombie_Driver/Zombie_zamboni_1_damage2");
		GameAPP.spritePrefab[31] = Resources.Load<Sprite>("Zombies/Zombie_Driver/Zombie_zamboni_2_damage2");
		GameAPP.spritePrefab[32] = Resources.Load<Sprite>("Zombies/Zombie_Driver/Zombie_zamboni_2_damage2");
		GameAPP.spritePrefab[33] = Resources.Load<Sprite>("Zombies/Zombie_Driver/SuperDriver/body_dmg1");
		GameAPP.spritePrefab[34] = Resources.Load<Sprite>("Zombies/Zombie_Driver/SuperDriver/lower_dmg1");
		GameAPP.spritePrefab[35] = Resources.Load<Sprite>("Zombies/Zombie_Driver/SuperDriver/body_dmg2");
		GameAPP.spritePrefab[36] = Resources.Load<Sprite>("Zombies/Zombie_Driver/SuperDriver/lower_dmg2");
		GameAPP.spritePrefab[37] = Resources.Load<Sprite>("Zombies/Zombie_Driver/SuperDriver/below_dmg");
		GameAPP.spritePrefab[38] = Resources.Load<Sprite>("Zombies/PaperZombie/paper_bone1");
		GameAPP.spritePrefab[39] = Resources.Load<Sprite>("Bullet/fireironpea");
		GameAPP.spritePrefab[40] = Resources.Load<Sprite>("Zombies/InTravel/FootballDrown/dmg1");
		GameAPP.spritePrefab[41] = Resources.Load<Sprite>("Zombies/InTravel/FootballDrown/dmg2");
		GameAPP.spritePrefab[42] = Resources.Load<Sprite>("Zombies/PaperZombie/book2");
		GameAPP.spritePrefab[43] = Resources.Load<Sprite>("Zombies/PaperZombie/book3");
		GameAPP.spritePrefab[44] = Resources.Load<Sprite>("Zombies/Zombie/DiamondDoll2");
		GameAPP.spritePrefab[45] = Resources.Load<Sprite>("Zombies/Zombie/DiamondDoll3");
		GameAPP.spritePrefab[46] = Resources.Load<Sprite>("Zombies/Zombie/GoldDoll2");
		GameAPP.spritePrefab[47] = Resources.Load<Sprite>("Zombies/Zombie/GoldDoll3");
		GameAPP.spritePrefab[48] = Resources.Load<Sprite>("Zombies/Zombie/SilverDoll2");
		GameAPP.spritePrefab[49] = Resources.Load<Sprite>("Zombies/Zombie/SilverDoll3");
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x00029618 File Offset: 0x00027818
	private static void LoadCoin()
	{
		GameAPP.coinPrefab[0] = Resources.Load<GameObject>("Items/Sun/Sun");
		GameAPP.coinPrefab[2] = Resources.Load<GameObject>("Items/Sun/SmallSun");
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x0002963C File Offset: 0x0002783C
	private static void LoadMusic()
	{
		GameAPP.musicPrefab[0] = Resources.Load<AudioClip>("Music/MainMenu");
		GameAPP.musicPrefab[1] = Resources.Load<AudioClip>("Music/SelectCard");
		GameAPP.musicPrefab[2] = Resources.Load<AudioClip>("Music/Day");
		GameAPP.musicPrefab[3] = Resources.Load<AudioClip>("Music/Day1");
		GameAPP.musicPrefab[4] = Resources.Load<AudioClip>("Music/Night");
		GameAPP.musicPrefab[5] = Resources.Load<AudioClip>("Music/Night1");
		GameAPP.musicPrefab[6] = Resources.Load<AudioClip>("Music/Pool");
		GameAPP.musicPrefab[7] = Resources.Load<AudioClip>("Music/Pool1");
		GameAPP.musicPrefab[12] = Resources.Load<AudioClip>("Music/loon");
		GameAPP.musicPrefab[13] = Resources.Load<AudioClip>("Music/battle");
		GameAPP.musicPrefab[14] = Resources.Load<AudioClip>("Music/winmusic");
		GameAPP.musicPrefab[15] = Resources.Load<AudioClip>("Music/IZE");
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x00029719 File Offset: 0x00027919
	private static void LoadGridItem()
	{
		GameAPP.gridItemPrefab[0] = Resources.Load<GameObject>("Image/crater/CraterDay");
		GameAPP.gridItemPrefab[1] = Resources.Load<GameObject>("Image/crater/CraterNight");
		GameAPP.gridItemPrefab[2] = Resources.Load<GameObject>("Image/crater/CraterNight");
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x00029750 File Offset: 0x00027950
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			if (this.isFullScreen)
			{
				Screen.SetResolution(1920, 1080, false);
			}
			else
			{
				Screen.SetResolution(1920, 1080, true);
			}
			this.isFullScreen = !this.isFullScreen;
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			Screen.SetResolution(1280, 720, false);
		}
		this.MusicUpdate();
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x000297C0 File Offset: 0x000279C0
	private void MusicUpdate()
	{
		this.musicChangeTime += Time.deltaTime;
		if (GameAPP.theGameStatus == 0)
		{
			int musicType = Board.Instance.musicType;
			if (musicType != 1)
			{
				if (musicType == 2)
				{
					this.NightMusicUpdate();
				}
			}
			else
			{
				this.DayMusicUpdate();
			}
		}
		if (GameAPP.theGameStatus < 0 || GameAPP.theGameStatus == 1)
		{
			GameAPP.music.volume = GameAPP.gameMusicVolume;
		}
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x00029828 File Offset: 0x00027A28
	private void DayMusicUpdate()
	{
		if (Board.Instance.theCurrentNumOfZombieUncontroled >= 10)
		{
			if (GameAPP.musicDrum.volume < GameAPP.gameMusicVolume)
			{
				GameAPP.musicDrum.volume += Time.deltaTime;
				GameAPP.music.volume -= Time.deltaTime;
			}
			else
			{
				GameAPP.musicDrum.volume = GameAPP.gameMusicVolume;
				GameAPP.music.volume = 0f;
			}
		}
		if (Board.Instance.theCurrentNumOfZombieUncontroled < 10)
		{
			if (GameAPP.musicDrum.volume > 0f)
			{
				GameAPP.musicDrum.volume -= Time.deltaTime;
				GameAPP.music.volume += Time.deltaTime;
			}
			else
			{
				GameAPP.music.volume = GameAPP.gameMusicVolume;
				GameAPP.musicDrum.volume = 0f;
			}
		}
		if (Input.GetKeyDown(KeyCode.K))
		{
			Debug.Log(GameAPP.music.time.ToString() + "  " + GameAPP.musicDrum.time.ToString());
		}
		if ((int)Time.time % 10 == 0 && GameAPP.music.time != GameAPP.musicDrum.time)
		{
			GameAPP.music.time = GameAPP.musicDrum.time;
		}
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x0002997C File Offset: 0x00027B7C
	private void NightMusicUpdate()
	{
		if (Board.Instance.theCurrentNumOfZombieUncontroled >= 10 && !this.onMusicDrumFade)
		{
			this.musicChangeTime = 0f;
			if (GameAPP.music.volume > 0f)
			{
				GameAPP.music.volume -= 0.2f * Time.deltaTime;
			}
			else
			{
				GameAPP.music.volume = 0f;
			}
			if (GameAPP.musicDrum.volume < GameAPP.gameMusicVolume)
			{
				GameAPP.musicDrum.volume += 0.2f * Time.deltaTime;
			}
			else
			{
				GameAPP.musicDrum.volume = GameAPP.gameMusicVolume;
			}
			if (!GameAPP.musicDrum.isPlaying)
			{
				GameAPP.musicDrum.clip = GameAPP.musicPrefab[5];
				GameAPP.musicDrum.time = 0f;
				GameAPP.musicDrum.Play();
			}
		}
		if (Board.Instance.theCurrentNumOfZombieUncontroled < 10 && this.musicChangeTime > 10f)
		{
			this.onMusicDrumFade = true;
			if (GameAPP.musicDrum.volume > 0f)
			{
				GameAPP.musicDrum.volume -= 0.2f * Time.deltaTime;
				if (GameAPP.musicDrum.volume < 0f)
				{
					GameAPP.musicDrum.volume = 0f;
					GameAPP.musicDrum.Stop();
				}
			}
			if (!GameAPP.musicDrum.isPlaying)
			{
				if (GameAPP.music.volume < GameAPP.gameMusicVolume)
				{
					GameAPP.music.volume += 0.2f * Time.deltaTime;
					return;
				}
				GameAPP.music.volume = GameAPP.gameMusicVolume;
				this.musicChangeTime = 0f;
				this.onMusicDrumFade = false;
			}
		}
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x00029B3C File Offset: 0x00027D3C
	public static void ClearItemInCanvas()
	{
		foreach (object obj in GameAPP.canvas.transform)
		{
			Object.Destroy(((Transform)obj).gameObject);
		}
		foreach (object obj2 in GameAPP.canvasUp.transform)
		{
			Object.Destroy(((Transform)obj2).gameObject);
		}
	}

	// Token: 0x04000236 RID: 566
	public static GameObject gameAPP;

	// Token: 0x04000237 RID: 567
	public static GameObject canvas;

	// Token: 0x04000238 RID: 568
	public static GameObject canvasUp;

	// Token: 0x04000239 RID: 569
	public static GameObject board;

	// Token: 0x0400023A RID: 570
	public static AudioClip[] audioPrefab = new AudioClip[1024];

	// Token: 0x0400023B RID: 571
	public static GameObject[] plantPrefab = new GameObject[2048];

	// Token: 0x0400023C RID: 572
	public static GameObject[] prePlantPrefab = new GameObject[2048];

	// Token: 0x0400023D RID: 573
	public static bool[] unlockMixPlant = new bool[2048];

	// Token: 0x0400023E RID: 574
	public static GameObject[] zombiePrefab = new GameObject[256];

	// Token: 0x0400023F RID: 575
	public static GameObject[] preZombiePrefab = new GameObject[128];

	// Token: 0x04000240 RID: 576
	public static GameObject[] bulletPrefab = new GameObject[64];

	// Token: 0x04000241 RID: 577
	public static GameObject[] particlePrefab = new GameObject[1024];

	// Token: 0x04000242 RID: 578
	public static GameObject[] coinPrefab = new GameObject[1024];

	// Token: 0x04000243 RID: 579
	public static GameObject[] gridItemPrefab = new GameObject[16];

	// Token: 0x04000244 RID: 580
	public static Sprite[] spritePrefab = new Sprite[1024];

	// Token: 0x04000245 RID: 581
	public static AudioClip[] musicPrefab = new AudioClip[32];

	// Token: 0x04000246 RID: 582
	public static float gameMusicVolume = 1f;

	// Token: 0x04000247 RID: 583
	public static float gameSoundVolume = 1f;

	// Token: 0x04000248 RID: 584
	public static int difficulty = 2;

	// Token: 0x04000249 RID: 585
	public static bool[] advLevelCompleted = new bool[128];

	// Token: 0x0400024A RID: 586
	public static bool[] clgLevelCompleted = new bool[128];

	// Token: 0x0400024B RID: 587
	public static bool[] gameLevelCompleted = new bool[128];

	// Token: 0x0400024C RID: 588
	public static bool[] survivalLevelCompleted = new bool[128];

	// Token: 0x0400024D RID: 589
	public static float gameSpeed = 1f;

	// Token: 0x0400024E RID: 590
	public static int theBoardLevel = 0;

	// Token: 0x0400024F RID: 591
	public static int theBoardType = 0;

	// Token: 0x04000250 RID: 592
	public static int theGameStatus = -1;

	// Token: 0x04000251 RID: 593
	public static bool autoCollect = true;

	// Token: 0x04000252 RID: 594
	public static bool developerMode = false;

	// Token: 0x04000253 RID: 595
	public static List<SoundCtrl> sound = new List<SoundCtrl>();

	// Token: 0x04000254 RID: 596
	public static List<GameAPP.EVEPlant> plantEVE = new List<GameAPP.EVEPlant>();

	// Token: 0x04000255 RID: 597
	public static AudioSource music;

	// Token: 0x04000256 RID: 598
	public static AudioSource musicDrum;

	// Token: 0x04000257 RID: 599
	private float musicChangeTime;

	// Token: 0x04000258 RID: 600
	private bool isFullScreen = true;

	// Token: 0x04000259 RID: 601
	private bool onMusicDrumFade;

	// Token: 0x0400025A RID: 602
	public static bool[] unlocked = new bool[4];

	// Token: 0x0400025B RID: 603
	public static bool hardZombie = false;

	// Token: 0x0200014D RID: 333
	public struct EVEPlant
	{
		// Token: 0x04000492 RID: 1170
		public int row;

		// Token: 0x04000493 RID: 1171
		public int column;

		// Token: 0x04000494 RID: 1172
		public int type;
	}

	// Token: 0x0200014E RID: 334
	public enum GameStatus
	{
		// Token: 0x04000496 RID: 1174
		OpenOptions = -2,
		// Token: 0x04000497 RID: 1175
		OutGame,
		// Token: 0x04000498 RID: 1176
		InGame,
		// Token: 0x04000499 RID: 1177
		Pause,
		// Token: 0x0400049A RID: 1178
		InInterlude,
		// Token: 0x0400049B RID: 1179
		Selecting
	}

	// Token: 0x0200014F RID: 335
	public enum MusicType
	{
		// Token: 0x0400049D RID: 1181
		MainMenu,
		// Token: 0x0400049E RID: 1182
		SelectCard,
		// Token: 0x0400049F RID: 1183
		Day,
		// Token: 0x040004A0 RID: 1184
		Day1,
		// Token: 0x040004A1 RID: 1185
		Night,
		// Token: 0x040004A2 RID: 1186
		Night1,
		// Token: 0x040004A3 RID: 1187
		Pool,
		// Token: 0x040004A4 RID: 1188
		Pool1,
		// Token: 0x040004A5 RID: 1189
		Loon = 12,
		// Token: 0x040004A6 RID: 1190
		UltimateBattle,
		// Token: 0x040004A7 RID: 1191
		WinMusic,
		// Token: 0x040004A8 RID: 1192
		IZE
	}
}

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameAPP : MonoBehaviour
{
	public struct EVEPlant
	{
		public int row;

		public int column;

		public int type;
	}

	public enum GameStatus
	{
		OpenOptions = -2,
		OutGame = -1,
		InGame = 0,
		Pause = 1,
		InInterlude = 2,
		Selecting = 3
	}

	public enum MusicType
	{
		MainMenu = 0,
		SelectCard = 1,
		Day = 2,
		Day1 = 3,
		Night = 4,
		Night1 = 5,
		Pool = 6,
		Pool1 = 7,
		Loon = 12,
		UltimateBattle = 13,
		WinMusic = 14,
		IZE = 15
	}

	public static GameObject gameAPP;

	public static GameObject canvas;

	public static GameObject canvasUp;

	public static GameObject board;

	public static AudioClip[] audioPrefab = new AudioClip[1024];

	public static GameObject[] plantPrefab = new GameObject[2048];

	public static GameObject[] prePlantPrefab = new GameObject[2048];

	public static bool[] unlockMixPlant = new bool[2048];

	public static GameObject[] zombiePrefab = new GameObject[256];

	public static GameObject[] preZombiePrefab = new GameObject[128];

	public static GameObject[] bulletPrefab = new GameObject[64];

	public static GameObject[] particlePrefab = new GameObject[1024];

	public static GameObject[] coinPrefab = new GameObject[1024];

	public static GameObject[] gridItemPrefab = new GameObject[16];

	public static Sprite[] spritePrefab = new Sprite[1024];

	public static AudioClip[] musicPrefab = new AudioClip[32];

	public static float gameMusicVolume = 1f;

	public static float gameSoundVolume = 1f;

	public static int difficulty = 2;

	public static bool[] advLevelCompleted = new bool[128];

	public static bool[] clgLevelCompleted = new bool[128];

	public static bool[] gameLevelCompleted = new bool[128];

	public static bool[] survivalLevelCompleted = new bool[128];

	public static float gameSpeed = 1f;

	public static int theBoardLevel = 0;

	public static int theBoardType = 0;

	public static int theGameStatus = -1;

	public static bool autoCollect = true;

	public static bool developerMode = false;

	public static List<SoundCtrl> sound = new List<SoundCtrl>();

	public static List<EVEPlant> plantEVE = new List<EVEPlant>();

	public static AudioSource music;

	public static AudioSource musicDrum;

	private float musicChangeTime;

	private bool isFullScreen = true;

	private bool onMusicDrumFade;

	public static bool[] unlocked = new bool[4];

	public static bool hardZombie = false;

	private void Awake()
	{
		this.AddComponent<SaveInfo>();
		music = GetComponent<AudioSource>();
		musicDrum = Camera.main.GetComponent<AudioSource>();
		LoadResources();
		gameAPP = base.gameObject;
		canvas = GameObject.Find("Canvas");
		canvasUp = GameObject.Find("CanvasUp");
		Application.targetFrameRate = 200;
		MixData.InitMixData();
		Time.timeScale = gameSpeed;
		CursorChange.SetDefaultCursor();
	}

	private void Start()
	{
		UIMgr.EnterMainMenu();
	}

	public static void PlaySoundNotPause(int theSoundID, float theVolume = 0.5f)
	{
		if (audioPrefab[theSoundID] == null)
		{
			LoadSound();
		}
		AudioClip clip = audioPrefab[theSoundID];
		AudioManager.Instance.PlaySound(clip, theVolume);
	}

	public static void PlaySound(int theSoundID, float theVolume = 0.5f)
	{
		int num = 0;
		float num2 = float.MaxValue;
		foreach (SoundCtrl item in sound)
		{
			if (item.theSoundID == theSoundID)
			{
				num++;
				if (item.existTime < num2)
				{
					num2 = item.existTime;
				}
			}
		}
		if (num2 < 0.1f || num > 4)
		{
			return;
		}
		switch (theSoundID)
		{
		case 69:
			if (num > 1)
			{
				return;
			}
			break;
		case 49:
			theVolume = 0.4f;
			break;
		}
		AudioClip clip = audioPrefab[theSoundID];
		GameObject obj = new GameObject
		{
			name = "SoundPlayer"
		};
		AudioSource audioSource = obj.AddComponent<AudioSource>();
		audioSource.clip = clip;
		SoundCtrl soundCtrl = obj.AddComponent<SoundCtrl>();
		soundCtrl.theSoundID = theSoundID;
		sound.Add(soundCtrl);
		audioSource.volume = theVolume * gameSoundVolume;
		switch (theSoundID)
		{
		case 0:
		case 1:
		case 2:
			audioSource.time = 0.05f;
			audioSource.pitch = Random.Range(1f, 1.7f);
			break;
		case 3:
		case 4:
		case 16:
		case 17:
			audioSource.pitch = Random.Range(1f, 1.8f);
			break;
		case 12:
		case 57:
		case 80:
			audioSource.pitch = Random.Range(1f, 1.4f);
			break;
		case 7:
		case 10:
		case 13:
		case 14:
		case 15:
			audioSource.pitch = Random.Range(1f, 1.5f);
			break;
		}
		audioSource.Play();
	}

	public static void ChangeMusic(int id)
	{
		music.Stop();
		musicDrum.Stop();
		AudioClip clip = musicPrefab[id];
		music.clip = clip;
		switch (id)
		{
		case 0:
			music.time = 1f;
			music.Play();
			return;
		case 2:
		{
			AudioClip clip3 = musicPrefab[3];
			musicDrum.clip = clip3;
			musicDrum.volume = 0f;
			musicDrum.time = 0f;
			musicDrum.Play();
			Board.Instance.musicType = 1;
			break;
		}
		case 4:
			Board.Instance.musicType = 2;
			break;
		case 6:
		{
			AudioClip clip2 = musicPrefab[7];
			musicDrum.clip = clip2;
			musicDrum.volume = 0f;
			musicDrum.time = 0f;
			musicDrum.Play();
			Board.Instance.musicType = 1;
			break;
		}
		}
		music.volume = gameMusicVolume;
		music.time = 0f;
		music.Play();
	}

	private static void LoadResources()
	{
		LoadSound();
		LoadPlant();
		LoadPrePlant();
		LoadZombie();
		LoadPreZombie();
		LoadBullet();
		LoadParticle();
		LoadCoin();
		LoadSprite();
		LoadMusic();
		LoadGridItem();
	}

	private static void LoadSound()
	{
		audioPrefab[0] = Resources.Load<AudioClip>("Sound/BulletHit/splat");
		audioPrefab[1] = Resources.Load<AudioClip>("Sound/BulletHit/splat2");
		audioPrefab[2] = Resources.Load<AudioClip>("Sound/BulletHit/splat3");
		audioPrefab[3] = Resources.Load<AudioClip>("Sound/PlantShoot/throw");
		audioPrefab[4] = Resources.Load<AudioClip>("Sound/PlantShoot/throw2");
		audioPrefab[5] = Resources.Load<AudioClip>("Sound/ZombieDie/zombie_falling_1");
		audioPrefab[6] = Resources.Load<AudioClip>("Sound/ZombieDie/zombie_falling_2");
		audioPrefab[7] = Resources.Load<AudioClip>("Sound/ZombieDie/limbs_pop");
		audioPrefab[8] = Resources.Load<AudioClip>("Sound/ZombieEat/chomp");
		audioPrefab[9] = Resources.Load<AudioClip>("Sound/ZombieEat/chomp2");
		audioPrefab[10] = Resources.Load<AudioClip>("Sound/ZombieEat/chompsoft");
		audioPrefab[11] = Resources.Load<AudioClip>("Sound/ZombieEat/gulp");
		audioPrefab[12] = Resources.Load<AudioClip>("Sound/BulletHit/plastichit");
		audioPrefab[13] = Resources.Load<AudioClip>("Sound/BulletHit/plastichit2");
		audioPrefab[14] = Resources.Load<AudioClip>("Sound/BulletHit/shieldhit");
		audioPrefab[15] = Resources.Load<AudioClip>("Sound/BulletHit/shieldhit2");
		audioPrefab[16] = Resources.Load<AudioClip>("Sound/CollectItem/points");
		audioPrefab[17] = Resources.Load<AudioClip>("Sound/CollectItem/coin");
		audioPrefab[18] = Resources.Load<AudioClip>("Sound/CollectItem/coin");
		audioPrefab[19] = Resources.Load<AudioClip>("Sound/BottonAndPutDown/tap");
		audioPrefab[20] = Resources.Load<AudioClip>("Sound/BottonAndPutDown/tap2");
		audioPrefab[21] = Resources.Load<AudioClip>("Sound/BottonAndPutDown/shovel");
		audioPrefab[22] = Resources.Load<AudioClip>("Sound/PlacePlant/plant");
		audioPrefab[23] = Resources.Load<AudioClip>("Sound/PlacePlant/plant2");
		audioPrefab[24] = Resources.Load<AudioClip>("Sound/PlacePlant/plant_water");
		audioPrefab[25] = Resources.Load<AudioClip>("Sound/PlacePlant/seedlift");
		audioPrefab[26] = Resources.Load<AudioClip>("Sound/ClickFail/buzzer");
		audioPrefab[27] = Resources.Load<AudioClip>("Sound/BottonAndPutDown/bleep");
		audioPrefab[28] = Resources.Load<AudioClip>("Sound/BottonAndPutDown/gravebutton");
		audioPrefab[29] = Resources.Load<AudioClip>("Sound/BottonAndPutDown/buttonclick");
		audioPrefab[30] = Resources.Load<AudioClip>("Sound/BottonAndPutDown/pause");
		audioPrefab[31] = Resources.Load<AudioClip>("Sound/TextSound/readysetplant");
		audioPrefab[32] = Resources.Load<AudioClip>("Sound/TextSound/hugewave");
		audioPrefab[33] = Resources.Load<AudioClip>("Sound/TextSound/finalwave");
		audioPrefab[34] = Resources.Load<AudioClip>("Sound/TextSound/awooga");
		audioPrefab[35] = Resources.Load<AudioClip>("Sound/TextSound/siren");
		audioPrefab[37] = Resources.Load<AudioClip>("Sound/Award/lightfill");
		audioPrefab[38] = Resources.Load<AudioClip>("Sound/BottonAndPutDown/roll_in");
		audioPrefab[39] = Resources.Load<AudioClip>("Sound/Bomb/reverse_explosion");
		audioPrefab[40] = Resources.Load<AudioClip>("Sound/Bomb/cherrybomb");
		audioPrefab[41] = Resources.Load<AudioClip>("Sound/Bomb/doomshroom");
		audioPrefab[42] = Resources.Load<AudioClip>("Sound/Bomb/jalapeno");
		audioPrefab[43] = Resources.Load<AudioClip>("Sound/Bomb/explosion");
		audioPrefab[44] = Resources.Load<AudioClip>("Sound/Zombie/newspaper_rip");
		audioPrefab[45] = Resources.Load<AudioClip>("Sound/Zombie/newspaper_rarrgh");
		audioPrefab[46] = Resources.Load<AudioClip>("Sound/Zombie/newspaper_rarrgh2");
		audioPrefab[47] = Resources.Load<AudioClip>("Sound/Bomb/potato_mine");
		audioPrefab[48] = Resources.Load<AudioClip>("Sound/plant/dirt_rise");
		audioPrefab[49] = Resources.Load<AudioClip>("Sound/plant/bigchomp");
		audioPrefab[50] = Resources.Load<AudioClip>("Sound/Zombie/grassstep");
		audioPrefab[51] = Resources.Load<AudioClip>("Sound/Zombie/polevault");
		audioPrefab[52] = Resources.Load<AudioClip>("Sound/lose/losemusic");
		audioPrefab[53] = Resources.Load<AudioClip>("Sound/plant/bowling");
		audioPrefab[54] = Resources.Load<AudioClip>("Sound/plant/bowlingimpact");
		audioPrefab[55] = Resources.Load<AudioClip>("Sound/plant/bowlingimpact2");
		audioPrefab[56] = Resources.Load<AudioClip>("Sound/plant/plantgrow");
		audioPrefab[57] = Resources.Load<AudioClip>("Sound/plant/puff");
		audioPrefab[58] = Resources.Load<AudioClip>("Sound/plant/fume");
		audioPrefab[59] = Resources.Load<AudioClip>("Sound/fire/ignite");
		audioPrefab[60] = Resources.Load<AudioClip>("Sound/fire/ignite2");
		audioPrefab[61] = Resources.Load<AudioClip>("Sound/fire/firepea");
		audioPrefab[62] = Resources.Load<AudioClip>("Sound/plant/floop");
		audioPrefab[63] = Resources.Load<AudioClip>("Sound/plant/mindcontrolled");
		audioPrefab[64] = Resources.Load<AudioClip>("Sound/Zombie/bonk");
		audioPrefab[65] = Resources.Load<AudioClip>("Sound/Item/fertilizer");
		audioPrefab[66] = Resources.Load<AudioClip>("Sound/Item/prize");
		audioPrefab[67] = Resources.Load<AudioClip>("Sound/Zombie/frozen");
		audioPrefab[68] = Resources.Load<AudioClip>("Sound/plant/snow_pea_sparkles");
		audioPrefab[69] = Resources.Load<AudioClip>("Sound/Zombie/dancer");
		audioPrefab[70] = Resources.Load<AudioClip>("Sound/Bomb/SmallDoom");
		audioPrefab[71] = Resources.Load<AudioClip>("Sound/PlacePlant/plant_water");
		audioPrefab[72] = Resources.Load<AudioClip>("Sound/plant/squash_hmm");
		audioPrefab[73] = Resources.Load<AudioClip>("Sound/plant/squash_hmm2");
		audioPrefab[74] = Resources.Load<AudioClip>("Sound/plant/gargantuar_thump");
		audioPrefab[75] = Resources.Load<AudioClip>("Sound/Zombie/zombiesplash");
		audioPrefab[76] = Resources.Load<AudioClip>("Sound/Zombie/zamboni");
		audioPrefab[77] = Resources.Load<AudioClip>("Sound/Zombie/balloon_pop");
		audioPrefab[78] = Resources.Load<AudioClip>("Sound/Zombie/dolphin_appears");
		audioPrefab[79] = Resources.Load<AudioClip>("Sound/Zombie/dolphin_before_jumping");
		audioPrefab[80] = Resources.Load<AudioClip>("Sound/Girl/Cattail_hit");
		audioPrefab[81] = Resources.Load<AudioClip>("Sound/Girl/Cattail_Plant1");
		audioPrefab[82] = Resources.Load<AudioClip>("Sound/Girl/Cattail_Plant2");
		audioPrefab[83] = Resources.Load<AudioClip>("Sound/plant/magnetshroom");
	}

	private static void LoadPlant()
	{
		plantPrefab[0] = Resources.Load<GameObject>("Plants/PeaShooter/PeashooterPrefab");
		plantPrefab[1] = Resources.Load<GameObject>("Plants/SunFlower/SunflowerPrefab");
		plantPrefab[2] = Resources.Load<GameObject>("Plants/CherryBomb/CherryBombPrefab");
		plantPrefab[3] = Resources.Load<GameObject>("Plants/WallNut/WallNutPrefab");
		plantPrefab[4] = Resources.Load<GameObject>("Plants/PotatoMine/PotatoMinePrefab");
		plantPrefab[5] = Resources.Load<GameObject>("Plants/Chomper/ChomperPrefab");
		plantPrefab[6] = Resources.Load<GameObject>("Plants/SmallPuff/SmallPuffPrefab");
		plantPrefab[7] = Resources.Load<GameObject>("Plants/FumeShroom/FumeShroomPrefab");
		plantPrefab[8] = Resources.Load<GameObject>("Plants/HypnoShroom/HypnoShroomPrefab");
		plantPrefab[9] = Resources.Load<GameObject>("Plants/ScaredyShroom/ScaredyShroomPrefab");
		plantPrefab[10] = Resources.Load<GameObject>("Plants/IceShroom/IceShroomPrefab");
		plantPrefab[11] = Resources.Load<GameObject>("Plants/DoomShroom/DoomShroomPrefab");
		plantPrefab[12] = Resources.Load<GameObject>("Plants/LilyPad/LilyPadPrefab");
		plantPrefab[13] = Resources.Load<GameObject>("Plants/Squash/SquashPrefab");
		plantPrefab[14] = Resources.Load<GameObject>("Plants/ThreePeater/ThreePeaterPrefab");
		plantPrefab[15] = Resources.Load<GameObject>("Plants/Tanglekelp/TanglekelpPrefab");
		plantPrefab[16] = Resources.Load<GameObject>("Plants/Jalapeno/JalapenoPrefab");
		plantPrefab[17] = Resources.Load<GameObject>("Plants/Caltrop/CaltropPrefab");
		plantPrefab[18] = Resources.Load<GameObject>("Plants/TorchWood/TorchWoodPrefab");
		plantPrefab[252] = Resources.Load<GameObject>("Plants/UniquePlants/PlantGirls/Cattail/CattailPrefab");
		plantPrefab[253] = Resources.Load<GameObject>("Plants/UniquePlants/Wheat/WheatPrefab");
		plantPrefab[254] = Resources.Load<GameObject>("Plants/UniquePlants/Endoflame/EndoFlamePrefab");
		plantPrefab[255] = Resources.Load<GameObject>("Plants/WallNut/BigWallNutPrefab");
		plantPrefab[256] = Resources.Load<GameObject>("Plants/Present/PresentPrefab");
		plantPrefab[900] = Resources.Load<GameObject>("Plants/Travel/HyponoT/HypnoEmperorPrefab");
		plantPrefab[901] = Resources.Load<GameObject>("Plants/_Mixer/SuperCherryShooter/SuperCherryGatlingPrefab");
		plantPrefab[902] = Resources.Load<GameObject>("Plants/TorchWood/FireSquashTorchPrefab");
		plantPrefab[903] = Resources.Load<GameObject>("Plants/_Mixer/SuperChomper/SuperCherryChomperPrefab");
		plantPrefab[904] = Resources.Load<GameObject>("Plants/_Mixer/IceDoomFume/FinalFumePrefab");
		plantPrefab[1000] = Resources.Load<GameObject>("Plants/_Mixer/PeaSunFlower/PeaSunFlowerPrefab");
		plantPrefab[1001] = Resources.Load<GameObject>("Plants/_Mixer/CherryShooter/CherryshooterPrefab");
		plantPrefab[1002] = Resources.Load<GameObject>("Plants/_Mixer/SunBomb/SunBombPrefab");
		plantPrefab[1003] = Resources.Load<GameObject>("Plants/_Mixer/CherryNut/CherryNutPrefab");
		plantPrefab[1004] = Resources.Load<GameObject>("Plants/_Mixer/NutShooter/NutShooterPrefab");
		plantPrefab[1005] = Resources.Load<GameObject>("Plants/_Mixer/SuperCherryShooter/SuperCherryShooterPrefab");
		plantPrefab[1006] = Resources.Load<GameObject>("Plants/_Mixer/SunNut/SunNutPrefab");
		plantPrefab[1007] = Resources.Load<GameObject>("Plants/_Mixer/PeaMine/PeaMinePrefab");
		plantPrefab[1008] = Resources.Load<GameObject>("Plants/_Mixer/DoubleCherry/DoubleCherryPrefab");
		plantPrefab[1009] = Resources.Load<GameObject>("Plants/_Mixer/SunMine/SunMinePrefab");
		plantPrefab[1010] = Resources.Load<GameObject>("Plants/_Mixer/PotatoNut/PotatoNutPrefab");
		plantPrefab[1011] = Resources.Load<GameObject>("Plants/_Mixer/PeaChomper/PeaChomperPrefab");
		plantPrefab[1012] = Resources.Load<GameObject>("Plants/_Mixer/NutChomper/NutChomperPrefab");
		plantPrefab[1013] = Resources.Load<GameObject>("Plants/_Mixer/SuperChomper/SuperChomperPrefab");
		plantPrefab[1014] = Resources.Load<GameObject>("Plants/_Mixer/SunChomper/SunChomperPrefab");
		plantPrefab[1015] = Resources.Load<GameObject>("Plants/_Mixer/PotatoChomper/PotatoChomperPrefab");
		plantPrefab[1016] = Resources.Load<GameObject>("Plants/_Mixer/CherryChomper/CherryChomperPrefab");
		plantPrefab[1017] = Resources.Load<GameObject>("Plants/_Mixer/CherryGatlingPea/CherryGatlingPrefab");
		plantPrefab[1018] = Resources.Load<GameObject>("Plants/_Mixer/PeaSmallPuff/PeaSmallPuffPrefab");
		plantPrefab[1019] = Resources.Load<GameObject>("Plants/_Mixer/DoublePuff/DoublePuffPrefab");
		plantPrefab[1020] = Resources.Load<GameObject>("Plants/_Mixer/IronPea/IronPeaPrefab");
		plantPrefab[1021] = Resources.Load<GameObject>("Plants/_Mixer/PuffNut/PuffNutPrefab");
		plantPrefab[1022] = Resources.Load<GameObject>("Plants/_Mixer/HypnoPuff/HypnoPuffPrefab");
		plantPrefab[1023] = Resources.Load<GameObject>("Plants/_Mixer/HypnoFume/HypnoFumePrefab");
		plantPrefab[1024] = Resources.Load<GameObject>("Plants/_Mixer/ScaredyHypno/ScaredyHypnoPrefab");
		plantPrefab[1025] = Resources.Load<GameObject>("Plants/_Mixer/ScaredFume/ScaredFumePrefab");
		plantPrefab[1026] = Resources.Load<GameObject>("Plants/_Mixer/SuperHypno/SuperHypnoPrefab");
		plantPrefab[1027] = Resources.Load<GameObject>("Plants/TallNut/TallNutPrefab");
		plantPrefab[1028] = Resources.Load<GameObject>("Plants/TallNut/TallNutFootballPrefab");
		plantPrefab[1029] = Resources.Load<GameObject>("Plants/WallNut/IronNutPrefab");
		plantPrefab[1030] = Resources.Load<GameObject>("Plants/DoublePea/DoubleShooterPrefab");
		plantPrefab[1031] = Resources.Load<GameObject>("Plants/SunShroom/SunShroomPrefab");
		plantPrefab[1032] = Resources.Load<GameObject>("Plants/GatlingPea/GatlingPeaPrefab");
		plantPrefab[1033] = Resources.Load<GameObject>("Plants/TwinFlower/TwinFlowerPrefab");
		plantPrefab[1034] = Resources.Load<GameObject>("Plants/ShowPeaShooter/SnowPeaShooterPrefab");
		plantPrefab[1035] = Resources.Load<GameObject>("Plants/_Mixer/IcePuff/IcePuffPrefab");
		plantPrefab[1036] = Resources.Load<GameObject>("Plants/_Mixer/SmallIceShroom/SmallIceShroomPrefab");
		plantPrefab[1037] = Resources.Load<GameObject>("Plants/_Mixer/IceFumeShroom/IceFumeShroomPrefab");
		plantPrefab[1038] = Resources.Load<GameObject>("Plants/_Mixer/IceScaredyShroom/IceScaredyShroomPrefab");
		plantPrefab[1039] = Resources.Load<GameObject>("Plants/TallNut/TallIceNutPrefab");
		plantPrefab[1040] = Resources.Load<GameObject>("Plants/_Mixer/IceDoom/IceDoomPrefab");
		plantPrefab[1041] = Resources.Load<GameObject>("Plants/_Mixer/IceHypno/IceHypnoPrefab");
		plantPrefab[1042] = Resources.Load<GameObject>("Plants/_Mixer/ScaredyDoom/ScaredyDoomPrefab");
		plantPrefab[1043] = Resources.Load<GameObject>("Plants/_Mixer/DoomFume/DoomFumePrefab");
		plantPrefab[1044] = Resources.Load<GameObject>("Plants/_Mixer/DoomPuff/PuffDoomPrefab");
		plantPrefab[1045] = Resources.Load<GameObject>("Plants/_Mixer/HypnoDoom/HypnoDoomPrefab");
		plantPrefab[1046] = Resources.Load<GameObject>("Plants/_Mixer/IceDoomFume/IceDoomFumePrefab");
		plantPrefab[1047] = Resources.Load<GameObject>("Plants/_Mixer/ThreeSquash/ThreeSquashPrefab");
		plantPrefab[1048] = Resources.Load<GameObject>("Plants/TorchWood/EliteTorchWoodPrefab");
		plantPrefab[1049] = Resources.Load<GameObject>("Plants/_Mixer/Jalatang/JalatangPrefab");
		plantPrefab[1050] = Resources.Load<GameObject>("Plants/_Mixer/Squashtang/SquashtangPrefab");
		plantPrefab[1051] = Resources.Load<GameObject>("Plants/_Mixer/Threetang/ThreetangPrefab");
		plantPrefab[1052] = Resources.Load<GameObject>("Plants/TorchWood/EpicTorchWoodPrefab");
		plantPrefab[1053] = Resources.Load<GameObject>("Plants/TorchWood/AdvancedTorchWoodPrefab");
		plantPrefab[1054] = Resources.Load<GameObject>("Plants/Squash/JalaSquashPrefab");
		plantPrefab[1055] = Resources.Load<GameObject>("Plants/_Mixer/ThreeTorch/ThreeTorchPrefab");
		plantPrefab[1056] = Resources.Load<GameObject>("Plants/_Mixer/KelpTorch/KelpTorchPrefab");
		plantPrefab[1057] = Resources.Load<GameObject>("Plants/Squash/FireSquashPrefab");
		plantPrefab[1058] = Resources.Load<GameObject>("Plants/ThreePeater/DarkThreePeaterPrefab");
		plantPrefab[1059] = Resources.Load<GameObject>("Plants/TorchWood/SquashTorchPrefab");
		plantPrefab[1060] = Resources.Load<GameObject>("Plants/Caltrop/SpikeRock/SpikerockPrefab");
		plantPrefab[1061] = Resources.Load<GameObject>("Plants/Caltrop/FireSpike/FireSpikePrefab");
		plantPrefab[1062] = Resources.Load<GameObject>("Plants/Caltrop/JalaSpike/JalaSpikePrefab");
		plantPrefab[1063] = Resources.Load<GameObject>("Plants/Caltrop/SquashSpike/SquashSpikePrefab");
		plantPrefab[1064] = Resources.Load<GameObject>("Plants/Caltrop/ThreeSpike/ThreeSpikePrefab");
		plantPrefab[1065] = Resources.Load<GameObject>("Plants/_Mixer/DoublePuff/GatlingPuffPrefab");
		plantPrefab[1066] = Resources.Load<GameObject>("Plants/_Mixer/SuperKelp/SuperKelpPrefab");
		plantPrefab[1067] = Resources.Load<GameObject>("Plants/CattailPlant/CattailPlantPrefab");
		plantPrefab[1068] = Resources.Load<GameObject>("Plants/CattailPlant/IceCattailPrefab");
		plantPrefab[1069] = Resources.Load<GameObject>("Plants/CattailPlant/FireCattailPrefab");
		plantPrefab[1070] = Resources.Load<GameObject>("Plants/GloomShroom/GloomShroomPrefab");
		plantPrefab[1071] = Resources.Load<GameObject>("Plants/GloomShroom/FireGloomPrefab");
		plantPrefab[1072] = Resources.Load<GameObject>("Plants/GloomShroom/IceGloomPrefab");
		plantPrefab[1073] = Resources.Load<GameObject>("Plants/TallNut/TallFireNutPrefab");
		plantPrefab[1074] = Resources.Load<GameObject>("Plants/Caltrop/SpikeRock/IceSpikerockPrefab");
		plantPrefab[1075] = Resources.Load<GameObject>("Plants/Caltrop/SpikeRock/FireSpikerockPrefab");
	}

	private static void LoadPrePlant()
	{
		prePlantPrefab[0] = Resources.Load<GameObject>("Plants/PeaShooter/PeaShooterPreview");
		prePlantPrefab[1] = Resources.Load<GameObject>("Plants/SunFlower/SunflowerPreview");
		prePlantPrefab[2] = Resources.Load<GameObject>("Plants/CherryBomb/CherryBombPreview");
		prePlantPrefab[3] = Resources.Load<GameObject>("Plants/WallNut/WallNutPreview");
		prePlantPrefab[4] = Resources.Load<GameObject>("Plants/PotatoMine/PotatoMinePreview");
		prePlantPrefab[5] = Resources.Load<GameObject>("Plants/Chomper/ChomperPreview");
		prePlantPrefab[6] = Resources.Load<GameObject>("Plants/SmallPuff/SmallPuffPreview");
		prePlantPrefab[7] = Resources.Load<GameObject>("Plants/FumeShroom/FumeShroomPreview");
		prePlantPrefab[8] = Resources.Load<GameObject>("Plants/HypnoShroom/HypnoShroomPreview");
		prePlantPrefab[9] = Resources.Load<GameObject>("Plants/ScaredyShroom/ScaredyShroomPreview");
		prePlantPrefab[10] = Resources.Load<GameObject>("Plants/IceShroom/IceShroomPreview");
		prePlantPrefab[11] = Resources.Load<GameObject>("Plants/DoomShroom/DoomShroomPreview");
		prePlantPrefab[12] = Resources.Load<GameObject>("Plants/LilyPad/LilyPadPreview");
		prePlantPrefab[13] = Resources.Load<GameObject>("Plants/Squash/SquashPreview");
		prePlantPrefab[14] = Resources.Load<GameObject>("Plants/ThreePeater/ThreePeaterPreview");
		prePlantPrefab[15] = Resources.Load<GameObject>("Plants/Tanglekelp/TanglekelpPreview");
		prePlantPrefab[16] = Resources.Load<GameObject>("Plants/Jalapeno/JalapenoPreview");
		prePlantPrefab[17] = Resources.Load<GameObject>("Plants/Caltrop/CaltropPreview");
		prePlantPrefab[18] = Resources.Load<GameObject>("Plants/TorchWood/TorchWoodPreview");
		prePlantPrefab[252] = Resources.Load<GameObject>("Plants/UniquePlants/PlantGirls/Cattail/CattailPreview");
		prePlantPrefab[253] = Resources.Load<GameObject>("Plants/UniquePlants/Wheat/WheatPreview");
		prePlantPrefab[254] = Resources.Load<GameObject>("Plants/UniquePlants/Endoflame/EndoFlamePreview");
		prePlantPrefab[256] = Resources.Load<GameObject>("Plants/Present/PresentPreview");
		prePlantPrefab[900] = Resources.Load<GameObject>("Plants/Travel/HyponoT/HypnoEmperorPreview");
		prePlantPrefab[901] = Resources.Load<GameObject>("Plants/_Mixer/SuperCherryShooter/SuperCherryGatlingPreview");
		prePlantPrefab[902] = Resources.Load<GameObject>("Plants/TorchWood/FireSquashTorchPreview");
		prePlantPrefab[903] = Resources.Load<GameObject>("Plants/_Mixer/SuperChomper/SuperCherryChomperPreview");
		prePlantPrefab[904] = Resources.Load<GameObject>("Plants/_Mixer/IceDoomFume/FinalFumePreview");
		prePlantPrefab[1000] = Resources.Load<GameObject>("Plants/_Mixer/PeaSunFlower/PeaSunFlowerPreview");
		prePlantPrefab[1001] = Resources.Load<GameObject>("Plants/_Mixer/CherryShooter/CherryshooterPreview");
		prePlantPrefab[1002] = Resources.Load<GameObject>("Plants/_Mixer/SunBomb/SunBombPreview");
		prePlantPrefab[1003] = Resources.Load<GameObject>("Plants/_Mixer/CherryNut/CherryNutPreview");
		prePlantPrefab[1004] = Resources.Load<GameObject>("Plants/_Mixer/NutShooter/NutShooterPreview");
		prePlantPrefab[1005] = Resources.Load<GameObject>("Plants/_Mixer/SuperCherryShooter/SuperCherryShooterPreview");
		prePlantPrefab[1006] = Resources.Load<GameObject>("Plants/_Mixer/SunNut/SunNutPreview");
		prePlantPrefab[1007] = Resources.Load<GameObject>("Plants/_Mixer/PeaMine/PeaMinePreview");
		prePlantPrefab[1008] = Resources.Load<GameObject>("Plants/_Mixer/DoubleCherry/DoubleCherryPreview");
		prePlantPrefab[1009] = Resources.Load<GameObject>("Plants/_Mixer/SunMine/SunMinePreview");
		prePlantPrefab[1010] = Resources.Load<GameObject>("Plants/_Mixer/PotatoNut/PotatoNutPreview");
		prePlantPrefab[1011] = Resources.Load<GameObject>("Plants/_Mixer/PeaChomper/PeaChomperPreview");
		prePlantPrefab[1012] = Resources.Load<GameObject>("Plants/_Mixer/NutChomper/NutChomperPreview");
		prePlantPrefab[1013] = Resources.Load<GameObject>("Plants/_Mixer/SuperChomper/SuperChomperPreview");
		prePlantPrefab[1014] = Resources.Load<GameObject>("Plants/_Mixer/SunChomper/SunChomperPreview");
		prePlantPrefab[1015] = Resources.Load<GameObject>("Plants/_Mixer/PotatoChomper/PotatoChomperPreview");
		prePlantPrefab[1016] = Resources.Load<GameObject>("Plants/_Mixer/CherryChomper/CherryChomperPreview");
		prePlantPrefab[1017] = Resources.Load<GameObject>("Plants/_Mixer/CherryGatlingPea/CherryGatlingPreview");
		prePlantPrefab[1018] = Resources.Load<GameObject>("Plants/_Mixer/PeaSmallPuff/PeaSmallPuffPreview");
		prePlantPrefab[1019] = Resources.Load<GameObject>("Plants/_Mixer/DoublePuff/DoublePuffPreview");
		prePlantPrefab[1020] = Resources.Load<GameObject>("Plants/_Mixer/IronPea/IronPeaPreview");
		prePlantPrefab[1021] = Resources.Load<GameObject>("Plants/_Mixer/PuffNut/PuffNutPreview");
		prePlantPrefab[1022] = Resources.Load<GameObject>("Plants/_Mixer/HypnoPuff/HypnoPuffPreview");
		prePlantPrefab[1023] = Resources.Load<GameObject>("Plants/_Mixer/HypnoFume/HypnoFumePreview");
		prePlantPrefab[1024] = Resources.Load<GameObject>("Plants/_Mixer/ScaredyHypno/ScaredyHypnoPreview");
		prePlantPrefab[1025] = Resources.Load<GameObject>("Plants/_Mixer/ScaredFume/ScaredFumePreview");
		prePlantPrefab[1026] = Resources.Load<GameObject>("Plants/_Mixer/SuperHypno/SuperHypnoPreview");
		prePlantPrefab[1027] = Resources.Load<GameObject>("Plants/TallNut/TallNutPreview");
		prePlantPrefab[1028] = Resources.Load<GameObject>("Plants/TallNut/TallNutFootballPreview");
		prePlantPrefab[1029] = Resources.Load<GameObject>("Plants/WallNut/IronNutPreview");
		prePlantPrefab[1030] = Resources.Load<GameObject>("Plants/DoublePea/DoubleShooterPreview");
		prePlantPrefab[1031] = Resources.Load<GameObject>("Plants/SunShroom/SunShroomPreview");
		prePlantPrefab[1032] = Resources.Load<GameObject>("Plants/GatlingPea/GatlingPeaPreview");
		prePlantPrefab[1033] = Resources.Load<GameObject>("Plants/TwinFlower/TwinFlowerPreview");
		prePlantPrefab[1034] = Resources.Load<GameObject>("Plants/ShowPeaShooter/SnowPeaShooterPreview");
		prePlantPrefab[1035] = Resources.Load<GameObject>("Plants/_Mixer/IcePuff/IcePuffPreview");
		prePlantPrefab[1036] = Resources.Load<GameObject>("Plants/_Mixer/SmallIceShroom/SmallIceShroomPreview");
		prePlantPrefab[1037] = Resources.Load<GameObject>("Plants/_Mixer/IceFumeShroom/IceFumeShroomPreview");
		prePlantPrefab[1038] = Resources.Load<GameObject>("Plants/_Mixer/IceScaredyShroom/IceScaredyShroomPreview");
		prePlantPrefab[1039] = Resources.Load<GameObject>("Plants/TallNut/TallIceNutPreview");
		prePlantPrefab[1040] = Resources.Load<GameObject>("Plants/_Mixer/IceDoom/IceDoomPreview");
		prePlantPrefab[1041] = Resources.Load<GameObject>("Plants/_Mixer/IceHypno/IceHypnoPreview");
		prePlantPrefab[1042] = Resources.Load<GameObject>("Plants/_Mixer/ScaredyDoom/ScaredyDoomPreview");
		prePlantPrefab[1043] = Resources.Load<GameObject>("Plants/_Mixer/DoomFume/DoomFumePreview");
		prePlantPrefab[1044] = Resources.Load<GameObject>("Plants/_Mixer/DoomPuff/PuffDoomPreview");
		prePlantPrefab[1045] = Resources.Load<GameObject>("Plants/_Mixer/HypnoDoom/HypnoDoomPreview");
		prePlantPrefab[1046] = Resources.Load<GameObject>("Plants/_Mixer/IceDoomFume/IceDoomFumePreview");
		prePlantPrefab[1047] = Resources.Load<GameObject>("Plants/_Mixer/ThreeSquash/ThreeSquashPreview");
		prePlantPrefab[1048] = Resources.Load<GameObject>("Plants/TorchWood/TorchWoodPreview");
		prePlantPrefab[1049] = Resources.Load<GameObject>("Plants/_Mixer/Jalatang/JalatangPreview");
		prePlantPrefab[1050] = Resources.Load<GameObject>("Plants/_Mixer/Squashtang/SquashtangPreview");
		prePlantPrefab[1051] = Resources.Load<GameObject>("Plants/_Mixer/Threetang/ThreetangPreview");
		prePlantPrefab[1052] = Resources.Load<GameObject>("Plants/TorchWood/EpicTorchWoodPreview");
		prePlantPrefab[1053] = Resources.Load<GameObject>("Plants/TorchWood/AdvancedTorchWoodPreview");
		prePlantPrefab[1054] = Resources.Load<GameObject>("Plants/Squash/JalaSquashPreview");
		prePlantPrefab[1055] = Resources.Load<GameObject>("Plants/_Mixer/ThreeTorch/ThreeTorchPreview");
		prePlantPrefab[1056] = Resources.Load<GameObject>("Plants/_Mixer/KelpTorch/KelpTorchPreview");
		prePlantPrefab[1057] = Resources.Load<GameObject>("Plants/Squash/FireSquashPreview");
		prePlantPrefab[1058] = Resources.Load<GameObject>("Plants/ThreePeater/DarkThreePeaterPreview");
		prePlantPrefab[1059] = Resources.Load<GameObject>("Plants/TorchWood/SquashTorchPreview");
		prePlantPrefab[1060] = Resources.Load<GameObject>("Plants/Caltrop/SpikeRock/SpikerockPreview");
		prePlantPrefab[1061] = Resources.Load<GameObject>("Plants/Caltrop/FireSpike/FireSpikePreview");
		prePlantPrefab[1062] = Resources.Load<GameObject>("Plants/Caltrop/JalaSpike/JalaSpikePreview");
		prePlantPrefab[1063] = Resources.Load<GameObject>("Plants/Caltrop/SquashSpike/SquashSpikePreview");
		prePlantPrefab[1064] = Resources.Load<GameObject>("Plants/Caltrop/ThreeSpike/ThreeSpikePreview");
		prePlantPrefab[1065] = Resources.Load<GameObject>("Plants/_Mixer/DoublePuff/GatlingPuffPreview");
		prePlantPrefab[1066] = Resources.Load<GameObject>("Plants/_Mixer/SuperKelp/SuperKelpPreview");
		prePlantPrefab[1067] = Resources.Load<GameObject>("Plants/CattailPlant/CattailPlantPreview");
		prePlantPrefab[1068] = Resources.Load<GameObject>("Plants/CattailPlant/IceCattailPreview");
		prePlantPrefab[1069] = Resources.Load<GameObject>("Plants/CattailPlant/FireCattailPreview");
		prePlantPrefab[1070] = Resources.Load<GameObject>("Plants/GloomShroom/GloomShroomPreview");
		prePlantPrefab[1071] = Resources.Load<GameObject>("Plants/GloomShroom/FireGloomPreview");
		prePlantPrefab[1072] = Resources.Load<GameObject>("Plants/GloomShroom/IceGloomPreview");
		prePlantPrefab[1073] = Resources.Load<GameObject>("Plants/TallNut/TallFireNutPreview");
		prePlantPrefab[1074] = Resources.Load<GameObject>("Plants/Caltrop/SpikeRock/IceSpikerockPreview");
		prePlantPrefab[1075] = Resources.Load<GameObject>("Plants/Caltrop/SpikeRock/FireSpikerockPreview");
	}

	private static void LoadZombie()
	{
		zombiePrefab[0] = Resources.Load<GameObject>("Zombies/Zombie/Zombie");
		zombiePrefab[1] = Resources.Load<GameObject>("Zombies/Zombie/Zombie");
		zombiePrefab[2] = Resources.Load<GameObject>("Zombies/Zombie/ConeZombie");
		zombiePrefab[3] = Resources.Load<GameObject>("Zombies/Zombie_polevaulter/PolevaulterZombie");
		zombiePrefab[4] = Resources.Load<GameObject>("Zombies/Zombie/BucketZombie");
		zombiePrefab[5] = Resources.Load<GameObject>("Zombies/PaperZombie/PaperZombie");
		zombiePrefab[6] = Resources.Load<GameObject>("Zombies/Zombie_polevaulter/Dance/DancePolevaulterPrefab");
		zombiePrefab[7] = Resources.Load<GameObject>("Zombies/Zombie_polevaulter/Dance/DancePolevaulter2Prefab");
		zombiePrefab[8] = Resources.Load<GameObject>("Zombies/Door/ZombieDoor");
		zombiePrefab[9] = Resources.Load<GameObject>("Zombies/Zombie_football/Zombie_footballPrefab");
		zombiePrefab[10] = Resources.Load<GameObject>("Zombies/Zombie_Jackson/Zombie_JacksonPrefab");
		zombiePrefab[11] = Resources.Load<GameObject>("Zombies/Zombie/InWater/ZombieDuck");
		zombiePrefab[12] = Resources.Load<GameObject>("Zombies/Zombie/InWater/ZombieDuckCone");
		zombiePrefab[13] = Resources.Load<GameObject>("Zombies/Zombie/InWater/ZombieDuckBucket");
		zombiePrefab[14] = Resources.Load<GameObject>("Zombies/SubmarineZombie/SubmarineZombiePrefab");
		zombiePrefab[15] = Resources.Load<GameObject>("Zombies/PaperZombie/PaperZombie95");
		zombiePrefab[16] = Resources.Load<GameObject>("Zombies/Zombie_Driver/ZombieDriverPrefab");
		zombiePrefab[17] = Resources.Load<GameObject>("Zombies/Zombie_snorkle/Zombie_snorklePrefab");
		zombiePrefab[18] = Resources.Load<GameObject>("Zombies/Zombie_Driver/SuperDriver/SuperDriverPrefab");
		zombiePrefab[19] = Resources.Load<GameObject>("Zombies/Zombie_dolphinrider/DolphinriderPrefab");
		zombiePrefab[20] = Resources.Load<GameObject>("Zombies/Zombie_drown/DrownZombie");
		zombiePrefab[21] = Resources.Load<GameObject>("Zombies/Zombie/DollDiamondZombie");
		zombiePrefab[22] = Resources.Load<GameObject>("Zombies/Zombie/DollGoldZombie");
		zombiePrefab[23] = Resources.Load<GameObject>("Zombies/Zombie/DollSilverZombie");
		zombiePrefab[100] = Resources.Load<GameObject>("Zombies/PlantZombie/PeaShooterZ");
		zombiePrefab[101] = Resources.Load<GameObject>("Zombies/PlantZombie/CherryShooterZ");
		zombiePrefab[102] = Resources.Load<GameObject>("Zombies/PlantZombie/SuperCherryZ");
		zombiePrefab[103] = Resources.Load<GameObject>("Zombies/PlantZombie/WallNutZ");
		zombiePrefab[104] = Resources.Load<GameObject>("Zombies/PlantZombie/Paper/CherryPaper");
		zombiePrefab[105] = Resources.Load<GameObject>("Zombies/Zombie/RandomZombie");
		zombiePrefab[106] = Resources.Load<GameObject>("Zombies/PlantZombie/BucketNutZ");
		zombiePrefab[107] = Resources.Load<GameObject>("Zombies/PlantZombie/CherryNutZ/CherryNutZ");
		zombiePrefab[108] = Resources.Load<GameObject>("Zombies/PlantZombie/IronPeaZ/IronPeaZPrefab");
		zombiePrefab[109] = Resources.Load<GameObject>("Zombies/Zombie_football/TallNutFootballZ/TallNutFootballZPrefab");
		zombiePrefab[110] = Resources.Load<GameObject>("Zombies/Zombie/RandomPlusZombie");
		zombiePrefab[111] = Resources.Load<GameObject>("Zombies/PlantZombie/TallIceNutZ/TallIceNutZ");
		zombiePrefab[200] = Resources.Load<GameObject>("Zombies/InTravel/SuperSubmarine/SuperSubmarinePrefab");
		zombiePrefab[201] = Resources.Load<GameObject>("Zombies/InTravel/JacksonDriver/JacksonDriverPrefab");
		zombiePrefab[202] = Resources.Load<GameObject>("Zombies/InTravel/FootballDrown/FootballDrownPrefab");
		zombiePrefab[203] = Resources.Load<GameObject>("Zombies/InTravel/CherryPaper95/CherryPaper95");
	}

	private static void LoadPreZombie()
	{
		preZombiePrefab[0] = Resources.Load<GameObject>("Zombies/Zombie/ZombiePreview");
		preZombiePrefab[1] = Resources.Load<GameObject>("Zombies/Zombie/ZombiePreview");
		preZombiePrefab[2] = Resources.Load<GameObject>("Zombies/Zombie/ConeZombiePreview");
		preZombiePrefab[3] = Resources.Load<GameObject>("Zombies/Zombie_polevaulter/PolevaulterZombiePreview");
		preZombiePrefab[4] = Resources.Load<GameObject>("Zombies/Zombie/BucketZombiePreview");
		preZombiePrefab[5] = Resources.Load<GameObject>("Zombies/PaperZombie/PaperZombiePreview");
		preZombiePrefab[8] = Resources.Load<GameObject>("Zombies/Door/ZombieDoorPreview");
		preZombiePrefab[9] = Resources.Load<GameObject>("Zombies/Zombie_football/Zombie_footballPreview");
		preZombiePrefab[10] = Resources.Load<GameObject>("Zombies/Zombie_Jackson/Zombie_JacksonPreview");
		preZombiePrefab[14] = Resources.Load<GameObject>("Zombies/SubmarineZombie/SubmarineZombiePreview");
		preZombiePrefab[16] = Resources.Load<GameObject>("Zombies/Zombie_Driver/ZombieDriverPreview");
		preZombiePrefab[17] = Resources.Load<GameObject>("Zombies/Zombie_snorkle/Zombie_snorklePreview");
		preZombiePrefab[19] = Resources.Load<GameObject>("Zombies/Zombie_dolphinrider/DolphinriderPreview");
		preZombiePrefab[104] = Resources.Load<GameObject>("Zombies/PlantZombie/Paper/CherryPaperPreview");
		preZombiePrefab[105] = Resources.Load<GameObject>("Zombies/Zombie/RandomZombiePreview");
		preZombiePrefab[109] = Resources.Load<GameObject>("Zombies/Zombie_football/TallNutFootballZ/TallNutFootballZPreview");
	}

	private static void LoadBullet()
	{
		bulletPrefab[0] = Resources.Load<GameObject>("Bullet/Prefabs/ProjectilePea");
		bulletPrefab[1] = Resources.Load<GameObject>("Bullet/Prefabs/CherryBullet");
		bulletPrefab[2] = Resources.Load<GameObject>("Bullet/Prefabs/NutBullet");
		bulletPrefab[3] = Resources.Load<GameObject>("Bullet/Prefabs/SuperCherryBullet");
		bulletPrefab[4] = Resources.Load<GameObject>("Bullet/Prefabs/zombieblock1");
		bulletPrefab[5] = Resources.Load<GameObject>("Bullet/Prefabs/zombieblock2");
		bulletPrefab[6] = Resources.Load<GameObject>("Bullet/Prefabs/zombieblock3");
		bulletPrefab[7] = Resources.Load<GameObject>("Bullet/Prefabs/ProjectilePotato");
		bulletPrefab[8] = Resources.Load<GameObject>("Bullet/Prefabs/BulletSun");
		bulletPrefab[9] = Resources.Load<GameObject>("Bullet/Prefabs/Puff");
		bulletPrefab[10] = Resources.Load<GameObject>("Bullet/Prefabs/PuffPea");
		bulletPrefab[11] = Resources.Load<GameObject>("Bullet/Prefabs/ProjectileIronPea");
		bulletPrefab[12] = Resources.Load<GameObject>("Bullet/ThreeSpikeBullet/ThreeSpikeBullet");
		bulletPrefab[13] = Resources.Load<GameObject>("Bullet/Prefabs/PuffRandomColor");
		bulletPrefab[14] = Resources.Load<GameObject>("Bullet/Prefabs/PuffLove");
		bulletPrefab[15] = Resources.Load<GameObject>("Bullet/Prefabs/ProjectileSnowPea");
		bulletPrefab[16] = Resources.Load<GameObject>("Bullet/Prefabs/PuffSnowPea");
		bulletPrefab[17] = Resources.Load<GameObject>("Bullet/Prefabs/ProjectileIcicle");
		bulletPrefab[18] = Resources.Load<GameObject>("Bullet/Prefabs/ProjectileIcicleSmall");
		bulletPrefab[19] = Resources.Load<GameObject>("Bullet/Prefabs/FirePuffPea");
		bulletPrefab[20] = Resources.Load<GameObject>("Bullet/Prefabs/TrackBullet");
		bulletPrefab[21] = Resources.Load<GameObject>("Bullet/Prefabs/PuffSnow");
		bulletPrefab[22] = Resources.Load<GameObject>("Bullet/Prefabs/PuffBlack");
		bulletPrefab[23] = Resources.Load<GameObject>("Bullet/Prefabs/DoomBullet");
		bulletPrefab[24] = Resources.Load<GameObject>("Bullet/Prefabs/IceDoomBullet");
		bulletPrefab[25] = Resources.Load<GameObject>("Bullet/Prefabs/FirePea1");
		bulletPrefab[26] = Resources.Load<GameObject>("Bullet/Prefabs/FirePea2");
		bulletPrefab[27] = Resources.Load<GameObject>("Bullet/Prefabs/FirePea3");
		bulletPrefab[28] = Resources.Load<GameObject>("Bullet/Prefabs/SquashBullet");
		bulletPrefab[29] = Resources.Load<GameObject>("Bullet/Prefabs/TangkelpBullet");
		bulletPrefab[30] = Resources.Load<GameObject>("Bullet/Prefabs/FirekelpBullet");
		bulletPrefab[31] = Resources.Load<GameObject>("Bullet/FireCherryAn/FireCherry");
		bulletPrefab[32] = Resources.Load<GameObject>("Bullet/Prefabs/SquashKelpBullet");
		bulletPrefab[33] = Resources.Load<GameObject>("Bullet/Prefabs/ProjectileCactus");
		bulletPrefab[34] = Resources.Load<GameObject>("Bullet/Prefabs/ProjectileIceCactus");
		bulletPrefab[35] = Resources.Load<GameObject>("Bullet/Prefabs/ProjectileFireCactus");
		bulletPrefab[36] = Resources.Load<GameObject>("Bullet/Prefabs/CherrySquashBullet");
	}

	private static void LoadParticle()
	{
		particlePrefab[0] = Resources.Load<GameObject>("Particle/Prefabs/PeaSplat");
		particlePrefab[1] = Resources.Load<GameObject>("Particle/Prefabs/Dirt");
		particlePrefab[2] = Resources.Load<GameObject>("Particle/Prefabs/BombCloud");
		particlePrefab[3] = Resources.Load<GameObject>("Particle/Prefabs/SunBombCloud");
		particlePrefab[4] = Resources.Load<GameObject>("Particle/Prefabs/CherrySplat");
		particlePrefab[5] = Resources.Load<GameObject>("Particle/Prefabs/NutPartical");
		particlePrefab[6] = Resources.Load<GameObject>("Particle/Prefabs/CherryNutPartical");
		particlePrefab[7] = Resources.Load<GameObject>("Particle/Prefabs/NutSplat");
		particlePrefab[8] = Resources.Load<GameObject>("Particle/Prefabs/PotaoParticle");
		particlePrefab[9] = Resources.Load<GameObject>("Particle/Prefabs/PotatoRise");
		particlePrefab[10] = Resources.Load<GameObject>("Particle/Prefabs/GreenCherrySplat");
		particlePrefab[11] = Resources.Load<GameObject>("Particle/Prefabs/RandomCloud");
		particlePrefab[12] = Resources.Load<GameObject>("Particle/Prefabs/ZombieBlockSplat");
		particlePrefab[13] = Resources.Load<GameObject>("Particle/Prefabs/PurpleNutDust");
		particlePrefab[14] = Resources.Load<GameObject>("Particle/Prefabs/BombCloudSmall");
		particlePrefab[15] = Resources.Load<GameObject>("Particle/Prefabs/PotatoSplat");
		particlePrefab[16] = Resources.Load<GameObject>("Particle/Prefabs/Health");
		particlePrefab[17] = Resources.Load<GameObject>("Particle/Prefabs/PuffSplat");
		particlePrefab[18] = Resources.Load<GameObject>("Particle/Prefabs/IronPeaSplat");
		particlePrefab[19] = Resources.Load<GameObject>("Particle/Prefabs/Fume");
		particlePrefab[20] = Resources.Load<GameObject>("Particle/Prefabs/MindControl");
		particlePrefab[21] = Resources.Load<GameObject>("Particle/Prefabs/FumeColorful");
		particlePrefab[22] = Resources.Load<GameObject>("Particle/Prefabs/FumeColorful2");
		particlePrefab[23] = Resources.Load<GameObject>("Particle/Prefabs/Star");
		particlePrefab[24] = Resources.Load<GameObject>("Particle/Prefabs/SnowPeaSplat");
		particlePrefab[25] = Resources.Load<GameObject>("Particle/Prefabs/Doom");
		particlePrefab[26] = Resources.Load<GameObject>("Particle/Prefabs/PuffBlackSplat");
		particlePrefab[27] = Resources.Load<GameObject>("Particle/Prefabs/DoomSplat");
		particlePrefab[28] = Resources.Load<GameObject>("Particle/Prefabs/IceDoomSplat");
		particlePrefab[29] = Resources.Load<GameObject>("Particle/Prefabs/IceDoom");
		particlePrefab[30] = Resources.Load<GameObject>("Particle/Prefabs/FumeIce");
		particlePrefab[31] = Resources.Load<GameObject>("Particle/Prefabs/FumeDoom");
		particlePrefab[32] = Resources.Load<GameObject>("Particle/Prefabs/WaterSplats");
		particlePrefab[33] = Resources.Load<GameObject>("Particle/Prefabs/Fire");
		particlePrefab[34] = Resources.Load<GameObject>("Particle/Prefabs/MachineExplode");
		particlePrefab[35] = Resources.Load<GameObject>("Particle/Prefabs/FireFree");
		particlePrefab[36] = Resources.Load<GameObject>("Particle/Prefabs/MachineExplodeRed");
		particlePrefab[37] = Resources.Load<GameObject>("Particle/Prefabs/Gloom");
		particlePrefab[38] = Resources.Load<GameObject>("Particle/Prefabs/GloomFire");
		particlePrefab[39] = Resources.Load<GameObject>("Particle/Prefabs/GloomIce");
	}

	private static void LoadSprite()
	{
		spritePrefab[0] = Resources.Load<Sprite>("Zombies/Image/reanim/Zombie_outerarm_upper2");
		spritePrefab[1] = Resources.Load<Sprite>("Zombies/Image/reanim/Zombie_cone2");
		spritePrefab[2] = Resources.Load<Sprite>("Zombies/Image/reanim/Zombie_cone3");
		spritePrefab[3] = Resources.Load<Sprite>("Zombies/Image/reanim/Zombie_bucket2");
		spritePrefab[4] = Resources.Load<Sprite>("Zombies/Image/reanim/Zombie_bucket3");
		spritePrefab[5] = Resources.Load<Sprite>("Zombies/PaperZombie/paper_bone");
		spritePrefab[6] = Resources.Load<Sprite>("Zombies/PaperZombie/paper2");
		spritePrefab[7] = Resources.Load<Sprite>("Zombies/PaperZombie/paper3");
		spritePrefab[8] = Resources.Load<Sprite>("Plants/WallNut/crackedA");
		spritePrefab[9] = Resources.Load<Sprite>("Plants/WallNut/crackedB");
		spritePrefab[10] = Resources.Load<Sprite>("Zombies/PlantZombie/Paper/CherryPaper1");
		spritePrefab[11] = Resources.Load<Sprite>("Zombies/PlantZombie/Paper/CherryPaper2");
		spritePrefab[12] = Resources.Load<Sprite>("Zombies/Zombie/Zombie_cone2");
		spritePrefab[13] = Resources.Load<Sprite>("Zombies/Zombie/Zombie_cone3");
		spritePrefab[14] = Resources.Load<Sprite>("Zombies/PlantZombie/ironNut2");
		spritePrefab[15] = Resources.Load<Sprite>("Zombies/PlantZombie/ironNut3");
		spritePrefab[16] = Resources.Load<Sprite>("Plants/_Mixer/CherryNut/crack1");
		spritePrefab[17] = Resources.Load<Sprite>("Plants/_Mixer/CherryNut/crack2");
		spritePrefab[18] = Resources.Load<Sprite>("Zombies/Door/Zombie_screendoor2");
		spritePrefab[19] = Resources.Load<Sprite>("Zombies/Door/Zombie_screendoor3");
		spritePrefab[20] = Resources.Load<Sprite>("Zombies/Zombie_football/Zombie_football_helmet2");
		spritePrefab[21] = Resources.Load<Sprite>("Zombies/Zombie_football/Zombie_football_helmet3");
		spritePrefab[22] = Resources.Load<Sprite>("Zombies/Zombie_football/TallNutFootballZ/tnf2");
		spritePrefab[23] = Resources.Load<Sprite>("Zombies/Zombie_football/TallNutFootballZ/tnf3");
		spritePrefab[24] = Resources.Load<Sprite>("Zombies/Zombie/Gold2");
		spritePrefab[25] = Resources.Load<Sprite>("Zombies/Zombie/Gold3");
		spritePrefab[26] = Resources.Load<Sprite>("Zombies/Zombie_Jackson/Zombie_Jackson_outerarm_upper2");
		spritePrefab[27] = Resources.Load<Sprite>("Zombies/PlantZombie/TallIceNutZ/TallIceCracked1");
		spritePrefab[28] = Resources.Load<Sprite>("Zombies/PlantZombie/TallIceNutZ/TallIceCracked2");
		spritePrefab[29] = Resources.Load<Sprite>("Zombies/Zombie_Driver/Zombie_zamboni_1_damage1");
		spritePrefab[30] = Resources.Load<Sprite>("Zombies/Zombie_Driver/Zombie_zamboni_1_damage2");
		spritePrefab[31] = Resources.Load<Sprite>("Zombies/Zombie_Driver/Zombie_zamboni_2_damage2");
		spritePrefab[32] = Resources.Load<Sprite>("Zombies/Zombie_Driver/Zombie_zamboni_2_damage2");
		spritePrefab[33] = Resources.Load<Sprite>("Zombies/Zombie_Driver/SuperDriver/body_dmg1");
		spritePrefab[34] = Resources.Load<Sprite>("Zombies/Zombie_Driver/SuperDriver/lower_dmg1");
		spritePrefab[35] = Resources.Load<Sprite>("Zombies/Zombie_Driver/SuperDriver/body_dmg2");
		spritePrefab[36] = Resources.Load<Sprite>("Zombies/Zombie_Driver/SuperDriver/lower_dmg2");
		spritePrefab[37] = Resources.Load<Sprite>("Zombies/Zombie_Driver/SuperDriver/below_dmg");
		spritePrefab[38] = Resources.Load<Sprite>("Zombies/PaperZombie/paper_bone1");
		spritePrefab[39] = Resources.Load<Sprite>("Bullet/fireironpea");
		spritePrefab[40] = Resources.Load<Sprite>("Zombies/InTravel/FootballDrown/dmg1");
		spritePrefab[41] = Resources.Load<Sprite>("Zombies/InTravel/FootballDrown/dmg2");
		spritePrefab[42] = Resources.Load<Sprite>("Zombies/PaperZombie/book2");
		spritePrefab[43] = Resources.Load<Sprite>("Zombies/PaperZombie/book3");
		spritePrefab[44] = Resources.Load<Sprite>("Zombies/Zombie/DiamondDoll2");
		spritePrefab[45] = Resources.Load<Sprite>("Zombies/Zombie/DiamondDoll3");
		spritePrefab[46] = Resources.Load<Sprite>("Zombies/Zombie/GoldDoll2");
		spritePrefab[47] = Resources.Load<Sprite>("Zombies/Zombie/GoldDoll3");
		spritePrefab[48] = Resources.Load<Sprite>("Zombies/Zombie/SilverDoll2");
		spritePrefab[49] = Resources.Load<Sprite>("Zombies/Zombie/SilverDoll3");
	}

	private static void LoadCoin()
	{
		coinPrefab[0] = Resources.Load<GameObject>("Items/Sun/Sun");
		coinPrefab[2] = Resources.Load<GameObject>("Items/Sun/SmallSun");
	}

	private static void LoadMusic()
	{
		musicPrefab[0] = Resources.Load<AudioClip>("Music/MainMenu");
		musicPrefab[1] = Resources.Load<AudioClip>("Music/SelectCard");
		musicPrefab[2] = Resources.Load<AudioClip>("Music/Day");
		musicPrefab[3] = Resources.Load<AudioClip>("Music/Day1");
		musicPrefab[4] = Resources.Load<AudioClip>("Music/Night");
		musicPrefab[5] = Resources.Load<AudioClip>("Music/Night1");
		musicPrefab[6] = Resources.Load<AudioClip>("Music/Pool");
		musicPrefab[7] = Resources.Load<AudioClip>("Music/Pool1");
		musicPrefab[12] = Resources.Load<AudioClip>("Music/loon");
		musicPrefab[13] = Resources.Load<AudioClip>("Music/battle");
		musicPrefab[14] = Resources.Load<AudioClip>("Music/winmusic");
		musicPrefab[15] = Resources.Load<AudioClip>("Music/IZE");
	}

	private static void LoadGridItem()
	{
		gridItemPrefab[0] = Resources.Load<GameObject>("Image/crater/CraterDay");
		gridItemPrefab[1] = Resources.Load<GameObject>("Image/crater/CraterNight");
		gridItemPrefab[2] = Resources.Load<GameObject>("Image/crater/CraterNight");
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			if (isFullScreen)
			{
				Screen.SetResolution(1920, 1080, fullscreen: false);
			}
			else
			{
				Screen.SetResolution(1920, 1080, fullscreen: true);
			}
			isFullScreen = !isFullScreen;
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			Screen.SetResolution(1280, 720, fullscreen: false);
		}
		MusicUpdate();
	}

	private void MusicUpdate()
	{
		musicChangeTime += Time.deltaTime;
		if (theGameStatus == 0)
		{
			switch (Board.Instance.musicType)
			{
			case 1:
				DayMusicUpdate();
				break;
			case 2:
				NightMusicUpdate();
				break;
			}
		}
		if (theGameStatus < 0 || theGameStatus == 1)
		{
			music.volume = gameMusicVolume;
		}
	}

	private void DayMusicUpdate()
	{
		if (Board.Instance.theCurrentNumOfZombieUncontroled >= 10)
		{
			if (musicDrum.volume < gameMusicVolume)
			{
				musicDrum.volume += Time.deltaTime;
				music.volume -= Time.deltaTime;
			}
			else
			{
				musicDrum.volume = gameMusicVolume;
				music.volume = 0f;
			}
		}
		if (Board.Instance.theCurrentNumOfZombieUncontroled < 10)
		{
			if (musicDrum.volume > 0f)
			{
				musicDrum.volume -= Time.deltaTime;
				music.volume += Time.deltaTime;
			}
			else
			{
				music.volume = gameMusicVolume;
				musicDrum.volume = 0f;
			}
		}
		if (Input.GetKeyDown(KeyCode.K))
		{
			Debug.Log(music.time + "  " + musicDrum.time);
		}
		if ((int)Time.time % 10 == 0 && music.time != musicDrum.time)
		{
			music.time = musicDrum.time;
		}
	}

	private void NightMusicUpdate()
	{
		if (Board.Instance.theCurrentNumOfZombieUncontroled >= 10 && !onMusicDrumFade)
		{
			musicChangeTime = 0f;
			if (music.volume > 0f)
			{
				music.volume -= 0.2f * Time.deltaTime;
			}
			else
			{
				music.volume = 0f;
			}
			if (musicDrum.volume < gameMusicVolume)
			{
				musicDrum.volume += 0.2f * Time.deltaTime;
			}
			else
			{
				musicDrum.volume = gameMusicVolume;
			}
			if (!musicDrum.isPlaying)
			{
				musicDrum.clip = musicPrefab[5];
				musicDrum.time = 0f;
				musicDrum.Play();
			}
		}
		if (Board.Instance.theCurrentNumOfZombieUncontroled >= 10 || !(musicChangeTime > 10f))
		{
			return;
		}
		onMusicDrumFade = true;
		if (musicDrum.volume > 0f)
		{
			musicDrum.volume -= 0.2f * Time.deltaTime;
			if (musicDrum.volume < 0f)
			{
				musicDrum.volume = 0f;
				musicDrum.Stop();
			}
		}
		if (!musicDrum.isPlaying)
		{
			if (music.volume < gameMusicVolume)
			{
				music.volume += 0.2f * Time.deltaTime;
				return;
			}
			music.volume = gameMusicVolume;
			musicChangeTime = 0f;
			onMusicDrumFade = false;
		}
	}

	public static void ClearItemInCanvas()
	{
		foreach (Transform item in canvas.transform)
		{
			Object.Destroy(item.gameObject);
		}
		foreach (Transform item2 in canvasUp.transform)
		{
			Object.Destroy(item2.gameObject);
		}
	}
}

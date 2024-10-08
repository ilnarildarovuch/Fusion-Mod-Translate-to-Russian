using UnityEngine;

public class AlmanacCard : MonoBehaviour
{
	public enum CardNumber
	{
		OtherCard = -10,
		CattailGirl = -3,
		Wheat = -2,
		EndoFlame = -1,
		PeaShooter = 0,
		SunFlower = 1,
		CherryBomb = 2,
		WallNut = 3,
		PotatoMine = 4,
		Chomper = 5,
		Present = 6,
		Puff = 7,
		Fume = 8,
		Hypono = 9,
		ScaredyShroom = 10,
		IceShroom = 11,
		DoomShroom = 12,
		LilyPad = 13,
		Squash = 14,
		ThreePeater = 15,
		Tanglekelp = 16,
		Jalapeno = 17,
		Caltrop = 18,
		TorchWood = 19,
		TallNut = 20,
		GloomShroom = 21,
		SpikeRock = 22,
		Cattail = 23
	}

	public int theSeedType;

	public CardNumber number;

	public bool isBasicCard;

	private void Start()
	{
		if (!GameAPP.developerMode && !CheckUnlock((int)number))
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void OnMouseDown()
	{
		GameAPP.PlaySound(19);
		base.transform.parent.gameObject.transform.parent.gameObject.GetComponent<AlmanacPlantCtrl>().GetSeedType(theSeedType, isBasicCard);
		if (isBasicCard)
		{
			base.transform.GetChild(base.transform.childCount - 1).gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_Brightness", 1f);
			CursorChange.SetDefaultCursor();
		}
	}

	private void OnMouseEnter()
	{
		base.transform.GetChild(base.transform.childCount - 1).gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_Brightness", 1.5f);
		CursorChange.SetClickCursor();
	}

	private void OnMouseExit()
	{
		base.transform.GetChild(base.transform.childCount - 1).gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_Brightness", 1f);
		CursorChange.SetDefaultCursor();
	}

	private bool CheckUnlock(int theSeedType)
	{
		int num;
		switch (theSeedType)
		{
		case -3:
			if (GameAPP.survivalLevelCompleted[8])
			{
				return true;
			}
			return false;
		case -2:
			if (GameAPP.gameLevelCompleted[1])
			{
				return true;
			}
			return false;
		case -1:
			if (GameAPP.clgLevelCompleted[17])
			{
				return true;
			}
			return false;
		case 0:
		case 1:
			num = 0;
			break;
		case 2:
			num = 1;
			break;
		case 3:
			num = 2;
			break;
		case 4:
			num = 4;
			break;
		case 5:
			num = 5;
			break;
		case 6:
			num = 6;
			break;
		case 7:
			num = 9;
			break;
		case 8:
			num = 10;
			break;
		case 9:
			num = 11;
			break;
		case 10:
			num = 13;
			break;
		case 11:
			num = 14;
			break;
		case 12:
			num = 15;
			break;
		case 13:
			num = 18;
			break;
		case 14:
			num = 19;
			break;
		case 15:
			num = 20;
			break;
		case 16:
			num = 21;
			break;
		case 17:
			num = 22;
			break;
		case 18:
			num = 23;
			break;
		case 19:
			num = 24;
			break;
		case 20:
			num = 8;
			break;
		case 21:
			num = 18;
			break;
		case 22:
			num = 25;
			break;
		case 23:
			num = 26;
			break;
		default:
			return false;
		}
		if (num == 0)
		{
			return true;
		}
		if (GameAPP.advLevelCompleted[num])
		{
			return true;
		}
		return false;
	}
}

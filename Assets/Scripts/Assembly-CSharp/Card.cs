using UnityEngine;

public class Card : MonoBehaviour
{
	public enum Unlock
	{
		CattailGirl = -3,
		Wheat = -2,
		EndoFlame = -1,
		Unlocked = 0,
		Advantrue1 = 1,
		Advantrue2 = 2,
		Advantrue3 = 3,
		Advantrue4 = 4,
		Advantrue5 = 5,
		Advantrue6 = 6,
		Advantrue7 = 7,
		Advantrue8 = 8,
		Advantrue9 = 9,
		Advantrue10 = 10,
		Advantrue11 = 11,
		Advantrue12 = 12,
		Advantrue13 = 13,
		Advantrue14 = 14,
		Advantrue15 = 15,
		Advantrue16 = 16,
		Advantrue17 = 17,
		Advantrue18 = 18,
		Advantrue19 = 19,
		Advantrue20 = 20,
		Advantrue21 = 21,
		Advantrue22 = 22,
		Advantrue23 = 23,
		Advantrue24 = 24,
		Advantrue25 = 25,
		Advantrue26 = 26,
		Advantrue27 = 27
	}

	public Unlock unlockLevel;

	public bool isNormalCard = true;

	private bool avaliable;

	private void Start()
	{
		if (GameAPP.developerMode)
		{
			return;
		}
		switch ((int)unlockLevel)
		{
		case -3:
			if (GameAPP.survivalLevelCompleted[8])
			{
				avaliable = true;
			}
			break;
		case -2:
			if (GameAPP.gameLevelCompleted[1])
			{
				avaliable = true;
			}
			break;
		case -1:
			if (GameAPP.clgLevelCompleted[17])
			{
				avaliable = true;
			}
			break;
		}
		if (unlockLevel >= Unlock.Unlocked && (unlockLevel == Unlock.Unlocked || GameAPP.advLevelCompleted[(int)unlockLevel]))
		{
			avaliable = true;
		}
		if (avaliable)
		{
			return;
		}
		foreach (Transform item in base.transform)
		{
			item.gameObject.SetActive(value: false);
		}
	}
}

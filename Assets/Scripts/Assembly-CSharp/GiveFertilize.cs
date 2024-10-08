using UnityEngine;

public class GiveFertilize : MonoBehaviour
{
	private int occurrences;

	private Vector2 pos = new Vector2(-7.5f, 4f);

	private void AnimGive()
	{
		if (GameAPP.theGameStatus == 0 && AvaliableToGive())
		{
			occurrences++;
			if (occurrences > 8)
			{
				occurrences = 0;
				Object.Instantiate(Resources.Load<GameObject>("Items/Fertilize/Ferilize"), pos, Quaternion.identity, GameAPP.board.transform);
				GameAPP.PlaySound(66);
			}
		}
	}

	public static bool AvaliableToGive()
	{
		if (GameAPP.advLevelCompleted[12])
		{
			return true;
		}
		if (GameAPP.theBoardType == 1)
		{
			int theBoardLevel = GameAPP.theBoardLevel;
			if ((uint)(theBoardLevel - 4) <= 2u)
			{
				return true;
			}
		}
		return false;
	}
}

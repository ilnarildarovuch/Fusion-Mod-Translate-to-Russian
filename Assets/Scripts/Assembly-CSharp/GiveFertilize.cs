using System;
using UnityEngine;

// Token: 0x020000FB RID: 251
public class GiveFertilize : MonoBehaviour
{
	// Token: 0x060004DE RID: 1246 RVA: 0x0002A25C File Offset: 0x0002845C
	private void AnimGive()
	{
		if (GameAPP.theGameStatus == 0 && GiveFertilize.AvaliableToGive())
		{
			this.occurrences++;
			if (this.occurrences > 8)
			{
				this.occurrences = 0;
				Object.Instantiate<GameObject>(Resources.Load<GameObject>("Items/Fertilize/Ferilize"), this.pos, Quaternion.identity, GameAPP.board.transform);
				GameAPP.PlaySound(66, 0.5f);
			}
		}
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x0002A2CC File Offset: 0x000284CC
	public static bool AvaliableToGive()
	{
		if (GameAPP.advLevelCompleted[12])
		{
			return true;
		}
		if (GameAPP.theBoardType == 1)
		{
			int theBoardLevel = GameAPP.theBoardLevel;
			if (theBoardLevel - 4 <= 2)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000263 RID: 611
	private int occurrences;

	// Token: 0x04000264 RID: 612
	private Vector2 pos = new Vector2(-7.5f, 4f);
}

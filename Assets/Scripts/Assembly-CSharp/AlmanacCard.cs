using System;
using UnityEngine;

// Token: 0x02000027 RID: 39
public class AlmanacCard : MonoBehaviour
{
	// Token: 0x060000AB RID: 171 RVA: 0x000055B7 File Offset: 0x000037B7
	private void Start()
	{
		if (GameAPP.developerMode)
		{
			return;
		}
		if (!this.CheckUnlock((int)this.number))
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060000AC RID: 172 RVA: 0x000055DC File Offset: 0x000037DC
	private void OnMouseDown()
	{
		GameAPP.PlaySound(19, 0.5f);
		base.transform.parent.gameObject.transform.parent.gameObject.GetComponent<AlmanacPlantCtrl>().GetSeedType(this.theSeedType, this.isBasicCard);
		if (this.isBasicCard)
		{
			base.transform.GetChild(base.transform.childCount - 1).gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_Brightness", 1f);
			CursorChange.SetDefaultCursor();
		}
	}

	// Token: 0x060000AD RID: 173 RVA: 0x0000566D File Offset: 0x0000386D
	private void OnMouseEnter()
	{
		base.transform.GetChild(base.transform.childCount - 1).gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_Brightness", 1.5f);
		CursorChange.SetClickCursor();
	}

	// Token: 0x060000AE RID: 174 RVA: 0x000056AA File Offset: 0x000038AA
	private void OnMouseExit()
	{
		base.transform.GetChild(base.transform.childCount - 1).gameObject.GetComponent<SpriteRenderer>().material.SetFloat("_Brightness", 1f);
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x060000AF RID: 175 RVA: 0x000056E8 File Offset: 0x000038E8
	private bool CheckUnlock(int theSeedType)
	{
		int num;
		switch (theSeedType)
		{
		case -3:
			return GameAPP.survivalLevelCompleted[8];
		case -2:
			return GameAPP.gameLevelCompleted[1];
		case -1:
			return GameAPP.clgLevelCompleted[17];
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
		return num == 0 || GameAPP.advLevelCompleted[num];
	}

	// Token: 0x04000091 RID: 145
	public int theSeedType;

	// Token: 0x04000092 RID: 146
	public AlmanacCard.CardNumber number;

	// Token: 0x04000093 RID: 147
	public bool isBasicCard;

	// Token: 0x02000128 RID: 296
	public enum CardNumber
	{
		// Token: 0x04000325 RID: 805
		OtherCard = -10,
		// Token: 0x04000326 RID: 806
		CattailGirl = -3,
		// Token: 0x04000327 RID: 807
		Wheat,
		// Token: 0x04000328 RID: 808
		EndoFlame,
		// Token: 0x04000329 RID: 809
		PeaShooter,
		// Token: 0x0400032A RID: 810
		SunFlower,
		// Token: 0x0400032B RID: 811
		CherryBomb,
		// Token: 0x0400032C RID: 812
		WallNut,
		// Token: 0x0400032D RID: 813
		PotatoMine,
		// Token: 0x0400032E RID: 814
		Chomper,
		// Token: 0x0400032F RID: 815
		Present,
		// Token: 0x04000330 RID: 816
		Puff,
		// Token: 0x04000331 RID: 817
		Fume,
		// Token: 0x04000332 RID: 818
		Hypono,
		// Token: 0x04000333 RID: 819
		ScaredyShroom,
		// Token: 0x04000334 RID: 820
		IceShroom,
		// Token: 0x04000335 RID: 821
		DoomShroom,
		// Token: 0x04000336 RID: 822
		LilyPad,
		// Token: 0x04000337 RID: 823
		Squash,
		// Token: 0x04000338 RID: 824
		ThreePeater,
		// Token: 0x04000339 RID: 825
		Tanglekelp,
		// Token: 0x0400033A RID: 826
		Jalapeno,
		// Token: 0x0400033B RID: 827
		Caltrop,
		// Token: 0x0400033C RID: 828
		TorchWood,
		// Token: 0x0400033D RID: 829
		TallNut,
		// Token: 0x0400033E RID: 830
		GloomShroom,
		// Token: 0x0400033F RID: 831
		SpikeRock,
		// Token: 0x04000340 RID: 832
		Cattail
	}
}

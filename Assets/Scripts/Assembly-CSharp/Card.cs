using System;
using UnityEngine;

// Token: 0x0200004E RID: 78
public class Card : MonoBehaviour
{
	// Token: 0x0600016A RID: 362 RVA: 0x0000B578 File Offset: 0x00009778
	private void Start()
	{
		if (GameAPP.developerMode)
		{
			return;
		}
		switch (this.unlockLevel)
		{
		case Card.Unlock.CattailGirl:
			if (GameAPP.survivalLevelCompleted[8])
			{
				this.avaliable = true;
			}
			break;
		case Card.Unlock.Wheat:
			if (GameAPP.gameLevelCompleted[1])
			{
				this.avaliable = true;
			}
			break;
		case Card.Unlock.EndoFlame:
			if (GameAPP.clgLevelCompleted[17])
			{
				this.avaliable = true;
			}
			break;
		}
		if (this.unlockLevel >= Card.Unlock.Unlocked && (this.unlockLevel == Card.Unlock.Unlocked || GameAPP.advLevelCompleted[(int)this.unlockLevel]))
		{
			this.avaliable = true;
		}
		if (this.avaliable)
		{
			return;
		}
		foreach (object obj in base.transform)
		{
			((Transform)obj).gameObject.SetActive(false);
		}
	}

	// Token: 0x04000102 RID: 258
	public Card.Unlock unlockLevel;

	// Token: 0x04000103 RID: 259
	public bool isNormalCard = true;

	// Token: 0x04000104 RID: 260
	private bool avaliable;

	// Token: 0x02000129 RID: 297
	public enum Unlock
	{
		// Token: 0x04000342 RID: 834
		CattailGirl = -3,
		// Token: 0x04000343 RID: 835
		Wheat,
		// Token: 0x04000344 RID: 836
		EndoFlame,
		// Token: 0x04000345 RID: 837
		Unlocked,
		// Token: 0x04000346 RID: 838
		Advantrue1,
		// Token: 0x04000347 RID: 839
		Advantrue2,
		// Token: 0x04000348 RID: 840
		Advantrue3,
		// Token: 0x04000349 RID: 841
		Advantrue4,
		// Token: 0x0400034A RID: 842
		Advantrue5,
		// Token: 0x0400034B RID: 843
		Advantrue6,
		// Token: 0x0400034C RID: 844
		Advantrue7,
		// Token: 0x0400034D RID: 845
		Advantrue8,
		// Token: 0x0400034E RID: 846
		Advantrue9,
		// Token: 0x0400034F RID: 847
		Advantrue10,
		// Token: 0x04000350 RID: 848
		Advantrue11,
		// Token: 0x04000351 RID: 849
		Advantrue12,
		// Token: 0x04000352 RID: 850
		Advantrue13,
		// Token: 0x04000353 RID: 851
		Advantrue14,
		// Token: 0x04000354 RID: 852
		Advantrue15,
		// Token: 0x04000355 RID: 853
		Advantrue16,
		// Token: 0x04000356 RID: 854
		Advantrue17,
		// Token: 0x04000357 RID: 855
		Advantrue18,
		// Token: 0x04000358 RID: 856
		Advantrue19,
		// Token: 0x04000359 RID: 857
		Advantrue20,
		// Token: 0x0400035A RID: 858
		Advantrue21,
		// Token: 0x0400035B RID: 859
		Advantrue22,
		// Token: 0x0400035C RID: 860
		Advantrue23,
		// Token: 0x0400035D RID: 861
		Advantrue24,
		// Token: 0x0400035E RID: 862
		Advantrue25,
		// Token: 0x0400035F RID: 863
		Advantrue26,
		// Token: 0x04000360 RID: 864
		Advantrue27
	}
}

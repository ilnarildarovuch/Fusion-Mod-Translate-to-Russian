using System;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class CheckAdv : MonoBehaviour
{
	// Token: 0x0600016C RID: 364 RVA: 0x0000B670 File Offset: 0x00009870
	private void Start()
	{
		this.theLevel = base.transform.GetChild(1).GetComponent<Advanture_Btn>().buttonNumber;
		if (this.theLevel > 1 && !GameAPP.developerMode && !GameAPP.advLevelCompleted[this.theLevel - 1])
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x04000105 RID: 261
	private int theLevel;
}

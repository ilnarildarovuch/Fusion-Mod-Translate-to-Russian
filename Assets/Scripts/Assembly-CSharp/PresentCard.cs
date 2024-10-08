using System;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public class PresentCard : MonoBehaviour
{
	// Token: 0x060003A8 RID: 936 RVA: 0x0001C4D8 File Offset: 0x0001A6D8
	private void Start()
	{
		if (GameAPP.theBoardType != 1)
		{
			base.gameObject.SetActive(false);
			return;
		}
		int theBoardLevel = GameAPP.theBoardLevel;
		if (theBoardLevel == 3 || theBoardLevel == 15 || theBoardLevel == 17)
		{
			base.gameObject.SetActive(true);
			return;
		}
		base.gameObject.SetActive(false);
	}
}

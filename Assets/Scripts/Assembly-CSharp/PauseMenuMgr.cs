using System;
using UnityEngine;

// Token: 0x02000021 RID: 33
public class PauseMenuMgr : MonoBehaviour
{
	// Token: 0x06000092 RID: 146 RVA: 0x00004E48 File Offset: 0x00003048
	private void Awake()
	{
		PauseMenuMgr.Instance = this;
	}

	// Token: 0x04000077 RID: 119
	public static PauseMenuMgr Instance;

	// Token: 0x04000078 RID: 120
	public GameObject checkQuit;

	// Token: 0x04000079 RID: 121
	public GameObject checkRestart;

	// Token: 0x0400007A RID: 122
	public GameObject btnRestart;

	// Token: 0x0400007B RID: 123
	public GameObject btnQuit;

	// Token: 0x0400007C RID: 124
	public GameObject backToGame;

	// Token: 0x0400007D RID: 125
	public bool isRecheck;
}

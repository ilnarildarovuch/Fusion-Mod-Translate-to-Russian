using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class LevelName3 : MonoBehaviour
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
	private void Awake()
	{
		LevelName3.Instance = this;
	}

	// Token: 0x04000002 RID: 2
	public static LevelName3 Instance;
}

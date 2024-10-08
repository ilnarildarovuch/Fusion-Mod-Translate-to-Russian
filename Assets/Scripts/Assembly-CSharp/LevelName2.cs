using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class LevelName2 : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private void Awake()
	{
		LevelName2.Instance = this;
	}

	// Token: 0x04000001 RID: 1
	public static LevelName2 Instance;
}

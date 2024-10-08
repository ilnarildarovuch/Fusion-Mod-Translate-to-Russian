using System;
using UnityEngine;

// Token: 0x0200010B RID: 267
public class SlowTrigger : MonoBehaviour
{
	// Token: 0x0600055A RID: 1370 RVA: 0x0002E333 File Offset: 0x0002C533
	private void Awake()
	{
		SlowTrigger.Instance = this;
	}

	// Token: 0x040002A1 RID: 673
	public static SlowTrigger Instance;
}

using System;
using UnityEngine;

// Token: 0x020000E0 RID: 224
public class Charred : MonoBehaviour
{
	// Token: 0x06000417 RID: 1047 RVA: 0x0001FE99 File Offset: 0x0001E099
	public void Die()
	{
		Object.Destroy(base.gameObject, 0.2f);
	}
}

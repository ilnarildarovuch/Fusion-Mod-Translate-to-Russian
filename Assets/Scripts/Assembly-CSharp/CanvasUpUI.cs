using System;
using UnityEngine;

// Token: 0x020000F2 RID: 242
public class CanvasUpUI : MonoBehaviour
{
	// Token: 0x060004A7 RID: 1191 RVA: 0x000268CE File Offset: 0x00024ACE
	private void Start()
	{
		base.transform.SetParent(GameAPP.canvasUp.transform);
	}
}

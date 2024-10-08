using System;
using UnityEngine;

// Token: 0x0200001F RID: 31
public class MainMenu_Mgr : MonoBehaviour
{
	// Token: 0x0600008D RID: 141 RVA: 0x00004E0F File Offset: 0x0000300F
	private void FixedUpdate()
	{
		Camera.main.transform.position = new Vector3(0f, 0f, -200f);
	}
}

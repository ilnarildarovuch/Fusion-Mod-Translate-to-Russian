using System;
using UnityEngine;

// Token: 0x020000F9 RID: 249
public class InputMgr : MonoBehaviour
{
	// Token: 0x060004D5 RID: 1237 RVA: 0x00029D91 File Offset: 0x00027F91
	private void Update()
	{
		this.HandleInput();
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x00029D99 File Offset: 0x00027F99
	private void HandleInput()
	{
		Input.GetMouseButtonDown(0);
	}

	// Token: 0x0400025D RID: 605
	public int itemTypeOnHand = -1;

	// Token: 0x0400025E RID: 606
	public int theCardTypeOnHand = -1;

	// Token: 0x0400025F RID: 607
	public GameObject thePlantSelectByGlove;

	// Token: 0x04000260 RID: 608
	public GameObject thePlantSelectByShovel;
}

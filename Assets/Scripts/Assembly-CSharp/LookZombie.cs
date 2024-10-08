using System;
using UnityEngine;

// Token: 0x0200002C RID: 44
public class LookZombie : MonoBehaviour
{
	// Token: 0x060000C8 RID: 200 RVA: 0x00005E02 File Offset: 0x00004002
	private void Start()
	{
		this.originPosition = base.transform.position;
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00005E15 File Offset: 0x00004015
	private void OnMouseEnter()
	{
		CursorChange.SetClickCursor();
	}

	// Token: 0x060000CA RID: 202 RVA: 0x00005E1C File Offset: 0x0000401C
	private void OnMouseExit()
	{
		base.transform.position = this.originPosition;
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00005E34 File Offset: 0x00004034
	private void OnMouseDown()
	{
		GameAPP.PlaySound(28, 0.5f);
		base.transform.position = new Vector3(this.originPosition.x + 0.02f, this.originPosition.y - 0.02f);
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00005E74 File Offset: 0x00004074
	private void OnMouseUp()
	{
		CursorChange.SetDefaultCursor();
		base.transform.position = this.originPosition;
	}

	// Token: 0x040000A6 RID: 166
	private Vector3 originPosition;

	// Token: 0x040000A7 RID: 167
	private SpriteRenderer r;
}

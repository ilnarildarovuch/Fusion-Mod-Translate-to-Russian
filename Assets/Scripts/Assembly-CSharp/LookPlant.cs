using System;
using UnityEngine;

// Token: 0x0200002B RID: 43
public class LookPlant : MonoBehaviour
{
	// Token: 0x060000C2 RID: 194 RVA: 0x00005D28 File Offset: 0x00003F28
	private void Start()
	{
		this.originPosition = base.transform.position;
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00005D3B File Offset: 0x00003F3B
	private void OnMouseEnter()
	{
		base.transform.GetChild(0).gameObject.SetActive(true);
		CursorChange.SetClickCursor();
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x00005D59 File Offset: 0x00003F59
	private void OnMouseExit()
	{
		base.transform.position = this.originPosition;
		base.transform.GetChild(0).gameObject.SetActive(false);
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00005D88 File Offset: 0x00003F88
	private void OnMouseDown()
	{
		GameAPP.PlaySound(19, 0.5f);
		base.transform.position = new Vector3(this.originPosition.x + 0.02f, this.originPosition.y - 0.02f);
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x00005DC8 File Offset: 0x00003FC8
	private void OnMouseUp()
	{
		CursorChange.SetDefaultCursor();
		base.transform.position = this.originPosition;
		UIMgr.LookPlant();
		Object.Destroy(base.transform.parent.gameObject);
	}

	// Token: 0x040000A4 RID: 164
	private Vector3 originPosition;

	// Token: 0x040000A5 RID: 165
	private SpriteRenderer r;
}

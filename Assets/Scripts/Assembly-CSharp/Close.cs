using System;
using UnityEngine;

// Token: 0x0200002A RID: 42
public class Close : MonoBehaviour
{
	// Token: 0x060000BC RID: 188 RVA: 0x00005C0D File Offset: 0x00003E0D
	private void Start()
	{
		this.originPosition = base.transform.position;
		this.r = base.GetComponent<SpriteRenderer>();
		this.originSprite = this.r.sprite;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00005C3D File Offset: 0x00003E3D
	private void OnMouseEnter()
	{
		this.r.sprite = this.highLightSprite;
		CursorChange.SetClickCursor();
	}

	// Token: 0x060000BE RID: 190 RVA: 0x00005C55 File Offset: 0x00003E55
	private void OnMouseExit()
	{
		base.transform.position = this.originPosition;
		this.r.sprite = this.originSprite;
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x060000BF RID: 191 RVA: 0x00005C7E File Offset: 0x00003E7E
	private void OnMouseDown()
	{
		GameAPP.PlaySound(29, 0.5f);
		base.transform.position = new Vector3(this.originPosition.x + 0.02f, this.originPosition.y - 0.02f);
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x00005CC0 File Offset: 0x00003EC0
	private void OnMouseUp()
	{
		CursorChange.SetDefaultCursor();
		base.transform.position = this.originPosition;
		if (!this.CloseGroup)
		{
			Object.Destroy(base.transform.parent.gameObject);
			UIMgr.EnterMainMenu();
			return;
		}
		base.transform.parent.parent.GetComponent<AlmanacPlantCtrl>().ShowBasicCard();
	}

	// Token: 0x0400009F RID: 159
	public Sprite highLightSprite;

	// Token: 0x040000A0 RID: 160
	private Sprite originSprite;

	// Token: 0x040000A1 RID: 161
	private Vector3 originPosition;

	// Token: 0x040000A2 RID: 162
	private SpriteRenderer r;

	// Token: 0x040000A3 RID: 163
	public bool CloseGroup;
}

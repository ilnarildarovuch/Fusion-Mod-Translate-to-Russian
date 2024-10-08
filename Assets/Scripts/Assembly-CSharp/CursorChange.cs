using System;
using UnityEngine;

// Token: 0x020000F3 RID: 243
public class CursorChange : MonoBehaviour
{
	// Token: 0x060004A9 RID: 1193 RVA: 0x000268ED File Offset: 0x00024AED
	public static void SetDefaultCursor()
	{
		CursorChange.curDefault = Resources.Load<Texture2D>("Image/CursorDefault");
		Cursor.SetCursor(CursorChange.curDefault, Vector2.zero, CursorMode.Auto);
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x0002690E File Offset: 0x00024B0E
	public static void SetClickCursor()
	{
		CursorChange.curClick = Resources.Load<Texture2D>("Image/CursorClick");
		Cursor.SetCursor(CursorChange.curClick, new Vector2(5f, 0f), CursorMode.Auto);
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x00026939 File Offset: 0x00024B39
	private void OnMouseEnter()
	{
		CursorChange.SetClickCursor();
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x00026940 File Offset: 0x00024B40
	private void OnMouseExit()
	{
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x0400022F RID: 559
	private static Texture2D curDefault;

	// Token: 0x04000230 RID: 560
	private static Texture2D curClick;
}

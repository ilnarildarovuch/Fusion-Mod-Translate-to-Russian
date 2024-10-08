using UnityEngine;

public class CursorChange : MonoBehaviour
{
	private static Texture2D curDefault;

	private static Texture2D curClick;

	public static void SetDefaultCursor()
	{
		curDefault = Resources.Load<Texture2D>("Image/CursorDefault");
		Cursor.SetCursor(curDefault, Vector2.zero, CursorMode.Auto);
	}

	public static void SetClickCursor()
	{
		curClick = Resources.Load<Texture2D>("Image/CursorClick");
		Cursor.SetCursor(curClick, new Vector2(5f, 0f), CursorMode.Auto);
	}

	private void OnMouseEnter()
	{
		SetClickCursor();
	}

	private void OnMouseExit()
	{
		SetDefaultCursor();
	}
}

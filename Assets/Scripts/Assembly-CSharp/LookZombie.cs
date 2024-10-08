using UnityEngine;

public class LookZombie : MonoBehaviour
{
	private Vector3 originPosition;

	private SpriteRenderer r;

	private void Start()
	{
		originPosition = base.transform.position;
	}

	private void OnMouseEnter()
	{
		CursorChange.SetClickCursor();
	}

	private void OnMouseExit()
	{
		base.transform.position = originPosition;
		CursorChange.SetDefaultCursor();
	}

	private void OnMouseDown()
	{
		GameAPP.PlaySound(28);
		base.transform.position = new Vector3(originPosition.x + 0.02f, originPosition.y - 0.02f);
	}

	private void OnMouseUp()
	{
		CursorChange.SetDefaultCursor();
		base.transform.position = originPosition;
	}
}

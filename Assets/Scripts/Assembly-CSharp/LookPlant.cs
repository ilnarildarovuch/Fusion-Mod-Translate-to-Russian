using UnityEngine;

public class LookPlant : MonoBehaviour
{
	private Vector3 originPosition;

	private SpriteRenderer r;

	private void Start()
	{
		originPosition = base.transform.position;
	}

	private void OnMouseEnter()
	{
		base.transform.GetChild(0).gameObject.SetActive(value: true);
		CursorChange.SetClickCursor();
	}

	private void OnMouseExit()
	{
		base.transform.position = originPosition;
		base.transform.GetChild(0).gameObject.SetActive(value: false);
		CursorChange.SetDefaultCursor();
	}

	private void OnMouseDown()
	{
		GameAPP.PlaySound(19);
		base.transform.position = new Vector3(originPosition.x + 0.02f, originPosition.y - 0.02f);
	}

	private void OnMouseUp()
	{
		CursorChange.SetDefaultCursor();
		base.transform.position = originPosition;
		UIMgr.LookPlant();
		Object.Destroy(base.transform.parent.gameObject);
	}
}

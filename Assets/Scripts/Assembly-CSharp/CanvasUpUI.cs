using UnityEngine;

public class CanvasUpUI : MonoBehaviour
{
	private void Start()
	{
		base.transform.SetParent(GameAPP.canvasUp.transform);
	}
}

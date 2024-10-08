using UnityEngine;

public class MainMenu_Mgr : MonoBehaviour
{
	private void FixedUpdate()
	{
		Camera.main.transform.position = new Vector3(0f, 0f, -200f);
	}
}

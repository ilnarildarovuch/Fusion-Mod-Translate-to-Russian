using UnityEngine;

public class GameLose : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Zombie") && collision.GetComponent<Zombie>().theStatus != 1)
		{
			UIMgr.EnterLoseMenu();
		}
	}
}

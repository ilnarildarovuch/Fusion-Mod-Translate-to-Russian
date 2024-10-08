using UnityEngine;

public class IceExplodeEvent : MonoBehaviour
{
	private void Start()
	{
		GameObject[] plantArray = GameAPP.board.GetComponent<Board>().plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				if (component.thePlantType == 1039)
				{
					component.Recover(1000);
				}
			}
		}
	}
}

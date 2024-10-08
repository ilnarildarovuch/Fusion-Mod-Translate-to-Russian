using UnityEngine;

public class BrainMgr : MonoBehaviour
{
	public GameObject[] brains = new GameObject[6];

	private Board board;

	public int winRoad = -1;

	public int loseRoadNum;

	private void Start()
	{
		board = GameAPP.board.GetComponent<Board>();
	}

	private void Update()
	{
		if (loseRoadNum == board.roadNum - 1 && board.isAutoEve)
		{
			loseRoadNum = 0;
			Invoke("Restart", 3f);
		}
		GameObject[] array = brains;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null)
			{
				return;
			}
		}
		if (!board.isEveStarted)
		{
			Victory();
		}
		Object.Destroy(base.gameObject);
	}

	private void Victory()
	{
		GameObject gameObject = Resources.Load<GameObject>("Board/Award/TrophyPrefab");
		GameObject gameObject2 = Object.Instantiate(gameObject, board.transform);
		gameObject2.name = gameObject.name;
		gameObject2.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 1f, 0f);
		Vector2 vector = Camera.main.WorldToViewportPoint(gameObject2.transform.position);
		if (vector.x < 0.2f)
		{
			vector.x = 0.2f;
		}
		else if (vector.x > 0.8f)
		{
			vector.x = 0.8f;
		}
		if (vector.y < 0.2f)
		{
			vector.y = 0.2f;
		}
		else if (vector.y > 0.8f)
		{
			vector.y = 0.8f;
		}
		gameObject2.transform.position = Camera.main.ViewportToWorldPoint(vector);
		gameObject2.transform.position = new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y, 0f);
	}

	private void Restart()
	{
		foreach (Transform item in GameAPP.canvasUp.transform)
		{
			if (item != null)
			{
				Object.Destroy(item.gameObject);
			}
		}
		Object.Destroy(GameObject.Find("InGameUIIZE"));
		Object.Destroy(GameAPP.board);
		GameAPP.board = null;
		UIMgr.EVEAuto(winRoad);
	}
}

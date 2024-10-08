using System;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class BrainMgr : MonoBehaviour
{
	// Token: 0x060000F9 RID: 249 RVA: 0x0000849A File Offset: 0x0000669A
	private void Start()
	{
		this.board = GameAPP.board.GetComponent<Board>();
	}

	// Token: 0x060000FA RID: 250 RVA: 0x000084AC File Offset: 0x000066AC
	private void Update()
	{
		if (this.loseRoadNum == this.board.roadNum - 1 && this.board.isAutoEve)
		{
			this.loseRoadNum = 0;
			base.Invoke("Restart", 3f);
		}
		GameObject[] array = this.brains;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null)
			{
				return;
			}
		}
		if (!this.board.isEveStarted)
		{
			this.Victory();
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00008534 File Offset: 0x00006734
	private void Victory()
	{
		GameObject gameObject = Resources.Load<GameObject>("Board/Award/TrophyPrefab");
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, this.board.transform);
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

	// Token: 0x060000FC RID: 252 RVA: 0x00008678 File Offset: 0x00006878
	private void Restart()
	{
		foreach (object obj in GameAPP.canvasUp.transform)
		{
			Transform transform = (Transform)obj;
			if (transform != null)
			{
				Object.Destroy(transform.gameObject);
			}
		}
		Object.Destroy(GameObject.Find("InGameUIIZE"));
		Object.Destroy(GameAPP.board);
		GameAPP.board = null;
		UIMgr.EVEAuto(this.winRoad);
	}

	// Token: 0x040000DC RID: 220
	public GameObject[] brains = new GameObject[6];

	// Token: 0x040000DD RID: 221
	private Board board;

	// Token: 0x040000DE RID: 222
	public int winRoad = -1;

	// Token: 0x040000DF RID: 223
	public int loseRoadNum;
}

using System;
using UnityEngine;

// Token: 0x02000052 RID: 82
public class CreateCoin : MonoBehaviour
{
	// Token: 0x06000179 RID: 377 RVA: 0x0000BDD4 File Offset: 0x00009FD4
	private void Awake()
	{
		CreateCoin.Instance = this;
	}

	// Token: 0x0600017A RID: 378 RVA: 0x0000BDDC File Offset: 0x00009FDC
	public GameObject SetCoin(int theColumn, int theRow, int theCoinType, int theMoveType, Vector3 pos = default(Vector3))
	{
		float boxXFromColumn = Mouse.Instance.GetBoxXFromColumn(theColumn);
		float boxYFromRow = Mouse.Instance.GetBoxYFromRow(theRow);
		GameObject gameObject = GameAPP.coinPrefab[theCoinType];
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject);
		GameAPP.board.GetComponent<Board>().theTotalNumOfCoin++;
		Vector2 vector = new Vector2(boxXFromColumn, boxYFromRow);
		gameObject2.name = gameObject.name;
		gameObject2.transform.position = new Vector3(vector.x, vector.y + 1f, -0.1f);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		if (pos != default(Vector3))
		{
			gameObject2.transform.position = pos;
		}
		for (int i = 0; i < Board.Instance.coinArray.Length; i++)
		{
			if (Board.Instance.coinArray[i] == null)
			{
				Board.Instance.coinArray[i] = gameObject2;
				break;
			}
		}
		Coin coin = gameObject2.AddComponent<Coin>();
		coin.theCoinType = theCoinType;
		coin.theMoveType = theMoveType;
		this.SetLayer(gameObject2);
		return gameObject2;
	}

	// Token: 0x0600017B RID: 379 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
	private void SetLayer(GameObject coin)
	{
		int num = Board.Instance.theTotalNumOfCoin;
		if (num > 1000)
		{
			num %= 1000;
		}
		int num2 = 5 * num;
		foreach (object obj in coin.transform)
		{
			((Transform)obj).GetComponent<Renderer>().sortingOrder += num2;
		}
	}

	// Token: 0x04000113 RID: 275
	public static CreateCoin Instance;
}

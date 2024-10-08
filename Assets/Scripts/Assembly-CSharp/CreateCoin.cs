using UnityEngine;

public class CreateCoin : MonoBehaviour
{
	public static CreateCoin Instance;

	private void Awake()
	{
		Instance = this;
	}

	public GameObject SetCoin(int theColumn, int theRow, int theCoinType, int theMoveType, Vector3 pos = default(Vector3))
	{
		float boxXFromColumn = Mouse.Instance.GetBoxXFromColumn(theColumn);
		float boxYFromRow = Mouse.Instance.GetBoxYFromRow(theRow);
		GameObject gameObject = GameAPP.coinPrefab[theCoinType];
		GameObject gameObject2 = Object.Instantiate(gameObject);
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
		SetLayer(gameObject2);
		return gameObject2;
	}

	private void SetLayer(GameObject coin)
	{
		int num = Board.Instance.theTotalNumOfCoin;
		if (num > 1000)
		{
			num %= 1000;
		}
		int num2 = 5 * num;
		foreach (Transform item in coin.transform)
		{
			item.GetComponent<Renderer>().sortingOrder += num2;
		}
	}
}

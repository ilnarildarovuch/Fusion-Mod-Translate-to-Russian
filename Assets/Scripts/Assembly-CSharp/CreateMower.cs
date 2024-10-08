using UnityEngine;

public class CreateMower : MonoBehaviour
{
	public void SetMower(int[] roadtype)
	{
		for (int i = 0; i < roadtype.Length; i++)
		{
			SetMowerOnRoad(roadtype[i], i);
		}
	}

	private void SetMowerOnRoad(int rowtype, int row)
	{
		GameObject gameObject;
		switch (rowtype)
		{
		case -1:
			return;
		case 0:
			gameObject = Resources.Load<GameObject>("Mower/lawn/LawnMower");
			break;
		case 1:
			gameObject = Resources.Load<GameObject>("Mower/pool/PoolCleanerPrefab");
			break;
		case 2:
			gameObject = Resources.Load<GameObject>("Mower/lawn/LawnMower");
			break;
		default:
			gameObject = Resources.Load<GameObject>("Mower/lawn/LawnMower");
			break;
		}
		GameObject gameObject2 = Object.Instantiate(gameObject);
		gameObject2.name = gameObject.name;
		Mower mower = gameObject2.AddComponent<Mower>();
		mower.theMowerType = rowtype;
		mower.theMowerRow = row;
		for (int i = 0; i < GameAPP.board.GetComponent<Board>().mowerArray.Length; i++)
		{
			if (GameAPP.board.GetComponent<Board>().mowerArray[i] == null)
			{
				GameAPP.board.GetComponent<Board>().mowerArray[i] = gameObject2;
				break;
			}
		}
		SetTransform(gameObject2, row);
		SetLayer(gameObject2, row);
	}

	private void SetLayer(GameObject mower, int theRow)
	{
		foreach (Transform item in mower.transform)
		{
			if (!(item.name == "Shadow"))
			{
				item.GetComponent<Renderer>().sortingLayerName = $"mower{theRow}";
			}
		}
	}

	private void SetTransform(GameObject theMower, int theRow)
	{
		float x = -6.6f;
		float y = ((Board.Instance.roadNum != 5) ? (2.2f - 1.45f * (float)theRow) : (1.9f - 1.7f * (float)theRow));
		theMower.transform.position = new Vector3(x, y, 0f);
		theMower.transform.SetParent(GameAPP.board.transform);
		theMower.transform.localPosition = new Vector3(3f, theMower.transform.localPosition.y);
	}
}

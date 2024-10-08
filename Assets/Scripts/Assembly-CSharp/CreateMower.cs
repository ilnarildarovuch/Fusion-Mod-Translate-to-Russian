using System;
using UnityEngine;

// Token: 0x02000053 RID: 83
public class CreateMower : MonoBehaviour
{
	// Token: 0x0600017D RID: 381 RVA: 0x0000BF84 File Offset: 0x0000A184
	public void SetMower(int[] roadtype)
	{
		for (int i = 0; i < roadtype.Length; i++)
		{
			this.SetMowerOnRoad(roadtype[i], i);
		}
	}

	// Token: 0x0600017E RID: 382 RVA: 0x0000BFAC File Offset: 0x0000A1AC
	private void SetMowerOnRoad(int rowtype, int row)
	{
		if (rowtype == -1)
		{
			return;
		}
		GameObject gameObject;
		switch (rowtype)
		{
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
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject);
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
		this.SetTransform(gameObject2, row);
		this.SetLayer(gameObject2, row);
	}

	// Token: 0x0600017F RID: 383 RVA: 0x0000C084 File Offset: 0x0000A284
	private void SetLayer(GameObject mower, int theRow)
	{
		foreach (object obj in mower.transform)
		{
			Transform transform = (Transform)obj;
			if (!(transform.name == "Shadow"))
			{
				transform.GetComponent<Renderer>().sortingLayerName = string.Format("mower{0}", theRow);
			}
		}
	}

	// Token: 0x06000180 RID: 384 RVA: 0x0000C104 File Offset: 0x0000A304
	private void SetTransform(GameObject theMower, int theRow)
	{
		float x = -6.6f;
		float y;
		if (Board.Instance.roadNum == 5)
		{
			y = 1.9f - 1.7f * (float)theRow;
		}
		else
		{
			y = 2.2f - 1.45f * (float)theRow;
		}
		theMower.transform.position = new Vector3(x, y, 0f);
		theMower.transform.SetParent(GameAPP.board.transform);
		theMower.transform.localPosition = new Vector3(3f, theMower.transform.localPosition.y);
	}
}

using UnityEngine;

public class GridItem : MonoBehaviour
{
	private float existTime;

	public int itemType;

	public int theItemRow;

	public int theItemColumn;

	private Sprite crater;

	public Sprite crater_fading;

	private SpriteRenderer r;

	public Board board;

	private void Start()
	{
		r = GetComponent<SpriteRenderer>();
		crater = r.sprite;
	}

	private void Update()
	{
		existTime += Time.deltaTime;
		if (itemType == 0)
		{
			CraterUpdate();
		}
	}

	private void CraterUpdate()
	{
		if (board.isIZ)
		{
			Die();
		}
		if (existTime > 90f)
		{
			r.sprite = crater_fading;
			if (existTime > 180f)
			{
				Die();
			}
		}
		else
		{
			r.sprite = crater;
		}
	}

	private void Die()
	{
		for (int i = 0; i < board.griditemArray.Length; i++)
		{
			if (board.griditemArray[i] == base.gameObject)
			{
				board.griditemArray[i] = null;
			}
		}
		Object.Destroy(base.gameObject);
	}

	public static void CreateGridItem(int theColumn, int theRow, int theType)
	{
		Board component = GameAPP.board.GetComponent<Board>();
		float boxXFromColumn = GameAPP.board.GetComponent<Mouse>().GetBoxXFromColumn(theColumn);
		GameObject gameObject = Object.Instantiate(position: (component.roadNum != 5) ? new Vector2(boxXFromColumn, 2.5f - 1.4f * (float)theRow) : new Vector2(boxXFromColumn, 2.5f - 1.65f * (float)theRow), original: GameAPP.gridItemPrefab[theType], rotation: Quaternion.identity, parent: component.transform);
		GridItem component2 = gameObject.GetComponent<GridItem>();
		component2.board = component;
		component2.theItemColumn = theColumn;
		component2.theItemRow = theRow;
		for (int i = 0; i < component.griditemArray.Length; i++)
		{
			if (component.griditemArray[i] == null)
			{
				component.griditemArray[i] = gameObject;
				break;
			}
		}
	}
}

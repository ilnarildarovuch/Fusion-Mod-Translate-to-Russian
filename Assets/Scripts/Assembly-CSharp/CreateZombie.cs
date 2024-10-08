using UnityEngine;

public class CreateZombie : MonoBehaviour
{
	public static CreateZombie Instance;

	private void Awake()
	{
		Instance = this;
	}

	public GameObject SetZombie(int theX, int theRow, int theZombieType, float fX = 0f, bool isIdle = false)
	{
		if (!Board.Instance.isEveStarted && !isIdle)
		{
			if (Board.Instance.roadType[theRow] == 1)
			{
				switch (theZombieType)
				{
				case 0:
					theZombieType = 11;
					break;
				case 2:
					theZombieType = 12;
					break;
				case 4:
					theZombieType = 13;
					break;
				default:
					Debug.LogWarning("尝试在水路放置错误的僵尸类型");
					return null;
				case 14:
				case 17:
				case 19:
				case 200:
					break;
				}
			}
			if (Board.Instance.roadType[theRow] == 0)
			{
				switch (theZombieType)
				{
				case 14:
				case 17:
				case 19:
				case 200:
					Debug.LogWarning("尝试在陆地放置错误的僵尸类型");
					return null;
				}
			}
		}
		if (theRow < 0 || theRow > Board.Instance.roadNum - 1)
		{
			Debug.LogWarning("尝试地图外面放置僵尸");
		}
		float boxYFromRow = GetComponent<Mouse>().GetBoxYFromRow(theRow);
		Vector3 vector = new Vector3(theX, boxYFromRow);
		GameObject gameObject = Object.Instantiate(GameAPP.zombiePrefab[theZombieType], new Vector3(11f, 0f, 0f), Quaternion.identity);
		gameObject.name = GameAPP.zombiePrefab[theZombieType].name;
		if (theX == 0)
		{
			vector = new Vector3(9.9f, vector.y);
		}
		vector = ((fX == 0f) ? new Vector3(vector.x, vector.y + 0.1f) : new Vector3(fX, vector.y + 0.1f));
		SetTransform(gameObject, vector);
		gameObject.transform.Translate(0f, 0f, 1f);
		if (!isIdle)
		{
			Board.Instance.theCurrentNumOfZombieUncontroled++;
			Board.Instance.theTotalNumOfZombie++;
			SetLayer(theRow, gameObject);
			int num = Board.Instance.zombieArray.FindIndex((GameObject obj) => obj == null);
			if (num != -1)
			{
				Board.Instance.zombieArray[num] = gameObject;
			}
			else
			{
				Board.Instance.zombieArray.Add(gameObject);
			}
		}
		Zombie component = gameObject.GetComponent<Zombie>();
		component.theZombieRow = theRow;
		component.theOriginSpeed = Random.Range(0.9f, 1.8f);
		component.board = Board.Instance;
		component.theZombieType = theZombieType;
		if (!isIdle)
		{
			switch (theZombieType)
			{
			case 0:
			case 2:
			case 4:
			case 8:
			case 105:
			case 110:
				if (component.theOriginSpeed > 1.35f)
				{
					component.anim.Play("walk2");
				}
				else
				{
					component.anim.Play("walk");
				}
				break;
			default:
				if (component.anim.HasState(0, Animator.StringToHash("walk")))
				{
					component.anim.Play("walk");
				}
				break;
			}
		}
		return gameObject;
	}

	public void SetLayer(int theRow, GameObject theZombie)
	{
		int num = Board.Instance.theTotalNumOfZombie;
		if (num > 1000)
		{
			num %= 1000;
		}
		int baseLayer = num * 40;
		theZombie.GetComponent<Zombie>().baseLayer = baseLayer;
		StartSetLayer(theZombie, baseLayer, theRow);
	}

	private void StartSetLayer(GameObject obj, int baseLayer, int theRow)
	{
		if (obj.name == "Shadow")
		{
			return;
		}
		if (obj.TryGetComponent<SpriteRenderer>(out var component))
		{
			component.sortingOrder += baseLayer;
			component.sortingLayerName = $"zombie{theRow}";
		}
		if (obj.transform.childCount == 0)
		{
			return;
		}
		foreach (Transform item in obj.transform)
		{
			StartSetLayer(item.gameObject, baseLayer, theRow);
		}
	}

	private void SetTransform(GameObject zombie, Vector3 position)
	{
		foreach (Transform item in zombie.transform)
		{
			if (item.name == "Shadow")
			{
				Vector3 position2 = item.position;
				Vector3 vector = position - position2;
				zombie.transform.position += vector;
			}
		}
		zombie.transform.SetParent(GameAPP.board.transform);
	}
}

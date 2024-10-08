using System;
using UnityEngine;

// Token: 0x02000055 RID: 85
public class CreateZombie : MonoBehaviour
{
	// Token: 0x0600019E RID: 414 RVA: 0x0000DC27 File Offset: 0x0000BE27
	private void Awake()
	{
		CreateZombie.Instance = this;
	}

	// Token: 0x0600019F RID: 415 RVA: 0x0000DC30 File Offset: 0x0000BE30
	public GameObject SetZombie(int theX, int theRow, int theZombieType, float fX = 0f, bool isIdle = false)
	{
		if (!Board.Instance.isEveStarted && !isIdle)
		{
			if (Board.Instance.roadType[theRow] == 1)
			{
				if (theZombieType <= 14)
				{
					switch (theZombieType)
					{
					case 0:
						theZombieType = 11;
						goto IL_7D;
					case 1:
					case 3:
						break;
					case 2:
						theZombieType = 12;
						goto IL_7D;
					case 4:
						theZombieType = 13;
						goto IL_7D;
					default:
						if (theZombieType == 14)
						{
							goto IL_7D;
						}
						break;
					}
				}
				else if (theZombieType == 17 || theZombieType == 19 || theZombieType == 200)
				{
					goto IL_7D;
				}
				Debug.LogWarning("尝试在水路放置错误的僵尸类型");
				return null;
			}
			IL_7D:
			if (Board.Instance.roadType[theRow] == 0)
			{
				if (theZombieType <= 17)
				{
					if (theZombieType != 14 && theZombieType != 17)
					{
						goto IL_B5;
					}
				}
				else if (theZombieType != 19 && theZombieType != 200)
				{
					goto IL_B5;
				}
				Debug.LogWarning("尝试在陆地放置错误的僵尸类型");
				return null;
			}
		}
		IL_B5:
		if (theRow < 0 || theRow > Board.Instance.roadNum - 1)
		{
			Debug.LogWarning("尝试地图外面放置僵尸");
		}
		float boxYFromRow = base.GetComponent<Mouse>().GetBoxYFromRow(theRow);
		Vector3 vector = new Vector3((float)theX, boxYFromRow);
		GameObject gameObject = Object.Instantiate<GameObject>(GameAPP.zombiePrefab[theZombieType], new Vector3(11f, 0f, 0f), Quaternion.identity);
		gameObject.name = GameAPP.zombiePrefab[theZombieType].name;
		if (theX == 0)
		{
			vector = new Vector3(9.9f, vector.y);
		}
		if (fX != 0f)
		{
			vector = new Vector3(fX, vector.y + 0.1f);
		}
		else
		{
			vector = new Vector3(vector.x, vector.y + 0.1f);
		}
		this.SetTransform(gameObject, vector);
		gameObject.transform.Translate(0f, 0f, 1f);
		if (!isIdle)
		{
			Board.Instance.theCurrentNumOfZombieUncontroled++;
			Board.Instance.theTotalNumOfZombie++;
			this.SetLayer(theRow, gameObject);
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
			if (theZombieType <= 8)
			{
				switch (theZombieType)
				{
				case 0:
				case 2:
				case 4:
					break;
				case 1:
				case 3:
					goto IL_2B8;
				default:
					if (theZombieType != 8)
					{
						goto IL_2B8;
					}
					break;
				}
			}
			else if (theZombieType != 105 && theZombieType != 110)
			{
				goto IL_2B8;
			}
			if (component.theOriginSpeed > 1.35f)
			{
				component.anim.Play("walk2");
				return gameObject;
			}
			component.anim.Play("walk");
			return gameObject;
			IL_2B8:
			if (component.anim.HasState(0, Animator.StringToHash("walk")))
			{
				component.anim.Play("walk");
			}
		}
		return gameObject;
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x0000DF20 File Offset: 0x0000C120
	public void SetLayer(int theRow, GameObject theZombie)
	{
		int num = Board.Instance.theTotalNumOfZombie;
		if (num > 1000)
		{
			num %= 1000;
		}
		int baseLayer = num * 40;
		theZombie.GetComponent<Zombie>().baseLayer = baseLayer;
		this.StartSetLayer(theZombie, baseLayer, theRow);
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x0000DF64 File Offset: 0x0000C164
	private void StartSetLayer(GameObject obj, int baseLayer, int theRow)
	{
		if (obj.name == "Shadow")
		{
			return;
		}
		SpriteRenderer spriteRenderer;
		if (obj.TryGetComponent<SpriteRenderer>(out spriteRenderer))
		{
			spriteRenderer.sortingOrder += baseLayer;
			spriteRenderer.sortingLayerName = string.Format("zombie{0}", theRow);
		}
		if (obj.transform.childCount != 0)
		{
			foreach (object obj2 in obj.transform)
			{
				Transform transform = (Transform)obj2;
				this.StartSetLayer(transform.gameObject, baseLayer, theRow);
			}
		}
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x0000E014 File Offset: 0x0000C214
	private void SetTransform(GameObject zombie, Vector3 position)
	{
		foreach (object obj in zombie.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.name == "Shadow")
			{
				Vector3 position2 = transform.position;
				Vector3 b = position - position2;
				zombie.transform.position += b;
			}
		}
		zombie.transform.SetParent(GameAPP.board.transform);
	}

	// Token: 0x04000116 RID: 278
	public static CreateZombie Instance;
}

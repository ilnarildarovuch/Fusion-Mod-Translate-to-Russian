using System.Collections;
using UnityEngine;

public class IZEBrains : MonoBehaviour
{
	public int theRow;

	public int theHealth = 300;

	private Board board;

	private BrainMgr brainMgr;

	private void Start()
	{
		board = GameAPP.board.GetComponent<Board>();
		brainMgr = base.transform.parent.GetComponent<BrainMgr>();
	}

	private void Update()
	{
		if (theHealth > 0)
		{
			return;
		}
		if (board.isEveStarted)
		{
			if (brainMgr.loseRoadNum < board.roadNum - 2)
			{
				board.disAllowSetZombie[theRow] = true;
				brainMgr.loseRoadNum++;
				foreach (GameObject item in board.zombieArray)
				{
					if (item != null)
					{
						Zombie component = item.GetComponent<Zombie>();
						if (component.theZombieRow == theRow)
						{
							Vector2 vector = component.shadow.transform.position;
							Object.Instantiate(GameAPP.particlePrefab[11], new Vector3(vector.x, vector.y + 1f, 0f), Quaternion.identity, GameAPP.board.transform);
							component.Die(2);
						}
					}
				}
				GameObject[] plantArray = board.plantArray;
				foreach (GameObject gameObject in plantArray)
				{
					if (gameObject != null)
					{
						Plant component2 = gameObject.GetComponent<Plant>();
						if (component2.thePlantRow == theRow)
						{
							Vector2 vector2 = component2.shadow.transform.position;
							Object.Instantiate(GameAPP.particlePrefab[11], new Vector3(vector2.x, vector2.y + 0.5f, 0f), Quaternion.identity, GameAPP.board.transform);
							component2.Die();
						}
					}
				}
			}
			else
			{
				board.disAllowSetZombie[theRow] = true;
				for (int j = 0; j < board.disAllowSetZombie.Length; j++)
				{
					if (!board.disAllowSetZombie[j])
					{
						brainMgr.winRoad = j;
						board.disAllowSetZombie[j] = true;
					}
				}
				foreach (GameObject item2 in board.zombieArray)
				{
					if (item2 != null)
					{
						Zombie component3 = item2.GetComponent<Zombie>();
						Vector2 vector3 = component3.shadow.transform.position;
						Object.Instantiate(GameAPP.particlePrefab[11], new Vector3(vector3.x, vector3.y + 1f, 0f), Quaternion.identity, GameAPP.board.transform);
						component3.Die(2);
					}
				}
				brainMgr.loseRoadNum++;
			}
		}
		Object.Destroy(base.gameObject);
	}

	public void FlashOnce()
	{
		SpriteRenderer component = GetComponent<SpriteRenderer>();
		StartCoroutine(FlashObject(component.material));
	}

	private IEnumerator FlashObject(Material mt)
	{
		for (float j = 1f; j < 4f; j += 1f)
		{
			mt.SetFloat("_Brightness", j);
			yield return new WaitForFixedUpdate();
		}
		for (float j = 4f; j > 0.75f; j -= 0.25f)
		{
			mt.SetFloat("_Brightness", j);
			yield return new WaitForFixedUpdate();
		}
	}
}

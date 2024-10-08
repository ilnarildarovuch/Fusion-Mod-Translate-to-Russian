using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000061 RID: 97
public class IZEBrains : MonoBehaviour
{
	// Token: 0x060001E1 RID: 481 RVA: 0x0000F88C File Offset: 0x0000DA8C
	private void Start()
	{
		this.board = GameAPP.board.GetComponent<Board>();
		this.brainMgr = base.transform.parent.GetComponent<BrainMgr>();
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0000F8B4 File Offset: 0x0000DAB4
	private void Update()
	{
		if (this.theHealth <= 0)
		{
			if (this.board.isEveStarted)
			{
				if (this.brainMgr.loseRoadNum < this.board.roadNum - 2)
				{
					this.board.disAllowSetZombie[this.theRow] = true;
					this.brainMgr.loseRoadNum++;
					foreach (GameObject gameObject in this.board.zombieArray)
					{
						if (gameObject != null)
						{
							Zombie component = gameObject.GetComponent<Zombie>();
							if (component.theZombieRow == this.theRow)
							{
								Vector2 vector = component.shadow.transform.position;
								Object.Instantiate<GameObject>(GameAPP.particlePrefab[11], new Vector3(vector.x, vector.y + 1f, 0f), Quaternion.identity, GameAPP.board.transform);
								component.Die(2);
							}
						}
					}
					foreach (GameObject gameObject2 in this.board.plantArray)
					{
						if (gameObject2 != null)
						{
							Plant component2 = gameObject2.GetComponent<Plant>();
							if (component2.thePlantRow == this.theRow)
							{
								Vector2 vector2 = component2.shadow.transform.position;
								Object.Instantiate<GameObject>(GameAPP.particlePrefab[11], new Vector3(vector2.x, vector2.y + 0.5f, 0f), Quaternion.identity, GameAPP.board.transform);
								component2.Die();
							}
						}
					}
				}
				else
				{
					this.board.disAllowSetZombie[this.theRow] = true;
					for (int j = 0; j < this.board.disAllowSetZombie.Length; j++)
					{
						if (!this.board.disAllowSetZombie[j])
						{
							this.brainMgr.winRoad = j;
							this.board.disAllowSetZombie[j] = true;
						}
					}
					foreach (GameObject gameObject3 in this.board.zombieArray)
					{
						if (gameObject3 != null)
						{
							Zombie component3 = gameObject3.GetComponent<Zombie>();
							Vector2 vector3 = component3.shadow.transform.position;
							Object.Instantiate<GameObject>(GameAPP.particlePrefab[11], new Vector3(vector3.x, vector3.y + 1f, 0f), Quaternion.identity, GameAPP.board.transform);
							component3.Die(2);
						}
					}
					this.brainMgr.loseRoadNum++;
				}
			}
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x0000FBAC File Offset: 0x0000DDAC
	public void FlashOnce()
	{
		SpriteRenderer component = base.GetComponent<SpriteRenderer>();
		base.StartCoroutine(this.FlashObject(component.material));
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x0000FBD3 File Offset: 0x0000DDD3
	private IEnumerator FlashObject(Material mt)
	{
		for (float i = 1f; i < 4f; i += 1f)
		{
			mt.SetFloat("_Brightness", i);
			yield return new WaitForFixedUpdate();
		}
		for (float i = 4f; i > 0.75f; i -= 0.25f)
		{
			mt.SetFloat("_Brightness", i);
			yield return new WaitForFixedUpdate();
		}
		yield break;
	}

	// Token: 0x0400013A RID: 314
	public int theRow;

	// Token: 0x0400013B RID: 315
	public int theHealth = 300;

	// Token: 0x0400013C RID: 316
	private Board board;

	// Token: 0x0400013D RID: 317
	private BrainMgr brainMgr;
}

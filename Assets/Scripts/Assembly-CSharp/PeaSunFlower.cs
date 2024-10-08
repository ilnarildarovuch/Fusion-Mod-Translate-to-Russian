using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000076 RID: 118
public class PeaSunFlower : Shooter
{
	// Token: 0x0600026B RID: 619 RVA: 0x0001443C File Offset: 0x0001263C
	protected override void Update()
	{
		base.Update();
		if (!this.board.isIZ && GameAPP.theGameStatus == 0)
		{
			this.PeaSunProduceUpdate();
		}
	}

	// Token: 0x0600026C RID: 620 RVA: 0x00014460 File Offset: 0x00012660
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 8, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 100;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		return gameObject;
	}

	// Token: 0x0600026D RID: 621 RVA: 0x000144D4 File Offset: 0x000126D4
	private void PeaSunProduceUpdate()
	{
		this.thePlantProduceCountDown -= Time.deltaTime;
		if (this.thePlantProduceCountDown < 0f)
		{
			this.thePlantProduceCountDown = this.thePlantProduceInterval;
			this.thePlantProduceCountDown += (float)Random.Range(-2, 3);
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				if (!(transform.name == "Shadow"))
				{
					if (transform.childCount != 0)
					{
						using (IEnumerator enumerator2 = transform.transform.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								SpriteRenderer spriteRenderer;
								if (((Transform)enumerator2.Current).TryGetComponent<SpriteRenderer>(out spriteRenderer))
								{
									Material material = spriteRenderer.material;
									base.StartCoroutine(this.SunBright(material));
								}
							}
						}
					}
					SpriteRenderer spriteRenderer2;
					if (transform.TryGetComponent<SpriteRenderer>(out spriteRenderer2))
					{
						Material material2 = spriteRenderer2.material;
						base.StartCoroutine(this.SunBright(material2));
					}
				}
			}
			base.Invoke("ProduceSun", 0.5f);
		}
	}

	// Token: 0x0600026E RID: 622 RVA: 0x00014628 File Offset: 0x00012828
	private IEnumerator SunBright(Material mt)
	{
		for (float i = 1f; i < 4f; i += 0.1f)
		{
			mt.SetFloat("_Brightness", i);
			yield return new WaitForFixedUpdate();
		}
		for (float i = 4f; i > 1f; i -= 0.1f)
		{
			mt.SetFloat("_Brightness", i);
			yield return new WaitForFixedUpdate();
		}
		yield break;
	}

	// Token: 0x0600026F RID: 623 RVA: 0x00014638 File Offset: 0x00012838
	protected virtual void ProduceSun()
	{
		GameAPP.PlaySound(Random.Range(3, 5), 0.3f);
		this.board.GetComponent<CreateCoin>().SetCoin(this.thePlantColumn, this.thePlantRow, 0, 0, default(Vector3));
	}
}

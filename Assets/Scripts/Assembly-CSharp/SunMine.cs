using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200007B RID: 123
public class SunMine : PotatoMine
{
	// Token: 0x06000286 RID: 646 RVA: 0x00014F19 File Offset: 0x00013119
	protected override void Update()
	{
		base.Update();
		if (!this.board.isIZ && GameAPP.theGameStatus == 0)
		{
			this.ProducerUpdate();
		}
	}

	// Token: 0x06000287 RID: 647 RVA: 0x00014F3C File Offset: 0x0001313C
	public override void ProducerUpdate()
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

	// Token: 0x06000288 RID: 648 RVA: 0x00015090 File Offset: 0x00013290
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

	// Token: 0x06000289 RID: 649 RVA: 0x000150A0 File Offset: 0x000132A0
	protected virtual void ProduceSun()
	{
		GameAPP.PlaySound(Random.Range(3, 5), 0.3f);
		this.board.GetComponent<CreateCoin>().SetCoin(this.thePlantColumn, this.thePlantRow, 0, 0, default(Vector3));
	}

	// Token: 0x0600028A RID: 650 RVA: 0x000150E8 File Offset: 0x000132E8
	public override void Die()
	{
		if (!this.board.isIZ)
		{
			this.board.GetComponent<CreateCoin>().SetCoin(this.thePlantColumn, this.thePlantRow, 0, 0, default(Vector3));
			this.board.GetComponent<CreateCoin>().SetCoin(this.thePlantColumn, this.thePlantRow, 0, 0, default(Vector3));
		}
		base.Die();
	}
}

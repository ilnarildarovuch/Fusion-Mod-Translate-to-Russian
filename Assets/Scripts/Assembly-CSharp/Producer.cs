using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200009D RID: 157
public class Producer : Plant
{
	// Token: 0x0600031E RID: 798 RVA: 0x00018F78 File Offset: 0x00017178
	protected override void Update()
	{
		base.Update();
		if (!this.board.isIZ && GameAPP.theGameStatus == 0)
		{
			this.ProducerUpdate();
		}
	}

	// Token: 0x0600031F RID: 799 RVA: 0x00018F9C File Offset: 0x0001719C
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

	// Token: 0x06000320 RID: 800 RVA: 0x000190F0 File Offset: 0x000172F0
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

	// Token: 0x06000321 RID: 801 RVA: 0x00019100 File Offset: 0x00017300
	protected virtual void ProduceSun()
	{
		GameAPP.PlaySound(Random.Range(3, 5), 0.3f);
		CreateCoin.Instance.SetCoin(this.thePlantColumn, this.thePlantRow, 0, 0, default(Vector3));
	}

	// Token: 0x06000322 RID: 802 RVA: 0x00019140 File Offset: 0x00017340
	protected virtual void ProduceSunWithNoSound()
	{
		Random.Range(3, 5);
		CreateCoin.Instance.SetCoin(this.thePlantColumn, this.thePlantRow, 0, 0, default(Vector3));
	}
}

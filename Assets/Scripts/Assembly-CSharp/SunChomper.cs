using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200007A RID: 122
public class SunChomper : Chomper
{
	// Token: 0x06000281 RID: 641 RVA: 0x00014D1E File Offset: 0x00012F1E
	protected override void Swallow()
	{
		base.Swallow();
		if (!this.board.isIZ)
		{
			base.Invoke("Produce", 1.5f);
		}
	}

	// Token: 0x06000282 RID: 642 RVA: 0x00014D44 File Offset: 0x00012F44
	private void Produce()
	{
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

	// Token: 0x06000283 RID: 643 RVA: 0x00014E54 File Offset: 0x00013054
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

	// Token: 0x06000284 RID: 644 RVA: 0x00014E64 File Offset: 0x00013064
	private void ProduceSun()
	{
		this.board.GetComponent<CreateCoin>().SetCoin(this.thePlantColumn, this.thePlantRow, 0, 0, default(Vector3));
		this.board.GetComponent<CreateCoin>().SetCoin(this.thePlantColumn, this.thePlantRow, 0, 0, default(Vector3));
		this.board.GetComponent<CreateCoin>().SetCoin(this.thePlantColumn, this.thePlantRow, 0, 0, default(Vector3));
		this.board.GetComponent<CreateCoin>().SetCoin(this.thePlantColumn, this.thePlantRow, 0, 0, default(Vector3));
	}
}

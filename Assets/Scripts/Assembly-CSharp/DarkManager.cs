using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class DarkManager : MonoBehaviour
{
	// Token: 0x0600001D RID: 29 RVA: 0x0000259A File Offset: 0x0000079A
	private void Start()
	{
		this.r = base.GetComponent<SpriteRenderer>();
		if (!this.isEnter)
		{
			this.color.a = 0f;
			return;
		}
		this.color.a = 1f;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x000025D4 File Offset: 0x000007D4
	private void Update()
	{
		this.existTime += Time.deltaTime;
		if (this.isEnter)
		{
			this.color.a = this.color.a - Time.deltaTime;
			this.r.color = this.color;
		}
		else
		{
			this.color.a = this.color.a + 1.5f * Time.deltaTime;
			this.r.color = this.color;
		}
		if (this.existTime > 1f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000014 RID: 20
	public bool isEnter;

	// Token: 0x04000015 RID: 21
	private SpriteRenderer r;

	// Token: 0x04000016 RID: 22
	private Color color = Color.black;

	// Token: 0x04000017 RID: 23
	private float existTime;
}

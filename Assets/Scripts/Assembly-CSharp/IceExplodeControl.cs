using System;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class IceExplodeControl : MonoBehaviour
{
	// Token: 0x0600000B RID: 11 RVA: 0x0000219D File Offset: 0x0000039D
	private void Start()
	{
		this.r = base.GetComponent<SpriteRenderer>();
		this.color = this.r.color;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000021BC File Offset: 0x000003BC
	private void FixedUpdate()
	{
		this.color.a = this.color.a - 0.04f;
		this.r.color = this.color;
	}

	// Token: 0x04000008 RID: 8
	private SpriteRenderer r;

	// Token: 0x04000009 RID: 9
	private Color color;
}

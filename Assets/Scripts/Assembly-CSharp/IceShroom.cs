using System;
using UnityEngine;

// Token: 0x0200008B RID: 139
public class IceShroom : Plant
{
	// Token: 0x060002D2 RID: 722 RVA: 0x00016C48 File Offset: 0x00014E48
	protected override void Update()
	{
		base.Update();
		this.ExplodeCountDown -= Time.deltaTime;
		if (this.ExplodeCountDown < 0f)
		{
			this.board.CreateFreeze(this.shadow.transform.position);
			this.Die();
		}
	}

	// Token: 0x040001A3 RID: 419
	private float ExplodeCountDown = 1.5f;
}

using System;
using UnityEngine;

// Token: 0x020000A0 RID: 160
public class SunShroom : Producer
{
	// Token: 0x06000328 RID: 808 RVA: 0x00019193 File Offset: 0x00017393
	protected override void Update()
	{
		base.Update();
		if (!this.isGrowen)
		{
			this.growTime += Time.deltaTime;
			if (this.growTime > 120f)
			{
				this.Grow();
			}
		}
	}

	// Token: 0x06000329 RID: 809 RVA: 0x000191C8 File Offset: 0x000173C8
	protected override void ProduceSun()
	{
		if (this.isGrowen)
		{
			base.ProduceSun();
			return;
		}
		GameAPP.PlaySound(Random.Range(3, 5), 0.3f);
		this.board.GetComponent<CreateCoin>().SetCoin(this.thePlantColumn, this.thePlantRow, 2, 0, default(Vector3));
	}

	// Token: 0x0600032A RID: 810 RVA: 0x0001921D File Offset: 0x0001741D
	public void Grow()
	{
		this.isGrowen = true;
		this.anim.SetTrigger("grow");
		GameAPP.PlaySound(56, 0.5f);
		this.isShort = false;
	}

	// Token: 0x040001BC RID: 444
	private bool isGrowen;

	// Token: 0x040001BD RID: 445
	private float growTime;
}

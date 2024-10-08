using System;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class DoomShroom : Plant
{
	// Token: 0x0600020F RID: 527 RVA: 0x00010EBC File Offset: 0x0000F0BC
	protected override void Start()
	{
		base.Start();
		this.anim.Play("explode");
		GameAPP.PlaySound(39, 0.5f);
	}

	// Token: 0x06000210 RID: 528 RVA: 0x00010EE0 File Offset: 0x0000F0E0
	public virtual void AnimExplode()
	{
		Vector2 position = new Vector2(this.shadow.transform.position.x - 0.3f, this.shadow.transform.position.y + 0.3f);
		this.board.SetDoom(this.thePlantColumn, this.thePlantRow, true, position);
	}

	// Token: 0x06000211 RID: 529 RVA: 0x00010F43 File Offset: 0x0000F143
	protected override void Update()
	{
		base.Update();
	}
}

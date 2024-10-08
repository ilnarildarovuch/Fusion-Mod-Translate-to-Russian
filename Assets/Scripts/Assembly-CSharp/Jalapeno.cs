using System;

// Token: 0x0200006A RID: 106
public class Jalapeno : Plant
{
	// Token: 0x06000217 RID: 535 RVA: 0x000110FA File Offset: 0x0000F2FA
	protected override void Start()
	{
		base.Start();
		this.anim.Play("explode");
		GameAPP.PlaySound(39, 0.5f);
	}

	// Token: 0x06000218 RID: 536 RVA: 0x0001111E File Offset: 0x0000F31E
	public void AnimExplode()
	{
		this.board.CreateFireLine(this.thePlantRow);
		this.Die();
	}
}

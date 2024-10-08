using System;

// Token: 0x020000D2 RID: 210
public class ConeZombieDuck : ConeZombie
{
	// Token: 0x060003D0 RID: 976 RVA: 0x0001D2AC File Offset: 0x0001B4AC
	protected override void Start()
	{
		base.Start();
		if (GameAPP.theGameStatus == 0)
		{
			this.anim.Play("swim");
			this.anim.SetBool("inWater", true);
			this.inWater = true;
			base.SetMaskLayer();
		}
	}
}

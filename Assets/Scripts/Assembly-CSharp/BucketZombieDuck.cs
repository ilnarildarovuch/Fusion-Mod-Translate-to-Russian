using System;

// Token: 0x020000CF RID: 207
public class BucketZombieDuck : BucketZombie
{
	// Token: 0x060003C9 RID: 969 RVA: 0x0001D058 File Offset: 0x0001B258
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

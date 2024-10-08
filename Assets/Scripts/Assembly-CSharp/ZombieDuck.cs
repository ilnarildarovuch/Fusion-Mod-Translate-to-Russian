using System;

// Token: 0x020000E3 RID: 227
public class ZombieDuck : Zombie
{
	// Token: 0x06000421 RID: 1057 RVA: 0x000203C8 File Offset: 0x0001E5C8
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

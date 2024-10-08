using System;

// Token: 0x0200009C RID: 156
public class Wheat : Plant
{
	// Token: 0x0600031C RID: 796 RVA: 0x00018F61 File Offset: 0x00017161
	protected override void Start()
	{
		base.Start();
		this.isFromWheat = true;
	}
}

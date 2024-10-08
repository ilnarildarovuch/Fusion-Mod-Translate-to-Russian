using System;
using UnityEngine;

// Token: 0x020000A2 RID: 162
public class Shooter : Plant
{
	// Token: 0x0600032E RID: 814 RVA: 0x000192CC File Offset: 0x000174CC
	protected override void Update()
	{
		if (this.dreamTime > 0f)
		{
			this.dreamTime -= Time.deltaTime;
			if (this.dreamTime <= 0f)
			{
				this.dreamTime = 0f;
			}
		}
		if (GameAPP.theGameStatus == 0)
		{
			if (this.board.isScaredyDream)
			{
				if (this.thePlantType == 9)
				{
					this.PlantShootUpdate();
				}
			}
			else
			{
				this.PlantShootUpdate();
			}
		}
		base.Update();
	}

	// Token: 0x0600032F RID: 815 RVA: 0x00019342 File Offset: 0x00017542
	public virtual GameObject AnimShoot()
	{
		return null;
	}

	// Token: 0x040001BE RID: 446
	public float dreamTime = 0.1f;
}

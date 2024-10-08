using System;
using UnityEngine;

// Token: 0x0200008E RID: 142
public class JalaSquash : Squash
{
	// Token: 0x060002D9 RID: 729 RVA: 0x00016E8A File Offset: 0x0001508A
	protected override void Awake()
	{
		base.Awake();
		this.range = new Vector2(8f, 8f);
	}

	// Token: 0x060002DA RID: 730 RVA: 0x00016EA7 File Offset: 0x000150A7
	protected override void AttackZombie()
	{
		base.AttackZombie();
		this.board.CreateFireLine(this.thePlantRow);
	}
}

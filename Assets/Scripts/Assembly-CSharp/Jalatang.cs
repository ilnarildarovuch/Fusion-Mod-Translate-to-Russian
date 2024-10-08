using System;

// Token: 0x0200006B RID: 107
public class Jalatang : Tanglekelp
{
	// Token: 0x0600021A RID: 538 RVA: 0x0001113F File Offset: 0x0000F33F
	public override void Die()
	{
		this.board.CreateFireLine(this.thePlantRow);
		base.Die();
	}
}

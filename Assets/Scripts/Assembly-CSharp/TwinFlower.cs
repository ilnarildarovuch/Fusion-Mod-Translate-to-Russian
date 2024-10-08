using System;
using UnityEngine;

// Token: 0x020000A1 RID: 161
public class TwinFlower : SunFlower
{
	// Token: 0x0600032C RID: 812 RVA: 0x00019254 File Offset: 0x00017454
	protected override void ProduceSun()
	{
		GameAPP.PlaySound(Random.Range(3, 5), 0.3f);
		this.board.GetComponent<CreateCoin>().SetCoin(this.thePlantColumn, this.thePlantRow, 0, 0, default(Vector3));
		this.board.GetComponent<CreateCoin>().SetCoin(this.thePlantColumn, this.thePlantRow, 0, 0, default(Vector3));
	}
}

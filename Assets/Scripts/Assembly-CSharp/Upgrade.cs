using System;
using UnityEngine;

// Token: 0x020000FD RID: 253
public class Upgrade : Bucket
{
	// Token: 0x060004F9 RID: 1273 RVA: 0x0002B120 File Offset: 0x00029320
	public override void Use()
	{
		Vector2 vector = new Vector2((float)this.m.theMouseColumn, (float)this.m.theMouseRow);
		foreach (GameObject gameObject in GameAPP.board.GetComponent<Board>().plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				if ((float)component.thePlantColumn == vector.x && (float)component.thePlantRow == vector.y && component.thePlantType == 3)
				{
					component.Die();
					GameAPP.board.GetComponent<CreatePlant>().SetPlant(component.thePlantColumn, component.thePlantRow, 1027, null, default(Vector2), false, 0f);
					Object.Destroy(base.gameObject);
				}
			}
		}
	}
}

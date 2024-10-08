using System;
using UnityEngine;

// Token: 0x0200005A RID: 90
public class BucketPea : Bucket
{
	// Token: 0x060001B9 RID: 441 RVA: 0x0000EC08 File Offset: 0x0000CE08
	public override void Use()
	{
		Vector2 vector = new Vector2((float)this.m.theMouseColumn, (float)this.m.theMouseRow);
		foreach (GameObject gameObject in GameAPP.board.GetComponent<Board>().plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				if ((float)component.thePlantColumn == vector.x && (float)component.thePlantRow == vector.y && component.thePlantType == 0)
				{
					component.Die();
					GameAPP.board.GetComponent<CreatePlant>().SetPlant(component.thePlantColumn, component.thePlantRow, 1020, null, default(Vector2), false, 0f);
					Object.Destroy(base.gameObject);
				}
			}
		}
	}
}

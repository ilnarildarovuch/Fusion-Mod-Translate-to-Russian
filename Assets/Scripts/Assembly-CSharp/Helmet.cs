using System;
using UnityEngine;

// Token: 0x0200005B RID: 91
public class Helmet : Bucket
{
	// Token: 0x060001BB RID: 443 RVA: 0x0000ECE4 File Offset: 0x0000CEE4
	public override void Use()
	{
		Vector2 vector = new Vector2((float)this.m.theMouseColumn, (float)this.m.theMouseRow);
		foreach (GameObject gameObject in GameAPP.board.GetComponent<Board>().plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				if ((float)component.thePlantColumn == vector.x && (float)component.thePlantRow == vector.y)
				{
					int thePlantType = component.thePlantType;
					if (thePlantType != 1027)
					{
						if (thePlantType == 1028)
						{
							component.Recover(5000);
							Object.Destroy(base.gameObject);
						}
					}
					else
					{
						component.Die();
						GameAPP.board.GetComponent<CreatePlant>().SetPlant(component.thePlantColumn, component.thePlantRow, 1028, null, default(Vector2), false, 0f);
						Object.Destroy(base.gameObject);
					}
				}
			}
		}
		base.GetComponent<Collider2D>().enabled = true;
	}
}

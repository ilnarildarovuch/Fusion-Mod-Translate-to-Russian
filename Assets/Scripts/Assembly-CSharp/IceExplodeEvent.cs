using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
public class IceExplodeEvent : MonoBehaviour
{
	// Token: 0x060001B0 RID: 432 RVA: 0x0000E770 File Offset: 0x0000C970
	private void Start()
	{
		foreach (GameObject gameObject in GameAPP.board.GetComponent<Board>().plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				if (component.thePlantType == 1039)
				{
					component.Recover(1000);
				}
			}
		}
	}
}

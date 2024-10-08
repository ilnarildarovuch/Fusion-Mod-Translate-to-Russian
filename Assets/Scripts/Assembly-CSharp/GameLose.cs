using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class GameLose : MonoBehaviour
{
	// Token: 0x060001BD RID: 445 RVA: 0x0000EDFF File Offset: 0x0000CFFF
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Zombie") && collision.GetComponent<Zombie>().theStatus != 1)
		{
			UIMgr.EnterLoseMenu();
		}
	}
}

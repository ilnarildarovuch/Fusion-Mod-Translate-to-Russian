using System;
using TMPro;
using UnityEngine;

// Token: 0x0200000C RID: 12
public class EndlessMgr : MonoBehaviour
{
	// Token: 0x0600001B RID: 27 RVA: 0x0000253C File Offset: 0x0000073C
	private void Start()
	{
		if (PlantsInLevel.maxRound > 0)
		{
			base.transform.GetChild(2).gameObject.SetActive(true);
			base.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = string.Format("{0}轮", PlantsInLevel.maxRound);
		}
	}
}

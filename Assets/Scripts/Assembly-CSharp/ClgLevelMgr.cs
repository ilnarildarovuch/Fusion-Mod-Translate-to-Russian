using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
public class ClgLevelMgr : MonoBehaviour
{
	// Token: 0x06000019 RID: 25 RVA: 0x000024B8 File Offset: 0x000006B8
	public void ChangePage(int page)
	{
		this.currentPage = page;
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.GetSiblingIndex() == page)
			{
				transform.gameObject.SetActive(true);
			}
			else
			{
				transform.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x04000013 RID: 19
	public int currentPage;
}

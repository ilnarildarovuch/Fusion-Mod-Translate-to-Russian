using System;
using TMPro;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class UIDifficulty : MonoBehaviour
{
	// Token: 0x06000084 RID: 132 RVA: 0x00004B86 File Offset: 0x00002D86
	private void Start()
	{
		this.t = base.GetComponent<TextMeshProUGUI>();
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00004B94 File Offset: 0x00002D94
	private void Update()
	{
		this.t.text = string.Format("难度：{0}", GameAPP.difficulty);
	}

	// Token: 0x0400006F RID: 111
	private TextMeshProUGUI t;
}

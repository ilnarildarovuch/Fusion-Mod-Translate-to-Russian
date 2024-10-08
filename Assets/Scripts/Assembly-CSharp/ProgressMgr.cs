using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000018 RID: 24
public class ProgressMgr : MonoBehaviour
{
	// Token: 0x06000067 RID: 103 RVA: 0x000045F1 File Offset: 0x000027F1
	private void Awake()
	{
		ProgressMgr.slider = base.GetComponent<Slider>();
		ProgressMgr.bg = GameAPP.board.GetComponent<Board>();
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00004610 File Offset: 0x00002810
	private void Update()
	{
		float num = (float)ProgressMgr.bg.theWave;
		float num2 = (float)ProgressMgr.bg.theMaxWave;
		float num3 = num / num2;
		if (ProgressMgr.slider.value < num3)
		{
			ProgressMgr.slider.value += Time.deltaTime * 0.1f;
		}
	}

	// Token: 0x0400005D RID: 93
	public static Slider slider;

	// Token: 0x0400005E RID: 94
	public static Board bg;
}

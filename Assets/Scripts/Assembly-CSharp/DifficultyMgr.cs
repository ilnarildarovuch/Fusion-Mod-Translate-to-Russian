using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F6 RID: 246
public class DifficultyMgr : MonoBehaviour
{
	// Token: 0x060004B7 RID: 1207 RVA: 0x00026DAE File Offset: 0x00024FAE
	private void Start()
	{
		this.slider = base.GetComponent<Slider>();
		this.slider.value = (float)GameAPP.difficulty;
		this.t = base.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x00026DE4 File Offset: 0x00024FE4
	private void Update()
	{
		GameAPP.difficulty = (int)this.slider.value;
		float value = this.slider.value;
		if (value <= 2f)
		{
			if (value == 1f)
			{
				this.t.text = "简单模式";
				return;
			}
			if (value != 2f)
			{
				return;
			}
			this.t.text = "正常模式";
			return;
		}
		else
		{
			if (value == 3f)
			{
				this.t.text = "困难模式";
				return;
			}
			if (value == 4f)
			{
				this.t.text = "极难模式";
				return;
			}
			if (value != 5f)
			{
				return;
			}
			this.t.text = "你确定？";
			return;
		}
	}

	// Token: 0x04000234 RID: 564
	private Slider slider;

	// Token: 0x04000235 RID: 565
	private TextMeshProUGUI t;
}

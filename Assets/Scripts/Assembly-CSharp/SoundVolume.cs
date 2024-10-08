using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000025 RID: 37
public class SoundVolume : MonoBehaviour
{
	// Token: 0x060000A3 RID: 163 RVA: 0x000054CE File Offset: 0x000036CE
	private void Start()
	{
		this.slider = base.GetComponent<Slider>();
		this.slider.value = GameAPP.gameSoundVolume;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x000054EC File Offset: 0x000036EC
	private void Update()
	{
		GameAPP.gameSoundVolume = this.slider.value;
	}

	// Token: 0x0400008E RID: 142
	private Slider slider;
}

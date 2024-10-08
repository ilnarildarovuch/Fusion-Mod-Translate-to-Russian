using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000024 RID: 36
public class MusicVolume : MonoBehaviour
{
	// Token: 0x060000A0 RID: 160 RVA: 0x00005496 File Offset: 0x00003696
	private void Start()
	{
		this.slider = base.GetComponent<Slider>();
		this.slider.value = GameAPP.gameMusicVolume;
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x000054B4 File Offset: 0x000036B4
	private void Update()
	{
		GameAPP.gameMusicVolume = this.slider.value;
	}

	// Token: 0x0400008D RID: 141
	private Slider slider;
}

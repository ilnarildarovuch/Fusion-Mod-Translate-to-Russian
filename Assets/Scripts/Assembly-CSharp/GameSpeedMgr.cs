using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F8 RID: 248
public class GameSpeedMgr : MonoBehaviour
{
	// Token: 0x060004D2 RID: 1234 RVA: 0x00029D57 File Offset: 0x00027F57
	private void Start()
	{
		this.slider = base.GetComponent<Slider>();
		this.slider.value = GameAPP.gameSpeed;
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x00029D75 File Offset: 0x00027F75
	private void Update()
	{
		GameAPP.gameSpeed = (float)((int)this.slider.value);
	}

	// Token: 0x0400025C RID: 604
	private Slider slider;
}

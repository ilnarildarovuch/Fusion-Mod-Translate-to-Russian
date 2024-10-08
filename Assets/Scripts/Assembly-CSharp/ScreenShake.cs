using System;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public class ScreenShake : MonoBehaviour
{
	// Token: 0x060003B2 RID: 946 RVA: 0x0001C82E File Offset: 0x0001AA2E
	private void Start()
	{
		ScreenShake.originalPosition = base.transform.localPosition;
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x0001C840 File Offset: 0x0001AA40
	public void Update()
	{
		if (GameAPP.theGameStatus != 0)
		{
			return;
		}
		if (ScreenShake.shakeDuration > 0f)
		{
			base.transform.localPosition = ScreenShake.originalPosition + Random.insideUnitSphere * ScreenShake.shakeMagnitude;
			ScreenShake.shakeDuration -= Time.deltaTime * ScreenShake.dampingSpeed;
			return;
		}
		ScreenShake.shakeDuration = 0f;
		base.transform.localPosition = ScreenShake.originalPosition;
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x0001C8B6 File Offset: 0x0001AAB6
	public static void TriggerShake(float duration = 0.15f)
	{
		ScreenShake.shakeDuration = duration;
	}

	// Token: 0x040001D7 RID: 471
	private static Vector3 originalPosition;

	// Token: 0x040001D8 RID: 472
	private static float shakeDuration = 0f;

	// Token: 0x040001D9 RID: 473
	private static readonly float shakeMagnitude = 0.1f;

	// Token: 0x040001DA RID: 474
	private static readonly float dampingSpeed = 1f;
}

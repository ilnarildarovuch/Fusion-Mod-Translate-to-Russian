using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000FE RID: 254
public class OpenUrl : MonoBehaviour
{
	// Token: 0x060004FB RID: 1275 RVA: 0x0002B1FD File Offset: 0x000293FD
	private void OnMouseUp()
	{
		base.StartCoroutine(this.OpenURLCoroutine());
	}

	// Token: 0x060004FC RID: 1276 RVA: 0x0002B20C File Offset: 0x0002940C
	private IEnumerator OpenURLCoroutine()
	{
		yield return null;
		if (this.type == 0)
		{
			Application.OpenURL(this.url);
		}
		else
		{
			Application.OpenURL(this.url2);
		}
		yield break;
	}

	// Token: 0x04000278 RID: 632
	private readonly string url = "https://space.bilibili.com/3546619314178489";

	// Token: 0x04000279 RID: 633
	private readonly string url2 = "https://space.bilibili.com/85881762";

	// Token: 0x0400027A RID: 634
	public int type;
}

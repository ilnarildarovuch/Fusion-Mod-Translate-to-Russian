using System;
using UnityEngine;

// Token: 0x0200005E RID: 94
public class IceRoad : MonoBehaviour
{
	// Token: 0x060001C5 RID: 453 RVA: 0x0000EFFF File Offset: 0x0000D1FF
	private void Awake()
	{
		this.iceCap = base.transform.GetChild(0).gameObject;
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x0000F018 File Offset: 0x0000D218
	private void FixedUpdate()
	{
		this.iceCap.transform.position = new Vector3(Board.Instance.iceRoadX[this.theIceRoadRow], this.iceCap.transform.position.y);
	}

	// Token: 0x04000132 RID: 306
	public int theIceRoadRow;

	// Token: 0x04000133 RID: 307
	private GameObject iceCap;
}

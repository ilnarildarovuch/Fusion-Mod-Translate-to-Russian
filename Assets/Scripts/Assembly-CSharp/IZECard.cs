using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000015 RID: 21
public class IZECard : MonoBehaviour
{
	// Token: 0x06000054 RID: 84 RVA: 0x00004106 File Offset: 0x00002306
	private void Start()
	{
		if (this.theZombieType <= 20 && this.theZombieType >= 14 && GameAPP.theBoardLevel <= 12)
		{
			Object.Destroy(base.transform.parent.gameObject);
		}
	}

	// Token: 0x06000055 RID: 85 RVA: 0x0000413A File Offset: 0x0000233A
	private void OnMouseEnter()
	{
		if (GameAPP.board.GetComponent<Mouse>().theItemOnMouse == null)
		{
			CursorChange.SetClickCursor();
		}
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00004158 File Offset: 0x00002358
	private void OnMouseExit()
	{
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x06000057 RID: 87 RVA: 0x0000415F File Offset: 0x0000235F
	public void PickUp()
	{
		base.transform.GetChild(2).gameObject.SetActive(true);
		this.isPickUp = true;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x0000417F File Offset: 0x0000237F
	public void PutDown()
	{
		base.transform.GetChild(2).gameObject.SetActive(false);
		this.isPickUp = false;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x000041A0 File Offset: 0x000023A0
	private void Update()
	{
		if (GameAPP.board != null && GameAPP.board.GetComponent<Board>().theSun >= this.theZombieCost && !this.isPickUp)
		{
			base.transform.GetChild(2).gameObject.SetActive(false);
		}
		else
		{
			base.transform.GetChild(2).gameObject.SetActive(true);
		}
		base.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = this.theZombieCost.ToString();
	}

	// Token: 0x04000055 RID: 85
	public int theZombieType;

	// Token: 0x04000056 RID: 86
	public int theZombieCost = 100;

	// Token: 0x04000057 RID: 87
	private Slider slider;

	// Token: 0x04000058 RID: 88
	public bool isPickUp;
}

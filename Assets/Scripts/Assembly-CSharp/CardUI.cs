using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200000E RID: 14
public class CardUI : MonoBehaviour
{
	// Token: 0x06000020 RID: 32 RVA: 0x00002678 File Offset: 0x00000878
	private void Start()
	{
		this.slider = base.transform.GetChild(2).gameObject.GetComponent<Slider>();
		if (GameAPP.theBoardType == 1)
		{
			int theBoardLevel = GameAPP.theBoardLevel;
			if (theBoardLevel <= 17)
			{
				if (theBoardLevel == 15 || theBoardLevel == 17)
				{
					if (this.theSeedType != 256)
					{
						Object.Destroy(base.gameObject);
					}
					else
					{
						this.theSeedCost = 75;
					}
				}
			}
			else if (theBoardLevel - 25 > 1)
			{
				if (theBoardLevel == 35)
				{
					if (this.theSeedType == 1)
					{
						Object.Destroy(base.gameObject);
					}
					else if (this.theSeedType == 256)
					{
						Object.Destroy(base.gameObject);
					}
				}
			}
			else if (this.theSeedType == 9)
			{
				Object.Destroy(base.gameObject);
			}
		}
		if (!GameAPP.board.GetComponent<Board>().isNight)
		{
			int theBoardLevel = this.theSeedType;
			if (theBoardLevel - 6 <= 5)
			{
				this.theSeedCost += 75;
			}
		}
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002767 File Offset: 0x00000967
	private void OnMouseEnter()
	{
		if (GameAPP.board.GetComponent<Mouse>().theItemOnMouse == null)
		{
			CursorChange.SetClickCursor();
		}
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002785 File Offset: 0x00000985
	private void OnMouseExit()
	{
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x06000023 RID: 35 RVA: 0x0000278C File Offset: 0x0000098C
	private void OnMouseDown()
	{
		CursorChange.SetDefaultCursor();
		if (GameAPP.theGameStatus == 3)
		{
			GameAPP.PlaySound(19, 0.5f);
			if (!this.isSelected)
			{
				this.isSelected = true;
				this.thisUI.AddCardToBank(base.gameObject);
				return;
			}
			this.isSelected = false;
			this.thisUI.RemoveCardFromBank(base.gameObject);
		}
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000027EB File Offset: 0x000009EB
	public void PickUp()
	{
		base.transform.GetChild(3).gameObject.SetActive(true);
		this.isPickUp = true;
	}

	// Token: 0x06000025 RID: 37 RVA: 0x0000280B File Offset: 0x00000A0B
	public void PutDown()
	{
		base.transform.GetChild(3).gameObject.SetActive(false);
		this.isPickUp = false;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x0000282C File Offset: 0x00000A2C
	private void Update()
	{
		if (GameAPP.theGameStatus == 0)
		{
			if (this.CD < this.fullCD)
			{
				this.CD += Time.deltaTime;
				this.isAvailable = false;
			}
			else
			{
				this.CD = this.fullCD;
			}
			if (this.CD == this.fullCD && Board.Instance.theSun >= this.theSeedCost && !this.isPickUp)
			{
				this.isAvailable = true;
				base.transform.GetChild(3).gameObject.SetActive(false);
			}
			else
			{
				this.isAvailable = false;
				base.transform.GetChild(3).gameObject.SetActive(true);
			}
			this.CDUpdate();
		}
		if (Board.Instance.freeCD)
		{
			this.CD = this.fullCD;
			this.isAvailable = true;
		}
		base.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = this.theSeedCost.ToString();
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002924 File Offset: 0x00000B24
	private void CDUpdate()
	{
		this.slider.value = 1f - this.CD / this.fullCD;
	}

	// Token: 0x04000018 RID: 24
	public int theSeedType;

	// Token: 0x04000019 RID: 25
	public int theSeedCost = 100;

	// Token: 0x0400001A RID: 26
	public bool isSelected;

	// Token: 0x0400001B RID: 27
	public int theNumberInCardSort;

	// Token: 0x0400001C RID: 28
	public InGameUIMgr thisUI;

	// Token: 0x0400001D RID: 29
	public GameObject parent;

	// Token: 0x0400001E RID: 30
	public bool isAvailable = true;

	// Token: 0x0400001F RID: 31
	public float CD;

	// Token: 0x04000020 RID: 32
	public float fullCD = 7.5f;

	// Token: 0x04000021 RID: 33
	private Slider slider;

	// Token: 0x04000022 RID: 34
	public bool isPickUp;

	// Token: 0x04000023 RID: 35
	public bool isExtra;
}

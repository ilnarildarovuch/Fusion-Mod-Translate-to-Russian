using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200001A RID: 26
public class StartGameBtn : MonoBehaviour
{
	// Token: 0x06000071 RID: 113 RVA: 0x0000477C File Offset: 0x0000297C
	private void Start()
	{
		this.rectTransform = base.GetComponent<RectTransform>();
		this.originPosition = this.rectTransform.anchoredPosition;
		this.image = base.GetComponent<Image>();
		this.originSprite = this.image.sprite;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x000047C8 File Offset: 0x000029C8
	private void OnMouseEnter()
	{
		base.transform.GetChild(2).gameObject.SetActive(true);
		CursorChange.SetClickCursor();
	}

	// Token: 0x06000073 RID: 115 RVA: 0x000047E6 File Offset: 0x000029E6
	private void OnMouseExit()
	{
		this.rectTransform.anchoredPosition = this.originPosition;
		base.transform.GetChild(2).gameObject.SetActive(false);
		CursorChange.SetDefaultCursor();
	}

	// Token: 0x06000074 RID: 116 RVA: 0x0000481A File Offset: 0x00002A1A
	private void OnMouseDown()
	{
		GameAPP.PlaySound(19, 0.5f);
		this.rectTransform.anchoredPosition = new Vector2(this.originPosition.x + 1f, this.originPosition.y - 1f);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x0000485A File Offset: 0x00002A5A
	private void OnMouseUp()
	{
		CursorChange.SetDefaultCursor();
		this.rectTransform.anchoredPosition = this.originPosition;
		GameAPP.board.GetComponent<InitBoard>().RemoveUI();
	}

	// Token: 0x04000062 RID: 98
	public Sprite highLightSprite;

	// Token: 0x04000063 RID: 99
	private Sprite originSprite;

	// Token: 0x04000064 RID: 100
	private Image image;

	// Token: 0x04000065 RID: 101
	private Vector3 originPosition;

	// Token: 0x04000066 RID: 102
	private RectTransform rectTransform;
}

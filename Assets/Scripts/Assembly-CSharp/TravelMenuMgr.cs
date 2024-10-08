using System;
using TMPro;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class TravelMenuMgr : MonoBehaviour
{
	// Token: 0x06000081 RID: 129 RVA: 0x00004B26 File Offset: 0x00002D26
	private void Awake()
	{
		TravelMenuMgr.Instance = this;
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00004B30 File Offset: 0x00002D30
	public void ChangeText(int type)
	{
		string text;
		if (type == 1)
		{
			text = "你可以获得随机1种究极植物\r\n但你将面对更强大的僵尸";
			this.btn.choiceNumber = 1;
		}
		else
		{
			text = "数据异常";
			Debug.LogError(type);
		}
		this.textShadow.text = text;
		this.text.text = text;
	}

	// Token: 0x0400006B RID: 107
	public static TravelMenuMgr Instance;

	// Token: 0x0400006C RID: 108
	public TextMeshProUGUI textShadow;

	// Token: 0x0400006D RID: 109
	public TextMeshProUGUI text;

	// Token: 0x0400006E RID: 110
	public TravelMenuBtn btn;
}

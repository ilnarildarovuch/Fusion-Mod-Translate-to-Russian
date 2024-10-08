using System;
using TMPro;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class InGameText : MonoBehaviour
{
	// Token: 0x060001C8 RID: 456 RVA: 0x0000F05D File Offset: 0x0000D25D
	private void Awake()
	{
		InGameText.INSTANCE = this;
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x0000F065 File Offset: 0x0000D265
	private void Start()
	{
		this.t = base.GetComponent<TextMeshProUGUI>();
	}

	// Token: 0x060001CA RID: 458 RVA: 0x0000F074 File Offset: 0x0000D274
	private void Update()
	{
		if (this.existTime > 0f)
		{
			this.existTime -= Time.deltaTime;
			if (this.existTime <= 0f)
			{
				this.t.enabled = false;
				base.transform.GetChild(0).gameObject.SetActive(false);
				this.existTime = 0f;
			}
		}
		if (GameAPP.theGameStatus == 1)
		{
			this.t.enabled = false;
			base.transform.GetChild(0).gameObject.SetActive(false);
			this.existTime = 0f;
		}
	}

	// Token: 0x060001CB RID: 459 RVA: 0x0000F111 File Offset: 0x0000D311
	public void EnableText(string text, float time)
	{
		this.t.enabled = true;
		base.transform.GetChild(0).gameObject.SetActive(true);
		this.t.text = text;
		this.existTime = time;
	}

	// Token: 0x060001CC RID: 460 RVA: 0x0000F14C File Offset: 0x0000D34C
	public static GameObject Instance()
	{
		GameObject gameObject = Resources.Load<GameObject>("UI/InGameMenu/Tutor");
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, GameAPP.canvasUp.transform);
		gameObject2.name = gameObject.name;
		return gameObject2;
	}

	// Token: 0x04000134 RID: 308
	private float existTime;

	// Token: 0x04000135 RID: 309
	private TextMeshProUGUI t;

	// Token: 0x04000136 RID: 310
	public static InGameText INSTANCE;
}

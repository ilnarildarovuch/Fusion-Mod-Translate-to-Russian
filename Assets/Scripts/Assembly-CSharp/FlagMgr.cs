using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200000F RID: 15
public class FlagMgr : MonoBehaviour
{
	// Token: 0x06000029 RID: 41 RVA: 0x00002966 File Offset: 0x00000B66
	private void Start()
	{
		this.grid = base.GetComponent<GridLayoutGroup>();
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002974 File Offset: 0x00000B74
	private void Update()
	{
		this.FlagUpdate();
		if (!this.once && ProgressMgr.bg != null)
		{
			this.once = true;
			this.flag = ProgressMgr.bg.theMaxWave / 10;
			switch (this.flag)
			{
			case 1:
				this.grid.spacing = new Vector2(140f, 0f);
				Object.Destroy(base.transform.GetChild(0).gameObject);
				Object.Destroy(base.transform.GetChild(1).gameObject);
				Object.Destroy(base.transform.GetChild(2).gameObject);
				Object.Destroy(base.transform.GetChild(3).gameObject);
				Object.Destroy(base.transform.GetChild(4).gameObject);
				Object.Destroy(base.transform.GetChild(5).gameObject);
				Object.Destroy(base.transform.GetChild(6).gameObject);
				Object.Destroy(base.transform.GetChild(7).gameObject);
				Object.Destroy(base.transform.GetChild(8).gameObject);
				return;
			case 2:
				this.grid.spacing = new Vector2(70f, 0f);
				Object.Destroy(base.transform.GetChild(0).gameObject);
				Object.Destroy(base.transform.GetChild(1).gameObject);
				Object.Destroy(base.transform.GetChild(2).gameObject);
				Object.Destroy(base.transform.GetChild(3).gameObject);
				Object.Destroy(base.transform.GetChild(4).gameObject);
				Object.Destroy(base.transform.GetChild(5).gameObject);
				Object.Destroy(base.transform.GetChild(6).gameObject);
				Object.Destroy(base.transform.GetChild(7).gameObject);
				return;
			case 3:
				this.grid.spacing = new Vector2(47f, 0f);
				Object.Destroy(base.transform.GetChild(0).gameObject);
				Object.Destroy(base.transform.GetChild(1).gameObject);
				Object.Destroy(base.transform.GetChild(2).gameObject);
				Object.Destroy(base.transform.GetChild(3).gameObject);
				Object.Destroy(base.transform.GetChild(4).gameObject);
				Object.Destroy(base.transform.GetChild(5).gameObject);
				Object.Destroy(base.transform.GetChild(6).gameObject);
				return;
			case 4:
				this.grid.spacing = new Vector2(35f, 0f);
				Object.Destroy(base.transform.GetChild(0).gameObject);
				Object.Destroy(base.transform.GetChild(1).gameObject);
				Object.Destroy(base.transform.GetChild(2).gameObject);
				Object.Destroy(base.transform.GetChild(3).gameObject);
				Object.Destroy(base.transform.GetChild(4).gameObject);
				Object.Destroy(base.transform.GetChild(5).gameObject);
				return;
			case 5:
				this.grid.spacing = new Vector2(26f, 0f);
				Object.Destroy(base.transform.GetChild(0).gameObject);
				Object.Destroy(base.transform.GetChild(1).gameObject);
				Object.Destroy(base.transform.GetChild(2).gameObject);
				Object.Destroy(base.transform.GetChild(3).gameObject);
				Object.Destroy(base.transform.GetChild(4).gameObject);
				return;
			case 6:
				this.grid.spacing = new Vector2(21.7f, 0f);
				Object.Destroy(base.transform.GetChild(0).gameObject);
				Object.Destroy(base.transform.GetChild(1).gameObject);
				Object.Destroy(base.transform.GetChild(2).gameObject);
				Object.Destroy(base.transform.GetChild(3).gameObject);
				return;
			case 7:
				this.grid.spacing = new Vector2(28.6f, 0f);
				Object.Destroy(base.transform.GetChild(0).gameObject);
				Object.Destroy(base.transform.GetChild(1).gameObject);
				Object.Destroy(base.transform.GetChild(2).gameObject);
				return;
			case 8:
				this.grid.spacing = new Vector2(16.25f, 0f);
				Object.Destroy(base.transform.GetChild(0).gameObject);
				Object.Destroy(base.transform.GetChild(1).gameObject);
				return;
			case 9:
				this.grid.spacing = new Vector2(14.4f, 0f);
				Object.Destroy(base.transform.GetChild(0).gameObject);
				return;
			case 10:
				this.grid.spacing = new Vector2(13f, 0f);
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002EE0 File Offset: 0x000010E0
	private void FlagUpdate()
	{
		this.wave = ProgressMgr.bg.theWave;
		int num = this.wave;
		if (num <= 50)
		{
			if (num <= 20)
			{
				if (num == 10)
				{
					this.flag10.anchoredPosition = new Vector2(10f, 5f);
					return;
				}
				if (num != 20)
				{
					return;
				}
				this.flag9.anchoredPosition = new Vector2(10f, 5f);
				return;
			}
			else
			{
				if (num == 30)
				{
					this.flag8.anchoredPosition = new Vector2(10f, 5f);
					return;
				}
				if (num == 40)
				{
					this.flag7.anchoredPosition = new Vector2(10f, 5f);
					return;
				}
				if (num != 50)
				{
					return;
				}
				this.flag6.anchoredPosition = new Vector2(10f, 5f);
				return;
			}
		}
		else if (num <= 70)
		{
			if (num == 60)
			{
				this.flag5.anchoredPosition = new Vector2(10f, 5f);
				return;
			}
			if (num != 70)
			{
				return;
			}
			this.flag4.anchoredPosition = new Vector2(10f, 5f);
			return;
		}
		else
		{
			if (num == 80)
			{
				this.flag3.anchoredPosition = new Vector2(10f, 5f);
				return;
			}
			if (num == 90)
			{
				this.flag2.anchoredPosition = new Vector2(10f, 5f);
				return;
			}
			if (num != 100)
			{
				return;
			}
			this.flag1.anchoredPosition = new Vector2(10f, 5f);
			return;
		}
	}

	// Token: 0x04000024 RID: 36
	private int flag;

	// Token: 0x04000025 RID: 37
	private bool once;

	// Token: 0x04000026 RID: 38
	private GridLayoutGroup grid;

	// Token: 0x04000027 RID: 39
	public RectTransform flag1;

	// Token: 0x04000028 RID: 40
	public RectTransform flag2;

	// Token: 0x04000029 RID: 41
	public RectTransform flag3;

	// Token: 0x0400002A RID: 42
	public RectTransform flag4;

	// Token: 0x0400002B RID: 43
	public RectTransform flag5;

	// Token: 0x0400002C RID: 44
	public RectTransform flag6;

	// Token: 0x0400002D RID: 45
	public RectTransform flag7;

	// Token: 0x0400002E RID: 46
	public RectTransform flag8;

	// Token: 0x0400002F RID: 47
	public RectTransform flag9;

	// Token: 0x04000030 RID: 48
	public RectTransform flag10;

	// Token: 0x04000031 RID: 49
	private int wave;
}

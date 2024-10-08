using System;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class NutChomper : Chomper
{
	// Token: 0x06000263 RID: 611 RVA: 0x00014140 File Offset: 0x00012340
	protected override void Start()
	{
		base.Start();
		this.backCrack1 = Resources.Load<Sprite>("Plants/_Mixer/NutChomper/cracked1_back");
		this.backCrack2 = Resources.Load<Sprite>("Plants/_Mixer/NutChomper/cracked2_back");
		this.headCrack1 = Resources.Load<Sprite>("Plants/_Mixer/NutChomper/cracked1_head");
		this.headCrack2 = Resources.Load<Sprite>("Plants/_Mixer/NutChomper/cracked2_head");
		this.back = base.transform.Find("Wallnut_body").gameObject;
		this.head = base.transform.Find("head").gameObject;
		this.originBack = this.back.GetComponent<SpriteRenderer>().sprite;
		this.originHead = this.head.GetComponent<SpriteRenderer>().sprite;
	}

	// Token: 0x06000264 RID: 612 RVA: 0x000141F5 File Offset: 0x000123F5
	protected override void Update()
	{
		base.Update();
		this.ReplaceSprite();
	}

	// Token: 0x06000265 RID: 613 RVA: 0x00014203 File Offset: 0x00012403
	protected override void Swallow()
	{
		base.Swallow();
		base.Recover(1000);
	}

	// Token: 0x06000266 RID: 614 RVA: 0x00014218 File Offset: 0x00012418
	private void ReplaceSprite()
	{
		if (this.thePlantHealth > this.thePlantMaxHealth * 2 / 3)
		{
			this.head.GetComponent<SpriteRenderer>().sprite = this.originHead;
			this.back.GetComponent<SpriteRenderer>().sprite = this.originBack;
			this.cracked1 = false;
			this.cracked2 = false;
		}
		if (this.thePlantHealth > this.thePlantMaxHealth / 3 && this.thePlantHealth < this.thePlantMaxHealth * 2 / 3)
		{
			this.head.GetComponent<SpriteRenderer>().sprite = this.headCrack1;
			this.back.GetComponent<SpriteRenderer>().sprite = this.backCrack1;
			this.cracked2 = false;
			if (!this.cracked1)
			{
				Object.Instantiate<GameObject>(GameAPP.particlePrefab[13], this.head.transform.position, Quaternion.identity).transform.SetParent(this.board.gameObject.transform);
				this.cracked1 = true;
			}
		}
		if (this.thePlantHealth < this.thePlantMaxHealth / 3)
		{
			this.head.GetComponent<SpriteRenderer>().sprite = this.headCrack2;
			this.back.GetComponent<SpriteRenderer>().sprite = this.backCrack2;
			if (!this.cracked2)
			{
				Object.Instantiate<GameObject>(GameAPP.particlePrefab[13], this.head.transform.position, Quaternion.identity).transform.SetParent(this.board.gameObject.transform);
				this.cracked2 = true;
			}
		}
	}

	// Token: 0x04000181 RID: 385
	private Sprite backCrack1;

	// Token: 0x04000182 RID: 386
	private Sprite backCrack2;

	// Token: 0x04000183 RID: 387
	private Sprite headCrack1;

	// Token: 0x04000184 RID: 388
	private Sprite headCrack2;

	// Token: 0x04000185 RID: 389
	private Sprite originBack;

	// Token: 0x04000186 RID: 390
	private Sprite originHead;

	// Token: 0x04000187 RID: 391
	private GameObject back;

	// Token: 0x04000188 RID: 392
	private GameObject head;

	// Token: 0x04000189 RID: 393
	private bool cracked1;

	// Token: 0x0400018A RID: 394
	private bool cracked2;
}

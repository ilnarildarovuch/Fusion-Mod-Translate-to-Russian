using System;
using UnityEngine;

// Token: 0x0200009B RID: 155
public class WallNut : Plant
{
	// Token: 0x06000317 RID: 791 RVA: 0x00018DAF File Offset: 0x00016FAF
	protected override void Update()
	{
		base.Update();
		this.ReplaceSprite();
	}

	// Token: 0x06000318 RID: 792 RVA: 0x00018DC0 File Offset: 0x00016FC0
	protected virtual void ReplaceSprite()
	{
		if (this.thePlantHealth > this.thePlantMaxHealth * 2 / 3)
		{
			base.transform.GetChild(0).gameObject.SetActive(true);
			base.transform.GetChild(1).gameObject.SetActive(false);
			base.transform.GetChild(2).gameObject.SetActive(false);
		}
		if (this.thePlantHealth > this.thePlantMaxHealth / 3 && this.thePlantHealth < this.thePlantMaxHealth * 2 / 3)
		{
			base.transform.GetChild(0).gameObject.SetActive(false);
			base.transform.GetChild(1).gameObject.SetActive(true);
			base.transform.GetChild(2).gameObject.SetActive(false);
		}
		if (this.thePlantHealth < this.thePlantMaxHealth / 3)
		{
			base.transform.GetChild(0).gameObject.SetActive(false);
			base.transform.GetChild(1).gameObject.SetActive(false);
			base.transform.GetChild(2).gameObject.SetActive(true);
		}
	}

	// Token: 0x06000319 RID: 793 RVA: 0x00018EE0 File Offset: 0x000170E0
	protected virtual void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Zombie"))
		{
			Zombie component = collision.GetComponent<Zombie>();
			if (component.theZombieRow == this.thePlantRow && component.theAttackTarget == base.gameObject && collision.gameObject.GetComponent<Zombie>().theStatus != 1)
			{
				this.thePlantSpeed = 0f;
			}
		}
	}

	// Token: 0x0600031A RID: 794 RVA: 0x00018F45 File Offset: 0x00017145
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		this.thePlantSpeed = this.theConstSpeed;
	}
}

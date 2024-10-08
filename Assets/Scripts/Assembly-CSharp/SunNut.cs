using System;
using UnityEngine;

// Token: 0x0200007C RID: 124
public class SunNut : Producer
{
	// Token: 0x0600028C RID: 652 RVA: 0x00015160 File Offset: 0x00013360
	protected override void Update()
	{
		base.Update();
		this.ReplaceSprite();
	}

	// Token: 0x0600028D RID: 653 RVA: 0x0001516E File Offset: 0x0001336E
	public override void Die()
	{
		if (!this.board.isIZ)
		{
			this.ProduceSunWithNoSound();
			this.ProduceSunWithNoSound();
		}
		base.Die();
	}

	// Token: 0x0600028E RID: 654 RVA: 0x00015190 File Offset: 0x00013390
	private void ReplaceSprite()
	{
		if (this.thePlantHealth < this.thePlantMaxHealth * 2 / 3 && this.thePlantHealth > this.thePlantMaxHealth / 3)
		{
			base.transform.GetChild(0).gameObject.SetActive(false);
			base.transform.GetChild(1).gameObject.SetActive(true);
			if (!this.produceSun1 && !this.board.isIZ)
			{
				this.ProduceSun();
				this.produceSun1 = true;
				return;
			}
		}
		else if (this.thePlantHealth < this.thePlantMaxHealth / 3)
		{
			base.transform.GetChild(1).gameObject.SetActive(false);
			base.transform.GetChild(2).gameObject.SetActive(true);
			if (!this.produceSun2 && !this.board.isIZ)
			{
				this.ProduceSun();
				this.produceSun2 = true;
			}
		}
	}

	// Token: 0x0600028F RID: 655 RVA: 0x00015270 File Offset: 0x00013470
	private void OnTriggerStay2D(Collider2D collision)
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

	// Token: 0x06000290 RID: 656 RVA: 0x000152D5 File Offset: 0x000134D5
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		this.thePlantSpeed = this.theConstSpeed;
	}

	// Token: 0x0400018F RID: 399
	private bool produceSun1;

	// Token: 0x04000190 RID: 400
	private bool produceSun2;
}

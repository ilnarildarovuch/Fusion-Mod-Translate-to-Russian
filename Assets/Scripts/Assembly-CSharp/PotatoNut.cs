using System;
using UnityEngine;

// Token: 0x02000078 RID: 120
public class PotatoNut : WallNut
{
	// Token: 0x06000277 RID: 631 RVA: 0x000148B4 File Offset: 0x00012AB4
	protected override void Update()
	{
		base.Update();
		this.SetFlash();
		this.flashTime += Time.deltaTime;
		if (this.flashTime > this.flashInterval)
		{
			this.flashTime = 0f;
			this.anim.Play("flash");
		}
	}

	// Token: 0x06000278 RID: 632 RVA: 0x00014908 File Offset: 0x00012B08
	protected override void ReplaceSprite()
	{
		if (this.thePlantHealth < this.thePlantMaxHealth * 2 / 3 && this.thePlantHealth > this.thePlantMaxHealth / 3)
		{
			base.transform.GetChild(0).gameObject.SetActive(false);
			base.transform.GetChild(1).gameObject.SetActive(true);
			base.transform.GetChild(3).gameObject.SetActive(false);
			if (!this.isExplode1)
			{
				this.Explode(500, false);
				this.isExplode1 = true;
				return;
			}
		}
		else if (this.thePlantHealth < this.thePlantMaxHealth / 3)
		{
			base.transform.GetChild(1).gameObject.SetActive(false);
			base.transform.GetChild(2).gameObject.SetActive(true);
			base.transform.GetChild(3).gameObject.SetActive(false);
			if (!this.isExplode2)
			{
				this.Explode(500, false);
				this.isExplode2 = true;
				return;
			}
		}
		else
		{
			base.transform.GetChild(0).gameObject.SetActive(true);
			base.transform.GetChild(1).gameObject.SetActive(false);
			base.transform.GetChild(2).gameObject.SetActive(false);
		}
	}

	// Token: 0x06000279 RID: 633 RVA: 0x00014A51 File Offset: 0x00012C51
	public override void Die()
	{
		this.Explode(1800, true);
		base.Die();
	}

	// Token: 0x0600027A RID: 634 RVA: 0x00014A68 File Offset: 0x00012C68
	private void Explode(int dmg, bool isShake)
	{
		foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(base.transform.position, 1f))
		{
			if (collider2D.CompareTag("Zombie"))
			{
				Zombie component = collider2D.GetComponent<Zombie>();
				if (component.theZombieRow == this.thePlantRow)
				{
					component.TakeDamage(10, dmg);
				}
			}
		}
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y + 1f, base.transform.position.z);
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[8], position, Quaternion.LookRotation(new Vector3(0f, 90f, 0f))).transform.SetParent(GameAPP.board.transform);
		GameAPP.PlaySound(47, 0.5f);
		if (isShake)
		{
			ScreenShake.TriggerShake(0.15f);
		}
	}

	// Token: 0x0600027B RID: 635 RVA: 0x00014B60 File Offset: 0x00012D60
	private void SetFlash()
	{
		if (this.thePlantHealth > 3000)
		{
			this.flashInterval = 5f;
		}
		if (this.thePlantHealth > 2667 && this.thePlantHealth < 3000)
		{
			this.flashInterval = 1f;
		}
		if (this.thePlantHealth > 2000 && this.thePlantHealth > 2667)
		{
			this.flashInterval = 5f;
		}
		if (this.thePlantHealth > 1333 && this.thePlantHealth < 2000)
		{
			this.flashInterval = 1f;
		}
		if (this.thePlantHealth > 500 && this.thePlantHealth < 1333)
		{
			this.flashInterval = 5f;
		}
		if (this.thePlantHealth < 500)
		{
			this.flashInterval = 1f;
		}
	}

	// Token: 0x0400018B RID: 395
	private bool isExplode1;

	// Token: 0x0400018C RID: 396
	private bool isExplode2;

	// Token: 0x0400018D RID: 397
	private float flashInterval = 3f;

	// Token: 0x0400018E RID: 398
	private float flashTime;
}

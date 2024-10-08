using System;
using UnityEngine;

// Token: 0x0200006D RID: 109
public class PotatoMine : Plant
{
	// Token: 0x0600021F RID: 543 RVA: 0x0001125C File Offset: 0x0000F45C
	protected override void Start()
	{
		base.Start();
		this.attributeCountdown = Random.Range(14f, 16f);
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0001127C File Offset: 0x0000F47C
	protected override void Update()
	{
		if (this.attributeCountdown > 0f)
		{
			this.attributeCountdown -= Time.deltaTime;
		}
		if (this.attributeCountdown <= 0f)
		{
			this.attributeCountdown = 0f;
			this.anim.SetTrigger("rise");
		}
		base.Update();
		this.SetFlash();
		if (this.isAready)
		{
			this.flashTime += Time.deltaTime;
			if (this.flashTime > this.flashInterval)
			{
				this.flashTime = 0f;
				this.anim.Play("flash");
			}
		}
	}

	// Token: 0x06000221 RID: 545 RVA: 0x00011320 File Offset: 0x0000F520
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Zombie"))
		{
			PolevaulterZombie polevaulterZombie;
			if (collision.TryGetComponent<PolevaulterZombie>(out polevaulterZombie) && polevaulterZombie.polevaulterStatus != 2)
			{
				return;
			}
			Zombie component = collision.GetComponent<Zombie>();
			if (component.theZombieRow == this.thePlantRow && this.isAready && component.theStatus != 1 && !component.isMindControlled)
			{
				this.isAready = false;
				this.Explode();
				Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y + 1f, base.transform.position.z);
				Object.Instantiate<GameObject>(GameAPP.particlePrefab[8], position, Quaternion.LookRotation(new Vector3(0f, 90f, 0f))).transform.SetParent(GameAPP.board.transform);
				GameAPP.PlaySound(47, 0.5f);
				ScreenShake.TriggerShake(0.15f);
				if (!this.isActive)
				{
					base.Invoke("DelayDie", 0.2f);
					this.isActive = true;
				}
			}
		}
	}

	// Token: 0x06000222 RID: 546 RVA: 0x00011444 File Offset: 0x0000F644
	private void DelayDie()
	{
		this.Die();
	}

	// Token: 0x06000223 RID: 547 RVA: 0x0001144C File Offset: 0x0000F64C
	public void AnimStartRise()
	{
		GameAPP.PlaySound(48, 0.5f);
		this.isAshy = true;
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y + 0.5f, base.transform.position.z);
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[9], position, Quaternion.identity).transform.SetParent(GameAPP.board.transform);
	}

	// Token: 0x06000224 RID: 548 RVA: 0x000114D0 File Offset: 0x0000F6D0
	public void AnimRiseOver()
	{
		this.isAready = true;
	}

	// Token: 0x06000225 RID: 549 RVA: 0x000114D9 File Offset: 0x0000F6D9
	public virtual void AnimMeshed()
	{
		base.Invoke("Die", 2f);
	}

	// Token: 0x06000226 RID: 550 RVA: 0x000114EC File Offset: 0x0000F6EC
	private void Explode()
	{
		foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(base.transform.position, 1f))
		{
			if (collider2D.CompareTag("Zombie"))
			{
				Zombie component = collider2D.GetComponent<Zombie>();
				if (component.theZombieRow == this.thePlantRow && !component.isMindControlled)
				{
					component.TakeDamage(10, 1800);
				}
			}
		}
	}

	// Token: 0x06000227 RID: 551 RVA: 0x00011560 File Offset: 0x0000F760
	private void SetFlash()
	{
		GameObject gameObject = this.GetNearestZombie();
		if (gameObject == null)
		{
			this.flashInterval = 6f;
			return;
		}
		float num = gameObject.transform.position.x - base.transform.position.x;
		if (num > 6f)
		{
			this.flashInterval = 6f;
			return;
		}
		if (num < 1f)
		{
			this.flashInterval = 0.2f;
			return;
		}
		this.flashInterval = num / 2f;
	}

	// Token: 0x06000228 RID: 552 RVA: 0x000115E0 File Offset: 0x0000F7E0
	protected virtual GameObject GetNearestZombie()
	{
		this.nearestZombie = null;
		float num = float.MaxValue;
		foreach (GameObject gameObject in GameAPP.board.GetComponent<Board>().zombieArray)
		{
			if (gameObject != null && gameObject.transform.position.x >= base.transform.position.x)
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (!component.isMindControlled && component.theZombieRow == this.thePlantRow && component.theStatus != 1)
				{
					float num2 = Vector3.Distance(gameObject.transform.position, base.transform.position);
					if (num2 < num)
					{
						num = num2;
						this.nearestZombie = gameObject;
					}
				}
			}
		}
		return this.nearestZombie;
	}

	// Token: 0x0400016C RID: 364
	private bool isAready;

	// Token: 0x0400016D RID: 365
	private GameObject nearestZombie;

	// Token: 0x0400016E RID: 366
	private float flashInterval = 3f;

	// Token: 0x0400016F RID: 367
	private float flashTime;

	// Token: 0x04000170 RID: 368
	private bool isActive;
}

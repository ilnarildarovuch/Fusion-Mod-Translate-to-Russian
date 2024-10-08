using System;
using UnityEngine;

// Token: 0x02000081 RID: 129
public class Chomper : Plant
{
	// Token: 0x060002AA RID: 682 RVA: 0x00015C24 File Offset: 0x00013E24
	protected virtual void SetAttackRange()
	{
		this.pos = new Vector2(this.shadow.transform.position.x + 1.5f, this.shadow.transform.position.y + 0.5f);
		this.range = new Vector2(1.5f, 1.5f);
	}

	// Token: 0x060002AB RID: 683 RVA: 0x00015C87 File Offset: 0x00013E87
	protected override void Update()
	{
		base.Update();
		this.SetAttackRange();
		if (this.attributeCountdown > 0f)
		{
			this.attributeCountdown -= Time.deltaTime;
			if (this.attributeCountdown < 0f)
			{
				this.Swallow();
			}
		}
	}

	// Token: 0x060002AC RID: 684 RVA: 0x00015CC7 File Offset: 0x00013EC7
	protected virtual void Swallow()
	{
		this.anim.SetTrigger("swallow");
		this.anim.SetBool("chew", false);
		base.Invoke("ResetCount", 1.5f);
	}

	// Token: 0x060002AD RID: 685 RVA: 0x00015CFC File Offset: 0x00013EFC
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.zombie == null && this.anim.GetCurrentAnimatorStateInfo(0).IsName("idle") && this.attributeCountdown == 0f)
		{
			this.colliders = Physics2D.OverlapBoxAll(this.pos, this.range, 0f);
			this.minX = float.PositiveInfinity;
			foreach (GameObject gameObject in this.board.GetComponent<Board>().zombieArray)
			{
				if (gameObject != null && gameObject.GetComponent<Zombie>().theAttackTarget == base.gameObject)
				{
					this.zombie = gameObject;
					this.anim.SetTrigger("bite");
					return;
				}
			}
			foreach (Collider2D collider2D in this.colliders)
			{
				if (!(collider2D == null) && collider2D.CompareTag("Zombie"))
				{
					Zombie component = collider2D.GetComponent<Zombie>();
					if (component.theStatus != 1 && component.theStatus != 7 && component.theZombieRow == this.thePlantRow && !component.isMindControlled)
					{
						Transform transform = collider2D.transform;
						if (transform != null)
						{
							float x = transform.position.x;
							if (x < this.minX)
							{
								this.minX = x;
								this.zombie = collider2D.gameObject;
							}
						}
					}
				}
			}
			if (this.zombie != null)
			{
				this.anim.SetTrigger("bite");
			}
		}
	}

	// Token: 0x060002AE RID: 686 RVA: 0x00015ED0 File Offset: 0x000140D0
	public virtual void BiteEvent()
	{
		if (this.zombie != null)
		{
			Zombie component = this.zombie.GetComponent<Zombie>();
			if (component.theAttackTarget == base.gameObject)
			{
				component.Die(2);
				this.attributeCountdown = this.swallowMaxCountDown;
				this.canToChew = true;
				this.zombie = null;
				GameAPP.PlaySound(49, 0.5f);
				return;
			}
			Collider2D[] array = this.colliders;
			int i = 0;
			while (i < array.Length)
			{
				Collider2D collider2D = array[i];
				if (!(collider2D == null) && collider2D.gameObject == this.zombie)
				{
					PolevaulterZombie polevaulterZombie;
					if (this.zombie.TryGetComponent<PolevaulterZombie>(out polevaulterZombie) && polevaulterZombie.polevaulterStatus != 2)
					{
						GameAPP.PlaySound(49, 0.5f);
						this.zombie = null;
						this.anim.SetTrigger("back");
						return;
					}
					if (component.theStatus == 1 || component.isMindControlled)
					{
						GameAPP.PlaySound(49, 0.5f);
						this.zombie = null;
						this.anim.SetTrigger("back");
						return;
					}
					component.Die(2);
					this.attributeCountdown = this.swallowMaxCountDown;
					this.canToChew = true;
					this.zombie = null;
					GameAPP.PlaySound(49, 0.5f);
					return;
				}
				else
				{
					i++;
				}
			}
		}
		this.zombie = null;
		this.anim.SetTrigger("back");
		GameAPP.PlaySound(49, 0.5f);
	}

	// Token: 0x060002AF RID: 687 RVA: 0x0001603F File Offset: 0x0001423F
	protected void ResetCount()
	{
		this.attributeCountdown = 0f;
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x0001604C File Offset: 0x0001424C
	public void ToChew()
	{
		if (this.canToChew)
		{
			this.anim.Play("chew");
		}
		this.canToChew = false;
	}

	// Token: 0x060002B1 RID: 689 RVA: 0x00016070 File Offset: 0x00014270
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(this.pos, this.range);
		foreach (Collider2D collider2D in Physics2D.OverlapBoxAll(this.pos, this.range, 0f))
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(collider2D.bounds.center, 0.1f);
		}
	}

	// Token: 0x04000199 RID: 409
	protected GameObject zombie;

	// Token: 0x0400019A RID: 410
	protected Vector2 pos = new Vector2(-20f, 0f);

	// Token: 0x0400019B RID: 411
	protected float minX = float.PositiveInfinity;

	// Token: 0x0400019C RID: 412
	[SerializeField]
	protected float swallowMaxCountDown = 40f;

	// Token: 0x0400019D RID: 413
	protected bool canToChew;

	// Token: 0x0400019E RID: 414
	protected Vector2 range = new Vector2(1.5f, 1.5f);

	// Token: 0x0400019F RID: 415
	protected Collider2D[] colliders;
}

using System;
using UnityEngine;

// Token: 0x02000077 RID: 119
public class PotatoChomper : Chomper
{
	// Token: 0x06000271 RID: 625 RVA: 0x00014688 File Offset: 0x00012888
	public override void Die()
	{
		this.Explode();
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y + 1.5f, base.transform.position.z);
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[8], position, Quaternion.LookRotation(new Vector3(0f, 90f, 0f))).transform.SetParent(GameAPP.board.transform);
		GameAPP.PlaySound(47, 0.5f);
		ScreenShake.TriggerShake(0.15f);
		base.Die();
	}

	// Token: 0x06000272 RID: 626 RVA: 0x00014730 File Offset: 0x00012930
	private void Explode()
	{
		foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(base.transform.position, 1f))
		{
			if (collider2D.CompareTag("Zombie"))
			{
				Zombie component = collider2D.GetComponent<Zombie>();
				if (component.theZombieRow == this.thePlantRow)
				{
					component.TakeDamage(10, 1800);
				}
			}
		}
	}

	// Token: 0x06000273 RID: 627 RVA: 0x0001479C File Offset: 0x0001299C
	protected override void Swallow()
	{
		this.anim.SetTrigger("swallow");
		this.anim.SetBool("chew", false);
		base.Invoke("ResetCount", 1.5f);
		base.Invoke("CreateMine", 0.5f);
	}

	// Token: 0x06000274 RID: 628 RVA: 0x000147EC File Offset: 0x000129EC
	private void CreateMine()
	{
		GameObject gameObject = this.board.GetComponent<CreatePlant>().SetPlant(this.thePlantColumn + 1, this.thePlantRow, 4, null, default(Vector2), false, 0f);
		PotatoMine potatoMine;
		if (gameObject != null && gameObject.TryGetComponent<PotatoMine>(out potatoMine))
		{
			potatoMine.attributeCountdown = 0f;
		}
	}

	// Token: 0x06000275 RID: 629 RVA: 0x00014848 File Offset: 0x00012A48
	protected override void SetAttackRange()
	{
		this.pos = new Vector2(this.shadow.transform.position.x + 0.5f, this.shadow.transform.position.y + 0.5f);
		this.range = new Vector2(0.5f, 1f);
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000057 RID: 87
public class Doom : MonoBehaviour
{
	// Token: 0x060001A9 RID: 425 RVA: 0x0000E54A File Offset: 0x0000C74A
	private void Start()
	{
		this.board = GameAPP.board.GetComponent<Board>();
		this.pos = base.transform.position;
		this.Explode();
	}

	// Token: 0x060001AA RID: 426 RVA: 0x0000E578 File Offset: 0x0000C778
	private void Explode()
	{
		if (this.theDoomType == 0)
		{
			this.NormalDoom();
		}
		if (this.theDoomType == 1)
		{
			this.IceDoom();
		}
	}

	// Token: 0x060001AB RID: 427 RVA: 0x0000E598 File Offset: 0x0000C798
	private void IceDoom()
	{
		List<Zombie> list = new List<Zombie>();
		foreach (GameObject gameObject in this.board.zombieArray)
		{
			if (gameObject != null)
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (!component.isMindControlled)
				{
					list.Add(component);
				}
			}
		}
		foreach (Zombie zombie in list)
		{
			if (zombie.theHealth > 1800f)
			{
				zombie.TakeDamage(3, 1800);
			}
			else
			{
				zombie.SetCold(10f);
				zombie.Charred();
			}
		}
	}

	// Token: 0x060001AC RID: 428 RVA: 0x0000E678 File Offset: 0x0000C878
	private void NormalDoom()
	{
		foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(this.pos, 5f))
		{
			Zombie zombie;
			if (collider2D != null && collider2D.TryGetComponent<Zombie>(out zombie) && !zombie.isMindControlled)
			{
				if (zombie.theHealth > 1800f)
				{
					zombie.TakeDamage(10, 1800);
				}
				else
				{
					zombie.Charred();
				}
			}
		}
	}

	// Token: 0x060001AD RID: 429 RVA: 0x0000E6E8 File Offset: 0x0000C8E8
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(this.pos, 5f, 0))
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(collider2D.bounds.center, 0.1f);
		}
	}

	// Token: 0x060001AE RID: 430 RVA: 0x0000E743 File Offset: 0x0000C943
	private void Die()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0400011E RID: 286
	private Vector2 pos = new Vector2(0f, 0f);

	// Token: 0x0400011F RID: 287
	private const float range = 5f;

	// Token: 0x04000120 RID: 288
	public int theDoomType;

	// Token: 0x04000121 RID: 289
	private Board board;
}

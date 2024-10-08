using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200007F RID: 127
public class BigWallNut : Plant
{
	// Token: 0x0600029F RID: 671 RVA: 0x00015896 File Offset: 0x00013A96
	protected override void Start()
	{
		GameAPP.PlaySound(53, 0.5f);
		this.anim.Play("Round");
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x000158B4 File Offset: 0x00013AB4
	protected override void FixedUpdate()
	{
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x000158B6 File Offset: 0x00013AB6
	protected override void Update()
	{
		if (Camera.main.WorldToViewportPoint(base.transform.position).x > 1f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x000158E4 File Offset: 0x00013AE4
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Zombie zombie;
		if (collision.TryGetComponent<Zombie>(out zombie))
		{
			if (zombie.theZombieRow != this.thePlantRow)
			{
				return;
			}
			if (zombie.isMindControlled)
			{
				return;
			}
			using (List<GameObject>.Enumerator enumerator = this.zombie.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == zombie.gameObject)
					{
						return;
					}
				}
			}
			this.zombie.Add(zombie.gameObject);
			zombie.TakeDamage(10, 1800);
			GameAPP.PlaySound(Random.Range(54, 56), 0.5f);
			ScreenShake.TriggerShake(0.02f);
		}
	}

	// Token: 0x04000198 RID: 408
	private readonly List<GameObject> zombie = new List<GameObject>();
}

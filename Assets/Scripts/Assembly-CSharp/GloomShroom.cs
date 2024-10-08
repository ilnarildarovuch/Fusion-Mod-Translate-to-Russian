using System;
using UnityEngine;

// Token: 0x020000AD RID: 173
public class GloomShroom : Shooter
{
	// Token: 0x06000355 RID: 853 RVA: 0x0001A1E1 File Offset: 0x000183E1
	protected override void Awake()
	{
		base.Awake();
		this.center = base.transform.Find("Shoot").gameObject;
	}

	// Token: 0x06000356 RID: 854 RVA: 0x0001A204 File Offset: 0x00018404
	protected override GameObject SearchZombie()
	{
		this.colliders = Physics2D.OverlapCircleAll(this.center.transform.position, this.range, this.zombieLayer);
		foreach (Collider2D collider2D in this.colliders)
		{
			Zombie zombie;
			if (collider2D.TryGetComponent<Zombie>(out zombie) && Mathf.Abs(zombie.theZombieRow - this.thePlantRow) <= 1 && base.SearchUniqueZombie(zombie))
			{
				return collider2D.gameObject;
			}
		}
		return null;
	}

	// Token: 0x06000357 RID: 855 RVA: 0x0001A286 File Offset: 0x00018486
	public override GameObject AnimShoot()
	{
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[37], this.center.transform.position, Quaternion.identity, this.board.transform);
		this.AttackZombie();
		return null;
	}

	// Token: 0x06000358 RID: 856 RVA: 0x0001A2C0 File Offset: 0x000184C0
	protected virtual void AttackZombie()
	{
		bool flag = false;
		this.colliders = Physics2D.OverlapCircleAll(this.center.transform.position, this.range, this.zombieLayer);
		Collider2D[] array = this.colliders;
		for (int i = 0; i < array.Length; i++)
		{
			Zombie zombie;
			if (array[i].TryGetComponent<Zombie>(out zombie) && Mathf.Abs(zombie.theZombieRow - this.thePlantRow) <= 1 && base.AttackUniqueZombie(zombie))
			{
				flag = true;
				this.zombieList.Add(zombie);
			}
		}
		for (int j = this.zombieList.Count - 1; j >= 0; j--)
		{
			if (this.zombieList[j] != null)
			{
				this.zombieList[j].TakeDamage(1, 20);
			}
		}
		this.zombieList.Clear();
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
		}
	}

	// Token: 0x040001C5 RID: 453
	protected Collider2D[] colliders;

	// Token: 0x040001C6 RID: 454
	protected GameObject center;

	// Token: 0x040001C7 RID: 455
	protected readonly float range = 2f;
}

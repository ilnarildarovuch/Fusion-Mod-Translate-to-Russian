using System;
using UnityEngine;

// Token: 0x020000B2 RID: 178
public class IceGloom : GloomShroom
{
	// Token: 0x06000366 RID: 870 RVA: 0x0001A9A4 File Offset: 0x00018BA4
	protected override void AttackZombie()
	{
		bool flag = false;
		this.colliders = Physics2D.OverlapCircleAll(this.center.transform.position, this.range, this.zombieLayer);
		Collider2D[] colliders = this.colliders;
		for (int i = 0; i < colliders.Length; i++)
		{
			Zombie zombie;
			if (colliders[i].TryGetComponent<Zombie>(out zombie) && Mathf.Abs(zombie.theZombieRow - this.thePlantRow) <= 1 && base.SearchUniqueZombie(zombie))
			{
				flag = true;
				this.zombieList.Add(zombie);
			}
		}
		for (int j = this.zombieList.Count - 1; j >= 0; j--)
		{
			if (this.zombieList[j] != null)
			{
				this.zombieList[j].TakeDamage(3, 20);
				this.zombieList[j].AddfreezeLevel(5);
			}
		}
		this.zombieList.Clear();
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
		}
	}

	// Token: 0x06000367 RID: 871 RVA: 0x0001AAA4 File Offset: 0x00018CA4
	public override GameObject AnimShoot()
	{
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[39], this.center.transform.position, Quaternion.identity, this.board.transform);
		this.AttackZombie();
		return null;
	}
}

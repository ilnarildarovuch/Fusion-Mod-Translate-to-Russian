using System;
using UnityEngine;

// Token: 0x020000AA RID: 170
public class FireGloom : GloomShroom
{
	// Token: 0x0600034C RID: 844 RVA: 0x00019CF8 File Offset: 0x00017EF8
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
				if (this.zombieList[j].isJalaed)
				{
					this.zombieList[j].TakeDamage(1, 80);
				}
				else
				{
					this.zombieList[j].TakeDamage(1, 40);
				}
				this.zombieList[j].Warm(0);
			}
		}
		this.zombieList.Clear();
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
		}
	}

	// Token: 0x0600034D RID: 845 RVA: 0x00019E23 File Offset: 0x00018023
	public override GameObject AnimShoot()
	{
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[38], this.center.transform.position, Quaternion.identity, this.board.transform);
		this.AttackZombie();
		return null;
	}
}

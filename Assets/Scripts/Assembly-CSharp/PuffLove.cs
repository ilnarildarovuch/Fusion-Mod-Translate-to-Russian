using System;
using UnityEngine;

// Token: 0x02000044 RID: 68
public class PuffLove : Bullet
{
	// Token: 0x06000146 RID: 326 RVA: 0x0000A97C File Offset: 0x00008B7C
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, this.theBulletDamage);
		this.PlaySound(component);
		this.SetFume();
		this.Die();
	}

	// Token: 0x06000147 RID: 327 RVA: 0x0000A9B0 File Offset: 0x00008BB0
	private void SetFume()
	{
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[22], base.transform.position, Quaternion.identity, GameAPP.board.transform).GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = string.Format("particle{0}", this.theBulletRow);
		this.AttackZombie();
	}

	// Token: 0x06000148 RID: 328 RVA: 0x0000AA10 File Offset: 0x00008C10
	private void AttackZombie()
	{
		foreach (Collider2D collider2D in Physics2D.OverlapBoxAll(base.transform.position, new Vector2(3f, 3f), 0f))
		{
			Zombie zombie;
			PolevaulterZombie polevaulterZombie;
			if (collider2D != null && collider2D.TryGetComponent<Zombie>(out zombie) && Mathf.Abs(zombie.theZombieRow - this.theBulletRow) <= 1 && (!zombie.gameObject.TryGetComponent<PolevaulterZombie>(out polevaulterZombie) || polevaulterZombie.polevaulterStatus != 1) && !zombie.isMindControlled)
			{
				this.TrySetMindControl(zombie);
			}
		}
	}

	// Token: 0x06000149 RID: 329 RVA: 0x0000AAAC File Offset: 0x00008CAC
	private void TrySetMindControl(Zombie zombie)
	{
		float num = (float)(zombie.theFirstArmorMaxHealth + zombie.theMaxHealth);
		float num2 = ((float)zombie.theFirstArmorHealth + zombie.theHealth) / num;
		if ((double)num2 > 0.5)
		{
			num2 = 1f;
		}
		else
		{
			num2 /= 0.5f;
		}
		num2 = Mathf.Sqrt(num2);
		float num3 = 0.75f;
		if (num2 < num3)
		{
			num2 = num3;
		}
		if (Random.value >= num2)
		{
			zombie.SetMindControl(false);
			return;
		}
		zombie.TakeDamage(1, 40);
	}
}

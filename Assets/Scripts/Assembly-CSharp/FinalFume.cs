using System;
using UnityEngine;

// Token: 0x020000A8 RID: 168
public class FinalFume : IceDoomFume
{
	// Token: 0x06000342 RID: 834 RVA: 0x00019900 File Offset: 0x00017B00
	protected override void Awake()
	{
		base.Awake();
		this.particle = base.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
		this.emission = this.particle.emission;
	}

	// Token: 0x06000343 RID: 835 RVA: 0x00019938 File Offset: 0x00017B38
	protected override void PlantShootUpdate()
	{
		this.thePlantAttackCountDown -= Time.deltaTime;
		if (this.thePlantAttackCountDown < 0f)
		{
			this.thePlantAttackCountDown = this.thePlantAttackInterval;
			this.thePlantAttackCountDown += Random.Range(-0.1f, 0.1f);
			if (this.SearchZombie() != null)
			{
				this.anim.SetBool("shooting", true);
				return;
			}
			this.anim.SetBool("shooting", false);
		}
	}

	// Token: 0x06000344 RID: 836 RVA: 0x000199BD File Offset: 0x00017BBD
	private void EnableParticle()
	{
		this.particle.GetComponent<Renderer>().sortingLayerName = string.Format("particle{0}", this.thePlantRow);
		this.emission.enabled = true;
	}

	// Token: 0x06000345 RID: 837 RVA: 0x000199F0 File Offset: 0x00017BF0
	private void DisableParticle()
	{
		this.emission.enabled = false;
	}

	// Token: 0x06000346 RID: 838 RVA: 0x00019A00 File Offset: 0x00017C00
	private void AttackZombie()
	{
		float x = this.shadow.transform.position.x;
		bool flag = false;
		foreach (GameObject gameObject in this.board.zombieArray)
		{
			if (gameObject != null)
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (component.theZombieRow == this.thePlantRow && component.shadow.transform.position.x > x && base.AttackUniqueZombie(component))
				{
					this.zombieList.Add(component);
					flag = true;
				}
			}
		}
		foreach (Zombie zombie in this.zombieList)
		{
			if (zombie != null && !zombie.isMindControlled)
			{
				this.TrySetMindControl(zombie);
			}
		}
		this.zombieList.Clear();
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
		}
	}

	// Token: 0x06000347 RID: 839 RVA: 0x00019B34 File Offset: 0x00017D34
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
			zombie.SetMindControl(true);
			this.SmallDoom(zombie);
			GameAPP.PlaySound(70, 0.5f);
			return;
		}
		zombie.TakeDamage(3, 60);
	}

	// Token: 0x06000348 RID: 840 RVA: 0x00019BC0 File Offset: 0x00017DC0
	private void SmallDoom(Zombie z)
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(z.shadow.transform.position, 1f, this.zombieLayer);
		for (int i = 0; i < array.Length; i++)
		{
			Zombie zombie;
			if (array[i].gameObject.TryGetComponent<Zombie>(out zombie) && zombie.theZombieRow == this.thePlantRow && !zombie.isMindControlled)
			{
				zombie.TakeDamage(10, 500);
			}
		}
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[27], z.shadow.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, this.board.transform);
	}

	// Token: 0x040001C3 RID: 451
	private ParticleSystem particle;

	// Token: 0x040001C4 RID: 452
	private ParticleSystem.EmissionModule emission;
}

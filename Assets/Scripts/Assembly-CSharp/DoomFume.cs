using System;
using UnityEngine;

// Token: 0x020000A7 RID: 167
public class DoomFume : Plant
{
	// Token: 0x0600033D RID: 829 RVA: 0x000196CC File Offset: 0x000178CC
	protected override void Update()
	{
		base.Update();
		if (this.ShootCD > 0f)
		{
			this.ShootCD -= Time.deltaTime;
			if (this.ShootCD <= 0f)
			{
				this.ShootCD = 0f;
				this.anim.SetTrigger("backToIdle");
			}
		}
	}

	// Token: 0x0600033E RID: 830 RVA: 0x00019726 File Offset: 0x00017926
	public void Shoot()
	{
		if (this.ShootCD == 0f)
		{
			this.anim.SetTrigger("shoot");
			this.ShootCD = 60f;
		}
	}

	// Token: 0x0600033F RID: 831 RVA: 0x00019750 File Offset: 0x00017950
	private void AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[31], position, Quaternion.Euler(0f, 90f, 0f), this.board.transform).GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = string.Format("particle{0}", this.thePlantRow);
		GameAPP.PlaySound(58, 0.5f);
		this.AttackZombie();
	}

	// Token: 0x06000340 RID: 832 RVA: 0x000197DC File Offset: 0x000179DC
	private void AttackZombie()
	{
		bool flag = false;
		foreach (GameObject gameObject in this.board.zombieArray)
		{
			if (gameObject != null)
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (component.shadow.transform.position.x >= this.shadow.transform.position.x && base.SearchUniqueZombie(component) && component.theZombieRow == this.thePlantRow)
				{
					this.zombieList.Add(component);
					flag = true;
				}
			}
		}
		for (int i = this.zombieList.Count - 1; i >= 0; i--)
		{
			if (this.zombieList[i] != null)
			{
				this.zombieList[i].TakeDamage(1, 1800);
			}
		}
		this.zombieList.Clear();
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
		}
	}

	// Token: 0x040001C2 RID: 450
	public float ShootCD;
}

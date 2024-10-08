using System;
using UnityEngine;

// Token: 0x020000AB RID: 171
public class FumeShroom : Shooter
{
	// Token: 0x0600034F RID: 847 RVA: 0x00019E64 File Offset: 0x00018064
	protected override GameObject SearchZombie()
	{
		foreach (GameObject gameObject in this.board.GetComponent<Board>().zombieArray)
		{
			if (gameObject != null)
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (!component.isMindControlled && component.theZombieRow == this.thePlantRow && component.shadow.transform.position.x < 9.2f && component.shadow.transform.position.x > this.shadow.transform.position.x && component.shadow.transform.position.x < this.shadow.transform.position.x + 7f && base.SearchUniqueZombie(component))
				{
					return gameObject;
				}
			}
		}
		return null;
	}

	// Token: 0x06000350 RID: 848 RVA: 0x00019F7C File Offset: 0x0001817C
	protected virtual void AttackZombie()
	{
		bool flag = false;
		foreach (GameObject gameObject in this.board.zombieArray)
		{
			if (gameObject != null)
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (component.shadow.transform.position.x <= this.shadow.transform.position.x + 7f && component.shadow.transform.position.x >= this.shadow.transform.position.x && base.SearchUniqueZombie(component) && component.theZombieRow == this.thePlantRow)
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
				this.zombieList[i].TakeDamage(1, 20);
			}
		}
		this.zombieList.Clear();
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
		}
	}

	// Token: 0x06000351 RID: 849 RVA: 0x0001A0D0 File Offset: 0x000182D0
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[19], position, Quaternion.Euler(0f, 90f, 0f), this.board.transform).GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = string.Format("particle{0}", this.thePlantRow);
		GameAPP.PlaySound(58, 0.5f);
		this.AttackZombie();
		return null;
	}
}

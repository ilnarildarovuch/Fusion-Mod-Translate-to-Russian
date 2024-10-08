using System;
using UnityEngine;

// Token: 0x020000B1 RID: 177
public class IceFumeShroom : FumeShroom
{
	// Token: 0x06000363 RID: 867 RVA: 0x0001A7BC File Offset: 0x000189BC
	protected override void AttackZombie()
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
				this.zombieList[i].TakeDamage(3, 20);
			}
		}
		this.zombieList.Clear();
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
		}
	}

	// Token: 0x06000364 RID: 868 RVA: 0x0001A910 File Offset: 0x00018B10
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[30], position, Quaternion.Euler(0f, 90f, 0f), this.board.transform).GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = string.Format("particle{0}", this.thePlantRow);
		GameAPP.PlaySound(58, 0.5f);
		this.AttackZombie();
		return null;
	}
}

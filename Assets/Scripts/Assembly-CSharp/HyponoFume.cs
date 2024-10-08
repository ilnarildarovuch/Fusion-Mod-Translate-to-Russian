using System;
using UnityEngine;

// Token: 0x020000AE RID: 174
public class HyponoFume : FumeShroom
{
	// Token: 0x0600035A RID: 858 RVA: 0x0001A3C0 File Offset: 0x000185C0
	protected override void AttackZombie()
	{
		bool flag = false;
		foreach (GameObject gameObject in this.board.zombieArray)
		{
			if (gameObject != null && gameObject.transform.position.x <= base.transform.position.x + 7f && gameObject.transform.position.x >= base.transform.position.x)
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (base.SearchUniqueZombie(component) && component.theZombieRow == this.thePlantRow)
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
				this.TrySetMindControl(this.zombieList[i]);
			}
		}
		this.zombieList.Clear();
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
		}
	}

	// Token: 0x0600035B RID: 859 RVA: 0x0001A4FC File Offset: 0x000186FC
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[21], position, Quaternion.Euler(0f, 90f, 0f), this.board.transform).GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = string.Format("particle{0}", this.thePlantRow);
		GameAPP.PlaySound(58, 0.5f);
		this.AttackZombie();
		return null;
	}

	// Token: 0x0600035C RID: 860 RVA: 0x0001A588 File Offset: 0x00018788
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
		int num3 = 0;
		bool[] controlledLevel = zombie.controlledLevel;
		for (int i = 0; i < controlledLevel.Length; i++)
		{
			if (controlledLevel[i])
			{
				num3++;
			}
		}
		num2 -= (float)num3 * 0.05f;
		num2 = Mathf.Sqrt(num2);
		float num4 = 0.75f;
		num4 -= (float)num3 * 0.05f;
		if (num2 < num4)
		{
			num2 = num4;
		}
		if (Random.value >= num2)
		{
			zombie.SetMindControl(false);
			return;
		}
		zombie.TakeDamage(1, 20);
	}
}

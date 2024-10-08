using System;
using UnityEngine;

// Token: 0x020000DA RID: 218
public class RandomZombie : ConeZombie
{
	// Token: 0x060003FE RID: 1022 RVA: 0x0001EDD4 File Offset: 0x0001CFD4
	protected override void FirstArmorFall()
	{
		Vector3 position = this.shadow.transform.position;
		Zombie component = this.SetRandomZombie(position).GetComponent<Zombie>();
		if (this.isMindControlled)
		{
			component.SetMindControl(true);
		}
		this.RandomEvent(component);
		base.FirstArmorFall();
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[11], new Vector3(base.transform.position.x, position.y + 1f, 0f), Quaternion.identity).transform.SetParent(GameAPP.board.transform);
		this.Die(2);
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x0001EE70 File Offset: 0x0001D070
	protected virtual GameObject SetRandomZombie(Vector3 pos)
	{
		GameObject result;
		if (Random.Range(0, 34) < 24)
		{
			result = this.board.GetComponent<CreateZombie>().SetZombie(0, this.theZombieRow, Random.Range(0, 24), pos.x, false);
		}
		else if (this.board.isEveStarted)
		{
			int num;
			do
			{
				num = Random.Range(100, 112);
			}
			while (num == 102);
			result = this.board.GetComponent<CreateZombie>().SetZombie(0, this.theZombieRow, num, pos.x, false);
		}
		else
		{
			result = this.board.GetComponent<CreateZombie>().SetZombie(0, this.theZombieRow, Random.Range(100, 112), pos.x, false);
		}
		return result;
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x0001EF1C File Offset: 0x0001D11C
	protected virtual void RandomEvent(Zombie zombie)
	{
		float num = (float)Random.Range(1, 6) / 5f;
		zombie.theHealth *= num;
		zombie.theMaxHealth = (int)((float)zombie.theMaxHealth * num);
		zombie.theFirstArmorHealth = (int)((float)zombie.theFirstArmorHealth * num);
		zombie.theFirstArmorMaxHealth = (int)((float)zombie.theFirstArmorMaxHealth * num);
		zombie.theSecondArmorHealth = (int)((float)zombie.theSecondArmorHealth * num);
		zombie.theSecondArmorMaxHealth = (int)((float)zombie.theSecondArmorMaxHealth * num);
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x0001EF98 File Offset: 0x0001D198
	protected override void FirstArmorBroken()
	{
		if (this.theFirstArmorHealth < this.theFirstArmorMaxHealth * 2 / 3 && this.theFirstArmorBroken < 1)
		{
			this.theFirstArmorBroken = 1;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[12];
		}
		if (this.theFirstArmorHealth < this.theFirstArmorMaxHealth / 3 && this.theFirstArmorBroken < 2)
		{
			this.theFirstArmorBroken = 2;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[13];
		}
	}
}

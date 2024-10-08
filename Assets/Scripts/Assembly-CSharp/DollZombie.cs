using System;
using UnityEngine;

// Token: 0x020000D3 RID: 211
public class DollZombie : ConeZombie
{
	// Token: 0x060003D2 RID: 978 RVA: 0x0001D2F4 File Offset: 0x0001B4F4
	protected override void Start()
	{
		base.Start();
		switch (this.theZombieType)
		{
		case 21:
			this.dollType = 0;
			return;
		case 22:
			this.dollType = 1;
			return;
		case 23:
			this.dollType = 2;
			return;
		default:
			return;
		}
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x0001D33C File Offset: 0x0001B53C
	protected override void FirstArmorFall()
	{
		Vector3 position = this.shadow.transform.position;
		int theZombieType = this.theZombieType;
		switch (this.dollType)
		{
		case 0:
			theZombieType = 22;
			break;
		case 1:
			theZombieType = 23;
			break;
		case 2:
			theZombieType = 2;
			break;
		}
		Zombie component = CreateZombie.Instance.SetZombie(0, this.theZombieRow, theZombieType, this.shadow.transform.position.x, false).GetComponent<Zombie>();
		if (this.isMindControlled)
		{
			component.SetMindControl(true);
		}
		base.FirstArmorFall();
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[11], new Vector3(base.transform.position.x, position.y + 1f, 0f), Quaternion.identity).transform.SetParent(GameAPP.board.transform);
		this.Die(2);
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x0001D420 File Offset: 0x0001B620
	protected override void FirstArmorBroken()
	{
		switch (this.dollType)
		{
		case 0:
			this.DiamondSpirte();
			return;
		case 1:
			this.GoldSpirte();
			return;
		case 2:
			this.SilverSpirte();
			return;
		default:
			return;
		}
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x0001D45C File Offset: 0x0001B65C
	private void DiamondSpirte()
	{
		if (this.theFirstArmorHealth < this.theFirstArmorMaxHealth * 2 / 3 && this.theFirstArmorBroken < 1)
		{
			this.theFirstArmorBroken = 1;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[44];
		}
		if (this.theFirstArmorHealth < this.theFirstArmorMaxHealth / 3 && this.theFirstArmorBroken < 2)
		{
			this.theFirstArmorBroken = 2;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[45];
		}
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x0001D4DC File Offset: 0x0001B6DC
	private void GoldSpirte()
	{
		if (this.theFirstArmorHealth < this.theFirstArmorMaxHealth * 2 / 3 && this.theFirstArmorBroken < 1)
		{
			this.theFirstArmorBroken = 1;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[46];
		}
		if (this.theFirstArmorHealth < this.theFirstArmorMaxHealth / 3 && this.theFirstArmorBroken < 2)
		{
			this.theFirstArmorBroken = 2;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[47];
		}
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x0001D55C File Offset: 0x0001B75C
	private void SilverSpirte()
	{
		if (this.theFirstArmorHealth < this.theFirstArmorMaxHealth * 2 / 3 && this.theFirstArmorBroken < 1)
		{
			this.theFirstArmorBroken = 1;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[48];
		}
		if (this.theFirstArmorHealth < this.theFirstArmorMaxHealth / 3 && this.theFirstArmorBroken < 2)
		{
			this.theFirstArmorBroken = 2;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[49];
		}
	}

	// Token: 0x040001DF RID: 479
	public int dollType;
}

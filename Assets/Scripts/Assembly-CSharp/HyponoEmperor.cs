using System;
using UnityEngine;

// Token: 0x02000089 RID: 137
public class HyponoEmperor : Plant
{
	// Token: 0x060002CD RID: 717 RVA: 0x00016A8E File Offset: 0x00014C8E
	protected override void Update()
	{
		base.Update();
		if (GameAPP.theGameStatus == 0)
		{
			this.SummonUpdate();
		}
		if (this.restHealth == 0)
		{
			this.Die();
		}
	}

	// Token: 0x060002CE RID: 718 RVA: 0x00016AB4 File Offset: 0x00014CB4
	private void SummonUpdate()
	{
		if (this.summonZombieTime > 0f)
		{
			this.summonZombieTime -= Time.deltaTime;
			if (this.summonZombieTime <= 0f)
			{
				this.anim.SetTrigger("summon");
				GameAPP.PlaySound(83, 0.5f);
				this.summonZombieTime = 30f;
			}
		}
	}

	// Token: 0x060002CF RID: 719 RVA: 0x00016B14 File Offset: 0x00014D14
	private void Summon()
	{
		if (GameAPP.theGameStatus == 0)
		{
			if (this.board.isEveStarted)
			{
				CreateZombie.Instance.SetZombie(0, this.thePlantRow, 105, this.shadow.transform.position.x, false).GetComponent<Zombie>().SetMindControl(true);
				return;
			}
			if (this.board.roadType[this.thePlantRow] == 1)
			{
				CreateZombie.Instance.SetZombie(0, this.thePlantRow, 14, this.shadow.transform.position.x, false).GetComponent<Zombie>().SetMindControl(true);
				return;
			}
			int num = Random.Range(0, 4);
			int num2;
			if (num == 0)
			{
				num2 = 15;
			}
			else if (num == 1)
			{
				num2 = 109;
			}
			else if (num == 2)
			{
				num2 = 18;
			}
			else
			{
				num2 = 104;
			}
			Zombie component = CreateZombie.Instance.SetZombie(0, this.thePlantRow, num2, this.shadow.transform.position.x, false).GetComponent<Zombie>();
			component.SetMindControl(true);
			if (num2 == 104)
			{
				component.TakeDamage(0, component.theSecondArmorMaxHealth);
			}
		}
	}

	// Token: 0x040001A1 RID: 417
	public int restHealth = 5;

	// Token: 0x040001A2 RID: 418
	[SerializeField]
	private float summonZombieTime = 30f;
}

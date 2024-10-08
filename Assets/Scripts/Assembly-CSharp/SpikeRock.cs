using System;
using UnityEngine;

// Token: 0x02000092 RID: 146
public class SpikeRock : Caltrop
{
	// Token: 0x060002EF RID: 751 RVA: 0x000179D5 File Offset: 0x00015BD5
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		this.SpriteUpdate();
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x000179E4 File Offset: 0x00015BE4
	private void SpriteUpdate()
	{
		if (this.thePlantHealth > 150 && this.thePlantHealth <= 300)
		{
			base.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
			base.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
			return;
		}
		if (this.thePlantHealth <= 150)
		{
			base.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
			base.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
			return;
		}
		base.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
		base.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x00017AC8 File Offset: 0x00015CC8
	protected override void KillCar()
	{
		Collider2D[] array = Physics2D.OverlapBoxAll(this.shadow.transform.position, new Vector2(1f, 1f), 0f);
		for (int i = 0; i < array.Length; i++)
		{
			DriverZombie driverZombie;
			if (array[i].TryGetComponent<DriverZombie>(out driverZombie) && driverZombie.theZombieRow == this.thePlantRow && !driverZombie.isMindControlled && driverZombie.theStatus != 1)
			{
				driverZombie.KillByCaltrop();
				this.thePlantHealth -= 50;
				GameAPP.PlaySound(77, 0.5f);
			}
		}
		if (this.thePlantHealth == 0)
		{
			this.Die();
		}
	}
}

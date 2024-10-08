using System;
using UnityEngine;

// Token: 0x02000096 RID: 150
public class SuperCherryChomper : SuperChomper
{
	// Token: 0x06000306 RID: 774 RVA: 0x0001856C File Offset: 0x0001676C
	public override void AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float x = position.x;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		this.board.GetComponent<CreateBullet>().SetBullet(x, y, thePlantRow, 3, 0).GetComponent<Bullet>().theBulletDamage = 0;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
	}

	// Token: 0x06000307 RID: 775 RVA: 0x000185D8 File Offset: 0x000167D8
	protected override void Bite(GameObject _zombie)
	{
		Zombie component = _zombie.GetComponent<Zombie>();
		if (component.theHealth + (float)component.theFirstArmorHealth <= 2000f)
		{
			component.TakeDamage(1, 8000);
			base.Recover(this.thePlantMaxHealth);
		}
		else
		{
			component.TakeDamage(1, 1000);
			base.Recover(1000);
		}
		GameAPP.PlaySound(49, 0.5f);
		this.zombie = null;
	}
}

using System;
using UnityEngine;

// Token: 0x0200006C RID: 108
public class KelpTorch : Tanglekelp
{
	// Token: 0x0600021C RID: 540 RVA: 0x00011160 File Offset: 0x0000F360
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Bullet bullet;
		if (collision.TryGetComponent<Bullet>(out bullet))
		{
			if (bullet.torchWood == base.gameObject || bullet.isZombieBullet)
			{
				return;
			}
			if (bullet.theMovingWay != 2 && bullet.theBulletRow != this.thePlantRow)
			{
				return;
			}
			int theBulletType = bullet.theBulletType;
			if (theBulletType != 0)
			{
				if (theBulletType == 29)
				{
					this.FireKelpBullet(bullet);
					return;
				}
			}
			else
			{
				this.board.YellowFirePea(bullet, this, false);
			}
		}
	}

	// Token: 0x0600021D RID: 541 RVA: 0x000111D4 File Offset: 0x0000F3D4
	private void FireKelpBullet(Bullet bullet)
	{
		GameAPP.PlaySound(61, 0.5f);
		Vector2 vector = base.transform.GetChild(0).position;
		Bullet component = CreateBullet.Instance.SetBullet(vector.x, vector.y, this.thePlantRow, 30, (bullet.theMovingWay == 2) ? 2 : 0).GetComponent<Bullet>();
		component.theBulletDamage = 40;
		component.torchWood = base.gameObject;
		component.isHot = true;
		bullet.Die();
	}
}

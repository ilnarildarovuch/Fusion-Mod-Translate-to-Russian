using System;
using UnityEngine;

// Token: 0x02000082 RID: 130
public class EliteTorchWood : Plant
{
	// Token: 0x060002B3 RID: 691 RVA: 0x00016140 File Offset: 0x00014340
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
			if (theBulletType == 0 || theBulletType == 7)
			{
				Board.Instance.OrangeFirePea(bullet, this);
			}
		}
	}
}

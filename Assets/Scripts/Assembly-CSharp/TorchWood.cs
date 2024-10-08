using System;
using UnityEngine;

// Token: 0x0200009A RID: 154
public class TorchWood : Plant
{
	// Token: 0x06000315 RID: 789 RVA: 0x00018D40 File Offset: 0x00016F40
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
				Board.Instance.YellowFirePea(bullet, this, false);
			}
		}
	}
}

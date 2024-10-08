using System;
using UnityEngine;

// Token: 0x02000087 RID: 135
public class FireSquash : Squash
{
	// Token: 0x060002C6 RID: 710 RVA: 0x0001679C File Offset: 0x0001499C
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

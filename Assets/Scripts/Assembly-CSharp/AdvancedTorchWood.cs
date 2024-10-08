using System;
using UnityEngine;

// Token: 0x0200007E RID: 126
public class AdvancedTorchWood : Plant
{
	// Token: 0x0600029A RID: 666 RVA: 0x00015738 File Offset: 0x00013938
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
				if (theBulletType == 1)
				{
					Board.Instance.FireCherry(bullet, this);
					return;
				}
				switch (theBulletType)
				{
				case 7:
					break;
				case 8:
					this.SunBullet(bullet);
					return;
				case 9:
				case 10:
					return;
				case 11:
					this.RedIronPea(bullet);
					return;
				default:
					return;
				}
			}
			Board.Instance.OrangeFirePea(bullet, this);
			return;
		}
	}

	// Token: 0x0600029B RID: 667 RVA: 0x000157DB File Offset: 0x000139DB
	private void RedIronPea(Bullet bullet)
	{
		GameAPP.PlaySound(61, 0.5f);
		bullet.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[39];
		bullet.theBulletDamage = 320;
		bullet.isHot = true;
	}

	// Token: 0x0600029C RID: 668 RVA: 0x00015810 File Offset: 0x00013A10
	private void SunBullet(Bullet bullet)
	{
		Vector2 vector = bullet.transform.localScale;
		bullet.transform.localScale = new Vector3(2f * vector.x, 2f * vector.y);
		bullet.theBulletDamage = 400;
		bullet.GetComponent<Coin>().sunPrice = 20;
		bullet.isHot = true;
	}

	// Token: 0x0600029D RID: 669 RVA: 0x00015875 File Offset: 0x00013A75
	public override void Die()
	{
		this.board.CreateFireLine(this.thePlantRow);
		base.Die();
	}
}

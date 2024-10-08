using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000084 RID: 132
public class EpicTorchWood : Plant
{
	// Token: 0x060002B8 RID: 696 RVA: 0x0001628E File Offset: 0x0001448E
	protected override void Update()
	{
		base.Update();
		if (this.fireCountDown > 0f)
		{
			this.fireCountDown -= Time.deltaTime;
			return;
		}
		this.fireCountDown = 1.5f;
		this.ReadyToFire();
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x000162C8 File Offset: 0x000144C8
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
			Board.Instance.RedFirePea(bullet, this);
			return;
		}
	}

	// Token: 0x060002BA RID: 698 RVA: 0x0001636C File Offset: 0x0001456C
	private void RedIronPea(Bullet bullet)
	{
		GameAPP.PlaySound(61, 0.5f);
		bullet.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[39];
		bullet.theBulletDamage = 320;
		bullet.GetComponent<Bullet>().isHot = true;
	}

	// Token: 0x060002BB RID: 699 RVA: 0x000163A4 File Offset: 0x000145A4
	private void SunBullet(Bullet bullet)
	{
		Vector2 vector = bullet.transform.localScale;
		bullet.transform.localScale = new Vector3(2f * vector.x, 2f * vector.y);
		bullet.GetComponent<Bullet>().theBulletDamage = 400;
		bullet.GetComponent<Coin>().sunPrice = 20;
		bullet.GetComponent<Bullet>().isHot = true;
	}

	// Token: 0x060002BC RID: 700 RVA: 0x00016413 File Offset: 0x00014613
	public override void Die()
	{
		this.board.CreateFireLine(this.thePlantRow);
		base.Die();
	}

	// Token: 0x060002BD RID: 701 RVA: 0x0001642C File Offset: 0x0001462C
	private void ReadyToFire()
	{
		Collider2D[] array = Physics2D.OverlapBoxAll(this.shadow.transform.position, new Vector2(4f, 4f), 0f, this.zombieLayer);
		bool flag = false;
		int num = 0;
		List<Zombie> list = new List<Zombie>();
		Collider2D[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			Zombie zombie;
			if (array2[i].TryGetComponent<Zombie>(out zombie) && zombie.theZombieRow == this.thePlantRow && !zombie.isMindControlled)
			{
				flag = true;
				list.Add(zombie);
			}
		}
		if (flag)
		{
			foreach (Zombie zombie2 in list)
			{
				if (num < 3)
				{
					Object.Instantiate<GameObject>(GameAPP.particlePrefab[35], zombie2.shadow.transform.position, Quaternion.identity, this.board.transform).GetComponent<SpriteRenderer>().sortingLayerName = string.Format("particle{0}", this.thePlantRow);
				}
				num++;
				zombie2.TakeDamage(1, 40);
				zombie2.Warm(0);
			}
			GameAPP.PlaySound(Random.Range(59, 61), 0.5f);
		}
	}

	// Token: 0x040001A0 RID: 416
	private float fireCountDown;
}

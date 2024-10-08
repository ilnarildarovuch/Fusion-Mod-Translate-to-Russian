using System;
using UnityEngine;

// Token: 0x02000088 RID: 136
public class FireSquashTorch : SquashTorch
{
	// Token: 0x060002C8 RID: 712 RVA: 0x0001680C File Offset: 0x00014A0C
	protected override void OnTriggerEnter2D(Collider2D collision)
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
			switch (bullet.theBulletType)
			{
			case 0:
				if (Board.Instance.RedFirePea(bullet, this))
				{
					this.fireTimes++;
					if (this.fireTimes > 20)
					{
						this.SummonSquash();
						return;
					}
				}
				break;
			case 1:
				if (Board.Instance.FireCherry(bullet, this))
				{
					this.fireTimes++;
					if (this.fireTimes > 20)
					{
						this.SummonSquash();
						return;
					}
				}
				break;
			case 2:
				break;
			case 3:
				if (this.SuperFireCherry(bullet))
				{
					this.fireTimes++;
					if (this.fireTimes > 20)
					{
						this.SummonSquash();
					}
				}
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x000168FC File Offset: 0x00014AFC
	private bool SuperFireCherry(Bullet bullet)
	{
		if (bullet.torchWood != this)
		{
			Vector3 position = bullet.transform.position;
			int thePlantRow = this.thePlantRow;
			CreateBullet.Instance.SetBullet(position.x, position.y, thePlantRow, 36, 0);
			GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
			bullet.Die();
			return true;
		}
		return false;
	}

	// Token: 0x060002CA RID: 714 RVA: 0x00016960 File Offset: 0x00014B60
	protected override void SummonSquash()
	{
		int num = 1;
		GameObject gameObject;
		do
		{
			if (this.board.boxType[this.thePlantColumn + num, this.thePlantRow] == 1)
			{
				CreatePlant.Instance.SetPlant(this.thePlantColumn + num, this.thePlantRow, 12, null, default(Vector2), false, 0f);
			}
			gameObject = CreatePlant.Instance.SetPlant(this.thePlantColumn + num, this.thePlantRow, 1054, null, default(Vector2), false, 0f);
			if (this.thePlantColumn + num > 9)
			{
				break;
			}
			num++;
		}
		while (gameObject == null);
		if (gameObject != null)
		{
			Vector2 vector = gameObject.GetComponent<Plant>().shadow.transform.position;
			vector = new Vector2(vector.x, vector.y + 0.5f);
			Object.Instantiate<GameObject>(GameAPP.particlePrefab[11], vector, Quaternion.identity, this.board.transform);
			this.fireTimes = 0;
		}
	}

	// Token: 0x060002CB RID: 715 RVA: 0x00016A6D File Offset: 0x00014C6D
	public override void Die()
	{
		this.board.CreateFireLine(this.thePlantRow);
		base.Die();
	}
}

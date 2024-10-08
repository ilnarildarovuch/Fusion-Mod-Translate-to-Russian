using System;
using UnityEngine;

// Token: 0x02000095 RID: 149
public class SquashTorch : Plant
{
	// Token: 0x06000303 RID: 771 RVA: 0x00018424 File Offset: 0x00016624
	protected virtual void OnTriggerEnter2D(Collider2D collision)
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
			if (bullet.theBulletType == 0 && Board.Instance.YellowFirePea(bullet, this, false))
			{
				this.fireTimes++;
				if (this.fireTimes > 60)
				{
					this.SummonSquash();
				}
			}
		}
	}

	// Token: 0x06000304 RID: 772 RVA: 0x000184A4 File Offset: 0x000166A4
	protected virtual void SummonSquash()
	{
		int num = 1;
		GameObject gameObject;
		do
		{
			gameObject = CreatePlant.Instance.SetPlant(this.thePlantColumn + num, this.thePlantRow, 1057, null, default(Vector2), false, 0f);
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

	// Token: 0x040001B9 RID: 441
	protected int fireTimes;
}

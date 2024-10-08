using System;
using UnityEngine;

// Token: 0x02000099 RID: 153
public class ThreeTorch : Plant
{
	// Token: 0x06000312 RID: 786 RVA: 0x0001895C File Offset: 0x00016B5C
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Bullet bullet;
		if (collision.TryGetComponent<Bullet>(out bullet))
		{
			if (bullet.torchWood == base.gameObject || bullet.isZombieBullet || bullet.isHot)
			{
				return;
			}
			if (bullet.theBulletType == 0)
			{
				this.FirePea(bullet);
			}
		}
	}

	// Token: 0x06000313 RID: 787 RVA: 0x000189A8 File Offset: 0x00016BA8
	private void FirePea(Bullet bullet)
	{
		Vector2 vector = base.transform.GetChild(0).position;
		GameObject gameObject = CreateBullet.Instance.SetBullet(vector.x, vector.y, this.thePlantRow, 0, 0);
		Vector2 vector2 = gameObject.transform.localScale;
		gameObject.transform.localScale = new Vector3(vector2.x * 0.75f, vector2.y * 0.75f);
		this.board.YellowFirePea(gameObject.GetComponent<Bullet>(), this, true);
		if (this.board.isEveStarted)
		{
			GameObject gameObject2 = CreateBullet.Instance.SetBullet(vector.x, vector.y + 0.3f, this.thePlantRow, 0, 0);
			Vector2 vector3 = gameObject2.transform.localScale;
			gameObject2.transform.localScale = new Vector3(vector3.x * 0.75f, vector3.y * 0.75f);
			this.board.YellowFirePea(gameObject2.GetComponent<Bullet>(), this, true);
			GameObject gameObject3 = CreateBullet.Instance.SetBullet(vector.x, vector.y - 0.3f, this.thePlantRow, 0, 0);
			Vector2 vector4 = gameObject3.transform.localScale;
			gameObject3.transform.localScale = new Vector3(vector4.x * 0.75f, vector4.y * 0.75f);
			this.board.YellowFirePea(gameObject3.GetComponent<Bullet>(), this, true);
		}
		else
		{
			if (this.thePlantRow != 0)
			{
				GameObject gameObject2 = CreateBullet.Instance.SetBullet(vector.x, vector.y, this.thePlantRow - 1, 0, 4);
				Vector2 vector5 = gameObject2.transform.localScale;
				gameObject2.transform.localScale = new Vector3(vector5.x * 0.75f, vector5.y * 0.75f);
				this.board.YellowFirePea(gameObject2.GetComponent<Bullet>(), this, true);
			}
			else
			{
				GameObject gameObject2 = CreateBullet.Instance.SetBullet(vector.x + 0.5f, vector.y, this.thePlantRow, 0, 0);
				Vector2 vector6 = gameObject2.transform.localScale;
				gameObject2.transform.localScale = new Vector3(vector6.x * 0.75f, vector6.y * 0.75f);
				this.board.YellowFirePea(gameObject2.GetComponent<Bullet>(), this, true);
			}
			if (this.thePlantRow != this.board.roadNum - 1)
			{
				GameObject gameObject3 = CreateBullet.Instance.SetBullet(vector.x, vector.y, this.thePlantRow + 1, 0, 5);
				Vector2 vector7 = gameObject3.transform.localScale;
				gameObject3.transform.localScale = new Vector3(vector7.x * 0.75f, vector7.y * 0.75f);
				this.board.YellowFirePea(gameObject3.GetComponent<Bullet>(), this, true);
			}
			else
			{
				GameObject gameObject3 = CreateBullet.Instance.SetBullet(vector.x + 0.5f, vector.y, this.thePlantRow, 0, 0);
				Vector2 vector8 = gameObject3.transform.localScale;
				gameObject3.transform.localScale = new Vector3(vector8.x * 0.75f, vector8.y * 0.75f);
				this.board.YellowFirePea(gameObject3.GetComponent<Bullet>(), this, true);
			}
		}
		bullet.Die();
	}
}

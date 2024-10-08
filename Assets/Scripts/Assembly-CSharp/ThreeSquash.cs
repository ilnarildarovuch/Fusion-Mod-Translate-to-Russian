using System;
using UnityEngine;

// Token: 0x020000C4 RID: 196
public class ThreeSquash : ThreePeater
{
	// Token: 0x0600039F RID: 927 RVA: 0x0001C134 File Offset: 0x0001A334
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("headPos2").Find("Shoot").transform.position;
		float num = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = CreateBullet.Instance.SetBullet(num, y, thePlantRow, 28, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 40;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		if (this.board.isEveStarted)
		{
			CreateBullet.Instance.SetBullet(num, y + 0.3f, thePlantRow, 28, 0);
			CreateBullet.Instance.SetBullet(num, y - 0.3f, thePlantRow, 28, 0);
			return gameObject;
		}
		if (this.thePlantRow == 0)
		{
			this.ShootLower(num, y, thePlantRow + 1);
			base.Invoke("ExtraBullet", 0.2f);
			return gameObject;
		}
		if (this.thePlantRow == this.board.roadNum - 1)
		{
			this.ShootUpper(num, y, thePlantRow - 1);
			base.Invoke("ExtraBullet", 0.2f);
			return gameObject;
		}
		this.ShootLower(num, y, thePlantRow + 1);
		this.ShootUpper(num, y, thePlantRow - 1);
		return gameObject;
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x0001C254 File Offset: 0x0001A454
	private void ShootUpper(float X, float Y, int row)
	{
		CreateBullet.Instance.SetBullet(X, Y, row, 28, 4).GetComponent<Bullet>().theBulletDamage = 40;
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x0001C272 File Offset: 0x0001A472
	private void ShootLower(float X, float Y, int row)
	{
		CreateBullet.Instance.SetBullet(X, Y, row, 28, 5).GetComponent<Bullet>().theBulletDamage = 40;
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x0001C290 File Offset: 0x0001A490
	private void ExtraBullet()
	{
		Vector3 position = base.transform.Find("headPos2").Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 28, 0).GetComponent<Bullet>().theBulletDamage = 40;
	}
}

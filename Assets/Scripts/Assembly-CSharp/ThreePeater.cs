using System;
using UnityEngine;

// Token: 0x020000C3 RID: 195
public class ThreePeater : Shooter
{
	// Token: 0x06000399 RID: 921 RVA: 0x0001BEC8 File Offset: 0x0001A0C8
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("headPos2").Find("Shoot").transform.position;
		float num = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = CreateBullet.Instance.SetBullet(num, y, thePlantRow, 0, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
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

	// Token: 0x0600039A RID: 922 RVA: 0x0001BFAB File Offset: 0x0001A1AB
	private void ShootUpper(float X, float Y, int row)
	{
		CreateBullet.Instance.SetBullet(X, Y, row, 0, 4).GetComponent<Bullet>().theBulletDamage = 20;
	}

	// Token: 0x0600039B RID: 923 RVA: 0x0001BFC8 File Offset: 0x0001A1C8
	private void ShootLower(float X, float Y, int row)
	{
		CreateBullet.Instance.SetBullet(X, Y, row, 0, 5).GetComponent<Bullet>().theBulletDamage = 20;
	}

	// Token: 0x0600039C RID: 924 RVA: 0x0001BFE8 File Offset: 0x0001A1E8
	private void ExtraBullet()
	{
		Vector3 position = base.transform.Find("headPos2").Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 0, 0).GetComponent<Bullet>().theBulletDamage = 20;
	}

	// Token: 0x0600039D RID: 925 RVA: 0x0001C054 File Offset: 0x0001A254
	protected override GameObject SearchZombie()
	{
		foreach (GameObject gameObject in GameAPP.board.GetComponent<Board>().zombieArray)
		{
			if (gameObject != null)
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (Mathf.Abs(component.theZombieRow - this.thePlantRow) <= 1 && component.shadow.transform.position.x < 9.2f && component.shadow.transform.position.x > this.shadow.transform.position.x && base.SearchUniqueZombie(component))
				{
					return gameObject;
				}
			}
		}
		return null;
	}
}

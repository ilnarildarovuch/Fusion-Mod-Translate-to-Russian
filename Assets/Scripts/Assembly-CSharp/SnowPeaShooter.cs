using System;
using UnityEngine;

// Token: 0x020000BF RID: 191
public class SnowPeaShooter : PeaShooter
{
	// Token: 0x0600038F RID: 911 RVA: 0x0001BBAC File Offset: 0x00019DAC
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.3f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 17, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(68, 0.5f);
		return gameObject;
	}
}

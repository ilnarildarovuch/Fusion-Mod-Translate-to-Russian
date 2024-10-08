using System;
using UnityEngine;

// Token: 0x020000C1 RID: 193
public class SuperHypno : PeaShooter
{
	// Token: 0x06000393 RID: 915 RVA: 0x0001BCA0 File Offset: 0x00019EA0
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 14, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 40;
		GameAPP.PlaySound(57, 0.5f);
		return gameObject;
	}
}

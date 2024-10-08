using System;
using UnityEngine;

// Token: 0x020000A6 RID: 166
public class CherryPeaShooter : Shooter
{
	// Token: 0x0600033B RID: 827 RVA: 0x00019650 File Offset: 0x00017850
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 1, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 60;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		return gameObject;
	}
}

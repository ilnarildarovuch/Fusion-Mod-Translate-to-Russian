using System;
using UnityEngine;

// Token: 0x020000B5 RID: 181
public class IronPeaShooter : PeaShooter
{
	// Token: 0x0600036E RID: 878 RVA: 0x0001AD10 File Offset: 0x00018F10
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 11, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 80;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		return gameObject;
	}
}

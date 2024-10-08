using System;
using UnityEngine;

// Token: 0x020000B3 RID: 179
public class IcePuff : PeaSmallPuff
{
	// Token: 0x06000369 RID: 873 RVA: 0x0001AAE4 File Offset: 0x00018CE4
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.3f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 18, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(68, 0.5f);
		return gameObject;
	}
}

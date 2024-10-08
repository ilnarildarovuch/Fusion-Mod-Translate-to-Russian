using System;
using UnityEngine;

// Token: 0x020000A9 RID: 169
public class FireCattail : CattailPlant
{
	// Token: 0x0600034A RID: 842 RVA: 0x00019C84 File Offset: 0x00017E84
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float x = position.x;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(x, y, thePlantRow, 35, 6);
		gameObject.GetComponent<Bullet>().theBulletDamage = 40;
		GameAPP.PlaySound(68, 0.5f);
		return gameObject;
	}
}

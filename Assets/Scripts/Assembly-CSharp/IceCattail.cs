using System;
using UnityEngine;

// Token: 0x020000AF RID: 175
public class IceCattail : CattailPlant
{
	// Token: 0x0600035E RID: 862 RVA: 0x0001A644 File Offset: 0x00018844
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float x = position.x;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(x, y, thePlantRow, 34, 6);
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(68, 0.5f);
		return gameObject;
	}
}

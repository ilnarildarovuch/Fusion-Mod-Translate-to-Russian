using System;
using UnityEngine;

// Token: 0x020000AC RID: 172
public class GatlingPea : Shooter
{
	// Token: 0x06000353 RID: 851 RVA: 0x0001A164 File Offset: 0x00018364
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("GatlingPea_head").GetChild(0).position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 0, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		return gameObject;
	}
}

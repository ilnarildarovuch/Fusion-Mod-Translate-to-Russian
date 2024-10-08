using System;
using UnityEngine;

// Token: 0x020000A5 RID: 165
public class CherryGatling : Shooter
{
	// Token: 0x06000339 RID: 825 RVA: 0x000195D0 File Offset: 0x000177D0
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("GatlingPea_head").GetChild(0).position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 1, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 60;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		return gameObject;
	}
}

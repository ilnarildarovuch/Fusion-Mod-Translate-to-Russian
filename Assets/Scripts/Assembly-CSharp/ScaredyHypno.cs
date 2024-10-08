using System;
using UnityEngine;

// Token: 0x020000BC RID: 188
public class ScaredyHypno : ScaredyShroom
{
	// Token: 0x06000386 RID: 902 RVA: 0x0001B82C File Offset: 0x00019A2C
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 13, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(57, 0.5f);
		return gameObject;
	}
}

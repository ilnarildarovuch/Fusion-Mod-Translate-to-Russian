using System;
using UnityEngine;

// Token: 0x020000C0 RID: 192
public class SuperCherryShooter : Shooter
{
	// Token: 0x06000391 RID: 913 RVA: 0x0001BC24 File Offset: 0x00019E24
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 3, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 0;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		return gameObject;
	}
}

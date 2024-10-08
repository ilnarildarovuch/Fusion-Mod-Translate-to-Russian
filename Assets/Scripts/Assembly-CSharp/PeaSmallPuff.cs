using System;
using UnityEngine;

// Token: 0x020000B9 RID: 185
public class PeaSmallPuff : Shooter
{
	// Token: 0x0600037B RID: 891 RVA: 0x0001B1F8 File Offset: 0x000193F8
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 10, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		return gameObject;
	}
}

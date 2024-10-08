using System;
using UnityEngine;

// Token: 0x020000A4 RID: 164
public class CattailPlant : CattailGirl
{
	// Token: 0x06000337 RID: 823 RVA: 0x00019558 File Offset: 0x00017758
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float x = position.x;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(x, y, thePlantRow, 33, 6);
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		return gameObject;
	}
}

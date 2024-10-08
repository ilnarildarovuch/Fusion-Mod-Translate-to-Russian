using System;
using UnityEngine;

// Token: 0x0200004A RID: 74
public class SuperCherryBullet : Bullet
{
	// Token: 0x0600015D RID: 349 RVA: 0x0000B24C File Offset: 0x0000944C
	protected override void HitZombie(GameObject zombie)
	{
		GameObject gameObject = GameAPP.particlePrefab[14];
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y, 0f);
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		gameObject2.GetComponent<BombCherry>().bombRow = this.theBulletRow;
		gameObject2.GetComponent<BombCherry>().bombType = 2;
		if (this.isZombieBullet)
		{
			gameObject2.GetComponent<BombCherry>().isFromZombie = true;
		}
		GameAPP.PlaySound(40, 0.2f);
		this.Die();
	}

	// Token: 0x0600015E RID: 350 RVA: 0x0000B300 File Offset: 0x00009500
	protected override void HitPlant(GameObject plant)
	{
		GameObject gameObject = GameAPP.particlePrefab[14];
		Vector3 position = new Vector3(base.transform.position.x - 0.5f, base.transform.position.y - 0.2f, 0f);
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		gameObject2.GetComponent<BombCherry>().bombRow = this.theBulletRow;
		gameObject2.GetComponent<BombCherry>().isFromZombie = true;
		gameObject2.GetComponent<BombCherry>().targetPlant = plant;
		GameAPP.PlaySound(40, 0.2f);
		this.Die();
	}
}

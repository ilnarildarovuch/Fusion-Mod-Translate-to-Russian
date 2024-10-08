using System;
using UnityEngine;

// Token: 0x02000033 RID: 51
public class CherrySquashBullet : SquashBullet
{
	// Token: 0x06000119 RID: 281 RVA: 0x000097D4 File Offset: 0x000079D4
	protected override void AttackZombie()
	{
		GameObject original = GameAPP.particlePrefab[14];
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y, 0f);
		GameObject gameObject = Object.Instantiate<GameObject>(original, position, Quaternion.identity);
		gameObject.transform.SetParent(GameAPP.board.transform);
		gameObject.GetComponent<BombCherry>().bombRow = this.theBulletRow;
		gameObject.GetComponent<BombCherry>().bombType = 2;
		GameAPP.PlaySound(40, 0.2f);
	}
}

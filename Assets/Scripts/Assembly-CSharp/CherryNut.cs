using System;
using UnityEngine;

// Token: 0x02000072 RID: 114
public class CherryNut : WallNut
{
	// Token: 0x06000260 RID: 608 RVA: 0x00014090 File Offset: 0x00012290
	public override void Die()
	{
		GameObject gameObject = GameAPP.particlePrefab[2];
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y + 0.5f, 0f);
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		gameObject2.GetComponent<BombCherry>().bombRow = this.thePlantRow;
		ScreenShake.TriggerShake(0.15f);
		GameAPP.PlaySound(40, 0.5f);
		base.Die();
	}
}

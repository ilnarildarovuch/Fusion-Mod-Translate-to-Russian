using System;
using UnityEngine;

// Token: 0x020000BB RID: 187
public class ScaredyDoom : ScaredyShroom
{
	// Token: 0x06000383 RID: 899 RVA: 0x0001B70C File Offset: 0x0001990C
	public override GameObject AnimShoot()
	{
		if (this.thePlantAttackInterval > 0.2f)
		{
			this.thePlantAttackInterval -= 0.1f;
		}
		else
		{
			this.thePlantAttackInterval = 0.2f;
		}
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 22, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(57, 0.5f);
		return gameObject;
	}

	// Token: 0x06000384 RID: 900 RVA: 0x0001B7A8 File Offset: 0x000199A8
	protected override void ScaredEvent()
	{
		if (this.thePlantAttackInterval == 0.2f)
		{
			Vector2 position = new Vector2(this.shadow.transform.position.x - 0.3f, this.shadow.transform.position.y + 0.3f);
			this.board.SetDoom(this.thePlantColumn, this.thePlantRow, true, position);
			return;
		}
		this.thePlantAttackInterval = 1.5f;
	}
}

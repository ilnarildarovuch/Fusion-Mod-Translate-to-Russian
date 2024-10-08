using System;
using UnityEngine;

// Token: 0x02000075 RID: 117
public class PeaChomper : Chomper
{
	// Token: 0x06000268 RID: 616 RVA: 0x000143A3 File Offset: 0x000125A3
	protected override void Update()
	{
		base.Update();
		if (this.attributeCountdown > 0f)
		{
			this.PlantShootUpdate();
		}
	}

	// Token: 0x06000269 RID: 617 RVA: 0x000143C0 File Offset: 0x000125C0
	public GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float x = position.x;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(x, y, thePlantRow, Random.Range(4, 6), 1);
		gameObject.GetComponent<Bullet>().theBulletDamage = 70;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		return gameObject;
	}
}

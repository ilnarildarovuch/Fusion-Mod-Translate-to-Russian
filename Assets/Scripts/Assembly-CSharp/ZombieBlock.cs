using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class ZombieBlock : NutBullet
{
	// Token: 0x06000167 RID: 359 RVA: 0x0000B490 File Offset: 0x00009690
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		int zombieBlockType = this.zombieBlockType;
		if (zombieBlockType > 1)
		{
			if (zombieBlockType == 2)
			{
				component.TakeDamage(0, 20);
				zombie.transform.position = new Vector3(zombie.transform.position.x + 0.25f, zombie.transform.position.y);
				this.hitTimes++;
			}
		}
		else
		{
			component.TakeDamage(0, this.theBulletDamage * 2);
		}
		GameObject gameObject = GameAPP.particlePrefab[12];
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, base.transform.position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		this.PlaySound(component);
	}

	// Token: 0x06000168 RID: 360 RVA: 0x0000B559 File Offset: 0x00009759
	protected override void Update()
	{
		base.Update();
		if (this.hitTimes > 5)
		{
			this.Die();
		}
	}
}

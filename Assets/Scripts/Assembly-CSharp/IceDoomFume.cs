using System;
using UnityEngine;

// Token: 0x020000B0 RID: 176
public class IceDoomFume : Shooter
{
	// Token: 0x06000360 RID: 864 RVA: 0x0001A6B8 File Offset: 0x000188B8
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 24, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		return gameObject;
	}

	// Token: 0x06000361 RID: 865 RVA: 0x0001A730 File Offset: 0x00018930
	protected override GameObject SearchZombie()
	{
		foreach (GameObject gameObject in GameAPP.board.GetComponent<Board>().zombieArray)
		{
			if (gameObject != null)
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (component.theZombieRow == this.thePlantRow && base.SearchUniqueZombie(component))
				{
					return gameObject;
				}
			}
		}
		return null;
	}
}

using System;
using UnityEngine;

// Token: 0x020000BE RID: 190
public class SmallPuff : Shooter
{
	// Token: 0x0600038C RID: 908 RVA: 0x0001BA1C File Offset: 0x00019C1C
	protected override GameObject SearchZombie()
	{
		foreach (GameObject gameObject in this.board.GetComponent<Board>().zombieArray)
		{
			if (gameObject != null)
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (!component.isMindControlled && component.theZombieRow == this.thePlantRow && component.shadow.transform.position.x < 9.2f && component.shadow.transform.position.x > this.shadow.transform.position.x && component.shadow.transform.position.x < this.shadow.transform.position.x + 4.5f && base.SearchUniqueZombie(component))
				{
					return gameObject;
				}
			}
		}
		return null;
	}

	// Token: 0x0600038D RID: 909 RVA: 0x0001BB34 File Offset: 0x00019D34
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 9, 3);
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(57, 0.5f);
		return gameObject;
	}
}

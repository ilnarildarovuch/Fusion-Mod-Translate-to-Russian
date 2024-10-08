using System;
using UnityEngine;

// Token: 0x020000C5 RID: 197
public class Threetang : Tanglekelp
{
	// Token: 0x060003A4 RID: 932 RVA: 0x0001C305 File Offset: 0x0001A505
	protected override void Update()
	{
		if (GameAPP.theGameStatus == 0)
		{
			this.PlantShootUpdate();
		}
		base.Update();
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x0001C31C File Offset: 0x0001A51C
	private void AnimShoot()
	{
		Vector3 position = this.shoot1.transform.position;
		Vector3 position2 = this.shoot1.transform.position;
		Vector3 position3 = this.shoot1.transform.position;
		GameObject gameObject = CreateBullet.Instance.SetBullet(position.x, position.y, this.thePlantRow + 1, 29, 5);
		GameObject gameObject2 = CreateBullet.Instance.SetBullet(position2.x, position2.y, this.thePlantRow, 29, 0);
		GameObject gameObject3 = CreateBullet.Instance.SetBullet(position3.x, position3.y, this.thePlantRow - 1, 29, 4);
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		gameObject2.GetComponent<Bullet>().theBulletDamage = 20;
		gameObject3.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x0001C3F8 File Offset: 0x0001A5F8
	protected override GameObject SearchZombie()
	{
		foreach (GameObject gameObject in GameAPP.board.GetComponent<Board>().zombieArray)
		{
			if (gameObject != null)
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (Mathf.Abs(component.theZombieRow - this.thePlantRow) <= 1 && component.shadow.transform.position.x < 9.2f && component.shadow.transform.position.x > this.shadow.transform.position.x && base.SearchUniqueZombie(component))
				{
					return gameObject;
				}
			}
		}
		return null;
	}

	// Token: 0x040001CC RID: 460
	public GameObject shoot1;

	// Token: 0x040001CD RID: 461
	public GameObject shoot2;

	// Token: 0x040001CE RID: 462
	public GameObject shoot3;
}

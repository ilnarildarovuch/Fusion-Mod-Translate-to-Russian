using System;
using UnityEngine;

// Token: 0x020000C2 RID: 194
public class SuperKelp : Tanglekelp
{
	// Token: 0x06000395 RID: 917 RVA: 0x0001BD18 File Offset: 0x00019F18
	protected override void Update()
	{
		base.Update();
		this.PlantShootUpdate();
	}

	// Token: 0x06000396 RID: 918 RVA: 0x0001BD28 File Offset: 0x00019F28
	private void AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").position;
		GameObject gameObject = CreateBullet.Instance.SetBullet(position.x, position.y, this.thePlantRow + 1, 32, 5);
		GameObject gameObject2 = CreateBullet.Instance.SetBullet(position.x, position.y, this.thePlantRow, 32, 0);
		GameObject gameObject3 = CreateBullet.Instance.SetBullet(position.x, position.y, this.thePlantRow - 1, 32, 4);
		gameObject.GetComponent<Bullet>().theBulletDamage = 40;
		gameObject2.GetComponent<Bullet>().theBulletDamage = 40;
		gameObject3.GetComponent<Bullet>().theBulletDamage = 40;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
	}

	// Token: 0x06000397 RID: 919 RVA: 0x0001BDE8 File Offset: 0x00019FE8
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
}

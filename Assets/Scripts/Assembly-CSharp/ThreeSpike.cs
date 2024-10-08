using System;
using UnityEngine;

// Token: 0x02000098 RID: 152
public class ThreeSpike : Caltrop
{
	// Token: 0x0600030A RID: 778 RVA: 0x00018655 File Offset: 0x00016855
	protected override void Update()
	{
		base.Update();
		if (GameAPP.theGameStatus == 0)
		{
			this.PlantShootUpdate();
		}
	}

	// Token: 0x0600030B RID: 779 RVA: 0x0001866C File Offset: 0x0001686C
	protected override void PlantShootUpdate()
	{
		this.shootTime -= Time.deltaTime;
		if (this.shootTime < 0f)
		{
			this.shootTime = this.shootMaxTime;
			this.shootTime += Random.Range(-0.1f, 0.1f);
			if (this.SearchZombie() != null)
			{
				this.anim.SetTrigger("shoot");
			}
		}
	}

	// Token: 0x0600030C RID: 780 RVA: 0x000186E0 File Offset: 0x000168E0
	private void AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float num = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		CreateBullet.Instance.SetBullet(num, y, thePlantRow, 12, 0).GetComponent<Bullet>().theBulletDamage = 5;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		if (this.thePlantRow == 0)
		{
			this.ShootLower(num, y, thePlantRow + 1);
			base.Invoke("ExtraBullet", 0.2f);
			return;
		}
		if (this.thePlantRow == this.board.roadNum - 1)
		{
			this.ShootUpper(num, y, thePlantRow - 1);
			base.Invoke("ExtraBullet", 0.2f);
			return;
		}
		this.ShootLower(num, y, thePlantRow + 1);
		this.ShootUpper(num, y, thePlantRow - 1);
	}

	// Token: 0x0600030D RID: 781 RVA: 0x000187B8 File Offset: 0x000169B8
	private void ShootUpper(float X, float Y, int row)
	{
		if (this.board.roadType[row] == 1)
		{
			return;
		}
		CreateBullet.Instance.SetBullet(X, Y, row, 12, 4).GetComponent<Bullet>().theBulletDamage = 5;
	}

	// Token: 0x0600030E RID: 782 RVA: 0x000187E6 File Offset: 0x000169E6
	private void ShootLower(float X, float Y, int row)
	{
		if (this.board.roadType[row] == 1)
		{
			return;
		}
		CreateBullet.Instance.SetBullet(X, Y, row, 12, 5).GetComponent<Bullet>().theBulletDamage = 5;
	}

	// Token: 0x0600030F RID: 783 RVA: 0x00018814 File Offset: 0x00016A14
	private void ExtraBullet()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float x = position.x;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		this.board.GetComponent<CreateBullet>().SetBullet(x, y, thePlantRow, 12, 0).GetComponent<Bullet>().theBulletDamage = 5;
	}

	// Token: 0x06000310 RID: 784 RVA: 0x00018870 File Offset: 0x00016A70
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

	// Token: 0x040001BA RID: 442
	private float shootTime;

	// Token: 0x040001BB RID: 443
	private float shootMaxTime = 1.5f;
}

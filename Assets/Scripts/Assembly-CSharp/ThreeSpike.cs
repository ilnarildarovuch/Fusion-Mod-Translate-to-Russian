using UnityEngine;

public class ThreeSpike : Caltrop
{
	private float shootTime;

	private float shootMaxTime = 1.5f;

	protected override void Update()
	{
		base.Update();
		if (GameAPP.theGameStatus == 0)
		{
			PlantShootUpdate();
		}
	}

	protected override void PlantShootUpdate()
	{
		shootTime -= Time.deltaTime;
		if (shootTime < 0f)
		{
			shootTime = shootMaxTime;
			shootTime += Random.Range(-0.1f, 0.1f);
			if (SearchZombie() != null)
			{
				anim.SetTrigger("shoot");
			}
		}
	}

	private void AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float num = position.x + 0.1f;
		float y = position.y;
		int num2 = thePlantRow;
		CreateBullet.Instance.SetBullet(num, y, num2, 12, 0).GetComponent<Bullet>().theBulletDamage = 5;
		GameAPP.PlaySound(Random.Range(3, 5));
		if (thePlantRow == 0)
		{
			ShootLower(num, y, num2 + 1);
			Invoke("ExtraBullet", 0.2f);
		}
		else if (thePlantRow == board.roadNum - 1)
		{
			ShootUpper(num, y, num2 - 1);
			Invoke("ExtraBullet", 0.2f);
		}
		else
		{
			ShootLower(num, y, num2 + 1);
			ShootUpper(num, y, num2 - 1);
		}
	}

	private void ShootUpper(float X, float Y, int row)
	{
		if (board.roadType[row] != 1)
		{
			CreateBullet.Instance.SetBullet(X, Y, row, 12, 4).GetComponent<Bullet>().theBulletDamage = 5;
		}
	}

	private void ShootLower(float X, float Y, int row)
	{
		if (board.roadType[row] != 1)
		{
			CreateBullet.Instance.SetBullet(X, Y, row, 12, 5).GetComponent<Bullet>().theBulletDamage = 5;
		}
	}

	private void ExtraBullet()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float x = position.x;
		float y = position.y;
		int theRow = thePlantRow;
		board.GetComponent<CreateBullet>().SetBullet(x, y, theRow, 12, 0).GetComponent<Bullet>()
			.theBulletDamage = 5;
	}

	protected override GameObject SearchZombie()
	{
		foreach (GameObject item in GameAPP.board.GetComponent<Board>().zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (Mathf.Abs(component.theZombieRow - thePlantRow) <= 1 && component.shadow.transform.position.x < 9.2f && component.shadow.transform.position.x > shadow.transform.position.x && SearchUniqueZombie(component))
				{
					return item;
				}
			}
		}
		return null;
	}
}

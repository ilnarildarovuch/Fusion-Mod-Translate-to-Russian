using UnityEngine;

public class ThreePeater : Shooter
{
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("headPos2").Find("Shoot").transform.position;
		float num = position.x + 0.1f;
		float y = position.y;
		int num2 = thePlantRow;
		GameObject obj = CreateBullet.Instance.SetBullet(num, y, num2, 0, 0);
		obj.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(Random.Range(3, 5));
		if (thePlantRow == 0)
		{
			ShootLower(num, y, num2 + 1);
			Invoke("ExtraBullet", 0.2f);
			return obj;
		}
		if (thePlantRow == board.roadNum - 1)
		{
			ShootUpper(num, y, num2 - 1);
			Invoke("ExtraBullet", 0.2f);
			return obj;
		}
		ShootLower(num, y, num2 + 1);
		ShootUpper(num, y, num2 - 1);
		return obj;
	}

	private void ShootUpper(float X, float Y, int row)
	{
		CreateBullet.Instance.SetBullet(X, Y, row, 0, 4).GetComponent<Bullet>().theBulletDamage = 20;
	}

	private void ShootLower(float X, float Y, int row)
	{
		CreateBullet.Instance.SetBullet(X, Y, row, 0, 5).GetComponent<Bullet>().theBulletDamage = 20;
	}

	private void ExtraBullet()
	{
		Vector3 position = base.transform.Find("headPos2").Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int theRow = thePlantRow;
		board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 0, 0).GetComponent<Bullet>()
			.theBulletDamage = 20;
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

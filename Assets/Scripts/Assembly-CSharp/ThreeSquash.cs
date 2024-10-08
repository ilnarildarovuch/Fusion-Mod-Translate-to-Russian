using UnityEngine;

public class ThreeSquash : ThreePeater
{
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("headPos2").Find("Shoot").transform.position;
		float num = position.x + 0.1f;
		float y = position.y;
		int num2 = thePlantRow;
		GameObject obj = CreateBullet.Instance.SetBullet(num, y, num2, 28, 0);
		obj.GetComponent<Bullet>().theBulletDamage = 40;
		GameAPP.PlaySound(Random.Range(3, 5));
		if (board.isEveStarted)
		{
			CreateBullet.Instance.SetBullet(num, y + 0.3f, num2, 28, 0);
			CreateBullet.Instance.SetBullet(num, y - 0.3f, num2, 28, 0);
			return obj;
		}
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
		CreateBullet.Instance.SetBullet(X, Y, row, 28, 4).GetComponent<Bullet>().theBulletDamage = 40;
	}

	private void ShootLower(float X, float Y, int row)
	{
		CreateBullet.Instance.SetBullet(X, Y, row, 28, 5).GetComponent<Bullet>().theBulletDamage = 40;
	}

	private void ExtraBullet()
	{
		Vector3 position = base.transform.Find("headPos2").Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int theRow = thePlantRow;
		board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 28, 0).GetComponent<Bullet>()
			.theBulletDamage = 40;
	}
}

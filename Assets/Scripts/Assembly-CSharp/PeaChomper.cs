using UnityEngine;

public class PeaChomper : Chomper
{
	protected override void Update()
	{
		base.Update();
		if (attributeCountdown > 0f)
		{
			PlantShootUpdate();
		}
	}

	public GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float x = position.x;
		float y = position.y;
		int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(x, y, theRow, Random.Range(4, 6), 1);
		obj.GetComponent<Bullet>().theBulletDamage = 70;
		GameAPP.PlaySound(Random.Range(3, 5));
		return obj;
	}
}

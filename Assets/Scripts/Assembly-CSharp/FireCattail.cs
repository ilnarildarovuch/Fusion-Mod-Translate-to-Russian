using UnityEngine;

public class FireCattail : CattailPlant
{
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float x = position.x;
		float y = position.y;
		int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(x, y, theRow, 35, 6);
		obj.GetComponent<Bullet>().theBulletDamage = 40;
		GameAPP.PlaySound(68);
		return obj;
	}
}

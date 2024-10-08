using UnityEngine;

public class IceCattail : CattailPlant
{
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float x = position.x;
		float y = position.y;
		int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(x, y, theRow, 34, 6);
		obj.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(68);
		return obj;
	}
}

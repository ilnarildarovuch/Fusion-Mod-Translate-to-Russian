using UnityEngine;

public class SuperHypno : PeaShooter
{
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 14, 0);
		obj.GetComponent<Bullet>().theBulletDamage = 40;
		GameAPP.PlaySound(57);
		return obj;
	}
}

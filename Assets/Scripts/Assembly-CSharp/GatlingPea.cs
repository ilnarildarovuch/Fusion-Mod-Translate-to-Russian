using UnityEngine;

public class GatlingPea : Shooter
{
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("GatlingPea_head").GetChild(0).position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 0, 0);
		obj.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(Random.Range(3, 5));
		return obj;
	}
}

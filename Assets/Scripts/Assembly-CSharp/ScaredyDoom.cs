using UnityEngine;

public class ScaredyDoom : ScaredyShroom
{
	public override GameObject AnimShoot()
	{
		if (thePlantAttackInterval > 0.2f)
		{
			thePlantAttackInterval -= 0.1f;
		}
		else
		{
			thePlantAttackInterval = 0.2f;
		}
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 22, 0);
		obj.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(57);
		return obj;
	}

	protected override void ScaredEvent()
	{
		if (thePlantAttackInterval == 0.2f)
		{
			Vector2 position = new Vector2(shadow.transform.position.x - 0.3f, shadow.transform.position.y + 0.3f);
			board.SetDoom(thePlantColumn, thePlantRow, setPit: true, position);
		}
		else
		{
			thePlantAttackInterval = 1.5f;
		}
	}
}

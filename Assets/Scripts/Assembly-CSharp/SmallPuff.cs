using UnityEngine;

public class SmallPuff : Shooter
{
	protected override GameObject SearchZombie()
	{
		foreach (GameObject item in board.GetComponent<Board>().zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (!component.isMindControlled && component.theZombieRow == thePlantRow && component.shadow.transform.position.x < 9.2f && component.shadow.transform.position.x > shadow.transform.position.x && component.shadow.transform.position.x < shadow.transform.position.x + 4.5f && SearchUniqueZombie(component))
				{
					return item;
				}
			}
		}
		return null;
	}

	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 9, 3);
		obj.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(57);
		return obj;
	}
}

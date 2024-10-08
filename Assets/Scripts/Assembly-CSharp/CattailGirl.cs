using UnityEngine;

public class CattailGirl : Shooter
{
	private float existTime;

	private readonly float floatStrength = 0.05f;

	private readonly float frequency = 1.2f;

	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot1").transform.position;
		float x = position.x;
		float y = position.y;
		int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(x, y, theRow, 20, 6);
		obj.GetComponent<Bullet>().theBulletDamage = 40;
		GameAPP.PlaySound(Random.Range(3, 5));
		return obj;
	}

	private GameObject AnimShoot2()
	{
		Vector3 position = base.transform.Find("Shoot2").transform.position;
		float x = position.x;
		float y = position.y;
		int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(x, y, theRow, 20, 6);
		obj.GetComponent<Bullet>().theBulletDamage = 40;
		GameAPP.PlaySound(Random.Range(3, 5));
		return obj;
	}

	protected override void Update()
	{
		base.Update();
		PostionUpdate();
	}

	private void PostionUpdate()
	{
		existTime += Time.deltaTime;
		float num = Mathf.Sin(existTime * frequency) * floatStrength;
		base.transform.position = startPos + Vector3.up * num;
	}

	protected override GameObject SearchZombie()
	{
		foreach (GameObject item in GameAPP.board.GetComponent<Board>().zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (component.shadow.transform.position.x < 9.2f && SearchUniqueZombie(component))
				{
					return item;
				}
			}
		}
		return null;
	}
}

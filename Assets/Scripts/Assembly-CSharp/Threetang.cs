using UnityEngine;

public class Threetang : Tanglekelp
{
	public GameObject shoot1;

	public GameObject shoot2;

	public GameObject shoot3;

	protected override void Update()
	{
		if (GameAPP.theGameStatus == 0)
		{
			PlantShootUpdate();
		}
		base.Update();
	}

	private void AnimShoot()
	{
		Vector3 position = shoot1.transform.position;
		Vector3 position2 = shoot1.transform.position;
		Vector3 position3 = shoot1.transform.position;
		GameObject gameObject = CreateBullet.Instance.SetBullet(position.x, position.y, thePlantRow + 1, 29, 5);
		GameObject gameObject2 = CreateBullet.Instance.SetBullet(position2.x, position2.y, thePlantRow, 29, 0);
		GameObject obj = CreateBullet.Instance.SetBullet(position3.x, position3.y, thePlantRow - 1, 29, 4);
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		gameObject2.GetComponent<Bullet>().theBulletDamage = 20;
		obj.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(Random.Range(3, 5));
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

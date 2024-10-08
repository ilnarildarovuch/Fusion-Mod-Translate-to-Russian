using UnityEngine;

public class SuperKelp : Tanglekelp
{
	protected override void Update()
	{
		base.Update();
		PlantShootUpdate();
	}

	private void AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").position;
		GameObject gameObject = CreateBullet.Instance.SetBullet(position.x, position.y, thePlantRow + 1, 32, 5);
		GameObject gameObject2 = CreateBullet.Instance.SetBullet(position.x, position.y, thePlantRow, 32, 0);
		GameObject obj = CreateBullet.Instance.SetBullet(position.x, position.y, thePlantRow - 1, 32, 4);
		gameObject.GetComponent<Bullet>().theBulletDamage = 40;
		gameObject2.GetComponent<Bullet>().theBulletDamage = 40;
		obj.GetComponent<Bullet>().theBulletDamage = 40;
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

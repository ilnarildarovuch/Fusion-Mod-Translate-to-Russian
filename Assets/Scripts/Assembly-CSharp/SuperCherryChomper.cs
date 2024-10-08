using UnityEngine;

public class SuperCherryChomper : SuperChomper
{
	public override void AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float x = position.x;
		float y = position.y;
		int theRow = thePlantRow;
		board.GetComponent<CreateBullet>().SetBullet(x, y, theRow, 3, 0).GetComponent<Bullet>()
			.theBulletDamage = 0;
		GameAPP.PlaySound(Random.Range(3, 5));
	}

	protected override void Bite(GameObject _zombie)
	{
		Zombie component = _zombie.GetComponent<Zombie>();
		if (component.theHealth + (float)component.theFirstArmorHealth <= 2000f)
		{
			component.TakeDamage(1, 8000);
			Recover(thePlantMaxHealth);
		}
		else
		{
			component.TakeDamage(1, 1000);
			Recover(1000);
		}
		GameAPP.PlaySound(49);
		zombie = null;
	}
}

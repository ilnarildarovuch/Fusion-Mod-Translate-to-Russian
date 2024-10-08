using UnityEngine;

public class SquashSpike : Caltrop
{
	protected override void AnimAttack()
	{
		KillCar();
		Collider2D[] array = Physics2D.OverlapBoxAll(shadow.transform.position, new Vector2(1f, 1f), 0f);
		bool flag = false;
		Collider2D[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			if (array2[i].TryGetComponent<Zombie>(out var component) && component.theZombieRow == thePlantRow && SearchUniqueZombie(component))
			{
				flag = true;
				component.TakeDamage(4, 20);
				component.transform.position = new Vector3(component.transform.position.x + 0.2f, component.transform.position.y);
			}
		}
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3));
			GameAPP.PlaySound(Random.Range(72, 74));
			CreateBullet.Instance.SetBullet(shadow.transform.position.x, shadow.transform.position.y + 0.5f, thePlantRow, 28, 0).GetComponent<Bullet>().theBulletDamage = 40;
		}
	}
}

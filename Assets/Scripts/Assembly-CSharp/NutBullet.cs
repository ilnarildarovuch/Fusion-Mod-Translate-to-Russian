using UnityEngine;

public class NutBullet : Bullet
{
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, theBulletDamage);
		GameObject gameObject = GameAPP.particlePrefab[7];
		GameObject obj = Object.Instantiate(gameObject, base.transform.position, Quaternion.identity);
		obj.transform.SetParent(GameAPP.board.transform);
		obj.name = gameObject.name;
		PlaySound(component);
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag("Zombie") || (collision.TryGetComponent<PolevaulterZombie>(out var component) && component.polevaulterStatus == 1))
		{
			return;
		}
		Zombie component2 = collision.GetComponent<Zombie>();
		if (component2.theZombieRow != theBulletRow || component2.isMindControlled)
		{
			return;
		}
		foreach (GameObject item in Z)
		{
			if (item != null && item == zombie)
			{
				return;
			}
		}
		Z.Add(collision.gameObject);
		HitZombie(collision.gameObject);
	}
}

using UnityEngine;

public class IronPeaSmall : Bullet
{
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		foreach (GameObject item in Z)
		{
			if (item == zombie)
			{
				return;
			}
		}
		component.TakeDamage(1, theBulletDamage);
		Z.Add(zombie);
		GameObject gameObject = GameAPP.particlePrefab[18];
		GameObject obj = Object.Instantiate(gameObject, base.transform.position, Quaternion.identity);
		obj.transform.SetParent(GameAPP.board.transform);
		obj.name = gameObject.name;
		PlaySound(component);
	}

	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Zombie") && (!collision.TryGetComponent<PolevaulterZombie>(out var component) || component.polevaulterStatus != 1))
		{
			HitZombie(collision.gameObject);
		}
	}
}

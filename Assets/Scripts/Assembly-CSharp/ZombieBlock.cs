using UnityEngine;

public class ZombieBlock : NutBullet
{
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		switch (zombieBlockType)
		{
		case 0:
		case 1:
			component.TakeDamage(0, theBulletDamage * 2);
			break;
		case 2:
			component.TakeDamage(0, 20);
			zombie.transform.position = new Vector3(zombie.transform.position.x + 0.25f, zombie.transform.position.y);
			hitTimes++;
			break;
		}
		GameObject gameObject = GameAPP.particlePrefab[12];
		GameObject obj = Object.Instantiate(gameObject, base.transform.position, Quaternion.identity);
		obj.transform.SetParent(GameAPP.board.transform);
		obj.name = gameObject.name;
		PlaySound(component);
	}

	protected override void Update()
	{
		base.Update();
		if (hitTimes > 5)
		{
			Die();
		}
	}
}

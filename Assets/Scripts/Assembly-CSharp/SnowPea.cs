using UnityEngine;

public class SnowPea : Pea
{
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(2, theBulletDamage);
		GameObject gameObject = GameAPP.particlePrefab[24];
		GameObject obj = Object.Instantiate(gameObject, base.transform.position, Quaternion.identity);
		obj.transform.SetParent(GameAPP.board.transform);
		obj.name = gameObject.name;
		PlaySound(component);
		Die();
	}
}

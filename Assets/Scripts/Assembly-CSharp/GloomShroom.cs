using UnityEngine;

public class GloomShroom : Shooter
{
	protected Collider2D[] colliders;

	protected GameObject center;

	protected readonly float range = 2f;

	protected override void Awake()
	{
		base.Awake();
		center = base.transform.Find("Shoot").gameObject;
	}

	protected override GameObject SearchZombie()
	{
		colliders = Physics2D.OverlapCircleAll(center.transform.position, range, zombieLayer);
		Collider2D[] array = colliders;
		foreach (Collider2D collider2D in array)
		{
			if (collider2D.TryGetComponent<Zombie>(out var component) && Mathf.Abs(component.theZombieRow - thePlantRow) <= 1 && SearchUniqueZombie(component))
			{
				return collider2D.gameObject;
			}
		}
		return null;
	}

	public override GameObject AnimShoot()
	{
		Object.Instantiate(GameAPP.particlePrefab[37], center.transform.position, Quaternion.identity, board.transform);
		AttackZombie();
		return null;
	}

	protected virtual void AttackZombie()
	{
		bool flag = false;
		colliders = Physics2D.OverlapCircleAll(center.transform.position, range, zombieLayer);
		Collider2D[] array = colliders;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].TryGetComponent<Zombie>(out var component) && Mathf.Abs(component.theZombieRow - thePlantRow) <= 1 && AttackUniqueZombie(component))
			{
				flag = true;
				zombieList.Add(component);
			}
		}
		for (int num = zombieList.Count - 1; num >= 0; num--)
		{
			if (zombieList[num] != null)
			{
				zombieList[num].TakeDamage(1, 20);
			}
		}
		zombieList.Clear();
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3));
		}
	}
}

using UnityEngine;

public class IceGloom : GloomShroom
{
	protected override void AttackZombie()
	{
		bool flag = false;
		colliders = Physics2D.OverlapCircleAll(center.transform.position, range, zombieLayer);
		Collider2D[] array = colliders;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].TryGetComponent<Zombie>(out var component) && Mathf.Abs(component.theZombieRow - thePlantRow) <= 1 && SearchUniqueZombie(component))
			{
				flag = true;
				zombieList.Add(component);
			}
		}
		for (int num = zombieList.Count - 1; num >= 0; num--)
		{
			if (zombieList[num] != null)
			{
				zombieList[num].TakeDamage(3, 20);
				zombieList[num].AddfreezeLevel(5);
			}
		}
		zombieList.Clear();
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3));
		}
	}

	public override GameObject AnimShoot()
	{
		Object.Instantiate(GameAPP.particlePrefab[39], center.transform.position, Quaternion.identity, board.transform);
		AttackZombie();
		return null;
	}
}

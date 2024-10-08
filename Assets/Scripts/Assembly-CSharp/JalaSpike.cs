using UnityEngine;

public class JalaSpike : Caltrop
{
	protected override void KillCar()
	{
		Collider2D[] array = Physics2D.OverlapBoxAll(shadow.transform.position, new Vector2(1f, 1f), 0f);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].TryGetComponent<DriverZombie>(out var component) && component.theZombieRow == thePlantRow && !component.isMindControlled && component.theStatus != 1)
			{
				component.Die(2);
				GameAPP.PlaySound(77);
				Die();
			}
		}
	}

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
				component.SetJalaed();
			}
		}
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3));
		}
	}
}

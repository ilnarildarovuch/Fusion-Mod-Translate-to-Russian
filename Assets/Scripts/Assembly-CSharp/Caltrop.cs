using UnityEngine;

public class Caltrop : Plant
{
	protected override void Update()
	{
		base.Update();
		if (thePlantAttackCountDown > 0f)
		{
			thePlantAttackCountDown -= Time.deltaTime;
			if (thePlantAttackCountDown <= 0f)
			{
				ReadyToAttack();
				thePlantAttackCountDown = thePlantAttackInterval + Random.Range(-0.1f, 0.1f);
			}
		}
	}

	protected virtual void ReadyToAttack()
	{
		Collider2D[] array = Physics2D.OverlapBoxAll(shadow.transform.position, new Vector2(1f, 1f), 0f);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].TryGetComponent<Zombie>(out var component) && SearchUniqueZombie(component) && component.theZombieRow == thePlantRow)
			{
				anim.SetTrigger("attack");
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Zombie>(out var component) && component.theStatus != 1 && component.theZombieRow == thePlantRow && !component.isMindControlled)
		{
			int theZombieType = component.theZombieType;
			if (theZombieType == 16 || theZombieType == 18 || theZombieType == 201)
			{
				anim.SetTrigger("attack");
			}
		}
	}

	protected virtual void KillCar()
	{
		Collider2D[] array = Physics2D.OverlapBoxAll(shadow.transform.position, new Vector2(1f, 1f), 0f);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].TryGetComponent<DriverZombie>(out var component) && component.theZombieRow == thePlantRow && !component.isMindControlled && component.theStatus != 1)
			{
				component.KillByCaltrop();
				GameAPP.PlaySound(77);
				Die();
			}
		}
	}

	protected virtual void AnimAttack()
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
			}
		}
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3));
		}
	}
}

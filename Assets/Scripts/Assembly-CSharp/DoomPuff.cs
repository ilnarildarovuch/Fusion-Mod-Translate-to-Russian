using UnityEngine;

public class DoomPuff : Plant
{
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Zombie>(out var component) && component.theZombieRow == thePlantRow && component.theStatus != 1 && !component.isMindControlled)
		{
			GameAPP.PlaySound(41);
			ScreenShake.TriggerShake(0.1f);
			Object.Instantiate(GameAPP.particlePrefab[27], shadow.transform.position, Quaternion.identity, board.transform);
			AttackZombie();
			Die();
		}
	}

	private void AttackZombie()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(shadow.transform.position, 1f);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].gameObject.TryGetComponent<Zombie>(out var component) && component.theZombieRow == thePlantRow && !component.isMindControlled)
			{
				if (component.theHealth > 1800f)
				{
					component.TakeDamage(10, 1800);
				}
				else
				{
					component.Charred();
				}
			}
		}
	}
}

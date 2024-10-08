using UnityEngine;

public class FireSpike : Plant
{
	protected override void Update()
	{
		base.Update();
		FireUpdate();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Bullet>(out var component) && !(component.torchWood == base.gameObject) && !component.isZombieBullet && (component.theMovingWay == 2 || component.theBulletRow == thePlantRow) && component.theBulletType == 10)
		{
			Board.Instance.FirePuffPea(component, this);
		}
	}

	private void FireUpdate()
	{
		if (thePlantAttackCountDown > 0f)
		{
			thePlantAttackCountDown -= Time.deltaTime;
			if (thePlantAttackCountDown <= 0f)
			{
				SummonFire();
				thePlantAttackCountDown = thePlantAttackInterval;
			}
		}
	}

	private void SummonFire()
	{
		Collider2D[] array = Physics2D.OverlapBoxAll(shadow.transform.position, new Vector2(1f, 1f), 0f);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].TryGetComponent<Zombie>(out var component) && component.theZombieRow == thePlantRow && !component.isMindControlled)
			{
				GameAPP.PlaySound(Random.Range(59, 61));
				int theZombieType = component.theZombieType;
				if (theZombieType == 16 || theZombieType == 18 || theZombieType == 201)
				{
					component.GetComponent<DriverZombie>().KillByCaltrop();
					thePlantHealth = 0;
				}
				else
				{
					component.TakeDamage(1, 20);
					component.Warm();
				}
			}
		}
	}
}

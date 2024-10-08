using UnityEngine;

public class FinalFume : IceDoomFume
{
	private ParticleSystem particle;

	private ParticleSystem.EmissionModule emission;

	protected override void Awake()
	{
		base.Awake();
		particle = base.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
		emission = particle.emission;
	}

	protected override void PlantShootUpdate()
	{
		thePlantAttackCountDown -= Time.deltaTime;
		if (thePlantAttackCountDown < 0f)
		{
			thePlantAttackCountDown = thePlantAttackInterval;
			thePlantAttackCountDown += Random.Range(-0.1f, 0.1f);
			if (SearchZombie() != null)
			{
				anim.SetBool("shooting", value: true);
			}
			else
			{
				anim.SetBool("shooting", value: false);
			}
		}
	}

	private void EnableParticle()
	{
		particle.GetComponent<Renderer>().sortingLayerName = $"particle{thePlantRow}";
		emission.enabled = true;
	}

	private void DisableParticle()
	{
		emission.enabled = false;
	}

	private void AttackZombie()
	{
		float x = shadow.transform.position.x;
		bool flag = false;
		foreach (GameObject item in board.zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (component.theZombieRow == thePlantRow && component.shadow.transform.position.x > x && AttackUniqueZombie(component))
				{
					zombieList.Add(component);
					flag = true;
				}
			}
		}
		foreach (Zombie zombie in zombieList)
		{
			if (zombie != null && !zombie.isMindControlled)
			{
				TrySetMindControl(zombie);
			}
		}
		zombieList.Clear();
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3));
		}
	}

	private void TrySetMindControl(Zombie zombie)
	{
		float num = zombie.theFirstArmorMaxHealth + zombie.theMaxHealth;
		float num2 = ((float)zombie.theFirstArmorHealth + zombie.theHealth) / num;
		num2 = ((!((double)num2 > 0.5)) ? (num2 / 0.5f) : 1f);
		num2 = Mathf.Sqrt(num2);
		float num3 = 0.75f;
		if (num2 < num3)
		{
			num2 = num3;
		}
		if (Random.value >= num2)
		{
			zombie.SetMindControl(mustControl: true);
			SmallDoom(zombie);
			GameAPP.PlaySound(70);
		}
		else
		{
			zombie.TakeDamage(3, 60);
		}
	}

	private void SmallDoom(Zombie z)
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(z.shadow.transform.position, 1f, zombieLayer);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].gameObject.TryGetComponent<Zombie>(out var component) && component.theZombieRow == thePlantRow && !component.isMindControlled)
			{
				component.TakeDamage(10, 500);
			}
		}
		Object.Instantiate(GameAPP.particlePrefab[27], z.shadow.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, board.transform);
	}
}

using UnityEngine;

public class DoomFume : Plant
{
	public float ShootCD;

	protected override void Update()
	{
		base.Update();
		if (ShootCD > 0f)
		{
			ShootCD -= Time.deltaTime;
			if (ShootCD <= 0f)
			{
				ShootCD = 0f;
				anim.SetTrigger("backToIdle");
			}
		}
	}

	public void Shoot()
	{
		if (ShootCD == 0f)
		{
			anim.SetTrigger("shoot");
			ShootCD = 60f;
		}
	}

	private void AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		Object.Instantiate(GameAPP.particlePrefab[31], position, Quaternion.Euler(0f, 90f, 0f), board.transform).GetComponent<ParticleSystem>().GetComponent<Renderer>()
			.sortingLayerName = $"particle{thePlantRow}";
		GameAPP.PlaySound(58);
		AttackZombie();
	}

	private void AttackZombie()
	{
		bool flag = false;
		foreach (GameObject item in board.zombieArray)
		{
			if (item != null)
			{
				Zombie component = item.GetComponent<Zombie>();
				if (!(component.shadow.transform.position.x < shadow.transform.position.x) && SearchUniqueZombie(component) && component.theZombieRow == thePlantRow)
				{
					zombieList.Add(component);
					flag = true;
				}
			}
		}
		for (int num = zombieList.Count - 1; num >= 0; num--)
		{
			if (zombieList[num] != null)
			{
				zombieList[num].TakeDamage(1, 1800);
			}
		}
		zombieList.Clear();
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3));
		}
	}
}

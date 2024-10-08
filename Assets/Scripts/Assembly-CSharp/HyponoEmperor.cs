using UnityEngine;

public class HyponoEmperor : Plant
{
	public int restHealth = 5;

	[SerializeField]
	private float summonZombieTime = 30f;

	protected override void Update()
	{
		base.Update();
		if (GameAPP.theGameStatus == 0)
		{
			SummonUpdate();
		}
		if (restHealth == 0)
		{
			Die();
		}
	}

	private void SummonUpdate()
	{
		if (summonZombieTime > 0f)
		{
			summonZombieTime -= Time.deltaTime;
			if (summonZombieTime <= 0f)
			{
				anim.SetTrigger("summon");
				GameAPP.PlaySound(83);
				summonZombieTime = 30f;
			}
		}
	}

	private void Summon()
	{
		if (GameAPP.theGameStatus != 0)
		{
			return;
		}
		if (board.isEveStarted)
		{
			CreateZombie.Instance.SetZombie(0, thePlantRow, 105, shadow.transform.position.x).GetComponent<Zombie>().SetMindControl(mustControl: true);
			return;
		}
		if (board.roadType[thePlantRow] == 1)
		{
			CreateZombie.Instance.SetZombie(0, thePlantRow, 14, shadow.transform.position.x).GetComponent<Zombie>().SetMindControl(mustControl: true);
			return;
		}
		int num = Random.Range(0, 4) switch
		{
			0 => 15, 
			1 => 109, 
			2 => 18, 
			_ => 104, 
		};
		Zombie component = CreateZombie.Instance.SetZombie(0, thePlantRow, num, shadow.transform.position.x).GetComponent<Zombie>();
		component.SetMindControl(mustControl: true);
		if (num == 104)
		{
			component.TakeDamage(0, component.theSecondArmorMaxHealth);
		}
	}
}

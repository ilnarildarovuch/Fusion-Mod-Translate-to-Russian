using UnityEngine;

public class SquashBullet : Bullet
{
	protected float landY;

	protected override void Awake()
	{
		base.Awake();
		g = 10f;
		Vx = Random.Range(1.8f, 2.2f);
		Vy = Random.Range(4.8f, 5.2f);
		Y = Vy;
	}

	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, theBulletDamage);
		theMovingWay = -1;
		Vy *= -0.75f;
		GetComponent<BoxCollider2D>().enabled = false;
		originY = shadow.transform.position.y;
		PlaySound(component);
		if (Board.Instance.isEveStarted)
		{
			SetShadowPosition();
		}
		landY = shadow.transform.position.y + 0.3f;
	}

	protected override void Update()
	{
		base.Update();
		if (theMovingWay == -1)
		{
			PositionUpdate();
		}
	}

	private void PositionUpdate()
	{
		base.transform.Translate(new Vector3(Vx * Time.deltaTime, 0f));
		base.transform.GetChild(0).transform.Translate(new Vector3(0f, Vy * Time.deltaTime));
		Vy -= g * Time.deltaTime;
		if (base.transform.GetChild(0).position.y < landY && Vy < 0f)
		{
			Vy = 0f - Vy;
			AttackZombie();
			if (Board.Instance.roadType[theBulletRow] == 1)
			{
				Object.Instantiate(GameAPP.particlePrefab[32], shadow.transform.position, Quaternion.identity, Board.Instance.transform);
			}
		}
	}

	protected virtual void AttackZombie()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 0.5f);
		bool flag = false;
		Collider2D[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			if (array2[i].TryGetComponent<Zombie>(out var component) && component.theZombieRow == theBulletRow && !component.isMindControlled)
			{
				component.TakeDamage(1, theBulletDamage);
				flag = true;
			}
		}
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3));
		}
	}
}

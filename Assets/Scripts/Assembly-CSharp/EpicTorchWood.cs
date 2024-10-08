using System.Collections.Generic;
using UnityEngine;

public class EpicTorchWood : Plant
{
	private float fireCountDown;

	protected override void Update()
	{
		base.Update();
		if (fireCountDown > 0f)
		{
			fireCountDown -= Time.deltaTime;
			return;
		}
		fireCountDown = 1.5f;
		ReadyToFire();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Bullet>(out var component) && !(component.torchWood == base.gameObject) && !component.isZombieBullet && (component.theMovingWay == 2 || component.theBulletRow == thePlantRow))
		{
			switch (component.theBulletType)
			{
			case 0:
			case 7:
				Board.Instance.RedFirePea(component, this);
				break;
			case 1:
				Board.Instance.FireCherry(component, this);
				break;
			case 11:
				RedIronPea(component);
				break;
			case 8:
				SunBullet(component);
				break;
			}
		}
	}

	private void RedIronPea(Bullet bullet)
	{
		GameAPP.PlaySound(61);
		bullet.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[39];
		bullet.theBulletDamage = 320;
		bullet.GetComponent<Bullet>().isHot = true;
	}

	private void SunBullet(Bullet bullet)
	{
		Vector2 vector = bullet.transform.localScale;
		bullet.transform.localScale = new Vector3(2f * vector.x, 2f * vector.y);
		bullet.GetComponent<Bullet>().theBulletDamage = 400;
		bullet.GetComponent<Coin>().sunPrice = 20;
		bullet.GetComponent<Bullet>().isHot = true;
	}

	public override void Die()
	{
		board.CreateFireLine(thePlantRow);
		base.Die();
	}

	private void ReadyToFire()
	{
		Collider2D[] array = Physics2D.OverlapBoxAll(shadow.transform.position, new Vector2(4f, 4f), 0f, zombieLayer);
		bool flag = false;
		int num = 0;
		List<Zombie> list = new List<Zombie>();
		Collider2D[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			if (array2[i].TryGetComponent<Zombie>(out var component) && component.theZombieRow == thePlantRow && !component.isMindControlled)
			{
				flag = true;
				list.Add(component);
			}
		}
		if (!flag)
		{
			return;
		}
		foreach (Zombie item in list)
		{
			if (num < 3)
			{
				Object.Instantiate(GameAPP.particlePrefab[35], item.shadow.transform.position, Quaternion.identity, board.transform).GetComponent<SpriteRenderer>().sortingLayerName = $"particle{thePlantRow}";
			}
			num++;
			item.TakeDamage(1, 40);
			item.Warm();
		}
		GameAPP.PlaySound(Random.Range(59, 61));
	}
}

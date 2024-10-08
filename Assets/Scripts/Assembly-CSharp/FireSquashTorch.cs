using UnityEngine;

public class FireSquashTorch : SquashTorch
{
	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.TryGetComponent<Bullet>(out var component) || component.torchWood == base.gameObject || component.isZombieBullet || (component.theMovingWay != 2 && component.theBulletRow != thePlantRow))
		{
			return;
		}
		switch (component.theBulletType)
		{
		case 0:
			if (Board.Instance.RedFirePea(component, this))
			{
				fireTimes++;
				if (fireTimes > 20)
				{
					SummonSquash();
				}
			}
			break;
		case 1:
			if (Board.Instance.FireCherry(component, this))
			{
				fireTimes++;
				if (fireTimes > 20)
				{
					SummonSquash();
				}
			}
			break;
		case 3:
			if (SuperFireCherry(component))
			{
				fireTimes++;
				if (fireTimes > 20)
				{
					SummonSquash();
				}
			}
			break;
		case 2:
			break;
		}
	}

	private bool SuperFireCherry(Bullet bullet)
	{
		if (bullet.torchWood != this)
		{
			Vector3 position = bullet.transform.position;
			int theRow = thePlantRow;
			CreateBullet.Instance.SetBullet(position.x, position.y, theRow, 36, 0);
			GameAPP.PlaySound(Random.Range(3, 5));
			bullet.Die();
			return true;
		}
		return false;
	}

	protected override void SummonSquash()
	{
		int num = 1;
		GameObject gameObject;
		do
		{
			if (board.boxType[thePlantColumn + num, thePlantRow] == 1)
			{
				CreatePlant.Instance.SetPlant(thePlantColumn + num, thePlantRow, 12);
			}
			gameObject = CreatePlant.Instance.SetPlant(thePlantColumn + num, thePlantRow, 1054);
			if (thePlantColumn + num > 9)
			{
				break;
			}
			num++;
		}
		while (gameObject == null);
		if (gameObject != null)
		{
			Vector2 vector = gameObject.GetComponent<Plant>().shadow.transform.position;
			Object.Instantiate(position: new Vector2(vector.x, vector.y + 0.5f), original: GameAPP.particlePrefab[11], rotation: Quaternion.identity, parent: board.transform);
			fireTimes = 0;
		}
	}

	public override void Die()
	{
		board.CreateFireLine(thePlantRow);
		base.Die();
	}
}

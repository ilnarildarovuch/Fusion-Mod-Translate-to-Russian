using UnityEngine;

public class SquashTorch : Plant
{
	protected int fireTimes;

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Bullet>(out var component) && !(component.torchWood == base.gameObject) && !component.isZombieBullet && (component.theMovingWay == 2 || component.theBulletRow == thePlantRow) && component.theBulletType == 0 && Board.Instance.YellowFirePea(component, this))
		{
			fireTimes++;
			if (fireTimes > 60)
			{
				SummonSquash();
			}
		}
	}

	protected virtual void SummonSquash()
	{
		int num = 1;
		GameObject gameObject;
		do
		{
			gameObject = CreatePlant.Instance.SetPlant(thePlantColumn + num, thePlantRow, 1057);
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
}

using UnityEngine;

public class AdvancedTorchWood : Plant
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Bullet>(out var component) && !(component.torchWood == base.gameObject) && !component.isZombieBullet && (component.theMovingWay == 2 || component.theBulletRow == thePlantRow))
		{
			switch (component.theBulletType)
			{
			case 0:
			case 7:
				Board.Instance.OrangeFirePea(component, this);
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
		bullet.isHot = true;
	}

	private void SunBullet(Bullet bullet)
	{
		Vector2 vector = bullet.transform.localScale;
		bullet.transform.localScale = new Vector3(2f * vector.x, 2f * vector.y);
		bullet.theBulletDamage = 400;
		bullet.GetComponent<Coin>().sunPrice = 20;
		bullet.isHot = true;
	}

	public override void Die()
	{
		board.CreateFireLine(thePlantRow);
		base.Die();
	}
}

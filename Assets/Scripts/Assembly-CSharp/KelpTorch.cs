using UnityEngine;

public class KelpTorch : Tanglekelp
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Bullet>(out var component) && !(component.torchWood == base.gameObject) && !component.isZombieBullet && (component.theMovingWay == 2 || component.theBulletRow == thePlantRow))
		{
			switch (component.theBulletType)
			{
			case 29:
				FireKelpBullet(component);
				break;
			case 0:
				board.YellowFirePea(component, this);
				break;
			}
		}
	}

	private void FireKelpBullet(Bullet bullet)
	{
		GameAPP.PlaySound(61);
		Vector2 vector = base.transform.GetChild(0).position;
		Bullet component = CreateBullet.Instance.SetBullet(vector.x, vector.y, thePlantRow, 30, (bullet.theMovingWay == 2) ? 2 : 0).GetComponent<Bullet>();
		component.theBulletDamage = 40;
		component.torchWood = base.gameObject;
		component.isHot = true;
		bullet.Die();
	}
}

using UnityEngine;

public class FireSquash : Squash
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Bullet>(out var component) && !(component.torchWood == base.gameObject) && !component.isZombieBullet && (component.theMovingWay == 2 || component.theBulletRow == thePlantRow))
		{
			int theBulletType = component.theBulletType;
			if (theBulletType == 0 || theBulletType == 7)
			{
				Board.Instance.YellowFirePea(component, this);
			}
		}
	}
}

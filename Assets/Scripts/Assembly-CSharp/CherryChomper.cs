using UnityEngine;

public class CherryChomper : Chomper
{
	public override void BiteEvent()
	{
		if (zombie != null)
		{
			Zombie component = zombie.GetComponent<Zombie>();
			Collider2D[] array = colliders;
			foreach (Collider2D collider2D in array)
			{
				if (component.theAttackTarget == base.gameObject)
				{
					Explode(zombie);
					return;
				}
				if (!(collider2D == null) && collider2D.gameObject == zombie)
				{
					if (zombie.TryGetComponent<PolevaulterZombie>(out var component2) && component2.polevaulterStatus != 2)
					{
						GameAPP.PlaySound(49);
						zombie = null;
						anim.SetTrigger("back");
					}
					else if (component.theStatus == 1 || component.isMindControlled)
					{
						GameAPP.PlaySound(49);
						zombie = null;
						anim.SetTrigger("back");
					}
					else
					{
						Explode(zombie);
					}
					return;
				}
			}
		}
		zombie = null;
		anim.SetTrigger("back");
		GameAPP.PlaySound(49);
	}

	private void Explode(GameObject _zombie)
	{
		_zombie.GetComponent<Zombie>().Die(2);
		attributeCountdown = swallowMaxCountDown;
		canToChew = true;
		zombie = null;
		GameObject gameObject = GameAPP.particlePrefab[2];
		Vector3 position = new Vector3(base.transform.position.x + 1.5f, base.transform.position.y + 0.5f, 0f);
		GameObject obj = Object.Instantiate(gameObject, position, Quaternion.identity);
		obj.transform.SetParent(GameAPP.board.transform);
		obj.name = gameObject.name;
		obj.GetComponent<BombCherry>().bombRow = thePlantRow;
		ScreenShake.TriggerShake(0.02f);
		GameAPP.PlaySound(40);
	}
}

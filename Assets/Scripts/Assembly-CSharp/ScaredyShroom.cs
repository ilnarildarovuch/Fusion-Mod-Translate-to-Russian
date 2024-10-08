using UnityEngine;

public class ScaredyShroom : Shooter
{
	private Vector2 pos = new Vector2(0f, 0f);

	private Vector2 range = new Vector2(2.5f, 2.5f);

	private Collider2D[] colliders;

	protected override void FixedUpdate()
	{
		pos = new Vector2(shadow.transform.position.x, shadow.transform.position.y + 0.5f);
		base.FixedUpdate();
		colliders = Physics2D.OverlapBoxAll(pos, range, 0f);
		bool flag = false;
		Collider2D[] array = colliders;
		foreach (Collider2D collider2D in array)
		{
			if (collider2D != null && collider2D.TryGetComponent<Zombie>(out var component) && !component.isMindControlled)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			anim.SetBool("NearZombie", value: true);
			ScaredEvent();
		}
		else
		{
			anim.SetBool("NearZombie", value: false);
		}
	}

	protected virtual void ScaredEvent()
	{
	}

	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 9, 0);
		obj.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(57);
		return obj;
	}
}

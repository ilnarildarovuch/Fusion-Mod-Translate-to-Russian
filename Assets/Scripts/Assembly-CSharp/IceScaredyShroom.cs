using UnityEngine;

public class IceScaredyShroom : ScaredyShroom
{
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 21, 0);
		obj.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(68);
		return obj;
	}

	private void AnimFreeze()
	{
		bool flag = false;
		Vector2 vector = shadow.transform.position;
		vector = new Vector2(vector.x, vector.y + 1f);
		Collider2D[] array = Physics2D.OverlapBoxAll(vector, new Vector2(3f, 3f), 0f);
		foreach (Collider2D collider2D in array)
		{
			if (collider2D != null && collider2D.TryGetComponent<Zombie>(out var component) && !component.isMindControlled && component.theStatus != 1 && Mathf.Abs(component.theZombieRow - thePlantRow) <= 1)
			{
				component.SetFreeze(4f);
				component.TakeDamage(1, 20);
				flag = true;
			}
		}
		if (flag)
		{
			GameAPP.PlaySound(67);
		}
		GameObject obj = Object.Instantiate(GameAPP.particlePrefab[24], shadow.transform.position, Quaternion.identity, board.transform);
		Vector2 vector2 = obj.transform.localScale;
		obj.transform.localScale = new Vector3(vector2.x * 1.5f, vector2.y * 1.5f);
	}
}

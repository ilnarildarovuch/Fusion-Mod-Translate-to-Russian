using UnityEngine;

public class PeaMine : PotatoMine
{
	protected override void Update()
	{
		if (attributeCountdown > 0f)
		{
			attributeCountdown -= Time.deltaTime;
		}
		if (attributeCountdown <= 0f)
		{
			attributeCountdown = 0f;
			anim.SetTrigger("rise");
		}
		base.Update();
		SetInterval();
		PlantShootUpdate();
	}

	public GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("PotatoMine_light1").GetChild(0).transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 7, 0);
		obj.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(Random.Range(3, 5));
		return obj;
	}

	private void SetInterval()
	{
		GameObject gameObject = GetNearestZombie();
		if (gameObject == null)
		{
			thePlantAttackInterval = 1.5f;
			return;
		}
		float num = gameObject.transform.position.x - base.transform.position.x;
		if (num > 6f)
		{
			thePlantAttackInterval = 1.5f;
		}
		else if (num < 1f)
		{
			thePlantAttackInterval = 0.5f;
		}
		else
		{
			thePlantAttackInterval = Mathf.Lerp(1.5f, 0.5f, (6f - num) / 5f);
		}
	}

	public override void Die()
	{
		for (int i = 0; i < 36; i++)
		{
			GameObject gameObject = board.GetComponent<CreateBullet>().SetBullet(base.transform.position.x, base.transform.position.y, thePlantRow, 7, 2);
			Vector3 position = gameObject.transform.GetChild(0).transform.position;
			gameObject.transform.rotation = Quaternion.AngleAxis((float)(10 * i) - 90f, Vector3.forward);
			gameObject.transform.GetChild(0).SetPositionAndRotation(position, Quaternion.Euler(new Vector3(0f, 0f, 0f - gameObject.transform.rotation.z)));
		}
		base.Die();
	}
}

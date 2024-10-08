using UnityEngine;

public class SuperChomper : Chomper
{
	private int shootType = 6;

	private GameObject face1;

	private GameObject face2;

	private GameObject face3;

	private GameObject body1;

	private GameObject body2;

	private GameObject body3;

	protected override void Start()
	{
		base.Start();
		face1 = base.transform.Find("Chomper_face_upper").gameObject;
		face2 = base.transform.Find("Chomper_face_upper1").gameObject;
		face3 = base.transform.Find("Chomper_face_upper2").gameObject;
		body1 = base.transform.Find("Chomper_body").gameObject;
		body2 = base.transform.Find("Chomper_body1").gameObject;
		body3 = base.transform.Find("Chomper_body2").gameObject;
	}

	protected override void Update()
	{
		base.Update();
		ReplaceSprite();
	}

	protected override void SetAttackRange()
	{
		pos = new Vector2(shadow.transform.position.x + 0.5f, shadow.transform.position.y + 0.5f);
		range = new Vector2(1f, 1.5f);
	}

	public override void BiteEvent()
	{
		if (zombie != null)
		{
			Zombie component = zombie.GetComponent<Zombie>();
			if (component.theAttackTarget == base.gameObject)
			{
				Bite(zombie);
				return;
			}
			Collider2D[] array = colliders;
			foreach (Collider2D collider2D in array)
			{
				if (!(collider2D == null) && collider2D.gameObject == zombie && !component.isMindControlled)
				{
					Bite(collider2D.gameObject);
					return;
				}
			}
			zombie = null;
		}
		GameAPP.PlaySound(49);
	}

	public virtual void AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float x = position.x;
		float y = position.y;
		int theRow = thePlantRow;
		board.GetComponent<CreateBullet>().SetBullet(x, y, theRow, shootType, 1).GetComponent<Bullet>()
			.theBulletDamage = 70;
		GameAPP.PlaySound(Random.Range(3, 5));
	}

	protected void ReplaceSprite()
	{
		if (thePlantHealth > thePlantMaxHealth * 2 / 3)
		{
			body1.SetActive(value: true);
			face1.SetActive(value: true);
			body2.SetActive(value: false);
			face2.SetActive(value: false);
			body3.SetActive(value: false);
			face3.SetActive(value: false);
		}
		if (thePlantHealth > thePlantMaxHealth / 3 && thePlantHealth < thePlantMaxHealth * 2 / 3)
		{
			body1.SetActive(value: false);
			face1.SetActive(value: false);
			body2.SetActive(value: true);
			face2.SetActive(value: true);
			body3.SetActive(value: false);
			face3.SetActive(value: false);
		}
		if (thePlantHealth < thePlantMaxHealth / 3)
		{
			body1.SetActive(value: false);
			face1.SetActive(value: false);
			body2.SetActive(value: false);
			face2.SetActive(value: false);
			body3.SetActive(value: true);
			face3.SetActive(value: true);
		}
	}

	protected virtual void Bite(GameObject _zombie)
	{
		Zombie component = _zombie.GetComponent<Zombie>();
		if (component.theZombieType == 3 && _zombie.GetComponent<PolevaulterZombie>().polevaulterStatus != 2)
		{
			GameAPP.PlaySound(49);
			zombie = null;
			return;
		}
		if (component.theFirstArmor == null && component.theHealth <= 500f)
		{
			component.TakeDamage(1, 500);
			Recover(200);
			shootType = 6;
		}
		else
		{
			component.TakeDamage(1, 200);
			Recover(100);
			shootType = Random.Range(4, 6);
		}
		if (thePlantHealth > thePlantMaxHealth)
		{
			thePlantHealth = thePlantMaxHealth;
		}
		GameAPP.PlaySound(49);
		zombie = null;
	}
}

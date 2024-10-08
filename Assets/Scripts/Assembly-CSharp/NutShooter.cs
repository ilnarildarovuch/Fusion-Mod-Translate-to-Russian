using UnityEngine;

public class NutShooter : Shooter
{
	protected override void Update()
	{
		base.Update();
		ReplaceSprite();
	}

	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.2f;
		float y = position.y;
		int theRow = thePlantRow;
		GameObject obj = board.GetComponent<CreateBullet>().SetBullet(theX, y, theRow, 2, 1);
		obj.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(Random.Range(3, 5));
		return obj;
	}

	private void ReplaceSprite()
	{
		if (thePlantHealth < thePlantMaxHealth * 2 / 3 && thePlantHealth > thePlantMaxHealth / 3)
		{
			base.transform.GetChild(1).gameObject.SetActive(value: false);
			base.transform.GetChild(2).gameObject.SetActive(value: true);
			base.transform.GetChild(3).gameObject.SetActive(value: false);
		}
		else if (thePlantHealth < thePlantMaxHealth / 3)
		{
			base.transform.GetChild(1).gameObject.SetActive(value: false);
			base.transform.GetChild(2).gameObject.SetActive(value: false);
			base.transform.GetChild(3).gameObject.SetActive(value: true);
		}
		else
		{
			base.transform.GetChild(1).gameObject.SetActive(value: true);
			base.transform.GetChild(2).gameObject.SetActive(value: false);
			base.transform.GetChild(3).gameObject.SetActive(value: false);
		}
	}
}

using UnityEngine;

public class WallNut : Plant
{
	protected override void Update()
	{
		base.Update();
		ReplaceSprite();
	}

	protected virtual void ReplaceSprite()
	{
		if (thePlantHealth > thePlantMaxHealth * 2 / 3)
		{
			base.transform.GetChild(0).gameObject.SetActive(value: true);
			base.transform.GetChild(1).gameObject.SetActive(value: false);
			base.transform.GetChild(2).gameObject.SetActive(value: false);
		}
		if (thePlantHealth > thePlantMaxHealth / 3 && thePlantHealth < thePlantMaxHealth * 2 / 3)
		{
			base.transform.GetChild(0).gameObject.SetActive(value: false);
			base.transform.GetChild(1).gameObject.SetActive(value: true);
			base.transform.GetChild(2).gameObject.SetActive(value: false);
		}
		if (thePlantHealth < thePlantMaxHealth / 3)
		{
			base.transform.GetChild(0).gameObject.SetActive(value: false);
			base.transform.GetChild(1).gameObject.SetActive(value: false);
			base.transform.GetChild(2).gameObject.SetActive(value: true);
		}
	}

	protected virtual void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Zombie"))
		{
			Zombie component = collision.GetComponent<Zombie>();
			if (component.theZombieRow == thePlantRow && component.theAttackTarget == base.gameObject && collision.gameObject.GetComponent<Zombie>().theStatus != 1)
			{
				thePlantSpeed = 0f;
			}
		}
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		thePlantSpeed = theConstSpeed;
	}
}

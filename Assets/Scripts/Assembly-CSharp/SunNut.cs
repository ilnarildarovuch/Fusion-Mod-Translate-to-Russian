using UnityEngine;

public class SunNut : Producer
{
	private bool produceSun1;

	private bool produceSun2;

	protected override void Update()
	{
		base.Update();
		ReplaceSprite();
	}

	public override void Die()
	{
		if (!board.isIZ)
		{
			ProduceSunWithNoSound();
			ProduceSunWithNoSound();
		}
		base.Die();
	}

	private void ReplaceSprite()
	{
		if (thePlantHealth < thePlantMaxHealth * 2 / 3 && thePlantHealth > thePlantMaxHealth / 3)
		{
			base.transform.GetChild(0).gameObject.SetActive(value: false);
			base.transform.GetChild(1).gameObject.SetActive(value: true);
			if (!produceSun1 && !board.isIZ)
			{
				ProduceSun();
				produceSun1 = true;
			}
		}
		else if (thePlantHealth < thePlantMaxHealth / 3)
		{
			base.transform.GetChild(1).gameObject.SetActive(value: false);
			base.transform.GetChild(2).gameObject.SetActive(value: true);
			if (!produceSun2 && !board.isIZ)
			{
				ProduceSun();
				produceSun2 = true;
			}
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
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

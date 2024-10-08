using UnityEngine;

public class SpikeRock : Caltrop
{
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		SpriteUpdate();
	}

	private void SpriteUpdate()
	{
		if (thePlantHealth > 150 && thePlantHealth <= 300)
		{
			base.transform.GetChild(0).GetChild(0).gameObject.SetActive(value: false);
			base.transform.GetChild(0).GetChild(1).gameObject.SetActive(value: true);
		}
		else if (thePlantHealth <= 150)
		{
			base.transform.GetChild(0).GetChild(0).gameObject.SetActive(value: false);
			base.transform.GetChild(0).GetChild(1).gameObject.SetActive(value: false);
		}
		else
		{
			base.transform.GetChild(0).GetChild(0).gameObject.SetActive(value: true);
			base.transform.GetChild(0).GetChild(1).gameObject.SetActive(value: true);
		}
	}

	protected override void KillCar()
	{
		Collider2D[] array = Physics2D.OverlapBoxAll(shadow.transform.position, new Vector2(1f, 1f), 0f);
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].TryGetComponent<DriverZombie>(out var component) && component.theZombieRow == thePlantRow && !component.isMindControlled && component.theStatus != 1)
			{
				component.KillByCaltrop();
				thePlantHealth -= 50;
				GameAPP.PlaySound(77);
			}
		}
		if (thePlantHealth == 0)
		{
			Die();
		}
	}
}

using UnityEngine;

public class PotatoNut : WallNut
{
	private bool isExplode1;

	private bool isExplode2;

	private float flashInterval = 3f;

	private float flashTime;

	protected override void Update()
	{
		base.Update();
		SetFlash();
		flashTime += Time.deltaTime;
		if (flashTime > flashInterval)
		{
			flashTime = 0f;
			anim.Play("flash");
		}
	}

	protected override void ReplaceSprite()
	{
		if (thePlantHealth < thePlantMaxHealth * 2 / 3 && thePlantHealth > thePlantMaxHealth / 3)
		{
			base.transform.GetChild(0).gameObject.SetActive(value: false);
			base.transform.GetChild(1).gameObject.SetActive(value: true);
			base.transform.GetChild(3).gameObject.SetActive(value: false);
			if (!isExplode1)
			{
				Explode(500, isShake: false);
				isExplode1 = true;
			}
		}
		else if (thePlantHealth < thePlantMaxHealth / 3)
		{
			base.transform.GetChild(1).gameObject.SetActive(value: false);
			base.transform.GetChild(2).gameObject.SetActive(value: true);
			base.transform.GetChild(3).gameObject.SetActive(value: false);
			if (!isExplode2)
			{
				Explode(500, isShake: false);
				isExplode2 = true;
			}
		}
		else
		{
			base.transform.GetChild(0).gameObject.SetActive(value: true);
			base.transform.GetChild(1).gameObject.SetActive(value: false);
			base.transform.GetChild(2).gameObject.SetActive(value: false);
		}
	}

	public override void Die()
	{
		Explode(1800, isShake: true);
		base.Die();
	}

	private void Explode(int dmg, bool isShake)
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 1f);
		foreach (Collider2D collider2D in array)
		{
			if (collider2D.CompareTag("Zombie"))
			{
				Zombie component = collider2D.GetComponent<Zombie>();
				if (component.theZombieRow == thePlantRow)
				{
					component.TakeDamage(10, dmg);
				}
			}
		}
		Object.Instantiate(position: new Vector3(base.transform.position.x, base.transform.position.y + 1f, base.transform.position.z), original: GameAPP.particlePrefab[8], rotation: Quaternion.LookRotation(new Vector3(0f, 90f, 0f))).transform.SetParent(GameAPP.board.transform);
		GameAPP.PlaySound(47);
		if (isShake)
		{
			ScreenShake.TriggerShake();
		}
	}

	private void SetFlash()
	{
		if (thePlantHealth > 3000)
		{
			flashInterval = 5f;
		}
		if (thePlantHealth > 2667 && thePlantHealth < 3000)
		{
			flashInterval = 1f;
		}
		if (thePlantHealth > 2000 && thePlantHealth > 2667)
		{
			flashInterval = 5f;
		}
		if (thePlantHealth > 1333 && thePlantHealth < 2000)
		{
			flashInterval = 1f;
		}
		if (thePlantHealth > 500 && thePlantHealth < 1333)
		{
			flashInterval = 5f;
		}
		if (thePlantHealth < 500)
		{
			flashInterval = 1f;
		}
	}
}

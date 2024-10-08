using UnityEngine;

public class PuffRandomColor : Bullet
{
	protected override void Start()
	{
		puffColor = Random.Range(0, 7);
		sprite = Resources.Load<Sprite>($"Bullet/Colorfulpuffs/ColorfulPuff{puffColor + 1}");
		GetComponent<SpriteRenderer>().sprite = sprite;
		base.transform.GetChild(0).GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, sprite);
	}

	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.controlledLevel[puffColor] = true;
		int num = 0;
		bool[] controlledLevel = component.controlledLevel;
		for (int i = 0; i < controlledLevel.Length; i++)
		{
			if (controlledLevel[i])
			{
				num++;
			}
		}
		if (num > 6)
		{
			component.SetMindControl(mustControl: true);
		}
		else
		{
			component.TakeDamage(0, theBulletDamage);
			PlaySound(component);
		}
		GameObject gameObject = GameAPP.particlePrefab[17];
		GameObject obj = Object.Instantiate(gameObject, base.transform.position, Quaternion.identity);
		obj.GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, sprite);
		obj.transform.SetParent(GameAPP.board.transform);
		obj.name = gameObject.name;
		Die();
	}

	private void AttackZombie()
	{
		Collider2D[] array = Physics2D.OverlapBoxAll(base.transform.position, new Vector2(3f, 3f), 0f);
		foreach (Collider2D collider2D in array)
		{
			if (collider2D != null && collider2D.TryGetComponent<Zombie>(out var component) && Mathf.Abs(component.theZombieRow - theBulletRow) <= 1 && (!component.gameObject.TryGetComponent<PolevaulterZombie>(out var component2) || component2.polevaulterStatus != 1) && !component.isMindControlled)
			{
				TrySetMindControl(component);
			}
		}
	}

	private void TrySetMindControl(Zombie zombie)
	{
		float num = zombie.theFirstArmorMaxHealth + zombie.theMaxHealth;
		float num2 = ((float)zombie.theFirstArmorHealth + zombie.theHealth) / num;
		num2 = ((!((double)num2 > 0.5)) ? (num2 / 0.5f) : 1f);
		int num3 = 0;
		bool[] controlledLevel = zombie.controlledLevel;
		for (int i = 0; i < controlledLevel.Length; i++)
		{
			if (controlledLevel[i])
			{
				num3++;
			}
		}
		num2 -= (float)num3 * 0.05f;
		num2 = Mathf.Sqrt(num2);
		float num4 = 0.75f;
		num4 -= (float)num3 * 0.05f;
		if (num2 < num4)
		{
			num2 = num4;
		}
		if (Random.value >= num2)
		{
			zombie.SetMindControl();
		}
		else
		{
			zombie.TakeDamage(1, 20);
		}
	}
}

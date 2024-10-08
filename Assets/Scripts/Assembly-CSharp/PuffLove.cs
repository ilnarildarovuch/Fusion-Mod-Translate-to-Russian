using UnityEngine;

public class PuffLove : Bullet
{
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, theBulletDamage);
		PlaySound(component);
		SetFume();
		Die();
	}

	private void SetFume()
	{
		Object.Instantiate(GameAPP.particlePrefab[22], base.transform.position, Quaternion.identity, GameAPP.board.transform).GetComponent<ParticleSystem>().GetComponent<Renderer>()
			.sortingLayerName = $"particle{theBulletRow}";
		AttackZombie();
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
		num2 = Mathf.Sqrt(num2);
		float num3 = 0.75f;
		if (num2 < num3)
		{
			num2 = num3;
		}
		if (Random.value >= num2)
		{
			zombie.SetMindControl();
		}
		else
		{
			zombie.TakeDamage(1, 40);
		}
	}
}

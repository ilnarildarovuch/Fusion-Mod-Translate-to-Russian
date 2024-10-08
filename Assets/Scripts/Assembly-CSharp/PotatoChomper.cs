using UnityEngine;

public class PotatoChomper : Chomper
{
	public override void Die()
	{
		Explode();
		Object.Instantiate(position: new Vector3(base.transform.position.x, base.transform.position.y + 1.5f, base.transform.position.z), original: GameAPP.particlePrefab[8], rotation: Quaternion.LookRotation(new Vector3(0f, 90f, 0f))).transform.SetParent(GameAPP.board.transform);
		GameAPP.PlaySound(47);
		ScreenShake.TriggerShake();
		base.Die();
	}

	private void Explode()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 1f);
		foreach (Collider2D collider2D in array)
		{
			if (collider2D.CompareTag("Zombie"))
			{
				Zombie component = collider2D.GetComponent<Zombie>();
				if (component.theZombieRow == thePlantRow)
				{
					component.TakeDamage(10, 1800);
				}
			}
		}
	}

	protected override void Swallow()
	{
		anim.SetTrigger("swallow");
		anim.SetBool("chew", value: false);
		Invoke("ResetCount", 1.5f);
		Invoke("CreateMine", 0.5f);
	}

	private void CreateMine()
	{
		GameObject gameObject = board.GetComponent<CreatePlant>().SetPlant(thePlantColumn + 1, thePlantRow, 4);
		if (gameObject != null && gameObject.TryGetComponent<PotatoMine>(out var component))
		{
			component.attributeCountdown = 0f;
		}
	}

	protected override void SetAttackRange()
	{
		pos = new Vector2(shadow.transform.position.x + 0.5f, shadow.transform.position.y + 0.5f);
		range = new Vector2(0.5f, 1f);
	}
}

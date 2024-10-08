using UnityEngine;

public class Chomper : Plant
{
	protected GameObject zombie;

	protected Vector2 pos = new Vector2(-20f, 0f);

	protected float minX = float.PositiveInfinity;

	[SerializeField]
	protected float swallowMaxCountDown = 40f;

	protected bool canToChew;

	protected Vector2 range = new Vector2(1.5f, 1.5f);

	protected Collider2D[] colliders;

	protected virtual void SetAttackRange()
	{
		pos = new Vector2(shadow.transform.position.x + 1.5f, shadow.transform.position.y + 0.5f);
		range = new Vector2(1.5f, 1.5f);
	}

	protected override void Update()
	{
		base.Update();
		SetAttackRange();
		if (attributeCountdown > 0f)
		{
			attributeCountdown -= Time.deltaTime;
			if (attributeCountdown < 0f)
			{
				Swallow();
			}
		}
	}

	protected virtual void Swallow()
	{
		anim.SetTrigger("swallow");
		anim.SetBool("chew", value: false);
		Invoke("ResetCount", 1.5f);
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (!(zombie == null) || !anim.GetCurrentAnimatorStateInfo(0).IsName("idle") || attributeCountdown != 0f)
		{
			return;
		}
		colliders = Physics2D.OverlapBoxAll(pos, range, 0f);
		minX = float.PositiveInfinity;
		foreach (GameObject item in board.GetComponent<Board>().zombieArray)
		{
			if (item != null && item.GetComponent<Zombie>().theAttackTarget == base.gameObject)
			{
				zombie = item;
				anim.SetTrigger("bite");
				return;
			}
		}
		Collider2D[] array = colliders;
		foreach (Collider2D collider2D in array)
		{
			if (collider2D == null || !collider2D.CompareTag("Zombie"))
			{
				continue;
			}
			Zombie component = collider2D.GetComponent<Zombie>();
			if (component.theStatus == 1 || component.theStatus == 7 || component.theZombieRow != thePlantRow || component.isMindControlled)
			{
				continue;
			}
			Transform transform = collider2D.transform;
			if (transform != null)
			{
				float x = transform.position.x;
				if (x < minX)
				{
					minX = x;
					zombie = collider2D.gameObject;
				}
			}
		}
		if (zombie != null)
		{
			anim.SetTrigger("bite");
		}
	}

	public virtual void BiteEvent()
	{
		if (zombie != null)
		{
			Zombie component = zombie.GetComponent<Zombie>();
			if (component.theAttackTarget == base.gameObject)
			{
				component.Die(2);
				attributeCountdown = swallowMaxCountDown;
				canToChew = true;
				zombie = null;
				GameAPP.PlaySound(49);
				return;
			}
			Collider2D[] array = colliders;
			foreach (Collider2D collider2D in array)
			{
				if (!(collider2D == null) && collider2D.gameObject == zombie)
				{
					if (zombie.TryGetComponent<PolevaulterZombie>(out var component2) && component2.polevaulterStatus != 2)
					{
						GameAPP.PlaySound(49);
						zombie = null;
						anim.SetTrigger("back");
					}
					else if (component.theStatus == 1 || component.isMindControlled)
					{
						GameAPP.PlaySound(49);
						zombie = null;
						anim.SetTrigger("back");
					}
					else
					{
						component.Die(2);
						attributeCountdown = swallowMaxCountDown;
						canToChew = true;
						zombie = null;
						GameAPP.PlaySound(49);
					}
					return;
				}
			}
		}
		zombie = null;
		anim.SetTrigger("back");
		GameAPP.PlaySound(49);
	}

	protected void ResetCount()
	{
		attributeCountdown = 0f;
	}

	public void ToChew()
	{
		if (canToChew)
		{
			anim.Play("chew");
		}
		canToChew = false;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(pos, range);
		Collider2D[] array = Physics2D.OverlapBoxAll(pos, range, 0f);
		foreach (Collider2D obj in array)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(obj.bounds.center, 0.1f);
		}
	}
}

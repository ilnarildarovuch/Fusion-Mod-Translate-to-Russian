using UnityEngine;

public class PotatoMine : Plant
{
	private bool isAready;

	private GameObject nearestZombie;

	private float flashInterval = 3f;

	private float flashTime;

	private bool isActive;

	protected override void Start()
	{
		base.Start();
		attributeCountdown = Random.Range(14f, 16f);
	}

	protected override void Update()
	{
		if (attributeCountdown > 0f)
		{
			attributeCountdown -= Time.deltaTime;
		}
		if (attributeCountdown <= 0f)
		{
			attributeCountdown = 0f;
			anim.SetTrigger("rise");
		}
		base.Update();
		SetFlash();
		if (isAready)
		{
			flashTime += Time.deltaTime;
			if (flashTime > flashInterval)
			{
				flashTime = 0f;
				anim.Play("flash");
			}
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (!collision.CompareTag("Zombie") || (collision.TryGetComponent<PolevaulterZombie>(out var component) && component.polevaulterStatus != 2))
		{
			return;
		}
		Zombie component2 = collision.GetComponent<Zombie>();
		if (component2.theZombieRow == thePlantRow && isAready && component2.theStatus != 1 && !component2.isMindControlled)
		{
			isAready = false;
			Explode();
			Object.Instantiate(position: new Vector3(base.transform.position.x, base.transform.position.y + 1f, base.transform.position.z), original: GameAPP.particlePrefab[8], rotation: Quaternion.LookRotation(new Vector3(0f, 90f, 0f))).transform.SetParent(GameAPP.board.transform);
			GameAPP.PlaySound(47);
			ScreenShake.TriggerShake();
			if (!isActive)
			{
				Invoke("DelayDie", 0.2f);
				isActive = true;
			}
		}
	}

	private void DelayDie()
	{
		Die();
	}

	public void AnimStartRise()
	{
		GameAPP.PlaySound(48);
		isAshy = true;
		Object.Instantiate(position: new Vector3(base.transform.position.x, base.transform.position.y + 0.5f, base.transform.position.z), original: GameAPP.particlePrefab[9], rotation: Quaternion.identity).transform.SetParent(GameAPP.board.transform);
	}

	public void AnimRiseOver()
	{
		isAready = true;
	}

	public virtual void AnimMeshed()
	{
		Invoke("Die", 2f);
	}

	private void Explode()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 1f);
		foreach (Collider2D collider2D in array)
		{
			if (collider2D.CompareTag("Zombie"))
			{
				Zombie component = collider2D.GetComponent<Zombie>();
				if (component.theZombieRow == thePlantRow && !component.isMindControlled)
				{
					component.TakeDamage(10, 1800);
				}
			}
		}
	}

	private void SetFlash()
	{
		GameObject gameObject = GetNearestZombie();
		if (gameObject == null)
		{
			flashInterval = 6f;
			return;
		}
		float num = gameObject.transform.position.x - base.transform.position.x;
		if (num > 6f)
		{
			flashInterval = 6f;
		}
		else if (num < 1f)
		{
			flashInterval = 0.2f;
		}
		else
		{
			flashInterval = num / 2f;
		}
	}

	protected virtual GameObject GetNearestZombie()
	{
		nearestZombie = null;
		float num = float.MaxValue;
		foreach (GameObject item in GameAPP.board.GetComponent<Board>().zombieArray)
		{
			if (!(item != null) || item.transform.position.x < base.transform.position.x)
			{
				continue;
			}
			Zombie component = item.GetComponent<Zombie>();
			if (!component.isMindControlled && component.theZombieRow == thePlantRow && component.theStatus != 1)
			{
				float num2 = Vector3.Distance(item.transform.position, base.transform.position);
				if (num2 < num)
				{
					num = num2;
					nearestZombie = item;
				}
			}
		}
		return nearestZombie;
	}
}

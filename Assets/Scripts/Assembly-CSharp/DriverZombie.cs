using UnityEngine;

public class DriverZombie : Zombie
{
	protected float startSpeed = 0.8f;

	protected float currentSpeed = 0.8f;

	protected override void Start()
	{
		base.Start();
		if (GameAPP.theGameStatus == 0)
		{
			GameAPP.PlaySound(76, 1f);
			currentSpeed = startSpeed;
		}
	}

	protected override void Update()
	{
		MoveUpdate();
		if (GameAPP.theGameStatus == 0 && theStatus != 1)
		{
			DriverPositionUpdate();
		}
		if (GameAPP.theGameStatus == 0 && ((isMindControlled && base.transform.position.x > 10f) || base.transform.position.x > 12f || base.transform.position.x < -10f))
		{
			Die(2);
		}
	}

	public override void SetFreeze(float time)
	{
	}

	public override void SetCold(float time)
	{
	}

	protected virtual void DriverPositionUpdate()
	{
		if (!isMindControlled)
		{
			CreateIceRoad();
		}
		base.transform.Translate((0f - currentSpeed) * Time.deltaTime, 0f, 0f);
		if (currentSpeed > 0.2f)
		{
			currentSpeed -= 0.05f * Time.deltaTime;
		}
	}

	private void CreateIceRoad()
	{
		float num = base.transform.GetChild(2).position.x;
		if (board.isTowerDefense)
		{
			num = 11f;
		}
		if (Board.Instance.iceRoadX[theZombieRow] > num)
		{
			Board.Instance.iceRoadX[theZombieRow] = num;
		}
		Board.Instance.iceRoadFadeTime[theZombieRow] = 30f;
	}

	protected override void BodyTakeDamage(int theDamage)
	{
		theHealth -= theDamage;
		if (theHealth >= (float)theMaxHealth / 3f && theHealth < (float)theMaxHealth * 2f / 3f)
		{
			base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[29];
			base.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[31];
		}
		if (theHealth < (float)theMaxHealth / 3f)
		{
			anim.SetTrigger("shake");
			base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[30];
			base.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[32];
			base.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
			GameObject obj = base.transform.GetChild(1).GetChild(0).gameObject;
			obj.SetActive(value: true);
			foreach (Transform item in obj.transform)
			{
				item.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = $"zombie{theZombieRow}";
				item.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = baseLayer + 29;
			}
		}
		if (theHealth <= 0f)
		{
			DieAndExplode();
		}
	}

	public virtual void KillByCaltrop()
	{
		anim.SetTrigger("shake");
		anim.SetTrigger("GoDie");
		base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[30];
		base.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[32];
		base.transform.GetChild(1).GetChild(0).gameObject.SetActive(value: true);
		GameObject obj = base.transform.GetChild(1).GetChild(0).gameObject;
		obj.SetActive(value: true);
		foreach (Transform item in obj.transform)
		{
			item.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = $"zombie{theZombieRow}";
			item.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = baseLayer + 29;
		}
		GetComponent<BoxCollider2D>().enabled = false;
		theStatus = 1;
		Invoke("DieAndExplode", 2f);
	}

	protected void DieAndExplode()
	{
		Die(2);
	}

	protected override void DieEvent()
	{
		GameAPP.PlaySound(43);
		Vector2 vector = shadow.transform.position;
		Object.Instantiate(position: new Vector2(vector.x, vector.y + 0.6f), original: GameAPP.particlePrefab[34], rotation: Quaternion.identity, parent: board.transform);
	}

	protected override void OnTriggerStay2D(Collider2D collision)
	{
		if (!isMindControlled && collision.TryGetComponent<Plant>(out var component))
		{
			if (TypeMgr.IsCaltrop(component.thePlantType) || (board.isTowerDefense && board.boxType[component.thePlantColumn, component.thePlantRow] != 2))
			{
				return;
			}
			if (component.thePlantRow == theZombieRow)
			{
				if (component.thePlantType == 903)
				{
					component.thePlantHealth -= 500;
					base.transform.Translate(1f, 0f, 0f);
					GameAPP.PlaySound(Random.Range(72, 75));
					return;
				}
				GameAPP.PlaySound(Random.Range(8, 10), 0.3f);
				component.Crashed();
			}
		}
		if (collision.TryGetComponent<Zombie>(out var component2) && component2.isMindControlled != isMindControlled && component2.theZombieRow == theZombieRow)
		{
			component2.TakeDamage(4, 20);
		}
	}
}

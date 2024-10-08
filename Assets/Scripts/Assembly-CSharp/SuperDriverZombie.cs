using UnityEngine;

public class SuperDriverZombie : DriverZombie
{
	protected override void BodyTakeDamage(int theDamage)
	{
		theHealth -= theDamage;
		if (theHealth >= (float)theMaxHealth / 3f && theHealth < (float)theMaxHealth * 2f / 3f)
		{
			base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[33];
			base.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[34];
		}
		if (theHealth < (float)theMaxHealth / 3f)
		{
			anim.SetTrigger("shake");
			base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[35];
			base.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[36];
			base.transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>()
				.sprite = GameAPP.spritePrefab[37];
			base.transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>()
				.sprite = GameAPP.spritePrefab[37];
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

	protected override void DriverPositionUpdate()
	{
		float x = base.transform.GetChild(4).position.x;
		if (Board.Instance.iceRoadX[theZombieRow] > x)
		{
			base.transform.Translate(-0.2f * Time.deltaTime, 0f, 0f);
		}
		else
		{
			base.transform.Translate((0f - currentSpeed) * Time.deltaTime, 0f, 0f);
		}
	}

	public override void KillByCaltrop()
	{
		if (theHealth > 0.5f * (float)theMaxHealth)
		{
			TakeDamage(0, (int)((float)theMaxHealth * 0.5f));
			return;
		}
		anim.SetTrigger("shake");
		anim.SetTrigger("GoDie");
		base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[35];
		base.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[36];
		base.transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>()
			.sprite = GameAPP.spritePrefab[37];
		base.transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>()
			.sprite = GameAPP.spritePrefab[37];
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

	protected override void DieEvent()
	{
		GameAPP.PlaySound(43);
		Vector2 vector = shadow.transform.position;
		Object.Instantiate(position: new Vector2(vector.x, vector.y + 0.6f), original: GameAPP.particlePrefab[36], rotation: Quaternion.identity, parent: board.transform);
	}
}

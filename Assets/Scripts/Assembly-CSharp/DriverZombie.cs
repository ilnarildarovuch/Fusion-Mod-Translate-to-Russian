using System;
using UnityEngine;

// Token: 0x020000D5 RID: 213
public class DriverZombie : Zombie
{
	// Token: 0x060003DD RID: 989 RVA: 0x0001DA04 File Offset: 0x0001BC04
	protected override void Start()
	{
		base.Start();
		if (GameAPP.theGameStatus == 0)
		{
			GameAPP.PlaySound(76, 1f);
			this.currentSpeed = this.startSpeed;
		}
	}

	// Token: 0x060003DE RID: 990 RVA: 0x0001DA2C File Offset: 0x0001BC2C
	protected override void Update()
	{
		base.MoveUpdate();
		if (GameAPP.theGameStatus == 0 && this.theStatus != 1)
		{
			this.DriverPositionUpdate();
		}
		if (GameAPP.theGameStatus == 0 && ((this.isMindControlled && base.transform.position.x > 10f) || base.transform.position.x > 12f || base.transform.position.x < -10f))
		{
			this.Die(2);
		}
	}

	// Token: 0x060003DF RID: 991 RVA: 0x0001DAB0 File Offset: 0x0001BCB0
	public override void SetFreeze(float time)
	{
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x0001DAB2 File Offset: 0x0001BCB2
	public override void SetCold(float time)
	{
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0001DAB4 File Offset: 0x0001BCB4
	protected virtual void DriverPositionUpdate()
	{
		if (!this.isMindControlled)
		{
			this.CreateIceRoad();
		}
		base.transform.Translate(-this.currentSpeed * Time.deltaTime, 0f, 0f);
		if (this.currentSpeed > 0.2f)
		{
			this.currentSpeed -= 0.05f * Time.deltaTime;
		}
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x0001DB18 File Offset: 0x0001BD18
	private void CreateIceRoad()
	{
		float num = base.transform.GetChild(2).position.x;
		if (this.board.isTowerDefense)
		{
			num = 11f;
		}
		if (Board.Instance.iceRoadX[this.theZombieRow] > num)
		{
			Board.Instance.iceRoadX[this.theZombieRow] = num;
		}
		Board.Instance.iceRoadFadeTime[this.theZombieRow] = 30f;
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x0001DB8C File Offset: 0x0001BD8C
	protected override void BodyTakeDamage(int theDamage)
	{
		this.theHealth -= (float)theDamage;
		if (this.theHealth >= (float)this.theMaxHealth / 3f && this.theHealth < (float)this.theMaxHealth * 2f / 3f)
		{
			base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[29];
			base.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[31];
		}
		if (this.theHealth < (float)this.theMaxHealth / 3f)
		{
			this.anim.SetTrigger("shake");
			base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[30];
			base.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[32];
			base.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
			GameObject gameObject = base.transform.GetChild(1).GetChild(0).gameObject;
			gameObject.SetActive(true);
			foreach (object obj in gameObject.transform)
			{
				Transform transform = (Transform)obj;
				transform.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
				transform.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = this.baseLayer + 29;
			}
		}
		if (this.theHealth <= 0f)
		{
			this.DieAndExplode();
		}
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x0001DD48 File Offset: 0x0001BF48
	public virtual void KillByCaltrop()
	{
		this.anim.SetTrigger("shake");
		this.anim.SetTrigger("GoDie");
		base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[30];
		base.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[32];
		base.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
		GameObject gameObject = base.transform.GetChild(1).GetChild(0).gameObject;
		gameObject.SetActive(true);
		foreach (object obj in gameObject.transform)
		{
			Transform transform = (Transform)obj;
			transform.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
			transform.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = this.baseLayer + 29;
		}
		base.GetComponent<BoxCollider2D>().enabled = false;
		this.theStatus = 1;
		base.Invoke("DieAndExplode", 2f);
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x0001DE90 File Offset: 0x0001C090
	protected void DieAndExplode()
	{
		this.Die(2);
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x0001DE9C File Offset: 0x0001C09C
	protected override void DieEvent()
	{
		GameAPP.PlaySound(43, 0.5f);
		Vector2 vector = this.shadow.transform.position;
		vector = new Vector2(vector.x, vector.y + 0.6f);
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[34], vector, Quaternion.identity, this.board.transform);
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x0001DF08 File Offset: 0x0001C108
	protected override void OnTriggerStay2D(Collider2D collision)
	{
		Plant plant;
		if (!this.isMindControlled && collision.TryGetComponent<Plant>(out plant))
		{
			if (TypeMgr.IsCaltrop(plant.thePlantType))
			{
				return;
			}
			if (this.board.isTowerDefense && this.board.boxType[plant.thePlantColumn, plant.thePlantRow] != 2)
			{
				return;
			}
			if (plant.thePlantRow == this.theZombieRow)
			{
				if (plant.thePlantType == 903)
				{
					plant.thePlantHealth -= 500;
					base.transform.Translate(1f, 0f, 0f);
					GameAPP.PlaySound(Random.Range(72, 75), 0.5f);
					return;
				}
				GameAPP.PlaySound(Random.Range(8, 10), 0.3f);
				plant.Crashed();
			}
		}
		Zombie zombie;
		if (collision.TryGetComponent<Zombie>(out zombie) && zombie.isMindControlled != this.isMindControlled && zombie.theZombieRow == this.theZombieRow)
		{
			zombie.TakeDamage(4, 20);
		}
	}

	// Token: 0x040001E1 RID: 481
	protected float startSpeed = 0.8f;

	// Token: 0x040001E2 RID: 482
	protected float currentSpeed = 0.8f;
}

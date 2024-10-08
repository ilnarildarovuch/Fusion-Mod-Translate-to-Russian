using System;
using UnityEngine;

// Token: 0x0200007D RID: 125
public class SuperChomper : Chomper
{
	// Token: 0x06000292 RID: 658 RVA: 0x000152F4 File Offset: 0x000134F4
	protected override void Start()
	{
		base.Start();
		this.face1 = base.transform.Find("Chomper_face_upper").gameObject;
		this.face2 = base.transform.Find("Chomper_face_upper1").gameObject;
		this.face3 = base.transform.Find("Chomper_face_upper2").gameObject;
		this.body1 = base.transform.Find("Chomper_body").gameObject;
		this.body2 = base.transform.Find("Chomper_body1").gameObject;
		this.body3 = base.transform.Find("Chomper_body2").gameObject;
	}

	// Token: 0x06000293 RID: 659 RVA: 0x000153A9 File Offset: 0x000135A9
	protected override void Update()
	{
		base.Update();
		this.ReplaceSprite();
	}

	// Token: 0x06000294 RID: 660 RVA: 0x000153B8 File Offset: 0x000135B8
	protected override void SetAttackRange()
	{
		this.pos = new Vector2(this.shadow.transform.position.x + 0.5f, this.shadow.transform.position.y + 0.5f);
		this.range = new Vector2(1f, 1.5f);
	}

	// Token: 0x06000295 RID: 661 RVA: 0x0001541C File Offset: 0x0001361C
	public override void BiteEvent()
	{
		if (this.zombie != null)
		{
			Zombie component = this.zombie.GetComponent<Zombie>();
			if (component.theAttackTarget == base.gameObject)
			{
				this.Bite(this.zombie);
				return;
			}
			foreach (Collider2D collider2D in this.colliders)
			{
				if (!(collider2D == null) && collider2D.gameObject == this.zombie && !component.isMindControlled)
				{
					this.Bite(collider2D.gameObject);
					return;
				}
			}
			this.zombie = null;
		}
		GameAPP.PlaySound(49, 0.5f);
	}

	// Token: 0x06000296 RID: 662 RVA: 0x000154C0 File Offset: 0x000136C0
	public virtual void AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float x = position.x;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		this.board.GetComponent<CreateBullet>().SetBullet(x, y, thePlantRow, this.shootType, 1).GetComponent<Bullet>().theBulletDamage = 70;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
	}

	// Token: 0x06000297 RID: 663 RVA: 0x00015534 File Offset: 0x00013734
	protected void ReplaceSprite()
	{
		if (this.thePlantHealth > this.thePlantMaxHealth * 2 / 3)
		{
			this.body1.SetActive(true);
			this.face1.SetActive(true);
			this.body2.SetActive(false);
			this.face2.SetActive(false);
			this.body3.SetActive(false);
			this.face3.SetActive(false);
		}
		if (this.thePlantHealth > this.thePlantMaxHealth / 3 && this.thePlantHealth < this.thePlantMaxHealth * 2 / 3)
		{
			this.body1.SetActive(false);
			this.face1.SetActive(false);
			this.body2.SetActive(true);
			this.face2.SetActive(true);
			this.body3.SetActive(false);
			this.face3.SetActive(false);
		}
		if (this.thePlantHealth < this.thePlantMaxHealth / 3)
		{
			this.body1.SetActive(false);
			this.face1.SetActive(false);
			this.body2.SetActive(false);
			this.face2.SetActive(false);
			this.body3.SetActive(true);
			this.face3.SetActive(true);
		}
	}

	// Token: 0x06000298 RID: 664 RVA: 0x00015660 File Offset: 0x00013860
	protected virtual void Bite(GameObject _zombie)
	{
		Zombie component = _zombie.GetComponent<Zombie>();
		if (component.theZombieType == 3 && _zombie.GetComponent<PolevaulterZombie>().polevaulterStatus != 2)
		{
			GameAPP.PlaySound(49, 0.5f);
			this.zombie = null;
			return;
		}
		if (component.theFirstArmor == null && component.theHealth <= 500f)
		{
			component.TakeDamage(1, 500);
			base.Recover(200);
			this.shootType = 6;
		}
		else
		{
			component.TakeDamage(1, 200);
			base.Recover(100);
			this.shootType = Random.Range(4, 6);
		}
		if (this.thePlantHealth > this.thePlantMaxHealth)
		{
			this.thePlantHealth = this.thePlantMaxHealth;
		}
		GameAPP.PlaySound(49, 0.5f);
		this.zombie = null;
	}

	// Token: 0x04000191 RID: 401
	private int shootType = 6;

	// Token: 0x04000192 RID: 402
	private GameObject face1;

	// Token: 0x04000193 RID: 403
	private GameObject face2;

	// Token: 0x04000194 RID: 404
	private GameObject face3;

	// Token: 0x04000195 RID: 405
	private GameObject body1;

	// Token: 0x04000196 RID: 406
	private GameObject body2;

	// Token: 0x04000197 RID: 407
	private GameObject body3;
}

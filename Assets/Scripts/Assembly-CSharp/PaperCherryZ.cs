using System;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x020000EA RID: 234
public class PaperCherryZ : ArmorZombie
{
	// Token: 0x06000445 RID: 1093 RVA: 0x00021DAC File Offset: 0x0001FFAC
	protected override void SecondArmorBroken()
	{
		if (this.theSecondArmorHealth < this.theSecondArmorMaxHealth * 2 / 3 && this.theSecondArmorBroken < 1)
		{
			this.theSecondArmorBroken = 1;
			this.theSecondArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[10];
		}
		if (this.theSecondArmorHealth < this.theSecondArmorMaxHealth / 3 && this.theSecondArmorBroken < 2)
		{
			this.theSecondArmorBroken = 2;
			this.theSecondArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[11];
		}
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x00021E2C File Offset: 0x0002002C
	protected override void SecondArmorFall()
	{
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.name == "LosePaper")
			{
				transform.gameObject.SetActive(true);
				transform.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
				transform.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += this.baseLayer + 29;
			}
		}
		GameAPP.PlaySound(44, 0.5f);
		this.anim.SetTrigger("losePaper");
		this.losePaper = true;
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x00021F04 File Offset: 0x00020104
	protected override void BodyTakeDamage(int theDamage)
	{
		this.theHealth -= (float)theDamage;
		if (!this.isLoseHand && this.theHealth < (float)(this.theMaxHealth * 2 / 3) && this.losePaper)
		{
			this.isLoseHand = true;
			GameAPP.PlaySound(7, 0.5f);
			for (int i = 0; i < base.transform.childCount; i++)
			{
				Transform child = base.transform.GetChild(i);
				if (child.CompareTag("ZombieHand"))
				{
					Object.Destroy(child.gameObject);
				}
				if (child.CompareTag("ZombieArmUpper"))
				{
					child.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[5];
				}
				if (child.name == "LoseArm")
				{
					child.gameObject.SetActive(true);
					child.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
					child.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += this.baseLayer + 29;
					child.gameObject.GetComponent<ParticleSystem>().collision.AddPlane(this.board.transform.GetChild(2 + this.theZombieRow));
					child.AddComponent<ZombieHead>();
				}
			}
		}
		if (this.theHealth < (float)(this.theMaxHealth / 3) && this.theStatus != 1)
		{
			this.theStatus = 1;
			GameAPP.PlaySound(7, 0.5f);
			for (int j = 0; j < base.transform.childCount; j++)
			{
				Transform child2 = base.transform.GetChild(j);
				if (child2.CompareTag("ZombieHead"))
				{
					Object.Destroy(child2.gameObject);
				}
			}
			if (!this.losePaper)
			{
				this.SecondArmorFall();
			}
		}
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x000220D3 File Offset: 0x000202D3
	public void AngrySound()
	{
		GameAPP.PlaySound(Random.Range(45, 47), 0.5f);
		this.isAngry = true;
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x000220EF File Offset: 0x000202EF
	private void Angry()
	{
		this.isAngry = true;
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x000220F8 File Offset: 0x000202F8
	protected override void Update()
	{
		base.Update();
		if (this.isAngry)
		{
			this.ZombieShootUpdate();
		}
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x00022110 File Offset: 0x00020310
	private void ZombieShootUpdate()
	{
		if (this.theStatus == 0 && GameAPP.theGameStatus == 0)
		{
			this.theZombieAttackCountDown -= Time.deltaTime;
			if (this.theZombieAttackCountDown < 0f)
			{
				this.theZombieAttackCountDown = this.theZombieAttackInterval;
				base.GetComponent<Animator>().Play("shoot", 1);
			}
		}
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x00022168 File Offset: 0x00020368
	public virtual GameObject AnimShoot()
	{
		if (this.theStatus == 1)
		{
			return null;
		}
		Vector3 position = base.transform.Find("Zombie_head").GetChild(0).transform.position;
		float x = position.x;
		float theY = position.y - 0.1f;
		int theZombieRow = this.theZombieRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(x, theY, theZombieRow, 3, 0);
		Vector3 position2 = gameObject.transform.GetChild(0).transform.position;
		gameObject.transform.GetChild(0).transform.position = new Vector3(position2.x, position2.y - 0.5f, position2.z);
		if (!this.isMindControlled)
		{
			gameObject.GetComponent<Bullet>().isZombieBullet = true;
		}
		gameObject.GetComponent<Bullet>().theBulletDamage = 0;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		return gameObject;
	}

	// Token: 0x040001ED RID: 493
	private bool losePaper;

	// Token: 0x040001EE RID: 494
	public float theZombieAttackInterval = 0.75f;

	// Token: 0x040001EF RID: 495
	[SerializeField]
	private float theZombieAttackCountDown;

	// Token: 0x040001F0 RID: 496
	[SerializeField]
	private bool isAngry;
}

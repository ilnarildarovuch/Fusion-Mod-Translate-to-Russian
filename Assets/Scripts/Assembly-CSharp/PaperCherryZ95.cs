using System;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x020000EB RID: 235
public class PaperCherryZ95 : PaperZombie
{
	// Token: 0x0600044E RID: 1102 RVA: 0x00022263 File Offset: 0x00020463
	protected override void Start()
	{
		base.Start();
		this.theStatus = 4;
		this.theAttackDamage = 400;
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x00022280 File Offset: 0x00020480
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
					child.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[38];
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
				if (child2.name == "LoseHead")
				{
					child2.gameObject.SetActive(true);
					child2.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
					child2.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += this.baseLayer + 29;
					child2.gameObject.GetComponent<ParticleSystem>().collision.AddPlane(this.board.transform.GetChild(2 + this.theZombieRow));
					child2.GetChild(0).gameObject.GetComponent<ParticleSystem>().collision.AddPlane(this.board.transform.GetChild(2 + this.theZombieRow));
					child2.GetChild(0).gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
					child2.GetChild(0).gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += this.baseLayer + 29;
					child2.AddComponent<ZombieHead>();
					Vector3 localScale = child2.transform.localScale;
					child2.transform.SetParent(this.board.transform);
					child2.transform.localScale = localScale;
				}
			}
			if (!this.losePaper)
			{
				this.SecondArmorFall();
			}
		}
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x000225BC File Offset: 0x000207BC
	public override void TakeDamage(int theDamageType, int theDamage)
	{
		if (this.theStatus == 4)
		{
			if (GameAPP.difficulty > 4 && !this.isMindControlled && theDamage > 0)
			{
				theDamage /= 2;
			}
			if (GameAPP.difficulty == 1 && !this.isMindControlled)
			{
				theDamage += 10;
			}
			this.flashTime = 0.3f;
			if (this.theSecondArmor != null)
			{
				this.SecondArmorTakeDamage(theDamage);
				return;
			}
		}
		else
		{
			if (this.theStatus == 5)
			{
				base.TakeDamage(theDamageType, 0);
				return;
			}
			base.TakeDamage(theDamageType, theDamage);
		}
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x0002263D File Offset: 0x0002083D
	public override void Charred()
	{
		if (this.theStatus != 6)
		{
			this.TakeDamage(10, 1800);
			return;
		}
		base.Charred();
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x0002265C File Offset: 0x0002085C
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

	// Token: 0x06000453 RID: 1107 RVA: 0x000226DB File Offset: 0x000208DB
	protected override void Update()
	{
		base.Update();
		if (this.theStatus == 6)
		{
			this.ZombieShootUpdate();
		}
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x000226F4 File Offset: 0x000208F4
	private void ZombieShootUpdate()
	{
		if (this.theStatus != 1 && GameAPP.theGameStatus == 0)
		{
			this.theZombieAttackCountDown -= Time.deltaTime;
			if (this.theZombieAttackCountDown < 0f)
			{
				this.theZombieAttackCountDown = this.theZombieAttackInterval;
				base.GetComponent<Animator>().Play("shoot", 1);
			}
		}
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x00022750 File Offset: 0x00020950
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

	// Token: 0x040001F1 RID: 497
	public float theZombieAttackInterval = 0.75f;

	// Token: 0x040001F2 RID: 498
	[SerializeField]
	private float theZombieAttackCountDown;
}

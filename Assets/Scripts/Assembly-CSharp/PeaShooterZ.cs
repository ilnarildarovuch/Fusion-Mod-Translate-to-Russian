using System;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x020000EC RID: 236
public class PeaShooterZ : Zombie
{
	// Token: 0x06000457 RID: 1111 RVA: 0x0002284B File Offset: 0x00020A4B
	protected override void Update()
	{
		base.Update();
		this.ZombieShootUpdate();
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x0002285C File Offset: 0x00020A5C
	private void ZombieShootUpdate()
	{
		if (this.theStatus == 1 || GameAPP.theGameStatus != 0)
		{
			return;
		}
		this.thePlantAttackCountDown -= Time.deltaTime;
		if (this.thePlantAttackCountDown < 0f)
		{
			this.thePlantAttackCountDown = this.thePlantAttackInterval;
			this.thePlantAttackCountDown += Random.Range(-0.1f, 0.1f);
			base.GetComponent<Animator>().Play("shoot", 1);
		}
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x000228D4 File Offset: 0x00020AD4
	public virtual GameObject AnimShoot()
	{
		if (this.theStatus == 1)
		{
			return null;
		}
		Vector3 position = base.transform.Find("Zombie_head").GetChild(0).transform.position;
		float theX;
		if (!this.isMindControlled)
		{
			theX = position.x - 0.4f;
		}
		else
		{
			theX = position.x + 0.4f;
		}
		float theY = position.y - 0.1f;
		int theZombieRow = this.theZombieRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, theY, theZombieRow, 0, 0);
		Vector3 position2 = gameObject.transform.GetChild(0).transform.position;
		gameObject.transform.GetChild(0).transform.position = new Vector3(position2.x, position2.y - 0.5f, position2.z);
		if (!this.isMindControlled)
		{
			gameObject.GetComponent<Bullet>().isZombieBullet = true;
		}
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		return gameObject;
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x000229E4 File Offset: 0x00020BE4
	protected override void BodyTakeDamage(int theDamage)
	{
		this.theHealth -= (float)theDamage;
		if (!this.isLoseHand && this.theHealth < (float)(this.theMaxHealth * 2 / 3))
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
					child.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[0];
					child.transform.localScale = new Vector3(4f, 4f, 4f);
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
			for (int j = 0; j < base.transform.childCount; j++)
			{
				Transform child2 = base.transform.GetChild(j);
				if (child2.CompareTag("ZombieHead"))
				{
					Object.Destroy(child2.gameObject);
				}
			}
		}
	}

	// Token: 0x040001F3 RID: 499
	public float thePlantAttackInterval = 1.5f;

	// Token: 0x040001F4 RID: 500
	private float thePlantAttackCountDown = 1.5f;
}

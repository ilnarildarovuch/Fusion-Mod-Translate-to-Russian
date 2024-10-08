using System;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x020000F0 RID: 240
public class ZombieJackson : Zombie
{
	// Token: 0x0600046E RID: 1134 RVA: 0x00023798 File Offset: 0x00021998
	protected override void Update()
	{
		base.Update();
		if (GameAPP.theGameStatus == 0)
		{
			this.moonWalkTime += Time.deltaTime;
		}
		if (this.moonWalkTime > 3f && !this.isMoonWalkFinish)
		{
			this.anim.SetTrigger("summon");
			this.isMoonWalkFinish = true;
		}
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x000237F0 File Offset: 0x000219F0
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.theZombieRow == 0)
		{
			if (this.dancer[1] == null || this.dancer[2] == null || this.dancer[3] == null)
			{
				this.anim.SetBool("loseDancer", true);
				return;
			}
		}
		else if (this.theZombieRow == 4)
		{
			if (this.dancer[0] == null || this.dancer[1] == null || this.dancer[2] == null)
			{
				this.anim.SetBool("loseDancer", true);
				return;
			}
		}
		else if (this.dancer[0] == null || this.dancer[1] == null || this.dancer[2] == null || this.dancer[3] == null)
		{
			this.anim.SetBool("loseDancer", true);
		}
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x000238EC File Offset: 0x00021AEC
	protected override void OnTriggerStay2D(Collider2D collision)
	{
		if (this.theStatus != 1 && this.theAttackTarget == null)
		{
			if (collision.gameObject.CompareTag("Plant") && !this.isMindControlled)
			{
				Plant component = collision.gameObject.GetComponent<Plant>();
				if (component.thePlantRow == this.theZombieRow)
				{
					if (TypeMgr.IsCaltrop(component.thePlantType))
					{
						return;
					}
					if (this.isAbledToAttack)
					{
						this.theAttackTarget = collision.gameObject;
						this.anim.SetBool("isAttacking", true);
						this.isAttacking = true;
						return;
					}
					if (!this.isMoonWalkFinish)
					{
						this.anim.SetTrigger("summon");
						this.isMoonWalkFinish = true;
					}
					return;
				}
			}
			if (collision.gameObject.CompareTag("Zombie"))
			{
				Zombie component2 = collision.gameObject.GetComponent<Zombie>();
				if (component2.theZombieRow == this.theZombieRow && component2.isMindControlled == !this.isMindControlled)
				{
					if (this.isAbledToAttack)
					{
						this.theAttackTarget = collision.gameObject;
						this.anim.SetBool("isAttacking", true);
						this.isAttacking = true;
						return;
					}
					if (!this.isMoonWalkFinish)
					{
						this.anim.SetTrigger("summon");
						this.isMoonWalkFinish = true;
					}
					return;
				}
			}
			IZEBrains izebrains;
			if (collision.TryGetComponent<IZEBrains>(out izebrains) && izebrains.theRow == this.theZombieRow && !this.isMindControlled)
			{
				if (this.isAbledToAttack)
				{
					this.theAttackTarget = collision.gameObject;
					this.anim.SetBool("isAttacking", true);
					this.isAttacking = true;
					return;
				}
				if (!this.isMoonWalkFinish)
				{
					this.anim.SetTrigger("summon");
					this.isMoonWalkFinish = true;
				}
				return;
			}
		}
		if (this.theStatus != 1)
		{
			if (collision.gameObject == this.theAttackTarget)
			{
				Plant plant;
				if (this.theAttackTarget.TryGetComponent<Plant>(out plant) && plant.thePlantRow != this.theZombieRow)
				{
					this.theAttackTarget = null;
					this.isAttacking = false;
					this.anim.SetBool("isAttacking", false);
					return;
				}
				Zombie zombie;
				if (this.theAttackTarget.TryGetComponent<Zombie>(out zombie) && zombie.theZombieRow != this.theZombieRow)
				{
					this.theAttackTarget = null;
					this.isAttacking = false;
					this.anim.SetBool("isAttacking", false);
					return;
				}
			}
		}
		else if (this.theStatus == 1)
		{
			this.theAttackTarget = null;
			this.isAttacking = false;
		}
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x00023B50 File Offset: 0x00021D50
	private void PointOver()
	{
		Debug.Log("允许攻击");
		this.isAbledToAttack = true;
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x00023B64 File Offset: 0x00021D64
	private void AnimSummon()
	{
		GameAPP.PlaySound(69, 0.5f);
		if (this.theStatus == 0)
		{
			this.anim.SetBool("loseDancer", false);
			if (this.dancer[0] == null && this.theZombieRow != 0 && this.board.roadType[this.theZombieRow - 1] != 1)
			{
				this.dancer[0] = this.board.GetComponent<CreateZombie>().SetZombie(0, this.theZombieRow - 1, 6, this.shadow.transform.position.x, false);
				this.CreateParticle(this.dancer[0].transform.Find("Shadow").position);
				if (this.isMindControlled)
				{
					this.dancer[0].GetComponent<Zombie>().SetMindControlWithOutEffect();
				}
			}
			if (this.dancer[1] == null)
			{
				this.dancer[1] = this.board.GetComponent<CreateZombie>().SetZombie(0, this.theZombieRow, 6, this.shadow.transform.position.x + 1f, false);
				this.CreateParticle(this.dancer[1].transform.Find("Shadow").position);
				if (this.isMindControlled)
				{
					this.dancer[1].GetComponent<Zombie>().SetMindControlWithOutEffect();
				}
			}
			if (this.dancer[2] == null)
			{
				this.dancer[2] = this.board.GetComponent<CreateZombie>().SetZombie(0, this.theZombieRow, 6, this.shadow.transform.position.x - 1.5f, false);
				this.CreateParticle(this.dancer[2].transform.Find("Shadow").position);
				if (this.isMindControlled)
				{
					this.dancer[2].GetComponent<Zombie>().SetMindControlWithOutEffect();
				}
			}
			if (this.dancer[3] == null && this.theZombieRow != this.board.roadNum - 1 && this.board.roadType[this.theZombieRow + 1] != 1)
			{
				this.dancer[3] = this.board.GetComponent<CreateZombie>().SetZombie(0, this.theZombieRow + 1, 6, this.shadow.transform.position.x, false);
				this.CreateParticle(this.dancer[3].transform.Find("Shadow").position);
				if (this.isMindControlled)
				{
					this.dancer[3].GetComponent<Zombie>().SetMindControlWithOutEffect();
				}
			}
		}
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x00023E08 File Offset: 0x00022008
	public override void SetMindControl(bool mustControl = false)
	{
		base.SetMindControl(mustControl);
		for (int i = 0; i < this.dancer.Length; i++)
		{
			this.dancer[i] = null;
		}
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x00023E38 File Offset: 0x00022038
	private void LookForward()
	{
		if (this.shadow != null)
		{
			if (this.isMindControlled)
			{
				Vector2 v = this.shadow.transform.position;
				base.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
				base.AdjustPosition(base.gameObject, v);
				return;
			}
			Vector2 v2 = this.shadow.transform.position;
			base.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			base.AdjustPosition(base.gameObject, v2);
		}
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x00023EF0 File Offset: 0x000220F0
	private void LookBack()
	{
		if (this.shadow != null)
		{
			if (this.isMindControlled)
			{
				Vector2 v = this.shadow.transform.position;
				base.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
				base.AdjustPosition(base.gameObject, v);
				return;
			}
			Vector2 v2 = this.shadow.transform.position;
			base.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			base.AdjustPosition(base.gameObject, v2);
		}
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x00023FA8 File Offset: 0x000221A8
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
					child.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[26];
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
					child2.gameObject.GetComponent<ParticleSystem>().collision.AddPlane(this.board.transform.GetChild(2 + this.theZombieRow));
					child2.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
					child2.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += this.baseLayer + 29;
					child2.AddComponent<ZombieHead>();
					Vector3 localScale = child2.transform.localScale;
					child2.transform.SetParent(this.board.transform);
					child2.transform.localScale = localScale;
				}
			}
		}
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x00024244 File Offset: 0x00022444
	private void AdjustAttackPosition()
	{
		if (this.shadow != null)
		{
			if (this.isMindControlled)
			{
				Vector2 v = this.shadow.transform.position;
				base.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
				base.AdjustPosition(base.gameObject, v);
				return;
			}
			Vector2 v2 = this.shadow.transform.position;
			base.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
			base.AdjustPosition(base.gameObject, v2);
		}
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x000242FC File Offset: 0x000224FC
	private void CreateParticle(Vector3 position)
	{
		Vector3 position2 = new Vector3(position.x, position.y + 0.7f);
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[11], position2, Quaternion.identity, this.board.transform);
	}

	// Token: 0x040001FA RID: 506
	private float moonWalkTime;

	// Token: 0x040001FB RID: 507
	private GameObject[] dancer = new GameObject[4];

	// Token: 0x040001FC RID: 508
	private bool isMoonWalkFinish;

	// Token: 0x040001FD RID: 509
	private bool isAbledToAttack;
}

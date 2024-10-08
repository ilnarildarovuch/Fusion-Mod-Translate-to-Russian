using System;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x020000E6 RID: 230
public class DolphinriderZ : Zombie
{
	// Token: 0x06000429 RID: 1065 RVA: 0x000208D5 File Offset: 0x0001EAD5
	protected override void Start()
	{
		base.Start();
		if (GameAPP.theGameStatus == 0)
		{
			GameAPP.PlaySound(78, 1f);
			this.theStatus = 8;
			this.anim.Play("ride");
			this.inWater = true;
			base.SetMaskLayer();
		}
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x00020914 File Offset: 0x0001EB14
	public override void Die(int reason = 0)
	{
		if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("ride"))
		{
			reason = 2;
		}
		if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("dolphinjump"))
		{
			reason = 2;
		}
		base.Die(reason);
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x00020964 File Offset: 0x0001EB64
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.theStatus == 8 && !this.isMindControlled && this.theFreezeCountDown == 0f)
		{
			this.jumpPos2 = new Vector2(this.shadow.transform.position.x - 0.4f, this.shadow.transform.position.y + 1f);
			Collider2D[] array = Physics2D.OverlapBoxAll(this.jumpPos2, this.range, 0f);
			bool flag = false;
			foreach (Collider2D collider2D in array)
			{
				if (collider2D.CompareTag("Plant"))
				{
					Plant component = collider2D.GetComponent<Plant>();
					if (!TypeMgr.IsTangkelp(component.thePlantType) && component.thePlantRow == this.theZombieRow)
					{
						if (TypeMgr.IsTallNut(component.thePlantType))
						{
							this.willJumpFail = true;
							this.failPos = component.shadow.transform.position;
							this.failPos = new Vector3(this.failPos.x + 0.5f, this.shadow.transform.position.y, 1f);
						}
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				this.theStatus = 9;
				this.anim.SetTrigger("jump");
				GameAPP.PlaySound(79, 0.5f);
			}
		}
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x00020AD2 File Offset: 0x0001ECD2
	protected override void OnTriggerStay2D(Collider2D collision)
	{
		if (this.theStatus == 0)
		{
			base.OnTriggerStay2D(collision);
		}
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x00020AE3 File Offset: 0x0001ECE3
	public virtual void JumpOver()
	{
		if (this.theStatus != 1)
		{
			this.theStatus = 0;
		}
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x00020AF8 File Offset: 0x0001ECF8
	private void CreateWaterSplash()
	{
		Vector2 vector = this.shadow.transform.position;
		vector = new Vector2(vector.x, vector.y - 0.4f);
		GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>("Particle/Anim/Water/WaterSplashPrefab"), vector, Quaternion.identity, GameAPP.board.transform);
		gameObject.transform.localScale = new Vector3(0.4f, 0.4f);
		foreach (object obj in gameObject.transform)
		{
			((Transform)obj).GetComponent<SpriteRenderer>().sortingLayerName = string.Format("particle{0}", this.theZombieRow);
		}
		vector = new Vector2(vector.x, vector.y + 0.4f);
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[32], vector, Quaternion.identity, GameAPP.board.transform);
		GameAPP.PlaySound(75, 0.5f);
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x00020C1C File Offset: 0x0001EE1C
	protected override void BodyTakeDamage(int theDamage)
	{
		this.theHealth -= (float)theDamage;
		if (!this.isLoseHand && this.theStatus == 0 && this.theHealth < (float)(this.theMaxHealth * 2 / 3))
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
					child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Zombies/Zombie_dolphinrider/Zombie_dolphinrider_outerarm_upper2");
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

	// Token: 0x06000430 RID: 1072 RVA: 0x00020EC4 File Offset: 0x0001F0C4
	public override void Charred()
	{
		if (GameAPP.difficulty == 4 && this.theHealth + (float)this.theFirstArmorHealth > 1800f)
		{
			this.TakeDamage(10, 1800);
			return;
		}
		if (GameAPP.difficulty == 5 && this.theHealth + (float)this.theFirstArmorHealth > 900f)
		{
			this.TakeDamage(10, 1800);
			return;
		}
		if (this.theStatus != 9 && this.shadow != null && this.theStatus == 0)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>("Zombies/Charred/Zombie_Charred"), Vector2.zero, Quaternion.identity, this.board.transform);
			Vector3 position = gameObject.transform.Find("Shadow").gameObject.transform.position;
			Vector3 b = this.shadow.transform.position - position;
			gameObject.transform.position += b;
			base.SetLayer(this.theZombieRow, gameObject);
		}
		this.Die(2);
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x00020FDC File Offset: 0x0001F1DC
	private void JumpFail()
	{
		if (this.willJumpFail)
		{
			GameAPP.PlaySound(64, 0.5f);
			if (this.theStatus != 1)
			{
				this.anim.Play("swim");
				if (this.shadow != null)
				{
					this.theStatus = 0;
					base.AdjustPosition(base.gameObject, this.failPos);
					Object.Instantiate<GameObject>(GameAPP.particlePrefab[23], new Vector3(this.shadow.transform.position.x, this.shadow.transform.position.y + 1.75f), Quaternion.identity, this.board.transform);
				}
			}
		}
	}

	// Token: 0x040001E4 RID: 484
	private Vector2 jumpPos2;

	// Token: 0x040001E5 RID: 485
	private Vector2 range = new Vector2(0.7f, 2f);

	// Token: 0x040001E6 RID: 486
	private bool willJumpFail;

	// Token: 0x040001E7 RID: 487
	private Vector3 failPos;

	// Token: 0x040001E8 RID: 488
	private bool loseHead;
}

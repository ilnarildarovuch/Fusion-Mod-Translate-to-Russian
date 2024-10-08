using System;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x020000ED RID: 237
public class PolevaulterZombie : Zombie
{
	// Token: 0x0600045C RID: 1116 RVA: 0x00022BCC File Offset: 0x00020DCC
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		if (this.polevaulterStatus == 0 && this.theStatus == 0 && !this.isMindControlled && this.theFreezeCountDown == 0f)
		{
			this.jumpPos2 = new Vector2(this.shadow.transform.position.x - 0.7f, this.shadow.transform.position.y + 1f);
			Collider2D[] array = Physics2D.OverlapBoxAll(this.jumpPos2, this.range, 0f);
			bool flag = false;
			foreach (Collider2D collider2D in array)
			{
				if (collider2D.CompareTag("Plant"))
				{
					Plant component = collider2D.GetComponent<Plant>();
					if (!TypeMgr.IsCaltrop(component.thePlantType) && component.thePlantRow == this.theZombieRow)
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
				this.polevaulterStatus = 1;
				this.theStatus = 3;
				this.anim.SetTrigger("jump");
				this.shadow.SetActive(false);
			}
		}
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x00022D4A File Offset: 0x00020F4A
	protected override void OnTriggerStay2D(Collider2D collision)
	{
		if (this.polevaulterStatus == 2)
		{
			base.OnTriggerStay2D(collision);
		}
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x00022D5C File Offset: 0x00020F5C
	public virtual void JumpOver()
	{
		if (this.shadow != null)
		{
			this.shadow.SetActive(true);
		}
		this.polevaulterStatus = 2;
		if (this.theStatus != 1)
		{
			this.theStatus = 0;
		}
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x00022D8F File Offset: 0x00020F8F
	public void PlayJumpSound1()
	{
		GameAPP.PlaySound(50, 0.5f);
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x00022D9D File Offset: 0x00020F9D
	public void PlayJumpSound2()
	{
		GameAPP.PlaySound(51, 0.5f);
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x00022DAC File Offset: 0x00020FAC
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
					child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Zombies/Zombie_polevaulter/Zombie_polevaulter_outerarm_upper2");
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

	// Token: 0x06000462 RID: 1122 RVA: 0x00023048 File Offset: 0x00021248
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
		if (this.polevaulterStatus != 1 && this.shadow != null && this.theStatus == 0)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>("Zombies/Charred/Zombie_Charred"), Vector2.zero, Quaternion.identity, this.board.transform);
			Vector3 position = gameObject.transform.Find("Shadow").gameObject.transform.position;
			Vector3 b = this.shadow.transform.position - position;
			gameObject.transform.position += b;
			base.SetLayer(this.theZombieRow, gameObject);
		}
		this.Die(2);
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x00023160 File Offset: 0x00021360
	private void JumpFail()
	{
		if (this.willJumpFail)
		{
			GameAPP.PlaySound(64, 0.5f);
			if (this.theStatus != 1)
			{
				this.anim.Play("walk2");
				if (this.shadow != null)
				{
					this.shadow.SetActive(true);
					this.polevaulterStatus = 2;
					this.theStatus = 0;
					base.AdjustPosition(base.gameObject, this.failPos);
					Object.Instantiate<GameObject>(GameAPP.particlePrefab[23], new Vector3(this.shadow.transform.position.x, this.shadow.transform.position.y + 1.75f), Quaternion.identity, this.board.transform);
				}
			}
		}
	}

	// Token: 0x040001F5 RID: 501
	public int polevaulterStatus;

	// Token: 0x040001F6 RID: 502
	private Vector2 jumpPos2;

	// Token: 0x040001F7 RID: 503
	private Vector2 range = new Vector2(0.7f, 2f);

	// Token: 0x040001F8 RID: 504
	private bool willJumpFail;

	// Token: 0x040001F9 RID: 505
	private Vector3 failPos;

	// Token: 0x02000145 RID: 325
	public enum PolStatus
	{
		// Token: 0x0400046B RID: 1131
		run,
		// Token: 0x0400046C RID: 1132
		jump,
		// Token: 0x0400046D RID: 1133
		walk
	}
}

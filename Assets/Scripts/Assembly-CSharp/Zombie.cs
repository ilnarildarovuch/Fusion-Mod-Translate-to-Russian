using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x020000F1 RID: 241
public class Zombie : MonoBehaviour
{
	// Token: 0x0600047A RID: 1146 RVA: 0x00024358 File Offset: 0x00022558
	protected virtual void Awake()
	{
		this.anim = base.GetComponent<Animator>();
		if (base.transform.Find("Shadow") != null)
		{
			this.shadow = base.transform.Find("Shadow").gameObject;
		}
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x000243A4 File Offset: 0x000225A4
	protected virtual void Start()
	{
		if (GameAPP.difficulty == 5 && !this.board.isIZ)
		{
			this.theOriginSpeed += 0.1f;
		}
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x000243D0 File Offset: 0x000225D0
	protected virtual void Update()
	{
		this.MoveUpdate();
		if (this.theHealth <= 0f)
		{
			this.Die(0);
		}
		if (this.theStatus == 1)
		{
			this.theHealth -= 0.3f * (float)this.theMaxHealth / 270f;
		}
		if (GameAPP.theGameStatus == 0 && ((this.isMindControlled && base.transform.position.x > 10f) || base.transform.position.x > 12f || base.transform.position.x < -10f))
		{
			this.Die(2);
		}
		if (Input.GetKeyDown(KeyCode.T))
		{
			this.ChangeRow(this.theZombieRow - 1);
		}
		if (Input.GetKeyDown(KeyCode.Y))
		{
			this.ChangeRow(this.theZombieRow + 1);
		}
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x000244A9 File Offset: 0x000226A9
	protected virtual void FixedUpdate()
	{
		this.FlashUpdate();
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x000244B4 File Offset: 0x000226B4
	public void ChangeRow(int theTargetRow)
	{
		if (theTargetRow == this.theZombieRow)
		{
			return;
		}
		if (theTargetRow < 0)
		{
			theTargetRow = 0;
		}
		if (theTargetRow > this.board.roadNum - 1)
		{
			theTargetRow = this.board.roadNum - 1;
		}
		int theCurrentRow = this.theZombieRow;
		if (this.isChangingRow)
		{
			base.StopCoroutine(this.changeRow);
			this.isChangingRow = false;
		}
		this.changeRow = base.StartCoroutine(this.MoveRow(theTargetRow, theCurrentRow));
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x00024528 File Offset: 0x00022728
	private void SetRowLayer(GameObject obj, int theRow)
	{
		if (obj.name == "Shadow")
		{
			return;
		}
		SpriteRenderer spriteRenderer;
		if (obj.TryGetComponent<SpriteRenderer>(out spriteRenderer))
		{
			spriteRenderer.sortingLayerName = string.Format("zombie{0}", theRow);
		}
		if (obj.transform.childCount != 0)
		{
			foreach (object obj2 in obj.transform)
			{
				Transform transform = (Transform)obj2;
				this.SetRowLayer(transform.gameObject, theRow);
			}
		}
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x000245C8 File Offset: 0x000227C8
	private IEnumerator MoveRow(int theTargetRow, int theCurrentRow)
	{
		this.isChangingRow = true;
		int theNextRow = theTargetRow;
		if (Mathf.Abs(theTargetRow - theCurrentRow) > 1)
		{
			if (theTargetRow > theCurrentRow)
			{
				theNextRow = theCurrentRow + 1;
			}
			else
			{
				theNextRow = theCurrentRow - 1;
			}
		}
		this.theZombieRow = theNextRow;
		this.SetRowLayer(base.gameObject, theNextRow);
		float startY = this.shadow.transform.position.y;
		float elapsedTime = 0f;
		float duringTime = 8f / this.theSpeed * (float)Mathf.Abs(theNextRow - theCurrentRow);
		float newY = Mouse.Instance.GetBoxYFromRow(theNextRow) + 0.1f;
		while (elapsedTime < duringTime)
		{
			float num = Mathf.Lerp(startY, newY, elapsedTime / duringTime) - this.shadow.transform.position.y;
			if (this.freezeSpeed != 0f && this.board.isTowerDefense && (!this.isAttacking || elapsedTime < 0.9f * duringTime))
			{
				base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + num, 1f);
				elapsedTime += Time.deltaTime;
			}
			yield return null;
		}
		theCurrentRow = theNextRow;
		this.isChangingRow = false;
		if (Mathf.Abs(theTargetRow - theCurrentRow) > 1)
		{
			this.changeRow = base.StartCoroutine(this.MoveRow(theTargetRow, theCurrentRow));
		}
		else if (Mathf.Abs(theTargetRow - theCurrentRow) == 1)
		{
			this.changeRow = base.StartCoroutine(this.MoveRow(theTargetRow, theCurrentRow));
		}
		float num2 = newY - this.shadow.transform.position.y;
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + num2);
		yield break;
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x000245E8 File Offset: 0x000227E8
	public virtual void Die(int reason = 0)
	{
		if (this.isDoom)
		{
			reason = 2;
			Vector2 position = new Vector2(this.shadow.transform.position.x - 0.3f, this.shadow.transform.position.y + 0.2f);
			Vector2 vector = Camera.main.WorldToScreenPoint(this.shadow.transform.position);
			this.board.SetDoom(Mouse.Instance.GetColumnFromX(vector.x), this.theZombieRow, true, position);
		}
		this.theStatus = 1;
		if (this.iceTrap != null || this.theFreezeCountDown > 0f)
		{
			this.theFreezeCountDown = 0f;
			this.Unfrezzing();
		}
		if (this.grap != null)
		{
			Object.Destroy(this.grap);
		}
		if (this.isDying)
		{
			if (reason != this.dieReason)
			{
				Object.Destroy(base.gameObject);
			}
			return;
		}
		this.isDying = true;
		this.dieReason = reason;
		this.DieEvent();
		if (!this.isMindControlled)
		{
			this.board.theCurrentNumOfZombieUncontroled--;
		}
		if (!this.isMindControlled)
		{
			this.DropItem();
		}
		switch (reason)
		{
		case 0:
			Object.Destroy(base.GetComponent<Collider2D>());
			this.shadow.GetComponent<SpriteRenderer>().enabled = false;
			this.anim.SetTrigger("GoDie");
			return;
		case 1:
		{
			GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
			Vector2 vector2 = base.transform.Find("Shadow").transform.position;
			Object.Instantiate<GameObject>(GameAPP.particlePrefab[11], new Vector3(vector2.x, vector2.y + 1f, 0f), Quaternion.identity).transform.SetParent(GameAPP.board.transform);
			Object.Destroy(base.gameObject);
			return;
		}
		case 2:
			Object.Destroy(base.gameObject);
			return;
		default:
			return;
		}
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x000247F3 File Offset: 0x000229F3
	protected virtual void DieEvent()
	{
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x000247F8 File Offset: 0x000229F8
	public virtual void TakeDamage(int theDamageType, int theDamage)
	{
		if (this.isJalaed)
		{
			theDamage = (int)(1.5f * (float)theDamage);
		}
		if (GameAPP.difficulty > 4 && !this.isMindControlled && theDamage > 0)
		{
			theDamage /= 2;
		}
		if (GameAPP.difficulty == 1 && !this.isMindControlled)
		{
			theDamage += 10;
		}
		this.flashTime = 0.3f;
		int num = theDamage;
		switch (theDamageType)
		{
		case 0:
			if (this.theSecondArmor != null)
			{
				num = this.SecondArmorTakeDamage(theDamage);
				if (num == 0)
				{
					return;
				}
			}
			if (this.theFirstArmor != null)
			{
				num = this.FirstArmorTakeDamage(num);
				if (num == 0)
				{
					return;
				}
			}
			this.BodyTakeDamage(num);
			return;
		case 1:
			if (this.theSecondArmor != null)
			{
				this.SecondArmorTakeDamage(num);
			}
			if (this.theFirstArmor != null)
			{
				num = this.FirstArmorTakeDamage(num);
				if (num == 0)
				{
					return;
				}
			}
			this.BodyTakeDamage(num);
			return;
		case 2:
			if (this.theSecondArmor != null)
			{
				num = this.SecondArmorTakeDamage(theDamage);
				if (num == 0)
				{
					return;
				}
			}
			this.SetCold(10f);
			if (this.theFirstArmor != null)
			{
				num = this.FirstArmorTakeDamage(num);
				if (num == 0)
				{
					return;
				}
			}
			this.BodyTakeDamage(num);
			return;
		case 3:
			if (this.theSecondArmor != null)
			{
				this.SecondArmorTakeDamage(num);
			}
			this.SetCold(10f);
			if (this.theFirstArmor != null)
			{
				num = this.FirstArmorTakeDamage(num);
				if (num == 0)
				{
					return;
				}
			}
			this.BodyTakeDamage(num);
			return;
		case 4:
			if (this.theFirstArmor != null)
			{
				num = this.FirstArmorTakeDamage(num);
				if (num == 0)
				{
					return;
				}
			}
			this.BodyTakeDamage(num);
			return;
		case 5:
			this.SetCold(10f);
			if (this.theFirstArmor != null)
			{
				num = this.FirstArmorTakeDamage(num);
				if (num == 0)
				{
					return;
				}
			}
			this.BodyTakeDamage(num);
			return;
		case 6:
		case 7:
		case 8:
		case 9:
			break;
		case 10:
			if (this.theSecondArmor != null)
			{
				this.SecondArmorTakeDamage(theDamage);
			}
			if (this.theFirstArmor != null)
			{
				num = this.FirstArmorTakeDamage(num);
				if (num == 0)
				{
					return;
				}
			}
			this.BodyTakeDamage(num);
			return;
		case 11:
			if (this.theSecondArmor != null)
			{
				this.SecondArmorTakeDamage(theDamage);
			}
			if (this.theFirstArmor != null)
			{
				num = this.FirstArmorTakeDamage(num);
				if (num == 0)
				{
					return;
				}
			}
			if (this.theHealth <= (float)num)
			{
				this.Die(2);
				return;
			}
			this.BodyTakeDamage(num);
			break;
		default:
			return;
		}
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x00024A59 File Offset: 0x00022C59
	protected virtual int FirstArmorTakeDamage(int theDamage)
	{
		return 0;
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x00024A5C File Offset: 0x00022C5C
	protected virtual int SecondArmorTakeDamage(int theDamage)
	{
		return 0;
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x00024A60 File Offset: 0x00022C60
	protected virtual void BodyTakeDamage(int theDamage)
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

	// Token: 0x06000487 RID: 1159 RVA: 0x00024D18 File Offset: 0x00022F18
	private void FlashUpdate()
	{
		if (this.flashTime > 0f)
		{
			if (this.flashTime > 0.2f)
			{
				this.SetBrightness(-30f * this.flashTime + 10f, base.gameObject);
			}
			else if (this.flashTime > 0f)
			{
				this.SetBrightness(15f * this.flashTime + 1f, base.gameObject);
			}
			this.flashTime -= 0.02f;
			if (this.flashTime == 0f)
			{
				this.SetBrightness(1f, base.gameObject);
			}
		}
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x00024DC0 File Offset: 0x00022FC0
	protected void SetBrightness(float b, GameObject obj)
	{
		if (obj != this.shadow)
		{
			SpriteRenderer spriteRenderer;
			if (obj.TryGetComponent<SpriteRenderer>(out spriteRenderer))
			{
				spriteRenderer.material.SetFloat("_Brightness", b);
			}
			if (obj.transform.childCount > 0)
			{
				foreach (object obj2 in obj.transform)
				{
					Transform transform = (Transform)obj2;
					this.SetBrightness(b, transform.gameObject);
				}
			}
		}
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x00024E58 File Offset: 0x00023058
	protected virtual void OnTriggerStay2D(Collider2D collision)
	{
		if (this.theStatus != 1 && this.theAttackTarget == null)
		{
			Plant plant;
			if (!this.isMindControlled && collision.TryGetComponent<Plant>(out plant))
			{
				if (this.board.isTowerDefense && this.board.boxType[plant.thePlantColumn, plant.thePlantRow] != 2)
				{
					return;
				}
				if (plant.thePlantRow == this.theZombieRow)
				{
					if (TypeMgr.IsCaltrop(plant.thePlantType))
					{
						return;
					}
					this.theAttackTarget = collision.gameObject;
					this.anim.SetBool("isAttacking", true);
					this.isAttacking = true;
					return;
				}
			}
			Zombie zombie;
			if (collision.TryGetComponent<Zombie>(out zombie) && zombie.isMindControlled == !this.isMindControlled && zombie.theZombieRow == this.theZombieRow)
			{
				this.theAttackTarget = collision.gameObject;
				this.anim.SetBool("isAttacking", true);
				this.isAttacking = true;
				return;
			}
			IZEBrains izebrains;
			if (this.board.isIZ && collision.TryGetComponent<IZEBrains>(out izebrains) && izebrains.theRow == this.theZombieRow && !this.isMindControlled)
			{
				this.theAttackTarget = izebrains.gameObject;
				this.anim.SetBool("isAttacking", true);
				this.isAttacking = true;
				return;
			}
		}
		if (this.theStatus != 1)
		{
			if (collision.gameObject == this.theAttackTarget)
			{
				Plant plant2;
				if (this.theAttackTarget.TryGetComponent<Plant>(out plant2) && plant2.thePlantRow != this.theZombieRow)
				{
					this.theAttackTarget = null;
					this.isAttacking = false;
					this.anim.SetBool("isAttacking", false);
					return;
				}
				Zombie zombie2;
				if (this.theAttackTarget.TryGetComponent<Zombie>(out zombie2) && zombie2.theZombieRow != this.theZombieRow)
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

	// Token: 0x0600048A RID: 1162 RVA: 0x00025058 File Offset: 0x00023258
	protected virtual void OnTriggerExit2D(Collider2D collision)
	{
		if (this.theStatus != 1)
		{
			if (collision.gameObject == this.theAttackTarget)
			{
				this.theAttackTarget = null;
				this.isAttacking = false;
				this.anim.SetBool("isAttacking", false);
				return;
			}
		}
		else
		{
			this.theAttackTarget = null;
			this.isAttacking = false;
		}
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x000250AF File Offset: 0x000232AF
	public void PlayFallSound()
	{
		GameAPP.PlaySound(Random.Range(5, 7), 0.5f);
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x000250C2 File Offset: 0x000232C2
	public void DestoryZombie()
	{
		this.DecreaseT(base.gameObject);
		Object.Destroy(base.gameObject, 0.1f);
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x000250E0 File Offset: 0x000232E0
	private void DecreaseT(GameObject obj)
	{
		SpriteRenderer spriteRenderer;
		if (obj.TryGetComponent<SpriteRenderer>(out spriteRenderer))
		{
			base.StartCoroutine(this.DecreaseTransparent(spriteRenderer.material));
		}
		if (obj.transform.childCount > 0)
		{
			foreach (object obj2 in obj.transform)
			{
				Transform transform = (Transform)obj2;
				this.DecreaseT(transform.gameObject);
			}
		}
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x0002516C File Offset: 0x0002336C
	private IEnumerator DecreaseTransparent(Material mt)
	{
		float i = 1f;
		while ((double)i > -0.2)
		{
			mt.SetFloat("_Transparent", i);
			yield return new WaitForFixedUpdate();
			i -= 0.2f;
		}
		mt.SetFloat("_Transparent", 0f);
		yield break;
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x0002517C File Offset: 0x0002337C
	public virtual void PlayEatSound()
	{
		if (this.theStatus == 1)
		{
			return;
		}
		GameObject gameObject = this.theAttackTarget;
		if (gameObject != null)
		{
			Plant plant;
			if (gameObject.TryGetComponent<Plant>(out plant))
			{
				if (this.isMindControlled)
				{
					this.isAttacking = false;
					this.anim.SetBool("isAttacking", false);
					return;
				}
				this.AttackPlant(plant);
			}
			Zombie zombie;
			if (gameObject.TryGetComponent<Zombie>(out zombie))
			{
				if (zombie.isMindControlled != this.isMindControlled)
				{
					this.AttackZombie(zombie);
				}
				else
				{
					this.isAttacking = false;
					this.anim.SetBool("isAttacking", false);
				}
			}
			IZEBrains brain;
			if (gameObject.TryGetComponent<IZEBrains>(out brain))
			{
				if (!this.isMindControlled)
				{
					this.AttackBrain(brain);
					return;
				}
				this.isAttacking = false;
				this.anim.SetBool("isAttacking", false);
				return;
			}
		}
		else
		{
			this.isAttacking = false;
			this.anim.SetBool("isAttacking", false);
		}
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x0002525E File Offset: 0x0002345E
	private void AttackBrain(IZEBrains brain)
	{
		brain.theHealth -= this.theAttackDamage;
		GameAPP.PlaySound(Random.Range(8, 10), 0.3f);
		brain.FlashOnce();
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x0002528C File Offset: 0x0002348C
	private void AttackPlant(Plant plant)
	{
		int thePlantType = plant.thePlantType;
		if (thePlantType <= 1026)
		{
			if (thePlantType != 8)
			{
				if (thePlantType == 900)
				{
					this.SetMindControl(true);
					plant.GetComponent<HyponoEmperor>().restHealth--;
					return;
				}
				switch (thePlantType)
				{
				case 1022:
				{
					this.SetMindControl(true);
					plant.Die();
					if (this.theSecondArmorMaxHealth > 0)
					{
						this.theSecondArmorMaxHealth /= 2;
						this.theSecondArmorHealth /= 2;
					}
					if (this.theFirstArmorMaxHealth > 0)
					{
						this.theFirstArmorMaxHealth /= 2;
						this.theFirstArmorHealth /= 2;
					}
					if (this.theHealth > 0f)
					{
						this.theMaxHealth /= 2;
						this.theHealth /= 2f;
					}
					Vector2 v = this.shadow.transform.position;
					Vector3 localScale = base.transform.localScale;
					base.transform.localScale = new Vector3(0.5f * localScale.x, 0.5f * localScale.y, 0.5f * localScale.z);
					using (IEnumerator enumerator = base.transform.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ParticleSystem particleSystem;
							if (((Transform)enumerator.Current).TryGetComponent<ParticleSystem>(out particleSystem))
							{
								Vector3 localScale2 = particleSystem.transform.localScale;
								particleSystem.transform.localScale = new Vector3(0.5f * localScale2.x, 0.5f * localScale2.y, 0.5f * localScale2.z);
							}
						}
					}
					this.AdjustPosition(base.gameObject, v);
					base.transform.Translate(0f, 0f, -1f);
					return;
				}
				case 1023:
				case 1024:
				case 1026:
					break;
				case 1025:
					goto IL_2D6;
				default:
					goto IL_2D6;
				}
			}
			this.SetMindControl(true);
			plant.Die();
			return;
		}
		if (thePlantType <= 1041)
		{
			if (thePlantType != 1039)
			{
				if (thePlantType == 1041)
				{
					this.board.CreateFreeze(plant.shadow.transform.position);
					Vector2 vector = this.shadow.transform.position;
					CreateZombie.Instance.SetZombie(0, this.theZombieRow, 111, vector.x, false).GetComponent<Zombie>().SetMindControl(true);
					plant.Die();
					this.Die(2);
					return;
				}
			}
			else
			{
				this.SetCold(10f);
				this.AddfreezeLevel(5);
			}
		}
		else
		{
			if (thePlantType == 1045)
			{
				this.isDoom = true;
				this.SetMindControl(true);
				plant.Die();
				return;
			}
			if (thePlantType == 1073)
			{
				this.TakeDamage(4, 20);
				this.SetJalaed();
			}
		}
		IL_2D6:
		if (plant.isNut)
		{
			GameAPP.PlaySound(10, 0.3f);
			GameObject gameObject = GameAPP.particlePrefab[5];
			if (plant.thePlantType == 1003)
			{
				gameObject = GameAPP.particlePrefab[6];
			}
			Transform transform = base.transform.Find("Zombie_jaw");
			if (transform != null)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, this.board.transform);
				gameObject2.name = gameObject.name;
				gameObject2.transform.position = transform.position;
			}
		}
		else
		{
			GameAPP.PlaySound(Random.Range(8, 10), 0.3f);
		}
		plant.FlashOnce();
		if (!plant.isAshy)
		{
			plant.thePlantHealth -= this.theAttackDamage;
			if (plant.thePlantHealth <= 0)
			{
				GameAPP.PlaySound(11, 0.5f);
			}
		}
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x00025648 File Offset: 0x00023848
	private void AttackZombie(Zombie zombie)
	{
		zombie.TakeDamage(4, this.theAttackDamage);
		GameAPP.PlaySound(Random.Range(8, 10), 0.5f);
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x0002566C File Offset: 0x0002386C
	protected void DropItem()
	{
		if (this.board.isTowerDefense && !this.droppedSun)
		{
			int num = this.theZombieType;
			if (num <= 9)
			{
				if (num - 4 > 1 && num != 9)
				{
					goto IL_196;
				}
			}
			else if (num - 15 > 1)
			{
				if (num != 18)
				{
					switch (num)
					{
					case 106:
					case 111:
						break;
					case 107:
						goto IL_15C;
					case 108:
					case 110:
						goto IL_196;
					case 109:
						CreateCoin.Instance.SetCoin(0, 0, 0, 0, this.shadow.transform.position + new Vector3(0f, 0.5f, 0f));
						CreateCoin.Instance.SetCoin(0, 0, 0, 0, this.shadow.transform.position + new Vector3(0f, 0.5f, 0f));
						goto IL_1CE;
					default:
						goto IL_196;
					}
				}
				CreateCoin.Instance.SetCoin(0, 0, 0, 0, this.shadow.transform.position + new Vector3(0f, 0.5f, 0f));
				CreateCoin.Instance.SetCoin(0, 0, 2, 0, this.shadow.transform.position + new Vector3(0f, 0.5f, 0f));
				goto IL_1CE;
			}
			IL_15C:
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, this.shadow.transform.position + new Vector3(0f, 0.5f, 0f));
			goto IL_1CE;
			IL_196:
			CreateCoin.Instance.SetCoin(0, 0, 2, 0, this.shadow.transform.position + new Vector3(0f, 0.5f, 0f));
			IL_1CE:
			this.droppedSun = true;
		}
		if (this.board.isIZ)
		{
			return;
		}
		if (this.board.droppedAwardOrOver)
		{
			return;
		}
		if (GameAPP.theBoardType == 3)
		{
			if (this.board.theWave >= this.board.theMaxWave && this.board.theCurrentNumOfZombieUncontroled <= 0)
			{
				if (this.board.theCurrentSurvivalRound >= this.board.theSurvivalMaxRound)
				{
					this.board.droppedAwardOrOver = true;
					GameObject gameObject = Resources.Load<GameObject>("Board/Award/TrophyPrefab");
					GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, this.board.gameObject.transform);
					gameObject2.name = gameObject.name;
					gameObject2.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 1f, 0f);
					Vector2 vector = Camera.main.WorldToViewportPoint(gameObject2.transform.position);
					if (vector.x < 0.2f)
					{
						vector.x = 0.2f;
					}
					else if (vector.x > 0.8f)
					{
						vector.x = 0.8f;
					}
					if (vector.y < 0.2f)
					{
						vector.y = 0.2f;
					}
					else if (vector.y > 0.8f)
					{
						vector.y = 0.8f;
					}
					gameObject2.transform.position = Camera.main.ViewportToWorldPoint(vector);
					gameObject2.transform.position = new Vector3(gameObject2.transform.position.x, gameObject2.transform.position.y, 0f);
					return;
				}
				this.board.droppedAwardOrOver = true;
				this.board.EnterNextRound();
				return;
			}
		}
		else if (this.board.theWave >= this.board.theMaxWave && this.board.theCurrentNumOfZombieUncontroled <= 0)
		{
			this.board.droppedAwardOrOver = true;
			GameObject gameObject3 = Resources.Load<GameObject>("Board/Award/TrophyPrefab");
			GameObject gameObject4 = Object.Instantiate<GameObject>(gameObject3, this.board.gameObject.transform);
			gameObject4.name = gameObject3.name;
			gameObject4.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + 1f, 0f);
			Vector2 vector2 = Camera.main.WorldToViewportPoint(gameObject4.transform.position);
			if (vector2.x < 0.2f)
			{
				vector2.x = 0.2f;
			}
			else if (vector2.x > 0.8f)
			{
				vector2.x = 0.8f;
			}
			if (vector2.y < 0.2f)
			{
				vector2.y = 0.2f;
			}
			else if (vector2.y > 0.8f)
			{
				vector2.y = 0.8f;
			}
			gameObject4.transform.position = Camera.main.ViewportToWorldPoint(vector2);
			gameObject4.transform.position = new Vector3(gameObject4.transform.position.x, gameObject4.transform.position.y, 0f);
		}
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x00025BA4 File Offset: 0x00023DA4
	public virtual void Charred()
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
		if (this.shadow != null && this.theStatus != 1 && !this.inWater && this.ExistAnim())
		{
			GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>("Zombies/Charred/Zombie_Charred"), Vector2.zero, Quaternion.identity, this.board.transform);
			Vector3 position = gameObject.transform.Find("Shadow").gameObject.transform.position;
			Vector3 b = this.shadow.transform.position - position;
			gameObject.transform.position += b;
			this.SetLayer(this.theZombieRow, gameObject);
		}
		this.Die(2);
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x00025CC8 File Offset: 0x00023EC8
	private bool ExistAnim()
	{
		int num = this.theZombieType;
		return num != 16 && num != 18 && num != 201;
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x00025CF4 File Offset: 0x00023EF4
	protected void SetLayer(int theRow, GameObject charred)
	{
		foreach (object obj in charred.transform)
		{
			Transform transform = (Transform)obj;
			Renderer component = transform.GetComponent<Renderer>();
			transform.GetComponent<Renderer>().sortingOrder += this.baseLayer;
			component.sortingLayerName = string.Format("zombie{0}", theRow);
		}
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x00025D7C File Offset: 0x00023F7C
	public virtual void SetMindControl(bool mustControl = false)
	{
		if (this.isMindControlled)
		{
			return;
		}
		if (!mustControl && GameAPP.difficulty == 5 && Random.Range(0, 2) == 1)
		{
			return;
		}
		this.SetLayerMask();
		this.isJalaed = false;
		this.board.theCurrentNumOfZombieUncontroled--;
		this.DropItem();
		GameAPP.PlaySound(62, 0.5f);
		GameAPP.PlaySound(63, 0.5f);
		Vector2 vector = this.shadow.transform.position;
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[20], new Vector3(vector.x, vector.y + 1.5f), Quaternion.identity, this.board.transform);
		this.isMindControlled = true;
		base.transform.Rotate(0f, 180f, 0f);
		this.AdjustPosition(base.gameObject, vector);
		base.transform.Translate(0f, 0f, -1f);
		if (this.isDoom)
		{
			this.SetColor(base.gameObject, 4);
		}
		else
		{
			this.SetColor(base.gameObject, 2);
		}
		this.theAttackTarget = null;
		this.isAttacking = false;
		if (this.anim.parameters.Any((AnimatorControllerParameter param) => param.name == "isAttacking"))
		{
			this.anim.SetBool("isAttacking", false);
		}
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x00025EF4 File Offset: 0x000240F4
	public void SetMindControlWithOutEffect()
	{
		if (this.isMindControlled)
		{
			return;
		}
		this.SetLayerMask();
		this.isJalaed = false;
		this.board.theCurrentNumOfZombieUncontroled--;
		this.DropItem();
		Vector2 v = this.shadow.transform.position;
		this.isMindControlled = true;
		base.transform.Rotate(0f, 180f, 0f);
		this.AdjustPosition(base.gameObject, v);
		base.transform.Translate(0f, 0f, -1f);
		this.SetColor(base.gameObject, 2);
		this.theAttackTarget = null;
		this.isAttacking = false;
		if (this.anim.parameters.Any((AnimatorControllerParameter param) => param.name == "isAttacking"))
		{
			this.anim.SetBool("isAttacking", false);
		}
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x00025FF0 File Offset: 0x000241F0
	private void SetLayerMask()
	{
		base.gameObject.layer = LayerMask.NameToLayer("MindControlledZombie");
		base.GetComponent<Collider2D>().excludeLayers = LayerMask.GetMask(new string[]
		{
			"MindControlledZombie"
		});
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0002602C File Offset: 0x0002422C
	public void AdjustPosition(GameObject zombie, Vector3 position)
	{
		if (this.shadow != null)
		{
			Vector3 position2 = this.shadow.transform.position;
			Vector3 b = position - position2;
			zombie.transform.position += b;
		}
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x00026078 File Offset: 0x00024278
	public void SetColor(GameObject obj, int colorType)
	{
		Color color = new Color
		{
			a = 1f
		};
		if (this.isJalaed)
		{
			colorType = 3;
		}
		switch (colorType)
		{
		case 1:
			color.r = 0.3529412f;
			color.g = 0.39215687f;
			color.b = 1f;
			break;
		case 2:
			color.r = 0.8235294f;
			color.g = 0.47058824f;
			color.b = 1f;
			break;
		case 3:
			color.r = 1f;
			color.g = 0.627451f;
			color.b = 0.627451f;
			break;
		case 4:
			color.r = 0.8235294f;
			color.g = 0.078431375f;
			color.b = 0.078431375f;
			break;
		default:
			color = Color.white;
			break;
		}
		if (obj.name == "Shadow")
		{
			return;
		}
		SpriteRenderer spriteRenderer;
		if (obj.TryGetComponent<SpriteRenderer>(out spriteRenderer))
		{
			spriteRenderer.color = color;
		}
		if (obj.transform.childCount != 0)
		{
			foreach (object obj2 in obj.transform)
			{
				Transform transform = (Transform)obj2;
				this.SetColor(transform.gameObject, colorType);
			}
		}
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x000261EC File Offset: 0x000243EC
	public void AddfreezeLevel(int level)
	{
		this.freezeLevel += level;
		if (this.freezeLevel >= this.freezeMaxLevel && this.freezeSpeed != 0f)
		{
			this.freezeLevel = 0;
			if (this.freezeMaxLevel < 400)
			{
				this.freezeMaxLevel += 100;
			}
			this.SetFreeze(4f);
		}
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x00026250 File Offset: 0x00024450
	public void SetJalaed()
	{
		this.SetColor(base.gameObject, 3);
		this.isJalaed = true;
		this.Warm(0);
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00026270 File Offset: 0x00024470
	public virtual void SetFreeze(float time)
	{
		if (this.isDying)
		{
			return;
		}
		this.isJalaed = false;
		int num = this.theStatus;
		if (num == 3 || num == 9)
		{
			this.SetCold(10f);
		}
		if (this.theFreezeCountDown > 0f && this.theFreezeCountDown < time)
		{
			this.theFreezeCountDown = time;
			return;
		}
		if (this.theFreezeCountDown == 0f)
		{
			this.theFreezeCountDown = time;
			this.theSlowCountDown = 10f + time;
			this.coldSpeed = 0.5f;
			this.SetColor(base.gameObject, 1);
			this.freezeSpeed = 0f;
			if (this.iceTrap != null)
			{
				Object.Destroy(this.iceTrap);
			}
			if (!this.inWater)
			{
				GameObject original = Resources.Load<GameObject>("Image/ice/icetrap");
				this.iceTrap = Object.Instantiate<GameObject>(original, this.shadow.transform.position, Quaternion.identity);
				this.iceTrap.transform.SetParent(base.transform);
				this.iceTrap.GetComponent<SpriteRenderer>().sortingLayerName = string.Format("particle{0}", this.theZombieRow);
			}
		}
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x00026398 File Offset: 0x00024598
	private void Unfrezzing()
	{
		if (this.shadow != null)
		{
			Vector2 vector = this.shadow.transform.position;
			vector = new Vector2(vector.x, vector.y + 1f);
			GameObject gameObject = Resources.Load<GameObject>("Particle/Prefabs/IceTrap");
			Object.Instantiate<GameObject>(gameObject, vector, Quaternion.identity, this.board.transform);
			gameObject.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = string.Format("particle{0}", this.theZombieRow);
		}
		this.freezeSpeed = 1f;
		this.freezeLevel = 0;
		Object.Destroy(this.iceTrap);
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x0002644C File Offset: 0x0002464C
	public virtual void SetCold(float time)
	{
		this.isJalaed = false;
		this.theSlowCountDown = time;
		this.coldSpeed = 0.5f;
		this.SetColor(base.gameObject, 1);
		if (this.theSlowCountDown == 0f)
		{
			GameAPP.PlaySound(67, 0.5f);
		}
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x00026498 File Offset: 0x00024698
	public void SetGrap(float time)
	{
		if (this.grap == null)
		{
			GameObject original = Resources.Load<GameObject>("Bullet/Other/Grap");
			Vector2 vector = this.shadow.transform.position;
			vector = new Vector2(vector.x, vector.y + 0.25f);
			GameObject gameObject = Object.Instantiate<GameObject>(original, vector, Quaternion.identity);
			gameObject.transform.SetParent(base.transform);
			gameObject.GetComponent<SpriteRenderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = this.baseLayer + 29;
			this.grap = gameObject;
			GameAPP.PlaySound(71, 0.5f);
		}
		this.theSlowCountDown += time;
		int num = this.theMaxHealth + this.theFirstArmorMaxHealth;
		if (this.grapTimes > 5)
		{
			this.TakeDamage(1, (int)(0.01f * (float)num));
		}
		else if (this.grapTimes > 10)
		{
			this.TakeDamage(1, (int)(0.03f * (float)num));
		}
		else if (this.grapTimes > 15)
		{
			this.TakeDamage(1, (int)(0.05f * (float)num));
		}
		if (this.grapTimes >= 30)
		{
			this.TakeDamage(1, num);
		}
		this.grapTimes++;
		this.grapSpeed = 0.5f;
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x000265F0 File Offset: 0x000247F0
	public void Warm(int warmType = 0)
	{
		this.freezeLevel = 0;
		if (this.theFreezeCountDown > 0f || this.iceTrap != null)
		{
			this.Unfrezzing();
		}
		if (warmType == 0 && this.grap == null)
		{
			this.theSlowCountDown = 0f;
			this.theFreezeCountDown = 0f;
			this.RestoreSpeed();
			return;
		}
		this.theFreezeCountDown = 0f;
		this.coldSpeed = 1f;
		if (this.isMindControlled)
		{
			this.SetColor(base.gameObject, 2);
			return;
		}
		this.SetColor(base.gameObject, 0);
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x0002668C File Offset: 0x0002488C
	private void RestoreSpeed()
	{
		this.coldSpeed = 1f;
		this.grapSpeed = 1f;
		if (this.isMindControlled)
		{
			this.SetColor(base.gameObject, 2);
		}
		else
		{
			this.SetColor(base.gameObject, 0);
		}
		if (this.grap != null)
		{
			Object.Destroy(this.grap);
		}
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x000266EC File Offset: 0x000248EC
	protected void MoveUpdate()
	{
		this.theSpeed = this.theOriginSpeed * this.freezeSpeed * this.coldSpeed * this.grapSpeed;
		this.anim.SetFloat("Speed", this.theSpeed);
		if (this.theSlowCountDown > 0f)
		{
			this.theSlowCountDown -= Time.deltaTime;
			if (this.theSlowCountDown < 0f)
			{
				this.theSlowCountDown = 0f;
				this.theSpeed = this.theOriginSpeed;
				this.RestoreSpeed();
			}
		}
		if (this.theFreezeCountDown > 0f)
		{
			this.theFreezeCountDown -= Time.deltaTime;
			if (this.theFreezeCountDown < 0f)
			{
				this.theFreezeCountDown = 0f;
				this.Unfrezzing();
			}
		}
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x000267B8 File Offset: 0x000249B8
	protected void SetMaskLayer()
	{
		GameObject gameObject = base.transform.GetChild(0).gameObject;
		gameObject.GetComponent<SpriteMask>().frontSortingOrder = this.baseLayer + 39;
		gameObject.GetComponent<SpriteMask>().frontSortingLayerID = SortingLayer.NameToID(string.Format("zombie{0}", this.theZombieRow));
		gameObject.GetComponent<SpriteMask>().backSortingOrder = this.baseLayer;
		gameObject.GetComponent<SpriteMask>().backSortingLayerID = SortingLayer.NameToID(string.Format("zombie{0}", this.theZombieRow));
	}

	// Token: 0x040001FE RID: 510
	public Board board;

	// Token: 0x040001FF RID: 511
	public int theZombieType;

	// Token: 0x04000200 RID: 512
	public int theZombieRow;

	// Token: 0x04000201 RID: 513
	public int theStatus;

	// Token: 0x04000202 RID: 514
	public float theSpeed = 1f;

	// Token: 0x04000203 RID: 515
	public float theOriginSpeed = 1f;

	// Token: 0x04000204 RID: 516
	public float freezeSpeed = 1f;

	// Token: 0x04000205 RID: 517
	public float coldSpeed = 1f;

	// Token: 0x04000206 RID: 518
	public float grapSpeed = 1f;

	// Token: 0x04000207 RID: 519
	public int theAttackDamage = 50;

	// Token: 0x04000208 RID: 520
	public float theFreezeCountDown;

	// Token: 0x04000209 RID: 521
	public float theSlowCountDown;

	// Token: 0x0400020A RID: 522
	public float theButterCountDown;

	// Token: 0x0400020B RID: 523
	public bool isMoving = true;

	// Token: 0x0400020C RID: 524
	public bool isAttacking;

	// Token: 0x0400020D RID: 525
	public int baseLayer;

	// Token: 0x0400020E RID: 526
	public bool isMindControlled;

	// Token: 0x0400020F RID: 527
	public bool[] controlledLevel = new bool[7];

	// Token: 0x04000210 RID: 528
	public bool isJalaed;

	// Token: 0x04000211 RID: 529
	public int freezeLevel;

	// Token: 0x04000212 RID: 530
	private int freezeMaxLevel = 100;

	// Token: 0x04000213 RID: 531
	public float theHealth = 270f;

	// Token: 0x04000214 RID: 532
	public int theMaxHealth = 270;

	// Token: 0x04000215 RID: 533
	public GameObject theFirstArmor;

	// Token: 0x04000216 RID: 534
	public int theFirstArmorHealth;

	// Token: 0x04000217 RID: 535
	public int theFirstArmorMaxHealth;

	// Token: 0x04000218 RID: 536
	public int theFirstArmorType;

	// Token: 0x04000219 RID: 537
	public int theFirstArmorBroken;

	// Token: 0x0400021A RID: 538
	public GameObject theSecondArmor;

	// Token: 0x0400021B RID: 539
	public int theSecondArmorHealth;

	// Token: 0x0400021C RID: 540
	public int theSecondArmorMaxHealth;

	// Token: 0x0400021D RID: 541
	public int theSecondArmorType;

	// Token: 0x0400021E RID: 542
	public int theSecondArmorBroken;

	// Token: 0x0400021F RID: 543
	public GameObject theAttackTarget;

	// Token: 0x04000220 RID: 544
	public Animator anim;

	// Token: 0x04000221 RID: 545
	protected bool isLoseHand;

	// Token: 0x04000222 RID: 546
	public GameObject shadow;

	// Token: 0x04000223 RID: 547
	private GameObject iceTrap;

	// Token: 0x04000224 RID: 548
	private GameObject grap;

	// Token: 0x04000225 RID: 549
	protected float flashTime;

	// Token: 0x04000226 RID: 550
	public bool inWater;

	// Token: 0x04000227 RID: 551
	public bool isStopped;

	// Token: 0x04000228 RID: 552
	public bool isChangingRow;

	// Token: 0x04000229 RID: 553
	private Coroutine changeRow;

	// Token: 0x0400022A RID: 554
	private bool isDying;

	// Token: 0x0400022B RID: 555
	private int dieReason = -1;

	// Token: 0x0400022C RID: 556
	public bool isDoom;

	// Token: 0x0400022D RID: 557
	private int grapTimes;

	// Token: 0x0400022E RID: 558
	private bool droppedSun;

	// Token: 0x02000146 RID: 326
	public enum ZombieColor
	{
		// Token: 0x0400046F RID: 1135
		Default,
		// Token: 0x04000470 RID: 1136
		Cold,
		// Token: 0x04000471 RID: 1137
		MindConrolled,
		// Token: 0x04000472 RID: 1138
		Jalaed,
		// Token: 0x04000473 RID: 1139
		Doom
	}
}

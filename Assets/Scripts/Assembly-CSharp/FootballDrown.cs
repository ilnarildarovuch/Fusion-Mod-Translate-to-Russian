using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x020000E8 RID: 232
public class FootballDrown : TallNutFootballZ
{
	// Token: 0x0600043A RID: 1082 RVA: 0x000215E4 File Offset: 0x0001F7E4
	protected override void Start()
	{
		base.Start();
		this.throwTime = (float)Random.Range(3, 6);
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x000215FC File Offset: 0x0001F7FC
	protected override void Update()
	{
		base.Update();
		if (this.throwTime > 0f && !this.isThrow && !this.isMindControlled && this.theStatus != 1)
		{
			this.throwTime -= Time.deltaTime;
			if (this.throwTime <= 0f)
			{
				this.StartThrow();
			}
		}
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x0002165A File Offset: 0x0001F85A
	private void StartThrow()
	{
		this.isThrow = true;
		this.anim.SetTrigger("throw");
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x00021674 File Offset: 0x0001F874
	private void AnimThrow()
	{
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		Vector2 vector = this.shadow.transform.position;
		vector = new Vector2(vector.x - 2f, vector.y + 3.1f);
		DrownWeapon drownWeapon = Object.Instantiate<GameObject>(Resources.Load<GameObject>("Zombies/Zombie_drown/weapon"), vector, Quaternion.Euler(0f, 0f, -17f), this.board.transform).AddComponent<DrownWeapon>();
		drownWeapon.theRow = this.theZombieRow;
		List<Plant> list = new List<Plant>();
		foreach (GameObject gameObject in this.board.plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				if (component.thePlantRow == this.theZombieRow && component.shadow.transform.position.x + 6f < this.shadow.transform.position.x)
				{
					list.Add(component);
				}
			}
		}
		if (list.Count != 0)
		{
			list.Sort((Plant a, Plant b) => b.thePlantColumn.CompareTo(a.thePlantColumn));
			drownWeapon.target = list[0].gameObject;
		}
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x000217D4 File Offset: 0x0001F9D4
	protected override void OnTriggerStay2D(Collider2D collision)
	{
		if (this.isThrow)
		{
			base.OnTriggerStay2D(collision);
			return;
		}
		if (collision.gameObject.CompareTag("Plant") && !this.isMindControlled)
		{
			Plant component = collision.GetComponent<Plant>();
			if (component.thePlantRow == this.theZombieRow)
			{
				if (TypeMgr.IsCaltrop(component.thePlantType))
				{
					return;
				}
				this.StartThrow();
				return;
			}
		}
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x00021838 File Offset: 0x0001FA38
	protected override void FirstArmorBroken()
	{
		if (this.theFirstArmorHealth < this.theFirstArmorMaxHealth * 2 / 3 && this.theFirstArmorBroken < 1)
		{
			this.theFirstArmorBroken = 1;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[40];
		}
		if (this.theFirstArmorHealth < this.theFirstArmorMaxHealth / 3 && this.theFirstArmorBroken < 2)
		{
			this.theFirstArmorBroken = 2;
			this.theFirstArmor.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[41];
		}
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x000218B8 File Offset: 0x0001FAB8
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

	// Token: 0x040001EB RID: 491
	protected float throwTime;

	// Token: 0x040001EC RID: 492
	private bool isThrow;
}

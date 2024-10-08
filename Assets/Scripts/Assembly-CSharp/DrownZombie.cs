using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x020000E7 RID: 231
public class DrownZombie : Zombie
{
	// Token: 0x06000433 RID: 1075 RVA: 0x000210B3 File Offset: 0x0001F2B3
	protected override void Start()
	{
		base.Start();
		this.throwTime = (float)Random.Range(5, 10);
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x000210CC File Offset: 0x0001F2CC
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

	// Token: 0x06000435 RID: 1077 RVA: 0x0002112A File Offset: 0x0001F32A
	private void StartThrow()
	{
		this.isThrow = true;
		this.anim.SetTrigger("throw");
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x00021144 File Offset: 0x0001F344
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

	// Token: 0x06000437 RID: 1079 RVA: 0x000212A4 File Offset: 0x0001F4A4
	protected override void OnTriggerStay2D(Collider2D collision)
	{
		if (this.isThrow)
		{
			base.OnTriggerStay2D(collision);
			return;
		}
		if (collision.gameObject.CompareTag("Plant") && !this.isMindControlled)
		{
			Plant component = collision.gameObject.GetComponent<Plant>();
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

	// Token: 0x06000438 RID: 1080 RVA: 0x0002130C File Offset: 0x0001F50C
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
					child.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Zombies/Zombie_drown/losearm");
					child.transform.GetChild(0).transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
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

	// Token: 0x040001E9 RID: 489
	protected float throwTime;

	// Token: 0x040001EA RID: 490
	private bool isThrow;
}

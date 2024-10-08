using System;
using UnityEngine;

// Token: 0x020000D7 RID: 215
public class JacksonDriver : SuperDriverZombie
{
	// Token: 0x060003EF RID: 1007 RVA: 0x0001E4A8 File Offset: 0x0001C6A8
	public override void Die(int reason = 0)
	{
		if (reason != 1 && !this.isMindControlled)
		{
			CreateZombie.Instance.SetZombie(0, this.theZombieRow, 16, this.shadow.transform.position.x - 1f, false);
			CreateZombie.Instance.SetZombie(0, this.theZombieRow, 18, this.shadow.transform.position.x + 1f, false);
			CreateZombie.Instance.SetZombie(0, this.theZombieRow, 10, this.shadow.transform.position.x, false);
		}
		base.Die(reason);
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x0001E558 File Offset: 0x0001C758
	public override void TakeDamage(int theDamageType, int theDamage)
	{
		if (theDamage > 0)
		{
			theDamage /= 2;
		}
		base.TakeDamage(theDamageType, theDamage);
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x0001E56C File Offset: 0x0001C76C
	protected override void DriverPositionUpdate()
	{
		float x = base.transform.GetChild(4).position.x;
		if (Board.Instance.iceRoadX[this.theZombieRow] > x)
		{
			base.transform.Translate(-0.4f * Time.deltaTime, 0f, 0f);
			return;
		}
		base.transform.Translate(-this.currentSpeed * Time.deltaTime, 0f, 0f);
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x0001E5E8 File Offset: 0x0001C7E8
	protected override void BodyTakeDamage(int theDamage)
	{
		this.theHealth -= (float)theDamage;
		if (this.theHealth >= (float)this.theMaxHealth / 3f && this.theHealth < (float)this.theMaxHealth * 2f / 3f)
		{
			base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Zombies/InTravel/JacksonDriver/damage1");
			base.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Zombies/InTravel/JacksonDriver/Zombie_zamboni_2_damage1");
		}
		if (this.theHealth < (float)this.theMaxHealth / 3f)
		{
			this.anim.SetTrigger("shake");
			base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Zombies/InTravel/JacksonDriver/damage2");
			base.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Zombies/InTravel/JacksonDriver/Zombie_zamboni_2_damage2");
			base.transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[37];
			base.transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[37];
			base.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
			GameObject gameObject = base.transform.GetChild(1).GetChild(0).gameObject;
			gameObject.SetActive(true);
			foreach (object obj in gameObject.transform)
			{
				Transform transform = (Transform)obj;
				transform.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
				transform.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = this.baseLayer + 29;
			}
		}
		if (this.theHealth <= 0f)
		{
			base.DieAndExplode();
		}
	}
}

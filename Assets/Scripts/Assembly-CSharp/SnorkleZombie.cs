using System;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x020000EE RID: 238
public class SnorkleZombie : Zombie
{
	// Token: 0x06000465 RID: 1125 RVA: 0x0002324A File Offset: 0x0002144A
	protected override void Awake()
	{
		base.Awake();
		this.theStatus = 7;
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x00023259 File Offset: 0x00021459
	protected override void Start()
	{
		base.Start();
		if (GameAPP.theGameStatus == 0)
		{
			this.anim.Play("swim");
			this.inWater = true;
			base.SetMaskLayer();
		}
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x00023288 File Offset: 0x00021488
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
					child.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Zombies/Zombie_snorkle/Zombie_snorkle_outerarm_upper2");
				}
			}
		}
		if (this.theHealth < (float)(this.theMaxHealth / 3))
		{
			if (this.theStatus != 1)
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
			if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("swim"))
			{
				this.Die(2);
			}
		}
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x000234A0 File Offset: 0x000216A0
	private void OutOfWater()
	{
		this.theStatus = 0;
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x000234A9 File Offset: 0x000216A9
	private void GoInToWater()
	{
		this.theStatus = 7;
	}
}

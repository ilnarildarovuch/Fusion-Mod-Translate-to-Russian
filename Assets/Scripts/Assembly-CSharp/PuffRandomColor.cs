using System;
using UnityEngine;

// Token: 0x02000045 RID: 69
public class PuffRandomColor : Bullet
{
	// Token: 0x0600014B RID: 331 RVA: 0x0000AB2C File Offset: 0x00008D2C
	protected override void Start()
	{
		this.puffColor = Random.Range(0, 7);
		this.sprite = Resources.Load<Sprite>(string.Format("Bullet/Colorfulpuffs/ColorfulPuff{0}", this.puffColor + 1));
		base.GetComponent<SpriteRenderer>().sprite = this.sprite;
		base.transform.GetChild(0).GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, this.sprite);
	}

	// Token: 0x0600014C RID: 332 RVA: 0x0000ABA0 File Offset: 0x00008DA0
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.controlledLevel[this.puffColor] = true;
		int num = 0;
		bool[] controlledLevel = component.controlledLevel;
		for (int i = 0; i < controlledLevel.Length; i++)
		{
			if (controlledLevel[i])
			{
				num++;
			}
		}
		if (num > 6)
		{
			component.SetMindControl(true);
		}
		else
		{
			component.TakeDamage(0, this.theBulletDamage);
			this.PlaySound(component);
		}
		GameObject gameObject = GameAPP.particlePrefab[17];
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, base.transform.position, Quaternion.identity);
		gameObject2.GetComponent<ParticleSystem>().textureSheetAnimation.SetSprite(0, this.sprite);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		this.Die();
	}

	// Token: 0x0600014D RID: 333 RVA: 0x0000AC6C File Offset: 0x00008E6C
	private void AttackZombie()
	{
		foreach (Collider2D collider2D in Physics2D.OverlapBoxAll(base.transform.position, new Vector2(3f, 3f), 0f))
		{
			Zombie zombie;
			PolevaulterZombie polevaulterZombie;
			if (collider2D != null && collider2D.TryGetComponent<Zombie>(out zombie) && Mathf.Abs(zombie.theZombieRow - this.theBulletRow) <= 1 && (!zombie.gameObject.TryGetComponent<PolevaulterZombie>(out polevaulterZombie) || polevaulterZombie.polevaulterStatus != 1) && !zombie.isMindControlled)
			{
				this.TrySetMindControl(zombie);
			}
		}
	}

	// Token: 0x0600014E RID: 334 RVA: 0x0000AD08 File Offset: 0x00008F08
	private void TrySetMindControl(Zombie zombie)
	{
		float num = (float)(zombie.theFirstArmorMaxHealth + zombie.theMaxHealth);
		float num2 = ((float)zombie.theFirstArmorHealth + zombie.theHealth) / num;
		if ((double)num2 > 0.5)
		{
			num2 = 1f;
		}
		else
		{
			num2 /= 0.5f;
		}
		int num3 = 0;
		bool[] controlledLevel = zombie.controlledLevel;
		for (int i = 0; i < controlledLevel.Length; i++)
		{
			if (controlledLevel[i])
			{
				num3++;
			}
		}
		num2 -= (float)num3 * 0.05f;
		num2 = Mathf.Sqrt(num2);
		float num4 = 0.75f;
		num4 -= (float)num3 * 0.05f;
		if (num2 < num4)
		{
			num2 = num4;
		}
		if (Random.value >= num2)
		{
			zombie.SetMindControl(false);
			return;
		}
		zombie.TakeDamage(1, 20);
	}
}

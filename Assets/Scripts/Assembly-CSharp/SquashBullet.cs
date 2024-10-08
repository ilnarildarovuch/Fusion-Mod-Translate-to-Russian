using System;
using UnityEngine;

// Token: 0x02000048 RID: 72
public class SquashBullet : Bullet
{
	// Token: 0x06000154 RID: 340 RVA: 0x0000AE80 File Offset: 0x00009080
	protected override void Awake()
	{
		base.Awake();
		this.g = 10f;
		this.Vx = Random.Range(1.8f, 2.2f);
		this.Vy = Random.Range(4.8f, 5.2f);
		this.Y = this.Vy;
	}

	// Token: 0x06000155 RID: 341 RVA: 0x0000AED4 File Offset: 0x000090D4
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, this.theBulletDamage);
		this.theMovingWay = -1;
		this.Vy *= -0.75f;
		base.GetComponent<BoxCollider2D>().enabled = false;
		this.originY = this.shadow.transform.position.y;
		this.PlaySound(component);
		if (Board.Instance.isEveStarted)
		{
			base.SetShadowPosition();
		}
		this.landY = this.shadow.transform.position.y + 0.3f;
	}

	// Token: 0x06000156 RID: 342 RVA: 0x0000AF6F File Offset: 0x0000916F
	protected override void Update()
	{
		base.Update();
		if (this.theMovingWay == -1)
		{
			this.PositionUpdate();
		}
	}

	// Token: 0x06000157 RID: 343 RVA: 0x0000AF88 File Offset: 0x00009188
	private void PositionUpdate()
	{
		base.transform.Translate(new Vector3(this.Vx * Time.deltaTime, 0f));
		base.transform.GetChild(0).transform.Translate(new Vector3(0f, this.Vy * Time.deltaTime));
		this.Vy -= this.g * Time.deltaTime;
		if (base.transform.GetChild(0).position.y < this.landY && this.Vy < 0f)
		{
			this.Vy = -this.Vy;
			this.AttackZombie();
			if (Board.Instance.roadType[this.theBulletRow] == 1)
			{
				Object.Instantiate<GameObject>(GameAPP.particlePrefab[32], this.shadow.transform.position, Quaternion.identity, Board.Instance.transform);
			}
		}
	}

	// Token: 0x06000158 RID: 344 RVA: 0x0000B07C File Offset: 0x0000927C
	protected virtual void AttackZombie()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 0.5f);
		bool flag = false;
		Collider2D[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			Zombie zombie;
			if (array2[i].TryGetComponent<Zombie>(out zombie) && zombie.theZombieRow == this.theBulletRow && !zombie.isMindControlled)
			{
				zombie.TakeDamage(1, this.theBulletDamage);
				flag = true;
			}
		}
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
		}
	}

	// Token: 0x04000101 RID: 257
	protected float landY;
}

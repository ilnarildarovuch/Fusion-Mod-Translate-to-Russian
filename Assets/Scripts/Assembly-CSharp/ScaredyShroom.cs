using System;
using UnityEngine;

// Token: 0x020000BD RID: 189
public class ScaredyShroom : Shooter
{
	// Token: 0x06000388 RID: 904 RVA: 0x0001B8A4 File Offset: 0x00019AA4
	protected override void FixedUpdate()
	{
		this.pos = new Vector2(this.shadow.transform.position.x, this.shadow.transform.position.y + 0.5f);
		base.FixedUpdate();
		this.colliders = Physics2D.OverlapBoxAll(this.pos, this.range, 0f);
		bool flag = false;
		foreach (Collider2D collider2D in this.colliders)
		{
			Zombie zombie;
			if (collider2D != null && collider2D.TryGetComponent<Zombie>(out zombie) && !zombie.isMindControlled)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			this.anim.SetBool("NearZombie", true);
			this.ScaredEvent();
			return;
		}
		this.anim.SetBool("NearZombie", false);
	}

	// Token: 0x06000389 RID: 905 RVA: 0x0001B975 File Offset: 0x00019B75
	protected virtual void ScaredEvent()
	{
	}

	// Token: 0x0600038A RID: 906 RVA: 0x0001B978 File Offset: 0x00019B78
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 9, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(57, 0.5f);
		return gameObject;
	}

	// Token: 0x040001C9 RID: 457
	private Vector2 pos = new Vector2(0f, 0f);

	// Token: 0x040001CA RID: 458
	private Vector2 range = new Vector2(2.5f, 2.5f);

	// Token: 0x040001CB RID: 459
	private Collider2D[] colliders;
}

using System;
using UnityEngine;

// Token: 0x0200003F RID: 63
public class NutBullet : Bullet
{
	// Token: 0x0600013A RID: 314 RVA: 0x0000A5CC File Offset: 0x000087CC
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, this.theBulletDamage);
		GameObject gameObject = GameAPP.particlePrefab[7];
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, base.transform.position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		this.PlaySound(component);
	}

	// Token: 0x0600013B RID: 315 RVA: 0x0000A634 File Offset: 0x00008834
	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Zombie"))
		{
			PolevaulterZombie polevaulterZombie;
			if (collision.TryGetComponent<PolevaulterZombie>(out polevaulterZombie) && polevaulterZombie.polevaulterStatus == 1)
			{
				return;
			}
			Zombie component = collision.GetComponent<Zombie>();
			if (component.theZombieRow != this.theBulletRow || component.isMindControlled)
			{
				return;
			}
			foreach (GameObject x in this.Z)
			{
				if (x != null && x == this.zombie)
				{
					return;
				}
			}
			this.Z.Add(collision.gameObject);
			this.HitZombie(collision.gameObject);
		}
	}
}

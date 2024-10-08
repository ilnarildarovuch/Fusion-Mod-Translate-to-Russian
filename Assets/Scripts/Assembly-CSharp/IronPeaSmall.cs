using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200003C RID: 60
public class IronPeaSmall : Bullet
{
	// Token: 0x06000133 RID: 307 RVA: 0x0000A448 File Offset: 0x00008648
	protected override void HitZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		using (List<GameObject>.Enumerator enumerator = this.Z.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == zombie)
				{
					return;
				}
			}
		}
		component.TakeDamage(1, this.theBulletDamage);
		this.Z.Add(zombie);
		GameObject gameObject = GameAPP.particlePrefab[18];
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, base.transform.position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		this.PlaySound(component);
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0000A504 File Offset: 0x00008704
	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Zombie"))
		{
			PolevaulterZombie polevaulterZombie;
			if (collision.TryGetComponent<PolevaulterZombie>(out polevaulterZombie) && polevaulterZombie.polevaulterStatus == 1)
			{
				return;
			}
			this.HitZombie(collision.gameObject);
		}
	}
}

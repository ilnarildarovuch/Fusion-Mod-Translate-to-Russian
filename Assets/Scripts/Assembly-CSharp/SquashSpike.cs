using System;
using UnityEngine;

// Token: 0x02000094 RID: 148
public class SquashSpike : Caltrop
{
	// Token: 0x06000301 RID: 769 RVA: 0x000182F0 File Offset: 0x000164F0
	protected override void AnimAttack()
	{
		this.KillCar();
		Collider2D[] array = Physics2D.OverlapBoxAll(this.shadow.transform.position, new Vector2(1f, 1f), 0f);
		bool flag = false;
		Collider2D[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			Zombie zombie;
			if (array2[i].TryGetComponent<Zombie>(out zombie) && zombie.theZombieRow == this.thePlantRow && base.SearchUniqueZombie(zombie))
			{
				flag = true;
				zombie.TakeDamage(4, 20);
				zombie.transform.position = new Vector3(zombie.transform.position.x + 0.2f, zombie.transform.position.y);
			}
		}
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
			GameAPP.PlaySound(Random.Range(72, 74), 0.5f);
			CreateBullet.Instance.SetBullet(this.shadow.transform.position.x, this.shadow.transform.position.y + 0.5f, this.thePlantRow, 28, 0).GetComponent<Bullet>().theBulletDamage = 40;
		}
	}
}

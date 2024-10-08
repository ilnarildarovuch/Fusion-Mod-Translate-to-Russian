using System;
using UnityEngine;

// Token: 0x02000083 RID: 131
public class EndoFlame : Plant
{
	// Token: 0x060002B5 RID: 693 RVA: 0x000161B0 File Offset: 0x000143B0
	public override void Die()
	{
		Vector2 vector = this.shadow.transform.position;
		vector = new Vector2(vector.x, vector.y + 0.5f);
		Object.Instantiate<GameObject>(Resources.Load<GameObject>("Items/Fertilize/Ferilize"), vector, Quaternion.identity, GameAPP.board.transform);
		base.Die();
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x00016218 File Offset: 0x00014418
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Bullet bullet;
		if (collision.TryGetComponent<Bullet>(out bullet))
		{
			if (this.thePlantHealth > 0)
			{
				GameAPP.PlaySound(Random.Range(59, 61), 0.5f);
				GameAPP.board.GetComponent<CreateCoin>().SetCoin(0, 0, 0, 0, base.transform.position);
			}
			bullet.hasHitTarget = true;
			this.thePlantHealth -= 50;
			bullet.Die();
		}
	}
}

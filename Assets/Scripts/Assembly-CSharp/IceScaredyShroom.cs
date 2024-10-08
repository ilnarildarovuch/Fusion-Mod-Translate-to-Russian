using System;
using UnityEngine;

// Token: 0x020000B4 RID: 180
public class IceScaredyShroom : ScaredyShroom
{
	// Token: 0x0600036B RID: 875 RVA: 0x0001AB5C File Offset: 0x00018D5C
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 21, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(68, 0.5f);
		return gameObject;
	}

	// Token: 0x0600036C RID: 876 RVA: 0x0001ABCC File Offset: 0x00018DCC
	private void AnimFreeze()
	{
		bool flag = false;
		Vector2 vector = this.shadow.transform.position;
		vector = new Vector2(vector.x, vector.y + 1f);
		foreach (Collider2D collider2D in Physics2D.OverlapBoxAll(vector, new Vector2(3f, 3f), 0f))
		{
			Zombie zombie;
			if (collider2D != null && collider2D.TryGetComponent<Zombie>(out zombie) && !zombie.isMindControlled && zombie.theStatus != 1 && Mathf.Abs(zombie.theZombieRow - this.thePlantRow) <= 1)
			{
				zombie.SetFreeze(4f);
				zombie.TakeDamage(1, 20);
				flag = true;
			}
		}
		if (flag)
		{
			GameAPP.PlaySound(67, 0.5f);
		}
		GameObject gameObject = Object.Instantiate<GameObject>(GameAPP.particlePrefab[24], this.shadow.transform.position, Quaternion.identity, this.board.transform);
		Vector2 vector2 = gameObject.transform.localScale;
		gameObject.transform.localScale = new Vector3(vector2.x * 1.5f, vector2.y * 1.5f);
	}
}

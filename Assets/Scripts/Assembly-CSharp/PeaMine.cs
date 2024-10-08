using System;
using UnityEngine;

// Token: 0x020000B7 RID: 183
public class PeaMine : PotatoMine
{
	// Token: 0x06000374 RID: 884 RVA: 0x0001AF28 File Offset: 0x00019128
	protected override void Update()
	{
		if (this.attributeCountdown > 0f)
		{
			this.attributeCountdown -= Time.deltaTime;
		}
		if (this.attributeCountdown <= 0f)
		{
			this.attributeCountdown = 0f;
			this.anim.SetTrigger("rise");
		}
		base.Update();
		this.SetInterval();
		this.PlantShootUpdate();
	}

	// Token: 0x06000375 RID: 885 RVA: 0x0001AF90 File Offset: 0x00019190
	public GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("PotatoMine_light1").GetChild(0).transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 7, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		return gameObject;
	}

	// Token: 0x06000376 RID: 886 RVA: 0x0001B00C File Offset: 0x0001920C
	private void SetInterval()
	{
		GameObject nearestZombie = this.GetNearestZombie();
		if (nearestZombie == null)
		{
			this.thePlantAttackInterval = 1.5f;
			return;
		}
		float num = nearestZombie.transform.position.x - base.transform.position.x;
		if (num > 6f)
		{
			this.thePlantAttackInterval = 1.5f;
			return;
		}
		if (num < 1f)
		{
			this.thePlantAttackInterval = 0.5f;
			return;
		}
		this.thePlantAttackInterval = Mathf.Lerp(1.5f, 0.5f, (6f - num) / 5f);
	}

	// Token: 0x06000377 RID: 887 RVA: 0x0001B0A4 File Offset: 0x000192A4
	public override void Die()
	{
		for (int i = 0; i < 36; i++)
		{
			GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(base.transform.position.x, base.transform.position.y, this.thePlantRow, 7, 2);
			Vector3 position = gameObject.transform.GetChild(0).transform.position;
			gameObject.transform.rotation = Quaternion.AngleAxis((float)(10 * i) - 90f, Vector3.forward);
			gameObject.transform.GetChild(0).SetPositionAndRotation(position, Quaternion.Euler(new Vector3(0f, 0f, -gameObject.transform.rotation.z)));
		}
		base.Die();
	}
}

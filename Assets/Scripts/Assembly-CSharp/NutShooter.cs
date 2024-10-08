using System;
using UnityEngine;

// Token: 0x020000B6 RID: 182
public class NutShooter : Shooter
{
	// Token: 0x06000370 RID: 880 RVA: 0x0001AD8D File Offset: 0x00018F8D
	protected override void Update()
	{
		base.Update();
		this.ReplaceSprite();
	}

	// Token: 0x06000371 RID: 881 RVA: 0x0001AD9C File Offset: 0x00018F9C
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.2f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 2, 1);
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		return gameObject;
	}

	// Token: 0x06000372 RID: 882 RVA: 0x0001AE10 File Offset: 0x00019010
	private void ReplaceSprite()
	{
		if (this.thePlantHealth < this.thePlantMaxHealth * 2 / 3 && this.thePlantHealth > this.thePlantMaxHealth / 3)
		{
			base.transform.GetChild(1).gameObject.SetActive(false);
			base.transform.GetChild(2).gameObject.SetActive(true);
			base.transform.GetChild(3).gameObject.SetActive(false);
			return;
		}
		if (this.thePlantHealth < this.thePlantMaxHealth / 3)
		{
			base.transform.GetChild(1).gameObject.SetActive(false);
			base.transform.GetChild(2).gameObject.SetActive(false);
			base.transform.GetChild(3).gameObject.SetActive(true);
			return;
		}
		base.transform.GetChild(1).gameObject.SetActive(true);
		base.transform.GetChild(2).gameObject.SetActive(false);
		base.transform.GetChild(3).gameObject.SetActive(false);
	}
}

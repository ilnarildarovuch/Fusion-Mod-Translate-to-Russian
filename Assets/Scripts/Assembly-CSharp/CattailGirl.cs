using System;
using UnityEngine;

// Token: 0x020000A3 RID: 163
public class CattailGirl : Shooter
{
	// Token: 0x06000331 RID: 817 RVA: 0x00019358 File Offset: 0x00017558
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot1").transform.position;
		float x = position.x;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(x, y, thePlantRow, 20, 6);
		gameObject.GetComponent<Bullet>().theBulletDamage = 40;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		return gameObject;
	}

	// Token: 0x06000332 RID: 818 RVA: 0x000193C8 File Offset: 0x000175C8
	private GameObject AnimShoot2()
	{
		Vector3 position = base.transform.Find("Shoot2").transform.position;
		float x = position.x;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(x, y, thePlantRow, 20, 6);
		gameObject.GetComponent<Bullet>().theBulletDamage = 40;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		return gameObject;
	}

	// Token: 0x06000333 RID: 819 RVA: 0x00019437 File Offset: 0x00017637
	protected override void Update()
	{
		base.Update();
		this.PostionUpdate();
	}

	// Token: 0x06000334 RID: 820 RVA: 0x00019448 File Offset: 0x00017648
	private void PostionUpdate()
	{
		this.existTime += Time.deltaTime;
		float d = Mathf.Sin(this.existTime * this.frequency) * this.floatStrength;
		base.transform.position = this.startPos + Vector3.up * d;
	}

	// Token: 0x06000335 RID: 821 RVA: 0x000194A4 File Offset: 0x000176A4
	protected override GameObject SearchZombie()
	{
		foreach (GameObject gameObject in GameAPP.board.GetComponent<Board>().zombieArray)
		{
			if (gameObject != null)
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (component.shadow.transform.position.x < 9.2f && base.SearchUniqueZombie(component))
				{
					return gameObject;
				}
			}
		}
		return null;
	}

	// Token: 0x040001BF RID: 447
	private float existTime;

	// Token: 0x040001C0 RID: 448
	private readonly float floatStrength = 0.05f;

	// Token: 0x040001C1 RID: 449
	private readonly float frequency = 1.2f;
}

using System;
using UnityEngine;

// Token: 0x02000079 RID: 121
public class SunBomb : Plant
{
	// Token: 0x0600027D RID: 637 RVA: 0x00014C44 File Offset: 0x00012E44
	protected override void Start()
	{
		base.Start();
		this.anim.Play("Bomb");
	}

	// Token: 0x0600027E RID: 638 RVA: 0x00014C5C File Offset: 0x00012E5C
	public void Bomb()
	{
		GameObject gameObject = GameAPP.particlePrefab[3];
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y + 0.5f, 0f);
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		gameObject2.GetComponent<BombCherry>().bombRow = this.thePlantRow;
		gameObject2.GetComponent<BombCherry>().bombType = 1;
		ScreenShake.TriggerShake(0.15f);
		GameAPP.PlaySound(40, 0.5f);
		this.Die();
	}

	// Token: 0x0600027F RID: 639 RVA: 0x00014D08 File Offset: 0x00012F08
	public void PlaySoundStart()
	{
		GameAPP.PlaySound(39, 0.5f);
	}
}

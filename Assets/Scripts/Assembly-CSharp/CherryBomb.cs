using System;
using UnityEngine;

// Token: 0x02000065 RID: 101
public class CherryBomb : Plant
{
	// Token: 0x06000208 RID: 520 RVA: 0x00010CD7 File Offset: 0x0000EED7
	protected override void Start()
	{
		base.Start();
		this.anim.Play("Bomb");
	}

	// Token: 0x06000209 RID: 521 RVA: 0x00010CF0 File Offset: 0x0000EEF0
	public void Bomb()
	{
		GameObject gameObject = GameAPP.particlePrefab[2];
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y + 0.5f, 0f);
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		gameObject2.GetComponent<BombCherry>().bombRow = this.thePlantRow;
		ScreenShake.TriggerShake(0.15f);
		GameAPP.PlaySound(40, 0.5f);
		this.Die();
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00010D90 File Offset: 0x0000EF90
	public void PlaySoundStart()
	{
		GameAPP.PlaySound(39, 0.5f);
	}
}

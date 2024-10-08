using System;
using UnityEngine;

// Token: 0x02000104 RID: 260
public class SoundCtrl : MonoBehaviour
{
	// Token: 0x06000519 RID: 1305 RVA: 0x0002BA0E File Offset: 0x00029C0E
	private void Update()
	{
		this.existTime += Time.deltaTime;
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x0002BA22 File Offset: 0x00029C22
	private void Awake()
	{
		base.Invoke("Die", base.GetComponent<AudioSource>().clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x0002BA58 File Offset: 0x00029C58
	private void Die()
	{
		GameAPP.sound.Remove(this);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000293 RID: 659
	public int theSoundID;

	// Token: 0x04000294 RID: 660
	public float existTime;
}

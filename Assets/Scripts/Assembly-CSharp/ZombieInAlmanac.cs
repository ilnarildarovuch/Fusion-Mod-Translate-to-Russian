using System;
using UnityEngine;

// Token: 0x0200002D RID: 45
public class ZombieInAlmanac : MonoBehaviour
{
	// Token: 0x060000CE RID: 206 RVA: 0x00005E94 File Offset: 0x00004094
	private void Start()
	{
		base.GetComponent<Animator>().Play("idle");
		base.GetComponent<Animator>().SetFloat("Speed", 1.3f);
	}
}

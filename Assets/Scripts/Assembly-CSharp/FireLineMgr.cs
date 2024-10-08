using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class FireLineMgr : MonoBehaviour
{
	// Token: 0x06000007 RID: 7 RVA: 0x00002088 File Offset: 0x00000288
	private void Start()
	{
		for (int i = 0; i < this.fireArray.Length; i++)
		{
			this.fireArray[i] = base.transform.GetChild(i).gameObject;
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000020C4 File Offset: 0x000002C4
	private void Update()
	{
		this.fadeTime += this.speed * Time.deltaTime;
		if (this.fadeTime > this.speed * 0.4f)
		{
			int num = (int)(this.fadeTime - this.speed * 0.4f);
			if (num < this.fireArray.Length)
			{
				this.fireArray[num].GetComponent<Animator>().SetTrigger("fade");
			}
		}
		if (this.fadeTime > this.speed * 2f && this.isMgr)
		{
			this.Die();
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002157 File Offset: 0x00000357
	private void Die()
	{
		Board.Instance.fireLineArray[this.theFireRow] = null;
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000003 RID: 3
	private float fadeTime;

	// Token: 0x04000004 RID: 4
	public bool isMgr;

	// Token: 0x04000005 RID: 5
	private readonly GameObject[] fireArray = new GameObject[15];

	// Token: 0x04000006 RID: 6
	private float speed = 15f;

	// Token: 0x04000007 RID: 7
	public int theFireRow = -1;
}

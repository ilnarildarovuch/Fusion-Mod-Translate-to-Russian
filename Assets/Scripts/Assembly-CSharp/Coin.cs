using System;
using UnityEngine;

// Token: 0x02000050 RID: 80
public class Coin : MonoBehaviour
{
	// Token: 0x0600016E RID: 366 RVA: 0x0000B6D0 File Offset: 0x000098D0
	private void Start()
	{
		this.target = GameObject.Find("SunPosition").transform;
		this.startPosition = base.transform.position;
		this.velocity = new Vector2(Random.Range(-1.5f, 1.5f), this.verticalSpeed);
		if (this.theCoinType == 2)
		{
			this.sunPrice = 15;
		}
	}

	// Token: 0x0600016F RID: 367 RVA: 0x0000B73C File Offset: 0x0000993C
	private void Update()
	{
		if ((GameAPP.autoCollect && this.isLand) || (this.theMoveType == 1 && Time.timeScale != 0f))
		{
			this.MoveToPosition();
			return;
		}
		if (!this.isLand)
		{
			this.velocity.y = this.velocity.y - this.gravity * Time.deltaTime;
			base.transform.Translate(this.velocity * Time.deltaTime);
			if (base.transform.position.y < this.startPosition.y - 0.5f)
			{
				this.isLand = true;
			}
		}
	}

	// Token: 0x06000170 RID: 368 RVA: 0x0000B7E4 File Offset: 0x000099E4
	private void MoveToPosition()
	{
		if (this.target == null)
		{
			this.Die();
			return;
		}
		if ((this.target.position - base.transform.position).magnitude > 2f)
		{
			base.transform.position = Vector3.MoveTowards(base.transform.position, this.target.position, this.moveSpeed * Time.deltaTime);
			return;
		}
		this.moveSpeed -= 20f * Time.deltaTime;
		base.transform.position = Vector3.MoveTowards(base.transform.position, this.target.position, this.moveSpeed * Time.deltaTime);
		base.transform.localScale -= new Vector3(5f * Time.deltaTime, 5f * Time.deltaTime, 5f * Time.deltaTime);
		if (base.transform.localScale.x < 0.3f)
		{
			GameAPP.board.GetComponent<Board>().theSun += this.sunPrice;
			this.Die();
		}
	}

	// Token: 0x06000171 RID: 369 RVA: 0x0000B924 File Offset: 0x00009B24
	public void Die()
	{
		GameObject[] coinArray = GameAPP.board.GetComponent<Board>().coinArray;
		for (int i = 0; i < coinArray.Length; i++)
		{
			if (coinArray[i] == base.gameObject)
			{
				coinArray[i] = null;
				break;
			}
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000106 RID: 262
	public int theCoinType;

	// Token: 0x04000107 RID: 263
	public float dieCountDown = 7.5f;

	// Token: 0x04000108 RID: 264
	public int theMoveType;

	// Token: 0x04000109 RID: 265
	public int sunPrice = 25;

	// Token: 0x0400010A RID: 266
	private Transform target;

	// Token: 0x0400010B RID: 267
	private float moveSpeed = 300f;

	// Token: 0x0400010C RID: 268
	private bool isLand;

	// Token: 0x0400010D RID: 269
	public float horizontalSpeed = 1.5f;

	// Token: 0x0400010E RID: 270
	public float verticalSpeed = 4f;

	// Token: 0x0400010F RID: 271
	public float gravity = 12f;

	// Token: 0x04000110 RID: 272
	private Vector2 velocity;

	// Token: 0x04000111 RID: 273
	private Vector2 startPosition;
}

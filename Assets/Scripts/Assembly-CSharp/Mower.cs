using System;
using UnityEngine;

// Token: 0x02000062 RID: 98
public class Mower : MonoBehaviour
{
	// Token: 0x060001E6 RID: 486 RVA: 0x0000FBF5 File Offset: 0x0000DDF5
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody2D>();
		this.anim = base.GetComponent<Animator>();
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x0000FC10 File Offset: 0x0000DE10
	private void Update()
	{
		Vector3 position = base.transform.position;
		position = new Vector3(position.x - 1f, position.y);
		Vector2 vector = Camera.main.WorldToScreenPoint(position);
		this.theBoxX = Mouse.Instance.GetColumnFromX(vector.x - 1f);
		if (Camera.main.WorldToViewportPoint(base.transform.position).x > 1f)
		{
			this.Die();
		}
		int num = this.theMowerType;
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x0000FCA0 File Offset: 0x0000DEA0
	private void PoolCleanerUpdate()
	{
		if (GameAPP.theGameStatus == 0 && this.isStart && base.transform.position.x > -5.1f)
		{
			if (!this.inWater && Board.Instance.boxType[this.theBoxX, this.theMowerRow] == 1)
			{
				this.inWater = true;
				this.anim.SetTrigger("EnterWater");
				GameObject original = Resources.Load<GameObject>("Particle/Anim/Water/WaterSplashPrefab");
				Vector2 vector = base.transform.position;
				Object.Instantiate<GameObject>(original, vector, Quaternion.identity, GameAPP.board.transform);
				vector = new Vector2(vector.x, vector.y + 0.5f);
				Object.Instantiate<GameObject>(GameAPP.particlePrefab[32], vector, Quaternion.identity, GameAPP.board.transform);
				GameAPP.PlaySound(71, 0.5f);
			}
			if (this.inWater && Board.Instance.boxType[this.theBoxX, this.theMowerRow] == 0)
			{
				this.inWater = false;
				this.anim.SetTrigger("BackToLand");
				GameAPP.PlaySound(71, 0.5f);
			}
		}
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x0000FDE8 File Offset: 0x0000DFE8
	private void OnTriggerStay2D(Collider2D collision)
	{
		GameObject gameObject = collision.gameObject;
		if (gameObject.CompareTag("Zombie"))
		{
			Zombie component = gameObject.GetComponent<Zombie>();
			if (component.theZombieRow == this.theMowerRow && !component.isMindControlled)
			{
				if (!this.isStart && component.theStatus != 1)
				{
					this.StartMove();
					this.isStart = true;
				}
				component.Die(1);
			}
		}
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000FE4C File Offset: 0x0000E04C
	private void StartMove()
	{
		if (this.theMowerType == 0)
		{
			this.anim.SetBool("isMoving", true);
		}
		else if (this.theMowerType == 1)
		{
			this.anim.SetFloat("Speed", 1f);
		}
		this.rb.velocity = new Vector2(this.speed, this.rb.velocity.y);
	}

	// Token: 0x060001EB RID: 491 RVA: 0x0000FEB8 File Offset: 0x0000E0B8
	public void Die()
	{
		GameObject[] mowerArray = GameAPP.board.GetComponent<Board>().mowerArray;
		for (int i = 0; i < mowerArray.Length; i++)
		{
			if (mowerArray[i] == base.gameObject)
			{
				mowerArray[i] = null;
				break;
			}
		}
		Object.Destroy(base.gameObject, 0.5f);
	}

	// Token: 0x0400013E RID: 318
	public int theMowerRow;

	// Token: 0x0400013F RID: 319
	public int theMowerType;

	// Token: 0x04000140 RID: 320
	private bool isStart;

	// Token: 0x04000141 RID: 321
	private readonly float speed = 5f;

	// Token: 0x04000142 RID: 322
	private Rigidbody2D rb;

	// Token: 0x04000143 RID: 323
	private Animator anim;

	// Token: 0x04000144 RID: 324
	private int theBoxX;

	// Token: 0x04000145 RID: 325
	private bool inWater;
}

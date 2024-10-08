using System;
using UnityEngine;

// Token: 0x02000023 RID: 35
public class DrownWeapon : MonoBehaviour
{
	// Token: 0x0600009B RID: 155 RVA: 0x00005138 File Offset: 0x00003338
	private void Start()
	{
		if (this.target == null)
		{
			this.vx = -10f;
			this.vy = 5f;
		}
		else
		{
			this.targetPlant = this.target.GetComponent<Plant>();
			this.distanceX = Mathf.Abs(base.transform.position.x - this.targetPlant.shadow.transform.position.x);
			this.distanceY = Mathf.Abs(base.transform.position.y - this.targetPlant.shadow.transform.position.y);
			this.duringTime = (this.vy + Mathf.Sqrt(this.vy * this.vy + 2f * this.g * this.distanceY)) / this.g;
			this.vx = -this.distanceX / this.duringTime;
		}
		base.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = string.Format("bullet{0}", this.theRow);
		base.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -10;
	}

	// Token: 0x0600009C RID: 156 RVA: 0x0000527C File Offset: 0x0000347C
	private void Update()
	{
		this.existTime += Time.deltaTime;
		if (this.existTime > 3f)
		{
			Object.Destroy(base.gameObject);
		}
		if (this.targetPlant != null)
		{
			if (base.transform.position.y > this.targetPlant.shadow.transform.position.y)
			{
				this.velocity = new Vector3(this.vx, this.vy, 0f);
				base.transform.position += this.velocity * Time.deltaTime;
				this.vy -= this.g * Time.deltaTime;
				float num = Mathf.Atan2(this.vy, -this.vx) * 57.29578f;
				base.transform.localRotation = Quaternion.Euler(0f, 0f, -num);
				return;
			}
		}
		else
		{
			this.velocity = new Vector3(this.vx, this.vy, 0f);
			base.transform.position += this.velocity * Time.deltaTime;
			this.vy -= this.g * Time.deltaTime;
			float num2 = Mathf.Atan2(this.vy, -this.vx) * 57.29578f;
			base.transform.localRotation = Quaternion.Euler(0f, 0f, -num2);
		}
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00005414 File Offset: 0x00003614
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Plant plant;
		if (collision.TryGetComponent<Plant>(out plant) && plant.thePlantRow == this.theRow)
		{
			plant.thePlantHealth -= Mathf.Max((int)(0.2 * (double)plant.thePlantHealth), 20);
			plant.FlashOnce();
			this.Die();
		}
	}

	// Token: 0x0600009E RID: 158 RVA: 0x0000546B File Offset: 0x0000366B
	private void Die()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000082 RID: 130
	public GameObject target;

	// Token: 0x04000083 RID: 131
	public float g = 10f;

	// Token: 0x04000084 RID: 132
	public float vx;

	// Token: 0x04000085 RID: 133
	public float vy = 4f;

	// Token: 0x04000086 RID: 134
	public float distanceX;

	// Token: 0x04000087 RID: 135
	public float distanceY;

	// Token: 0x04000088 RID: 136
	public int theRow;

	// Token: 0x04000089 RID: 137
	private float duringTime;

	// Token: 0x0400008A RID: 138
	private Plant targetPlant;

	// Token: 0x0400008B RID: 139
	private Vector3 velocity;

	// Token: 0x0400008C RID: 140
	private float existTime;
}

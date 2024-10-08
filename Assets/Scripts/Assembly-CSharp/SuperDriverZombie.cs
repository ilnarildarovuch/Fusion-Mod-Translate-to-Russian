using System;
using UnityEngine;

// Token: 0x020000DB RID: 219
public class SuperDriverZombie : DriverZombie
{
	// Token: 0x06000403 RID: 1027 RVA: 0x0001F020 File Offset: 0x0001D220
	protected override void BodyTakeDamage(int theDamage)
	{
		this.theHealth -= (float)theDamage;
		if (this.theHealth >= (float)this.theMaxHealth / 3f && this.theHealth < (float)this.theMaxHealth * 2f / 3f)
		{
			base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[33];
			base.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[34];
		}
		if (this.theHealth < (float)this.theMaxHealth / 3f)
		{
			this.anim.SetTrigger("shake");
			base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[35];
			base.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[36];
			base.transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[37];
			base.transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[37];
			base.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
			GameObject gameObject = base.transform.GetChild(1).GetChild(0).gameObject;
			gameObject.SetActive(true);
			foreach (object obj in gameObject.transform)
			{
				Transform transform = (Transform)obj;
				transform.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
				transform.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = this.baseLayer + 29;
			}
		}
		if (this.theHealth <= 0f)
		{
			base.DieAndExplode();
		}
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x0001F224 File Offset: 0x0001D424
	protected override void DriverPositionUpdate()
	{
		float x = base.transform.GetChild(4).position.x;
		if (Board.Instance.iceRoadX[this.theZombieRow] > x)
		{
			base.transform.Translate(-0.2f * Time.deltaTime, 0f, 0f);
			return;
		}
		base.transform.Translate(-this.currentSpeed * Time.deltaTime, 0f, 0f);
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x0001F2A0 File Offset: 0x0001D4A0
	public override void KillByCaltrop()
	{
		if (this.theHealth > 0.5f * (float)this.theMaxHealth)
		{
			this.TakeDamage(0, (int)((float)this.theMaxHealth * 0.5f));
			return;
		}
		this.anim.SetTrigger("shake");
		this.anim.SetTrigger("GoDie");
		base.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[35];
		base.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[36];
		base.transform.GetChild(1).GetChild(1).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[37];
		base.transform.GetChild(1).GetChild(2).GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[37];
		base.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
		GameObject gameObject = base.transform.GetChild(1).GetChild(0).gameObject;
		gameObject.SetActive(true);
		foreach (object obj in gameObject.transform)
		{
			Transform transform = (Transform)obj;
			transform.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
			transform.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = this.baseLayer + 29;
		}
		base.GetComponent<BoxCollider2D>().enabled = false;
		this.theStatus = 1;
		base.Invoke("DieAndExplode", 2f);
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x0001F45C File Offset: 0x0001D65C
	protected override void DieEvent()
	{
		GameAPP.PlaySound(43, 0.5f);
		Vector2 vector = this.shadow.transform.position;
		vector = new Vector2(vector.x, vector.y + 0.6f);
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[36], vector, Quaternion.identity, this.board.transform);
	}
}

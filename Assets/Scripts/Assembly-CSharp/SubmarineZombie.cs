using System;
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class SubmarineZombie : Zombie
{
	// Token: 0x0600041A RID: 1050 RVA: 0x0001FEBB File Offset: 0x0001E0BB
	protected override void Start()
	{
		base.Start();
		this.inWater = true;
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x0001FECC File Offset: 0x0001E0CC
	protected override void Update()
	{
		base.MoveUpdate();
		if (GameAPP.theGameStatus == 0 && ((this.isMindControlled && base.transform.position.x > 10f) || base.transform.position.x > 12f || base.transform.position.x < -10f))
		{
			this.Die(2);
		}
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x0001FF3C File Offset: 0x0001E13C
	protected override void BodyTakeDamage(int theDamage)
	{
		this.theHealth -= (float)theDamage;
		if (this.theHealth >= (float)(this.theMaxHealth * 2) / 3f)
		{
			base.transform.GetChild(0).gameObject.SetActive(true);
			base.transform.GetChild(1).gameObject.SetActive(false);
			base.transform.GetChild(2).gameObject.SetActive(false);
			base.transform.GetChild(3).gameObject.SetActive(true);
			base.transform.GetChild(4).gameObject.SetActive(false);
			base.transform.GetChild(5).gameObject.SetActive(false);
		}
		if (this.theHealth >= (float)this.theMaxHealth / 3f && this.theHealth < (float)(this.theMaxHealth * 2) / 3f)
		{
			base.transform.GetChild(0).gameObject.SetActive(false);
			base.transform.GetChild(1).gameObject.SetActive(true);
			base.transform.GetChild(2).gameObject.SetActive(false);
			base.transform.GetChild(3).gameObject.SetActive(false);
			base.transform.GetChild(4).gameObject.SetActive(true);
			base.transform.GetChild(5).gameObject.SetActive(false);
		}
		if (this.theHealth < (float)this.theMaxHealth / 3f)
		{
			base.transform.GetChild(0).gameObject.SetActive(false);
			base.transform.GetChild(1).gameObject.SetActive(false);
			base.transform.GetChild(2).gameObject.SetActive(true);
			base.transform.GetChild(3).gameObject.SetActive(false);
			base.transform.GetChild(4).gameObject.SetActive(false);
			base.transform.GetChild(5).gameObject.SetActive(true);
		}
		if (this.theHealth <= 0f)
		{
			this.Die(2);
		}
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x00020170 File Offset: 0x0001E370
	protected override void DieEvent()
	{
		GameAPP.PlaySound(43, 0.5f);
		Vector2 vector = this.shadow.transform.position;
		vector = new Vector2(vector.x, vector.y + 0.6f);
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[34], vector, Quaternion.identity, this.board.transform);
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x000201DC File Offset: 0x0001E3DC
	protected override void OnTriggerStay2D(Collider2D collision)
	{
		Plant plant;
		if (!this.isMindControlled && collision.TryGetComponent<Plant>(out plant))
		{
			if (TypeMgr.IsCaltrop(plant.thePlantType))
			{
				return;
			}
			if (plant.thePlantRow == this.theZombieRow)
			{
				if (plant.thePlantType == 903)
				{
					plant.thePlantHealth -= 500;
					base.transform.Translate(1f, 0f, 0f);
					GameAPP.PlaySound(Random.Range(72, 75), 0.5f);
					return;
				}
				GameAPP.PlaySound(75, 0.5f);
				Vector2 vector = plant.shadow.transform.position;
				vector = new Vector2(vector.x, vector.y - 0.4f);
				this.SetWaterSplat(vector, new Vector2(0.5f, 0.5f));
				plant.Crashed();
			}
		}
		Zombie zombie;
		if (collision.TryGetComponent<Zombie>(out zombie) && zombie.isMindControlled != this.isMindControlled && zombie.theZombieRow == this.theZombieRow)
		{
			zombie.TakeDamage(4, 20);
		}
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x000202F4 File Offset: 0x0001E4F4
	private GameObject SetWaterSplat(Vector2 position, Vector2 scale)
	{
		GameObject gameObject = Object.Instantiate<GameObject>(Resources.Load<GameObject>("Particle/Anim/Water/WaterSplashPrefab"), position, Quaternion.identity, GameAPP.board.transform);
		foreach (object obj in gameObject.transform)
		{
			((Transform)obj).GetComponent<SpriteRenderer>().sortingLayerName = string.Format("particle{0}", this.theZombieRow);
		}
		gameObject.transform.localScale = scale;
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[32], position, Quaternion.identity, GameAPP.board.transform);
		return gameObject;
	}
}

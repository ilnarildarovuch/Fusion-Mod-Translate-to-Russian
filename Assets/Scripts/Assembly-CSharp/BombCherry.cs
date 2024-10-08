using System;
using UnityEngine;

// Token: 0x02000056 RID: 86
public class BombCherry : MonoBehaviour
{
	// Token: 0x060001A4 RID: 420 RVA: 0x0000E0C0 File Offset: 0x0000C2C0
	private void Start()
	{
		this.board = GameAPP.board.GetComponent<Board>();
		Vector3 position = base.transform.position;
		base.transform.position = new Vector3(position.x, position.y, 1f);
		if (this.isFromZombie)
		{
			this.ZombieExplode(position, 1.5f);
			return;
		}
		if (this.bombType == 2)
		{
			this.Explode(position, 1.5f);
			return;
		}
		this.Explode(position, 2f);
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x0000E150 File Offset: 0x0000C350
	private void Explode(Vector2 explosionPosition, float explosionRadius)
	{
		foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(explosionPosition, explosionRadius))
		{
			if (collider2D.CompareTag("Plant"))
			{
				Plant component = collider2D.GetComponent<Plant>();
				if (Mathf.Abs(component.thePlantRow - this.bombRow) <= 1 && (component.thePlantType == 1003 || component.thePlantType == 903))
				{
					component.Recover(25);
					if (this.bombType == 0)
					{
						component.Recover(975);
					}
					component.FlashOnce();
				}
			}
			else if (collider2D.CompareTag("Zombie"))
			{
				Zombie component2 = collider2D.GetComponent<Zombie>();
				if (!component2.isMindControlled)
				{
					if (component2.theZombieType == 107)
					{
						component2.TakeDamage(10, 150);
					}
					else if (Mathf.Abs(component2.theZombieRow - this.bombRow) <= 1)
					{
						if (this.bombType == 2)
						{
							if (component2.theStatus != 7)
							{
								component2.TakeDamage(10, 300);
							}
						}
						else if (component2.theHealth > 1800f)
						{
							component2.TakeDamage(10, 1800);
						}
						else
						{
							if (this.bombType == 1)
							{
								GameAPP.board.GetComponent<CreateCoin>().SetCoin(0, 0, 0, 0, base.transform.position);
							}
							component2.Charred();
						}
					}
				}
			}
		}
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x0000E2B4 File Offset: 0x0000C4B4
	private void ZombieExplode(Vector2 explosionPosition, float explosionRadius)
	{
		if (this.targetPlant != null)
		{
			this.thePlant = this.targetPlant.GetComponent<Plant>();
			if (this.thePlant.isNut && GameAPP.difficulty <= 3)
			{
				this.PlantTakeDamage(this.thePlant);
				return;
			}
			foreach (GameObject gameObject in this.board.plantArray)
			{
				if (!(gameObject == null))
				{
					Plant component = gameObject.GetComponent<Plant>();
					if (component.thePlantRow == this.bombRow)
					{
						if (Mathf.Abs(component.thePlantColumn - this.thePlant.thePlantColumn) <= 1)
						{
							this.PlantTakeDamage(component);
						}
					}
					else if (component.thePlantRow == this.thePlant.thePlantRow + 1 && !this.board.isEveStarted)
					{
						if (component.thePlantColumn == this.thePlant.thePlantColumn)
						{
							this.PlantTakeDamage(component);
						}
					}
					else if (component.thePlantRow == this.thePlant.thePlantRow - 1 && !this.board.isEveStarted && component.thePlantColumn == this.thePlant.thePlantColumn)
					{
						this.PlantTakeDamage(component);
					}
				}
			}
		}
		foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(explosionPosition, explosionRadius))
		{
			if (collider2D.CompareTag("Zombie"))
			{
				Zombie component2 = collider2D.GetComponent<Zombie>();
				if (component2.isMindControlled)
				{
					if (component2.theZombieType == 107)
					{
						component2.TakeDamage(10, 150);
					}
					else if (Mathf.Abs(component2.theZombieRow - this.bombRow) <= 1)
					{
						component2.TakeDamage(10, 300);
					}
				}
			}
		}
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x0000E468 File Offset: 0x0000C668
	private void PlantTakeDamage(Plant plant)
	{
		if (TypeMgr.IsCaltrop(plant.thePlantType))
		{
			return;
		}
		int thePlantType;
		if (GameAPP.difficulty < 5)
		{
			thePlantType = plant.thePlantType;
			if (thePlantType <= 903)
			{
				if (thePlantType == 12)
				{
					return;
				}
				if (thePlantType != 903)
				{
					goto IL_72;
				}
			}
			else if (thePlantType != 1003)
			{
				if (thePlantType != 1020 && thePlantType - 1028 > 1)
				{
					goto IL_72;
				}
				plant.thePlantHealth -= 500;
				goto IL_84;
			}
			plant.Recover(200);
			goto IL_84;
			IL_72:
			plant.thePlantHealth -= 1000;
			IL_84:
			plant.FlashOnce();
			return;
		}
		thePlantType = plant.thePlantType;
		if (thePlantType != 12)
		{
			if (thePlantType == 903 || thePlantType == 1003)
			{
				plant.Recover(200);
			}
			else
			{
				plant.thePlantHealth -= 1000;
			}
			plant.FlashOnce();
			return;
		}
	}

	// Token: 0x04000117 RID: 279
	public bool isFromZombie;

	// Token: 0x04000118 RID: 280
	public int bombType;

	// Token: 0x04000119 RID: 281
	public int bombRow;

	// Token: 0x0400011A RID: 282
	public GameObject targetPlant;

	// Token: 0x0400011B RID: 283
	private Plant thePlant;

	// Token: 0x0400011C RID: 284
	private Board board;

	// Token: 0x0400011D RID: 285
	private const int plantSmallExplodeDamage = 300;
}

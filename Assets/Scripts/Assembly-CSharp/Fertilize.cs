using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000FA RID: 250
public class Fertilize : Bucket
{
	// Token: 0x060004D8 RID: 1240 RVA: 0x00029DB8 File Offset: 0x00027FB8
	protected override void Start()
	{
		base.Start();
		this.anim = base.GetComponent<Animator>();
		this.gravity = 15f;
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x00029DD7 File Offset: 0x00027FD7
	protected override void Update()
	{
		base.PositionUpdate();
		this.existTime += Time.deltaTime;
		if (this.existTime > 30f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x00029E09 File Offset: 0x00028009
	public override void Pick()
	{
		base.Pick();
		this.anim.SetTrigger("idleToStatic");
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x00029E24 File Offset: 0x00028024
	public override void Use()
	{
		Vector2 vector = new Vector2((float)this.m.theMouseColumn, (float)this.m.theMouseRow);
		foreach (GameObject gameObject in GameAPP.board.GetComponent<Board>().plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				if ((float)component.thePlantColumn == vector.x && (float)component.thePlantRow == vector.y)
				{
					this.theTargetPlant = component;
					base.transform.position = new Vector3(component.shadow.transform.position.x, component.shadow.transform.position.y + 1.5f);
					this.anim.SetTrigger("staticToUse");
					GameAPP.PlaySound(65, 0.5f);
					break;
				}
			}
		}
		base.GetComponent<Collider2D>().enabled = true;
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x00029F20 File Offset: 0x00028120
	private void Upgrade()
	{
		if (this.theTargetPlant == null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		int thePlantColumn = this.theTargetPlant.thePlantColumn;
		int thePlantRow = this.theTargetPlant.thePlantRow;
		foreach (GameObject gameObject in GameAPP.board.GetComponent<Board>().plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				if (component.thePlantRow == thePlantRow && component.thePlantColumn == thePlantColumn)
				{
					component.Recover(component.thePlantMaxHealth);
					if (component.attributeCountdown != 0f)
					{
						component.attributeCountdown = ((component.attributeCountdown > 0.5f) ? 0.5f : component.attributeCountdown);
					}
					int thePlantType = component.thePlantType;
					if (thePlantType <= 17)
					{
						if (thePlantType <= 7)
						{
							if (thePlantType != 3)
							{
								if (thePlantType == 7)
								{
									component.Die();
									CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, 1070, null, default(Vector2), true, 0f);
								}
							}
							else
							{
								component.Die();
								CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, 1027, null, default(Vector2), true, 0f);
							}
						}
						else if (thePlantType != 12)
						{
							if (thePlantType == 17)
							{
								component.Die();
								CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, 1060, null, default(Vector2), true, 0f);
							}
						}
						else
						{
							bool flag = false;
							using (IEnumerator enumerator = gameObject.transform.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									if (((Transform)enumerator.Current).gameObject.CompareTag("Plant"))
									{
										flag = true;
										break;
									}
								}
							}
							if (!flag)
							{
								component.Die();
								CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, 1067, null, default(Vector2), true, 0f);
							}
						}
					}
					else if (thePlantType <= 1037)
					{
						if (thePlantType != 1031)
						{
							if (thePlantType == 1037)
							{
								component.Die();
								CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, 1072, null, default(Vector2), true, 0f);
							}
						}
						else
						{
							component.GetComponent<SunShroom>().Grow();
						}
					}
					else if (thePlantType != 1043)
					{
						if (thePlantType != 1058)
						{
							if (thePlantType == 1061)
							{
								component.Die();
								CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, 1075, null, default(Vector2), true, 0f);
							}
						}
						else
						{
							component.Die();
							CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, 14, null, default(Vector2), true, 0f);
						}
					}
					else
					{
						component.GetComponent<DoomFume>().ShootCD = 0.5f;
					}
				}
			}
		}
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000261 RID: 609
	private Plant theTargetPlant;

	// Token: 0x04000262 RID: 610
	private Animator anim;
}

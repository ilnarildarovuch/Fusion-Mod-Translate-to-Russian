using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200008F RID: 143
public class LilyPad : Plant
{
	// Token: 0x060002DC RID: 732 RVA: 0x00016EC8 File Offset: 0x000150C8
	protected override void Start()
	{
		base.Start();
		this.col = base.GetComponents<BoxCollider2D>();
		this.startPos = base.transform.position;
		this.r = base.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
		this.existTime = Random.Range(0f, 0.5f);
	}

	// Token: 0x060002DD RID: 733 RVA: 0x00016F29 File Offset: 0x00015129
	protected override void Update()
	{
		base.Update();
		this.PostionUpdate();
		if (this.lilyType != -1)
		{
			this.SummonUpdate();
		}
	}

	// Token: 0x060002DE RID: 734 RVA: 0x00016F48 File Offset: 0x00015148
	private void SummonUpdate()
	{
		this.growTime += Time.deltaTime;
		if (this.growTime > 90f)
		{
			this.growTime = 0f;
			if (CreatePlant.Instance.SetPlant(this.thePlantColumn, this.thePlantRow, this.lilyType, null, default(Vector2), false, 0f) != null)
			{
				Vector2 vector = base.transform.position;
				vector = new Vector2(vector.x, vector.y + 0.5f);
				Object.Instantiate<GameObject>(GameAPP.particlePrefab[11], vector, Quaternion.identity, this.board.transform);
			}
		}
	}

	// Token: 0x060002DF RID: 735 RVA: 0x00017004 File Offset: 0x00015204
	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		bool flag = false;
		foreach (GameObject gameObject in this.board.plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				if (component.thePlantRow == this.thePlantRow && component.thePlantColumn == this.thePlantColumn && component != this)
				{
					if (this.lilyType != component.thePlantType && this.AllowChange(component.thePlantType))
					{
						this.lilyType = component.thePlantType;
						this.ChangeSprite(this.lilyType);
					}
					if (!component.adjustPosByLily)
					{
						component.adjustPosByLily = true;
						component.gameObject.transform.parent = base.transform;
						Vector2 vector = this.shadow.transform.position;
						vector = new Vector2(vector.x, vector.y + 0.08f);
						this.AdjustPosition(component.gameObject, vector);
					}
					flag = true;
				}
			}
		}
		BoxCollider2D[] array;
		if (!flag)
		{
			array = this.col;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = true;
			}
			return;
		}
		array = this.col;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = false;
		}
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x00017170 File Offset: 0x00015370
	private void ChangeSprite(int type)
	{
		Sprite sprite = null;
		if (type != 1)
		{
			switch (type)
			{
			case 13:
				sprite = Resources.Load<Sprite>("Plants/LilyPad/Lily_Squash");
				break;
			case 14:
				sprite = Resources.Load<Sprite>("Plants/LilyPad/Lily_Three");
				break;
			case 16:
				sprite = Resources.Load<Sprite>("Plants/LilyPad/Lily_Jalapeno");
				break;
			case 18:
				sprite = Resources.Load<Sprite>("Plants/LilyPad/Lily_TorchWood");
				break;
			}
		}
		else
		{
			sprite = Resources.Load<Sprite>("Plants/LilyPad/Lily_Sun");
		}
		if (sprite != null)
		{
			this.r.sprite = sprite;
		}
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x000171FA File Offset: 0x000153FA
	private bool AllowChange(int theSeedType)
	{
		if (theSeedType != 1)
		{
			switch (theSeedType)
			{
			case 13:
			case 14:
			case 16:
			case 18:
				return true;
			}
			return false;
		}
		return true;
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x00017228 File Offset: 0x00015428
	private void PostionUpdate()
	{
		this.existTime += Time.deltaTime;
		float d = Mathf.Sin(this.existTime * this.frequency) * this.floatStrength;
		base.transform.position = this.startPos + Vector3.up * d;
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x00017284 File Offset: 0x00015484
	private void AdjustPosition(GameObject plant, Vector3 position)
	{
		foreach (object obj in plant.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.name == "Shadow")
			{
				Vector3 position2 = transform.position;
				Vector3 b = position - position2;
				plant.transform.position += b;
			}
		}
		this.SetPuffTransform(plant);
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x00017318 File Offset: 0x00015518
	public void SetPuffTransform(GameObject plant)
	{
		Plant component = plant.GetComponent<Plant>();
		if (CreatePlant.Instance.IsPuff(component.thePlantType))
		{
			bool[] array = new bool[3];
			Vector3 position = plant.transform.position;
			foreach (GameObject gameObject in this.board.plantArray)
			{
				if (gameObject != null)
				{
					Plant component2 = gameObject.GetComponent<Plant>();
					if (CreatePlant.Instance.IsPuff(component2.thePlantType) && CreatePlant.Instance.InTheSameBox(component, component2))
					{
						array[component2.place] = true;
					}
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				if (!array[j])
				{
					component.place = j;
					break;
				}
			}
			switch (component.place)
			{
			case 0:
				component.transform.position = new Vector3(position.x, position.y + 0.2f);
				CreatePlant.Instance.SetPuffLayer(component.gameObject, true, component.thePlantRow);
				return;
			case 1:
				component.transform.position = new Vector3(position.x + 0.25f, position.y - 0.2f);
				CreatePlant.Instance.SetPuffLayer(component.gameObject, false, component.thePlantRow);
				return;
			case 2:
				component.transform.position = new Vector3(position.x - 0.25f, position.y - 0.2f);
				CreatePlant.Instance.SetPuffLayer(component.gameObject, false, component.thePlantRow);
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x000174AE File Offset: 0x000156AE
	private IEnumerator SunBright(Material mt)
	{
		for (float i = 1f; i < 4f; i += 0.1f)
		{
			mt.SetFloat("_Brightness", i);
			yield return new WaitForFixedUpdate();
		}
		for (float i = 4f; i > 1f; i -= 0.1f)
		{
			mt.SetFloat("_Brightness", i);
			yield return new WaitForFixedUpdate();
		}
		yield break;
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x000174C0 File Offset: 0x000156C0
	protected virtual void ProduceSun()
	{
		GameAPP.PlaySound(Random.Range(3, 5), 0.3f);
		CreateCoin.Instance.SetCoin(this.thePlantColumn, this.thePlantRow, 0, 0, default(Vector3));
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x00017500 File Offset: 0x00015700
	private void SunLilyUpdate()
	{
		this.thePlantProduceCountDown -= Time.deltaTime;
		if (this.thePlantProduceCountDown < 0f)
		{
			this.thePlantProduceCountDown = this.thePlantProduceInterval;
			this.thePlantProduceCountDown += (float)Random.Range(-2, 3);
			foreach (object obj in base.transform)
			{
				Transform transform = (Transform)obj;
				if (!(transform.name == "Shadow"))
				{
					if (transform.childCount != 0)
					{
						using (IEnumerator enumerator2 = transform.transform.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								SpriteRenderer spriteRenderer;
								if (((Transform)enumerator2.Current).TryGetComponent<SpriteRenderer>(out spriteRenderer))
								{
									Material material = spriteRenderer.material;
									base.StartCoroutine(this.SunBright(material));
								}
							}
						}
					}
					SpriteRenderer spriteRenderer2;
					if (transform.TryGetComponent<SpriteRenderer>(out spriteRenderer2))
					{
						Material material2 = spriteRenderer2.material;
						base.StartCoroutine(this.SunBright(material2));
					}
				}
			}
			base.Invoke("ProduceSun", 0.5f);
		}
	}

	// Token: 0x040001A4 RID: 420
	private BoxCollider2D[] col;

	// Token: 0x040001A5 RID: 421
	private readonly float floatStrength = 0.05f;

	// Token: 0x040001A6 RID: 422
	private readonly float frequency = 1.2f;

	// Token: 0x040001A7 RID: 423
	private float existTime;

	// Token: 0x040001A8 RID: 424
	private int lilyType = -1;

	// Token: 0x040001A9 RID: 425
	private SpriteRenderer r;

	// Token: 0x040001AA RID: 426
	[SerializeField]
	private float growTime;
}

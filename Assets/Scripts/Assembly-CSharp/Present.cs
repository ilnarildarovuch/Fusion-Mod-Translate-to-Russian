using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000090 RID: 144
public class Present : Plant
{
	// Token: 0x060002E9 RID: 745 RVA: 0x00017679 File Offset: 0x00015879
	protected override void Start()
	{
		base.Start();
		this.anim.Play("idle");
	}

	// Token: 0x060002EA RID: 746 RVA: 0x00017694 File Offset: 0x00015894
	public void AnimEvent()
	{
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[11], base.transform.position, Quaternion.identity).transform.SetParent(GameAPP.board.transform);
		this.Die();
		if (this.board.isEveStarted)
		{
			Board.Instance.SetEvePlants(this.thePlantColumn, this.thePlantRow);
			return;
		}
		this.RandomPlant();
	}

	// Token: 0x060002EB RID: 747 RVA: 0x00017704 File Offset: 0x00015904
	private void RandomPlant()
	{
		for (int i = 0; i < this.basePlantNum; i++)
		{
			this.list.Add(i);
		}
		int num = Random.Range(0, this.basePlantNum);
		while (CreatePlant.Instance.IsWaterPlant(num))
		{
			num = Random.Range(0, this.basePlantNum);
		}
		foreach (GameObject gameObject in this.board.plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				if (component.thePlantRow == this.thePlantRow && component.thePlantColumn == this.thePlantColumn && component.thePlantType != 12)
				{
					int num2 = 1000;
					while (num2-- >= 0 && this.list.Count != 0)
					{
						int index = Random.Range(0, this.list.Count);
						num = this.list[index];
						if (MixData.data[component.thePlantType, num] != 0)
						{
							break;
						}
						this.list.RemoveAt(index);
					}
				}
			}
		}
		if (num == 6)
		{
			CreatePlant.Instance.SetPlant(this.thePlantColumn, this.thePlantRow, num, null, default(Vector2), false, 0f);
			CreatePlant.Instance.SetPlant(this.thePlantColumn, this.thePlantRow, num, null, default(Vector2), false, 0f);
		}
		if (CreatePlant.Instance.SetPlant(this.thePlantColumn, this.thePlantRow, num, null, default(Vector2), false, 0f) == null)
		{
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, base.transform.position);
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, base.transform.position);
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, base.transform.position);
			CreateCoin.Instance.SetCoin(0, 0, 0, 0, base.transform.position);
		}
	}

	// Token: 0x040001AB RID: 427
	private readonly List<int> list = new List<int>();

	// Token: 0x040001AC RID: 428
	private readonly int basePlantNum = 19;
}

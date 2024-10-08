using System.Collections;
using UnityEngine;

public class Producer : Plant
{
	protected override void Update()
	{
		base.Update();
		if (!board.isIZ && GameAPP.theGameStatus == 0)
		{
			ProducerUpdate();
		}
	}

	public override void ProducerUpdate()
	{
		thePlantProduceCountDown -= Time.deltaTime;
		if (!(thePlantProduceCountDown < 0f))
		{
			return;
		}
		thePlantProduceCountDown = thePlantProduceInterval;
		thePlantProduceCountDown += Random.Range(-2, 3);
		foreach (Transform item in base.transform)
		{
			if (item.name == "Shadow")
			{
				continue;
			}
			if (item.childCount != 0)
			{
				foreach (Transform item2 in item.transform)
				{
					if (item2.TryGetComponent<SpriteRenderer>(out var component))
					{
						Material material = component.material;
						StartCoroutine(SunBright(material));
					}
				}
			}
			if (item.TryGetComponent<SpriteRenderer>(out var component2))
			{
				Material material2 = component2.material;
				StartCoroutine(SunBright(material2));
			}
		}
		Invoke("ProduceSun", 0.5f);
	}

	private IEnumerator SunBright(Material mt)
	{
		for (float j = 1f; j < 4f; j += 0.1f)
		{
			mt.SetFloat("_Brightness", j);
			yield return new WaitForFixedUpdate();
		}
		for (float j = 4f; j > 1f; j -= 0.1f)
		{
			mt.SetFloat("_Brightness", j);
			yield return new WaitForFixedUpdate();
		}
	}

	protected virtual void ProduceSun()
	{
		GameAPP.PlaySound(Random.Range(3, 5), 0.3f);
		CreateCoin.Instance.SetCoin(thePlantColumn, thePlantRow, 0, 0);
	}

	protected virtual void ProduceSunWithNoSound()
	{
		Random.Range(3, 5);
		CreateCoin.Instance.SetCoin(thePlantColumn, thePlantRow, 0, 0);
	}
}

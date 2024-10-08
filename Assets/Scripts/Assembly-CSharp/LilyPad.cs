using System.Collections;
using UnityEngine;

public class LilyPad : Plant
{
	private BoxCollider2D[] col;

	private readonly float floatStrength = 0.05f;

	private readonly float frequency = 1.2f;

	private float existTime;

	private int lilyType = -1;

	private SpriteRenderer r;

	[SerializeField]
	private float growTime;

	protected override void Start()
	{
		base.Start();
		col = GetComponents<BoxCollider2D>();
		startPos = base.transform.position;
		r = base.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
		existTime = Random.Range(0f, 0.5f);
	}

	protected override void Update()
	{
		base.Update();
		PostionUpdate();
		if (lilyType != -1)
		{
			SummonUpdate();
		}
	}

	private void SummonUpdate()
	{
		growTime += Time.deltaTime;
		if (growTime > 90f)
		{
			growTime = 0f;
			if (CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, lilyType) != null)
			{
				Vector2 vector = base.transform.position;
				Object.Instantiate(position: new Vector2(vector.x, vector.y + 0.5f), original: GameAPP.particlePrefab[11], rotation: Quaternion.identity, parent: board.transform);
			}
		}
	}

	protected override void FixedUpdate()
	{
		base.FixedUpdate();
		bool flag = false;
		GameObject[] plantArray = board.plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (!(gameObject != null))
			{
				continue;
			}
			Plant component = gameObject.GetComponent<Plant>();
			if (component.thePlantRow == thePlantRow && component.thePlantColumn == thePlantColumn && component != this)
			{
				if (lilyType != component.thePlantType && AllowChange(component.thePlantType))
				{
					lilyType = component.thePlantType;
					ChangeSprite(lilyType);
				}
				if (!component.adjustPosByLily)
				{
					component.adjustPosByLily = true;
					component.gameObject.transform.parent = base.transform;
					Vector2 vector = shadow.transform.position;
					AdjustPosition(position: new Vector2(vector.x, vector.y + 0.08f), plant: component.gameObject);
				}
				flag = true;
			}
		}
		if (!flag)
		{
			BoxCollider2D[] array = col;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = true;
			}
		}
		else
		{
			BoxCollider2D[] array = col;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
		}
	}

	private void ChangeSprite(int type)
	{
		Sprite sprite = null;
		switch (type)
		{
		case 1:
			sprite = Resources.Load<Sprite>("Plants/LilyPad/Lily_Sun");
			break;
		case 13:
			sprite = Resources.Load<Sprite>("Plants/LilyPad/Lily_Squash");
			break;
		case 16:
			sprite = Resources.Load<Sprite>("Plants/LilyPad/Lily_Jalapeno");
			break;
		case 18:
			sprite = Resources.Load<Sprite>("Plants/LilyPad/Lily_TorchWood");
			break;
		case 14:
			sprite = Resources.Load<Sprite>("Plants/LilyPad/Lily_Three");
			break;
		}
		if (sprite != null)
		{
			r.sprite = sprite;
		}
	}

	private bool AllowChange(int theSeedType)
	{
		switch (theSeedType)
		{
		case 1:
		case 13:
		case 14:
		case 16:
		case 18:
			return true;
		default:
			return false;
		}
	}

	private void PostionUpdate()
	{
		existTime += Time.deltaTime;
		float num = Mathf.Sin(existTime * frequency) * floatStrength;
		base.transform.position = startPos + Vector3.up * num;
	}

	private void AdjustPosition(GameObject plant, Vector3 position)
	{
		foreach (Transform item in plant.transform)
		{
			if (item.name == "Shadow")
			{
				Vector3 position2 = item.position;
				Vector3 vector = position - position2;
				plant.transform.position += vector;
			}
		}
		SetPuffTransform(plant);
	}

	public void SetPuffTransform(GameObject plant)
	{
		Plant component = plant.GetComponent<Plant>();
		if (!CreatePlant.Instance.IsPuff(component.thePlantType))
		{
			return;
		}
		bool[] array = new bool[3];
		Vector3 position = plant.transform.position;
		GameObject[] plantArray = board.plantArray;
		foreach (GameObject gameObject in plantArray)
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
			CreatePlant.Instance.SetPuffLayer(component.gameObject, isLower: true, component.thePlantRow);
			break;
		case 1:
			component.transform.position = new Vector3(position.x + 0.25f, position.y - 0.2f);
			CreatePlant.Instance.SetPuffLayer(component.gameObject, isLower: false, component.thePlantRow);
			break;
		case 2:
			component.transform.position = new Vector3(position.x - 0.25f, position.y - 0.2f);
			CreatePlant.Instance.SetPuffLayer(component.gameObject, isLower: false, component.thePlantRow);
			break;
		}
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

	private void SunLilyUpdate()
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
}

using UnityEngine;

public class Fertilize : Bucket
{
	private Plant theTargetPlant;

	private Animator anim;

	protected override void Start()
	{
		base.Start();
		anim = GetComponent<Animator>();
		gravity = 15f;
	}

	protected override void Update()
	{
		PositionUpdate();
		existTime += Time.deltaTime;
		if (existTime > 30f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	public override void Pick()
	{
		base.Pick();
		anim.SetTrigger("idleToStatic");
	}

	public override void Use()
	{
		Vector2 vector = new Vector2(m.theMouseColumn, m.theMouseRow);
		GameObject[] plantArray = GameAPP.board.GetComponent<Board>().plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				if ((float)component.thePlantColumn == vector.x && (float)component.thePlantRow == vector.y)
				{
					theTargetPlant = component;
					base.transform.position = new Vector3(component.shadow.transform.position.x, component.shadow.transform.position.y + 1.5f);
					anim.SetTrigger("staticToUse");
					GameAPP.PlaySound(65);
					break;
				}
			}
		}
		GetComponent<Collider2D>().enabled = true;
	}

	private void Upgrade()
	{
		if (theTargetPlant == null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		int thePlantColumn = theTargetPlant.thePlantColumn;
		int thePlantRow = theTargetPlant.thePlantRow;
		GameObject[] plantArray = GameAPP.board.GetComponent<Board>().plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (!(gameObject != null))
			{
				continue;
			}
			Plant component = gameObject.GetComponent<Plant>();
			if (component.thePlantRow != thePlantRow || component.thePlantColumn != thePlantColumn)
			{
				continue;
			}
			component.Recover(component.thePlantMaxHealth);
			if (component.attributeCountdown != 0f)
			{
				component.attributeCountdown = ((component.attributeCountdown > 0.5f) ? 0.5f : component.attributeCountdown);
			}
			switch (component.thePlantType)
			{
			case 3:
				component.Die();
				CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, 1027, null, default(Vector2), isFreeSet: true);
				break;
			case 1043:
				component.GetComponent<DoomFume>().ShootCD = 0.5f;
				break;
			case 1031:
				component.GetComponent<SunShroom>().Grow();
				break;
			case 1058:
				component.Die();
				CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, 14, null, default(Vector2), isFreeSet: true);
				break;
			case 17:
				component.Die();
				CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, 1060, null, default(Vector2), isFreeSet: true);
				break;
			case 7:
				component.Die();
				CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, 1070, null, default(Vector2), isFreeSet: true);
				break;
			case 12:
			{
				bool flag = false;
				foreach (Transform item in gameObject.transform)
				{
					if (item.gameObject.CompareTag("Plant"))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					component.Die();
					CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, 1067, null, default(Vector2), isFreeSet: true);
				}
				break;
			}
			case 1061:
				component.Die();
				CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, 1075, null, default(Vector2), isFreeSet: true);
				break;
			case 1037:
				component.Die();
				CreatePlant.Instance.SetPlant(thePlantColumn, thePlantRow, 1072, null, default(Vector2), isFreeSet: true);
				break;
			}
		}
		Object.Destroy(base.gameObject);
	}
}

using UnityEngine;

public class BombCherry : MonoBehaviour
{
	public bool isFromZombie;

	public int bombType;

	public int bombRow;

	public GameObject targetPlant;

	private Plant thePlant;

	private Board board;

	private const int plantSmallExplodeDamage = 300;

	private void Start()
	{
		board = GameAPP.board.GetComponent<Board>();
		Vector3 position = base.transform.position;
		base.transform.position = new Vector3(position.x, position.y, 1f);
		if (isFromZombie)
		{
			ZombieExplode(position, 1.5f);
		}
		else if (bombType == 2)
		{
			Explode(position, 1.5f);
		}
		else
		{
			Explode(position, 2f);
		}
	}

	private void Explode(Vector2 explosionPosition, float explosionRadius)
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(explosionPosition, explosionRadius);
		foreach (Collider2D collider2D in array)
		{
			if (collider2D.CompareTag("Plant"))
			{
				Plant component = collider2D.GetComponent<Plant>();
				if (Mathf.Abs(component.thePlantRow - bombRow) <= 1 && (component.thePlantType == 1003 || component.thePlantType == 903))
				{
					component.Recover(25);
					if (bombType == 0)
					{
						component.Recover(975);
					}
					component.FlashOnce();
				}
			}
			else
			{
				if (!collider2D.CompareTag("Zombie"))
				{
					continue;
				}
				Zombie component2 = collider2D.GetComponent<Zombie>();
				if (component2.isMindControlled)
				{
					continue;
				}
				if (component2.theZombieType == 107)
				{
					component2.TakeDamage(10, 150);
				}
				else
				{
					if (Mathf.Abs(component2.theZombieRow - bombRow) > 1)
					{
						continue;
					}
					if (bombType == 2)
					{
						if (component2.theStatus != 7)
						{
							component2.TakeDamage(10, 300);
						}
						continue;
					}
					if (component2.theHealth > 1800f)
					{
						component2.TakeDamage(10, 1800);
						continue;
					}
					if (bombType == 1)
					{
						GameAPP.board.GetComponent<CreateCoin>().SetCoin(0, 0, 0, 0, base.transform.position);
					}
					component2.Charred();
				}
			}
		}
	}

	private void ZombieExplode(Vector2 explosionPosition, float explosionRadius)
	{
		if (targetPlant != null)
		{
			thePlant = targetPlant.GetComponent<Plant>();
			if (thePlant.isNut && GameAPP.difficulty <= 3)
			{
				PlantTakeDamage(thePlant);
				return;
			}
			GameObject[] plantArray = board.plantArray;
			foreach (GameObject gameObject in plantArray)
			{
				if (gameObject == null)
				{
					continue;
				}
				Plant component = gameObject.GetComponent<Plant>();
				if (component.thePlantRow == bombRow)
				{
					if (Mathf.Abs(component.thePlantColumn - thePlant.thePlantColumn) <= 1)
					{
						PlantTakeDamage(component);
					}
				}
				else if (component.thePlantRow == thePlant.thePlantRow + 1 && !board.isEveStarted)
				{
					if (component.thePlantColumn == thePlant.thePlantColumn)
					{
						PlantTakeDamage(component);
					}
				}
				else if (component.thePlantRow == thePlant.thePlantRow - 1 && !board.isEveStarted && component.thePlantColumn == thePlant.thePlantColumn)
				{
					PlantTakeDamage(component);
				}
			}
		}
		Collider2D[] array = Physics2D.OverlapCircleAll(explosionPosition, explosionRadius);
		foreach (Collider2D collider2D in array)
		{
			if (!collider2D.CompareTag("Zombie"))
			{
				continue;
			}
			Zombie component2 = collider2D.GetComponent<Zombie>();
			if (component2.isMindControlled)
			{
				if (component2.theZombieType == 107)
				{
					component2.TakeDamage(10, 150);
				}
				else if (Mathf.Abs(component2.theZombieRow - bombRow) <= 1)
				{
					component2.TakeDamage(10, 300);
				}
			}
		}
	}

	private void PlantTakeDamage(Plant plant)
	{
		if (TypeMgr.IsCaltrop(plant.thePlantType))
		{
			return;
		}
		if (GameAPP.difficulty < 5)
		{
			switch (plant.thePlantType)
			{
			case 903:
			case 1003:
				plant.Recover(200);
				break;
			case 1020:
			case 1028:
			case 1029:
				plant.thePlantHealth -= 500;
				break;
			case 12:
				return;
			default:
				plant.thePlantHealth -= 1000;
				break;
			}
			plant.FlashOnce();
		}
		else
		{
			switch (plant.thePlantType)
			{
			case 903:
			case 1003:
				plant.Recover(200);
				break;
			case 12:
				return;
			default:
				plant.thePlantHealth -= 1000;
				break;
			}
			plant.FlashOnce();
		}
	}
}

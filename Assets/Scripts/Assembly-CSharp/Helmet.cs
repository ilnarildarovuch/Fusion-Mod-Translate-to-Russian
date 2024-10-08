using UnityEngine;

public class Helmet : Bucket
{
	public override void Use()
	{
		Vector2 vector = new Vector2(m.theMouseColumn, m.theMouseRow);
		GameObject[] plantArray = GameAPP.board.GetComponent<Board>().plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (!(gameObject != null))
			{
				continue;
			}
			Plant component = gameObject.GetComponent<Plant>();
			if ((float)component.thePlantColumn == vector.x && (float)component.thePlantRow == vector.y)
			{
				switch (component.thePlantType)
				{
				case 1027:
					component.Die();
					GameAPP.board.GetComponent<CreatePlant>().SetPlant(component.thePlantColumn, component.thePlantRow, 1028);
					Object.Destroy(base.gameObject);
					break;
				case 1028:
					component.Recover(5000);
					Object.Destroy(base.gameObject);
					break;
				}
			}
		}
		GetComponent<Collider2D>().enabled = true;
	}
}

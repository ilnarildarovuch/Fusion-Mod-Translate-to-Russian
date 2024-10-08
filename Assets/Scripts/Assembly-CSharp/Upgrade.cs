using UnityEngine;

public class Upgrade : Bucket
{
	public override void Use()
	{
		Vector2 vector = new Vector2(m.theMouseColumn, m.theMouseRow);
		GameObject[] plantArray = GameAPP.board.GetComponent<Board>().plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				if ((float)component.thePlantColumn == vector.x && (float)component.thePlantRow == vector.y && component.thePlantType == 3)
				{
					component.Die();
					GameAPP.board.GetComponent<CreatePlant>().SetPlant(component.thePlantColumn, component.thePlantRow, 1027);
					Object.Destroy(base.gameObject);
				}
			}
		}
	}
}

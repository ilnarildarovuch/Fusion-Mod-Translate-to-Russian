using UnityEngine;

public class TwinFlower : SunFlower
{
	protected override void ProduceSun()
	{
		GameAPP.PlaySound(Random.Range(3, 5), 0.3f);
		board.GetComponent<CreateCoin>().SetCoin(thePlantColumn, thePlantRow, 0, 0);
		board.GetComponent<CreateCoin>().SetCoin(thePlantColumn, thePlantRow, 0, 0);
	}
}

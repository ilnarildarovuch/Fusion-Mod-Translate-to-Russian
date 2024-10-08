using UnityEngine;

public class JalaSquash : Squash
{
	protected override void Awake()
	{
		base.Awake();
		range = new Vector2(8f, 8f);
	}

	protected override void AttackZombie()
	{
		base.AttackZombie();
		board.CreateFireLine(thePlantRow);
	}
}

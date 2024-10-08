public class Jalatang : Tanglekelp
{
	public override void Die()
	{
		board.CreateFireLine(thePlantRow);
		base.Die();
	}
}

public class Jalapeno : Plant
{
	protected override void Start()
	{
		base.Start();
		anim.Play("explode");
		GameAPP.PlaySound(39);
	}

	public void AnimExplode()
	{
		board.CreateFireLine(thePlantRow);
		Die();
	}
}

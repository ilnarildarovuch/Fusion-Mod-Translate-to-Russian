public class BucketZombieDuck : BucketZombie
{
	protected override void Start()
	{
		base.Start();
		if (GameAPP.theGameStatus == 0)
		{
			anim.Play("swim");
			anim.SetBool("inWater", value: true);
			inWater = true;
			SetMaskLayer();
		}
	}
}

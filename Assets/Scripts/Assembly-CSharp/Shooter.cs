using UnityEngine;

public class Shooter : Plant
{
	public float dreamTime = 0.1f;

	protected override void Update()
	{
		if (dreamTime > 0f)
		{
			dreamTime -= Time.deltaTime;
			if (dreamTime <= 0f)
			{
				dreamTime = 0f;
			}
		}
		if (GameAPP.theGameStatus == 0)
		{
			if (board.isScaredyDream)
			{
				if (thePlantType == 9)
				{
					PlantShootUpdate();
				}
			}
			else
			{
				PlantShootUpdate();
			}
		}
		base.Update();
	}

	public virtual GameObject AnimShoot()
	{
		return null;
	}
}

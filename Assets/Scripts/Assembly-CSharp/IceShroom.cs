using UnityEngine;

public class IceShroom : Plant
{
	private float ExplodeCountDown = 1.5f;

	protected override void Update()
	{
		base.Update();
		ExplodeCountDown -= Time.deltaTime;
		if (ExplodeCountDown < 0f)
		{
			board.CreateFreeze(shadow.transform.position);
			Die();
		}
	}
}

using UnityEngine;

public class DoomShroom : Plant
{
	protected override void Start()
	{
		base.Start();
		anim.Play("explode");
		GameAPP.PlaySound(39);
	}

	public virtual void AnimExplode()
	{
		Vector2 position = new Vector2(shadow.transform.position.x - 0.3f, shadow.transform.position.y + 0.3f);
		board.SetDoom(thePlantColumn, thePlantRow, setPit: true, position);
	}

	protected override void Update()
	{
		base.Update();
	}
}

using UnityEngine;

public class SunBomb : Plant
{
	protected override void Start()
	{
		base.Start();
		anim.Play("Bomb");
	}

	public void Bomb()
	{
		GameObject gameObject = GameAPP.particlePrefab[3];
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y + 0.5f, 0f);
		GameObject obj = Object.Instantiate(gameObject, position, Quaternion.identity);
		obj.transform.SetParent(GameAPP.board.transform);
		obj.name = gameObject.name;
		obj.GetComponent<BombCherry>().bombRow = thePlantRow;
		obj.GetComponent<BombCherry>().bombType = 1;
		ScreenShake.TriggerShake();
		GameAPP.PlaySound(40);
		Die();
	}

	public void PlaySoundStart()
	{
		GameAPP.PlaySound(39);
	}
}

using UnityEngine;

public class CherryBomb : Plant
{
	protected override void Start()
	{
		base.Start();
		anim.Play("Bomb");
	}

	public void Bomb()
	{
		GameObject gameObject = GameAPP.particlePrefab[2];
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y + 0.5f, 0f);
		GameObject obj = Object.Instantiate(gameObject, position, Quaternion.identity);
		obj.transform.SetParent(GameAPP.board.transform);
		obj.name = gameObject.name;
		obj.GetComponent<BombCherry>().bombRow = thePlantRow;
		ScreenShake.TriggerShake();
		GameAPP.PlaySound(40);
		Die();
	}

	public void PlaySoundStart()
	{
		GameAPP.PlaySound(39);
	}
}

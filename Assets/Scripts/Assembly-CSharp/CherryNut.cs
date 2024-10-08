using UnityEngine;

public class CherryNut : WallNut
{
	public override void Die()
	{
		GameObject gameObject = GameAPP.particlePrefab[2];
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y + 0.5f, 0f);
		GameObject obj = Object.Instantiate(gameObject, position, Quaternion.identity);
		obj.transform.SetParent(GameAPP.board.transform);
		obj.name = gameObject.name;
		obj.GetComponent<BombCherry>().bombRow = thePlantRow;
		ScreenShake.TriggerShake();
		GameAPP.PlaySound(40);
		base.Die();
	}
}

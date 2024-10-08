using UnityEngine;

public class CherrySquashBullet : SquashBullet
{
	protected override void AttackZombie()
	{
		GameObject original = GameAPP.particlePrefab[14];
		Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y, 0f);
		GameObject obj = Object.Instantiate(original, position, Quaternion.identity);
		obj.transform.SetParent(GameAPP.board.transform);
		obj.GetComponent<BombCherry>().bombRow = theBulletRow;
		obj.GetComponent<BombCherry>().bombType = 2;
		GameAPP.PlaySound(40, 0.2f);
	}
}

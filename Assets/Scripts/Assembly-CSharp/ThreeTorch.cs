using UnityEngine;

public class ThreeTorch : Plant
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Bullet>(out var component) && !(component.torchWood == base.gameObject) && !component.isZombieBullet && !component.isHot && component.theBulletType == 0)
		{
			FirePea(component);
		}
	}

	private void FirePea(Bullet bullet)
	{
		Vector2 vector = base.transform.GetChild(0).position;
		GameObject gameObject = CreateBullet.Instance.SetBullet(vector.x, vector.y, thePlantRow, 0, 0);
		Vector2 vector2 = gameObject.transform.localScale;
		gameObject.transform.localScale = new Vector3(vector2.x * 0.75f, vector2.y * 0.75f);
		board.YellowFirePea(gameObject.GetComponent<Bullet>(), this, fromThreeTorch: true);
		if (board.isEveStarted)
		{
			GameObject gameObject2 = CreateBullet.Instance.SetBullet(vector.x, vector.y + 0.3f, thePlantRow, 0, 0);
			Vector2 vector3 = gameObject2.transform.localScale;
			gameObject2.transform.localScale = new Vector3(vector3.x * 0.75f, vector3.y * 0.75f);
			board.YellowFirePea(gameObject2.GetComponent<Bullet>(), this, fromThreeTorch: true);
			GameObject gameObject3 = CreateBullet.Instance.SetBullet(vector.x, vector.y - 0.3f, thePlantRow, 0, 0);
			Vector2 vector4 = gameObject3.transform.localScale;
			gameObject3.transform.localScale = new Vector3(vector4.x * 0.75f, vector4.y * 0.75f);
			board.YellowFirePea(gameObject3.GetComponent<Bullet>(), this, fromThreeTorch: true);
		}
		else
		{
			if (thePlantRow != 0)
			{
				GameObject gameObject2 = CreateBullet.Instance.SetBullet(vector.x, vector.y, thePlantRow - 1, 0, 4);
				Vector2 vector5 = gameObject2.transform.localScale;
				gameObject2.transform.localScale = new Vector3(vector5.x * 0.75f, vector5.y * 0.75f);
				board.YellowFirePea(gameObject2.GetComponent<Bullet>(), this, fromThreeTorch: true);
			}
			else
			{
				GameObject gameObject2 = CreateBullet.Instance.SetBullet(vector.x + 0.5f, vector.y, thePlantRow, 0, 0);
				Vector2 vector6 = gameObject2.transform.localScale;
				gameObject2.transform.localScale = new Vector3(vector6.x * 0.75f, vector6.y * 0.75f);
				board.YellowFirePea(gameObject2.GetComponent<Bullet>(), this, fromThreeTorch: true);
			}
			if (thePlantRow != board.roadNum - 1)
			{
				GameObject gameObject3 = CreateBullet.Instance.SetBullet(vector.x, vector.y, thePlantRow + 1, 0, 5);
				Vector2 vector7 = gameObject3.transform.localScale;
				gameObject3.transform.localScale = new Vector3(vector7.x * 0.75f, vector7.y * 0.75f);
				board.YellowFirePea(gameObject3.GetComponent<Bullet>(), this, fromThreeTorch: true);
			}
			else
			{
				GameObject gameObject3 = CreateBullet.Instance.SetBullet(vector.x + 0.5f, vector.y, thePlantRow, 0, 0);
				Vector2 vector8 = gameObject3.transform.localScale;
				gameObject3.transform.localScale = new Vector3(vector8.x * 0.75f, vector8.y * 0.75f);
				board.YellowFirePea(gameObject3.GetComponent<Bullet>(), this, fromThreeTorch: true);
			}
		}
		bullet.Die();
	}
}

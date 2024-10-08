using UnityEngine;

public class Coin : MonoBehaviour
{
	public int theCoinType;

	public float dieCountDown = 7.5f;

	public int theMoveType;

	public int sunPrice = 25;

	private Transform target;

	private float moveSpeed = 300f;

	private bool isLand;

	public float horizontalSpeed = 1.5f;

	public float verticalSpeed = 4f;

	public float gravity = 12f;

	private Vector2 velocity;

	private Vector2 startPosition;

	private void Start()
	{
		target = GameObject.Find("SunPosition").transform;
		startPosition = base.transform.position;
		velocity = new Vector2(Random.Range(-1.5f, 1.5f), verticalSpeed);
		if (theCoinType == 2)
		{
			sunPrice = 15;
		}
	}

	private void Update()
	{
		if ((GameAPP.autoCollect && isLand) || (theMoveType == 1 && Time.timeScale != 0f))
		{
			MoveToPosition();
		}
		else if (!isLand)
		{
			velocity.y -= gravity * Time.deltaTime;
			base.transform.Translate(velocity * Time.deltaTime);
			if (base.transform.position.y < startPosition.y - 0.5f)
			{
				isLand = true;
			}
		}
	}

	private void MoveToPosition()
	{
		if (target == null)
		{
			Die();
			return;
		}
		if ((target.position - base.transform.position).magnitude > 2f)
		{
			base.transform.position = Vector3.MoveTowards(base.transform.position, target.position, moveSpeed * Time.deltaTime);
			return;
		}
		moveSpeed -= 20f * Time.deltaTime;
		base.transform.position = Vector3.MoveTowards(base.transform.position, target.position, moveSpeed * Time.deltaTime);
		base.transform.localScale -= new Vector3(5f * Time.deltaTime, 5f * Time.deltaTime, 5f * Time.deltaTime);
		if (base.transform.localScale.x < 0.3f)
		{
			GameAPP.board.GetComponent<Board>().theSun += sunPrice;
			Die();
		}
	}

	public void Die()
	{
		GameObject[] coinArray = GameAPP.board.GetComponent<Board>().coinArray;
		for (int i = 0; i < coinArray.Length; i++)
		{
			if (coinArray[i] == base.gameObject)
			{
				coinArray[i] = null;
				break;
			}
		}
		Object.Destroy(base.gameObject);
	}
}

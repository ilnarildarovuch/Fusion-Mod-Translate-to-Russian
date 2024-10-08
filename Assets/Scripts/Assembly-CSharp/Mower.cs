using UnityEngine;

public class Mower : MonoBehaviour
{
	public int theMowerRow;

	public int theMowerType;

	private bool isStart;

	private readonly float speed = 5f;

	private Rigidbody2D rb;

	private Animator anim;

	private int theBoxX;

	private bool inWater;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		Vector3 position = base.transform.position;
		position = new Vector3(position.x - 1f, position.y);
		Vector2 vector = Camera.main.WorldToScreenPoint(position);
		theBoxX = Mouse.Instance.GetColumnFromX(vector.x - 1f);
		if (Camera.main.WorldToViewportPoint(base.transform.position).x > 1f)
		{
			Die();
		}
		_ = theMowerType;
		_ = 1;
	}

	private void PoolCleanerUpdate()
	{
		if (GameAPP.theGameStatus == 0 && isStart && base.transform.position.x > -5.1f)
		{
			if (!inWater && Board.Instance.boxType[theBoxX, theMowerRow] == 1)
			{
				inWater = true;
				anim.SetTrigger("EnterWater");
				GameObject original = Resources.Load<GameObject>("Particle/Anim/Water/WaterSplashPrefab");
				Vector2 vector = base.transform.position;
				Object.Instantiate(original, vector, Quaternion.identity, GameAPP.board.transform);
				Object.Instantiate(position: new Vector2(vector.x, vector.y + 0.5f), original: GameAPP.particlePrefab[32], rotation: Quaternion.identity, parent: GameAPP.board.transform);
				GameAPP.PlaySound(71);
			}
			if (inWater && Board.Instance.boxType[theBoxX, theMowerRow] == 0)
			{
				inWater = false;
				anim.SetTrigger("BackToLand");
				GameAPP.PlaySound(71);
			}
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		GameObject gameObject = collision.gameObject;
		if (!gameObject.CompareTag("Zombie"))
		{
			return;
		}
		Zombie component = gameObject.GetComponent<Zombie>();
		if (component.theZombieRow == theMowerRow && !component.isMindControlled)
		{
			if (!isStart && component.theStatus != 1)
			{
				StartMove();
				isStart = true;
			}
			component.Die(1);
		}
	}

	private void StartMove()
	{
		if (theMowerType == 0)
		{
			anim.SetBool("isMoving", value: true);
		}
		else if (theMowerType == 1)
		{
			anim.SetFloat("Speed", 1f);
		}
		rb.velocity = new Vector2(speed, rb.velocity.y);
	}

	public void Die()
	{
		GameObject[] mowerArray = GameAPP.board.GetComponent<Board>().mowerArray;
		for (int i = 0; i < mowerArray.Length; i++)
		{
			if (mowerArray[i] == base.gameObject)
			{
				mowerArray[i] = null;
				break;
			}
		}
		Object.Destroy(base.gameObject, 0.5f);
	}
}

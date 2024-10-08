using UnityEngine;

public class DrownWeapon : MonoBehaviour
{
	public GameObject target;

	public float g = 10f;

	public float vx;

	public float vy = 4f;

	public float distanceX;

	public float distanceY;

	public int theRow;

	private float duringTime;

	private Plant targetPlant;

	private Vector3 velocity;

	private float existTime;

	private void Start()
	{
		if (target == null)
		{
			vx = -10f;
			vy = 5f;
		}
		else
		{
			targetPlant = target.GetComponent<Plant>();
			distanceX = Mathf.Abs(base.transform.position.x - targetPlant.shadow.transform.position.x);
			distanceY = Mathf.Abs(base.transform.position.y - targetPlant.shadow.transform.position.y);
			duringTime = (vy + Mathf.Sqrt(vy * vy + 2f * g * distanceY)) / g;
			vx = (0f - distanceX) / duringTime;
		}
		base.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = $"bullet{theRow}";
		base.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -10;
	}

	private void Update()
	{
		existTime += Time.deltaTime;
		if (existTime > 3f)
		{
			Object.Destroy(base.gameObject);
		}
		if (targetPlant != null)
		{
			if (base.transform.position.y > targetPlant.shadow.transform.position.y)
			{
				velocity = new Vector3(vx, vy, 0f);
				base.transform.position += velocity * Time.deltaTime;
				vy -= g * Time.deltaTime;
				float num = Mathf.Atan2(vy, 0f - vx) * 57.29578f;
				base.transform.localRotation = Quaternion.Euler(0f, 0f, 0f - num);
			}
		}
		else
		{
			velocity = new Vector3(vx, vy, 0f);
			base.transform.position += velocity * Time.deltaTime;
			vy -= g * Time.deltaTime;
			float num2 = Mathf.Atan2(vy, 0f - vx) * 57.29578f;
			base.transform.localRotation = Quaternion.Euler(0f, 0f, 0f - num2);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Plant>(out var component) && component.thePlantRow == theRow)
		{
			component.thePlantHealth -= Mathf.Max((int)(0.2 * (double)component.thePlantHealth), 20);
			component.FlashOnce();
			Die();
		}
	}

	private void Die()
	{
		Object.Destroy(base.gameObject);
	}
}

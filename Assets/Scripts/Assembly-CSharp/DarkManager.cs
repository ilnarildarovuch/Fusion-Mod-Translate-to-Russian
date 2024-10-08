using UnityEngine;

public class DarkManager : MonoBehaviour
{
	public bool isEnter;

	private SpriteRenderer r;

	private Color color = Color.black;

	private float existTime;

	private void Start()
	{
		r = GetComponent<SpriteRenderer>();
		if (!isEnter)
		{
			color.a = 0f;
		}
		else
		{
			color.a = 1f;
		}
	}

	private void Update()
	{
		existTime += Time.deltaTime;
		if (isEnter)
		{
			color.a -= Time.deltaTime;
			r.color = color;
		}
		else
		{
			color.a += 1.5f * Time.deltaTime;
			r.color = color;
		}
		if (existTime > 1f)
		{
			Object.Destroy(base.gameObject);
		}
	}
}

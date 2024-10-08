using UnityEngine;

public class IceExplodeControl : MonoBehaviour
{
	private SpriteRenderer r;

	private Color color;

	private void Start()
	{
		r = GetComponent<SpriteRenderer>();
		color = r.color;
	}

	private void FixedUpdate()
	{
		color.a -= 0.04f;
		r.color = color;
	}
}

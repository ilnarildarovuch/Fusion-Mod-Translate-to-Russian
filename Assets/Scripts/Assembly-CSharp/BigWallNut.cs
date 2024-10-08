using System.Collections.Generic;
using UnityEngine;

public class BigWallNut : Plant
{
	private readonly List<GameObject> zombie = new List<GameObject>();

	protected override void Start()
	{
		GameAPP.PlaySound(53);
		anim.Play("Round");
	}

	protected override void FixedUpdate()
	{
	}

	protected override void Update()
	{
		if (Camera.main.WorldToViewportPoint(base.transform.position).x > 1f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.TryGetComponent<Zombie>(out var component) || component.theZombieRow != thePlantRow || component.isMindControlled)
		{
			return;
		}
		foreach (GameObject item in zombie)
		{
			if (item == component.gameObject)
			{
				return;
			}
		}
		zombie.Add(component.gameObject);
		component.TakeDamage(10, 1800);
		GameAPP.PlaySound(Random.Range(54, 56));
		ScreenShake.TriggerShake(0.02f);
	}
}

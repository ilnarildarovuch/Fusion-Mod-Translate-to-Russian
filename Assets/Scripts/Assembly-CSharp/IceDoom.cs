using UnityEngine;

public class IceDoom : DoomShroom
{
	public override void AnimExplode()
	{
		Vector2 vector = new Vector2(shadow.transform.position.x - 0.3f, shadow.transform.position.y + 0.3f);
		board.iceDoomFreezeTime = 10f;
		Object.Instantiate(GameAPP.particlePrefab[29], vector, Quaternion.identity, board.transform).GetComponent<Doom>().theDoomType = 1;
		ScreenShake.TriggerShake();
		GameAPP.PlaySound(41);
		SetParticle();
		if (board.isNight)
		{
			GridItem.CreateGridItem(thePlantColumn, thePlantRow, 1);
		}
		else
		{
			GridItem.CreateGridItem(thePlantColumn, thePlantRow, 0);
		}
		GameObject[] plantArray = board.plantArray;
		foreach (GameObject gameObject in plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				if (component.thePlantRow == thePlantRow && component.thePlantColumn == thePlantColumn)
				{
					component.Die();
				}
			}
		}
		Die();
	}

	private void SetParticle()
	{
		GameObject original = Resources.Load<GameObject>("Particle/Prefabs/IceShroomExplode");
		Vector2 vector = shadow.transform.position;
		vector = new Vector2(vector.x, vector.y + 0.5f);
		Object.Instantiate(original, vector, Quaternion.identity, board.transform);
	}
}

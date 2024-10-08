using System;
using UnityEngine;

// Token: 0x02000069 RID: 105
public class IceDoom : DoomShroom
{
	// Token: 0x06000214 RID: 532 RVA: 0x00010F5C File Offset: 0x0000F15C
	public override void AnimExplode()
	{
		Vector2 v = new Vector2(this.shadow.transform.position.x - 0.3f, this.shadow.transform.position.y + 0.3f);
		this.board.iceDoomFreezeTime = 10f;
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[29], v, Quaternion.identity, this.board.transform).GetComponent<Doom>().theDoomType = 1;
		ScreenShake.TriggerShake(0.15f);
		GameAPP.PlaySound(41, 0.5f);
		this.SetParticle();
		if (this.board.isNight)
		{
			GridItem.CreateGridItem(this.thePlantColumn, this.thePlantRow, 1);
		}
		else
		{
			GridItem.CreateGridItem(this.thePlantColumn, this.thePlantRow, 0);
		}
		foreach (GameObject gameObject in this.board.plantArray)
		{
			if (gameObject != null)
			{
				Plant component = gameObject.GetComponent<Plant>();
				if (component.thePlantRow == this.thePlantRow && component.thePlantColumn == this.thePlantColumn)
				{
					component.Die();
				}
			}
		}
		this.Die();
	}

	// Token: 0x06000215 RID: 533 RVA: 0x00011090 File Offset: 0x0000F290
	private void SetParticle()
	{
		GameObject original = Resources.Load<GameObject>("Particle/Prefabs/IceShroomExplode");
		Vector2 vector = this.shadow.transform.position;
		vector = new Vector2(vector.x, vector.y + 0.5f);
		Object.Instantiate<GameObject>(original, vector, Quaternion.identity, this.board.transform);
	}
}

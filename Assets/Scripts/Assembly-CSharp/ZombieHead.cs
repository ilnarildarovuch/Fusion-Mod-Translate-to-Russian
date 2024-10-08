using System;
using UnityEngine;

// Token: 0x02000105 RID: 261
public class ZombieHead : MonoBehaviour
{
	// Token: 0x0600051D RID: 1309 RVA: 0x0002BA7C File Offset: 0x00029C7C
	private void OnParticleCollision(GameObject other)
	{
		ParticleSystem component = base.GetComponent<ParticleSystem>();
		component.rotationOverLifetime.enabled = false;
		ParticleSystem.MainModule main = component.main;
		main.startRotation = new ParticleSystem.MinMaxCurve(0f);
		main.startRotation3D = false;
		component.collision.bounce = 0f;
	}
}

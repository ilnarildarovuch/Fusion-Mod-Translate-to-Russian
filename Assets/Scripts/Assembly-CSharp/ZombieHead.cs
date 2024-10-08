using UnityEngine;

public class ZombieHead : MonoBehaviour
{
	private void OnParticleCollision(GameObject other)
	{
		ParticleSystem component = GetComponent<ParticleSystem>();
		ParticleSystem.RotationOverLifetimeModule rotationOverLifetime = component.rotationOverLifetime;
		rotationOverLifetime.enabled = false;
		ParticleSystem.MainModule main = component.main;
		main.startRotation = new ParticleSystem.MinMaxCurve(0f);
		main.startRotation3D = false;
		ParticleSystem.CollisionModule collision = component.collision;
		collision.bounce = 0f;
	}
}

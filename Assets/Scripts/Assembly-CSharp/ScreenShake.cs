using UnityEngine;

public class ScreenShake : MonoBehaviour
{
	private static Vector3 originalPosition;

	private static float shakeDuration = 0f;

	private static readonly float shakeMagnitude = 0.1f;

	private static readonly float dampingSpeed = 1f;

	private void Start()
	{
		originalPosition = base.transform.localPosition;
	}

	public void Update()
	{
		if (GameAPP.theGameStatus == 0)
		{
			if (shakeDuration > 0f)
			{
				base.transform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;
				shakeDuration -= Time.deltaTime * dampingSpeed;
			}
			else
			{
				shakeDuration = 0f;
				base.transform.localPosition = originalPosition;
			}
		}
	}

	public static void TriggerShake(float duration = 0.15f)
	{
		shakeDuration = duration;
	}
}

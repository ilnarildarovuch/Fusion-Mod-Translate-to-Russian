using UnityEngine;

public class SoundCtrl : MonoBehaviour
{
	public int theSoundID;

	public float existTime;

	private void Update()
	{
		existTime += Time.deltaTime;
	}

	private void Awake()
	{
		Invoke("Die", GetComponent<AudioSource>().clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));
	}

	private void Die()
	{
		GameAPP.sound.Remove(this);
		Object.Destroy(base.gameObject);
	}
}

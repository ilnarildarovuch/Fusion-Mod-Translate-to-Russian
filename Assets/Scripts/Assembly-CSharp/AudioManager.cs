using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	private static AudioManager instance;

	private AudioSource audioSource;

	public static AudioManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Object.FindObjectOfType<AudioManager>();
				if (instance == null)
				{
					instance = new GameObject
					{
						name = "AudioManager"
					}.AddComponent<AudioManager>();
				}
			}
			return instance;
		}
	}

	private void Awake()
	{
		if (!TryGetComponent<AudioSource>(out audioSource))
		{
			audioSource = base.gameObject.AddComponent<AudioSource>();
		}
	}

	public void PlaySound(AudioClip clip, float volume = 1f, float pitch = 1f)
	{
		StartCoroutine(PlaySoundCoroutine(clip, volume, pitch));
	}

	private IEnumerator PlaySoundCoroutine(AudioClip clip, float volume, float pitch)
	{
		AudioSource source = base.gameObject.AddComponent<AudioSource>();
		source.clip = clip;
		source.volume = volume * GameAPP.gameSoundVolume;
		source.pitch = pitch;
		source.Play();
		yield return new WaitForSeconds(clip.length);
		Object.Destroy(source);
	}
}

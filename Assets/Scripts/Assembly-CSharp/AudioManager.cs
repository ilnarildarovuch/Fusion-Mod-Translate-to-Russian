using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class AudioManager : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x060000A6 RID: 166 RVA: 0x00005508 File Offset: 0x00003708
	public static AudioManager Instance
	{
		get
		{
			if (AudioManager.instance == null)
			{
				AudioManager.instance = Object.FindObjectOfType<AudioManager>();
				if (AudioManager.instance == null)
				{
					AudioManager.instance = new GameObject
					{
						name = "AudioManager"
					}.AddComponent<AudioManager>();
				}
			}
			return AudioManager.instance;
		}
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00005558 File Offset: 0x00003758
	private void Awake()
	{
		if (!base.TryGetComponent<AudioSource>(out this.audioSource))
		{
			this.audioSource = base.gameObject.AddComponent<AudioSource>();
		}
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00005579 File Offset: 0x00003779
	public void PlaySound(AudioClip clip, float volume = 1f, float pitch = 1f)
	{
		base.StartCoroutine(this.PlaySoundCoroutine(clip, volume, pitch));
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x0000558B File Offset: 0x0000378B
	private IEnumerator PlaySoundCoroutine(AudioClip clip, float volume, float pitch)
	{
		AudioSource source = base.gameObject.AddComponent<AudioSource>();
		source.clip = clip;
		source.volume = volume * GameAPP.gameSoundVolume;
		source.pitch = pitch;
		source.Play();
		yield return new WaitForSeconds(clip.length);
		Object.Destroy(source);
		yield break;
	}

	// Token: 0x0400008F RID: 143
	private static AudioManager instance;

	// Token: 0x04000090 RID: 144
	private AudioSource audioSource;
}

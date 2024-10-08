using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
	private Slider slider;

	private void Start()
	{
		slider = GetComponent<Slider>();
		slider.value = GameAPP.gameSoundVolume;
	}

	private void Update()
	{
		GameAPP.gameSoundVolume = slider.value;
	}
}

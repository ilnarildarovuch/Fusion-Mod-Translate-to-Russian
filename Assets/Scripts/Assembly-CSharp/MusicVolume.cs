using UnityEngine;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
	private Slider slider;

	private void Start()
	{
		slider = GetComponent<Slider>();
		slider.value = GameAPP.gameMusicVolume;
	}

	private void Update()
	{
		GameAPP.gameMusicVolume = slider.value;
	}
}

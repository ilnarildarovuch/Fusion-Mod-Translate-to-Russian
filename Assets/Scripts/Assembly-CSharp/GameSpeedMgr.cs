using UnityEngine;
using UnityEngine.UI;

public class GameSpeedMgr : MonoBehaviour
{
	private Slider slider;

	private void Start()
	{
		slider = GetComponent<Slider>();
		slider.value = GameAPP.gameSpeed;
	}

	private void Update()
	{
		GameAPP.gameSpeed = (int)slider.value;
	}
}

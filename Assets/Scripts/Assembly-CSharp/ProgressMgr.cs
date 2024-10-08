using UnityEngine;
using UnityEngine.UI;

public class ProgressMgr : MonoBehaviour
{
	public static Slider slider;

	public static Board bg;

	private void Awake()
	{
		slider = GetComponent<Slider>();
		bg = GameAPP.board.GetComponent<Board>();
	}

	private void Update()
	{
		float num = bg.theWave;
		float num2 = bg.theMaxWave;
		float num3 = num / num2;
		if (slider.value < num3)
		{
			slider.value += Time.deltaTime * 0.1f;
		}
	}
}

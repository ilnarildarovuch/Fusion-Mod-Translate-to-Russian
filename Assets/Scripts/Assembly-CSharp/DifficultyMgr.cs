using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyMgr : MonoBehaviour
{
	private Slider slider;

	private TextMeshProUGUI t;

	private void Start()
	{
		slider = GetComponent<Slider>();
		slider.value = GameAPP.difficulty;
		t = base.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		GameAPP.difficulty = (int)slider.value;
		float value = slider.value;
		if (value <= 2f)
		{
			if (value != 1f)
			{
				if (value == 2f)
				{
					t.text = "正常模式";
				}
			}
			else
			{
				t.text = "简单模式";
			}
		}
		else if (value != 3f)
		{
			if (value != 4f)
			{
				if (value == 5f)
				{
					t.text = "你确定？";
				}
			}
			else
			{
				t.text = "极难模式";
			}
		}
		else
		{
			t.text = "困难模式";
		}
	}
}

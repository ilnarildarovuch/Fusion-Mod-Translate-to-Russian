using TMPro;
using UnityEngine;

public class TravelMenuMgr : MonoBehaviour
{
	public static TravelMenuMgr Instance;

	public TextMeshProUGUI textShadow;

	public TextMeshProUGUI text;

	public TravelMenuBtn btn;

	private void Awake()
	{
		Instance = this;
	}

	public void ChangeText(int type)
	{
		string text;
		if (type == 1)
		{
			text = "你可以获得随机1种究极植物\r\n但你将面对更强大的僵尸";
			btn.choiceNumber = 1;
		}
		else
		{
			text = "数据异常";
			Debug.LogError(type);
		}
		textShadow.text = text;
		this.text.text = text;
	}
}

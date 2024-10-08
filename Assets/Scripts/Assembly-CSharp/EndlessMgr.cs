using TMPro;
using UnityEngine;

public class EndlessMgr : MonoBehaviour
{
	private void Start()
	{
		if (PlantsInLevel.maxRound > 0)
		{
			base.transform.GetChild(2).gameObject.SetActive(value: true);
			base.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"{PlantsInLevel.maxRound}轮";
		}
	}
}

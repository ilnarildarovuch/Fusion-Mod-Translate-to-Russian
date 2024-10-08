using TMPro;
using UnityEngine;

public class UIDifficulty : MonoBehaviour
{
	private TextMeshProUGUI t;

	private void Start()
	{
		t = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		t.text = $"难度：{GameAPP.difficulty}";
	}
}

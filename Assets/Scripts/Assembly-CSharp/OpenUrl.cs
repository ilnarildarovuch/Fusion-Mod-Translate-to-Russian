using System.Collections;
using UnityEngine;

public class OpenUrl : MonoBehaviour
{
	private readonly string url = "https://space.bilibili.com/3546619314178489";

	private readonly string url2 = "https://space.bilibili.com/85881762";

	public int type;

	private void OnMouseUp()
	{
		StartCoroutine(OpenURLCoroutine());
	}

	private IEnumerator OpenURLCoroutine()
	{
		yield return null;
		if (type == 0)
		{
			Application.OpenURL(url);
		}
		else
		{
			Application.OpenURL(url2);
		}
	}
}

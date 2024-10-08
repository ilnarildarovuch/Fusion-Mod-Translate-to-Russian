using UnityEngine;

public class ClgLevelMgr : MonoBehaviour
{
	public int currentPage;

	public void ChangePage(int page)
	{
		currentPage = page;
		foreach (Transform item in base.transform)
		{
			if (item.GetSiblingIndex() == page)
			{
				item.gameObject.SetActive(value: true);
			}
			else
			{
				item.gameObject.SetActive(value: false);
			}
		}
	}
}

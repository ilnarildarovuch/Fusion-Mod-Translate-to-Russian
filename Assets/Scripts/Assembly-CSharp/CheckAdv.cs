using UnityEngine;

public class CheckAdv : MonoBehaviour
{
	private int theLevel;

	private void Start()
	{
		theLevel = base.transform.GetChild(1).GetComponent<Advanture_Btn>().buttonNumber;
		if (theLevel > 1 && !GameAPP.developerMode && !GameAPP.advLevelCompleted[theLevel - 1])
		{
			base.gameObject.SetActive(value: false);
		}
	}
}

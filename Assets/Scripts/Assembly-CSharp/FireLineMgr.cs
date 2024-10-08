using UnityEngine;

public class FireLineMgr : MonoBehaviour
{
	private float fadeTime;

	public bool isMgr;

	private readonly GameObject[] fireArray = new GameObject[15];

	private float speed = 15f;

	public int theFireRow = -1;

	private void Start()
	{
		for (int i = 0; i < fireArray.Length; i++)
		{
			fireArray[i] = base.transform.GetChild(i).gameObject;
		}
	}

	private void Update()
	{
		fadeTime += speed * Time.deltaTime;
		if (fadeTime > speed * 0.4f)
		{
			int num = (int)(fadeTime - speed * 0.4f);
			if (num < fireArray.Length)
			{
				fireArray[num].GetComponent<Animator>().SetTrigger("fade");
			}
		}
		if (fadeTime > speed * 2f && isMgr)
		{
			Die();
		}
	}

	private void Die()
	{
		Board.Instance.fireLineArray[theFireRow] = null;
		Object.Destroy(base.gameObject);
	}
}

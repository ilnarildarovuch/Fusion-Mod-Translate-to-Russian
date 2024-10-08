using UnityEngine;
using UnityEngine.UI;

public class FlagMgr : MonoBehaviour
{
	private int flag;

	private bool once;

	private GridLayoutGroup grid;

	public RectTransform flag1;

	public RectTransform flag2;

	public RectTransform flag3;

	public RectTransform flag4;

	public RectTransform flag5;

	public RectTransform flag6;

	public RectTransform flag7;

	public RectTransform flag8;

	public RectTransform flag9;

	public RectTransform flag10;

	private int wave;

	private void Start()
	{
		grid = GetComponent<GridLayoutGroup>();
	}

	private void Update()
	{
		FlagUpdate();
		if (!once && ProgressMgr.bg != null)
		{
			once = true;
			flag = ProgressMgr.bg.theMaxWave / 10;
			switch (flag)
			{
			case 1:
				grid.spacing = new Vector2(140f, 0f);
				Object.Destroy(base.transform.GetChild(0).gameObject);
				Object.Destroy(base.transform.GetChild(1).gameObject);
				Object.Destroy(base.transform.GetChild(2).gameObject);
				Object.Destroy(base.transform.GetChild(3).gameObject);
				Object.Destroy(base.transform.GetChild(4).gameObject);
				Object.Destroy(base.transform.GetChild(5).gameObject);
				Object.Destroy(base.transform.GetChild(6).gameObject);
				Object.Destroy(base.transform.GetChild(7).gameObject);
				Object.Destroy(base.transform.GetChild(8).gameObject);
				break;
			case 2:
				grid.spacing = new Vector2(70f, 0f);
				Object.Destroy(base.transform.GetChild(0).gameObject);
				Object.Destroy(base.transform.GetChild(1).gameObject);
				Object.Destroy(base.transform.GetChild(2).gameObject);
				Object.Destroy(base.transform.GetChild(3).gameObject);
				Object.Destroy(base.transform.GetChild(4).gameObject);
				Object.Destroy(base.transform.GetChild(5).gameObject);
				Object.Destroy(base.transform.GetChild(6).gameObject);
				Object.Destroy(base.transform.GetChild(7).gameObject);
				break;
			case 3:
				grid.spacing = new Vector2(47f, 0f);
				Object.Destroy(base.transform.GetChild(0).gameObject);
				Object.Destroy(base.transform.GetChild(1).gameObject);
				Object.Destroy(base.transform.GetChild(2).gameObject);
				Object.Destroy(base.transform.GetChild(3).gameObject);
				Object.Destroy(base.transform.GetChild(4).gameObject);
				Object.Destroy(base.transform.GetChild(5).gameObject);
				Object.Destroy(base.transform.GetChild(6).gameObject);
				break;
			case 4:
				grid.spacing = new Vector2(35f, 0f);
				Object.Destroy(base.transform.GetChild(0).gameObject);
				Object.Destroy(base.transform.GetChild(1).gameObject);
				Object.Destroy(base.transform.GetChild(2).gameObject);
				Object.Destroy(base.transform.GetChild(3).gameObject);
				Object.Destroy(base.transform.GetChild(4).gameObject);
				Object.Destroy(base.transform.GetChild(5).gameObject);
				break;
			case 5:
				grid.spacing = new Vector2(26f, 0f);
				Object.Destroy(base.transform.GetChild(0).gameObject);
				Object.Destroy(base.transform.GetChild(1).gameObject);
				Object.Destroy(base.transform.GetChild(2).gameObject);
				Object.Destroy(base.transform.GetChild(3).gameObject);
				Object.Destroy(base.transform.GetChild(4).gameObject);
				break;
			case 6:
				grid.spacing = new Vector2(21.7f, 0f);
				Object.Destroy(base.transform.GetChild(0).gameObject);
				Object.Destroy(base.transform.GetChild(1).gameObject);
				Object.Destroy(base.transform.GetChild(2).gameObject);
				Object.Destroy(base.transform.GetChild(3).gameObject);
				break;
			case 7:
				grid.spacing = new Vector2(28.6f, 0f);
				Object.Destroy(base.transform.GetChild(0).gameObject);
				Object.Destroy(base.transform.GetChild(1).gameObject);
				Object.Destroy(base.transform.GetChild(2).gameObject);
				break;
			case 8:
				grid.spacing = new Vector2(16.25f, 0f);
				Object.Destroy(base.transform.GetChild(0).gameObject);
				Object.Destroy(base.transform.GetChild(1).gameObject);
				break;
			case 9:
				grid.spacing = new Vector2(14.4f, 0f);
				Object.Destroy(base.transform.GetChild(0).gameObject);
				break;
			case 10:
				grid.spacing = new Vector2(13f, 0f);
				break;
			}
		}
	}

	private void FlagUpdate()
	{
		wave = ProgressMgr.bg.theWave;
		switch (wave)
		{
		case 10:
			flag10.anchoredPosition = new Vector2(10f, 5f);
			break;
		case 20:
			flag9.anchoredPosition = new Vector2(10f, 5f);
			break;
		case 30:
			flag8.anchoredPosition = new Vector2(10f, 5f);
			break;
		case 40:
			flag7.anchoredPosition = new Vector2(10f, 5f);
			break;
		case 50:
			flag6.anchoredPosition = new Vector2(10f, 5f);
			break;
		case 60:
			flag5.anchoredPosition = new Vector2(10f, 5f);
			break;
		case 70:
			flag4.anchoredPosition = new Vector2(10f, 5f);
			break;
		case 80:
			flag3.anchoredPosition = new Vector2(10f, 5f);
			break;
		case 90:
			flag2.anchoredPosition = new Vector2(10f, 5f);
			break;
		case 100:
			flag1.anchoredPosition = new Vector2(10f, 5f);
			break;
		}
	}
}

using UnityEngine;

public class NutChomper : Chomper
{
	private Sprite backCrack1;

	private Sprite backCrack2;

	private Sprite headCrack1;

	private Sprite headCrack2;

	private Sprite originBack;

	private Sprite originHead;

	private GameObject back;

	private GameObject head;

	private bool cracked1;

	private bool cracked2;

	protected override void Start()
	{
		base.Start();
		backCrack1 = Resources.Load<Sprite>("Plants/_Mixer/NutChomper/cracked1_back");
		backCrack2 = Resources.Load<Sprite>("Plants/_Mixer/NutChomper/cracked2_back");
		headCrack1 = Resources.Load<Sprite>("Plants/_Mixer/NutChomper/cracked1_head");
		headCrack2 = Resources.Load<Sprite>("Plants/_Mixer/NutChomper/cracked2_head");
		back = base.transform.Find("Wallnut_body").gameObject;
		head = base.transform.Find("head").gameObject;
		originBack = back.GetComponent<SpriteRenderer>().sprite;
		originHead = head.GetComponent<SpriteRenderer>().sprite;
	}

	protected override void Update()
	{
		base.Update();
		ReplaceSprite();
	}

	protected override void Swallow()
	{
		base.Swallow();
		Recover(1000);
	}

	private void ReplaceSprite()
	{
		if (thePlantHealth > thePlantMaxHealth * 2 / 3)
		{
			head.GetComponent<SpriteRenderer>().sprite = originHead;
			back.GetComponent<SpriteRenderer>().sprite = originBack;
			cracked1 = false;
			cracked2 = false;
		}
		if (thePlantHealth > thePlantMaxHealth / 3 && thePlantHealth < thePlantMaxHealth * 2 / 3)
		{
			head.GetComponent<SpriteRenderer>().sprite = headCrack1;
			back.GetComponent<SpriteRenderer>().sprite = backCrack1;
			cracked2 = false;
			if (!cracked1)
			{
				Object.Instantiate(GameAPP.particlePrefab[13], head.transform.position, Quaternion.identity).transform.SetParent(board.gameObject.transform);
				cracked1 = true;
			}
		}
		if (thePlantHealth < thePlantMaxHealth / 3)
		{
			head.GetComponent<SpriteRenderer>().sprite = headCrack2;
			back.GetComponent<SpriteRenderer>().sprite = backCrack2;
			if (!cracked2)
			{
				Object.Instantiate(GameAPP.particlePrefab[13], head.transform.position, Quaternion.identity).transform.SetParent(board.gameObject.transform);
				cracked2 = true;
			}
		}
	}
}

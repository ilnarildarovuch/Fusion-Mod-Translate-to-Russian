using UnityEngine;

public class InputMgr : MonoBehaviour
{
	public int itemTypeOnHand = -1;

	public int theCardTypeOnHand = -1;

	public GameObject thePlantSelectByGlove;

	public GameObject thePlantSelectByShovel;

	private void Update()
	{
		HandleInput();
	}

	private void HandleInput()
	{
		Input.GetMouseButtonDown(0);
	}
}

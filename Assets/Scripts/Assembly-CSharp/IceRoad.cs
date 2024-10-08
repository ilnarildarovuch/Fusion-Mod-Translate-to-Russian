using UnityEngine;

public class IceRoad : MonoBehaviour
{
	public int theIceRoadRow;

	private GameObject iceCap;

	private void Awake()
	{
		iceCap = base.transform.GetChild(0).gameObject;
	}

	private void FixedUpdate()
	{
		iceCap.transform.position = new Vector3(Board.Instance.iceRoadX[theIceRoadRow], iceCap.transform.position.y);
	}
}

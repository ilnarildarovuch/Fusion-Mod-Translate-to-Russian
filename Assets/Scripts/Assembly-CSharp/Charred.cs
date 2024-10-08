using UnityEngine;

public class Charred : MonoBehaviour
{
	public void Die()
	{
		Object.Destroy(base.gameObject, 0.2f);
	}
}

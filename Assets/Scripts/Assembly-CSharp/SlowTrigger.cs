using UnityEngine;

public class SlowTrigger : MonoBehaviour
{
	public static SlowTrigger Instance;

	private void Awake()
	{
		Instance = this;
	}
}

using UnityEngine;

public class LevelName3 : MonoBehaviour
{
	public static LevelName3 Instance;

	private void Awake()
	{
		Instance = this;
	}
}

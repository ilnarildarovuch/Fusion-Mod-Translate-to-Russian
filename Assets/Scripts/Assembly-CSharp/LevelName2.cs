using UnityEngine;

public class LevelName2 : MonoBehaviour
{
	public static LevelName2 Instance;

	private void Awake()
	{
		Instance = this;
	}
}

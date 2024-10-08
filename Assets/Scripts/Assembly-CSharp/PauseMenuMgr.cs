using UnityEngine;

public class PauseMenuMgr : MonoBehaviour
{
	public static PauseMenuMgr Instance;

	public GameObject checkQuit;

	public GameObject checkRestart;

	public GameObject btnRestart;

	public GameObject btnQuit;

	public GameObject backToGame;

	public bool isRecheck;

	private void Awake()
	{
		Instance = this;
	}
}

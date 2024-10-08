using UnityEngine;

public class ZombieInAlmanac : MonoBehaviour
{
	private void Start()
	{
		GetComponent<Animator>().Play("idle");
		GetComponent<Animator>().SetFloat("Speed", 1.3f);
	}
}

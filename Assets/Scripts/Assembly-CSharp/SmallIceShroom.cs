using UnityEngine;

public class SmallIceShroom : Plant
{
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.TryGetComponent<Zombie>(out var component) && component.theZombieRow == thePlantRow && component.theStatus != 1 && !component.isMindControlled && component.theFreezeCountDown == 0f && (!component.TryGetComponent<PolevaulterZombie>(out var component2) || component2.polevaulterStatus != 1))
		{
			component.SetFreeze(4f);
			GameAPP.PlaySound(67);
			Object.Instantiate(GameAPP.particlePrefab[24], shadow.transform.position, Quaternion.identity, board.transform);
			Die();
		}
	}
}

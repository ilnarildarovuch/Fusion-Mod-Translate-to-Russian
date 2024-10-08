using UnityEngine;

public class DancePolevaulterZombie : PolevaulterZombie
{
	public override void JumpOver()
	{
		base.JumpOver();
		if (!(shadow != null) || theStatus != 0)
		{
			return;
		}
		GameObject gameObject = board.GetComponent<CreateZombie>().SetZombie(0, theZombieRow, 7, shadow.transform.position.x + 1.5f);
		CreateParticle(gameObject.transform.position);
		GameObject gameObject2 = board.GetComponent<CreateZombie>().SetZombie(0, theZombieRow, 7, shadow.transform.position.x - 1.5f);
		CreateParticle(gameObject2.transform.position);
		if (!board.isEveStarted)
		{
			if (theZombieRow > 0 && board.roadType[theZombieRow - 1] != 1)
			{
				GameObject gameObject3 = board.GetComponent<CreateZombie>().SetZombie(0, theZombieRow - 1, 7, shadow.transform.position.x);
				CreateParticle(gameObject3.transform.position);
			}
			if (theZombieRow < board.roadNum - 1 && board.roadType[theZombieRow + 1] != 1)
			{
				GameObject gameObject4 = board.GetComponent<CreateZombie>().SetZombie(0, theZombieRow + 1, 7, shadow.transform.position.x);
				CreateParticle(gameObject4.transform.position);
			}
		}
	}

	private void CreateParticle(Vector3 position)
	{
		Object.Instantiate(position: new Vector3(position.x, position.y + 0.5f), original: GameAPP.particlePrefab[11], rotation: Quaternion.identity, parent: board.transform);
	}
}

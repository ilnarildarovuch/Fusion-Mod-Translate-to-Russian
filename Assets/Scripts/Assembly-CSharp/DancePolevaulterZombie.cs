using System;
using UnityEngine;

// Token: 0x020000E5 RID: 229
public class DancePolevaulterZombie : PolevaulterZombie
{
	// Token: 0x06000426 RID: 1062 RVA: 0x000206EC File Offset: 0x0001E8EC
	public override void JumpOver()
	{
		base.JumpOver();
		if (this.shadow != null && this.theStatus == 0)
		{
			GameObject gameObject = this.board.GetComponent<CreateZombie>().SetZombie(0, this.theZombieRow, 7, this.shadow.transform.position.x + 1.5f, false);
			this.CreateParticle(gameObject.transform.position);
			GameObject gameObject2 = this.board.GetComponent<CreateZombie>().SetZombie(0, this.theZombieRow, 7, this.shadow.transform.position.x - 1.5f, false);
			this.CreateParticle(gameObject2.transform.position);
			if (!this.board.isEveStarted)
			{
				if (this.theZombieRow > 0 && this.board.roadType[this.theZombieRow - 1] != 1)
				{
					GameObject gameObject3 = this.board.GetComponent<CreateZombie>().SetZombie(0, this.theZombieRow - 1, 7, this.shadow.transform.position.x, false);
					this.CreateParticle(gameObject3.transform.position);
				}
				if (this.theZombieRow < this.board.roadNum - 1 && this.board.roadType[this.theZombieRow + 1] != 1)
				{
					GameObject gameObject4 = this.board.GetComponent<CreateZombie>().SetZombie(0, this.theZombieRow + 1, 7, this.shadow.transform.position.x, false);
					this.CreateParticle(gameObject4.transform.position);
				}
			}
		}
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x00020888 File Offset: 0x0001EA88
	private void CreateParticle(Vector3 position)
	{
		Vector3 position2 = new Vector3(position.x, position.y + 0.5f);
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[11], position2, Quaternion.identity, this.board.transform);
	}
}

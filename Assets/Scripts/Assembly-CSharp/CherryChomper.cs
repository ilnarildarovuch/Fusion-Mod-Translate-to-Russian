using System;
using UnityEngine;

// Token: 0x02000071 RID: 113
public class CherryChomper : Chomper
{
	// Token: 0x0600025D RID: 605 RVA: 0x00013E94 File Offset: 0x00012094
	public override void BiteEvent()
	{
		if (this.zombie != null)
		{
			Zombie component = this.zombie.GetComponent<Zombie>();
			Collider2D[] colliders = this.colliders;
			int i = 0;
			while (i < colliders.Length)
			{
				Collider2D collider2D = colliders[i];
				if (component.theAttackTarget == base.gameObject)
				{
					this.Explode(this.zombie);
					return;
				}
				if (!(collider2D == null) && collider2D.gameObject == this.zombie)
				{
					PolevaulterZombie polevaulterZombie;
					if (this.zombie.TryGetComponent<PolevaulterZombie>(out polevaulterZombie) && polevaulterZombie.polevaulterStatus != 2)
					{
						GameAPP.PlaySound(49, 0.5f);
						this.zombie = null;
						this.anim.SetTrigger("back");
						return;
					}
					if (component.theStatus == 1 || component.isMindControlled)
					{
						GameAPP.PlaySound(49, 0.5f);
						this.zombie = null;
						this.anim.SetTrigger("back");
						return;
					}
					this.Explode(this.zombie);
					return;
				}
				else
				{
					i++;
				}
			}
		}
		this.zombie = null;
		this.anim.SetTrigger("back");
		GameAPP.PlaySound(49, 0.5f);
	}

	// Token: 0x0600025E RID: 606 RVA: 0x00013FC0 File Offset: 0x000121C0
	private void Explode(GameObject _zombie)
	{
		_zombie.GetComponent<Zombie>().Die(2);
		this.attributeCountdown = this.swallowMaxCountDown;
		this.canToChew = true;
		this.zombie = null;
		GameObject gameObject = GameAPP.particlePrefab[2];
		Vector3 position = new Vector3(base.transform.position.x + 1.5f, base.transform.position.y + 0.5f, 0f);
		GameObject gameObject2 = Object.Instantiate<GameObject>(gameObject, position, Quaternion.identity);
		gameObject2.transform.SetParent(GameAPP.board.transform);
		gameObject2.name = gameObject.name;
		gameObject2.GetComponent<BombCherry>().bombRow = this.thePlantRow;
		ScreenShake.TriggerShake(0.02f);
		GameAPP.PlaySound(40, 0.5f);
	}
}

using System;
using Unity.VisualScripting;
using UnityEngine;

// Token: 0x020000EF RID: 239
public class SuperCherryZ : PeaShooterZ
{
	// Token: 0x0600046B RID: 1131 RVA: 0x000234BC File Offset: 0x000216BC
	public override GameObject AnimShoot()
	{
		if (this.theStatus != 0)
		{
			return null;
		}
		Vector3 position = base.transform.Find("Zombie_head").GetChild(0).transform.position;
		float theX;
		if (!this.isMindControlled)
		{
			theX = position.x - 0.1f;
		}
		else
		{
			theX = position.x + 0.1f;
		}
		float y = position.y;
		int theZombieRow = this.theZombieRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, theZombieRow, 3, 0);
		Vector3 position2 = gameObject.transform.GetChild(0).transform.position;
		gameObject.transform.GetChild(0).transform.position = new Vector3(position2.x, position2.y - 0.5f, position2.z);
		if (!this.isMindControlled)
		{
			gameObject.GetComponent<Bullet>().isZombieBullet = true;
		}
		gameObject.GetComponent<Bullet>().theBulletDamage = 0;
		GameAPP.PlaySound(Random.Range(3, 5), 0.5f);
		return gameObject;
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x000235C4 File Offset: 0x000217C4
	protected override void BodyTakeDamage(int theDamage)
	{
		this.theHealth -= (float)theDamage;
		if (!this.isLoseHand && this.theHealth < (float)(this.theMaxHealth * 2 / 3))
		{
			this.isLoseHand = true;
			GameAPP.PlaySound(7, 0.5f);
			for (int i = 0; i < base.transform.childCount; i++)
			{
				Transform child = base.transform.GetChild(i);
				if (child.CompareTag("ZombieHand"))
				{
					Object.Destroy(child.gameObject);
				}
				if (child.CompareTag("ZombieArmUpper"))
				{
					child.GetComponent<SpriteRenderer>().sprite = GameAPP.spritePrefab[0];
					child.transform.localScale = new Vector3(4f, 4f, 4f);
				}
				if (child.name == "LoseArm")
				{
					child.gameObject.SetActive(true);
					child.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = string.Format("zombie{0}", this.theZombieRow);
					child.gameObject.GetComponent<ParticleSystemRenderer>().sortingOrder += this.baseLayer + 29;
					child.gameObject.GetComponent<ParticleSystem>().collision.AddPlane(this.board.transform.GetChild(2 + this.theZombieRow));
					child.AddComponent<ZombieHead>();
				}
			}
		}
		if (this.theHealth < (float)(this.theMaxHealth / 3) && this.theStatus != 1)
		{
			this.theStatus = 1;
			for (int j = 0; j < base.transform.childCount; j++)
			{
				Transform child2 = base.transform.GetChild(j);
				if (child2.CompareTag("ZombieHead"))
				{
					Object.Destroy(child2.gameObject);
				}
			}
		}
	}
}

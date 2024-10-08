using System;
using UnityEngine;

// Token: 0x020000BA RID: 186
public class ScaredFume : Shooter
{
	// Token: 0x0600037D RID: 893 RVA: 0x0001B278 File Offset: 0x00019478
	public override GameObject AnimShoot()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		float theX = position.x + 0.1f;
		float y = position.y;
		int thePlantRow = this.thePlantRow;
		GameObject gameObject = this.board.GetComponent<CreateBullet>().SetBullet(theX, y, thePlantRow, 9, 0);
		gameObject.GetComponent<Bullet>().theBulletDamage = 20;
		GameAPP.PlaySound(57, 0.5f);
		return gameObject;
	}

	// Token: 0x0600037E RID: 894 RVA: 0x0001B2E8 File Offset: 0x000194E8
	public void AttackZombie()
	{
		bool flag = false;
		foreach (GameObject gameObject in this.board.zombieArray)
		{
			if (gameObject != null)
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (component.shadow.transform.position.x <= this.shadow.transform.position.x + 7f && component.shadow.transform.position.x >= this.shadow.transform.position.x && base.SearchUniqueZombie(component) && component.theZombieRow == this.thePlantRow)
				{
					this.zombieList.Add(component);
					flag = true;
				}
			}
		}
		for (int i = this.zombieList.Count - 1; i >= 0; i--)
		{
			if (this.zombieList[i] != null)
			{
				this.zombieList[i].TakeDamage(1, 20);
			}
		}
		this.zombieList.Clear();
		if (flag)
		{
			GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
		}
	}

	// Token: 0x0600037F RID: 895 RVA: 0x0001B43C File Offset: 0x0001963C
	public void AnimShootFume()
	{
		Vector3 position = base.transform.Find("Shoot").transform.position;
		Object.Instantiate<GameObject>(GameAPP.particlePrefab[19], position, Quaternion.Euler(0f, 90f, 0f), this.board.transform).GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = string.Format("particle{0}", this.thePlantRow);
		GameAPP.PlaySound(58, 0.5f);
		this.AttackZombie();
	}

	// Token: 0x06000380 RID: 896 RVA: 0x0001B4C8 File Offset: 0x000196C8
	protected override void PlantShootUpdate()
	{
		this.thePlantAttackCountDown -= Time.deltaTime;
		if (this.thePlantAttackCountDown < 0f)
		{
			this.thePlantAttackCountDown = this.thePlantAttackInterval;
			if (this.SearchZombie() != null)
			{
				if (this.shootType == 0)
				{
					this.anim.Play("shoot", 1);
					return;
				}
				this.anim.SetTrigger("shoot1");
			}
		}
	}

	// Token: 0x06000381 RID: 897 RVA: 0x0001B538 File Offset: 0x00019738
	protected override GameObject SearchZombie()
	{
		Zombie zombie = null;
		float num = float.MaxValue;
		foreach (GameObject gameObject in this.board.GetComponent<Board>().zombieArray)
		{
			if (gameObject != null)
			{
				Zombie component = gameObject.GetComponent<Zombie>();
				if (component.theZombieRow == this.thePlantRow && component.shadow.transform.position.x < 9.2f && component.shadow.transform.position.x > this.shadow.transform.position.x && base.SearchUniqueZombie(component) && component.shadow.transform.position.x < num)
				{
					num = component.shadow.transform.position.x;
					zombie = component;
				}
			}
		}
		if (zombie == null)
		{
			return null;
		}
		if (zombie.shadow.transform.position.x > this.shadow.transform.position.x && zombie.shadow.transform.position.x < this.shadow.transform.position.x + 7f)
		{
			this.shootType = 1;
			return zombie.gameObject;
		}
		if (zombie.shadow.transform.position.x > this.shadow.transform.position.x + 7f)
		{
			this.shootType = 0;
			return zombie.gameObject;
		}
		return null;
	}

	// Token: 0x040001C8 RID: 456
	private int shootType;
}

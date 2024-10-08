using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000031 RID: 49
public class Bullet : MonoBehaviour
{
	// Token: 0x060000FE RID: 254 RVA: 0x00008728 File Offset: 0x00006928
	protected virtual void Awake()
	{
		if (base.transform.Find("Shadow") != null)
		{
			this.shadow = base.transform.Find("Shadow").gameObject;
		}
		this.zombieLayer = LayerMask.GetMask(new string[]
		{
			"Zombie"
		});
		this.startPosition = base.transform.position;
		this.mainCamara = Camera.main;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x000087A2 File Offset: 0x000069A2
	protected virtual void Start()
	{
		this.SetShadowPosition();
	}

	// Token: 0x06000100 RID: 256 RVA: 0x000087AC File Offset: 0x000069AC
	protected void SetShadowPosition()
	{
		if (this.shadow != null)
		{
			if (this.isShort)
			{
				this.shadow.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - 0.3f);
				return;
			}
			int num = this.theMovingWay;
			if (num <= 1 || num == 3)
			{
				this.shadow.transform.position = new Vector3(this.shadow.transform.position.x, Mouse.Instance.GetBoxYFromRow(this.theBulletRow) + 0.2f);
				return;
			}
			this.shadow.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - 0.6f);
		}
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00008899 File Offset: 0x00006A99
	public virtual void Die()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x06000102 RID: 258 RVA: 0x000088A8 File Offset: 0x00006AA8
	protected virtual void Update()
	{
		this.theExistTime += Time.deltaTime;
		if (this.theExistTime > 0.75f && this.theMovingWay == 3)
		{
			this.Die();
		}
		if (!this.isZombieBullet)
		{
			switch (this.theMovingWay)
			{
			case -1:
				break;
			case 0:
			case 2:
			case 3:
				base.transform.Translate(6f * Time.deltaTime * Vector3.right);
				return;
			case 1:
				this.RollingUpdate();
				return;
			case 4:
				if (this.theExistTime < 0.5f)
				{
					base.transform.Translate(5.5f * (1f - this.theExistTime) * (1f - this.theExistTime) * Time.deltaTime * Vector3.up);
				}
				base.transform.Translate(6f * Time.deltaTime * Vector3.right);
				return;
			case 5:
				if (this.theExistTime < 0.5f)
				{
					base.transform.Translate(5.5f * (1f - this.theExistTime) * (1f - this.theExistTime) * Time.deltaTime * Vector3.down);
				}
				base.transform.Translate(6f * Time.deltaTime * Vector3.right);
				return;
			case 6:
				this.TrackUpdate();
				return;
			default:
				return;
			}
		}
		else
		{
			base.transform.Translate(6f * Time.deltaTime * Vector3.left);
		}
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00008A40 File Offset: 0x00006C40
	private void TrackUpdate()
	{
		if (this.zombie == null)
		{
			this.zombie = this.GetNearestZombie();
			base.transform.position += 4f * Time.deltaTime * this.currentDireciton.normalized;
			return;
		}
		if (this.zombie.GetComponent<Zombie>().theStatus != 1)
		{
			Vector2 vector = this.zombie.GetComponent<Collider2D>().bounds.center;
			base.transform.position = Vector2.MoveTowards(base.transform.position, vector, 4f * Time.deltaTime);
			Vector2 vector2 = vector - base.transform.position;
			if (vector2.magnitude > 0.1f)
			{
				this.currentDireciton = vector2;
			}
			float z = Mathf.Atan2(vector2.y, vector2.x) * 57.29578f;
			base.transform.rotation = Quaternion.Euler(0f, 0f, z);
			this.shadow.transform.position = base.transform.GetChild(0).position - new Vector3(0f, 0.5f, 0f);
			this.shadow.transform.rotation = Quaternion.identity;
			return;
		}
		this.zombie = this.GetNearestZombie();
	}

	// Token: 0x06000104 RID: 260 RVA: 0x00008BC0 File Offset: 0x00006DC0
	protected virtual GameObject GetNearestZombie()
	{
		float num = float.MaxValue;
		GameObject gameObject = null;
		foreach (GameObject gameObject2 in Board.Instance.zombieArray)
		{
			if (gameObject2 != null)
			{
				Zombie component = gameObject2.GetComponent<Zombie>();
				Collider2D collider2D;
				if (!component.isMindControlled && component.theStatus != 1 && component.shadow.transform.position.x < 9.2f && component.TryGetComponent<Collider2D>(out collider2D) && Vector2.Distance(collider2D.bounds.center, base.transform.position) < num)
				{
					gameObject = gameObject2;
					num = Vector2.Distance(collider2D.bounds.center, base.transform.position);
				}
			}
		}
		if (gameObject != null)
		{
			int theZombieRow = gameObject.GetComponent<Zombie>().theZombieRow;
			CreateBullet.Instance.SetLayer(theZombieRow, base.gameObject);
		}
		return gameObject;
	}

	// Token: 0x06000105 RID: 261 RVA: 0x00008CF8 File Offset: 0x00006EF8
	private void FixedUpdate()
	{
		Vector3 vector = this.mainCamara.WorldToScreenPoint(base.transform.position);
		if (vector.x < 0f || vector.x > (float)Screen.width || vector.y < 0f || vector.y > (float)Screen.height)
		{
			this.Die();
		}
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00008D58 File Offset: 0x00006F58
	private void RollingUpdate()
	{
		base.transform.Translate(2.5f * Time.deltaTime * Vector3.right);
		if (!this.isLand)
		{
			base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y - this.g * Time.deltaTime);
			this.shadow.transform.position = new Vector3(this.shadow.transform.position.x, this.shadow.transform.position.y + this.g * Time.deltaTime);
			this.g += 10f * Time.deltaTime;
			if (base.transform.position.y < this.startPosition.y - 0.6f)
			{
				this.isLand = true;
			}
		}
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00008E5C File Offset: 0x0000705C
	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (this.hasHitTarget)
		{
			return;
		}
		if (collision.CompareTag("Plant") && (this.isZombieBullet || Board.Instance.isScaredyDream))
		{
			this.CheckPlant(collision.gameObject);
			return;
		}
		if (collision.CompareTag("Zombie"))
		{
			this.CheckZombie(collision.gameObject);
		}
	}

	// Token: 0x06000108 RID: 264 RVA: 0x00008EBC File Offset: 0x000070BC
	private void CheckPlant(GameObject plant)
	{
		Plant component = plant.GetComponent<Plant>();
		Shooter shooter;
		if (GameAPP.board.GetComponent<Board>().isScaredyDream && !this.isZombieBullet && (this.theBulletType == 0 || this.theBulletType == 9) && component.TryGetComponent<Shooter>(out shooter) && !component.isShort)
		{
			this.HitPlantInDream(shooter);
			return;
		}
		if (this.isZombieBullet && component.thePlantRow == this.theBulletRow && !component.isShort && component.thePlantType != 254)
		{
			this.hasHitTarget = true;
			this.HitPlant(plant);
		}
	}

	// Token: 0x06000109 RID: 265 RVA: 0x00008F50 File Offset: 0x00007150
	private void HitPlantInDream(Shooter shooter)
	{
		if (shooter.gameObject != this.parentPlant)
		{
			if (shooter.dreamTime == 0f)
			{
				shooter.dreamTime = this.GetTime(shooter.thePlantType);
				GameObject gameObject = shooter.AnimShoot();
				if (gameObject == null)
				{
					this.Die();
					return;
				}
				Bullet component = gameObject.GetComponent<Bullet>();
				component.parentPlant = shooter.gameObject;
				component.transform.Translate(0.2f, 0f, 0f);
				if (component.theMovingWay != 1)
				{
					if (this.AllowUp(shooter.thePlantType))
					{
						GameObject gameObject2 = GameAPP.board.GetComponent<CreateBullet>().SetBullet(gameObject.transform.position.x, gameObject.transform.position.y, component.theBulletRow, component.theBulletType, 2);
						gameObject2.transform.Translate(-0.5f, 0f, 0f);
						this.Rotate(gameObject2, 90);
						gameObject2.GetComponent<Bullet>().parentPlant = shooter.gameObject;
					}
					if (this.AllowDown(shooter.thePlantType))
					{
						GameObject gameObject3 = GameAPP.board.GetComponent<CreateBullet>().SetBullet(gameObject.transform.position.x, gameObject.transform.position.y, component.theBulletRow, component.theBulletType, 2);
						gameObject3.transform.Translate(-0.5f, 0f, 0f);
						this.Rotate(gameObject3, -90);
						gameObject3.GetComponent<Bullet>().parentPlant = shooter.gameObject;
					}
				}
			}
			this.Die();
		}
	}

	// Token: 0x0600010A RID: 266 RVA: 0x000090E9 File Offset: 0x000072E9
	private bool AllowUp(int type)
	{
		return type == 1017 || type == 1030 || type == 1032;
	}

	// Token: 0x0600010B RID: 267 RVA: 0x00009106 File Offset: 0x00007306
	private bool AllowDown(int type)
	{
		return type == 1017 || type == 1032;
	}

	// Token: 0x0600010C RID: 268 RVA: 0x0000911C File Offset: 0x0000731C
	private float GetTime(int type)
	{
		if (type <= 1023)
		{
			if (type <= 1000)
			{
				if (type != 7 && type != 1000)
				{
					goto IL_6C;
				}
			}
			else if (type - 1004 > 1 && type != 1023)
			{
				goto IL_6C;
			}
		}
		else if (type <= 1034)
		{
			if (type - 1025 > 1 && type != 1034)
			{
				goto IL_6C;
			}
		}
		else if (type != 1037 && type != 1043 && type != 1046)
		{
			goto IL_6C;
		}
		return 0.2f;
		IL_6C:
		return 0.05f;
	}

	// Token: 0x0600010D RID: 269 RVA: 0x0000919C File Offset: 0x0000739C
	private void Rotate(GameObject obj, int angle)
	{
		Vector2 v = Vector2.zero;
		foreach (object obj2 in obj.transform)
		{
			Transform transform = (Transform)obj2;
			if (transform.name == "Shadow")
			{
				v = transform.transform.position;
			}
		}
		obj.transform.Rotate(0f, 0f, (float)angle);
		foreach (object obj3 in obj.transform)
		{
			Transform transform2 = (Transform)obj3;
			if (transform2.name == "Shadow")
			{
				transform2.Rotate(0f, 0f, (float)(-(float)angle));
				transform2.transform.position = v;
			}
		}
	}

	// Token: 0x0600010E RID: 270 RVA: 0x000092AC File Offset: 0x000074AC
	protected virtual void CheckZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		if (this.isZombieBullet)
		{
			if (component.isMindControlled && component.theZombieRow == this.theBulletRow)
			{
				this.HitZombie(zombie);
				return;
			}
		}
		else
		{
			if (component.isMindControlled)
			{
				return;
			}
			int theStatus = component.theStatus;
			if (theStatus == 3 || theStatus == 9)
			{
				return;
			}
			switch (this.theMovingWay)
			{
			case 0:
			case 2:
			case 3:
			case 5:
				if (component.theStatus == 7)
				{
					return;
				}
				break;
			}
			if (component.theZombieRow != this.theBulletRow && this.theMovingWay != 2)
			{
				return;
			}
			this.hasHitTarget = true;
			this.HitZombie(zombie);
		}
	}

	// Token: 0x0600010F RID: 271 RVA: 0x0000935A File Offset: 0x0000755A
	protected virtual void HitPlant(GameObject plant)
	{
		GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
		plant.GetComponent<Plant>().thePlantHealth -= this.theBulletDamage;
		this.Die();
	}

	// Token: 0x06000110 RID: 272 RVA: 0x0000938B File Offset: 0x0000758B
	protected virtual void HitZombie(GameObject zombie)
	{
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00009390 File Offset: 0x00007590
	protected virtual void PlaySound(Zombie zombie)
	{
		if (zombie.theSecondArmorType != 0)
		{
			if (zombie.theSecondArmorType == 1)
			{
				GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
				return;
			}
			if (zombie.theSecondArmorType == 2)
			{
				GameAPP.PlaySound(Random.Range(14, 16), 0.5f);
				return;
			}
		}
		if (zombie.theFirstArmorType != 0)
		{
			if (zombie.theFirstArmorType == 1)
			{
				GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
				GameAPP.PlaySound(Random.Range(12, 14), 0.5f);
				return;
			}
			if (zombie.theFirstArmorType == 2)
			{
				GameAPP.PlaySound(Random.Range(14, 16), 0.5f);
				return;
			}
		}
		int theZombieType = zombie.theZombieType;
		switch (theZombieType)
		{
		case 14:
		case 16:
		case 18:
			break;
		case 15:
		case 17:
			goto IL_D5;
		default:
			if (theZombieType - 200 > 1)
			{
				goto IL_D5;
			}
			break;
		}
		GameAPP.PlaySound(Random.Range(14, 16), 0.5f);
		return;
		IL_D5:
		GameAPP.PlaySound(Random.Range(0, 3), 0.5f);
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00009484 File Offset: 0x00007684
	public void FireZombie(GameObject zombie)
	{
		Zombie component = zombie.GetComponent<Zombie>();
		component.TakeDamage(0, this.theBulletDamage);
		component.Warm(0);
		if (this.AllowSputter(component))
		{
			GameAPP.PlaySound(Random.Range(59, 61), 0.5f);
			PoolMgr.Instance.SpawnParticle(base.transform.position, 33).GetComponent<SpriteRenderer>().sortingLayerName = string.Format("particle{0}", this.theBulletRow);
			this.AttackOtherZombie(component);
		}
		else
		{
			this.PlaySound(component);
		}
		this.Die();
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00009514 File Offset: 0x00007714
	private void AttackOtherZombie(Zombie zombie)
	{
		int num = this.theBulletDamage;
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 1f, this.zombieLayer);
		for (int i = 0; i < array.Length; i++)
		{
			Zombie component = array[i].GetComponent<Zombie>();
			if (!(component == zombie) && component.theZombieRow == this.theBulletRow && !component.isMindControlled && this.AllowSputter(component))
			{
				this.zombieToFired.Add(component);
			}
		}
		int count = this.zombieToFired.Count;
		if (count == 0)
		{
			return;
		}
		int num2 = num / count;
		if (num2 == 0)
		{
			num2 = 1;
		}
		if ((float)num2 > 0.33333334f * (float)this.theBulletDamage)
		{
			num2 = (int)(0.33333334f * (float)this.theBulletDamage);
		}
		foreach (Zombie zombie2 in this.zombieToFired)
		{
			zombie2.TakeDamage(0, num2);
			zombie2.Warm(0);
		}
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00009628 File Offset: 0x00007828
	private bool AllowSputter(Zombie zombie)
	{
		if (zombie.theSecondArmorType == 2)
		{
			return false;
		}
		int theZombieType = zombie.theZombieType;
		switch (theZombieType)
		{
		case 14:
		case 16:
		case 18:
			break;
		case 15:
		case 17:
			return true;
		default:
			if (theZombieType - 200 > 1)
			{
				return true;
			}
			break;
		}
		return false;
	}

	// Token: 0x040000E0 RID: 224
	public int theBulletType;

	// Token: 0x040000E1 RID: 225
	public int theMovingWay;

	// Token: 0x040000E2 RID: 226
	public bool isZombieBullet;

	// Token: 0x040000E3 RID: 227
	public int theBulletRow;

	// Token: 0x040000E4 RID: 228
	public GameObject zombie;

	// Token: 0x040000E5 RID: 229
	public GameObject torchWood;

	// Token: 0x040000E6 RID: 230
	public float theExistTime;

	// Token: 0x040000E7 RID: 231
	public int theBulletDamage = 20;

	// Token: 0x040000E8 RID: 232
	public bool hasHitTarget;

	// Token: 0x040000E9 RID: 233
	public bool isShort;

	// Token: 0x040000EA RID: 234
	public int zombieLayer;

	// Token: 0x040000EB RID: 235
	private Camera mainCamara;

	// Token: 0x040000EC RID: 236
	private Vector2 currentDireciton = new Vector2(1f, 0f);

	// Token: 0x040000ED RID: 237
	public bool isFromThreeTorch;

	// Token: 0x040000EE RID: 238
	public int hitTimes;

	// Token: 0x040000EF RID: 239
	public readonly List<GameObject> Z = new List<GameObject>();

	// Token: 0x040000F0 RID: 240
	public readonly List<Zombie> zombieToFired = new List<Zombie>();

	// Token: 0x040000F1 RID: 241
	public int fireLevel;

	// Token: 0x040000F2 RID: 242
	public int zombieBlockType;

	// Token: 0x040000F3 RID: 243
	public GameObject shadow;

	// Token: 0x040000F4 RID: 244
	public GameObject fireParticle;

	// Token: 0x040000F5 RID: 245
	public Sprite sprite;

	// Token: 0x040000F6 RID: 246
	public int puffColor;

	// Token: 0x040000F7 RID: 247
	public Vector2 startPosition;

	// Token: 0x040000F8 RID: 248
	public bool isLand;

	// Token: 0x040000F9 RID: 249
	public bool isHot;

	// Token: 0x040000FA RID: 250
	public float g = 2f;

	// Token: 0x040000FB RID: 251
	public GameObject parentPlant;

	// Token: 0x040000FC RID: 252
	public float Vx;

	// Token: 0x040000FD RID: 253
	public float Vy;

	// Token: 0x040000FE RID: 254
	public float Y;

	// Token: 0x040000FF RID: 255
	public float originY;

	// Token: 0x04000100 RID: 256
	public bool firstLand;
}

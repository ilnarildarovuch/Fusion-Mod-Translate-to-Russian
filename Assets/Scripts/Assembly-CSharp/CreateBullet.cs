using UnityEngine;

public class CreateBullet : MonoBehaviour
{
	public enum BulletType
	{
		Pea = 0,
		Cherry = 1,
		Peanut = 2,
		SuperCherry = 3,
		ZombieBlock1 = 4,
		ZombieBlock2 = 5,
		ZombieBlock3 = 6,
		Potato = 7,
		SmallSun = 8,
		Puff = 9,
		PuffPea = 10,
		IronPea = 11,
		ThreeSpike = 12,
		Puff_randomColor = 13,
		Puff_love = 14,
		SnowPea = 15,
		Puff_snowPea = 16,
		IceSpark = 17,
		IceSpark_small = 18,
		Track = 20,
		Puff_snow = 21,
		Puff_dark = 22,
		Doom = 23,
		IceDoom = 24,
		FirePea_yellow = 25,
		FirePea_orange = 26,
		FirePea_red = 27,
		Squash = 28,
		Tangkelp = 29,
		Firekelp = 30,
		SquashKelp = 32,
		NormalTrack = 33,
		IceTrack = 34,
		FireTrack = 35,
		CherrySquashBullet = 36
	}

	public static CreateBullet Instance;

	private void Awake()
	{
		Instance = this;
	}

	public GameObject SetBullet(float theX, float theY, int theRow, int theBulletType, int theMovingWay)
	{
		GameObject gameObject = Object.Instantiate(position: new Vector3(theX, theY, 1f), original: GameAPP.bulletPrefab[theBulletType], rotation: Quaternion.identity, parent: base.transform);
		Bullet bullet = AddUniqueComponent(theBulletType, gameObject);
		bullet.theBulletType = ((theBulletType != 25) ? theBulletType : 0);
		bullet.theBulletRow = theRow;
		bullet.theMovingWay = theMovingWay;
		SetLayer(theRow, gameObject);
		AddToList(gameObject);
		return gameObject;
	}

	private void AddToList(GameObject bullet)
	{
		for (int i = 0; i < Board.Instance.bulletArray.Length; i++)
		{
			if (Board.Instance.bulletArray[i] == null)
			{
				Board.Instance.bulletArray[i] = bullet;
				break;
			}
		}
	}

	public void SetLayer(int theRow, GameObject theBullet)
	{
		if (theBullet.transform.childCount != 0)
		{
			foreach (Transform item in theBullet.transform)
			{
				if (!(item.gameObject.name == "Shadow"))
				{
					ParticleSystem component2;
					if (item.TryGetComponent<SpriteRenderer>(out var component))
					{
						component.sortingOrder += (theRow + 1) * 100;
						component.sortingLayerName = $"bullet{theRow}";
					}
					else if (item.TryGetComponent<ParticleSystem>(out component2))
					{
						component2.GetComponent<Renderer>().sortingOrder += (theRow + 1) * 100;
						component2.GetComponent<Renderer>().sortingLayerName = $"bullet{theRow}";
					}
				}
			}
		}
		if (theBullet.TryGetComponent<SpriteRenderer>(out var component3))
		{
			component3.sortingOrder += (theRow + 1) * 100;
			component3.sortingLayerName = $"bullet{theRow}";
		}
	}

	public Bullet AddUniqueComponent(int theBulletType, GameObject bullet)
	{
		Bullet bullet2;
		switch (theBulletType)
		{
		case 0:
			bullet2 = bullet.AddComponent<Pea>();
			break;
		case 10:
			bullet2 = bullet.AddComponent<Pea>();
			bullet2.isShort = true;
			break;
		case 1:
			bullet2 = bullet.AddComponent<CherryBullet>();
			break;
		case 2:
			bullet2 = bullet.AddComponent<NutBullet>();
			break;
		case 3:
			bullet2 = bullet.AddComponent<SuperCherryBullet>();
			break;
		case 4:
			bullet2 = bullet.AddComponent<ZombieBlock>();
			bullet2.zombieBlockType = 0;
			break;
		case 5:
			bullet2 = bullet.AddComponent<ZombieBlock>();
			bullet2.zombieBlockType = 1;
			break;
		case 6:
			bullet2 = bullet.AddComponent<ZombieBlock>();
			bullet2.zombieBlockType = 2;
			break;
		case 7:
			bullet2 = bullet.AddComponent<PotatoPea>();
			break;
		case 8:
			bullet2 = bullet.AddComponent<SmallSun>();
			break;
		case 9:
			bullet2 = bullet.AddComponent<Puff>();
			bullet2.isShort = true;
			break;
		case 11:
			bullet2 = bullet.AddComponent<IronPea>();
			break;
		case 13:
			bullet2 = bullet.AddComponent<PuffRandomColor>();
			break;
		case 14:
			bullet2 = bullet.AddComponent<PuffLove>();
			break;
		case 15:
		case 16:
		case 21:
			bullet2 = bullet.AddComponent<SnowPea>();
			break;
		case 17:
			bullet2 = bullet.AddComponent<IceSpark>();
			break;
		case 18:
			bullet2 = bullet.AddComponent<IceSpark>();
			bullet2.isShort = true;
			break;
		case 22:
			bullet2 = bullet.AddComponent<PuffBlack>();
			break;
		case 23:
			bullet2 = bullet.AddComponent<DoomBullet>();
			break;
		case 24:
			bullet2 = bullet.AddComponent<IceDoomBullet>();
			break;
		case 28:
			bullet2 = bullet.AddComponent<SquashBullet>();
			break;
		case 29:
			bullet2 = bullet.AddComponent<KelpBullet>();
			break;
		case 30:
			bullet2 = bullet.AddComponent<FireKelpBullet>();
			break;
		case 12:
			bullet2 = bullet.AddComponent<ThreeSpikeBullet>();
			break;
		case 20:
			bullet2 = bullet.AddComponent<TrackBullet>();
			break;
		case 32:
			bullet2 = bullet.AddComponent<SquashKelpBullet>();
			break;
		case 33:
			bullet2 = bullet.AddComponent<NormalTrack>();
			break;
		case 34:
			bullet2 = bullet.AddComponent<IceTrack>();
			break;
		case 35:
			bullet2 = bullet.AddComponent<FireTrack>();
			break;
		case 36:
			bullet2 = bullet.AddComponent<CherrySquashBullet>();
			break;
		default:
			bullet2 = bullet.AddComponent<Pea>();
			break;
		}
		return bullet2;
	}
}

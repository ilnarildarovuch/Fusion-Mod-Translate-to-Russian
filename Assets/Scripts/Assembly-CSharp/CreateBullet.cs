using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
public class CreateBullet : MonoBehaviour
{
	// Token: 0x06000173 RID: 371 RVA: 0x0000B9C2 File Offset: 0x00009BC2
	private void Awake()
	{
		CreateBullet.Instance = this;
	}

	// Token: 0x06000174 RID: 372 RVA: 0x0000B9CC File Offset: 0x00009BCC
	public GameObject SetBullet(float theX, float theY, int theRow, int theBulletType, int theMovingWay)
	{
		Vector3 position = new Vector3(theX, theY, 1f);
		GameObject gameObject = Object.Instantiate<GameObject>(GameAPP.bulletPrefab[theBulletType], position, Quaternion.identity, base.transform);
		Bullet bullet = this.AddUniqueComponent(theBulletType, gameObject);
		bullet.theBulletType = ((theBulletType == 25) ? 0 : theBulletType);
		bullet.theBulletRow = theRow;
		bullet.theMovingWay = theMovingWay;
		this.SetLayer(theRow, gameObject);
		this.AddToList(gameObject);
		return gameObject;
	}

	// Token: 0x06000175 RID: 373 RVA: 0x0000BA3C File Offset: 0x00009C3C
	private void AddToList(GameObject bullet)
	{
		for (int i = 0; i < Board.Instance.bulletArray.Length; i++)
		{
			if (Board.Instance.bulletArray[i] == null)
			{
				Board.Instance.bulletArray[i] = bullet;
				return;
			}
		}
	}

	// Token: 0x06000176 RID: 374 RVA: 0x0000BA84 File Offset: 0x00009C84
	public void SetLayer(int theRow, GameObject theBullet)
	{
		if (theBullet.transform.childCount != 0)
		{
			foreach (object obj in theBullet.transform)
			{
				Transform transform = (Transform)obj;
				if (!(transform.gameObject.name == "Shadow"))
				{
					SpriteRenderer spriteRenderer;
					ParticleSystem particleSystem;
					if (transform.TryGetComponent<SpriteRenderer>(out spriteRenderer))
					{
						spriteRenderer.sortingOrder += (theRow + 1) * 100;
						spriteRenderer.sortingLayerName = string.Format("bullet{0}", theRow);
					}
					else if (transform.TryGetComponent<ParticleSystem>(out particleSystem))
					{
						particleSystem.GetComponent<Renderer>().sortingOrder += (theRow + 1) * 100;
						particleSystem.GetComponent<Renderer>().sortingLayerName = string.Format("bullet{0}", theRow);
					}
				}
			}
		}
		SpriteRenderer spriteRenderer2;
		if (theBullet.TryGetComponent<SpriteRenderer>(out spriteRenderer2))
		{
			spriteRenderer2.sortingOrder += (theRow + 1) * 100;
			spriteRenderer2.sortingLayerName = string.Format("bullet{0}", theRow);
		}
	}

	// Token: 0x06000177 RID: 375 RVA: 0x0000BBB0 File Offset: 0x00009DB0
	public Bullet AddUniqueComponent(int theBulletType, GameObject bullet)
	{
		Bullet bullet2;
		switch (theBulletType)
		{
		case 0:
			return bullet.AddComponent<Pea>();
		case 1:
			return bullet.AddComponent<CherryBullet>();
		case 2:
			return bullet.AddComponent<NutBullet>();
		case 3:
			return bullet.AddComponent<SuperCherryBullet>();
		case 4:
			bullet2 = bullet.AddComponent<ZombieBlock>();
			bullet2.zombieBlockType = 0;
			return bullet2;
		case 5:
			bullet2 = bullet.AddComponent<ZombieBlock>();
			bullet2.zombieBlockType = 1;
			return bullet2;
		case 6:
			bullet2 = bullet.AddComponent<ZombieBlock>();
			bullet2.zombieBlockType = 2;
			return bullet2;
		case 7:
			return bullet.AddComponent<PotatoPea>();
		case 8:
			return bullet.AddComponent<SmallSun>();
		case 9:
			bullet2 = bullet.AddComponent<Puff>();
			bullet2.isShort = true;
			return bullet2;
		case 10:
			bullet2 = bullet.AddComponent<Pea>();
			bullet2.isShort = true;
			return bullet2;
		case 11:
			return bullet.AddComponent<IronPea>();
		case 12:
			return bullet.AddComponent<ThreeSpikeBullet>();
		case 13:
			return bullet.AddComponent<PuffRandomColor>();
		case 14:
			return bullet.AddComponent<PuffLove>();
		case 15:
		case 16:
		case 21:
			return bullet.AddComponent<SnowPea>();
		case 17:
			return bullet.AddComponent<IceSpark>();
		case 18:
			bullet2 = bullet.AddComponent<IceSpark>();
			bullet2.isShort = true;
			return bullet2;
		case 20:
			return bullet.AddComponent<TrackBullet>();
		case 22:
			return bullet.AddComponent<PuffBlack>();
		case 23:
			return bullet.AddComponent<DoomBullet>();
		case 24:
			return bullet.AddComponent<IceDoomBullet>();
		case 28:
			return bullet.AddComponent<SquashBullet>();
		case 29:
			return bullet.AddComponent<KelpBullet>();
		case 30:
			return bullet.AddComponent<FireKelpBullet>();
		case 32:
			return bullet.AddComponent<SquashKelpBullet>();
		case 33:
			return bullet.AddComponent<NormalTrack>();
		case 34:
			return bullet.AddComponent<IceTrack>();
		case 35:
			return bullet.AddComponent<FireTrack>();
		case 36:
			return bullet.AddComponent<CherrySquashBullet>();
		}
		bullet2 = bullet.AddComponent<Pea>();
		return bullet2;
	}

	// Token: 0x04000112 RID: 274
	public static CreateBullet Instance;

	// Token: 0x0200012A RID: 298
	public enum BulletType
	{
		// Token: 0x04000362 RID: 866
		Pea,
		// Token: 0x04000363 RID: 867
		Cherry,
		// Token: 0x04000364 RID: 868
		Peanut,
		// Token: 0x04000365 RID: 869
		SuperCherry,
		// Token: 0x04000366 RID: 870
		ZombieBlock1,
		// Token: 0x04000367 RID: 871
		ZombieBlock2,
		// Token: 0x04000368 RID: 872
		ZombieBlock3,
		// Token: 0x04000369 RID: 873
		Potato,
		// Token: 0x0400036A RID: 874
		SmallSun,
		// Token: 0x0400036B RID: 875
		Puff,
		// Token: 0x0400036C RID: 876
		PuffPea,
		// Token: 0x0400036D RID: 877
		IronPea,
		// Token: 0x0400036E RID: 878
		ThreeSpike,
		// Token: 0x0400036F RID: 879
		Puff_randomColor,
		// Token: 0x04000370 RID: 880
		Puff_love,
		// Token: 0x04000371 RID: 881
		SnowPea,
		// Token: 0x04000372 RID: 882
		Puff_snowPea,
		// Token: 0x04000373 RID: 883
		IceSpark,
		// Token: 0x04000374 RID: 884
		IceSpark_small,
		// Token: 0x04000375 RID: 885
		Track = 20,
		// Token: 0x04000376 RID: 886
		Puff_snow,
		// Token: 0x04000377 RID: 887
		Puff_dark,
		// Token: 0x04000378 RID: 888
		Doom,
		// Token: 0x04000379 RID: 889
		IceDoom,
		// Token: 0x0400037A RID: 890
		FirePea_yellow,
		// Token: 0x0400037B RID: 891
		FirePea_orange,
		// Token: 0x0400037C RID: 892
		FirePea_red,
		// Token: 0x0400037D RID: 893
		Squash,
		// Token: 0x0400037E RID: 894
		Tangkelp,
		// Token: 0x0400037F RID: 895
		Firekelp,
		// Token: 0x04000380 RID: 896
		SquashKelp = 32,
		// Token: 0x04000381 RID: 897
		NormalTrack,
		// Token: 0x04000382 RID: 898
		IceTrack,
		// Token: 0x04000383 RID: 899
		FireTrack,
		// Token: 0x04000384 RID: 900
		CherrySquashBullet
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FF RID: 255
public class PoolMgr : MonoBehaviour
{
	// Token: 0x060004FE RID: 1278 RVA: 0x0002B239 File Offset: 0x00029439
	private void Awake()
	{
		PoolMgr.Instance = this;
		this.pooledParticles = new List<GameObject>();
		this.pooledBullets = new List<GameObject>();
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x0002B258 File Offset: 0x00029458
	public GameObject GetPooledParticle(int type)
	{
		foreach (GameObject gameObject in this.pooledParticles)
		{
			if (!gameObject.activeInHierarchy && gameObject.name == GameAPP.particlePrefab[type].name)
			{
				gameObject.SetActive(true);
				return gameObject;
			}
		}
		GameObject gameObject2 = Object.Instantiate<GameObject>(GameAPP.particlePrefab[type], base.transform);
		gameObject2.name = GameAPP.particlePrefab[type].name;
		this.pooledParticles.Add(gameObject2);
		return gameObject2;
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x0002B304 File Offset: 0x00029504
	public void ReturnToPool(GameObject particle)
	{
		particle.SetActive(false);
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x0002B310 File Offset: 0x00029510
	public GameObject SpawnParticle(Vector3 position, int type)
	{
		GameObject pooledParticle = PoolMgr.Instance.GetPooledParticle(type);
		pooledParticle.transform.position = position;
		if (type != 33)
		{
			base.StartCoroutine(this.DelayReturnParticle(pooledParticle.GetComponent<ParticleSystem>().main.startLifetime.constant, pooledParticle));
		}
		return pooledParticle;
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x0002B364 File Offset: 0x00029564
	private void ReturnParticle(GameObject particle)
	{
		PoolMgr.Instance.ReturnToPool(particle);
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x0002B371 File Offset: 0x00029571
	private IEnumerator DelayReturnParticle(float delay, GameObject particle)
	{
		yield return new WaitForSeconds(delay);
		this.ReturnParticle(particle);
		yield break;
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x0002B390 File Offset: 0x00029590
	public GameObject GetPooledBullet(int type)
	{
		foreach (GameObject gameObject in this.pooledBullets)
		{
			if (gameObject != null && !gameObject.activeInHierarchy && gameObject.name == GameAPP.bulletPrefab[type].name)
			{
				gameObject.SetActive(true);
				return gameObject;
			}
		}
		GameObject gameObject2 = Object.Instantiate<GameObject>(GameAPP.bulletPrefab[type], base.transform);
		gameObject2.name = GameAPP.bulletPrefab[type].name;
		this.pooledBullets.Add(gameObject2);
		return gameObject2;
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x0002B448 File Offset: 0x00029648
	public GameObject SpawnBullet(Vector3 position, int type)
	{
		GameObject pooledBullet = PoolMgr.Instance.GetPooledBullet(type);
		pooledBullet.transform.SetPositionAndRotation(position, Quaternion.identity);
		return pooledBullet;
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x0002B466 File Offset: 0x00029666
	public void ReturnBullet(GameObject bullet)
	{
		bullet.SetActive(false);
	}

	// Token: 0x0400027B RID: 635
	public static PoolMgr Instance;

	// Token: 0x0400027C RID: 636
	public GameObject particlePrefab;

	// Token: 0x0400027D RID: 637
	public GameObject bulletPrefab;

	// Token: 0x0400027E RID: 638
	public List<GameObject> pooledParticles;

	// Token: 0x0400027F RID: 639
	public List<GameObject> pooledBullets;
}

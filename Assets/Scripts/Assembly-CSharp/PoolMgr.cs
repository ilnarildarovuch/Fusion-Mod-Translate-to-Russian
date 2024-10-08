using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMgr : MonoBehaviour
{
	public static PoolMgr Instance;

	public GameObject particlePrefab;

	public GameObject bulletPrefab;

	public List<GameObject> pooledParticles;

	public List<GameObject> pooledBullets;

	private void Awake()
	{
		Instance = this;
		pooledParticles = new List<GameObject>();
		pooledBullets = new List<GameObject>();
	}

	public GameObject GetPooledParticle(int type)
	{
		foreach (GameObject pooledParticle in pooledParticles)
		{
			if (!pooledParticle.activeInHierarchy && pooledParticle.name == GameAPP.particlePrefab[type].name)
			{
				pooledParticle.SetActive(value: true);
				return pooledParticle;
			}
		}
		GameObject gameObject = Object.Instantiate(GameAPP.particlePrefab[type], base.transform);
		gameObject.name = GameAPP.particlePrefab[type].name;
		pooledParticles.Add(gameObject);
		return gameObject;
	}

	public void ReturnToPool(GameObject particle)
	{
		particle.SetActive(value: false);
	}

	public GameObject SpawnParticle(Vector3 position, int type)
	{
		GameObject pooledParticle = Instance.GetPooledParticle(type);
		pooledParticle.transform.position = position;
		if (type != 33)
		{
			StartCoroutine(DelayReturnParticle(pooledParticle.GetComponent<ParticleSystem>().main.startLifetime.constant, pooledParticle));
		}
		return pooledParticle;
	}

	private void ReturnParticle(GameObject particle)
	{
		Instance.ReturnToPool(particle);
	}

	private IEnumerator DelayReturnParticle(float delay, GameObject particle)
	{
		yield return new WaitForSeconds(delay);
		ReturnParticle(particle);
	}

	public GameObject GetPooledBullet(int type)
	{
		foreach (GameObject pooledBullet in pooledBullets)
		{
			if (pooledBullet != null && !pooledBullet.activeInHierarchy && pooledBullet.name == GameAPP.bulletPrefab[type].name)
			{
				pooledBullet.SetActive(value: true);
				return pooledBullet;
			}
		}
		GameObject gameObject = Object.Instantiate(GameAPP.bulletPrefab[type], base.transform);
		gameObject.name = GameAPP.bulletPrefab[type].name;
		pooledBullets.Add(gameObject);
		return gameObject;
	}

	public GameObject SpawnBullet(Vector3 position, int type)
	{
		GameObject pooledBullet = Instance.GetPooledBullet(type);
		pooledBullet.transform.SetPositionAndRotation(position, Quaternion.identity);
		return pooledBullet;
	}

	public void ReturnBullet(GameObject bullet)
	{
		bullet.SetActive(value: false);
	}
}

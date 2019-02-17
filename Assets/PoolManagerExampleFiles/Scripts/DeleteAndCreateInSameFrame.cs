using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;


public class DeleteAndCreateInSameFrame : MonoBehaviour
{
	public SpawnPool poolPrefab;
	
	private void Start()
	{
		this.StartCoroutine(DoIt());
	}

	private IEnumerator DoIt()
	{
		SpawnPool pool;
		while (true)
		{
			pool = (SpawnPool)Instantiate(this.poolPrefab);

			yield return new WaitForSeconds(2);

			PoolManager.Pools.Destroy(pool.poolName);
			//GameObject.Destroy(pool.gameObject);
			
			pool = (SpawnPool)Instantiate(this.poolPrefab);
			

			yield return new WaitForSeconds(2);
			
			PoolManager.Pools.DestroyAll();

		}
	}
}

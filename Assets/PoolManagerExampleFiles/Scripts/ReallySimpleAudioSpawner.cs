using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;


/// <description>
///	Example that spawns and particles at the position of this components GameObject
/// </description>
public class ReallySimpleAudioSpawner : MonoBehaviour 
{
	public AudioSource prefab;
	public AudioSource musicPrefab;

	public float spawnInterval = 2;
    
    private SpawnPool pool;

    private void Start()
    {
        this.pool = this.GetComponent<SpawnPool>();
        this.StartCoroutine(this.Spawner());

		if (this.musicPrefab != null)
			this.StartCoroutine(this.MusicSpawner());
    }

	private IEnumerator MusicSpawner()
	{
		AudioSource music;

		// Spawn and interupt by Despawn()
		music = this.pool.Spawn(this.musicPrefab);
		yield return new WaitForSeconds(2);
		this.pool.Despawn(music.transform);

		yield return new WaitForSeconds(1);

		// Spawn and interupt by Stop()
		music = this.pool.Spawn(this.musicPrefab);
		yield return new WaitForSeconds(2);
		music.Stop();

		yield return new WaitForSeconds(1);

		// Play again to ensure still starting from the beginning
		music = this.pool.Spawn(this.musicPrefab);
		yield return new WaitForSeconds(2);
		music.Stop();	}

	private IEnumerator Spawner()
	{			
		AudioSource current;
		while (true)
		{
			current = this.pool.Spawn
			(
				this.prefab, 
				this.transform.position, 
				this.transform.rotation
			);

			current.pitch = Random.Range(0.7f, 1.4f);
			
			yield return new WaitForSeconds(this.spawnInterval);
		}
	}
}
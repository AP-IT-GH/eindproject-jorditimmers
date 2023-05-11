using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieScript : MonoBehaviour
{
    public GameObject zombiePrefab; // Prefab die spawner moet zijn - beste optie is empty prefab
    public float spawnRadius = 10f; // Hoe hard de zombies uit elkaar spawnen

    private int waveNumber = 0; // huidige wave nummer
    private bool spawningWave = false; // checkt of wave is gespawned
    private int numZombiesSpawned = 0; // aantal zombies die zijn gespawned in de huidige wave
    private int numZombiesDestroyed = 0; // aantal zombies die zijn vernietigd in de huidige wave
    private int numZombiesPerWave = 0; // aantal zombies dat in de huidige wave gespawned moet worden

    void Update()
    {
        if (!spawningWave && numZombiesDestroyed == numZombiesSpawned)
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        spawningWave = true;
        numZombiesSpawned = 0;
        numZombiesDestroyed = 0;
        waveNumber++;

        // rekent uit hoeveel zombies er per wave moetten spawnen
        numZombiesPerWave = (int)Mathf.Pow(2, waveNumber);

        // Spawn zombies within a radius
        for (int i = 0; i < numZombiesPerWave; i++)
        {
            Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPos.y = Terrain.activeTerrain.SampleHeight(spawnPos);
            Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
            numZombiesSpawned++;
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Called when a zombie is destroyed
    public void ZombieDestroyed()
    {
        numZombiesDestroyed++;

        // Check if all zombies are destroyed
        if (numZombiesDestroyed == numZombiesSpawned)
        {
            spawningWave = false;
        }
    }
}
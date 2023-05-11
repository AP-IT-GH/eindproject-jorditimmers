using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieScript : MonoBehaviour
{
    // Prefab die spawner moet zijn - beste optie is empty prefab
    public GameObject zombiePrefab;
    // Hoe hard de zombies uit elkaar spawnen
    public float spawnRadius = 10f;

    // huidige wave nummer
    private int waveNumber = 0;
    // checkt of wave is gespawned
    private bool spawningWave = false;
    // aantal zombies die zijn gespawned in de huidige wave
    private int numZombiesSpawned = 0;
    // aantal zombies die zijn vernietigd in de huidige wave
    private int numZombiesDestroyed = 0;
    // aantal zombies dat in de huidige wave gespawned moet worden
    private int numZombiesPerWave = 0; 

    // zombiewave is voorbij en zombies zijn dood
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

        // Spawn zombie in een bepaalde radius
        for (int i = 0; i < numZombiesPerWave; i++)
        {
            Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPos.y = Terrain.activeTerrain.SampleHeight(spawnPos);
            Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
            numZombiesSpawned++;
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Wordt getriggered elke keer een zombie dood gaat
    public void ZombieDestroyed()
    {
        numZombiesDestroyed++;

        // Checkt of zombies dood zijn
        if (numZombiesDestroyed == numZombiesSpawned)
        {
            spawningWave = false;
        }
    }
}
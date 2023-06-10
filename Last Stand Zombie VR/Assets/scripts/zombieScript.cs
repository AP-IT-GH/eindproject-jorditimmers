using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieScript : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject specialZombie;
    public int[] zombiesPerWave = { 1, 2, 4, 8, 16 };
    public float spawnRadius = 10f;
    public float timeBetweenWaves = 5f;
    public int wavesToDisableSpawner = 3;
    public int startSpawnerAfterWave = 2;

    private int waveNumber = 0;
    private bool spawningWave = false;
    private int multiplier = 1;
    private List<GameObject> activeZombies = new List<GameObject>();
    private static zombieScript instance;

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    void Update()
    {
        if (waveNumber >= startSpawnerAfterWave && !spawningWave && activeZombies.Count == 0 && zombiePrefab != null)
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        spawningWave = true;
        waveNumber++;
        int waveIndex = waveNumber - 1;
        if (waveIndex < zombiesPerWave.Length)
        {
            int numZombiesPerWave = zombiesPerWave[waveIndex];
            if (activeZombies.Count == 0)
            {
                for (int i = 0; i < numZombiesPerWave; i++)
                {
                    Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
                    spawnPos.y = Terrain.activeTerrain.SampleHeight(spawnPos);
                    GameObject newZombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
                    activeZombies.Add(newZombie);
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }

        while (activeZombies.Count > 0)
        {
            yield return null;
        }

        if (waveNumber >= wavesToDisableSpawner)
        {
            gameObject.SetActive(false); // Disable the spawner after a certain number of waves
        }
        else
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            spawningWave = false;
            multiplier++;
        }
    }

    public void RemoveZombie(GameObject zombie)
    {
        activeZombies.Remove(zombie);
        Destroy(zombie);
    }
}

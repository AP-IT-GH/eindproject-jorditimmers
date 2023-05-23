using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieScript : MonoBehaviour
{
    public GameObject zombiePrefab;
    public GameObject specialZombie;
    public int baseNumZombiesPerWave = 10;
    public int baseNumSpecialZombiesPerWave = 1;
    public float spawnRadius = 10f;
    public float timeBetweenWaves = 5f;

    private int waveNumber = 0;
    private bool spawningWave = false;
    private int multiplier = 1;
    private List<GameObject> activeZombies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    void Update()
    {
        if (!spawningWave && activeZombies.Count == 0 && zombiePrefab != null )
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        spawningWave = true;
        waveNumber++;
        int numZombiesPerWave = baseNumZombiesPerWave * multiplier;
        int numSpecialZombiesPerWave = baseNumSpecialZombiesPerWave * multiplier;
        if (activeZombies.Count == 0)
        {
            for (int i = 0; i < numZombiesPerWave; i++)
            {
                Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
                spawnPos.y = Terrain.activeTerrain.SampleHeight(spawnPos);
                GameObject newZombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
               
                activeZombies.Add(newZombie);
               
                yield return new WaitForSeconds(0.5f);
                /*
                    for (int j = 0; j < numSpecialZombiesPerWave; j++)
                    {
                        Vector3 spawnSpecialPos = transform.position + Random.insideUnitSphere * spawnRadius;
                        spawnSpecialPos.y = Terrain.activeTerrain.SampleHeight(spawnSpecialPos);
                        GameObject otherZombie = Instantiate(specialZombie, spawnPos, Quaternion.identity);
                        activeZombies.Add(otherZombie);
                        yield return new WaitForSeconds(0.5f);
                    }
                    */
                
            }
        }
        

        while (activeZombies.Count > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(timeBetweenWaves);
        spawningWave = false;
        multiplier++; 
    }

    public void RemoveZombie(GameObject zombie)
    {
        activeZombies.Remove(zombie);
       
    }

    /*
    public void RemoveSpecialZombie(GameObject specialZombie)
    {
        activeZombies.Remove(specialZombie);

    }
    */
}

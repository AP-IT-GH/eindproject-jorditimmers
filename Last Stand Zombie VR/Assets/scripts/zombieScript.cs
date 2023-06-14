using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieScript : MonoBehaviour
{
    public GameObject zombiePrefab;
   
    public float spawnRadius = 10f;
    public float timeBetweenWaves = 5f;
    public int wavesToDisableSpawner = 3;
    public int startSpawnerAfterWave = 1;
    public int[] zombiesPerWave = { 1, 2, 4, 8, 16 };
    

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
        if (waveNumber >= startSpawnerAfterWave && !spawningWave && AllZombiesDestroyed() )
        {
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        spawningWave = true;
        waveNumber++;

        if (waveNumber < startSpawnerAfterWave)
        {
            spawningWave = false;
            yield break;
        }

        int numZombiesPerWave = GetNumZombiesPerWave(waveNumber);
        

        if (numZombiesPerWave > 0 && zombiePrefab != null)
        {
            for (int i = 0; i < numZombiesPerWave; i++)
            {
                Vector3 spawnPos = GetSpawnPosition();
                GameObject newZombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
                activeZombies.Add(newZombie);
                yield return new WaitForSeconds(0.5f);
            }
        }

        

        while (!AllZombiesDestroyed())
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

   

    private bool AllZombiesDestroyed()
    {
        return activeZombies.Count == 0;
    }

    
    private int GetNumZombiesPerWave(int waveIndex)
    {
        int index = waveIndex - startSpawnerAfterWave;
        if (index >= 0 && index < zombiesPerWave.Length)
        {
            return zombiesPerWave[index];
        }
        return 0;
    }

   

    private Vector3 GetSpawnPosition()
    {
        Vector3 spawnPos = transform.position + Random.insideUnitSphere * spawnRadius;
        RaycastHit hit;
        if (Physics.Raycast(spawnPos, Vector3.down, out hit))
        {
            spawnPos.y = hit.point.y;
        }
        return spawnPos;
    }
}
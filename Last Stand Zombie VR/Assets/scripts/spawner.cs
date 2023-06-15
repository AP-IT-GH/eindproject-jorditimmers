using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject zombiePrefab; // Drag your Zombie prefab here
    public GameObject player;  // Drag your Player object here

    private int waveNumber = 1;

    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        while(true)
        {
            for (int i = 0; i < waveNumber * 2; i++)
            {
                // Instantiate the zombie at the position of the object that this script is attached to
                GameObject zombie = Instantiate(zombiePrefab, transform.position, Quaternion.Euler(180, 0, 0));

                // Get the ZombieAgent script from the newly instantiated zombie
                ZombieAgent zombieAgent = zombie.GetComponent<ZombieAgent>();

                // Set the target of the zombie to the player object
                zombieAgent.target = player;

                yield return new WaitForSeconds(3); // You may adjust the time interval between each zombie spawns
            }
            
            waveNumber++;
            yield return new WaitForSeconds(10); // Wait for 10 seconds before spawning the next wave
        }
    }
}

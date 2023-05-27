using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour
{
    private zombieScript zombiescript;

    float timer = 0.0f;

    void Start()
    {
        zombiescript = FindObjectOfType<zombieScript>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        float seconds = timer;

        if (seconds > 5)
        {
            timer = 0.0f; // Reset the timer for the next zombie
            zombiescript.RemoveZombie(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        zombiescript.RemoveZombie(gameObject);
    }
}
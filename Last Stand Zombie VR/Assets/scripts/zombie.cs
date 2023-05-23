using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour
{
    private zombieScript zombiescript;

   // private bool isSpecialZombie = false;
    float timer = 0.0f;

    void Start()
    {
      //  isSpecialZombie = gameObject.CompareTag("specialZombie");
        zombiescript = FindObjectOfType<zombieScript>();
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        float seconds = timer % 60;

        if (seconds > 5)
        {
            zombiescript.RemoveZombie(gameObject);
            Destroy(gameObject);
        }
        /*
        if (isSpecialZombie)
        {
            StartCoroutine(RemoveSpecialZombieAfterDelay());
        }
        */
    }
    /*
    private IEnumerator RemoveSpecialZombieAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        zombiescript.RemoveSpecialZombie(gameObject);
        Destroy(gameObject);
    }
    */
    private void OnDestroy()
    {
        zombiescript.RemoveZombie(gameObject);
    }
}

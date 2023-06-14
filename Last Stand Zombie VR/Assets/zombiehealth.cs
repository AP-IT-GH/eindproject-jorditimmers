using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombiehealth : MonoBehaviour
{
    // Start is called before the first frame update

    private int hitCount = 0;
    
    private Animator animator;
   

    private void Start()
    {
        animator = GetComponent<Animator>();
        

    }


    public void GetHit()
    {
        hitCount++;
        animator.SetTrigger("takeHitTrigger");
        if (hitCount >= 3)
        {

            Destroy(gameObject, 1f);

            Despawn();
            
        }
    }

    private void Despawn()
    {
        // Perform any necessary cleanup or effects here
        // For example, play a death animation or particle effect

        // Destroy the zombie game object
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a bullet
        if (collision.gameObject.CompareTag("bullet"))
        {
            
           

            
                GetHit();
            
        }
    }
}


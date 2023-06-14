using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombiehealth : MonoBehaviour
{
    // Start is called before the first frame update

    private int hitCount = 0;
    
    private Animator animator;
    private bool isMoving = false;
    public GameObject player;
    private bool canAttack = true;
    private float attackCooldown = 7f;
    private float attackTimer = 0f;
    private float attackRange = 2f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");

    }


    public void GetHit()
    {
        hitCount++;
        animator.SetTrigger("takeHitTrigger");
        if (hitCount >= 3)
        {

            Destroy(gameObject, 3f);

            Despawn();
            
        }
    }
    private void Update()
    {
        // Check if the zombie's position is changing
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.01f)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }


        
        // Check if the player is close to the zombie and the attack cooldown has expired
        if ( player != null && Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {transform.LookAt(player.transform);
            if (canAttack) { 
            // Trigger the attack animation
            animator.SetTrigger("attackTrigger");
            
            // Start the attack cooldown timer
            attackTimer = attackCooldown;
            canAttack = false;
        }
        }

        // Update the attack cooldown timer
        if (!canAttack)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                canAttack = true;
            }
        }

        
        
    }

    private void Despawn()
    {
        animator.SetTrigger("isDeath");

        // Delay the despawn by the duration of the death animation
        float deathAnimationDuration = 5f;
        Invoke("DestroyZombie", deathAnimationDuration);
    }

    private void DestroyZombie()
    {
        // Perform any necessary cleanup or effects here
        // For example, play particle effects or trigger game events

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


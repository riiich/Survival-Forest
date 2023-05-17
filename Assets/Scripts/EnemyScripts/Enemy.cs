using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 10;

    GameObject player;
    Animator anim;  //  implementing animations later

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }

    // deal damage to the enemy
    public void takeDamage(float damage)
    {
        this.health -= damage;
        // set animator trigger here
        print(this.health);
        // if the enemy has 0 or less hp, destroy the enemy object
        if (this.health <= 0)
            Die();
    }
    
    // destroy the game object
    void Die()
    {
        Destroy(this.gameObject);
    }
}

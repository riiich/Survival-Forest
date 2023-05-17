using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class BossEnemy : MonoBehaviour
{
    [SerializeField] private float health; // enemy health

    [SerializeField] private float attackCooldown = 2f; // the amount of time it takes between each attack by the enemy
    [SerializeField] private float attackRange = 2f;    // range of the enemies attacks to the player
    [SerializeField] private float aggroDistance = 6f;  // distance from when the enemy will aggro the player

    [SerializeField] GameObject winningItem;    // game object that drops

    GameObject player;
    Animator anim;  //  implementing animations later
    NavMeshAgent navAgent;
    private float timePassed;
    private float newDestinationCooldown = 0.5f;
    
    // Audio
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");    // any gameObject that has the tag 'Player'
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        anim.SetFloat("Speed", navAgent.velocity.magnitude / navAgent.speed);   // setting Speed value for the parameter 'Speed' in the animator to always have it between 0-1

        if (player == null)
            return;

        if (timePassed >= attackCooldown)
        {
            // enemy will attack if player gets within the attack range of the enemy
            if (Vector3.Distance(player.transform.position, transform.position) <= this.attackRange)
            {
                anim.SetTrigger("Attack");
                this.timePassed = 0;    // reset back to 0 so it can attack every attackCooldown seconds
            }
        }
        this.timePassed += Time.deltaTime;

        if ((newDestinationCooldown <= 0) && (Vector3.Distance(player.transform.position, transform.position) <= this.aggroDistance))
        {
            newDestinationCooldown = 0.5f;
            navAgent.SetDestination(player.transform.position);
        }
        newDestinationCooldown -= Time.deltaTime;
        transform.LookAt(player.transform); // rotate to face the player
    }

    // deal damage to the enemy (used in deal damage classes)
    public void takeDamage(float damage)
    {
        this.health -= damage;
        // set animator trigger here
        print("Boss Health: " + this.health);
        anim.SetTrigger("Damaged");
        // if the enemy has 0 or less hp, destroy the enemy object
        if (this.health <= 0)
        {
            Instantiate(winningItem, transform.position, transform.rotation);    // drop item when enemy dies
            Die();
        }
    }

    public void BeginDealDmg()
    {
        GetComponentInChildren<BossDamageDealer>().BeginDealDmg();
    }

    public void EndDealDmg()
    {
        GetComponentInChildren<BossDamageDealer>().EndDealDmg();
    }

    // destroy the game object
    void Die()
    {
        anim.SetTrigger("Dead");
        Destroy(this.gameObject);    // kills the game object after X seconds (set a timer to let the dying animation trigger)
        print("BOSS DEAD!");
    }

    // visually show the attack and aggro range of the enemy
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroDistance);
    }
}

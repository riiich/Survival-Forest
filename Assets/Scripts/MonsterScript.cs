using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    float health = 10;
    GameObject player;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
    }

    public void dmgTaken(float dmgAmt)
    {
        //try to randomize how much health to deal
        this.health -= 2;

        if(health <= 0)
            die();
    }

    void die()
    {
        anim.SetBool("isEnemyDead", true);
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

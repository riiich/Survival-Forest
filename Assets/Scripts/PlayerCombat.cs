using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    Animator anim;

    //combat variables
    public bool isAttacking = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //attack (left mouse button)
        if (Input.GetMouseButton(0))
            anim.SetBool("is", true);
        if (!Input.GetMouseButton(0))
            anim.SetBool("isAttack", false);

        
    }

    public void Attack1()
    {
        if(!isAttacking)
        {
            startAttack();
            anim.SetTrigger("isMeleeMachete");
        }
    }

    public void startAttack()
    {
        isAttacking = true;
    }
}

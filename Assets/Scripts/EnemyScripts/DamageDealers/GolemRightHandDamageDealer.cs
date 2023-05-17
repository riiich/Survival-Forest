using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemRightHandDamageDealer : MonoBehaviour
{
    bool isDealableDmg;
    bool dmgDealt;

    [SerializeField] float lengthOfWeapon;
    [SerializeField] float damage;

    // Start is called before the first frame update
    void Start()
    {
        this.isDealableDmg = false;
        this.dmgDealt = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDealableDmg && !dmgDealt)
        {
            RaycastHit hit;
            int layermask = 1 << 7;

            if (Physics.Raycast(transform.position, -transform.up, out hit, lengthOfWeapon, layermask))
            {
                print("Enemy dealing damage!");
                this.dmgDealt = true;
            }
        }
    }

    public void beginDealDmg()
    {
        this.isDealableDmg = true;
        this.dmgDealt = false;
    }

    public void endDealDmg()
    {
        this.isDealableDmg = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * this.lengthOfWeapon);
    }
}

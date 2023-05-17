using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurDamageDealer : MonoBehaviour
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
                if (hit.transform.TryGetComponent(out HealthSystem health))
                {
                    print("Minotaur dealing " + this.damage + " damage!");
                    health.takeDamage(this.damage); // enemy dealing damage to player
                    this.dmgDealt = true;
                }
            }
        }
    }

    public void BeginDealDmg()
    {
        this.isDealableDmg = true;
        this.dmgDealt = false;
    }

    public void EndDealDmg()
    {
        this.isDealableDmg = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * this.lengthOfWeapon);
    }
}

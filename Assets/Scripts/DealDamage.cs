using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    bool isDamageable;   
    List<GameObject> dmgDealt;  

    [SerializeField] private float lengthOfWeapon;
    [SerializeField] float damage = 3;

    // Start is called before the first frame update
    void Start()
    {
        this.isDamageable = false;
        this.dmgDealt = new List<GameObject>();
    }

    // Update is called once per frame
    void Update() 
    {
        if(this.isDamageable)
        {
            RaycastHit hit;

            int layermask = 1 << 8;     // using layermask to ignore objects that don't want to be damaged

            if(Physics.Raycast(transform.position, -transform.up, out hit, this.lengthOfWeapon, layermask))
            {
                // checks if an enemy is hit
                if(!dmgDealt.Contains(hit.transform.gameObject) && (hit.transform.TryGetComponent(out GolemEnemy golemEnemy)))
                {
                    print(damage + " damage dealt to golem!");
                    golemEnemy.takeDamage(this.damage);
                    this.dmgDealt.Add(hit.transform.gameObject);
                }
                if (!dmgDealt.Contains(hit.transform.gameObject) && (hit.transform.TryGetComponent(out DemonGirlEnemy demonGirlEnemy)))
                {
                    print(damage + " damage dealt to demon girl!");
                    demonGirlEnemy.takeDamage(this.damage);
                    this.dmgDealt.Add(hit.transform.gameObject);
                }
                if (!dmgDealt.Contains(hit.transform.gameObject) && (hit.transform.TryGetComponent(out CrocEnemy crocEnemy)))
                {
                    print(damage + " damage dealt to croc!");
                    crocEnemy.takeDamage(this.damage);
                    this.dmgDealt.Add(hit.transform.gameObject);
                }
                if (!dmgDealt.Contains(hit.transform.gameObject) && (hit.transform.TryGetComponent(out MinotaurEnemy minotaurEnemy)))
                {
                    print(damage + " damage dealt to minotaur!");
                    minotaurEnemy.takeDamage(this.damage);
                    this.dmgDealt.Add(hit.transform.gameObject);
                }
                if (!dmgDealt.Contains(hit.transform.gameObject) && (hit.transform.TryGetComponent(out BossEnemy bossEnemy)))
                {
                    print(damage + " damage dealt to boss!");
                    bossEnemy.takeDamage(this.damage);
                    this.dmgDealt.Add(hit.transform.gameObject);
                }
            }
        }
    }

    public void beginDealDamage()
    {
        this.isDamageable = true;   // weapon can deal damage
        this.dmgDealt.Clear();  // clear the list of gameObjects so that no gameObjects that have already been damaged can be immune from taking damage
    }

    public void endDealDamage()
    {
        this.isDamageable = false;  // weapon can no longer deal damage
    }

    public void gainDamage(float damage)
    {
        this.damage += damage;
    }

    public float getCurrDamage()
    {
        return this.damage;
    }
    
    // if want player to be able to touch and go through this script has to somehow be on the player for this trigger function to work
    //  but this script is applied to the actual weapon itself
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crystal"))
        {
            // gain damage
            gainDamage(1);
            Debug.Log("Gained 1 extra permanent damage!");
            Debug.Log("Current damage: " + getCurrDamage());
            Destroy(other.gameObject);
        }
    }

    // used to get the length of the sword
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * this.lengthOfWeapon);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    // references to the weapon slot, weapon prefab, and sheath slot
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject sheath;

    GameObject weaponOnHand;
    GameObject weaponInSheath;

    // Start is called before the first frame update
    void Start()
    {
        weaponInSheath = Instantiate(weapon, sheath.transform); 
    }
    
    // unsheath weapon
    public void UnsheathWeapon()
    {
        weaponOnHand = Instantiate(weapon, weaponHolder.transform);
        Destroy(weaponInSheath);   //destroy the weapon in the sheath when the player unsheaths the weapon
        Debug.Log("Weapon unsheathed and destroyed");
    }

    // sheath weapon
    public void SheathWeapon()
    {
        weaponInSheath = Instantiate(weapon, sheath.transform);
        Destroy(weaponOnHand);     // destroy the weapon when the player sheaths the weapon
        Debug.Log("Weapon sheathed and destroyed");
    }

    public void beginDealDamage()
    {
        //calling every component in the children of type DealDamage and deals damage
        weaponOnHand.GetComponentInChildren<DealDamage>().beginDealDamage();
    }

    public void endDealDamage()
    {
        this.weaponOnHand.GetComponentInChildren<DealDamage>().endDealDamage();
    }
}

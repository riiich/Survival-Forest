using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float maxHealth = 30f;
    float currHealth;
    Animator anim;
    public HealthBar healthBar;

    // Audio
    private AudioSource PlayerDamagedAudio;

    // Start is called before the first frame update
    void Start()
    {
        this.currHealth = this.maxHealth;
        healthBar.setMaxHealth(this.maxHealth);
        anim = GetComponent<Animator>();    
    }

    public void takeDamage(float damage)
    {
        this.currHealth -= damage;
        anim.SetTrigger("Damaged"); // player animation for taking damage
        print("Ty Charter's current health: " + this.currHealth);

        healthBar.SetHealth(this.currHealth);

        if(!isAlive())
        {
            print("You Died!");
            anim.SetTrigger("Dead");
            Die();
            SceneManager.LoadScene("DeathScreen");  // change scenes to death screen
        }
    }

    public void gainHealth(float health)
    {
        this.currHealth += health;
        this.healthBar.SetHealth(this.currHealth);

        if (this.currHealth >= this.maxHealth)
            this.currHealth = this.maxHealth;
    }

    public void gainPermanentHealth(float health)
    {
        this.maxHealth += health;
        this.healthBar.setMaxHealth(this.maxHealth);
    }

    // checks whether the player is alive or not
    bool isAlive()
    {
        if (this.currHealth <= 0)
            return false;

        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potion"))
        {
            gainHealth(5);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Coin"))
        {
            // gain more health
            gainPermanentHealth(2);
            Debug.Log("Gained 2 extra permanent health!");
            Debug.Log("Current max health: " + this.maxHealth);
            Destroy(other.gameObject);
        }

        // this should be in another script
        if (other.CompareTag("Star"))
        {
            Debug.Log("WIN!!!!");
            Destroy(other.gameObject);
            SceneManager.LoadScene("VictoryScreen");    // change screen to victory screen
        }
    }

    // destroys the player object in X seconds
    void Die()
    {
        Destroy(this.gameObject, 4);
    }
}

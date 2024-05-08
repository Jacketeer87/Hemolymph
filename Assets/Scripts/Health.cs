using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth; //external max health
    public int currentHealth = 1; // Current health
    public bool dead = false;
    public float timer = 0f;
    public float interval = 2f;
    [SerializeField] GameManager gm;


    void Update(){
        timer += Time.deltaTime;
    }
    public void Initialize(int initialMaxHealth)
    {
        maxHealth = initialMaxHealth; // Set max health externally
        currentHealth = maxHealth; // Initialize health
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount; // Reduce health
        Debug.Log(gameObject.name + " takes " + amount + " damage.");
        if(gameObject.tag == "Player"){
            EnemyCounter.singleton.SubtractHealth(amount);
        }
        if (currentHealth <= 0)
        {
            Die(); // Call the Die method if health is 0 or less
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount; // Increase health
        currentHealth = Mathf.Min(currentHealth, maxHealth); // Prevent health from going over max
        Debug.Log(gameObject.name + " heals " + amount + " health.");
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died.");
        if(gameObject.tag == "Player"){
            Destroy(gameObject);
            gm.GameOver();
        }
        else{
        gameObject.GetComponent<AudioSource>().Play();
        Destroy(gameObject);
        EnemyCounter.singleton.enemyKill();
        dead = true;
        }
    }

    public bool IsDead(){
        return dead;
    }
}

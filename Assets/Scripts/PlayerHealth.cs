using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHP = 100;
    private float currentHP;

    public float CurrentHP => currentHP;
    public float MaxHP => maxHP;

    public Animator animator;

    private bool isDead = false;
    public GameOverScreen gameOverScreen;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage) // function to take damage
    {
        if (isDead) // return if the player is not dead
        {
            return;
        }

        currentHP -= damage; //reduces damage after hit by enemy
        Debug.Log("Player =" + currentHP);

        if (animator != null)
        {
            animator.SetTrigger("Hit"); // Plays hit animation
        }

        if (currentHP <= 0)
        {
            Die(); // destroys the player object
        }
    }

    public void Heal(float increaseHP)// function to heal the player
    {
        if (currentHP >= maxHP)// do nothing if health is full
        {
            return;
        }

        currentHP += increaseHP;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP); // to avoid over healing of the player

        Debug.Log("Player healed by " + increaseHP + ", current HP: " + currentHP);
    }

    void Die()
    {
        isDead = true;
        if (animator != null)
        {
            animator.SetTrigger("Die"); // plays death animation
        }

        if (gameOverScreen != null)
        {
            gameOverScreen.TriggerGameOver(); // displays game over screen
        }
        // Destroys after animation
        Destroy(gameObject, 2f); //destroys the player object after a certain delay
    }
}

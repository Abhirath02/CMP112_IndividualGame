using UnityEngine;
using static Unity.VisualScripting.Member;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 50;
    private int currentHP;

    public AudioSource source;
    public AudioClip death;

    public Animator animator;

    private bool isDead = false;

    void Start()
    {
        currentHP = maxHP;
    }

    //function to receive damage
    public void TakeDamage(int damage)
    {
        if (isDead) // returns if enemy is not dead
        {
            return;
        }

        currentHP -= damage;
        Debug.Log("OGRE =" + currentHP);
        
        if (animator != null)
        {
            animator.SetTrigger("Hit"); // Plays hit animation
        }

        if (currentHP <= 0)
        {
            Die(); // destroys the enemy
        }
    }

    void Die()
    {
        isDead = true;
        if (animator != null)
        {
            animator.SetTrigger("Die"); // Plays death animation
        }
        source.PlayOneShot(death);
        // Destroys after animation
        Destroy(gameObject, 2f);
    }
}
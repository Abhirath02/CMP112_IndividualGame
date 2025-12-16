using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 50;
    private int currentHP;

    public Animator animator;

    private bool isDead = false;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
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
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
        // Destroy after animation
        Destroy(gameObject, 2f);
    }
}
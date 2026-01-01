using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHP = 100;
    private float currentHP;

    public float CurrentHP => currentHP;
    public float MaxHP => maxHP;

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
        Debug.Log("Player =" + currentHP);

        if (animator != null)
        {
            animator.SetTrigger("Hit"); // Plays hit animation
        }

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Heal(float increaseHP)
    {
        if (currentHP >= maxHP) return; // do nothing if full health

        currentHP += increaseHP;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        Debug.Log("Player healed by " + increaseHP + ", current HP: " + currentHP);
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

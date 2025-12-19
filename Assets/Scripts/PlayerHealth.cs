using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHP = 100;
    private float currentHP;

    public float CurrentHP => currentHP;
    public float MaxHP => maxHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int dmg)
    {
        currentHP -= dmg;
        Debug.Log("Player HP: " + currentHP);

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
        Debug.Log("Player Died");
        Destroy(gameObject);
    }
}
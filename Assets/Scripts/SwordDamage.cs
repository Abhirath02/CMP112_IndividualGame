using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public int minDamage = 10;
    public int maxDamage = 15;
    public float activeTime = 0.3f; // the duration in which the sword can deal damage

    private bool active = false;

    public void Activate()
    {
        if (!active)
        {
            StartCoroutine(DoDamageWindow());
        }
    }

    private System.Collections.IEnumerator DoDamageWindow()
    {
        active = true;
        yield return new WaitForSeconds(activeTime);
        active = false;
    }

    private void OnTriggerEnter(Collider other) //function to damage the enemy after the sword collides with the enemy
    {
        Debug.Log("Sword collided with: " + other.name);
        if (!active)
        {
            return;
        }

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            int dmg = Random.Range(minDamage, maxDamage);
            enemy.TakeDamage(dmg);
            Debug.Log("Dealt " + dmg + " damage!");
        }
    }
}
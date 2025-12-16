using UnityEngine;

public class SwordDamage : MonoBehaviour
{
    public int minDamage = 5;
    public int maxDamage = 12;
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

    private void OnTriggerEnter(Collider other)
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
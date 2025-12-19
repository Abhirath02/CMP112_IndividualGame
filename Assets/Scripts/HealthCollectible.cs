using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public float increaseHP;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null && player.CurrentHP < player.MaxHP) // only heal if HP < max
        {
            Debug.Log("The pickup collided and healed player");
            player.Heal(increaseHP);

            Destroy(gameObject); // remove the prefab after pickup
        }
    }
}
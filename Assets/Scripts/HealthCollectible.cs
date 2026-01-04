using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public float increaseHP;

    public AudioSource source;
    public AudioClip pickup;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) // if the player does not collide with the item return 0
        {
            return;
        }

        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null && player.CurrentHP < player.MaxHP) // only heal if HP < max
        {
            AudioSource.PlayClipAtPoint(pickup, transform.position);
            Debug.Log("The pickup collided and healed player");
            player.Heal(increaseHP);

            Destroy(gameObject); // destroy the item after pickup
        }
    }
}
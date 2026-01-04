using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    //moves along with the player
    void LateUpdate()
    {
        if (player == null)
        {
            return;
        }

        Vector3 targetPos = player.transform.position + offset;
        transform.position = targetPos;

    }
}
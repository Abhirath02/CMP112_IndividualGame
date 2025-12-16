using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; // drag player from Hierarchy
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

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
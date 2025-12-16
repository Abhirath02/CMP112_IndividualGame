using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    [Header("Target")]
    public Transform player;
    public Vector3 lookOffset = new Vector3(0, 1.5f, 0);

    [Header("Camera Settings")]
    public float distance = 5f;
    public float height = 2f;
    public float sensitivity = 200f;
    public float smoothSpeed = 10f;

    private float yaw;
    private float pitch = 15f; 

    private InputSystem_Actions controls;
    private Vector2 lookInput;

    private Vector3 currentVelocity; 

    private void Awake()
    {
        controls = new InputSystem_Actions();

        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    void LateUpdate()
    {
        if (player == null)
        {
            return;
        }
        yaw += lookInput.x * sensitivity * Time.deltaTime;

        Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPos = player.position + rot * new Vector3(0, 0, -distance) + Vector3.up * height;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPos, ref currentVelocity, 1f / smoothSpeed);

        transform.LookAt(player.position + lookOffset);
    }
}
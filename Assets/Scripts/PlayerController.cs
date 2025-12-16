using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public GameObject sword;
    public Animator animator;
    public AudioSource audioSource;
    public Transform cam;

    [Header("Movement")]
    public float speed = 5f;
    public float sprintMultiplier = 1.8f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private InputSystem_Actions controls;
    private Vector2 movementInput;
    private bool isSprinting = false;
    private bool isGrounded = true;

    [Header("Combat")]
    public float attackCooldown = 1f;
    public int minDamage = 5;
    public int maxDamage = 12;
    public AudioClip swordSwingSFX;
    private bool canAttack = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new InputSystem_Actions();

        // Movement input
        controls.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => movementInput = Vector2.zero;

        // Attack input
        controls.Player.Attack.performed += ctx => Attack();

        // Jump input
        controls.Player.Jump.performed += ctx => Jump();

        // Sprint input
        controls.Player.Sprint.performed += ctx => isSprinting = true;
        controls.Player.Sprint.canceled += ctx => isSprinting = false;
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();

    private void FixedUpdate()
    {
        // camera movement
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 move = camForward * movementInput.y + camRight * movementInput.x;

        float currentSpeed = speed * (isSprinting ? sprintMultiplier : 1f);

        rb.MovePosition(rb.position + move * currentSpeed * Time.fixedDeltaTime);

        Vector3 faceDirection = cam.forward; // face forward
        faceDirection.y = 0f;

        if (faceDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(faceDirection, Vector3.up);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRot, 15f * Time.fixedDeltaTime);
        }

        // Animaton
        animator.SetFloat("MoveInputX", movementInput.x);
        animator.SetFloat("MoveInputY", movementInput.y);
        animator.SetBool("Sprint", isSprinting);
    }

 
    private void Attack() // attack function
    {
        if (!canAttack)
        {
            return;
        }

        animator.SetTrigger("Attack");

        if (audioSource != null && swordSwingSFX != null)
        {
            audioSource.PlayOneShot(swordSwingSFX);
        }

        if (sword != null)
        {
            sword.GetComponent<SwordDamage>().Activate();
        }

        canAttack = false;
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    private void Jump()// jump function
    {
        if (!isGrounded)
        {
            return;
        }
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        animator.SetTrigger("Jump");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
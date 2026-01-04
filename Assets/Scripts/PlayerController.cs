using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public GameObject sword;
    public Animator animator;
    public AudioSource source;
    public Transform cam;

    [Header("Movement")]
    public float speed = 5f;
    public float sprintMultiplier = 1.8f;


    private Rigidbody rb;
    private InputSystem_Actions controls;
    private Vector2 movementInput;
    private bool isSprinting = false;

    [Header("Combat")]
    public float attackCooldown = 1f;
    public int minDamage = 5;
    public int maxDamage = 12;
    public AudioClip swordSwingSFX;
    private bool canAttack = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new InputSystem_Actions(); // using the new input system

        // Movement input
        controls.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>(); //to perform movement
        controls.Player.Move.canceled += ctx => movementInput = Vector2.zero; // to cancel movement

        // Attack input
        controls.Player.Attack.performed += ctx => Attack(); //left click to attack


        // Sprint input
        controls.Player.Sprint.performed += ctx => isSprinting = true;
        controls.Player.Sprint.canceled += ctx => isSprinting = false;
    }

    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();

    private void FixedUpdate()
    {
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * movementInput.y + camRight * movementInput.x;
        float moveMagnitude = new Vector2(movementInput.x, movementInput.y).magnitude;
        animator.SetFloat("MoveSpeed", moveMagnitude);

        // Calculates the horizontal velocity
        Vector3 horizontalVelocity = moveDir * speed * (isSprinting ? sprintMultiplier : 1f);

        // Keeps the current vertical velocity for gravity to take effect
        Vector3 velocity = new Vector3(horizontalVelocity.x, rb.linearVelocity.y, horizontalVelocity.z);
        rb.linearVelocity = velocity;

        // Rotates the player to face camera direction
        Vector3 faceDirection = cam.forward;
        faceDirection.y = 0f;

        if (faceDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(faceDirection, Vector3.up);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRot, 15f * Time.fixedDeltaTime);
        }

        //animations of movements
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

        if (source != null && swordSwingSFX != null) //plays sword sfx
        {
            source.PlayOneShot(swordSwingSFX);
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

}
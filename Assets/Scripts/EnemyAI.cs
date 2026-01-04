using System.Collections;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class EnemyAI : MonoBehaviour
{
    [Header("Detection & Combat")]
    public GameObject player;
    public float detectionRange = 5f;
    public float attackRange = 2f;
    public float moveSpeed = 3f;
    public float attackCooldown = 1.5f;
    public int damage;
    public AudioSource source;
    public Animator animator;

    [Header("Patrol")]
    public float patrolRadius = 5f;
    public float patrolTime = 3f;

    private Vector3 patrolDirection;
    private float patrolTimer;

    [Header("Attack")]
    private bool canAttack = true;
    public AudioClip clubSwingSFX;

    private Rigidbody rb;

    private enum State { Patrol, Chase, Attack }
    private State currentState = State.Patrol;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetNewpatrolDirection();
    }
    void Update()
    {
        if (player == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch (currentState)
        {
            case State.Patrol: 
                Patrol();

                animator.SetBool("IsChasing", false);

                if (distanceToPlayer <= detectionRange)
                {
                    currentState = State.Chase;
                    animator.SetTrigger("Detected");
                    animator.SetBool("IsChasing", true);
                }
                break;

            case State.Chase:
                Chase();
                animator.SetBool("IsChasing", true);

                if (distanceToPlayer <= attackRange)
                {
                    currentState = State.Attack;
                }
                else if (distanceToPlayer > detectionRange)
                {
                    currentState = State.Patrol;
                }
                break;

            case State.Attack:
                animator.SetBool("IsChasing", false);
                Attack();

                if (distanceToPlayer > attackRange)
                { 
                currentState = State.Chase;
                }
                break;
        }
    }
    void Patrol() // patrol function for the enemy to move from one direction to another
    {
        patrolTimer -= Time.deltaTime;

        if (patrolTimer <= 0)
        {
            SetNewpatrolDirection();
        }

        rb.MovePosition(transform.position + patrolDirection * moveSpeed * Time.deltaTime);
        FaceTarget(patrolDirection);

        if (animator != null)
        {
            Vector3 localDir = transform.InverseTransformDirection(patrolDirection);
            animator.SetFloat("EnemyMoveX", localDir.x);
            animator.SetFloat("EnemyMoveY", localDir.z);
        }
    }
    void SetNewpatrolDirection() // to set the direction the enemy will patrol to
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        patrolDirection = new Vector3(randomDir.x, 0, randomDir.y);
        patrolTimer = patrolTime;
    }
    void Chase() // function to chase the player
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
        FaceTarget(direction);

        if (animator != null)
        {
            Vector3 localDir = transform.InverseTransformDirection(direction);
            animator.SetFloat("EnemyMoveX", localDir.x);
            animator.SetFloat("EnemyMoveY", localDir.z);
        }
    }
    void Attack() // to attack the player
    {

        if (!canAttack)
        {
            return;
        }

        animator.SetTrigger("EnemyAttack");

        if (source != null && clubSwingSFX != null)
        {
            source.PlayOneShot(clubSwingSFX);
        }

        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(damage);
        }
        canAttack = false;
        Invoke(nameof(ResetAttack), attackCooldown);
    }
    void ResetAttack()
    {
        canAttack = true;
    }
    void FaceTarget(Vector3 direction) //to face the player
    {
        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 10f * Time.deltaTime);
        }
    }
    private void OnDrawGizmos()// to create a radius from which the enemy can detect the player from
    {
        Gizmos.color = Color.red;
        if (player != null)
        {
            Gizmos.DrawWireSphere(player.transform.position, detectionRange);
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
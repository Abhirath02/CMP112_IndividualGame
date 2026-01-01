using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [Header("Detection & Combat")]
    public GameObject player;          // Assign player GameObject in Inspector
    public float detectionRange = 5f;
    public float attackRange = 2f;
    public float moveSpeed = 3f;
    public float attackCooldown = 1.5f;
    public int damage;
    public Animator animator;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    private bool canAttack = true;
    private Rigidbody rb;

    private enum State { Patrol, Chase, Attack }
    private State currentState = State.Patrol;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
    void Patrol()
    {
        if (patrolPoints.Length == 0)
        {
            return;
        }

        Transform targetPoint = patrolPoints[currentPatrolIndex];
        Vector3 direction = (targetPoint.position - transform.position).normalized;

        rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
        FaceTarget(direction);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        if (animator != null)
        {
            Vector3 patrolDir = transform.InverseTransformDirection((patrolPoints[currentPatrolIndex].position - transform.position).normalized);
            animator.SetFloat("EnemyMoveX", patrolDir.x);
            animator.SetFloat("EnemyMoveY", patrolDir.z);
        }
    }
    void Chase()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
        FaceTarget(direction);

        if (animator != null)
        {
            // Use world or local direction for blend tree
            Vector3 localDir = transform.InverseTransformDirection(direction);
            animator.SetFloat("EnemyMoveX", localDir.x);
            animator.SetFloat("EnemyMoveY", localDir.z); // z is forward in Unity
        }
    }
    void Attack()
    {

        if (!canAttack)
        {
            return;
        }

        animator.SetTrigger("EnemyAttack");

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
    void FaceTarget(Vector3 direction)
    {
        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, 10f * Time.deltaTime);
        }
    }
    private void OnDrawGizmos()
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
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference movement, attack;
    [Header("References")]
    private CharacterController controller;
    private Animator animator;
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5.0F;
    [SerializeField] private float rotateSpeed = 0.3F;
    [Header("Combat")]
    [SerializeField] private float detectionRadius = 4.0F;
    [Header("Input")]
    private Vector2 moveDirection;
    private GameObject closestEnemy;
    [Header("Public")]
    public bool inCombat = false;
    private void Start()
    {
        Transform childTransform = transform.Find("Walking");
        GameObject childGameObject = childTransform.gameObject;
        controller = GetComponent<CharacterController>();
        animator = childGameObject.GetComponent<Animator>();
        movement.action.Enable();
        attack.action.Enable();
    }

    private void Update()
    {
        InputManagement();
        Movement();

        //Animations
        if (moveDirection == Vector2.zero)
            animator.SetFloat("Speed", 0);
        else
            animator.SetFloat("Speed", 1);
    }

    private void Movement()
    {
        GroundMovement();
    }

    private void GroundMovement()
    {
        transform.Rotate(0, moveDirection.x * rotateSpeed, 0);
        var forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = walkSpeed * moveDirection.y;
        controller.SimpleMove(forward * curSpeed);
    }

    private void InputManagement()
    {
        moveDirection = movement.action.ReadValue<Vector2>();

        // Check if attack button is pressed and set Punching parameter
        if (attack.action.WasPressedThisFrame() && !animator.GetCurrentAnimatorStateInfo(0).IsName("Punch") && !animator.GetBool("Punching"))
        {
            animator.SetBool("Punching", true);
            FindClosestEnemy();
        }
        else
        {
            animator.SetBool("Punching", false);
        }
    }

    private void FindClosestEnemy()
    {
        // Find all game objects with the "Enemy" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // If no enemies found, return
        if (enemies.Length == 0)
        {
            Debug.Log("No enemies found");
            return;
        }

        // Get the player's forward direction
        Vector3 playerForward = transform.forward;

        // Find the closest enemy that is in front of the player
        float closestDistance = Mathf.Infinity;
        closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            // Calculate direction from player to enemy
            Vector3 directionToEnemy = enemy.transform.position - transform.position;
            directionToEnemy.Normalize();

            // Calculate dot product to determine if enemy is in front of player
            float dotProduct = Vector3.Dot(playerForward, directionToEnemy);

            // Only consider enemies that are in front of the player (dot product > 0)
            if (dotProduct > 0)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                // Check if this enemy is within detection radius and closer than the current closest
                if (distance < detectionRadius && distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }

        if (closestEnemy != null)
        {
            Debug.Log("Found closest enemy in front: " + closestEnemy.name + " at distance: " + closestDistance);
            var enemyCombat = closestEnemy.GetComponent<EnemyAI>();
            if (enemyCombat)
            {
                enemyCombat.inCombat = true;
                inCombat = true;
            }
        }
    }
}

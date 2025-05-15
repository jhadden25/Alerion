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
    [Header("Input")]
    private Vector2 moveDirection;
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
        if(moveDirection == Vector2.zero)
        animator.SetFloat("Speed", 0);
        else
        animator.SetFloat("Speed", 1);
    }

    private void Movement(){
        GroundMovement();
    }

    private void GroundMovement() {
        transform.Rotate(0, moveDirection.x * rotateSpeed, 0);
        var forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = walkSpeed * moveDirection.y;
        controller.SimpleMove(forward * curSpeed);
    }

    private void InputManagement()
    {
        moveDirection = movement.action.ReadValue<Vector2>();
        
        // Check if attack button is pressed and set Punching parameter
        if (attack.action.WasPressedThisFrame())
        {
            animator.SetBool("Punching", true);
        }
        else if (attack.action.WasReleasedThisFrame())
        {
            animator.SetBool("Punching", false);
        }
    }
}

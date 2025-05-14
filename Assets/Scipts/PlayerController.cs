using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference movement;
    [Header("References")]
    private CharacterController controller;
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [Header("Input")]
    private Vector2 moveDirection;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        movement.action.Enable();
    }

    private void Update()
    {
        InputManagement();
        Movement();
    }

    private void Movement(){
        GroundMovement();
    }

    private void GroundMovement() {
        transform.Rotate(0, moveDirection.x * 0.5F, 0);
        var forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = 5.0F * moveDirection.y;
        controller.SimpleMove(forward * curSpeed);
        // Vector3 move = new Vector3(moveDirection.x, 0, moveDirection.y);
        // move.y = 0;
        // move *= walkSpeed;
        // controller.Move(move * Time.deltaTime);
    }

    private void InputManagement()
    {
        moveDirection = movement.action.ReadValue<Vector2>();
    }
}

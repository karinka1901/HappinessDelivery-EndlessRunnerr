using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private PlayerInputActions inputActions;

    public float speed = 5f;
    private Rigidbody rb;

    private Vector2 horizontalInput;
    [SerializeField] private float horizontalMultiplier = 0.5f;

    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private bool isGrounded;


    private void Awake()
    {
       animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();

        inputActions = new PlayerInputActions();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Jump.performed += OnJump;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void FixedUpdate()
    {
        Vector3 forwardMove = speed * Time.fixedDeltaTime * transform.forward;
        Vector3 horizontalMove = horizontalMultiplier * speed * Time.fixedDeltaTime * horizontalInput;
        rb.MovePosition(rb.position +  forwardMove + horizontalMove);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        horizontalInput = inputActions.Player.Move.ReadValue<Vector2>();
        if (horizontalInput != null)
        {
            if (horizontalInput.sqrMagnitude > 0 && isGrounded)
            {
                Jump();
            }
            else
            {
                DebugColor.Log("No input detected or player is not grounded.", "magenta");
            }
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger("Jump");
       isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }


}

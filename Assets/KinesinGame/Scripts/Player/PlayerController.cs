using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [HideInInspector]public Animator animator;
    private PlayerInputActions inputActions;
    private Rigidbody rb;

    [Header("Movement Settings")]
    private Vector2 horizontalInput;
    public float speed = 10f;

    [Header("Lane Settings")]
    [SerializeField] private float laneDistance = 3f; // Distance between lanes
    [SerializeField] private float laneChangeSpeed = 5f; 
    [SerializeField] private int currentLane = 1; // 0: Left, 1: Center, 2: Right
    [SerializeField] private bool isChangingLane = false;

    [Header("Jump Settings")]    
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float hopForce = 3f; // Force for lane change jump
    [SerializeField] private bool isGrounded;
    [SerializeField] private float fallMultiplier = 2.5f; // Multiplier for faster falling

    [Header("Stun Settings")]
    [SerializeField] private float knockbackForce = 6f;
    private bool isStunned;
    [SerializeField] private float stunDuration = 0.7f;
    public bool isInvulnerable;


    private void Awake()
    {
       animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();

        inputActions = new PlayerInputActions();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Jump.performed += OnJump;
        inputActions.Player.Move.performed += OnCrouch;
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
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver || isStunned) return;

        Vector3 currentPosition = rb.position;

        //always move forward
        currentPosition += speed * Time.fixedDeltaTime * transform.forward;

        //move towards the target lane
        float targetPos = (currentLane - 1) * laneDistance; // Calculate target X position based on current lane
        currentPosition.x = Mathf.MoveTowards(currentPosition.x, targetPos, laneChangeSpeed * Time.fixedDeltaTime);

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += (fallMultiplier - 1) * Physics.gravity.y * Time.fixedDeltaTime * Vector3.up;
        }

        rb.MovePosition(currentPosition);
    }

    private void OnMove(InputAction.CallbackContext context) // Handle horizontal input for lane changes
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        horizontalInput = context.ReadValue<Vector2>();

        if (horizontalInput.x > 0.5f && currentLane < 2 && isGrounded) // Move right
        {
            currentLane++;
            isChangingLane = true;
            if (isGrounded) Hop();

        }
        else if (horizontalInput.x < -0.5f && currentLane > 0 && isGrounded) // Move left
        {
            currentLane--;
            isChangingLane = true;
            if (isGrounded) Hop();

        }
    }

    #region Jump
    private void OnJump(InputAction.CallbackContext context)
    {
        if(GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        if (isGrounded && !isChangingLane)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger("Jump");
        isGrounded = false;
    }

    private void Hop() //lane change jump
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * hopForce, ForceMode.Impulse);
        animator.SetTrigger("Jump");
        isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isChangingLane = false;
        }
    }
    #endregion

    #region Crouch

    private void OnCrouch(InputAction.CallbackContext context)
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        Vector2 verticalInput = context.ReadValue<Vector2>();

        if (verticalInput.y < -0.2f && isGrounded && !isChangingLane)
        {
            animator.SetTrigger("Crouch");
        }
            
    }

    #endregion

    #region Death & Hurt
    public void Die()
    {
        animator.SetTrigger("Death");
        speed = 0f;
        rb.isKinematic = true;
    }

    public void Hurt()
    { 
        Vector3 knockback = (-transform.forward + Vector3.up * 0.4f) * knockbackForce;
        rb.AddForce(knockback, ForceMode.Impulse);

        animator.SetTrigger("Hurt");

        StartCoroutine(Stun());
    }

    public IEnumerator Stun()
    {
        isStunned = true;
        isInvulnerable = true;

        float originalSpeed = speed;
        speed = 0f;

        yield return new WaitForSeconds(stunDuration);

        speed = originalSpeed;
        isStunned = false;

        yield return new WaitForSeconds(2f);

        isInvulnerable = false;
    }


    #endregion
}

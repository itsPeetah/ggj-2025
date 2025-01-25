using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rbody;
    public Transform leftFoot, rightFoot;
    public Transform visualsRoot;


    [Header("Values")]
    public float fallGravity = 10f;
    public float jumpGravity = 20f;
    public float jumpForce = 10f;
    public float maxJumpDuration = 1f;
    public float walkSpeed = 6f;
    public float jumpBufferWindow = 0.2f;


    [Header("Groundcheck")]
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    // State
    private float currentGravityScale = 0;
    [HideInInspector] public Vector2 currentMoveInput = Vector2.zero;
    private bool currentJumpInput = false;
    private bool previousJumpInput = false;
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool queueJump = false;
    private float queueJumpTime = 0;
    private float jumpTimeRemaining = 0;
    private bool facingLeft = false;

    public bool FacingLeft => facingLeft;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Handle jumping
        if (currentJumpInput && !previousJumpInput)
        {
            queueJump = true;
            queueJumpTime = Time.time;
        }

        if (!isGrounded && Time.time - queueJumpTime >= jumpBufferWindow)
        {
            queueJump = false;
        }

        previousJumpInput = currentJumpInput;

        // Handle direction
        bool prevFacingLeft = facingLeft;
        if (currentMoveInput.x < 0) facingLeft = true;
        else if (currentMoveInput.x > 0) facingLeft = false;

        if (prevFacingLeft != facingLeft)
        {
            Vector3 scale = visualsRoot.transform.localScale;
            scale.x *= -1;
            visualsRoot.transform.localScale = scale;
        }
    }

    private void FixedUpdate()
    {
        CheckGround();

        Vector2 vel = rbody.linearVelocity;
        vel.x = currentMoveInput.x * walkSpeed;

        // Initiate jump
        if (queueJump && isGrounded && !isJumping)
        {
            jumpTimeRemaining = maxJumpDuration;
            isJumping = true;
            queueJump = false;
        }
        if (isJumping)
        {
            if (!currentJumpInput || jumpTimeRemaining <= 0)
                isJumping = false;
            jumpTimeRemaining -= Time.fixedDeltaTime;
        }


        // Jump vs fall/airborne
        if (isJumping)
        {
            currentGravityScale = Mathf.Abs(jumpGravity / Physics2D.gravity.y);
            vel.y = jumpForce;
        }
        else
        {
            currentGravityScale = Mathf.Abs(fallGravity / Physics2D.gravity.y);
        }

        // apply physics
        rbody.gravityScale = currentGravityScale;
        rbody.linearVelocity = vel;
    }

    public void HandleMoveInput(InputAction.CallbackContext ctx)
    {
        currentMoveInput = ctx.ReadValue<Vector2>();
    }

    public void HandleJumpInput(InputAction.CallbackContext ctx)
    {
        currentJumpInput = ctx.performed;
    }

    private void CheckGround()
    {
        bool left = Physics2D.OverlapCircle(leftFoot.position, groundCheckRadius, whatIsGround) != null;
        bool right = Physics2D.OverlapCircle(rightFoot.position, groundCheckRadius, whatIsGround) != null;
        isGrounded = left || right;
    }
}

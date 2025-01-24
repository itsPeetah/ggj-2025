using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class Movement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rbody;
    public Transform leftFoot, rightFoot;


    [Header("Values")]
    public float fallGravity = 10f;
    public float jumpGravity = 20f;
    public float jumpForce = 10f;
    public float maxJumpDuration = 1f;
    public float walkSpeed = 6f;
    public float acceleration = 100f;
    public float jumpBufferWindow = 0.2f;


    [Header("Groundcheck")]
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    // State
    private float currentSpeed = 0;
    private float currentAcceleration = 0;
    private float currentGravityScale = 0;
    private Vector2 currentMoveInput = Vector2.zero;
    private bool currentJumpInput = false;
    private bool previousJumpInput = false;
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool queueJump = false;
    private float queueJumpTime = 0;
    private float jumpTimeRemaining = 0;

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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
    }

    private void FixedUpdate()
    {
        CheckGround();
        Vector2 vel = rbody.linearVelocity;

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


        vel.x = currentMoveInput.x * walkSpeed;

        if (isJumping)
        {
            currentGravityScale = Mathf.Abs(jumpGravity / Physics2D.gravity.y);
            vel.y = jumpForce;
        }
        else
        {
            currentGravityScale = Mathf.Abs(fallGravity / Physics2D.gravity.y);
        }



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

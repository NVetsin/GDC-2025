using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public InputActionReference userInput;

    // Components
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D playerRigidBody;

    // Variables
    [SerializeField] private float playerMovementSpeed = 7f;
    [SerializeField] private float playerJumpPower = 9f;
    [SerializeField] private float playerWallSlidingSpeed = 2f;
    private bool isWallSliding = false;
    private bool isWallJumping = false;
    private Vector2 playerDirection;
    private int wallJumpingDirection;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        playerDirection = userInput.action.ReadValue<Vector2>();
        playerMove();
        playerJump();
        playerWallSliding();
        playerWallJumping();

        if (isOnGround())
            isWallJumping = false;

        if (!isWallSliding)
            playerFaceDirection();
    }

    private void playerFaceDirection()
    {
        // Change player direction
        if (playerDirection.x > 0)
            transform.localScale = new Vector2(1, 1);
        else if (playerDirection.x < 0)
            transform.localScale = new Vector2(-1, 1);

        wallJumpingDirection = (int) Mathf.Ceil(playerDirection.x) * -1;
    }

    private void playerMove()
    {
        if (isWallJumping)
            return;

        playerRigidBody.linearVelocity = new Vector2(playerDirection.x * playerMovementSpeed, playerRigidBody.linearVelocity.y);
    }

    private void playerJump()
    {
        if (isOnGround() && playerDirection.y > 0)
        {
            playerRigidBody.linearVelocity = new Vector2(playerRigidBody.linearVelocity.x, playerJumpPower);
        }
    }

    private void playerWallSliding()
    {
        if (isWall() && !isOnGround() && playerDirection.x != 0)
        {
            isWallSliding = true;
            playerRigidBody.linearVelocity = new Vector2(playerRigidBody.linearVelocity.x, Mathf.Clamp(playerRigidBody.linearVelocity.y, -playerWallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void playerWallJumping()
    {
        if (isWallSliding && playerDirection.y > 0)
        {
            isWallJumping = true;
            playerRigidBody.linearVelocity = new Vector2(wallJumpingDirection * playerMovementSpeed, playerJumpPower);
        }
    }

    private bool isOnGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool isWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
}

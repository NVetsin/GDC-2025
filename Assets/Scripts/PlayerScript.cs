using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public InputActionReference userInput;

    // Components
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D playerRigidBody;

    // Variables
    [SerializeField] private float playerMovementSpeed = 7.0f;
    [SerializeField] private float playerJumpPower = 12.0f;
    private Vector2 playerDirection;

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
        playerMove();
    }

    private void playerMove()
    {
        playerDirection = userInput.action.ReadValue<Vector2>();
        Debug.Log(playerDirection);

        if (playerDirection.x > 0)
            transform.localScale = new Vector2(1, 1);
        else if (playerDirection.x < 0)
            transform.localScale = new Vector2(-1, 1);

        if (isOnGround() && playerDirection.y > 0)
            playerRigidBody.linearVelocity = new Vector2(playerDirection.x * playerMovementSpeed, playerDirection.y * playerJumpPower);
        else
            playerRigidBody.linearVelocity = new Vector2(playerDirection.x * playerMovementSpeed, playerRigidBody.linearVelocity.y);
    }

    private bool isOnGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}

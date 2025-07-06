using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public InputActionReference userInput;

    // 
    [SerializeField] private TextMeshPro healthText;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D playerRigidBody;
    private GameObject playerChatBubble;
    private RectTransform playerChatBubbleRect;
    private TextMeshPro playerChatBubbleText;

    // Variables
    [SerializeField] private int playerHealth = 100;
    [SerializeField] private float playerMovementSpeed = 7f;
    [SerializeField] private float playerJumpPower = 9f;
    [SerializeField] private float playerWallSlidingSpeed = 2f;
    private bool isInvincible = false;
    private bool isWallSliding = false;
    private bool isWallJumping = false;
    private Vector2 playerDirection;
    private int wallJumpingDirection;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerChatBubble = transform.Find("Bubble").gameObject;
        playerChatBubbleRect = playerChatBubble.transform.Find("TextBubble").GetComponent<RectTransform>();
        playerChatBubbleText = playerChatBubble.transform.Find("TextBubble").GetComponent<TextMeshPro>();
        healthText.SetText(playerHealth.ToString());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Debug.Log(playerHealth);
    }

    // Update is called once per frame
    private void Update()
    {
        playerDirection = userInput.action.ReadValue<Vector2>();
        playerMove();
        playerJump();
        playerWallSliding();
        playerWallJumping();

        if (!isWallSliding)
            playerFaceDirection();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Health"))
        {
            playerHealth += 10;
            Debug.Log(playerHealth);
            checkPlayerHealth();
            Destroy(collision.gameObject);
        }

        if (collision.collider.CompareTag("Spike"))
        {
            if (isInvincible)
                return;

            Debug.Log("Player got hit!");
            playerHealth -= 25;
            checkPlayerHealth();
            playerRigidBody.linearVelocity = new Vector2(wallJumpingDirection * playerMovementSpeed, playerJumpPower);
            StartCoroutine(invincibility());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sign"))
        {
            playerChatBubbleText.SetText("E");
            playerChatBubble.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerChatBubble.SetActive(false);
    }

    private void playerFaceDirection()
    {
        // Change player direction
        if (playerDirection.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
            playerChatBubbleRect.localScale = new Vector2(0.2f, 0.2f);
        }
        else if (playerDirection.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            playerChatBubbleRect.localScale = new Vector2(-0.2f, 0.2f);
        }

        wallJumpingDirection = (int) Mathf.Ceil(playerDirection.x) * -1;
    }

    private void checkPlayerHealth()
    {
        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (playerHealth > 100)
        {
            playerHealth = 100;
            Debug.Log("Player Health Exceeding!");
            Debug.Log(playerHealth);
        }

        healthText.SetText(playerHealth.ToString());
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
            playerRigidBody.linearVelocity = new Vector2(wallJumpingDirection * playerMovementSpeed, playerJumpPower);

            StartCoroutine(midairCooldown());
        }
    }

    private IEnumerator midairCooldown()
    {
        isWallJumping = true;
        yield return new WaitForSeconds(0.25f);
        isWallJumping = false;
    }

    private IEnumerator invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(3);
        isInvincible = false;
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

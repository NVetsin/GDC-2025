<<<<<<< HEAD
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    //SerializeField are used if you want to customize certain elements in the Unity instead keep coming back to here (can be modified while game testing too
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheckLeft;
    [SerializeField] private Transform wallCheckRight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Animator anim;

    // Player main movement setting
    private float speed = 7f;
    private float jumpheight = 10f;
    public float gerakhorizontal;

    // Wall sliding mechanic setting
    private float wallSlidingSpeed = 2f;
    private bool isWallSliding;

    // Wall slide jumping mechanic
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 10f);

    private void Start()
    {
    }

    private void Update()
    {
        isMoving();
        isJumping();
        wallSlide();
        wallJump();

        if (!isWallJumping)
        {
            faceRightLeft();
        }
           
        //Mengaktifkan parameter animator

        anim.SetBool("Run", gerakhorizontal != 0);
        anim.SetBool("Grounded", isGrounded());
    }

    private void faceRightLeft()
    {
        // Changing Sprite when moving the other direction
        if (gerakhorizontal < 0) spriteRenderer.flipX = true;
        else if(gerakhorizontal > 0) spriteRenderer.flipX = false;
    }
    private void isMoving()
    {
        // Handle movement
        gerakhorizontal = Input.GetAxis("Horizontal");
        if (!isWallJumping) body.linearVelocity = new Vector2(gerakhorizontal * speed, body.linearVelocity.y);
    }

    private void isJumping()
    {
        // Handle Jumping
        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpheight);
            anim.SetTrigger("Jump");
        }
    }

    private void wallSlide()
    {
        if (isWall() && !isGrounded() && gerakhorizontal != 0f)
        {
            isWallSliding = true;
            body.linearVelocity = new Vector2(body.linearVelocity.x, Mathf.Clamp( body.linearVelocity.y, -wallSlidingSpeed, float.MaxValue ));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void wallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = (spriteRenderer.flipX == true) ? 1f : -1f;
            wallJumpingCounter = wallJumpingTime;
            
            CancelInvoke(nameof(stopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            body.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (spriteRenderer.flipX != ((wallJumpingDirection == 1) ? false : true))
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }

            Invoke(nameof(stopWallJumping), wallJumpingDuration);
        }
    }

    private void stopWallJumping()
    {
        isWallJumping = false;
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool isWall()
    {
        return Physics2D.OverlapCircle(wallCheckLeft.position, 0.2f, wallLayer) || Physics2D.OverlapCircle(wallCheckRight.position, 0.2f, wallLayer);
    }
=======
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    //SerializeField are used if you want to customize certain elements in the Unity instead keep coming back to here (can be modified while game testing too
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Animator anim;

    // Player main movement setting
    private float speed = 7f;
    private float jumpheight = 10f;
    public float gerakhorizontal;

    // Wall sliding mechanic setting
    private float wallSlidingSpeed = 2f;
    private bool isWallSliding;

    // Wall slide jumping mechanic
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private float isMovingDuration = 0.5f;
    private Vector2 wallJumpingPower = new Vector2(8f, 10f);

    private void Start()
    {
    }

    private void Update()
    {
        Debug.Log("isWallJumping: " + isWallJumping);
        Debug.Log("isWallSliding: " + isWallSliding);
        Debug.Log("isGrounded: " + isGrounded());
        isMoving();
        isJumping();
        wallSlide();
        wallJump();

        if (!isWallJumping)
        {
            faceRightLeft();
        }
           
        //Mengaktifkan parameter animator
        anim.SetBool("Run", gerakhorizontal != 0);
        anim.SetBool("Grounded", isGrounded());
    }

    private void faceRightLeft()
    {
        // Changing Sprite when moving the other direction
        if (gerakhorizontal < 0) spriteRenderer.flipX = true;
        else if(gerakhorizontal > 0) spriteRenderer.flipX = false;
    }
    private void isMoving()
    {
        // Handle movement
        gerakhorizontal = Input.GetAxis("Horizontal");
        if (!isWallJumping) body.linearVelocity = new Vector2(gerakhorizontal * speed, body.linearVelocity.y);
    }

    private void isJumping()
    {
        // Handle Jumping
        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpheight);
            anim.SetTrigger("Jump");
        }
    }

    private void wallSlide()
    {
        if (isWall() && !isGrounded() && gerakhorizontal != 0f)
        {
            isWallSliding = true;
            body.linearVelocity = new Vector2(body.linearVelocity.x, Mathf.Clamp( body.linearVelocity.y, -wallSlidingSpeed, float.MaxValue ));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void wallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = (spriteRenderer.flipX == true) ? 1f : -1f;
            wallJumpingCounter = wallJumpingTime;
            
            CancelInvoke(nameof(stopWallJumping));
            Invoke(nameof(isMoving), isMovingDuration); 
            
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            body.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (spriteRenderer.flipX != ((wallJumpingDirection == 1) ? false : true))
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }

            Invoke(nameof(stopWallJumping), wallJumpingDuration);
            CancelInvoke(nameof(isMoving));
        }
    }

    private void stopWallJumping()
    {
        isWallJumping = false;
    }

    private bool isGrounded()
    {
        Invoke(nameof(isMoving), isMovingDuration);
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool isWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
>>>>>>> 87c9ee819a48b85e7f49e546006c2a0fa1b21489
}
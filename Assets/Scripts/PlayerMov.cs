using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    //SerializeField are used if you want to customize certain elements in the Unity instead keep coming back to here (can be modified while game testing too
   [SerializeField] private float speed;
   [SerializeField] private float jumpheight;
    public float gerakhorizontal;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded; 

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //input movement
        gerakhorizontal = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);

        // Changing Sprite when moving the other direction
        if (gerakhorizontal < -0.01f) spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;

        //lompat
        if (Input.GetKey(KeyCode.Space) && grounded)
            jump();
           
        //Mengaktifkan parameter animator
        anim.SetBool("Run", gerakhorizontal != 0);
        anim.SetBool("Grounded", grounded);
    }

    private void jump()
    {
        //lompat
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpheight);
        anim.SetTrigger("Jump"); 
        grounded = false; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //untuk memberitahu kalo ada di tanah
        if (collision.gameObject.tag == "Ground")
            grounded = true; 
    }
}
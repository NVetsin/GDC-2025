using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    //SerializeField are used if you want to customize certain elements in the Unity instead keep coming back to here (can be modified while game testing too
   [SerializeField] private float speed;
   [SerializeField] private float jumpheight;
    public float gerakhorizontal;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded; 

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //input movement
        gerakhorizontal = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);

        //hadap kanan
        //EXTRANOTE Vector(X,Y,Z) means Scale, numbers are fucked up cuz sprite used is fucked up since the beginning 
        if (gerakhorizontal > 0.01f)
            transform.localScale = new Vector3(0.2659709f, 0.2624599f, 0.2814357f);

        //hadap kiri
        else if (gerakhorizontal < -0.01f)
            transform.localScale = new Vector3(-0.2659709f, 0.2624599f, 0.2814357f);

        //lompat
        if (Input.GetKey(KeyCode.Space) && grounded)
            jump();
           
        //Mengaktifkan parameter animator
        anim.SetBool ("Run", gerakhorizontal != 0);
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
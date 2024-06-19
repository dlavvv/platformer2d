using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpheight;
    [SerializeField] private LayerMask groundLayer;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;

    private void Awake()
    {
        // grab references for rigidbody and animator from game object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed,body.velocity.y);

        // flip the player when moving left-right
        if(horizontalInput > 0.01f)
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);


        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            Jump();

            if(Input.GetKeyDown(KeyCode.Space) && isGrounded()) 
                SoundManager.instance.PlaySound(jumpSound);
        }

        // set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpheight);
        anim.SetTrigger("jump");
    }


    private bool isGrounded()
    {
        // Physics2D.BoxCast(origin,size,angle,direction,distance)
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded();
    }
}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpheight;
    [SerializeField] private LayerMask groundLayer;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        // grab references for rigidbody and animator from game object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed,body.velocity.y);

        // flip player when moving left-right
        if(horizontalInput > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);


        if (Input.GetKey(KeyCode.Space) && isGrounded())
        {
            Jump();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private bool isGrounded()
    {
        // Physics2D.BoxCast(origin,size,angle,direction,distance)
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}

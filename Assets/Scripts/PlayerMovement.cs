using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speedMultiplier;
    [SerializeField] float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D playerBody;
    private Animator playerAnimator;
    private BoxCollider2D boxCollider;
    private float wallJumpCoolDown;
    private float horizontalInput;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        // Horizontal input
        horizontalInput = Input.GetAxis("Horizontal");

        //Flip Character sprite by changing the x scale from 1 to -1
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);


        // Set animator parameters (Make player enter run anime when moving)
        playerAnimator.SetBool("Run", horizontalInput != 0);
        playerAnimator.SetBool("Ground", isGrounded());

        // Wall jumping logic
        if (wallJumpCoolDown > 0.2f)
        {
            // Horizontal Movement
            playerBody.velocity = new Vector2(horizontalInput * speedMultiplier, playerBody.velocity.y);

            // Disable gravity and movement if player is on a wall and not on ground
            if (onWall() && !isGrounded())
            {
                playerBody.gravityScale = 0;
                playerBody.velocity = Vector2.zero;
            }
            // turn on gravity again
            else
            {
                playerBody.gravityScale = 3;
            }

            // Jump if space is pressed
            if (Input.GetKey(KeyCode.Space))
                jump();
        }
        //Replinish colldown
        else
        {
            wallJumpCoolDown += Time.deltaTime;
        }
    }

    // Called every time script isloaded
    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Handles the jump mechanic
    private void jump()
    {
        if (isGrounded()) // Regular jump
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpPower);
            playerAnimator.SetTrigger("Jump");
        }
        else if (onWall() && !isGrounded()) // Wall jumping
        {
            if (horizontalInput == 0)
            {
                playerBody.velocity = new Vector2(
                -Mathf.Sign(transform.localScale.x) * 10, // Opposite directiorn from player facing (1/-1)* multiplier
                0
                );
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                playerBody.velocity = new Vector2(
                -Mathf.Sign(transform.localScale.x) * 3, // Opposite directiorn from player facing (1/-1)* multiplier
                6
                );
            }
            wallJumpCoolDown = 0;
        }

    }


    // When character touches something
    private void OnCollisionEnter2D(Collision2D other)
    {
    }

    private bool isGrounded()
    {
        // Cast a vitual box of RaysTracing rays  
        // The size of the player 
        // Slightly underneath the player (0.1f)
        // And make it point down 
        // Make it look for collisons in a specific layer (groundLayer)
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,
        boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

        // Collider is null when the rays don't collide with anything
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        // Cast a vitual box of RaysTracing rays  
        // The size of the player 
        // Slightly to either side of the player (0.1f)
        // And make it point right and left 
        // Make it look for collisons in a specific layer (wallLayer)
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,
        boxCollider.bounds.size, 0,
        new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);

        // Collider is null when the rays don't collide with anything
        return raycastHit.collider != null;
    }
}

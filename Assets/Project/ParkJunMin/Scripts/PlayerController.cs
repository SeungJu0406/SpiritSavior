using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public float jumpForce = 10f; 
    public float maxJumpTime = 1f; 

    private SpriteRenderer renderer;
    private Rigidbody2D rb;
    private bool isJumping = false;
    private float jumpTime = 0f;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();

        // 점프 키를 누를 때
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            isJumping = true;
            jumpTime = 0f;
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTime < maxJumpTime)
            {
                jumpTime += Time.deltaTime;
            }
        }

        if (Input.GetButtonUp("Jump") && isJumping)
        {
            Jump();
            isJumping = false;
        }
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);

        //if(x < 0)
        //{
        //    renderer.flipX = true;
        //}
        //if(x > 0)
        //{
        //    renderer.flipX = false;
        //}

    }

    private void Jump()
    {
        float jumpAmount = jumpForce * (jumpTime / maxJumpTime);
        rb.velocity = new Vector2(rb.velocity.x, jumpAmount);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null;
    }
}



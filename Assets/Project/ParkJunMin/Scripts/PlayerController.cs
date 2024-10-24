using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public float jumpForce = 10f; 
    public float maxJumpTime = 0.5f; 

    public SpriteRenderer renderer;
    private Rigidbody2D rb;
    private bool _isJumping = false;
    private float _jumpTime = 0f;
    [SerializeField] float _maxSpeed;

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
            _isJumping = true;
            _jumpTime = 0f;
        }

        if (Input.GetButton("Jump") && _isJumping)
        {
            if (_jumpTime < maxJumpTime)
            {
                _jumpTime += Time.deltaTime;
            }
        }

        if (Input.GetButtonUp("Jump") && _isJumping)
        {
            Jump();
            _isJumping = false;
        }
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);

        if(rb.velocity.x > _maxSpeed)
            rb.velocity = new Vector2(_maxSpeed, rb.velocity.y);
        else if(rb.velocity.x < -_maxSpeed)
            rb.velocity = new Vector2(-_maxSpeed, rb.velocity.y);

        if (x < 0)
        {
            renderer.flipX = true;
        }
        if (x > 0)
        {
            renderer.flipX = false;
        }

    }

    private void Jump()
    {
        float jumpAmount = jumpForce * (_jumpTime / maxJumpTime);
        rb.velocity = new Vector2(rb.velocity.x, jumpAmount);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        return hit.collider != null;
    }
}



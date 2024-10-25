using UnityEngine;

public class TrampolineController : MonoBehaviour
{
    [SerializeField] private float jumpSpeed;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
                collision.rigidbody.velocity = Vector2.up * jumpSpeed;
                animator.SetTrigger("boing");
        }
    }
}

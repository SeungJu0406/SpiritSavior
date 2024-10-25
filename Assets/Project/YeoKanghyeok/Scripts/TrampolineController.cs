using UnityEngine;

public class TrampolineController : MonoBehaviour
{
    [SerializeField] private float _jumpSpeed;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
                collision.rigidbody.velocity = Vector2.up * _jumpSpeed;
                _animator.SetTrigger("boing");
        }
    }
}

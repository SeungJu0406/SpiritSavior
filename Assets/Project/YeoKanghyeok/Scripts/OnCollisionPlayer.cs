using UnityEngine;

public class OnCollisionPlayer : MonoBehaviour
{
    [SerializeField] float xPower;
    [SerializeField] float yPower;
    Transform transform;
    private void Start()
    {
        transform = GetComponent<Transform>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(collision.transform.position.x - transform.position.x<0)
            {
                collision.rigidbody.velocity = Vector2.left*xPower;
            }
            else
            {
                collision.rigidbody.velocity = Vector2.right * xPower;
            }
            collision.rigidbody.velocity = Vector2.up * yPower;
        }
    }
}

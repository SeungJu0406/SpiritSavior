using UnityEngine;

public class OnCollisionPlayer : MonoBehaviour
{
    [SerializeField] float xPower;
    [SerializeField] float yPower;
    [SerializeField] PlayerController _player;
    [SerializeField] int attackPower = 1;
    Transform transform;
    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        _player = playerObject.GetComponent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player.playerModel.TakeDamageEvent(attackPower);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    _player.playerModel.TakeDamage(attackPower);
    //}

    //if (collision.gameObject.tag == "Player")
    //{
    //    if(collision.transform.position.x - transform.position.x<0)
    //    {
    //        collision.rigidbody.velocity = Vector2.left*xPower;
    //    }
    //    else
    //    {
    //        collision.rigidbody.velocity = Vector2.right * xPower;
    //    }
    //    collision.rigidbody.velocity = Vector2.up * yPower;
    //}
}


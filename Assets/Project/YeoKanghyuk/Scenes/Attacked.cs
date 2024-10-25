using UnityEngine;

public class Attacked : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Mine")
        {
            Destroy(gameObject);
        }
    }
}

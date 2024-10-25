using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionPlayer : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
    }
}
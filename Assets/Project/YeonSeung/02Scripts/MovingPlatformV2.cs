using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformV2 : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    [SerializeField] float moveSpeed;

   // [SerializeField] GameObject platform;
   // Rigidbody2D rigid;

    private Vector3 nextPosition;
    void Start()
    {
        nextPosition = pointB.position;
        
    }


    void Update()
    {
        
        // transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
        // // A·Î º¹±Í
        // if(transform.position == nextPosition )
        // {
        //     nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
        // }
    }

    public void MovePlatform()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
        // A·Î º¹±Í
        if (transform.position == nextPosition)
        {
            nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
        }
    }
    private void GoPlatform()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            collision.gameObject.transform.parent = transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = null;
        }
    }
}

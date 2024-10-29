using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] MovingPlatformV2 platform2;
    PolygonCollider2D _platform2Collider;
    void Start()
    {
        _platform2Collider = GetComponent<PolygonCollider2D>();
    }


    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            platform2.MovePlatform();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    Transform transform;
    int moveSpeed = 50;
    
    void Start()
    {
        transform = GetComponent<Transform>();

    }


    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 moveDir = new Vector3(x, y);
        if (moveDir == Vector2.zero)
            return;
        
        transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.World);

    }
}

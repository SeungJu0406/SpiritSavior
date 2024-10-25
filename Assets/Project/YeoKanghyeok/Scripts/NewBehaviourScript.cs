using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody2D rg;
    private void Start()
    {
        rg = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Debug.Log(rg.velocity.y);
    }
}

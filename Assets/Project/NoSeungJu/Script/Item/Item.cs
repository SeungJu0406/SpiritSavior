using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            Use(player);
        }
    }

    protected abstract void Use(PlayerController player);
}

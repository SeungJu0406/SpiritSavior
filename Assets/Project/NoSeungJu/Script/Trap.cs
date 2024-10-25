using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] bool _isDisposable;

    private void Start()
    {
        if (_isDisposable) 
        {
            bool keeping = SceneChanger.Instance.CheckKeepingTrap(transform.position);
            if (!keeping) 
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDisposable) 
        {
            if (collision.gameObject.tag == "Player")
            {
                SceneChanger.Instance.SetKeepingTrap(transform.position, false);              
            }
        }
    }
}

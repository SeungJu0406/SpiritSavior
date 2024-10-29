using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("게임에서 일회용인가?")]
    [SerializeField] bool _isDisposable;

    protected virtual void Start()
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

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDisposable) 
        {
            if (collision.gameObject.tag == "Player")
            {
                UnActiveTrap();
            }
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDisposable)
        {
            if (collision.gameObject.tag == "Player")
            {
                UnActiveTrap();
            }
        }
    }

    protected void ActiveTrap()
    {
        SceneChanger.Instance.SetKeepingTrap(transform.position, true);
    }
    protected void UnActiveTrap()
    {
        SceneChanger.Instance.SetKeepingTrap(transform.position, false);
    }
}

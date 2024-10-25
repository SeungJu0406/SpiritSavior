using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrapObject : MonoBehaviour
{
    Transform _fallingPoint;
    WaitForSeconds _lifeTimeDelay;

    Rigidbody2D _rb;
    Coroutine _lifeTimeRoutine;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetFallingPoint(Transform fallingPoint)
    {
        _fallingPoint = fallingPoint;
    }
    /// <summary>
    /// 라이프타임 딜레이 세팅
    /// </summary>
    /// <param name="lifeTimeDelay"></param>
    public void SetLifeTimeDelay(WaitForSeconds lifeTimeDelay)
    {
        _lifeTimeDelay = lifeTimeDelay;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            _lifeTimeRoutine = _lifeTimeRoutine == null ? StartCoroutine(LifeTimeRoutine()) : _lifeTimeRoutine; 
        }
    }

    IEnumerator LifeTimeRoutine()
    {
        yield return _lifeTimeDelay;
        transform.position = _fallingPoint.position;
        transform.rotation = _fallingPoint.rotation;
        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0f;
        gameObject.SetActive(false);
    }
}

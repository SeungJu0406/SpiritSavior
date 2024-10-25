using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenRockPiece : MonoBehaviour
{
    private WaitForSeconds _lifeTIme;

    private Vector2 _originPos;
    private Quaternion _originRot;

    private Coroutine _lifeTimeRoutine;
    /// <summary>
    /// 돌 조각 지속시간 세팅
    /// </summary>
    /// <param name="lifeTime"></param>
    public void SetLifeTime(WaitForSeconds lifeTime)
    {
        _lifeTIme = lifeTime;
    }
    private void OnEnable()
    {
        _originPos = transform.position;
        _originRot = transform.rotation;
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
        yield return _lifeTIme;
        gameObject.SetActive(false);
        transform.position = _originPos;
        transform.rotation = _originRot;
        _lifeTimeRoutine = null;
    }
}

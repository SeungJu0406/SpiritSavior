using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrapObject : MonoBehaviour
{
    [Space(10)]
    int _damage;   

    WaitForSeconds _lifeTimeDelay;

    Coroutine _lifeTimeRoutine;

    bool _canAttack = true;

    /// <summary>
    /// 돌 데미지 세팅
    /// </summary>
    /// <param name="damage"></param>
    public void SetDamage(int damage)
    {
        _damage = damage;
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
        if (collision.gameObject.tag == "Player")
        {
            if (_canAttack)
            {
                Manager.Game.Player.TakeDamage(_damage);
                _canAttack = false;
            }
        }
        else
        {
            _lifeTimeRoutine = _lifeTimeRoutine == null ? StartCoroutine(LifeTimeRoutine()) : _lifeTimeRoutine; 
        }
    }

    /// <summary>
    /// 물체 지면에 떨어진 후 삭제 대기 루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator LifeTimeRoutine()
    {
        _canAttack = false;
        yield return _lifeTimeDelay;
        Destroy(gameObject);
    }
}

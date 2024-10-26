using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrapObject : MonoBehaviour
{
    [Space(10)]
    [SerializeField] int _damage = 1;   

    WaitForSeconds _lifeTimeDelay;

    Coroutine _lifeTimeRoutine;

    bool _canAttack = true;
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
            }
        }
        else
        {
            _canAttack = false;
            _lifeTimeRoutine = _lifeTimeRoutine == null ? StartCoroutine(LifeTimeRoutine()) : _lifeTimeRoutine; 
        }
    }

    IEnumerator LifeTimeRoutine()
    {
        yield return _lifeTimeDelay;
        Destroy(gameObject);
    }
}

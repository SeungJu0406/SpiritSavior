using System;
using System.Collections;
using UnityEngine;

public class OnCollisionPlayer : MonoBehaviour
{
    [SerializeField] int attackPower = 1;

    bool _canHit =true ;
    private void Start()
    {
        Manager.Game.Player.playerModel.OnPlayerDied += StopHit;
        Manager.Game.Player.playerModel.OnPlayerSpawn += StartHit;
    }

    private void StartHit()
    {
        _canHit = true;
    }

    private void StopHit()
    {
        _canHit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Manager.Game.Player.gameObject)
        {
            if(_hitDamagePlayer == null)
                _hitDamagePlayer = StartCoroutine(HitDamagePlayer());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Manager.Game.Player.gameObject)
        {
           if(_hitDamagePlayer != null)
            {
                StopCoroutine(_hitDamagePlayer);
                _hitDamagePlayer = null;
            }
        }
    }

    WaitForSeconds delay = new WaitForSeconds(0.2f);
    Coroutine _hitDamagePlayer;
    IEnumerator HitDamagePlayer()
    {
        while (true)
        {
            if (_canHit) 
                Manager.Game.Player.playerModel.TakeDamageEvent(attackPower);
            yield return delay;
        }
    }
}


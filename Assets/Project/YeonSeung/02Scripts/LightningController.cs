using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    [Header("파란번개")]
    [SerializeField] GameObject LightningB;
    [Header("빨간번개")]
    [SerializeField] GameObject LightningR;
    [Header("힌트 파란")]
    [SerializeField] GameObject glimpseB;
    [Header("힌트 빨간")]
    [SerializeField] GameObject glimpseR;
    [Header("번개 칠 위치")]
    [SerializeField] Transform hitSpot;
    [Header("번개 치는 주기")]
    [SerializeField] float hittingPeriod;
   // [Header("번개 치는 주기")]
   // [SerializeField] float strikeSpeed;  // 스피드가 필요한가 싶은 느낌? 그냥 닿으면 죽는거야~
    [Header("번개 데미지")]
    [SerializeField] int lightningDamage;
    [Header("번개 랜덤")]
    [SerializeField] bool isRandom;


    [SerializeField] PlayerModel.Nature _lightingNature;
    // 파 - 0
    // 빨 - 1
    private GameObject _thisStrike;

    // OnTriggerEnter2D 사용

    PlayerController _player;


    private int _defaultLayer;
    private int _ignorePlayerLayer;

    private bool _canAttack = false;


    private void Awake()
    {
        // Layer 추가
        _defaultLayer = gameObject.layer;
        _ignorePlayerLayer = LayerMask.NameToLayer("Ignore Player");
    }
    void Start()
    {
        if (_player != null)
        {
            return;
        }
        _player = Manager.Game.Player;
        _player.playerModel.OnPlayerTagged += SetActiveCollider;

        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ObjectPool.SpawnObject(LightningB, hitSpot.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
        }

    }


    /*
     * Tag 색상 != 번개색상, 회피가능
     * Tag 색상 == 번개색상, 피해받음
     */

    private void Lightning()
    {
        ObjectPool.SpawnObject(LightningB, hitSpot.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    private void SetActiveCollider(PlayerModel.Nature nature)
    {
        if (nature == _lightingNature)
        {
            
        }
    }
 //   private void OnCollisionEnter2D(Collision2D collision)
 //   {
 //       if (collision.gameObject.tag == "Player")
 //       {
 //           _player.playerModel.TakeDamageEvent(lightningDamage);
 //       }
 //   }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("OnTriggerEnter TEST");
            Lightning();
            _player.playerModel.TakeDamageEvent(lightningDamage);
        }
    }

    private void GenerateRandom()
    {
        int _chance = Random.Range(0, 1);
        if (_chance == 0)
            _thisStrike = LightningB;
            else _thisStrike = LightningR;
    }

}

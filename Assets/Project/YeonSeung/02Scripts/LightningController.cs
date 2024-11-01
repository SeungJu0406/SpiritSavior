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
    [Space(10)]

    [Header("번개 치는 주기:  __초 마다 콰광")]  // 근데 
    [SerializeField] float hittingPeriod;
   // [SerializeField] bool _canRespawn;

    [Header("번개 데미지")]
    [SerializeField] int lightningDamage;
  // 
  //
  //  [SerializeField] Collider2D _hitBox;



    [SerializeField] PlayerModel.Nature _lightingNature;

    // 파 - 0 : 빨 - 1  / 나중에 물어보고 다른스타일 기믹하면
    [Header("번개 랜덤")]
    [SerializeField] bool isRandom;
    private GameObject _thisStrike;

    // OnTriggerEnter2D 사용

    PlayerController _player;


    private int _defaultLayer;
    private int _ignorePlayerLayer;

    private bool _canAttack = true;

    private Coroutine _PeriodicStrike;

    WaitForSeconds _respawnDelay;
    WaitForSeconds _poolDelay;
    float _poolDelayTime = 0.5f;

 // Tag 색상 != 번개색상, 회피가능
 // Tag 색상 == 번개색상, 피해받음



    

    void Awake()
    {
        // Layer 추가
        _defaultLayer = gameObject.layer;
        _ignorePlayerLayer = LayerMask.NameToLayer("Ignore Player");
        _respawnDelay = new WaitForSeconds(hittingPeriod);
        _poolDelay = new WaitForSeconds(_poolDelayTime);
      //  _hitBox = GetComponent<Collider2D>();
    }
    void Start()
    {
        if (_player != null)
        {
            return;
        }
        _player = Manager.Game.Player;
        _player.playerModel.OnPlayerTagged += SetActiveCollider;
        SetActiveCollider(_player.playerModel.curNature);
      //  _hitBox.enabled = false;
    }
    private void OnEnable()
    {
        if (Manager.Game == null) return;
        _player = Manager.Game.Player;
        _player.playerModel.OnPlayerTagged += SetActiveCollider;
        SetActiveCollider(_player.playerModel.curNature);
    }

    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Q))
        {
            Lightning();
        }

        if (_PeriodicStrike == null)
        {
            _PeriodicStrike = StartCoroutine(RespawnRoutine());
            Debug.Log("코루틴끝");
         //   Lightning();
        }
       // else if (_PeriodicStrike != null)
       // {
       //     StopCoroutine(_PeriodicStrike);
       //     
       // }
       // StopCoroutine 구현해야됨
        
    }
    IEnumerator RespawnRoutine()
    {
       while(true)
        {
            Debug.Log("Coroutine번개");
            Lightning();
            yield return _respawnDelay;

        }
       
    }
    private void BoxOn()
    {
      //  _hitBox.enabled = true;
    }
    private void BoxOff()
    {
       // _hitBox.enabled = false;
    }

    private void SetActiveCollider(PlayerModel.Nature nature)
    {
        if (nature == _lightingNature)
        {
            _canAttack = true;
        }
        else if (nature != _lightingNature)
        {
            _canAttack = false;
        }
    }
    private void Lightning()
    {
        ObjectPool.SpawnObject(LightningB, hitSpot.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(_canAttack)
            {
                Debug.Log("OnTriggerEnter TEST");
                // Lightning();
                _player.playerModel.TakeDamageEvent(lightningDamage);
            }
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

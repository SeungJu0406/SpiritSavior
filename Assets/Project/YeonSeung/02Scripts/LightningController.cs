using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    [Header("번개 선택 \n1 = 파랑 / 2 = 빨강")]
    [SerializeField] int selectLightning;
    private GameObject _lightning;

    [Header("파란번개")]
    [SerializeField] GameObject LightningB;
    [Header("빨간번개")]
    [SerializeField] GameObject LightningR;
    [Header("힌트 파란")]
    [SerializeField] GameObject glimpseB;
    [Header("힌트 빨간")]
    [SerializeField] GameObject glimpseR;
    private GameObject _glimpse;
    [Header("번개 칠 위치")]
    [SerializeField] Transform hitSpot;
    [Space(10)]

    [Header("번개 치는 주기:  __초 마다 콰광")]  // 근데 
    [SerializeField] float hittingPeriod;


   // [Header("번개 데미지")]
   // [SerializeField] int lightningDamage;
    // 
    //
    //  [SerializeField] Collider2D _hitBox;



    [SerializeField] PlayerModel.Nature _lightningNature;

    // 파 - 0 : 빨 - 1  / 나중에 물어보고 다른스타일 기믹하면
    // [Header("번개 랜덤")]
    // [SerializeField] bool isRandom;
    // private GameObject _thisStrike;


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
        if (selectLightning == 1)
        {
            _lightning = LightningB;
            _glimpse = glimpseB;
            _lightningNature = PlayerModel.Nature.Blue;
        }
        else if (selectLightning == 2)
        {
            _lightning = LightningR;
            _glimpse = glimpseR;
            _lightningNature = PlayerModel.Nature.Red;
        }
        _glimpse.SetActive(true);

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
        // 테스트용
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     Lightning();
        // }

        if (_PeriodicStrike == null)
        {
            _PeriodicStrike = StartCoroutine(RespawnRoutine());
           // Debug.Log("코루틴끝");
            //  Lightning();
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
        while (true)
        {
          //  Debug.Log("Coroutine번개");
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
        if (nature == _lightningNature)
        {
            _canAttack = true;
        }
        else if (nature != _lightningNature)
        {
            _canAttack = false;
        }
    }
    private void Lightning()
    {
        ObjectPool.SpawnObject(_lightning, hitSpot.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
        Manager.Sound.PlaySFX(Manager.Sound.Data.ThunderSound);
    }

 //   private void OnTriggerEnter2D(Collider2D collision)
 //   {
 //       if (collision.gameObject.tag == "Player")
 //       {
 //           if (_canAttack)
 //           {
 //               Debug.Log("OnTriggerEnter TEST");
 //               // Lightning();
 //               _player.playerModel.TakeDamageEvent(lightningDamage);
 //           }
 //       }
 //   }


    /* 생각만하고 아직 미사용
    private void GenerateRandom()
    {
        int _chance = Random.Range(0, 1);
        if (_chance == 0)
            _thisStrike = LightningB;
        else _thisStrike = LightningR;
    }
    */
}

using UnityEngine;

public class Lightning : MonoBehaviour
{
    [Header("번개 Object")]
    [SerializeField] GameObject lightning;

    // [SerializeField] bool _canRespawn;

    [Header("번개 데미지")]
    [SerializeField] int lightningDamage;


    [SerializeField] Collider2D _hitBox;



    [SerializeField] PlayerModel.Nature _lightingNature;



    // OnTriggerEnter2D 사용

    PlayerController _player;


    private int _defaultLayer;
    private int _ignorePlayerLayer;

    private bool _canAttack = true;

    //  private Coroutine _PeriodicStrike;
    //
    //  WaitForSeconds _respawnDelay;
    //  WaitForSeconds _poolDelay;
    //  float _poolDelayTime = 0.5f;





    void Awake()
    {
        // Debug.Log("Lightning AWAKE");
        // Layer 추가
        _defaultLayer = gameObject.layer;
        _ignorePlayerLayer = LayerMask.NameToLayer("Ignore Player");
        //   _respawnDelay = new WaitForSeconds(hittingPeriod);
        //   _poolDelay = new WaitForSeconds(_poolDelayTime);
        //   _hitBox = GetComponent<Collider2D>();
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
        _hitBox.enabled = true;
        // _hitBox.enabled = false;
    }
    private void OnEnable()
    {
        if (Manager.Game == null) return;
        _player = Manager.Game.Player;
        _player.playerModel.OnPlayerTagged += SetActiveCollider;
        SetActiveCollider(_player.playerModel.curNature);
        _hitBox.enabled = true;
    }

    private void Update()
    {

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


    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.tag == "Player")
    //     {
    //         if (_canAttack)
    //         {
    //             Debug.Log("번개소환번개");
    //             // Lightning();
    //             _player.playerModel.TakeDamageEvent(lightningDamage);
    //         }
    //     }
    // }
    //

    // _player.playerModel.curNature == _lightingNature
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && _canAttack)
        {

            Debug.Log("번개Enter");
            // _hitBox.enabled = true;
            // Lightning();
            _player.playerModel.TakeDamageEvent(lightningDamage);

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && _canAttack)
        {

           // Debug.Log("번개Exit");
           // _hitBox.enabled = false;

        }
    }

}

using Project.ParkJunMin.Scripts;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [Header("���� Object")]
    [SerializeField] GameObject lightning;

    // [SerializeField] bool _canRespawn;

    [Header("���� ������")]
    [SerializeField] int lightningDamage;


    [SerializeField] Collider2D _hitBox;



    [SerializeField] PlayerModel.Nature _lightingNature;
    


    // OnTriggerEnter2D ���

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
        InitLayer();
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
    // ������� / �̺�Ʈ �����ϴ°� ����Ǯ�ҋ� �ؾߵ� (-=SetActiveCollider);


    private void SetActiveCollider(PlayerModel.Nature nature)
    {
        if (nature == _lightingNature)
        {
            SetDefaultLayer();
            _canAttack = true;
            
        }
        else if (nature != _lightingNature)
        {
            SetIgnorePlayerLayer();
            _canAttack = false;
           
        }
    }
    private void OnParticleSystemStopped()
    {
        // Debug.Log("������� �׽�Ʈ, ��ƼŬ ������");
        _player.playerModel.OnPlayerTagged -= SetActiveCollider;
        ObjectPool.ReturnObjectPool(gameObject);
    }
    void InitLayer()
    {
        _defaultLayer = gameObject.layer;
        _ignorePlayerLayer = LayerMask.NameToLayer("Ignore Player");
    }
    void SetDefaultLayer()
    {
        gameObject.layer = _defaultLayer;
    }
    void SetIgnorePlayerLayer()
    {
        gameObject.layer = _ignorePlayerLayer;
    }


    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.tag == "Player")
    //     {
    //         if (_canAttack)
    //         {
    //             Debug.Log("������ȯ����");
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

            // Debug.Log("����Enter");
            // _hitBox.enabled = true;
            // Lightning();
            _player.playerModel.TakeDamageEvent(lightningDamage);

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && _canAttack)
        {

           // Debug.Log("����Exit");
           // _hitBox.enabled = false;

        }
    }

}

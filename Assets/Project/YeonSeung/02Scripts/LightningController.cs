using System.Collections;
using Project.ParkJunMin.Scripts;
using Unity.VisualScripting;
using UnityEngine;

public class LightningController : MonoBehaviour
{
    [Header("���� ���� \n1 = �Ķ� / 2 = ����")]
    [SerializeField] int selectLightning;
    private GameObject _lightning;

    [Header("�Ķ�����")]
    [SerializeField] GameObject LightningB;
    [Header("��������")]
    [SerializeField] GameObject LightningR;
    [Header("��Ʈ �Ķ�")]
    [SerializeField] GameObject glimpseB;
    [Header("��Ʈ ����")]
    [SerializeField] GameObject glimpseR;
    private GameObject _glimpse;
    [Header("���� ĥ ��ġ")]
    [SerializeField] Transform hitSpot;
    [Space(10)]

    [Header("���� ġ�� �ֱ�:  __�� ���� �Ɽ")]  // �ٵ� 
    [SerializeField] float hittingPeriod;


   // [Header("���� ������")]
   // [SerializeField] int lightningDamage;
    // 
    //
    //  [SerializeField] Collider2D _hitBox;



    [SerializeField] PlayerModel.Nature _lightningNature;

    // �� - 0 : �� - 1  / ���߿� ����� �ٸ���Ÿ�� ����ϸ�
    // [Header("���� ����")]
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

    // Tag ���� != ��������, ȸ�ǰ���
    // Tag ���� == ��������, ���ع���





    void Awake()
    {
        // Layer �߰�
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
        // �׽�Ʈ��
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     Lightning();
        // }

        if (_PeriodicStrike == null)
        {
            _PeriodicStrike = StartCoroutine(RespawnRoutine());
           // Debug.Log("�ڷ�ƾ��");
            //  Lightning();
        }
        // else if (_PeriodicStrike != null)
        // {
        //     StopCoroutine(_PeriodicStrike);
        //     
        // }
        // StopCoroutine �����ؾߵ�

    }
    IEnumerator RespawnRoutine()
    {
        while (true)
        {
          //  Debug.Log("Coroutine����");
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


    /* �������ϰ� ���� �̻��
    private void GenerateRandom()
    {
        int _chance = Random.Range(0, 1);
        if (_chance == 0)
            _thisStrike = LightningB;
        else _thisStrike = LightningR;
    }
    */
}

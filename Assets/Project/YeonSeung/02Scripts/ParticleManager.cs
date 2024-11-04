using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;

    PlayerController _player;

    // float x;
    // float y;
    // float z;

    Transform _pBottom;


    // Transform _collisionPos;

    Vector3 _collisionPos;


    [Header("플레이어 관련 파티클")]
    [Space(5)]
    [Header("달리기 FX ")]
    [SerializeField] GameObject runFX;
    [Header("점프 FX ")]
    [SerializeField] GameObject jumpFX;
    [Header("이단 점프 FX ")]
    [SerializeField] GameObject dJumpFX;
    [Header("피격 FX ")]
    [SerializeField] GameObject hitFX;
    [Header("체력회복 FX ")]
    [SerializeField] GameObject healFX;
    [Header("대시효과 FX ")]
    [SerializeField] GameObject dashFX;
    [Space(20)]
    [Header("기능언락 효과 FX")]
    [Space(5)]
    [Header("태그 언락FX")]
    [SerializeField] GameObject unlockTagFX;
    [Header("벽점프 언락FX ")]
    [SerializeField] GameObject unlockWallJumpFX;
    [Header("더블점프 언락FX ")]
    [SerializeField] GameObject unlockDoubleJumpFX;
    [Header("대시 언락FX ")]
    [SerializeField] GameObject unlockDashFX;

    [Header("리스폰 FX ")]
    [SerializeField] GameObject spawnFX;
    [Space(20)]
    [Header("다른파티클들")]
    [Space(5)]



    [Header("잔디 밟는 FX ")]
    [SerializeField] GameObject GrassFX;

    // public Transform location;



    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);



        //this.location = transform;
    }


    #region 함수리스트
    public void PlayRunFX()
    {
        ObjectPool.SpawnObject(runFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayJumpFX()
    {
        ObjectPool.SpawnObject(jumpFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayDoubleJumpFX()
    {
        ObjectPool.SpawnObject(dJumpFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayHitFX()
    {
        ObjectPool.SpawnObject(hitFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayHealFX()
    {
        ObjectPool.SpawnObject(healFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayDashFX()
    {
        ObjectPool.SpawnObject(dashFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlaySpawnFX()
    {
        ObjectPool.SpawnObject(spawnFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }

    // 해금 FX
    public void PlayUnlockTagFX()
    {
        ObjectPool.SpawnObject(unlockTagFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayUnlockWallJumpFX()
    {
        ObjectPool.SpawnObject(unlockWallJumpFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayUnlockDoubleJumpFX()
    {
        ObjectPool.SpawnObject(unlockDoubleJumpFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayUnlockDashFX()
    {
        ObjectPool.SpawnObject(unlockDashFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }



    // 다른 파티클들
    public void PlayGrassFX()
    {
        ObjectPool.SpawnObject(GrassFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }



    #endregion


    void Start()
    {
        SubscribeEvents();
        _player = Manager.Game.Player;
        // _pBottom.y = _player.transform.position.y;
        _pBottom = _player.bottomPivot;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("SpaceBar EventTest");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _collisionPos = collision.transform.position;
        }
    }



    void SubscribeEvents()
    {
        Manager.Game.Player.playerModel.OnPlayerRan += PlayRunFX;
        Manager.Game.Player.playerModel.OnPlayerJumped += PlayJumpFX;
        Manager.Game.Player.playerModel.OnPlayerDoubleJumped += PlayDoubleJumpFX;
        Manager.Game.Player.playerModel.OnPlayerDamageTaken += PlayHitFX;
        Manager.Game.Player.playerModel.OnPlayerHealth += PlayHealFX;
        Manager.Game.Player.playerModel.OnPlayerDashed += PlayDashFX;
        Manager.Game.Player.playerModel.OnAbilityUnlocked += UpdateSkillFX;
        Manager.Game.Player.playerModel.OnPlayerSpawn += PlaySpawnFX;
    }

    void UpdateSkillFX(PlayerModel.Ability ability)
    {
        switch (ability)
        {
            case PlayerModel.Ability.Tag:
                PlayUnlockTagFX();
                break;
            case PlayerModel.Ability.WallJump:
                PlayUnlockWallJumpFX();
                break;
            case PlayerModel.Ability.DoubleJump:
                PlayUnlockDoubleJumpFX();
                break;
            case PlayerModel.Ability.Dash:
                PlayUnlockDashFX();
                break;
            default:
                break;
        }
    }
}

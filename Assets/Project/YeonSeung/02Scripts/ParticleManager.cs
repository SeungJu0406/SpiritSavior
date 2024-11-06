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
    [Header("버섯 밟는 FX ")]
    [SerializeField] GameObject trapomlineFX;
    TrampolineController _trampoline;

    // LandingSound, WallclimbingSound

    /* WalkingSounds & Effect Loop w/Coroutine
    private Coroutine loopWalk;
    IEnumerator LoopWalk()
    {
        while (true)
        {
            Debug.Log("Loop WALKING SOUND");
            ObjectPool.SpawnObject(runFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
            // Manager.Sound.PlaySFX(Manager.Sound.Data.RunningSound);
            yield return new WaitForSeconds(.4f);
        }
    }
    */

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }


    #region 함수리스트
    public void PlayRunFX()
    {
        ObjectPool.SpawnObject(runFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
        Manager.Sound.PlaySFX(Manager.Sound.Data.RunningSound);

        // loopWalk = StartCoroutine(LoopWalk());
        // StopCoroutine(loopWalk);

    }
    public void PlayJumpFX()
    {
        Manager.Sound.PlaySFX(Manager.Sound.Data.JumpSound);
        ObjectPool.SpawnObject(jumpFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayDoubleJumpFX()
    {
        Manager.Sound.PlaySFX(Manager.Sound.Data.DoubleJumpSound);
        ObjectPool.SpawnObject(dJumpFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayHitFX()
    {
        Manager.Sound.PlaySFX(Manager.Sound.Data.HitSound);
        ObjectPool.SpawnObject(hitFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayHealFX()
    {
        Manager.Sound.PlaySFX(Manager.Sound.Data.HealSound);
        ObjectPool.SpawnObject(healFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayDashFX()
    {
        Manager.Sound.PlaySFX(Manager.Sound.Data.DashSound);
        ObjectPool.SpawnObject(dashFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlaySpawnFX()
    {
        Manager.Sound.PlaySFX(Manager.Sound.Data.SpawnSound);
        ObjectPool.SpawnObject(spawnFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }

    // 해금 FX
    public void PlayUnlockTagFX()
    {
        Manager.Sound.PlaySFX(Manager.Sound.Data.UnlockTagSound);
        ObjectPool.SpawnObject(unlockTagFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayUnlockWallJumpFX()
    {
        Manager.Sound.PlaySFX(Manager.Sound.Data.UnlockWallJumpSound);
        ObjectPool.SpawnObject(unlockWallJumpFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayUnlockDoubleJumpFX()
    {
        Manager.Sound.PlaySFX(Manager.Sound.Data.UnlockDoubleJumpSound);
        ObjectPool.SpawnObject(unlockDoubleJumpFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayUnlockDashFX()
    {
        Manager.Sound.PlaySFX(Manager.Sound.Data.UnlockDashSound);
        ObjectPool.SpawnObject(unlockDashFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }

    // 추가 사운드
    public void PlayWallGrabbedFX()
    {
        Manager.Sound.PlaySFX(Manager.Sound.Data.WallClimbSound);
    }
    public void PlayWallSlidFX()
    {
        Manager.Sound.PlaySFX(Manager.Sound.Data.WallSlidSound);
    }
    public void PlayWallJumpFX()
    {
        Manager.Sound.PlaySFX(Manager.Sound.Data.WallJumpSound);
    }
    public void PlayerLandFX()
    {
        Manager.Sound.PlaySFX(Manager.Sound.Data.LandSound);
    }
    public void PlayWakeUpFX()
    {
        Manager.Sound.PlaySFX(Manager.Sound.Data.WakeUpSound);
    }


    // 다른 파티클들
    public void PlayGrassFX()
    {
        // ParticleManager.Instance.PlayGrassFX();
        ObjectPool.SpawnObject(GrassFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }

    public void PlayTrampolineFX()
    {
        // ParticleManager.Instance.PlayTrampolineFX();
        ObjectPool.SpawnObject(trapomlineFX, _pBottom.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }

    #endregion


    void Start()
    {
        SubscribeEvents();
        _player = Manager.Game.Player;
        // _pBottom.y = _player.transform.position.y;
        _pBottom = _player.bottomPivot;
        // _trampoline = GetComponent<TrampolineController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Debug.Log("SpaceBar EventTest");
        }
    }
    private void OnParticleSystemStopped()
    {
        ObjectPool.ReturnObjectPool(gameObject);
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

        Manager.Game.Player.playerModel.OnPlayerWallGrabed += PlayWallGrabbedFX;
        Manager.Game.Player.playerModel.OnPlayerWallSlided += PlayWallSlidFX;
        Manager.Game.Player.playerModel.OnPlayerWallJumped += PlayWallJumpFX;
        Manager.Game.Player.playerModel.OnPlayerLanded += PlayerLandFX;
        Manager.Game.Player.playerModel.OnPlayerWakedUp += PlayWakeUpFX;

        // Climbing
        // landing
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    public static PlayerParticles Instance;


    [Header("달리기 FX ")]
    [SerializeField] GameObject runFX;
    [Header("점프 FX ")]
    [SerializeField] GameObject jumpFX;
    [Header("이단 점프 FX ")]
    [SerializeField] GameObject dJumpFX;
    [Header("피격 FX ")]
    [SerializeField] GameObject hitFX;
    [Header("회복 FX ")]
    [SerializeField] GameObject healFX;

    [Header("잔디 밟는 FX ")]
    [SerializeField] GameObject GrassFX;

    public Transform location;

    PlayerModel pModel;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);


        this.location = transform;
    }


    #region 함수리스트
    public void PlayRunFX()
    {
        ObjectPool.SpawnObject(runFX, this.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayJumpFX()
    {
        ObjectPool.SpawnObject(jumpFX, transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayDoubleJumpFX()
    {
        ObjectPool.SpawnObject(dJumpFX, this.transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayHitFX()
    {
        ObjectPool.SpawnObject(hitFX, transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayHealFX()
    {
        ObjectPool.SpawnObject(healFX, transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }
    public void PlayGrassFX()
    {
        ObjectPool.SpawnObject(GrassFX, transform.position, transform.rotation, ObjectPool.PoolType.ParticleSystem);
    }

    #endregion


    void Start()
    {
        SubscribeEvents();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("SpaceBar EventTest");
        }
    }
    void SubscribeEvents()
    {
        Manager.Game.Player.playerModel.OnPlayerRan += PlayRunFX;
        Manager.Game.Player.playerModel.OnPlayerJumped += PlayJumpFX;
        Manager.Game.Player.playerModel.OnPlayerDoubleJumped += PlayDoubleJumpFX;
        Manager.Game.Player.playerModel.OnPlayerDamageTaken += PlayHitFX;
        Manager.Game.Player.playerModel.OnPlayerHealth += PlayHealFX;
    }
}

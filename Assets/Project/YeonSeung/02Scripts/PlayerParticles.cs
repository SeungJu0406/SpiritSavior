using System.Collections;
using System.Collections.Generic;
using Project.ParkJunMin.Scripts;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    public static PlayerParticles Instance;


    [Header("�޸��� FX ")]
    [SerializeField] GameObject runFX;
    [Header("���� FX ")]
    [SerializeField] GameObject jumpFX;
    [Header("�̴� ���� FX ")]
    [SerializeField] GameObject dJumpFX;
    [Header("�ǰ� FX ")]
    [SerializeField] GameObject hitFX;
    [Header("ȸ�� FX ")]
    [SerializeField] GameObject healFX;

    [Header("�ܵ� ��� FX ")]
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


    #region �Լ�����Ʈ
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
